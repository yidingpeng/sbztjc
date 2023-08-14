<template>
  <div class="detail-container">
    <el-page-header
      :content="'【' + ClientBaseData.clientName + '】详情页面'"
      @back="goBack"
    />
    <el-collapse v-model="CollactiveNames" @change="handleChange">
      <el-collapse-item name="1" title="【基础信息】">
        <div>
          <el-descriptions border :column="3">
            <el-descriptions-item width="200px">
              <template #label>单位编号</template>
              {{ ClientBaseData.companyCode }}
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>客户简称</template>
              {{ ClientBaseData.clientName }}
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>客户全称</template>
              {{ ClientBaseData.clientFullName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>市场片区</template>
              {{ ClientBaseData.marketAreaTxt }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>地址</template>
              {{ ClientBaseData.address }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>客户级别</template>
              {{ ClientBaseData.clientRankText }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>合作业务</template>
              {{ ClientBaseData.cooperativeBusinessText }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司董事长</template>
              {{ ClientBaseData.ceoName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司总经理</template>
              {{ ClientBaseData.gmName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司副总经理</template>
              {{ ClientBaseData.deputyGMName }}
            </el-descriptions-item>
            <el-descriptions-item span="2">
              <template #label>营销负责人</template>
              {{ ClientBaseData.personInChargeName }}
            </el-descriptions-item>
            <el-descriptions-item span="3">
              <template #label>备注</template>
              {{ ClientBaseData.remark }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="2" title="【项目信息】">
        <div>
          <el-descriptions :column="3" :size="size" title="">
            <el-descriptions-item>
              <el-tabs v-model="ProActiveName" class="demo-tabs" type="card">
                <el-tab-pane
                  v-for="item in proClassesList"
                  :key="item.key"
                  :label="item.label"
                  :name="item.sort"
                  @row-dblclick="ProjectDoubleClick"
                >
                  <el-table v-loading="listLoading" :data="item.conList">
                    <el-table-column
                      align="center"
                      label="项目名称"
                      prop="projectName"
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
                      label="项目接收日"
                      prop="projectReceiveDates"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="状态"
                      prop="proStateName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="业务类型"
                      prop="businessTypeName"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
              </el-tabs>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="3" title="【三大指标信息】">
        <div>
          <el-descriptions :column="3" :size="size" title="">
            <el-descriptions-item>
              <el-tabs
                v-model="IndicatorsActiveName"
                class="demo-tabs"
                type="card"
              >
                <el-tab-pane label="合同信息" name="one">
                  <el-table
                    v-loading="listLoading"
                    border
                    :data="ContractList"
                    @row-dblclick="ProContractDoubleClick"
                  >
                    <el-table-column
                      align="center"
                      label="合同编号"
                      prop="ct_code"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合同名称"
                      prop="contractName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="项目编号"
                      prop="pt_codesTxt"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="项目名称"
                      prop="pt_NamesTxt"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合同金额(元)"
                      prop="ct_cash"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合同税率(%)"
                      prop="ctTaxRate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合约签约日期"
                      prop="ct_signingDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合约交货日期"
                      prop="ct_deliveryDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="回款条款"
                      prop="payment_collection"
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
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="回款信息" name="two">
                  <el-table
                    v-loading="listLoading"
                    border
                    :data="PaymentList"
                    @row-dblclick="ProPaymentDoubleClick"
                  >
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
                    <el-table-column
                      align="center"
                      label="回款金额(元)"
                      prop="payment_Cash"
                      show-overflow-tooltip
                    />
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
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="开票信息" name="three">
                  <el-table
                    v-loading="listLoading"
                    border
                    :data="InvoicingList"
                    @row-dblclick="ProInvoicingDoubleClick"
                  >
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
                      label="开票日期"
                      prop="invoiceDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="开票金额（元）"
                      prop="invoiceAmount"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="开票税率(%)"
                      prop="invoiceTaxRate"
                      show-overflow-tooltip
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
                    />
                  </el-table>
                </el-tab-pane>
              </el-tabs>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="4" title="【客户干系人信息】">
        <div>
          <el-descriptions :column="3" :size="size" title="">
            <el-descriptions-item>
              <el-tabs
                v-model="ContactsActiveName"
                class="demo-tabs"
                type="card"
              >
                <el-tab-pane label="干系人" name="one">
                  <el-table
                    v-loading="listLoading"
                    border
                    :data="ContactsList"
                    @row-dblclick="ProPaymentDoubleClic"
                  >
                    <el-table-column
                      align="center"
                      label="联系人姓名"
                      prop="contactsName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="现任职公司"
                      prop="curCompany"
                      show-overflow-tooltip
                      width="200px"
                    />
                    <el-table-column
                      align="center"
                      label="单位编号"
                      prop="comCode"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="部门"
                      prop="departmenttext"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="客户级别"
                      prop="clientRankTxt"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="性别"
                      prop="sex"
                      show-overflow-tooltip
                    >
                      <template #default="scope">
                        <span v-if="scope.row.sex == 1">男</span>
                        <span v-if="scope.row.sex == 2">女</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      align="center"
                      label="年龄"
                      prop="age"
                      show-overflow-tooltip
                    >
                      <template #default="scope">
                        {{
                          scope.row.birthday != '0001-01-01 00:00:00'
                            ? scope.row.age
                            : '暂未录入'
                        }}
                      </template>
                    </el-table-column>
                    <el-table-column
                      align="center"
                      label="电话1"
                      prop="telephone1"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="联系人类别"
                      prop="contactsCategorytext"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="负责业务"
                      prop="responsibleBusiness"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
              </el-tabs>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
    </el-collapse>
  </div>
</template>

<script>
  import { useTabsStore } from '@/store/modules/tabs'
  import { handleActivePath } from '@/utils/routes'
  import { Refresh } from '@element-plus/icons-vue'
  import {
    GetListByClientIdAndClass,
    GetInvoicingListByClientId,
    GetContractsListByClientId,
    GetPaymentListByClientId,
    GetListByClientId,
    GetByIdList,
    GetProjectClass,
  } from '@/api/client'
  import { ref } from 'vue'

  export default defineComponent({
    name: 'ClientDetail',
    setup() {
      const CollactiveNames = ref(['1', '2', '3', '4'])
      const ProActiveName = ref(1)
      const IndicatorsActiveName = ref('one')
      const ContactsActiveName = ref('one')
      const route = useRoute()
      const router = useRouter()

      const $pub = inject('$pub')

      const tabsStore = useTabsStore()
      const { changeTabsMeta, delVisitedRoute } = tabsStore

      const state = reactive({
        route: { query: { title: '加载中' } },
        rate: 0,
        form: {
          text: '',
        },
        url: '',
        title: '加载中',
        ContractList: [],
        PaymentList: [],
        InvoicingList: [],
        ContactsList: [],
        ClientBaseData: [],
        proClassesList: [],
      })
      const goBack = async () => {
        const detailPath = await handleActivePath(route, true)
        await router.push('/project/client')
        await delVisitedRoute(detailPath)
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
      //获取客户公司基础数据
      const GetClientDataById = async () => {
        const listData = await GetByIdList({ Id: route.query.id })
        state.ClientBaseData = listData.data[0]
      }

      //项目合同数据
      const ProContractDetail = async () => {
        const List = await GetContractsListByClientId({
          clietId: route.query.id,
        })
        state.ContractList = List.data
        state.ContractList.forEach((item) => {
          item.ct_signingDate = changeDate(item.ct_signingDate)
          item.ct_deliveryDate = changeDate(item.ct_deliveryDate)
          item.ct_cash = formatCurrency(item.ct_cash)
          item.ctTaxRate = parseFloat(item.ctTaxRate).toFixed(2)
        })
      }

      //项目回款数据
      const ProPaymentDetail = async () => {
        const List = await GetPaymentListByClientId({
          clietId: route.query.id,
        })
        state.PaymentList = List.data
        state.PaymentList.forEach((item) => {
          item.ctUnInvoiceAmount = parseFloat(item.ctUnInvoiceAmount).toFixed(2)
          item.payment_Precent = parseFloat(item.payment_Precent).toFixed(2)
          item.payment_Cash = formatCurrency(item.payment_Cash)
        })
      }

      //项目开票数据
      const ProInvoicingDetail = async () => {
        const List = await GetInvoicingListByClientId({
          clietId: route.query.id,
        })
        state.InvoicingList = List.data
        state.InvoicingList.forEach((item) => {
          item.invoiceDate = changeDate(item.invoiceDate)
          item.creditDate = changeDate(item.creditDate)
          item.invoiceTaxRate = parseFloat(item.invoiceTaxRate).toFixed(2)
          item.invoiceAmount = formatCurrency(item.invoiceAmount)
        })
      }
      //联系人数据
      const ContactsDetail = async () => {
        const List = await GetListByClientId({
          ClientId: route.query.id,
        })
        state.ContactsList = List.data
      }
      //项目信息双击事件
      const ProjectDoubleClick = (row) => {
        console.log(row)
        router.push({
          path: '/project/projectbasics/detail',
          query: { id: row.id, clientCompany: route.query.id },
        })
      }
      //合同信息表格双击事件
      const ProContractDoubleClick = () => {
        router.push({
          path: '/project/contract',
        })
      }
      //项目回款表格双击事件
      const ProPaymentDoubleClick = () => {
        router.push({
          path: '/project/payment',
        })
      }
      //项目开票明细表格双击事件
      const ProInvoicingDoubleClick = () => {
        router.push({
          path: '/project/projectinvoicing',
        })
      }

      const handleRefreshMainPage = () => {
        $pub('reload-router-view', 'client')
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

      //获取项目类型
      const getProInfo = async () => {
        const proClassData = await GetProjectClass()
        state.proClassesList = proClassData.data
        let i = 1
        state.proClassesList.forEach((item) => {
          getProClassConList(item)
          item.sort = i++
        })
      }

      //根据项目类型获取数据
      const getProClassConList = async (data) => {
        const List = await GetListByClientIdAndClass({
          clientId: route.query.id,
          proClassTxt: data.label,
        })
        data.conList = List.data
      }

      onMounted(() => {
        changeTabsMeta({
          title: '详情页',
          meta: {
            title: `${route.query.title} 详情页`,
          },
        })
        state.title = route.query.title
        state.route = {
          path: route.path,
          params: route.params,
          query: { ...route.query, ...{ rate: parseInt(route.query.rate) } },
          name: route.name,
          meta: route.meta,
        }
      })
      onMounted(() => {
        ProInvoicingDetail()
        ProPaymentDetail()
        ProContractDetail()
        ContactsDetail()
        GetClientDataById()
        getProInfo()
      })
      return {
        ...toRefs(state),
        goBack,
        handleRefreshMainPage,
        Refresh,
        changeDate,
        CollactiveNames,
        ProActiveName,
        ContactsActiveName,
        GetClientDataById,
        IndicatorsActiveName,
        ProContractDetail,
        ProPaymentDetail,
        ProInvoicingDetail,
        ContactsDetail,
        ProjectDoubleClick,
        ProContractDoubleClick,
        ProPaymentDoubleClick,
        ProInvoicingDoubleClick,
        formatCurrency,
        getProInfo,
        getProClassConList,
      }
    },
  })
</script>
