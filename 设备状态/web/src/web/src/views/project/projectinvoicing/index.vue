<template>
  <div class="project-projectinvoicing-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目编号：">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="合同编号：">
            <el-input
              v-model.trim="queryForm.ct_code"
              clearable
              placeholder="请输入合同编号"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['projectinvoicing:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['projectinvoicing:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="开票编号"
        prop="invoiceNo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合同编号"
        prop="ct_code"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合同金额（元）"
        prop="ct_cashShow"
        show-overflow-tooltip
        width="130px"
      />
      <el-table-column
        align="center"
        label="开票日期"
        prop="invoiceDate"
        show-overflow-tooltip
        width="100px"
      />
      <el-table-column
        align="center"
        label="开票金额(元)"
        prop="invoiceAmountShow"
        show-overflow-tooltip
        width="110px"
      />
      <el-table-column
        align="center"
        label="不含税金额(元)"
        prop="amounExcludingTax"
        show-overflow-tooltip
        width="130px"
      >
        <template #default="scope">
          <span v-if="scope.row.amounExcludingTax == 0"></span>
          <span v-else>{{ scope.row.amounExcludingTax }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="开票税率(%)"
        prop="invoiceTaxRate"
        show-overflow-tooltip
        width="110px"
      >
        <template #default="scope">
          <span v-if="scope.row.invoiceTaxRate == 0"></span>
          <span v-else>{{ scope.row.invoiceTaxRate }}</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="开票申请人"
        prop="applyManTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="开票是否挂账"
        prop="isCredit"
        show-overflow-tooltip
        width="130px"
      >
        <template #default="scope">
          <span v-if="scope.row.isCredit == 1">是</span>
          <span v-if="scope.row.isCredit == 0">否</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="挂账日期"
        prop="creditDate"
        show-overflow-tooltip
        width="100px"
      />
      <!-- <el-table-column
        align="center"
        label="合同未开票金额（元）"
        prop="ct_UninvoicedAmount"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合同应开票总金额（元）"
        prop="ct_InvoicedAmount"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['projectinvoicing:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['projectinvoicing:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      background
      :current-page="queryForm.pageNo"
      :layout="layout"
      :page-size="queryForm.pageSize"
      :total="total"
      @current-change="handleCurrentChange"
      @size-change="handleSizeChange"
    />
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getList, doDelete } from '@/api/projectProjectinvoicing'
  import Edit from './components/ProjectProjectinvoicingEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'ProjectProjectinvoicing',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
      })

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        fetchData()
      }
      const changeDate = (val) => {
        if (val == '0001-01-01 00:00:00' || val == null) {
          return null
        }
        const newDate = new Date(val)
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return `${y}-${m}-${d}`
      }
      //金额转换为千分位
      const formatCurrency = (value, str) => {
        if (!str) str = ''
        // str 规定货币类型
        if (value == '0') return '0.00'
        if (value == '.') return ''
        if (!value) return ''
        let val = Number(value).toFixed(2) // 提前保留两位小数
        const intPart = parseInt(val) // 获取整数部分
        const intPartFormat = intPart
          .toString()
          .replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断 ？？？
        let floatPart = '.00' // 预定义小数部分
        val = val.toString() // 将number类型转为字符串类型，方便操作
        const value2Array = val.split('.')
        if (value2Array.length === 2) {
          // =2表示数据有小数位
          floatPart = value2Array[1].toString() // 拿到小数部分
          if (floatPart.length === 1) {
            // 补0,实际上用不着
            return `${str + intPartFormat}.${floatPart}0`
          } else {
            return `${str + intPartFormat}.${floatPart}`
          }
        } else {
          return str + intPartFormat + floatPart
        }
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.invoiceDate = changeDate(item.invoiceDate)
          item.creditDate = changeDate(item.creditDate)
          item.ct_InvoicedAmount = parseFloat(item.ct_InvoicedAmount).toFixed(2)
          item.invoiceAmountShow = formatCurrency(item.invoiceAmount)
          item.ct_cashShow = formatCurrency(item.ct_cash)
        })
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        changeDate,
        formatCurrency,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
