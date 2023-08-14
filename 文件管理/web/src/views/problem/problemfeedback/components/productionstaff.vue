<!-- 责任判定：工艺人员编辑 -->
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
        <el-col :span="24">
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
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="问题类型" prop="problemTypeID">
            <el-select
              v-model="form.problemTypeID"
              placeholder="请选择"
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
          <el-form-item label="接收人员" prop="solutionId">
            <rw-user v-model="form.solutionId" filterable style="width: 100%" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="原因类型" prop="reasonType">
            <el-select
              v-model="form.reasonType"
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
        <el-col :span="12">
          <el-form-item label="要求完成时间" prop="feedbackTime">
            <el-date-picker
              v-model="form.feedbackTime"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="处理动态" prop="dealWithDynamic">
            <el-select
              v-model="form.dealWithDynamic"
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
          <el-form-item label="判定原因" prop="pfb_ExceptionDesc">
            <el-input
              v-model="form.pfb_ExceptionDesc"
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
  import RwUser from '@/plugins/RwUser'
  import RwProject from '@/plugins/RwProject'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'ProblemfeedbackEdit',
    components: { RwUser, RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
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
          pfb_ExceptionDesc: [
            { required: true, trigger: 'blur', message: '请输入判定原因' },
          ],
          problemDescription: [
            { required: true, trigger: 'blur', message: '请输入问题描述' },
          ],
          solutionId: [
            { required: true, trigger: 'blur', message: '请选择接收人员' },
          ],
          problemTypeID: [
            { required: true, trigger: 'blur', message: '请选择问题类型' },
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
        dialogVisible: false,
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
      })
      const fetchDataM = async () => {
        const SearchoptionsData = await getProblemType()
        state.options = SearchoptionsData.data
        const dictStore = useDictionaryStore()
        state.reasonTypeOptions = dictStore.getByType('ReasonType')
        state.dealWithDynamicOptions = dictStore.getByType('ProblemFBStatus')
        state.sourceOptions = dictStore.getByType('ProblemSource')
      }

      const showEdit = (row) => {
        state.title = '责任判定编辑'
        state.form = Object.assign({}, row)
        if (state.form.solutionId == 0) {
          state.form.solutionId = null
        }
        if (state.form.problemTypeID == 0) {
          state.form.problemTypeID = null
        }
        state.form.dealWithDynamic = 'ProblemFBStatus2'
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
        emit('fetch-data')
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = await DoDealWith(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
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
      }
    },
  })
</script>
