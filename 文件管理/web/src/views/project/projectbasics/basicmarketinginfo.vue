<template>
  <div ref="containerRef" class="custom-table-container">
    <vab-query-form>
      <vab-query-form-left-panel>
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目编码">
            <el-input
              v-model="queryForm.projectCode"
              clearable
              placeholder="项目编码"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称">
            <el-input
              v-model="queryForm.projectName"
              clearable
              placeholder="项目名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="到期质保金">
            <el-select
              v-model="queryForm.isPeriodMoneyZero"
              clearable
              placeholder="已到期质保金大于0"
            >
              <el-option
                v-for="item in IsPerOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="已回款比">
            <el-select
              v-model="queryForm.proReturnedScale"
              clearable
              placeholder="项目已回款比"
            >
              <el-option
                v-for="item in ProReturnedScaleOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="客户公司">
            <el-select
              v-model="queryForm.clientCompany"
              clearable
              filterable
              placeholder="客户公司"
            >
              <el-option
                v-for="(item, index) in ClientCompanyOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="项目分类">
            <el-select
              v-model="queryForm.projectClass"
              clearable
              filterable
              placeholder="项目分类"
            >
              <el-option
                v-for="(item, index) in ProjectClassOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="业务类型">
            <el-select
              v-model="queryForm.businessType"
              clearable
              filterable
              placeholder="业务类型"
            >
              <el-option
                v-for="(item, index) in BusinesstypeOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="紧急程度">
            <el-select
              v-model="queryForm.urgentGrade"
              clearable
              filterable
              placeholder="紧急程度"
            >
              <el-option
                v-for="(item, index) in UrgentGradeOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="管理方式">
            <el-select
              v-model="queryForm.proManagementStyle"
              clearable
              filterable
              placeholder="管理方式"
            >
              <el-option
                v-for="(item, index) in ProManagementStyleOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item
            v-show="!fold"
            label="阶&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;段"
          >
            <el-select
              v-model="queryForm.proState"
              clearable
              filterable
              placeholder="状态"
            >
              <el-option
                v-for="(item, index) in ProStateOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="市场片区">
            <el-select
              v-model="queryForm.marketArea"
              clearable
              filterable
              placeholder="市场片区"
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
          <el-form-item v-show="!fold" label="营销经理">
            <el-input
              v-model.trim="queryForm.personInChargeName"
              clearable
              placeholder="营销经理"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="项目经理">
            <el-input
              v-model.trim="queryForm.projectManager"
              clearable
              placeholder="项目经理"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="产品经理">
            <el-input
              v-model.trim="queryForm.productManager"
              clearable
              placeholder="产品经理"
            />
          </el-form-item>
          <el-form-item>
            <el-button
              :icon="Search"
              native-type="submit"
              type="primary"
              @click="queryData"
            >
              查询
            </el-button>
            <el-button link type="primary" @click="handleFold">
              <span v-if="fold">展开</span>
              <span v-else>合并</span>
              <vab-icon
                class="vab-dropdown"
                :class="{ 'vab-dropdown-active': fold }"
                icon="arrow-up-s-line"
              />
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel>
        <div class="stripe-panel">
          <el-checkbox v-model="stripe">斑马纹</el-checkbox>
        </div>
        <div class="border-panel">
          <el-checkbox v-model="border">边框（可拉伸列）</el-checkbox>
        </div>
        <el-button
          link
          style="margin: 0 10px 10px 0 !important"
          type="primary"
          @click="clickFullScreen"
        >
          <vab-icon
            :icon="isFullscreen ? 'fullscreen-exit-fill' : 'fullscreen-fill'"
          />
        </el-button>
        <el-popover popper-class="custom-table-radio" trigger="hover">
          <el-radio-group v-model="lineHeight">
            <el-radio label="large">大</el-radio>
            <el-radio label="default">中</el-radio>
            <el-radio label="small">小</el-radio>
          </el-radio-group>
          <template #reference>
            <el-button
              link
              style="margin: 0 10px 10px 0 !important"
              type="primary"
            >
              <vab-icon icon="line-height" />
            </el-button>
          </template>
        </el-popover>
        <el-popover
          placement="left-start"
          popper-class="custom-table-checkbox"
          trigger="hover"
          :width="200"
        >
          <template #reference>
            <el-button
              link
              style="margin: 0 0 10px 0 !important"
              type="primary"
            >
              <vab-icon icon="settings-line" />
            </el-button>
          </template>
          <el-checkbox-group v-model="checkList" :width="160">
            <vab-draggable
              v-bind="dragOptions"
              item-key="{ element }"
              :list="columns"
            >
              <template #item="{ element }">
                <div>
                  <vab-icon icon="drag-drop-line" />
                  <el-checkbox
                    :disabled="element.disableCheck === true"
                    :label="element.label"
                  >
                    {{ element.label }}
                  </el-checkbox>
                </div>
              </template>
            </vab-draggable>
          </el-checkbox-group>
        </el-popover>
      </vab-query-form-right-panel>
    </vab-query-form>
    <el-table
      ref="tableSortRef"
      v-loading="listLoading"
      :border="border"
      :data="list"
      :height="height"
      lazy
      :load="loadSearch"
      row-key="id"
      :size="lineHeight"
      :stripe="stripe"
      style="width: 100%"
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column
        v-for="(item, index) in finallyColumns"
        :key="index"
        align="center"
        :label="item.label"
        :prop="item.prop"
        :sortable="item.sortable"
        width="auto"
      >
        <template #default="{ row }">
          <span v-if="item.label === '紧急程度'">
            <el-rate v-model="row.urgentGrade" disabled />
          </span>
          <span v-else>{{ row[item.prop] }}</span>
        </template>
      </el-table-column>
      <template #empty>
        <el-empty class="vab-data-empty" description="暂无数据" />
      </template>
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
  </div>
</template>

<script>
  import {
    getByParentIdList,
    GetSellBasicsList,
    GetBusinesstype,
    GetUrgentGrade,
    GetProManagementStyle,
    GetClientCompany,
    GetProState,
    GetSalseAreaSelect,
    GetParentSellBasicsList,
    GetProjectClass,
  } from '@/api/project'
  import VabDraggable from 'vuedraggable'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Basicmarketinginfo',
    components: {
      VabDraggable,
    },
    setup() {
      const $baseTableHeight = inject('$baseTableHeight')
      const state = reactive({
        BusinesstypeOption: [],
        UrgentGradeOption: [],
        ClientCompanyOption: [],
        ProManagementStyleOption: [],
        ProStateOption: [],
        marketAreaOptions: [],
        ProjectClassOption: [],
        fold: true,
        lineHeight: 'small',
        height: $baseTableHeight(1),
        tableSortRef: null,
        stripe: false,
        border: true,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          projectClass: 30, //在产项目
        },
        IsPerOptions: [
          {
            value: '1',
            label: '是',
          },
          {
            value: '2',
            label: '否',
          },
        ],
        ProReturnedScaleOptions: [
          {
            value: '1',
            label: '0',
          },
          {
            value: '2',
            label: '0-30',
          },
          {
            value: '3',
            label: '30-90',
          },
          {
            value: '4',
            label: '90-99.99',
          },
          {
            value: '5',
            label: '100',
          },
        ],
        checkList: [
          '项目名称',
          '项目编码',
          '营销经理',
          '项目经理',
          '产品经理',
          '项目分类',
          '业务类型',
          '紧急程度',
          '客户公司',
          '合同',
          '技术协议',
          '管理方式',
          '招标编号',
          '图纸代号',
          '阶段',
          '项目总金额',
          '项目未签合同金额',
          '合同应开票总金额',
          '合同已开票总金额',
          '合同未开票总金额',
          '已回款总金额',
          '项目已回款比',
          '质保金比例',
          '质保金金额',
          '已到期质保金金额',
          '项目接收日期',
          '合同签订日期',
          '近期开票日期',
          '近期回款日期',
        ],
        columns: [
          {
            label: '项目名称',
            prop: 'projectName',
            disableCheck: true,
          },
          {
            label: '项目编码',
            prop: 'projectCode',
          },
          {
            label: '营销经理',
            prop: 'personInChargeName',
          },
          {
            label: '项目经理',
            prop: 'projectManager',
          },
          {
            label: '产品经理',
            prop: 'productManager',
          },
          {
            label: '项目分类',
            prop: 'projectClassName',
          },
          {
            label: '业务类型',
            prop: 'businessTypeName',
          },
          {
            label: '紧急程度',
            prop: 'urgentGrade',
          },
          {
            label: '客户公司',
            prop: 'clientCompanyName',
          },
          {
            label: '合同',
            prop: 'isContract',
          },
          {
            label: '技术协议',
            prop: 'isTechnicalAgreement',
          },
          {
            label: '管理方式',
            prop: 'proManagementStyleName',
          },
          {
            label: '招标编号',
            prop: 'biddingNo',
          },
          {
            label: '图纸代号',
            prop: 'attr1',
          },
          {
            label: '阶段',
            prop: 'proStateName',
          },
          {
            label: '项目总金额',
            prop: 'totalProAmount',
            sortable: true,
          },
          {
            label: '项目未签合同金额',
            prop: 'proUnsignedContractAmount',
            sortable: true,
          },
          {
            label: '合同应开票总金额',
            prop: 'conInvoiceableAmount',
            sortable: true,
          },
          {
            label: '合同已开票总金额',
            prop: 'proInvoicedAmount',
            sortable: true,
          },
          {
            label: '合同未开票总金额',
            prop: 'conUnbilledAmount',
            sortable: true,
          },
          {
            label: '已回款总金额',
            prop: 'returnedTotalAmount',
            sortable: true,
          },
          {
            label: '项目已回款比',
            prop: 'proReturnedScale',
            sortable: true,
          },
          {
            label: '质保金比例',
            prop: 'warrantyScale',
            sortable: true,
          },
          {
            label: '质保金金额',
            prop: 'warrantyAmount',
            sortable: true,
          },
          {
            label: '已到期质保金金额',
            prop: 'expirationWarrantyAmount',
            sortable: true,
          },
          {
            label: '项目接收日期',
            prop: 'projectReceiveDates',
          },
          {
            label: '合同签订日期',
            prop: 'contractSigningDate',
          },
          {
            label: '近期开票日期',
            prop: 'recentlyBilledDate',
          },
          {
            label: '近期回款日期',
            prop: 'recentlyPaymentDate',
          },
        ],
      })
      //市场片区下拉框
      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }
      const dragOptions = computed(() => {
        return {
          animation: 600,
          group: 'description',
        }
      })
      const finallyColumns = computed(() => {
        return state.columns.filter((item) =>
          state.checkList.includes(item.label)
        )
      })
      const handleSizeChange = (val) => {
        //console.log(val)
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        //console.log(val)
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        if (state.queryForm.clientCompany == '') {
          delete state.queryForm.clientCompany
        }
        if (state.queryForm.businessType == '') {
          delete state.queryForm.businessType
        }
        if (state.queryForm.urgentGrade == '') {
          delete state.queryForm.urgentGrade
        }
        if (state.queryForm.proManagementStyle == '') {
          delete state.queryForm.proManagementStyle
        }
        if (state.queryForm.proState == '') {
          delete state.queryForm.proState
        }
        if (state.queryForm.isPeriodMoneyZero == '') {
          delete state.queryForm.isPeriodMoneyZero
        }
        if (state.queryForm.proReturnedScale == '') {
          delete state.queryForm.proReturnedScale
        }
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await GetSellBasicsList(state.queryForm)
        state.list = list
        // state.list.forEach((item) => {
        //   item.salesPrice = parseFloat(item.salesPrice).toFixed(2)
        //   item.proSignedCtAmount = parseFloat(item.proSignedCtAmount).toFixed(2)
        //   item.ctInvoicedAmount = parseFloat(item.ctInvoicedAmount).toFixed(2)
        //   item.proPaymentCollectionRatio = parseFloat(
        //     item.proPaymentCollectionRatio
        //   ).toFixed(2)
        //   item.ctUnInvoicedAmount = parseFloat(item.ctUnInvoicedAmount).toFixed(
        //     2
        //   )
        //   item.amountPaymentCollection = parseFloat(
        //     item.amountPaymentCollection
        //   ).toFixed(2)
        // })
        state.total = total
        state.listLoading = false
      }
      const loadSearch = async (row, treeNode, resolve) => {
        const ByParentIdList = await GetParentSellBasicsList({ Id: row.id })
        //console.log(ByParentIdList.data)
        resolve(ByParentIdList.data)
      }
      const containerRef = ref()
      const { toggle, isFullscreen } = useFullscreen(containerRef)
      const clickFullScreen = () => {
        toggle().then(() => {
          handleHeight()
          state['tableSortRef'].doLayout()
        })
      }
      const handleHeight = () => {
        if (isFullscreen.value) state.height = $baseTableHeight(1) + 200
        else state.height = $baseTableHeight(1)
      }

      const handleFold = () => {
        state.fold = !state.fold
        handleHeight2()
      }
      const handleHeight2 = () => {
        if (state.fold) state.height = $baseTableHeight(2) - 47
        else state.height = $baseTableHeight(3) - 30
      }
      //业务类型
      const GetBusinesstypeData = async () => {
        const BusinesstypeList = await GetBusinesstype()
        state.BusinesstypeOption = BusinesstypeList.data
      }
      //重要程度
      const GetUrgentGradeData = async () => {
        const UrgentGradeList = await GetUrgentGrade()
        state.UrgentGradeOption = UrgentGradeList.data
      }
      //管理方式
      const GetProManagementStyleData = async () => {
        const ProManagementStyleList = await GetProManagementStyle()
        state.ProManagementStyleOption = ProManagementStyleList.data
      }
      //ClientCompanyOption
      //管理方式
      const GetClientCompanyData = async () => {
        const ClientCompanyList = await GetClientCompany()
        state.ClientCompanyOption = ClientCompanyList.data
      }
      //ProStateOption
      //状态
      const GetProStateData = async () => {
        const ProStateList = await GetProState()
        state.ProStateOption = ProStateList.data
      }
      //项目分类
      const GetProjectClassData = async () => {
        const ProjectClassList = await GetProjectClass()
        state.ProjectClassOption = ProjectClassList.data
      }
      onMounted(() => {
        fetchData()
        GetBusinesstypeData()
        GetUrgentGradeData()
        GetProManagementStyleData()
        GetClientCompanyData()
        GetProStateData()
        GetProjectClassData()
        fetchMarketArea()
      })

      return {
        ...toRefs(state),
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        getByParentIdList,
        GetSellBasicsList,
        GetParentSellBasicsList,
        loadSearch,
        clickFullScreen,
        handleFold,
        handleHeight2,
        GetBusinesstype,
        GetUrgentGrade,
        GetProManagementStyle,
        GetClientCompany,
        GetProState,
        GetSalseAreaSelect,
        fetchMarketArea,
        finallyColumns,
        dragOptions,
        containerRef,
        isFullscreen,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>

<style lang="scss" scoped>
  @use 'sass:math';
  .custom-table-container {
    :deep() {
      i {
        cursor: pointer;
      }
    }

    .right-panel {
      .stripe-panel,
      .border-panel {
        margin: 0 10px #{math.div($base-margin, 2)} 10px !important;

        :deep() {
          .el-checkbox__label {
            margin-left: 0 !important;
          }
        }
      }

      [class*='ri'] {
        font-size: $base-font-size-big;
        color: var(--el-color-black);
      }
    }
  }
</style>
<style lang="scss">
  html body .custom-table-checkbox {
    [class*='ri'] {
      vertical-align: -0.5px !important;
      cursor: pointer;
    }

    .el-checkbox {
      margin: 5px 0 5px 8px;
    }
    height: 600px;
    overflow: auto;
  }

  .custom-table-radio {
    width: 240px !important;
  }
</style>
