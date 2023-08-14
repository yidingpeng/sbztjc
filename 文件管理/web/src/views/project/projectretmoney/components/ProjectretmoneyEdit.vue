<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="140px" :model="form" :rules="rules">
      <el-form-item label="项目名称" prop="projectID">
        <rw-project
          v-model="form.projectID"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="质保金比例（%）" prop="retMoneyProportion">
        <!-- <el-input v-model="form.retMoneyProportion" /> -->
        <el-input-number
          v-model="form.retMoneyProportion"
          :max="999999999"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="质保金金额" prop="retMoneyAmount">
        <!-- <el-input v-model="form.retMoneyAmount" /> -->
        <el-input-number
          v-model="form.retMoneyAmount"
          :max="999999999"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="质保金期限" prop="WarrantyPeriodOne">
        <el-date-picker
          v-model="form.WarrantyPeriodOne"
          :disabled-date="disabledDate"
          placeholder="开始时间"
          :style="{ width: '50%', borderRight: '0' }"
          type="date"
        />
        <el-date-picker
          v-model="form.WarrantyPeriodTwo"
          :disabled-date="disabledDate1"
          placeholder="结束时间"
          :style="{ width: '50%', borderLeft: '0' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="质保期限(月)" prop="expirationDate">
        <el-select
          v-model="form.expirationDate"
          filterable
          placeholder="请选择质保期限(月)"
          :style="{ width: '100%' }"
        >
          <el-option label="12个月" value="12个月" />
          <el-option label="24个月" value="24个月" />
        </el-select>
      </el-form-item>
      <el-form-item label="已到期质保金金额" prop="alrExpirationMoney">
        <!-- <el-input v-model="form.alrExpirationMoney" /> -->
        <el-input-number
          v-model="form.alrExpirationMoney"
          :max="form.retMoneyAmount"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          placeholder="请输入备注"
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
    GetProjectPayment,
    getProjectCode,
  } from '@/api/projectretmoney'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'ProjectretmoneyEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        disabledDate(time) {
          if (state.form.WarrantyPeriodTwo != '') {
            return (
              time.getTime() > new Date(state.form.WarrantyPeriodTwo).getTime()
            )
          } else {
            return false
          }
        },
        disabledDate1(time) {
          if (state.form.WarrantyPeriodOne != '') {
            return (
              time.getTime() < new Date(state.form.WarrantyPeriodOne).getTime()
            )
          } else {
            return false
          }
        },
        formRef: null,
        form: {
          deliveryID: [],
          retMoneyProportion: '',
          retMoneyAmount: '',
          WarrantyPeriodObject: ['', ''],
          WarrantyPeriodOne: '',
          WarrantyPeriodTwo: '',
          expirationDate: '',
          alrExpirationMoney: '',
          remark: '',
          projectID: [],
        },
        Searchoptions: [],
        projectCodeOptions: [],
        rules: {
          deliveryID: [
            { required: true, trigger: 'blur', message: '请选择交付单据号' },
          ],
          projectID: [
            { required: true, trigger: 'blur', message: '请选择项目名称' },
          ],
          retMoneyAmount: [
            { required: true, trigger: 'blur', message: '请输入质保金金额' },
          ],
          retMoneyProportion: [
            { required: true, trigger: 'blur', message: '请输入质保金比例' },
          ],
          expirationDate: [
            { required: true, trigger: 'blur', message: '请输入质保期限(月)' },
          ],
          alrExpirationMoney: [
            {
              required: true,
              trigger: 'blur',
              message: '请输入已到期质保金金额',
            },
          ],
          WarrantyPeriodOne: [
            {
              required: true,
              type: 'date',
              message: '请至少选择一个开始时间',
              trigger: 'change',
            },
          ],
          WarrantyPeriodTwo: [
            {
              required: true,
              type: 'date',
              message: '请至少选择一个结束时间',
              trigger: 'change',
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
          const Warr = row.warrantyPeriod.split('~')
          const starttime = new Date(Warr[0])
          const endtime = new Date(Warr[1])
          row.WarrantyPeriodOne = starttime
          row.WarrantyPeriodTwo = endtime
          state.title = '编辑'
          row.projectID = parseInt(row.projectID)
          state.form = Object.assign({}, row)
          state.projectCodeOptions = [
            { label: row.projectName, value: row.projectID },
          ]
          console.log(state.projectCodeOptions)
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
      // const fetchData = async () => {
      //   const ptlist = await GetProjectPayment()
      //   state.projectCodeOptions = ptlist.data
      // }
      //远程搜索项目信息
      const fetchData = async (query) => {
        //console.log(query)
        if (query) {
          state.loading = true
          const ptlist = await GetProjectPayment({ name: query })
          setTimeout(() => {
            state.loading = false
            //console.log(ptlist.data)
            state.projectCodeOptions = ptlist.data
          }, 200)
        } else {
          state.projectCodeOptions = []
        }
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          const onetime = new Date(state.form.WarrantyPeriodOne).getTime()
          const warstart = onetime
            .toString()
            .slice(0, onetime.toString().length - 3)
          const twotime = new Date(state.form.WarrantyPeriodTwo).getTime()
          const warend = twotime
            .toString()
            .slice(0, twotime.toString().length - 3)
          state.form.WarrantyPeriodObject = `${warstart}~${warend}`
          if (valid) {
            if (valid) {
              if (state.form.projectID.length != undefined) {
                state.form.projectID =
                  state.form.projectID[state.form.projectID.length - 1]
              }
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
        fetchProCodeData()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchData,
        fetchProCodeData,
      }
    },
  })
</script>
