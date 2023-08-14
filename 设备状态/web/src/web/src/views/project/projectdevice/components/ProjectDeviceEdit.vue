<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="110px" :model="form" :rules="rules">
      <el-form-item label="设备编号" prop="deviceNo">
        <el-input v-model.trim="form.deviceNo" placeholder="请输入设备编号" />
      </el-form-item>
      <el-form-item label="设备名称" prop="deviceName">
        <el-input v-model.trim="form.deviceName" placeholder="请输入设备名称" />
      </el-form-item>
      <el-form-item label="项目名称" prop="projectID">
        <rw-project
          v-model="form.projectID"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="产品系列" prop="productLine">
        <el-select
          v-model="form.productLine"
          placeholder="请选择产品系列"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in productLineOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="数量" prop="qty">
        <el-input-number
          v-model="form.qty"
          :max="999999999"
          :min="1"
          :precision="0"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="单价" prop="devicePrice">
        <el-input-number
          v-model="form.devicePrice"
          :max="999999999"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="单位" prop="deviceUnit">
        <el-input v-model.trim="form.deviceUnit" placeholder="请输入单位" />
      </el-form-item>
      <el-form-item label="设备使用地点" prop="devicePlaceOfUse">
        <el-input
          v-model="form.devicePlaceOfUse"
          placeholder="请输入使用地点"
        />
      </el-form-item>
      <el-form-item label="要求交付日期" prop="deliverDate">
        <el-date-picker
          v-model="form.deliverDate"
          placeholder="请选择要求交付日期"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="设备主要功能" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          placeholder="输入设备主要功能"
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
    ProductLine,
    getProjectCode,
  } from '@/api/projectdevice'
  import RwProject from '@/plugins/RwProject'
  import moment from 'moment'
  //import { GetProjectPayment } from '@/api/projectretmoney'

  export default defineComponent({
    name: 'ProjectDeviceEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        formRef: null,
        form: {
          deviceNo: '',
          deviceName: '',
          qty: 1,
          devicePrice: 0,
          remark: '',
        },
        productLineOptions: [],
        projectCodeOptions: [],
        Searchoptions: [],
        rules: {
          deviceNo: [
            { required: true, trigger: 'blur', message: '请填写设备编号' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          deviceName: [
            { required: true, trigger: 'blur', message: '请填写设备名称' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          projectID: [
            { required: true, trigger: 'blur', message: '请选择项目名称' },
          ],
          productLine: [
            { required: true, trigger: 'blur', message: '请选择产品系列' },
          ],
          qty: [{ required: true, trigger: 'blur', message: '请输入数量' }],
          deviceUnit: [
            { required: true, trigger: 'blur', message: '请输入单位' },
          ],
          devicePrice: [
            {
              required: true,
              trigger: 'blur',
              message: '请输入单价',
            },
          ],
          deliverDate: [
            {
              required: true,
              trigger: 'change',
              message: '请选择要求交付日期',
            },
          ],
          devicePlaceOfUse: [
            {
              max: 23,
              trigger: 'blur',
              message: '设备使用地点不能超过23个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
        loading: false,
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
          state.title = '添加'
        } else {
          state.title = '编辑'
          state.form = Object.assign({}, row)
          state.projectCodeOptions = [
            { label: row.projectName, value: row.projectID },
          ]
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
      //远程搜索合同
      const fetchDa = async (query) => {
        //console.log(query)
        if (query) {
          state.loading = true
          const contractIDList = await GetDeliveryPayment({ name: query })
          setTimeout(() => {
            state.loading = false
            //console.log(contractIDList.data)
            state.deliveryIDOptions = contractIDList.data
          }, 200)
        } else {
          state.deliveryIDOptions = []
        }
      }
      //产品系列
      const GetProjectClassData = async () => {
        const ProjectClassList = await ProductLine()
        state.productLineOptions = ProjectClassList.data
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (valid) {
              if (state.form.projectID.length != undefined) {
                state.form.projectID =
                  state.form.projectID[state.form.projectID.length - 1]
              }
              state.form.deliverDate = moment(state.form.deliverDate).format(
                'YYYY-MM-DD'
              )
              if (state.form.id > 0) {
                await doEdit(state.form)
                $baseMessage('修改成功', 'success', 'vab-hey-message-success')
                emit('fetch-data')
                close()
              } else {
                await doAdd(state.form)
                $baseMessage('添加成功', 'success', 'vab-hey-message-success')
                emit('fetch-data')
                close()
              }
            }
          }
        })
      }
      onMounted(() => {
        //fetchData()
        //fetchDa()
        GetProjectClassData()
        fetchProCodeData()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        //fetchData,
        fetchDa,
        ProductLine,
        GetProjectClassData,
        fetchProCodeData,
      }
    },
  })
</script>
