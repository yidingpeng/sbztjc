<template>
  <div class="contract-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <!-- <el-form-item label="市场片区：">
            <el-select
              v-model="queryForm.marketAreaValue"
              clearable
              filterable
              style="width: 195px"
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
              style="width: 195px"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称：">
            <el-input
              v-model.trim="queryForm.pt_Name"
              clearable
              placeholder="请输入项目名称"
              style="width: 195px"
            />
          </el-form-item>
          &nbsp; -->
          <el-form-item label="内部合同编号：">
            <el-input
              v-model.trim="queryForm.ct_code"
              clearable
              placeholder="请输入内部合同编号"
              style="width: 195px"
            />
          </el-form-item>
          <!-- &nbsp;
          <el-form-item label="营销经理：">
            <el-input
              v-model.trim="queryForm.PersonInChargeName"
              clearable
              placeholder="请输入营销经理"
              style="width: 195px"
            />
          </el-form-item> -->
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['contract:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <!-- <el-button
          v-permissions="{ permission: ['contract:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button> -->
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column type="expand">
        <template #default="props">
          <el-table border :data="props.row.contractDetailList">
            <el-table-column align="center" label="项目编号" prop="projectId" />
            <el-table-column
              align="center"
              label="项目名称"
              prop="projectName"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="设备数量"
              prop="equipmentCount"
            />
            <el-table-column
              align="center"
              label="设备合同单价"
              prop="equipmentUnitPrice"
            />
            <el-table-column
              align="center"
              label="设备合同总价"
              prop="equipmentTotalPrice"
            />
            <el-table-column
              align="center"
              label="要求交付日期"
              prop="deliverDate"
            />
            <!-- <el-table-column
              align="center"
              label="预付款"
              prop="advanceCharge"
            />
            <el-table-column
              align="center"
              label="进度款"
              prop="progressPayment"
            />
            <el-table-column
              align="center"
              label="验收款"
              prop="acceptancePayment"
            />
            <el-table-column
              align="center"
              label="质保金"
              prop="retentionMoney"
            />
            <el-table-column
              align="center"
              label="结算款"
              prop="settlementFunds"
            />
            <el-table-column
              align="center"
              label="尾款"
              prop="balancePayment"
            /> -->
            <el-table-column align="center" label="操作">
              <template #default="{ row }">
                <!-- v-permissions="{ permission: ['contract:Sedit'] }" -->
                <el-link
                  title="编辑"
                  :underline="false"
                  @click="handleCDAdd(row, 'edit')"
                >
                  <vab-icon icon="edit-2-line" />
                </el-link>
                <!-- v-permissions="{ permission: ['contract:Sdelete'] }" -->
                <el-link
                  title="删除"
                  :underline="false"
                  @click="handleDeleteS(row)"
                >
                  <vab-icon icon="delete-bin-5-line" />
                </el-link>
              </template>
            </el-table-column>
          </el-table>
          <!-- <el-descriptions border :column="4">
            <el-descriptions-item label="项目编号">
              {{ props.row.pt_codesTxt }}
            </el-descriptions-item>
            <el-descriptions-item label="项目名称">
              {{ props.row.pt_NamesTxt }}
            </el-descriptions-item>
            <el-descriptions-item label="市场片区">
              {{ props.row.marketAreaTxt }}
            </el-descriptions-item>
            <el-descriptions-item label="客户名称">
              {{ props.row.clientName }}
            </el-descriptions-item>
            <el-descriptions-item label="营销经理">
              {{ props.row.personInChargeName }}
            </el-descriptions-item>
            <el-descriptions-item label="要求交付日期">
              {{ props.row.ct_deliveryDate }}
            </el-descriptions-item>
            <el-descriptions-item label="项目数量">
              {{ props.row.proCount }}
            </el-descriptions-item>
            <el-descriptions-item label="项目合同单价">
              {{ props.row.proUnitPrice }}
            </el-descriptions-item>
          </el-descriptions> -->
        </template>
      </el-table-column>
      <!-- <el-table-column align="center" show-overflow-tooltip type="selection" /> -->
      <el-table-column
        align="center"
        label="内部合同编号"
        prop="ct_code"
        show-overflow-tooltip
        width="150"
      />
      <el-table-column
        align="center"
        label="合同名称"
        prop="contractName"
        show-overflow-tooltip
        width="170"
      />
      <el-table-column
        align="center"
        label="外部合同编号"
        prop="externalCt_code"
        show-overflow-tooltip
        width="150"
      />
      <!-- <el-table-column
        align="center"
        label="项目编号"
        prop="pt_codesTxt"
        show-overflow-tooltip
        width="135"
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="pt_NamesTxt"
        show-overflow-tooltip
        width="135"
      /> -->
      <el-table-column
        align="center"
        label="合同总金额(元)"
        prop="ct_cashShow"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="合同税率(%)"
        prop="ctTaxRate"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="合同签订日期"
        prop="ct_signingDate"
        show-overflow-tooltip
        width="120"
      />
      <!-- <el-table-column
        align="center"
        label="要求交付日期"
        prop="ct_deliveryDate"
        show-overflow-tooltip
        width="115"
      /> -->
      <el-table-column
        align="center"
        label="回款条款"
        prop="payment_collection"
        show-overflow-tooltip
        width="150"
      />
      <!-- <el-table-column
        align="center"
        label="市场片区"
        prop="marketAreaTxt"
        show-overflow-tooltip
        width="110"
      />
      <el-table-column
        align="center"
        label="客户名称"
        prop="clientName"
        show-overflow-tooltip
        width="150"
      />
      <el-table-column
        align="center"
        label="营销经理"
        prop="personInChargeName"
        show-overflow-tooltip
        width="150"
      /> -->
      <el-table-column
        v-permissions="{ permission: ['contract:annex'] }"
        align="center"
        label="合同扫描件"
        prop="ct_attachmentPdfUrl"
        show-overflow-tooltip
        width="110"
      >
        <template #default="scope">
          <el-link
            v-if="scope.row.ct_attachmentPdfUrl.length > 0"
            target="_blank"
            @click="
              downloadFile(
                scope.row.ct_attachmentPdfUrl,
                scope.row.filePdfName,
                scope.row.filePdfIds
              )
            "
          >
            查看
          </el-link>
        </template>
      </el-table-column>
      <el-table-column
        v-permissions="{ permission: ['contract:annex'] }"
        align="center"
        label="合同可编辑件"
        prop="ct_attachmentWordUrl"
        show-overflow-tooltip
        width="110"
      >
        <template #default="scope">
          <el-link
            v-if="scope.row.ct_attachmentWordUrl.length > 0"
            target="_blank"
            @click="
              downloadFile2(
                scope.row.ct_attachmentWordUrl,
                scope.row.fileWordName,
                scope.row.fileWordIds
              )
            "
          >
            下载
          </el-link>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
        width="150"
      />
      <el-table-column align="center" label="操作">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['contract:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['contract:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
          <!-- <el-link
            title="回款信息"
            :underline="false"
            @click="handleHKInfo(row.pt_idsTxt)"
          >
            <vab-icon icon="money-cny-circle-line" />
          </el-link>
          <el-link
            title="新增合同项目信息"
            :underline="false"
            @click="handleCDAdd(row.id, 'add')"
          >
            新增
          </el-link> -->
          <el-link
            title="回款比例"
            :underline="false"
            @click="handleHKAdd(row.id, row.ct_cash)"
          >
            回款比例
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
    <DetailEdit ref="detaileditRef" @fetch-data="fetchData" />
    <ProportionEdit ref="proportionRef" @fetch-data="fetchData" />
  </div>

  <el-dialog
    v-model="DialogTableVisible"
    :close-on-click-modal="false"
    title="回款信息"
  >
    <el-table :data="gridData">
      <el-table-column label="项目编号" property="pt_Code" width="150" />
      <el-table-column label="项目名称" property="pt_Name" width="200" />
      <el-table-column label="回款类型" property="returnTypeText" />
      <el-table-column label="回款金额" property="payment_Cash" />
      <el-table-column label="回款方式" property="returnWayText" />
    </el-table>
  </el-dialog>
</template>

<script>
  import {
    getList,
    doDelete,
    GetDownloadFilePath,
    GetSalseAreaSelect,
    doDownload,
    doDownload2,
    GetIdsList,
    ContractDetailDelete,
  } from '@/api/contract'
  import Edit from './components/ContractEdit'
  import DetailEdit from './components/ContractDetailEdit'
  import ProportionEdit from './components/ContractProportionEdit.vue'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Contract',
    components: { Edit, DetailEdit, ProportionEdit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        url: '',
        editRef: null,
        detaileditRef: null,
        proportionRef: null,
        list: [],
        listLoading: true,
        marketAreaOptions: [],
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          pt_Name: '',
          ct_code: '',
        },
        DialogTableVisible: false,
        gridData: [],
      })
      //市场片区下拉框
      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }

      const changeDate = (val) => {
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

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
        fetchData()
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
        state.queryForm.marketArea =
          state.queryForm.marketAreaValue === ''
            ? 0
            : state.queryForm.marketAreaValue
        state.queryForm.pageNo = 1
        fetchData()
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
        list.forEach((item) => {
          item.ct_signingDate = changeDate(item.ct_signingDate)
          // item.ct_deliveryDate = changeDate(item.ct_deliveryDate)
          item.ct_cashShow = formatCurrency(item.ct_cash)
        })
        //console.log(list)
        state.list = list
        state.total = total
        state.listLoading = false
      }

      const fetchInsertFileData = async () => {
        state.url = await GetDownloadFilePath()
      }
      const downloadFile = async (FileUrl, FileName, filePdfIds) => {
        fetchInsertFileData()
        for (let i = 0; i < FileUrl.length; i++) {
          // window.open(
          //   state.url + '?fileUrl=' + FileUrl[i] + '&fileName=' + FileName[i]
          // )
          await doDownload(state.url, FileUrl[i], filePdfIds[i], FileName[i])
        }
      }

      const downloadFile2 = async (FileUrl, FileName, fileWordIds) => {
        fetchInsertFileData()
        for (let i = 0; i < FileUrl.length; i++) {
          // window.open(
          //   state.url + '?fileUrl=' + FileUrl[i] + '&fileName=' + FileName[i]
          // )
          await doDownload2(state.url, FileUrl[i], fileWordIds[i], FileName[i])
        }
      }
      const handleHKInfo = async (ids) => {
        state.DialogTableVisible = true
        const data = await GetIdsList({ Ids: ids })
        state.gridData = data.data
      }

      const handleCDAdd = (id, type) => {
        state.detaileditRef.showEdit(id, type)
        //fetchData()
      }

      const handleHKAdd = (id, totalPrice) => {
        state.proportionRef.showEdit(id, totalPrice)
        fetchData()
      }
      //handleDeleteS
      const handleDeleteS = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await ContractDetailDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      onMounted(() => {
        fetchData()
        fetchInsertFileData()
        fetchMarketArea()
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
        downloadFile,
        downloadFile2,
        fetchInsertFileData,
        fetchMarketArea,
        changeDate,
        formatCurrency,
        handleHKInfo,
        handleCDAdd,
        handleDeleteS,
        handleHKAdd,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
<style lang="scss">
  html body .el-scrollbar__thumb:hover,
  html body[class*='vab-theme-'] .el-scrollbar__thumb:hover {
    background-color: #252525;
  }
  html body .el-scrollbar__thumb,
  html body[class*='vab-theme-'] .el-scrollbar__thumb {
    background-color: #424242;
  }
</style>
