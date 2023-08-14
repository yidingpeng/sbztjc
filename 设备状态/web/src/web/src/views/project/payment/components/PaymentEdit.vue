<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-form-item label="项目编号" prop="pt_Id">
        <rw-project
          v-model="form.pt_Id"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="回款日期：" prop="returnDate">
        <el-date-picker
          v-model="form.returnDate"
          :disabled-date="disabledDate"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="回款金额：" prop="payment_Cash">
        <!-- <el-input
          v-model="form.payment_Cash"
          oninput="value=value.replace(/[^0-9.]/g,'')"
        /> -->
        <el-input-number
          v-model="form.payment_Cash"
          :max="999999999"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="回款类型：" prop="returnType">
        <el-select
          v-model="form.returnType"
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in ReturnTypeRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="回款方式：" prop="returnWay">
        <el-select
          v-model="form.returnWay"
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in ReturnWayRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          show-word-limit
          :style="{ width: '100%' }"
          type="textarea"
        />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import {
    doEdit,
    doAdd,
    GetReturnTypeList,
    GetReturnWayList,
    getProjectCode,
  } from '@/api/payment'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'PaymentEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      let isAdd = false
      const state = reactive({
        formRef: null,
        form: {
          deliveryID: '',
          pt_Id: [],
          payment_Time: '',
          ct_Id: [],
          remark: '',
        },
        title: '',
        ReturnTypeRadio: [],
        ReturnWayRadio: [],
        Searchoptions: [],
        ptOptions: [],
        rules: {
          deliveryID: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择交付信息单据号',
            },
          ],
          pt_Id: [
            { required: true, trigger: 'blur', message: '请选择项目名称' },
          ],
          ct_Id: [
            { required: true, trigger: 'blur', message: '请选择合同编码' },
          ],
          payment_Cash: [
            { required: true, trigger: 'blur', message: '请输入已回款总额' },
          ],
          returnType: [
            { required: true, trigger: 'blur', message: '请选择回款类型' },
          ],
          returnWay: [
            { required: true, trigger: 'blur', message: '请选择回款方式' },
          ],
          returnDate: [
            { required: true, trigger: 'blur', message: '请选择回款日期' },
          ],
        },
        dialogFormVisible: false,
        disabledDate(time) {
          return time.getTime() >= Date.now()
        },
      })
      const fetchProCodeData = async () => {
        const SearchoptionsData = await getProjectCode()
        state.Searchoptions = SearchoptionsData.data
        state.Searchoptions.forEach((item) => {
          item.projectCode = `${item.projectCode} ${item.projectName}`
          if (item.children != undefined) {
            item.children.forEach((item) => {
              item.projectCode = `${item.projectCode} ${item.projectName}`
            })
          }
        })
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加回款'
          state.form.payment_Cash = null
          isAdd = true
        } else {
          if (row > 0) {
            state.title = '添加回款'
            isAdd = true
            state.form.pt_Id = row
          } else {
            isAdd = false
            state.title = '编辑回款'
            state.form = JSON.parse(JSON.stringify(row))
          }
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.pt_Id.length != undefined) {
              state.form.pt_Id = state.form.pt_Id[state.form.pt_Id.length - 1]
            }
            const submit = isAdd ? doAdd : doEdit
            const { msg } = await submit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      //回款类型
      const GetReturnTypeData = async () => {
        const ReturnType = await GetReturnTypeList()
        state.ReturnTypeRadio = ReturnType.data
      }

      //回款方式
      const GetReturnWayData = async () => {
        const ReturnWay = await GetReturnWayList()
        state.ReturnWayRadio = ReturnWay.data
      }

      onMounted(() => {
        GetReturnTypeData()
        GetReturnWayData()
        fetchProCodeData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetReturnTypeData,
        fetchProCodeData,
        GetReturnWayData,
      }
    },
  })
</script>
