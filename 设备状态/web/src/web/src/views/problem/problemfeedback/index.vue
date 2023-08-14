<template>
  <div>
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目名称">
            <el-input
              v-model.trim="queryForm.projectName"
              clearable
              placeholder="请输入项目编码或名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="要求完成时间">
            <el-date-picker
              v-model="queryForm.feedbackTimeS"
              end-placeholder="结束时间"
              range-separator="~"
              start-placeholder="开始时间"
              type="daterange"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="发生地址">
            <el-select
              v-model="queryForm.addressId"
              class="m-2"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in addresssOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="问题类型">
            <el-select
              v-model="queryForm.problemTypeID"
              class="m-2"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in problemTypeOptions"
                :key="item.id"
                :label="item.dictionaryText"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="接收人">
            <rw-user v-model="queryForm.solutionId" filterable />
          </el-form-item>
          &nbsp;
          <el-form-item label="处理动态">
            <el-select
              v-model="queryForm.dealWithDynamic"
              class="m-2"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in dealWithDynamicOptions"
                :key="item.id"
                :label="item.key"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['problemfeedback:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <!-- <el-button
          v-permissions="{ permission: ['problemfeedback:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button> -->
        <el-button
          v-permissions="{ permission: ['problemfeedback:derive'] }"
          type="primary"
          @click="handleDataDerive"
        >
          数据导出
        </el-button>
        <el-button
          v-permissions="{ permission: ['problemfeedback:CSTX'] }"
          type="primary"
          @click="handleSendMessage"
        >
          一键发送超时提醒
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      height="480px"
      :row-class-name="tableRowClassName"
      scrollbar-always-on
      @row-click="tabRowClick"
      @selection-change="setSelectRows"
    >
      <!-- <el-table-column align="center" show-overflow-tooltip type="selection" /> -->
      <el-table-column type="expand">
        <template #default="props">
          <el-descriptions border :column="3">
            <el-descriptions-item
              align="left"
              label="判定原因"
              label-align="center"
              span="3"
              width="120px"
            >
              {{ props.row.pfb_ExceptionDesc }}
            </el-descriptions-item>
            <el-descriptions-item
              align="left"
              label="原因分析"
              label-align="center"
              span="3"
              width="120px"
            >
              {{ props.row.causeAnalysis }}
            </el-descriptions-item>
            <el-descriptions-item
              align="left"
              label="改善措施"
              label-align="center"
              span="3"
              width="120px"
            >
              {{ props.row.improvement }}
            </el-descriptions-item>
          </el-descriptions>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
        width="130"
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
        width="150"
      />
      <el-table-column
        align="center"
        label="发生地址"
        prop="addressId"
        show-overflow-tooltip
        width="82"
      >
        <template #default="scope">
          <span v-if="scope.row.addressId == '1'">厂内</span>
          <span v-else-if="scope.row.addressId == '2'">厂外</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="问题来源"
        prop="source"
        show-overflow-tooltip
        width="86"
      />
      <el-table-column
        align="center"
        label="问题类型"
        prop="problemTypeText"
        show-overflow-tooltip
        width="86"
      />
      <!-- <el-table-column
        align="center"
        label="判定原因"
        prop="pfb_ExceptionDesc"
        show-overflow-tooltip
        width="100"
      /> -->
      <el-table-column
        align="center"
        label="反馈人"
        prop="userName"
        show-overflow-tooltip
        width="72"
      />
      <el-table-column
        align="center"
        label="接收人"
        prop="solutionName"
        show-overflow-tooltip
        width="72"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="创建时间"
        prop="createdAt"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="要求完成时间"
        prop="feedbackTime"
        width="110"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="实际开始时间"
        prop="estimatedSettlementDate"
        show-overflow-tooltip
        width="110"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="实际完成时间"
        prop="actualSettlementDate"
        show-overflow-tooltip
        width="110"
      />
      <!-- <el-table-column
        align="center"
        label="原因分析"
        prop="causeAnalysis"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="改善措施"
        prop="improvement"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="影响时间"
        prop="influenceDate"
        show-overflow-tooltip
        width="90"
      /> -->
      <el-table-column align="center" label="处理动态" width="178">
        <template #default="{ row }">
          <el-tag
            v-if="row.dealWithDynamic == 'ProblemFBStatus1'"
            effect="light"
            style="color: red"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus2'"
            effect="light"
            style="color: #ff571a"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus3'"
            effect="light"
            style="color: #ff7c4d"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus4'"
            effect="light"
            style="color: green"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus5'"
            effect="light"
            style="color: #7ba428"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus6'"
            effect="light"
            style="color: #7ba428"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column align="center" label="图片" prop="picId" width="60">
        <template #default="scope">
          <!-- @click="fetchInsertFileData" -->
          <el-link v-if="scope.row.picId">查看</el-link>
          <span v-else></span>
        </template>
      </el-table-column>
      <el-table-column align="center" fixed="right" label="操作">
        <template #default="{ row }">
          <!-- <el-button
            v-if="row.dealWithDynamic == '1'"
            v-permissions="{ permission: ['problemfeedback:process'] }"
            link type="primary"
            @click="handleDelWith(row)"
          >
            处理
          </el-button> -->
          <el-link
            v-permissions="{ permission: ['problemfeedback:detail'] }"
            title="详"
            :underline="false"
            @click="handleDetail(row)"
          >
            <vab-icon icon="book-read-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:edit'] }"
            title="模块编辑"
            :underline="false"
            @click="handlemoduleEdit(row)"
          >
            编
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:panze'] }"
            :disabled="row.dealWithDynamic == 'ProblemFBStatus1' ? false : true"
            title="判定"
            :underline="false"
            @click="handleDelWith2(row)"
          >
            <vab-icon icon="error-warning-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:chuli'] }"
            :disabled="row.dealWithDynamic == 'ProblemFBStatus2' ? false : true"
            title="处理"
            :underline="false"
            @click="handleDelWith(row)"
          >
            <vab-icon icon="feedback-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:yanzheng'] }"
            :disabled="
              row.dealWithDynamic == 'ProblemFBStatus3' ||
              row.dealWithDynamic == 'ProblemFBStatus5' ||
              row.dealWithDynamic == 'ProblemFBStatus6'
                ? false
                : true
            "
            title="验证"
            :underline="false"
            @click="handleDelWith1(row)"
          >
            <vab-icon icon="chat-check-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['problemfeedback:cuiban'] }"
            title="催办"
            :underline="false"
            @click="handleCuiBan(row)"
          >
            催
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
    <dealwith ref="dealwithRef" @fetch-data="fetchData" />
    <dealwith1 ref="dealwithRef1" @fetch-data="fetchData" />
    <Staff ref="Staff" @fetch-data="fetchData" />
    <module ref="module" @fetch-data="fetchData" />
    <el-dialog
      v-model="dialogPicVisible"
      style="max-width: 320px"
      @close="PicDigClose"
    >
      <img
        v-for="item in imgUrls"
        :key="item.value"
        class="prewImg"
        :src="item"
      />
    </el-dialog>
  </div>
</template>

<script>
  import {
    getList,
    doDelete,
    GetFeedbackIdByPtid,
    getFeedbackImg,
    GetDownloadFilePath,
    GetPictureData,
    getProblemType,
    GetProblemAllList,
    FateMagSend,
  } from '@/api/problemProblemfeedback'
  import Edit from './components/problemfeedbackEdit'
  import dealwith from './components/problemdealwith'
  import dealwith1 from './components/problemdealwith1'
  import Staff from './components/productionstaff.vue'
  import module from './components/moduleEdit.vue'
  import RwUser from '@/plugins/RwUser'
  import { useDictionaryStore } from '@/store/modules/dictionary'
  import { Delete, Plus, Search, Download } from '@element-plus/icons-vue'
  import { GetUserById } from '@/api/system/user'
  import moment from 'moment'

  export default defineComponent({
    name: 'ProblemProblemfeedback',
    components: { Edit, dealwith, dealwith1, RwUser, Staff, module },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const router = useRouter()
      const state = reactive({
        dialogPicVisible: false,
        url: '',
        editRef: null,
        dealwithRef: null,
        dealwithRef1: null,
        Staff: null,
        module: null,
        list: [],
        listLoading: true,
        imgFiles: [],
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        imgUrls: [],
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          projectName: '',
          feedbackTimeS: null,
          solutionId: 0,
          dealWithDynamic: '',
        },
        addresssOptions: [
          {
            label: '请选择',
            value: '0',
          },
          {
            label: '厂内',
            value: '1',
          },
          {
            label: '厂外',
            value: '2',
          },
        ],
        dealWithDynamicOptions: [],
        problemTypeOptions: [],
        filename: '',
        autoWidth: true,
        bookType: 'xlsx',
      })
      //获取问题类型
      const problemType = async () => {
        const SearchoptionsData = await getProblemType()
        state.problemTypeOptions = SearchoptionsData.data
      }
      //根据id获取反馈图片ids
      const GetPicIds = async (param) => {
        const picData = await GetFeedbackIdByPtid({
          pid: param,
        })
        //console.log(picData.ids)
        state.picId = picData.ids
        state.picId.forEach((item) => {
          GetPic(item)
        })
      }
      //获取图片
      const GetPic = async (param) => {
        const picurl = await getFeedbackImg({
          id: param,
        })
        state.imgUrls.push(picurl.url)
        // state.fileLists.push({
        //   url: picurl.url,
        //   extname: 'png',
        //   name: '111.png',
        // })
        state.dialogPicVisible = true
      }

      const PicDigClose = () => {
        state.imgUrls = []
      }

      const tabRowClick = async (row, column) => {
        //console.log(column)
        if (row.picId) {
          if (column.label == '图片') {
            // var result = await GetFilesByPid({ Id: row.id })
            // debugger
            // state.imgFiles = result.data
            // fetchInsertFileData()
            // console.log(result.data)
            GetPicIds(row.id)
          }
        }
      }
      const fetchInsertFileData = async () => {
        const result = await GetDownloadFilePath()
        state.url = result
        state.imgFiles.forEach((item) => {
          window.open(`${state.url}?fileUrl=${item.rootPath}`)
        })
      }
      const PicDataChangeBlob = async (picPath) => {
        const PicData = await GetPictureData({ fileUrl: picPath })
        console.log(PicData)
        const blob = new Blob([PicData])
        const url = window.URL.createObjectURL(blob)
        state.url = url
        state.dialogPicVisible = true
      }

      const statusFilter = (status) => {
        const statusMap = {
          1: 'warning',
          2: 'success',
          3: 'danger',
        }
        return statusMap[status]
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
      const handleDelWith = (row) => {
        state.dealwithRef.showEdit(row)
      }
      const handleDelWith1 = (row) => {
        state.dealwithRef1.showEdit(row)
      }
      const handleDelWith2 = (row) => {
        state.Staff.showEdit(row)
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
        if (state.queryForm.feedbackTimeS != null) {
          state.queryForm.feedbackTimeS[0] = changeDate(
            state.queryForm.feedbackTimeS[0]
          )
          state.queryForm.feedbackTimeS[1] = changeDate(
            state.queryForm.feedbackTimeS[1]
          )
          state.queryForm.feedbackTime = state.queryForm.feedbackTimeS.join('~')
        }
        state.queryForm.pageNo = 1
        state.queryForm.feedbackTime = ''
        if (state.queryForm.projectName == '') {
          delete state.queryForm.projectName
        }
        if (state.queryForm.addressId == '') {
          delete state.queryForm.addressId
        }
        if (state.queryForm.problemTypeID == '') {
          delete state.queryForm.problemTypeID
        }
        if (state.queryForm.dealWithDynamic == '') {
          delete state.queryForm.dealWithDynamic
        }
        if (state.queryForm.solutionId == '') {
          delete state.queryForm.solutionId
        }
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        // list.forEach((item) => {
        //   item.feedbackTime = changeDate(item.feedbackTime)
        //   item.estimatedSettlementDate = changeDate(
        //     item.estimatedSettlementDate
        //   )
        //   item.actualSettlementDate = changeDate(item.actualSettlementDate)
        // })
        state.list = list
        state.total = total
        const dictStore = useDictionaryStore()
        state.dealWithDynamicOptions = dictStore
          .getByType('ProblemFBStatus')
          .sort(function (a, b) {
            return a.value > b.value ? 1 : -1
          })
        state.listLoading = false
      }
      //详情页
      const handleDetail = (row) => {
        if (row.id)
          router.push({
            path: '/problem/problemfeedback/detail',
            query: {
              id: row.id,
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
      //问题反馈数据导出
      const handleDataDerive = async () => {
        $baseConfirm('确定要进行导出操作吗？', null, async () => {
          const dbloading = ElLoading.service({
            lock: true,
            text: '数据导出中...',
            background: 'rgba(0, 0, 0, 0.7)',
          })
          if (state.queryForm.feedbackTimeS != null) {
            state.queryForm.feedbackTimeS[0] = changeDate(
              state.queryForm.feedbackTimeS[0]
            )
            state.queryForm.feedbackTimeS[1] = changeDate(
              state.queryForm.feedbackTimeS[1]
            )
            state.queryForm.feedbackTime =
              state.queryForm.feedbackTimeS.join('~')
          }
          state.queryForm.pageNo = 1
          state.queryForm.feedbackTime = ''
          if (state.queryForm.projectName == '') {
            delete state.queryForm.projectName
          }
          if (state.queryForm.addressId == '') {
            delete state.queryForm.addressId
          }
          if (state.queryForm.problemTypeID == '') {
            delete state.queryForm.problemTypeID
          }
          if (state.queryForm.dealWithDynamic == '') {
            delete state.queryForm.dealWithDynamic
          }
          if (state.queryForm.solutionId == '') {
            delete state.queryForm.solutionId
          }
          const list = await GetProblemAllList(state.queryForm)
          import('~/src/utils/excel').then((excel) => {
            const tHeader = [
              '序号',
              '项目编号',
              '项目名称',
              '发生地址',
              '问题类型',
              '问题描述',
              '判定原因',
              '反馈人',
              '接收人',
              '创建时间',
              '要求完成时间',
              '原因分析',
              '改善措施',
              '实际开始时间',
              '实际完成时间',
              '影响时间',
              '处理动态',
            ]
            const filterVal = [
              'id',
              'projectCode',
              'projectName',
              'addressId',
              'problemTypeText',
              'problemDescription',
              'pfb_ExceptionDesc',
              'userName',
              'solutionName',
              'createdAt',
              'feedbackTime',
              'causeAnalysis',
              'improvement',
              'estimatedSettlementDate',
              'actualSettlementDate',
              'influenceDate',
              'dealWithDynamicTxt',
            ]
            const data = formatJson(filterVal, list.data)
            excel.export_json_to_excel({
              header: tHeader,
              data,
              filename: '问题反馈数据列表',
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
      //登录人信息
      const getUserInfo = async () => {
        const data = await GetUserById()
        state.queryForm.solutionId = data.data.id //默认条件：接收人为本人
        state.queryForm.dealWithDynamic = 'ProblemFBStatus2' //默认条件：处理动态为已定责
      }
      const handleCuiBan = async (row) => {
        if (row.dealWithDynamic == 'ProblemFBStatus2') {
          const { msg } = await problemMagSend({
            proId: row.pt_ID,
            userId: row.solutionId,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        } else {
          $baseMessage('处理动态不支持', 'error', 'vab-hey-message-error')
        }
      }
      const handlemoduleEdit = (row) => {
        state.module.showEdit(row)
      }
      const tableRowClassName = (row) => {
        if (row.row.dealWithDynamic === 'ProblemFBStatus2') {
          const time = moment().format('YYYY-MM-DD HH:mm:ss')
          const time2 = moment(row.row.lastModifiedAt).format(
            'YYYY-MM-DD HH:mm:ss'
          )
          const diffTime = moment(time).diff(moment(time2))
          if (diffTime < 7 * 86400000) {
            return ''
          } else if (diffTime > 7 * 86400000 && diffTime < 10 * 86400000) {
            return 'warning-row'
          } else if (diffTime > 10 * 86400000) {
            return 'error-row'
          }
        } else if (row.row.dealWithDynamic === 'ProblemFBStatus4') {
          return 'success-row'
        }
      }
      const dateFormat = (row, column) => {
        let date = null
        if (column.property == 'feedbackTime') {
          date = row.feedbackTime
        } else if (column.property == 'estimatedSettlementDate') {
          date = row.estimatedSettlementDate
        } else if (column.property == 'actualSettlementDate') {
          date = row.actualSettlementDate
        } else if (column.property == 'createdAt') {
          date = row.createdAt
        }
        if (date == null) {
          return ''
        }
        return moment(date).format('YYYY-MM-DD')
      }

      const handleSendMessage = async () => {
        const { msg } = await FateMagSend()
        $baseMessage(msg, 'success', 'vab-hey-message-success')
      }
      onMounted(async () => {
        await getUserInfo()
        await fetchData()
        await problemType()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleDelWith,
        handleDelWith1,
        handleDelWith2,
        tabRowClick,
        handleCurrentChange,
        fetchInsertFileData,
        queryData,
        fetchData,
        statusFilter,
        PicDataChangeBlob,
        Delete,
        Plus,
        Search,
        Download,
        GetPicIds,
        GetPic,
        changeDate,
        PicDigClose,
        handleDetail,
        problemType,
        handleDataDerive,
        formatJson,
        getUserInfo,
        handleCuiBan,
        handlemoduleEdit,
        tableRowClassName,
        dateFormat,
        handleSendMessage,
      }
    },
  })
</script>
<style lang="scss">
  .prewImg {
    width: 300px;
    height: 250px;
    margin-right: 5px;
    border-radius: 10%;
  }
  .el-scrollbar__bar.is-horizontal {
    height: 12px;
  }

  .el-table .error-row {
    --el-table-tr-bg-color: var(--el-color-error-light-8);
  }
  .el-table .warning-row {
    --el-table-tr-bg-color: var(--el-color-warning-light-8);
  }
  .el-table .success-row {
    --el-table-tr-bg-color: var(--el-color-success-light-8);
  }
</style>
