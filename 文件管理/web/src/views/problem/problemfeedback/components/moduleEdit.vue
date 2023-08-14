<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="980px"
    @close="close"
  >
    <el-tabs v-model="activeName" type="card">
      <el-tab-pane label="基础信息" name="JC">
        <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
          <el-row>
            <el-col :span="24">
              <el-form-item label="项目编号" prop="pt_ID">
                <rw-project v-model="form.pt_ID" :style="{ width: '100%' }" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="发生地址" prop="addressId">
                <el-select
                  v-model="form.addressId"
                  class="m-2"
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
                  class="m-2"
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
                  :maxlength="500"
                  show-word-limit
                  :style="{ width: '100%' }"
                  type="textarea"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <el-row>
          <el-col :span="12" />
          <el-col :span="12">
            <el-button type="primary" @click="save">保存</el-button>
          </el-col>
        </el-row>
      </el-tab-pane>
      <el-tab-pane label="责任判定" name="PD">
        <el-form
          ref="formRef1"
          label-width="120px"
          :model="form1"
          :rules="rules1"
        >
          <el-row>
            <el-col :span="12">
              <el-form-item label="问题类型" prop="problemTypeID">
                <el-select
                  v-model="form1.problemTypeID"
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
                <rw-user
                  v-model="form1.solutionId"
                  disabled
                  filterable
                  style="width: 100%"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="原因类型" prop="reasonType">
                <el-select
                  v-model="form1.reasonType"
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
                  v-model="form1.feedbackTime"
                  :style="{ width: '100%' }"
                  type="date"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item label="判定原因" prop="pfb_ExceptionDesc">
                <el-input
                  v-model="form1.pfb_ExceptionDesc"
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
        <el-row>
          <el-col :span="12" />
          <el-col :span="12">
            <el-button type="primary" @click="save1">保存</el-button>
          </el-col>
        </el-row>
      </el-tab-pane>
      <el-tab-pane label="问题处理" name="CL">
        <el-form
          ref="formRef2"
          label-width="120px"
          :model="form2"
          :rules="rules2"
        >
          <el-row>
            <el-col :span="12">
              <el-form-item label="计划开始时间" prop="planStartTime">
                <el-date-picker
                  v-model="form2.planStartTime"
                  :style="{ width: '100%' }"
                  type="date"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="计划结束时间" prop="planEndTime">
                <el-date-picker
                  v-model="form2.planEndTime"
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
                  v-model="form2.estimatedSettlementDate"
                  :style="{ width: '100%' }"
                  type="date"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="原因分析" prop="causeAnalysis">
                <el-input
                  v-model="form2.causeAnalysis"
                  :autosize="{ minRows: 4, maxRows: 4 }"
                  :maxlength="500"
                  show-word-limit
                  :style="{ width: '100%' }"
                  type="textarea"
                />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="改善措施" prop="improvement">
                <el-input
                  v-model="form2.improvement"
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
        <el-row>
          <el-col :span="12" />
          <el-col :span="12">
            <el-button type="primary" @click="save2">保存</el-button>
          </el-col>
        </el-row>
      </el-tab-pane>
      <el-tab-pane label="结果验证" name="YZ">
        <el-form
          ref="formRef3"
          label-width="120px"
          :model="form3"
          :rules="rules3"
        >
          <el-row>
            <el-col :span="24">
              <el-form-item label="验证结果" prop="isQualified">
                <el-radio-group
                  v-model="form3.isQualified"
                  style="margin-top: -4px"
                  @change="isQualifiedChange"
                >
                  <el-radio :label="0" size="large">合格</el-radio>
                  <el-radio :label="1" size="large">不合格</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item
                v-show="isHG"
                label="不合格原因"
                prop="unqualifiedReason"
                :rules="
                  form3.isQualified == 0
                    ? []
                    : [
                        {
                          required: true,
                          trigger: 'blur',
                          message: '请输入不合格原因',
                        },
                      ]
                "
              >
                <el-input
                  v-model="form3.unqualifiedReason"
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
            <el-col :span="12">
              <el-form-item label="处理动态" prop="dealWithDynamic">
                <el-select
                  v-model="form3.dealWithDynamic"
                  class="m-2"
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
            <el-col :span="12">
              <el-form-item
                v-show="isHG2"
                label="实际完成时间："
                prop="actualSettlementDate"
                :rules="
                  form3.isQualified == 1
                    ? []
                    : [
                        {
                          required: true,
                          trigger: 'blur',
                          message: '请选择实际完成时间',
                        },
                      ]
                "
              >
                <el-date-picker
                  v-model="form3.actualSettlementDate"
                  :style="{ width: '100%' }"
                  type="date"
                />
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <el-row>
          <el-col :span="12" />
          <el-col :span="12">
            <el-button type="primary" @click="save3">保存</el-button>
          </el-col>
        </el-row>
      </el-tab-pane>
    </el-tabs>
  </el-dialog>
</template>

<script>
  import { doEdit, getProblemType } from '@/api/problemProblemfeedback'
  import { ref } from 'vue'
  import RwUser from '@/plugins/RwUser'
  import RwProject from '@/plugins/RwProject'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'ProblemfeedbackEdit',
    components: { RwProject, RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const activeName = ref('JC')
      const state = reactive({
        headers: { authorization: '' },
        formRef: null,
        formRef1: null,
        formRef2: null,
        formRef3: null,
        form: {
          roles: [],
          imgFilesId: '',
        },
        form1: {},
        form2: {},
        form3: {},
        rules: {
          pt_ID: [
            { required: true, trigger: 'blur', message: '请选择项目编号' },
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
        },
        rules1: {
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
        },
        rules2: {
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
        },
        rules3: {
          isQualified: [
            { required: true, trigger: 'blur', message: '请选择验证结果' },
          ],
          dealWithDynamic: [
            { required: true, trigger: 'blur', message: '请选择处理动态' },
          ],
        },
        title: '',
        fileUrl: '',
        dialogFormVisible: false,
        dialogVisible: false,
        dialogImageUrl: '',
        editFiles: [],
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
        sourceOptions: [],
        reasonTypeOptions: [],
        dealWithDynamicOptions: [],
        disabledDate(time) {
          return time.getTime() <= new Date(state.form.feedbackTime)
        },
        disabledDate1(time) {
          return time.getTime() < Date.now()
        },
        fileList: [],
        row: [],
        isHG: false,
        isHG2: false,
      })
      const fetchDataM = async () => {
        const SearchoptionsData = await getProblemType()
        state.options = SearchoptionsData.data
        const dictStore = useDictionaryStore()
        state.reasonTypeOptions = dictStore.getByType('ReasonType')
        state.sourceOptions = dictStore.getByType('ProblemSource')
        state.dealWithDynamicOptions = dictStore
          .getByType('ProblemFBStatus')
          .sort(function (a, b) {
            return a.value > b.value ? 1 : -1
          })
      }

      const showEdit = (row) => {
        if (row.planEndTime.indexOf('0001-01-01') != -1) {
          row.planEndTime = null
        }
        if (row.planStartTime.indexOf('0001-01-01') != -1) {
          row.planStartTime = null
        }
        if (
          row.dealWithDynamic == 'ProblemFBStatus1' ||
          row.dealWithDynamic == 'ProblemFBStatus2'
        ) {
          row.isQualified = ''
        } else if (
          row.dealWithDynamic == 'ProblemFBStatus3' ||
          row.dealWithDynamic == 'ProblemFBStatus4' ||
          row.dealWithDynamic == 'ProblemFBStatus5' ||
          row.dealWithDynamic == 'ProblemFBStatus6'
        ) {
          if (row.isQualified == 0) {
            state.isHG = false
            state.isHG2 = true
          } else {
            state.isHG2 = false
            state.isHG = true
          }
        }
        state.title = '编辑'
        row.imgFilesId = ''
        state.row = row
        state.form = Object.assign({}, row)
        state.form1 = Object.assign({}, row)
        state.form2 = Object.assign({}, row)
        state.form3 = Object.assign({}, row)
        state.dialogFormVisible = true
      }

      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state['formRef1'].resetFields()
        state.form1 = {
          roles: [],
        }
        state['formRef2'].resetFields()
        state.form2 = {
          roles: [],
        }
        state['formRef3'].resetFields()
        state.form3 = {
          roles: [],
        }
        state.dialogFormVisible = false
        emit('fetch-data')
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.isQualified == '') {
              state.form.isQualified = 0
            }
            if (state.form.pt_ID.length != undefined) {
              state.form.pt_ID = state.form.pt_ID[state.form.pt_ID.length - 1]
            }
            state.form.problemTypeID =
              state.form.problemTypeID == '' || state.form.problemTypeID == null
                ? 0
                : state.form.problemTypeID
            const { msg } = await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            close()
          }
        })
      }

      const save1 = () => {
        state['formRef1'].validate(async (valid) => {
          if (valid) {
            if (state.form1.isQualified == '') {
              state.form1.isQualified = 0
            }
            //console.log(state.form1)
            const { msg } = await doEdit(state.form1)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            state.dialogFormVisible = false
          }
        })
      }
      const save2 = () => {
        state['formRef2'].validate(async (valid) => {
          if (valid) {
            if (state.form2.isQualified == '') {
              state.form2.isQualified = 0
            }
            //console.log(state.form2)
            const { msg } = await doEdit(state.form2)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            state.dialogFormVisible = false
          }
        })
      }
      const save3 = () => {
        state['formRef3'].validate(async (valid) => {
          if (valid) {
            if (state.form3.isQualified == '') {
              state.form3.isQualified = 0
            }
            //console.log(state.form3)
            const { msg } = await doEdit(state.form3)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            state.dialogFormVisible = false
          }
        })
      }
      //判断是否合格字段选择事件
      const isQualifiedChange = (val) => {
        if (val == 0) {
          state.form3.isQualified = val
          state.form3.dealWithDynamic = 'ProblemFBStatus4'
          state.isHG = false
          state.isHG2 = true
        } else {
          state.form3.dealWithDynamic = 'ProblemFBStatus3'
          state.isHG2 = false
          state.isHG = true
        }
      }
      onMounted(() => {
        fetchDataM()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        save1,
        save2,
        save3,
        fetchDataM,
        activeName,
        isQualifiedChange,
      }
    },
  })
</script>
