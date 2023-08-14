<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="24">
          <el-form-item label="项目编号" prop="pt_ID">
            <rw-project
              v-model="form.pt_ID"
              disabled
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="发生地址" prop="addressId">
            <el-select
              v-model="form.addressId"
              disabled
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
        </el-col>
        <el-col :span="12">
          <el-form-item label="问题来源" prop="source">
            <el-select
              v-model="form.source"
              disabled
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in sourceOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="接收人员" prop="solutionId">
            <rw-user
              v-model="form.solutionId"
              disabled
              filterable
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="要求完成时间" prop="feedbackTime">
            <el-date-picker
              v-model="form.feedbackTime"
              disabled
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="问题类型" prop="problemTypeID">
            <el-select
              v-model="form.problemTypeID"
              disabled
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in options"
                :key="item.id"
                :label="item.dictionaryText"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="原因类型" prop="reasonType">
            <el-select
              v-model="form.reasonType"
              disabled
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in reasonTypeOptions"
                :key="item.value"
                :label="item.key"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="问题描述" prop="problemDescription">
            <el-input
              v-model="form.problemDescription"
              :autosize="{ minRows: 4, maxRows: 4 }"
              disabled
              :maxlength="500"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="判定原因" prop="pfb_ExceptionDesc">
            <el-input
              v-model="form.pfb_ExceptionDesc"
              :autosize="{ minRows: 4, maxRows: 4 }"
              disabled
              :maxlength="500"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="计划开始时间" prop="planStartTime">
            <el-date-picker
              v-model="form.planStartTime"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="计划结束时间" prop="planEndTime">
            <el-date-picker
              v-model="form.planEndTime"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="实际开始时间" prop="estimatedSettlementDate">
            <el-date-picker
              v-model="form.estimatedSettlementDate"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="处理动态" prop="dealWithDynamic">
            <el-select
              v-model="form.dealWithDynamic"
              class="m-2"
              disabled
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
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="原因分析" prop="causeAnalysis">
            <el-input
              v-model="form.causeAnalysis"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="改善措施" prop="improvement">
            <el-input
              v-model="form.improvement"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { DoDealWith, getProblemType } from '@/api/problemProblemfeedback'
  import RwProject from '@/plugins/RwProject'
  import RwUser from '@/plugins/RwUser'
  import { useDictionaryStore } from '@/store/modules/dictionary'
  export default defineComponent({
    name: 'Problemdealwith',
    components: { RwProject, RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const dictStore = useDictionaryStore()
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          pt_ID: [
            { required: true, trigger: 'blur', message: '请选择项目编号' },
          ],
          feedbackTime: [
            { required: true, trigger: 'blur', message: '请选择要求完成时间' },
          ],
          addressId: [
            { required: true, trigger: 'blur', message: '请选择发生地址' },
          ],
          source: [
            { required: true, trigger: 'blur', message: '请选择问题来源' },
          ],
          problemDescription: [
            { required: true, trigger: 'blur', message: '请输入问题描述' },
          ],
          pfb_ExceptionDesc: [
            { required: true, trigger: 'blur', message: '请输入判定原因' },
          ],
          solutionId: [
            { required: true, trigger: 'blur', message: '请选择接收人员' },
          ],
          problemTypeID: [
            { required: true, trigger: 'blur', message: '请选择问题类型' },
          ],
          causeAnalysis: [
            { required: true, trigger: 'blur', message: '请输入原因分析' },
          ],
          improvement: [
            { required: true, trigger: 'blur', message: '请输入改善措施' },
          ],
          estimatedSettlementDate: [
            { required: true, trigger: 'blur', message: '请选择实际开始时间' },
          ],
          planStartTime: [
            { required: true, trigger: 'blur', message: '请选择计划开始时间' },
          ],
          planEndTime: [
            { required: true, trigger: 'blur', message: '请选择计划结束时间' },
          ],
          reasonType: [
            { required: true, trigger: 'blur', message: '请选择原因类型' },
          ],
          dealWithDynamic: [
            { required: true, trigger: 'blur', message: '请选择处理动态' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        options: [],
        addresssOptions: [
          {
            label: '厂内',
            value: '1',
          },
          {
            label: '厂外',
            value: '2',
          },
        ],
        reasonTypeOptions: [],
        dealWithDynamicOptions: [],
        sourceOptions: [],
        disabledDate(time) {
          return time.getTime() <= new Date(state.form.feedbackTime)
        },
        disabledDate1(time) {
          return time.getTime() < Date.now()
        },
      })
      const fetchDataM = async () => {
        const SearchoptionsData = await getProblemType()
        state.options = SearchoptionsData.data
        state.reasonTypeOptions = dictStore.getByType('ReasonType')
        state.dealWithDynamicOptions = dictStore.getByType('ProblemFBStatus')
        state.sourceOptions = dictStore.getByType('ProblemSource')
      }

      const showEdit = (row) => {
        state.title = '问题处理编辑'
        state.form = Object.assign({}, row)
        if (state.form.planStartTime.indexOf('0001-01-01') != -1) {
          state.form.planStartTime = null
        }
        if (state.form.planEndTime.indexOf('0001-01-01') != -1) {
          state.form.planEndTime = null
        }
        if (state.form.estimatedSettlementDate == null) {
          state.form.estimatedSettlementDate = new Date()
            .toISOString()
            .slice(0, 10)
        }
        state.form.dealWithDynamic = 'ProblemFBStatus3'
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
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
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = await DoDealWith(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      onMounted(() => {
        fetchDataM()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchDataM,
        changeDate,
      }
    },
  })
</script>
