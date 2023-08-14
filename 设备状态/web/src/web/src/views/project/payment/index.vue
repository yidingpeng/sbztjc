<template>
  <div class="payment-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="市场片区：">
            <el-select
              v-model="queryForm.marketAreaValue"
              clearable
              filterable
              style="width: 100%"
            >
              <el-option
                v-for="item in marketAreaOptions"
                :key="item.id"
                :label="item.areaNameAndPlaceName"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="客户名称：">
            <el-input
              v-model.trim="queryForm.clientName"
              clearable
              placeholder="请输入客户名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称：">
            <el-input
              v-model.trim="queryForm.pt_Name"
              clearable
              placeholder="请输入项目名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目编号：">
            <el-input
              v-model.trim="queryForm.pt_Code"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="营销经理：">
            <el-input
              v-model.trim="queryForm.PersonInChargeName"
              clearable
              placeholder="请输入营销经理"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="回款日期范围：">
            <el-date-picker
              v-model="queryForm.paymentDateRangQ"
              end-placeholder="结束时间"
              range-separator="~"
              start-placeholder="开始时间"
              type="daterange"
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
          v-permissions="{ permission: ['payment:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit($event)"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['payment:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete($event)"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="项目编号"
        prop="pt_Code"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="pt_Name"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="项目未签合同金额(元)"
        prop="pt_SignedCash"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目未签合同金额(元)"
        prop="pt_UnSignedCash"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合同已开票金额(元)"
        prop="ctInvoiceAmount"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合同未开票金额(元)"
        prop="ctUnInvoiceAmount"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="回款金额(元)"
        prop="payment_CashShow"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        :formatter="prece"
        label="回款比"
        prop="payment_Precent"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="回款类型"
        prop="returnTypeText"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="回款方式"
        prop="returnWayText"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="回款日期"
        prop="returnDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="市场片区"
        prop="marketAreaTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="客户名称"
        prop="clientName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="营销经理"
        prop="personInChargeName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['payment:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['payment:delete'] }"
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
  import { defineComponent, onMounted, reactive, toRefs } from 'vue'
  import { getList, doDelete, GetSalseAreaSelect } from '@/api/payment'
  import Edit from './components/PaymentEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Payment',
    components: { Edit },

    setup() {
      const $baseMessage = inject('$baseMessage')
      const $baseConfirm = inject('$baseConfirm')
      const state = reactive({
        paymentDateRangQ: null,
        editRef: null,
        list: [],
        listLoading: true,
        marketAreaOptions: [],
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          deliver_Code: '',
          pt_Name: '',
        },
      })
      //市场片区下拉框
      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }
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
            await doDelete({ ids: row.id })
            $baseMessage(
              '所选数据已被删除',
              'success',
              'vab-hey-message-success'
            )
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              await doDelete({ ids })
              $baseMessage(
                '所选数据已被删除',
                'success',
                'vab-hey-message-success'
              )
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
        if (state.queryForm.paymentDateRangQ != null) {
          state.queryForm.paymentDateRangQ[0] = changeDate(
            state.queryForm.paymentDateRangQ[0]
          )
          state.queryForm.paymentDateRangQ[1] = changeDate(
            state.queryForm.paymentDateRangQ[1]
          )
          state.queryForm.paymentDateRangT =
            state.queryForm.paymentDateRangQ.join('~')
        }
        state.queryForm.marketArea =
          state.queryForm.marketAreaValue === ''
            ? 0
            : state.queryForm.marketAreaValue
        state.queryForm.pageNo = 1
        fetchData()
        state.queryForm.paymentDateRangT = ''
      }
      const changeDate = (val) => {
        if (val == null) {
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
      const fetchData = async (isProDetail, id) => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.payment_CashShow = formatCurrency(item.payment_Cash)
        })
        state.total = total
        state.listLoading = false
        if (isProDetail) {
          const AfterQueryList = state.list.filter((item) => {
            return item.id && item.id === id
          })
          handleEdit(AfterQueryList)
        }
      }
      onMounted((isProDetail, id) => {
        fetchData(isProDetail, id)
        fetchMarketArea()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        fetchMarketArea,
        queryData,
        fetchData,
        formatCurrency,
        Delete,
        Plus,
        Search,
      }
    },
    methods: {
      prece(row, column, cellValue) {
        if (column.property == 'ct_Cash') {
          return `${cellValue}元`
        } else if (column.property == 'non_Payment') {
          return `${cellValue}元`
        } else if (column.property == 'payment_Cash') {
          return `${cellValue}元`
        } else if (column.property == 'payment_Precent') {
          return `${cellValue}%`
        }
      },
    },
  })
</script>
