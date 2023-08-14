<!-- 采购申请单 -->
<template>
  <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
    <el-row>
      <el-col :span="24">
        <h2 style="padding: 10px; margin: 0 auto; text-align: center">
          申请信息
        </h2>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="申请主题" prop="title">
          <el-input v-model="form.title" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="申请单号" prop="applicationNo">
          <el-input v-model="form.applicationNo" />
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="理由/用途" prop="reason">
          <el-input v-model="form.reason" type="textarea" />
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="备注" prop="remark">
          <el-input v-model="form.remark" type="textarea" />
        </el-form-item>
      </el-col>
    </el-row>
    <h2 style="margin: 0 auto; text-align: center">采购明细</h2>
    <el-table
      v-loading="listLoading"
      :data="tableData"
      style="width: 100%; max-height: 500px; padding-top: 10px; overflow: auto"
    >
      <el-table-column align="center" label="序号" type="index" width="80px" />
      <el-table-column
        align="center"
        label="物料名称"
        prop="name"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料编码"
        prop="code"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="规格型号"
        prop="model"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="品牌"
        prop="brand"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-input v-model="row.brand" />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="数量"
        prop="count"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-input-number
            v-model="row.count"
            :max="999999999"
            :min="1"
            :precision="0"
            :step="1"
          />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="单位"
        prop="basicUnit"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作">
        <template #default="{ row }">
          <el-button type="danger" @click.prevent="deleteItem(row)">
            删
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-button class="mt-4" style="width: 100%" @click="addItem">
      新增明细行
    </el-button>
  </el-form>
  <div class="pay-button-group">
    <el-button style="margin-bottom: 8px" type="primary" @click="save">
      提交
    </el-button>
    <el-button style="margin-bottom: 8px" type="primary" @click="tongguo">
      通过
    </el-button>
    <el-button style="margin-bottom: 8px" type="primary" @click="bohui">
      驳回
    </el-button>
  </div>
  <chooseDig ref="chooseRef" @get-purdetail="getPurDetail" />
</template>
<script>
  import { getDataByCid, doPurOrderAdd, OrderSH } from '@/api/purchase/purchase'
  import chooseDig from './components/ChoosePurDetail'

  export default defineComponent({
    name: 'PurchaseQuisition',
    components: { chooseDig },
    setup() {
      //const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const route = useRoute()
      const state = reactive({
        listLoading: false,
        formRef: null,
        chooseRef: null,
        tableData: [],
        form: {
          applicationNo: '',
          reason: '',
          purchaseDetail: [],
        },
        chooseData: [],
        rules: {
          reason: [
            { required: true, trigger: 'blur', message: '请输入理由/用途' },
          ],
          title: [
            { required: true, trigger: 'blur', message: '请输入申请主题' },
          ],
          applicationNo: [
            { required: true, trigger: 'blur', message: '请输入申请单号' },
          ],
        },
      })
      const addItem = () => {
        state.chooseRef.showEdit(state.chooseData)
      }

      //删除采购明细某条数据
      const deleteItem = (row) => {
        state.chooseData.splice(
          state.chooseData.findIndex((item) => item.cid === row.cid),
          1
        )
        fetchDetail()
      }
      //const tableData = ref([])
      const save = () => {
        state.formRef.validate(async (valid) => {
          if (valid) {
            for (let i = 0; i < state.tableData.length; i++) {
              if (
                state.tableData[i].brand == '' ||
                state.tableData[i].count < 1
              ) {
                $baseMessage('采购明细表格“品牌”或“数量”字段未填写', 'error')
                return
              }
            }
            //将所选采购明细信息添加到form对象中
            state.form.purDetaileData = state.tableData
            const { msg } = await doPurOrderAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            state['formRef'].resetFields()
            state.tableData = []
            state.form.applicationNo = generateNo()
          } else {
            return false
          }
        })
      }

      const tongguo = async () => {
        if (state.form.status == 0) {
          //IMdoAdd
          const data = {
            BomCode: state.form.applicationNo,
            ProjectCode: '',
            ProjectName: '',
            CurrentVersion: 'VA.0',
            Applicant: state.form.createdBy,
            ApplicationDate: state.form.createdAt,
            CompleteSetRate: '',
            DrawingCode: '',
            BomId: '',
            Status: 'Completed',
            remark: '',
          }
          const { msg } = await OrderSH(data)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        } else {
          $baseMessage('当前状态不支持', 'error', 'vab-hey-message-error')
        }
      }
      const bohui = async () => {
        if (state.form.status == 0) {
          $baseMessage('当前状态不支持', 'error', 'vab-hey-message-error')
        } else {
          $baseMessage('当前状态不支持', 'error', 'vab-hey-message-error')
        }
      }

      //获取采购明细
      const fetchDetail = async () => {
        state.listLoading = true
        if (state.chooseData.length > 0) {
          const result = await getDataByCid(state.chooseData)
          state.tableData = result.data
          state.tableData = state.tableData.map((v) => {
            v.brand = ''
            v.count = 0
            return v
          })
        } else {
          state.tableData = []
        }
        state.listLoading = false
      }

      const getPurDetail = (val) => {
        state.chooseData = val
        fetchDetail()
      }

      const GetNowDate = () => {
        const newDate = new Date()
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return y + m + d
      }

      const generateNo = () => {
        return `ON${GetNowDate()}${Math.floor(Math.random() * 10000)}`
      }

      onMounted(() => {
        console.log(route.query)
        state.form.applicationNo = generateNo()
      })

      return {
        ...toRefs(state),
        addItem,
        deleteItem,
        save,
        GetNowDate,
        getPurDetail,
        generateNo,
        tongguo,
        bohui,
      }
    },
  })
</script>
<style lang="scss" scoped>
  .pay-button-group {
    display: block;
    margin: 20px auto;
    text-align: center;
  }
</style>
