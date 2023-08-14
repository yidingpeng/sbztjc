<template>
  <div class="project-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目编码">
            <el-input
              v-model="queryForm.projectCode"
              placeholder="请输入项目编码"
              style="width: 200px"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目名称">
            <el-input
              v-model="queryForm.projectName"
              placeholder="请输入项目名称"
              style="width: 200px"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="客户公司" prop="clientCompany">
            <el-select
              v-model="queryForm.clientCompany"
              clearable
              filterable
              placeholder="请选择客户公司"
              style="width: 200px"
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
          <!-- &nbsp;
          <el-form-item v-show="!fold" label="项目分类">
            <el-select
              v-model="queryForm.projectClass"
              clearable
              filterable
              placeholder="请选择项目分类"
              style="width: 200px"
            >
              <el-option
                v-for="(item, index) in ProjectClassOption"
                :key="index"
                :disabled="item.label == '潜在项目' ? true : item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item> -->
          &nbsp;
          <el-form-item v-show="!fold" label="业务类型" prop="businessType">
            <el-select
              v-model="queryForm.businessType"
              clearable
              filterable
              placeholder="请选择业务类型"
              style="width: 200px"
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
          <el-form-item v-show="!fold" label="接收日期范围">
            <el-date-picker
              v-model="queryForm.projectReceiveDateS"
              end-placeholder="结束时间"
              range-separator="~"
              start-placeholder="开始时间"
              type="daterange"
            />
          </el-form-item>
          &nbsp;
          <el-form-item
            v-show="!fold"
            label="管理方式"
            prop="proManagementStyle"
          >
            <el-select
              v-model="queryForm.proManagementStyle"
              clearable
              filterable
              placeholder="请选择管理方式"
              style="width: 200px"
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
            label="阶  段"
            :label-width="67"
            prop="proState"
          >
            <el-select
              v-model="queryForm.proState"
              clearable
              filterable
              placeholder="请选择阶段"
              style="width: 200px"
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
              style="width: 200px"
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
              v-model.trim="queryForm.personInCharge"
              placeholder="请输入营销经理"
              style="width: 200px"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="紧急程度" prop="urgentGrade">
            <el-select
              v-model="queryForm.urgentGrade"
              clearable
              filterable
              placeholder="请选择紧急程度"
              style="width: 200px"
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
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['project:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleAdd"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['project:notice'] }"
          type="primary"
          @click="handleNewsFeed"
        >
          一键发送待办
        </el-button>
        <el-button
          v-permissions="{ permission: ['project:derive'] }"
          type="primary"
          @click="handleDataDerive"
        >
          数据导出
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>
    <el-table
      ref="tableRef"
      v-loading="listLoading"
      border
      :data="list"
      lazy
      :load="loadSearch"
      row-key="id"
      :scrollbar-always-on="true"
      style="width: 100%"
      :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
    >
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
        width="160"
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
        width="130"
      />
      <el-table-column
        align="center"
        label="客户公司"
        prop="clientCompanyName"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="项目状态"
        prop="projectStatusName"
        show-overflow-tooltip
        width="90"
      />
      <el-table-column align="center" label="阶段" width="85">
        <template #default="{ row }">
          <el-tag :type="statusFilter(row.proStateName)">
            {{ row.proStateName }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column align="center" label="紧急程度" width="145">
        <template #default="{ row }">
          <el-rate
            v-model="row.urgentGrade"
            disabled
            size="small"
            text-color="#ff9900"
          />
        </template>
      </el-table-column>
      <el-table-column align="center" label="产线类型" width="130">
        <template #default="{ row }">
          <el-select
            v-model="row.proLine"
            disabled
            placeholder="暂无数据"
            size="small"
          >
            <el-option
              v-for="(item, index) in ProLineOption"
              :key="index"
              :label="item.key"
              :value="item.value"
            />
          </el-select>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="项目接收日"
        prop="projectReceiveDates"
        width="100"
      />
      <el-table-column
        align="center"
        label="项目分类"
        prop="projectClassName"
        width="100"
      />
      <el-table-column
        align="center"
        label="业务类型"
        prop="businessTypeName"
        width="120"
      />
      <el-table-column
        align="center"
        label="市场片区"
        prop="marketAreaTxt"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="营销经理"
        prop="personInChargeName"
        show-overflow-tooltip
        width="90"
      />
      <el-table-column align="center" label="项目动态" width="90px">
        <template #default="{ row }">
          <el-link
            title="查看动态"
            :underline="false"
            @click="handleProDynamic(row)"
          >
            <vab-icon icon="eye-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['project:addTrends'] }"
            title="新增动态"
            :underline="false"
            @click="handleAddDT(row)"
          >
            <vab-icon icon="add-box-line" />
          </el-link>
        </template>
      </el-table-column>
      <el-table-column align="center" label="设计计划" width="100px">
        <template #default="{ row }">
          <el-link
            title="新增项目计划"
            :underline="false"
            @click="handleAddProWorkPlan(row)"
          >
            <vab-icon icon="file-add-line" />
          </el-link>
          <el-link
            v-if="row.projectBasicFileId"
            title="查看项目计划"
            :underline="false"
            @click="handleShowProWorkPlan(row)"
          >
            <vab-icon icon="file-search-line" />
          </el-link>
          <el-link
            v-if="row.projectBasicFileId"
            title="下载项目计划"
            :underline="false"
            @click="handleDownloadProWorkPlan(row)"
          >
            <vab-icon icon="download-2-line" />
          </el-link>
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="150px">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['project:detail'] }"
            title="详情"
            :underline="false"
            @click="handleDetail(row)"
          >
            <vab-icon icon="book-read-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['project:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['project:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['project:editState'] }"
            title="更新状态"
            :underline="false"
            @click="handleEditProState(row)"
          >
            <vab-icon icon="chat-heart-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['project:editRemark'] }"
            title="更新备注"
            :underline="false"
            @click="handleEditProRemark(row)"
          >
            <vab-icon icon="chat-poll-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" @subset-data="Subsetdata" />
    <edit2 ref="editRef2" @fetch-data="fetchData" />
    <edit3 ref="editRef3" @fetch-data="fetchData" />
    <edit4 ref="editRef4" @fetch-data="fetchData" />
    <edit5 ref="editRef5" />
    <edit6 ref="editRef6" @fetch-data="fetchData" />
    <edit7 ref="editRef7" />
    <!-- <div class="viewItemFile">
      <el-dialog
        v-model="dialogExcelVisible"
        class="ViewItemFileDialog"
        title="文件预览"
      >
        <div class="excel-view-container">
          <div id="excelView" v-html="excelView"></div>
        </div>
      </el-dialog>
    </div> -->
  </div>
</template>

<script>
  import {
    getList,
    getByParentIdList,
    doDelete,
    //GetProjectClass,
    GetBusinesstype,
    GetUrgentGrade,
    GetProManagementStyle,
    GetClientCompany,
    GetProState,
    GetSalseAreaSelect,
    TeamNewsFeed,
    GetFile,
    ExportProList,
  } from '@/api/project'
  import Edit from './components/ProjectEdit'
  import Edit2 from './components/DeviceEdit'
  import Edit3 from './components/ProStateEdit'
  import Edit4 from './components/ProRemarkEdit'
  import Edit5 from './components/ProDynamicList'
  import Edit6 from './components/ProjectFilesUpload'
  import Edit7 from './components/ShowFile'
  import {
    Delete,
    Plus,
    Search,
    Edit as iconEdit,
  } from '@element-plus/icons-vue'
  import { getPersonal } from '@/api/system/user'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'Project',
    components: { Edit, Edit2, Edit3, Edit4, Edit5, Edit6, Edit7 },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const $baseTableHeight = inject('$baseTableHeight')
      const router = useRouter()

      const state = reactive({
        projectReceiveDateS: null,
        excelView: null,
        dialogExcelVisible: false,
        loading: false,
        //ProjectClassOption: [],
        BusinesstypeOption: [],
        UrgentGradeOption: [],
        ClientCompanyOption: [],
        ProManagementStyleOption: [],
        ProStateOption: [],
        marketAreaOptions: [],
        editRef: null,
        editRef2: null,
        editRef3: null,
        editRef4: null,
        editRef5: null,
        editRef6: null,
        editRef7: null,
        tableRef: null,
        fold: true,
        height: $baseTableHeight(3) - 30,
        list: [],
        imageList: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          projectClass: 0, //在产项目
          account: '',
          role: '',
        },
        maps: new Map(),
        filename: '',
        autoWidth: true,
        bookType: 'xlsx',
        ProLineOption: [],
      })
      const userData = async () => {
        const {
          data: { userInfo },
        } = await getPersonal()
        state.queryForm.account = userInfo.account
        state.queryForm.role = userInfo.role
        state.queryForm.projectClass = 30
        fetchData()
      }
      //市场片区下拉框
      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleAdd = () => {
        state['editRef'].showEdit('', '在产', 'add')
      }
      //一键消息发送

      const handleNewsFeed = async () => {
        //await TeamNewsFeed()
        $baseConfirm('确定要发送待办吗？', null, async () => {
          const dbloading = ElLoading.service({
            lock: true,
            text: '发送消息中...',
            background: 'rgba(0, 0, 0, 0.7)',
          })
          const { msg } = await TeamNewsFeed()
          dbloading.close()
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        })
      }
      //handleDataDerive
      //项目数据导出
      const handleDataDerive = async () => {
        //await TeamNewsFeed()
        $baseConfirm('确定要进行导出操作吗？', null, async () => {
          const dbloading = ElLoading.service({
            lock: true,
            text: '数据导出中...',
            background: 'rgba(0, 0, 0, 0.7)',
          })
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
          if (state.queryForm.projectReceiveDateS != null) {
            state.queryForm.projectReceiveDateS[0] = changeDate(
              state.queryForm.projectReceiveDateS[0]
            )
            state.queryForm.projectReceiveDateS[1] = changeDate(
              state.queryForm.projectReceiveDateS[1]
            )
            state.queryForm.projectReceiveDate =
              state.queryForm.projectReceiveDateS.join('~')
          }
          const list = await ExportProList(state.queryForm)
          import('~/src/utils/excel').then((excel) => {
            const tHeader = [
              '序号',
              '父编号',
              '项目编号',
              '项目名称',
              '客户公司',
              '项目状态',
              '项目阶段',
              '紧急程度',
              '项目接收日',
              '项目分类',
              '业务类型',
              '市场片区',
              '营销经理',
            ]
            const filterVal = [
              'id',
              'parentId',
              'projectCode',
              'projectName',
              'clientCompanyName',
              'projectStatusName',
              'proStateName',
              'urgentGrade',
              'projectReceiveDate',
              'projectClassName',
              'businessTypeName',
              'marketAreaTxt',
              'personInChargeName',
            ]
            const data = formatJson(filterVal, list.data)
            excel.export_json_to_excel({
              header: tHeader,
              data,
              filename: '项目数据列表',
              autoWidth: state.autoWidth,
              bookType: state.bookType,
            })
          })
          dbloading.close()
        })
      }
      const formatJson = (filterVal, jsonData) => {
        return jsonData.map((v) =>
          filterVal.map((j) => {
            return v[j]
          })
        )
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row, '在产', 'edit')
        } else {
          state.editRef.showEdit('', '在产', 'add')
        }
      }
      //新增动态
      const handleAddDT = (row) => {
        if (row.id) {
          state.editRef2.showEdit(row)
        } else {
          state.editRef2.showEdit()
        }
      }
      //查看动态
      const handleProDynamic = (row) => {
        state.editRef5.showEdit(row)
      }
      //更新状态
      const handleEditProState = (row) => {
        if (row.id) {
          state.editRef3.showEdit(row)
        } else {
          state.editRef3.showEdit()
        }
      }
      //更新备注
      const handleEditProRemark = (row) => {
        if (row.id) {
          state.editRef4.showEdit(row)
        } else {
          state.editRef4.showEdit()
        }
      }
      const handleDelete = (row) => {
        let msgTxt = '你确定要删除当前项吗'
        if (row.hasChildren) {
          msgTxt = '当前选中父项还存有子项，你确定要删除当前项吗'
        }
        if (row.id) {
          $baseConfirm(msgTxt, null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm(msgTxt, null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
      //项目分类
      // const GetProjectClassData = async () => {
      //   const ProjectClassList = await GetProjectClass()
      //   state.ProjectClassOption = ProjectClassList.data
      // }
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
      //客户公司
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
        if (state.queryForm.projectReceiveDateS != null) {
          state.queryForm.projectReceiveDateS[0] = changeDate(
            state.queryForm.projectReceiveDateS[0]
          )
          state.queryForm.projectReceiveDateS[1] = changeDate(
            state.queryForm.projectReceiveDateS[1]
          )
          state.queryForm.projectReceiveDate =
            state.queryForm.projectReceiveDateS.join('~')
        }
        fetchData()
        state.queryForm.projectReceiveDate = ''
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
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.salesPrice = parseFloat(item.salesPrice).toFixed(2)
          item.proSignedCtAmount = parseFloat(item.proSignedCtAmount).toFixed(2)
          item.ctInvoicedAmount = parseFloat(item.ctInvoicedAmount).toFixed(2)
          item.proPaymentCollectionRatio = parseFloat(
            item.proPaymentCollectionRatio
          ).toFixed(2)
          item.ctUnInvoicedAmount = parseFloat(item.ctUnInvoicedAmount).toFixed(
            2
          )
          item.amountPaymentCollection = parseFloat(
            item.amountPaymentCollection
          ).toFixed(2)
        })
        state.total = total
        const dictStore = useDictionaryStore()
        state.ProLineOption = dictStore.getByType('ProLine')
        //ProStateOption
        state.listLoading = false
      }
      const handleFold = () => {
        state.fold = !state.fold
        handleHeight()
      }
      const handleHeight = () => {
        if (state.fold) state.height = $baseTableHeight(2) - 47
        else state.height = $baseTableHeight(3) - 30
      }
      //详情页
      const handleDetail = (row) => {
        if (row.id)
          router.push({
            path: '/project/projectbasics/detail',
            query: {
              id: row.id,
              clientCompany: row.clientCompany,
              timestamp: new Date().getTime(),
            },
          })
        else {
          $baseMessage(
            '请选择一行进行详情页跳转',
            'error',
            'vab-hey-message-error'
          )
        }
      }
      const statusFilter = (status) => {
        const statusMap = {
          已发货: 'success',
          待验收: 'info',
          终验收: 'warning',
          开口项验收: 'danger',
          质保期: 'primary',
        }
        return statusMap[status]
      }
      const loadSearch = async (row, treeNode, resolve) => {
        state.maps.set(row.id, { row, treeNode, resolve }) //将当前选中节点数据存储到maps中
        const ByParentIdList = await getByParentIdList({ Id: row.id })
        // console.log(ByParentIdList.data)
        resolve(ByParentIdList.data)
      }
      //新增修改后重新加载子集数据
      const Subsetdata = (data) => {
        if (data.Id > 0) {
          const { row, treeNode, resolve } = state.maps.get(data.Id) //根据pid取出对应的节点数据
          loadSearch(row, treeNode, resolve)
        }
      }

      //新增项目工作计划
      const handleAddProWorkPlan = (row) => {
        state.editRef6.showEdit(row)
      }
      //查看最新项目工作计划
      const handleShowProWorkPlan = async (row) => {
        state.editRef7.showEdit(row)
      }

      const handleDownloadProWorkPlan = async (row) => {
        let blobData = ''
        blobData = await GetFile({ Pid: row.id })

        const binaryData = []
        binaryData.push(blobData)
        const fileURL = window.URL.createObjectURL(
          new Blob(binaryData, { type: 'application/vnd.ms-excel' })
        )
        const fileLink = document.createElement('a')

        fileLink.href = fileURL
        fileLink.setAttribute('download', `${row.projectCode}项目计划`)
        document.body.appendChild(fileLink)

        fileLink.click()
      }
      // onActivated(() => {
      //   state['tableRef'].doLayout()
      //   userData()
      // })

      onMounted(() => {
        fetchMarketArea()
        GetBusinesstypeData()
        GetUrgentGradeData()
        GetProManagementStyleData()
        GetClientCompanyData()
        GetProStateData()
        userData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleAdd,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleFold,
        GetBusinesstypeData,
        GetUrgentGradeData,
        GetProManagementStyleData,
        GetClientCompanyData,
        GetProStateData,
        handleDetail,
        loadSearch,
        getByParentIdList,
        statusFilter,
        Subsetdata,
        fetchMarketArea,
        handleAddDT,
        handleAddProWorkPlan,
        handleEditProState,
        handleEditProRemark,
        handleProDynamic,
        userData,
        handleNewsFeed,
        handleShowProWorkPlan,
        handleDownloadProWorkPlan,
        handleDataDerive,
        formatJson,
        Delete,
        Plus,
        Search,
        iconEdit,
      }
    },
  })
</script>

<style lang="scss">
  .el-scrollbar__bar.is-horizontal {
    height: 12px;
  }
</style>
