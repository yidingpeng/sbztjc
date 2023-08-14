<template>
  <div class="detail-container">
    <el-page-header
      :content="
        ListData.projectCode +
        '(' +
        ListData.projectName +
        ')' +
        ListData.problemTypeText +
        '反馈详情页面'
      "
      @back="goBack"
    />
    <el-descriptions border :column="3" title="基本信息">
      <!-- <template #extra>
        <el-button
          v-if="ListData.dealWithDynamic == 'ProblemFBStatus1'"
          v-show="ListData.userName == userData.account ? true : false"
          v-permissions="{ permission: ['problemfeedback:edit'] }"
          type="primary"
          @click="handleEdit(ListData)"
        >
          操作
        </el-button>
      </template> -->
      <el-descriptions-item label="项目编号" width="200px">
        {{ ListData.projectCode }}
      </el-descriptions-item>
      <el-descriptions-item label="项目名称" width="200px">
        {{ ListData.projectName }}
      </el-descriptions-item>
      <el-descriptions-item label="发生地址" width="200px">
        <span v-if="ListData.addressId == '1'">厂内</span>
        <span v-else-if="ListData.addressId == '2'">厂外</span>
      </el-descriptions-item>
      <el-descriptions-item label="问题来源" span="3">
        {{ ListData.source }}
      </el-descriptions-item>
      <el-descriptions-item label="问题描述" span="3">
        {{ ListData.problemDescription }}
      </el-descriptions-item>
    </el-descriptions>
    <el-descriptions
      border
      :column="3"
      style="padding-top: 15px"
      title="责任判定（工艺人员）"
    >
      <!-- <template #extra>
        <el-button
          v-if="ListData.dealWithDynamic == 'ProblemFBStatus1'"
          v-show="ListData.solutionName == userData.account ? true : false"
          v-permissions="{ permission: ['problemfeedback:panze'] }"
          type="primary"
          @click="handleDelWith2(ListData)"
        >
          操作
        </el-button>
      </template> -->
      <el-descriptions-item label="问题类型" width="200px">
        {{ ListData.problemTypeText }}
      </el-descriptions-item>
      <el-descriptions-item label="接收人员" width="200px">
        {{ ListData.solutionName }}
      </el-descriptions-item>
      <el-descriptions-item label="要求完成时间" span="3" width="200px">
        {{ DateTransform(ListData.feedbackTime) }}
      </el-descriptions-item>
      <el-descriptions-item label="判定原因" span="3" width="200px">
        {{ ListData.pfb_ExceptionDesc }}
      </el-descriptions-item>
    </el-descriptions>
    <el-descriptions
      border
      :column="3"
      style="padding-top: 15px"
      title="问题处理（接收人员）"
    >
      <!-- <template #extra>
        <el-button
          v-if="ListData.dealWithDynamic != 'ProblemFBStatus2'"
          v-show="ListData.solutionName == userData.account ? true : false"
          v-permissions="{ permission: ['problemfeedback:chuli'] }"
          type="primary"
          @click="handleDelWith(ListData)"
        >
          操作
        </el-button>
      </template> -->
      <el-descriptions-item label="计划开始时间" width="200px">
        {{ ListData.planStartTime }}
      </el-descriptions-item>
      <el-descriptions-item label="计划完成时间" width="200px">
        {{ ListData.planEndTime }}
      </el-descriptions-item>
      <el-descriptions-item label="实际开始时间" width="200px">
        {{ DateTransform(ListData.estimatedSettlementDate) }}
      </el-descriptions-item>
      <el-descriptions-item label="原因分析" :span="3" width="200px">
        {{ ListData.causeAnalysis }}
      </el-descriptions-item>
      <el-descriptions-item label="改善措施" :span="3" width="200px">
        {{ ListData.improvement }}
      </el-descriptions-item>
    </el-descriptions>
    <el-descriptions
      border
      :column="3"
      style="padding-top: 15px"
      title="验证结果（反馈人员）"
    >
      <!-- <template #extra>
        <el-button
          v-if="ListData.dealWithDynamic == 'ProblemFBStatus3'"
          v-show="ListData.userName == userData.account ? true : false"
          v-permissions="{ permission: ['problemfeedback:yanzheng'] }"
          type="primary"
          @click="handleDelWith1(ListData)"
        >
          操作
        </el-button>
      </template> -->
      <el-descriptions-item label="验证结果" :span="3" width="200px">
        <el-tag
          v-if="ListData.isQualified == 0"
          v-show="
            ListData.dealWithDynamic == 'ProblemFBStatus4' ||
            ListData.dealWithDynamic == 'ProblemFBStatus5' ||
            ListData.dealWithDynamic == 'ProblemFBStatus6'
          "
          effect="light"
          style="color: green"
        >
          合格
        </el-tag>
        <el-tag
          v-else-if="ListData.isQualified == 1"
          effect="light"
          style="color: red"
        >
          不合格
        </el-tag>
      </el-descriptions-item>
      <el-descriptions-item label="处理动态" :span="3">
        <!-- {{ ListData.dealWithDynamicTxt }} -->
        <el-tag
          v-if="ListData.dealWithDynamic == 'ProblemFBStatus1'"
          effect="light"
          style="color: red"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
        <el-tag
          v-else-if="ListData.dealWithDynamic == 'ProblemFBStatus2'"
          effect="light"
          style="color: #ff571a"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
        <el-tag
          v-else-if="ListData.dealWithDynamic == 'ProblemFBStatus3'"
          effect="light"
          style="color: #ff7c4d"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
        <el-tag
          v-else-if="ListData.dealWithDynamic == 'ProblemFBStatus4'"
          effect="light"
          style="color: green"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
        <el-tag
          v-else-if="ListData.dealWithDynamic == 'ProblemFBStatus5'"
          effect="light"
          style="color: #7ba428"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
        <el-tag
          v-else-if="ListData.dealWithDynamic == 'ProblemFBStatus6'"
          effect="light"
          style="color: #7ba428"
        >
          {{ ListData.dealWithDynamicTxt }}
        </el-tag>
      </el-descriptions-item>
      <el-descriptions-item
        v-if="ListData.isQualified == true"
        label="实际完成时间"
        :span="3"
      >
        {{ DateTransform(ListData.actualSettlementDate) }}
      </el-descriptions-item>
      <el-descriptions-item
        v-if="ListData.isQualified == false"
        label="不合格原因"
        :span="3"
      >
        {{ ListData.unqualifiedReason }}
      </el-descriptions-item>
    </el-descriptions>
    <edit ref="editRef" @fetch-data="problembyid" />
    <dealwith ref="dealwithRef" @fetch-data="problembyid" />
    <dealwith1 ref="dealwithRef1" @fetch-data="problembyid" />
    <dealwith2 ref="dealwithRef2" @fetch-data="problembyid" />
  </div>
</template>

<script>
  import { useTabsStore } from '@/store/modules/tabs'
  import { handleActivePath } from '@/utils/routes'
  import { Refresh } from '@element-plus/icons-vue'
  import { GetFeedbackById } from '@/api/problemProblemfeedback'
  import Edit from './components/problemfeedbackEdit'
  import dealwith from './components/problemdealwith'
  import dealwith1 from './components/problemdealwith1'
  import dealwith2 from './components/productionstaff.vue'
  import { getPersonal } from '@/api/system/user'

  export default defineComponent({
    name: 'Detail',
    components: { Edit, dealwith, dealwith1, dealwith2 },
    setup() {
      const route = useRoute()
      const router = useRouter()

      const tabsStore = useTabsStore()
      const { changeTabsMeta, delVisitedRoute } = tabsStore

      const state = reactive({
        route: { query: { title: '加载中' } },
        rate: 0,
        form: {
          text: '',
        },
        title: '加载中',
        ListData: [],
        editRef: null,
        dealwithRef: null,
        dealwithRef1: null,
        dealwithRef2: null,
        userData: [],
      })

      const goBack = async () => {
        const detailPath = handleActivePath(route, true)
        await router.push('/problem/problemfeedback')
        delVisitedRoute(detailPath)
      }

      //根据ID获取问题反馈信息
      const problembyid = async () => {
        const content = await GetFeedbackById({ Id: route.query.id })
        state.ListData = content.data[0]
        if (state.ListData.planStartTime.indexOf('0001-01-01') != -1) {
          state.ListData.planStartTime = ''
        } else {
          state.ListData.planStartTime = DateTransform(
            state.ListData.planStartTime
          )
        }
        if (state.ListData.planEndTime.indexOf('0001-01-01') != -1) {
          state.ListData.planEndTime = ''
        } else {
          state.ListData.planEndTime = DateTransform(state.ListData.planEndTime)
        }
        //console.log(state.ListData)
      }
      //日期转换
      const DateTransform = (val) => {
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
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }
      const handleDelWith = (row) => {
        state.dealwithRef.showEdit(row)
      }
      const handleDelWith1 = (row) => {
        state.dealwithRef1.showEdit(row)
      }
      const handleDelWith2 = (row) => {
        state.dealwithRef2.showEdit(row)
      }
      const userData = async () => {
        const {
          data: { userInfo },
        } = await getPersonal()
        state.userData = userInfo
        //console.log(userInfo)
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
        problembyid()
        userData()
      })

      return {
        ...toRefs(state),
        goBack,
        problembyid,
        DateTransform,
        handleEdit,
        handleDelWith,
        handleDelWith1,
        handleDelWith2,
        Refresh,
      }
    },
  })
</script>
