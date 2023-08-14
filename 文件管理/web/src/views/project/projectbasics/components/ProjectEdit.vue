<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="1000px"
    @close="close"
  >
    <el-form
      ref="formRef"
      label-position="right"
      label-width="110px"
      :model="form"
      :rules="rules"
    >
      <el-row>
        <el-col :span="24">
          <el-form-item label="父级项目" prop="parentId">
            <rw-project
              v-model="form.parentId"
              placeholder="输入编号或名称模糊搜索"
              style="width: 100%"
              @change="handleParent"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="项目编号" prop="projectCode">
            <el-input
              v-model.trim="form.projectCode"
              placeholder="请输入项目编号"
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
        <el-col :span="16">
          <el-form-item label="项目名称" prop="projectName">
            <el-input
              v-model.trim="form.projectName"
              placeholder="请输入项目名称"
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="项目分类" prop="projectClass">
            <el-select
              v-model="form.projectClass"
              filterable
              placeholder="请选择项目分类"
              style="width: 100%"
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
        </el-col>
        <el-col :span="8">
          <el-form-item label="业务类型" prop="businessType">
            <el-select
              v-model="form.businessType"
              filterable
              placeholder="请选择业务类型"
              style="width: 100%"
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
        </el-col>
        <el-col :span="8">
          <el-form-item label="紧急程度" prop="urgentGrade">
            <el-rate v-model="form.urgentGrade" style="padding-top: 14px" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="项目状态" prop="projectStatus">
            <el-select
              v-model="form.projectStatus"
              filterable
              placeholder="请选择项目状态"
              style="width: 100%"
            >
              <el-option
                v-for="(item, index) in ProjectStatusOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="客户公司" prop="clientCompany">
            <el-select
              v-model="form.clientCompany"
              filterable
              placeholder="请选择客户公司"
              style="width: 100%"
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
        </el-col>
        <el-col :span="8">
          <el-form-item label="最终业主名称" prop="ownerUnit">
            <el-input
              v-model.trim="form.ownerUnit"
              placeholder="请输入最终业主名称"
              style="width: 100%"
            />
            <!-- <el-select
              v-model="form.OwnerUnit"
              filterable
              placeholder="请选择最终业主名称"
              style="width: 100%"
            >
              <el-option
                v-for="(item, index) in ClientCompanyOption"
                :key="index"
                :disabled="item.disabled"
                :label="item.label"
                :value="item.value"
              />
            </el-select> -->
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="管理方式" prop="proManagementStyle">
            <el-select
              v-model="form.proManagementStyle"
              filterable
              placeholder="请选择管理方式"
              style="width: 100%"
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
        </el-col>
        <el-col :span="8">
          <el-form-item label="项目接收日" prop="projectReceiveDate">
            <el-date-picker
              v-model="form.projectReceiveDate"
              :disabled-date="disabledDate1"
              placeholder="项目接收日期"
              style="width: 100%"
              type="date"
            />
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="预计使用日" prop="expectedUseDate">
            <el-date-picker
              v-model="form.expectedUseDate"
              placeholder="预计使用日期"
              style="width: 100%"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="阶段" prop="proState">
            <el-select
              v-model="form.proState"
              filterable
              placeholder="请选择阶段"
              style="width: 100%"
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
        </el-col>
        <el-col :span="8">
          <el-form-item label="产线类型" prop="proLine">
            <el-select
              v-model="form.proLine"
              filterable
              placeholder="请选择产线类型"
              style="width: 100%"
            >
              <el-option
                v-for="(item, index) in ProLineOption"
                :key="index"
                :label="item.key"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="图纸代号" prop="attr1">
            <el-input
              v-model.trim="form.attr1"
              placeholder="请输入图纸代号"
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="有无合同" prop="isContract">
            <el-radio-group v-model="form.isContract" style="margin-top: -4px">
              <el-radio label="无" size="large">无</el-radio>
              <el-radio label="有" size="large">有</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="有无技术协议" prop="isTechnicalAgreement">
            <el-radio-group
              v-model="form.isTechnicalAgreement"
              style="margin-top: -4px"
            >
              <el-radio label="无" size="large">无</el-radio>
              <el-radio label="有" size="large">有</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="8">
          <el-form-item label="招标编号" prop="biddingNo">
            <el-input v-model="form.biddingNo" placeholder="请输入招标编号" />
          </el-form-item>
        </el-col>
        <el-col v-show="qianxiang" :span="8">
          <el-form-item label="潜项编号" prop="initialProjectCode">
            <el-input
              v-model="form.initialProjectCode"
              placeholder="请输入潜项编号"
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="项目背景" prop="projectBackground">
            <el-input
              v-model="form.projectBackground"
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
          <el-form-item label="项目概述" prop="projectSummary">
            <el-input
              v-model="form.projectSummary"
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
          <el-form-item label="备注" prop="remark">
            <el-input
              v-model="form.remark"
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
      <el-button type="primary" @click="save">保 存</el-button>
    </template>
  </el-dialog>
</template>

<script>
  // import { defineComponent, reactive, toRefs } from 'vue'
  import {
    doAdd,
    doEdit,
    GetProjectClass,
    GetBusinesstype,
    GetUrgentGrade,
    GetProManagementStyle,
    GetClientCompany,
    GetProState,
    GetProBasicsTreeList,
    GetContractList,
    ProjectStatus,
  } from '@/api/project'
  import RwProject from '@/plugins/RwProject'
  import { useDictionaryStore } from '@/store/modules/dictionary'
  export default defineComponent({
    name: 'ProjectEdit',
    components: { RwProject },
    emits: ['fetch-data', 'subset-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        ProjectClassOption: [],
        BusinesstypeOption: [],
        UrgentGradeOption: [],
        ClientCompanyOption: [],
        ProjectStatusOption: [],
        ProManagementStyleOption: [],
        ProStateOption: [],
        TreeListOptions: [],
        contractIDOptions: [],
        ProLineOption: [],
        formRef: null,
        form: {
          projectReceiveDate: '',
          contractDeliveryDate: '',
          expectedUseDate: '',
          salesPrice: 0,
          contractID: 0,
          isContract: '无',
          isTechnicalAgreement: '无',
        },
        rules: {
          projectCode: [
            { required: true, trigger: 'blur', message: '请输入项目编号' },
          ],
          projectName: [
            { required: true, trigger: 'blur', message: '请输入项目名称' },
          ],
          projectClass: [
            { required: true, trigger: 'blur', message: '请选择项目分类' },
          ],
          projectStatus: [
            { required: true, trigger: 'blur', message: '请选择项目状态' },
          ],
          businessType: [
            { required: true, trigger: 'blur', message: '请选择业务类型' },
          ],
          urgentGrade: [
            { required: true, trigger: 'blur', message: '请选择紧急程度' },
          ],
          clientCompany: [
            { required: true, trigger: 'blur', message: '请选择客户公司' },
          ],
          ownerUnit: [
            { required: true, trigger: 'blur', message: '请选择最终业主名称' },
          ],
          proManagementStyle: [
            { required: true, trigger: 'blur', message: '请选择管理方式' },
          ],
          projectReceiveDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择项目接收日期',
            },
          ],
          contractDeliveryDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择合同交货日期',
            },
          ],
          expectedUseDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择预计使用日期',
            },
          ],
          // isContract: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '请选择是否已有合同',
          //   },
          // ],
          // isTechnicalAgreement: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '请选择是否已有技术协议',
          //   },
          // ],
          proState: [
            { required: true, trigger: 'blur', message: '请选择状态' },
          ],
          contractID: [
            { required: true, trigger: 'blur', message: '请选择合同' },
          ],
          salesPrice: [
            { required: true, trigger: 'blur', message: '请输入总价' },
          ],
          proLine: [
            { required: true, trigger: 'blur', message: '请选择产线类型' },
          ],
        },
        propParent: {
          multiple: false,
          label: 'projectName',
          value: 'id',
          children: 'children',
          checkStrictly: true,
        },
        title: '',
        dialogFormVisible: false,
        loading: false,
        qianxiang: true,
        disabledDate(time) {
          return time.getTime() < Date.now()
        },
        disabledDate1(time) {
          return time.getTime() >= Date.now()
        },
      })
      const showEdit = (row, v1, v2) => {
        const dictStore = useDictionaryStore()
        state.ProLineOption = dictStore.getByType('ProLine')
        if (v2 == 'add') {
          state.title = '添加项目信息'
          if (v1 == '非在产') {
            state.qianxiang = false
            state.form.projectClass = 31
            state.form.projectStatus = 216
            state.form.proManagementStyle = 43
            state.form.projectReceiveDate = new Date()
            state.form.proState = 170
          }
          state.form.isContract = '无'
          state.form.isTechnicalAgreement = '无'
        } else {
          state.title = '编辑项目信息'
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (state.form.id != undefined) {
            if (state.form.parentId == state.form.id) {
              $baseMessage(
                '父级项目不能选择自身',
                'error',
                'vab-hey-message-error'
              )
              return false
            }
          }
          if (valid) {
            state.form.contractDeliveryDate = '0001-01-01'
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            emit('subset-data', { Id: state.form.parentId })
            close()
          }
        })
      }
      //项目分类
      const GetProjectClassData = async () => {
        const ProjectClassList = await GetProjectClass()
        state.ProjectClassOption = ProjectClassList.data
      }

      //项目阶段
      const GetProjectStatusOption = async () => {
        const ProjectClassList = await ProjectStatus()
        state.ProjectStatusOption = ProjectClassList.data
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
      //ProStateOption
      //项目信息
      const GetProBasicsTreeListData = async () => {
        const TreeList = await GetProBasicsTreeList()
        state.TreeListOptions = TreeList.data
        //console.log(TreeList)
      }
      //ProBasicsTreeList
      const handleParent = (value) => {
        state.form.parentId = value ? value[value.length - 1] : 0
      }
      //远程搜索合同
      const remoteMethod = async (query) => {
        //console.log(query)
        if (query) {
          state.loading = true
          const contractIDList = await GetContractList({ name: query })
          setTimeout(() => {
            state.loading = false
            //console.log(contractIDList.data)
            state.contractIDOptions = contractIDList.data
          }, 200)
        } else {
          state.contractIDOptions = []
        }
      }

      onMounted(() => {
        GetProjectClassData()
        GetBusinesstypeData()
        GetUrgentGradeData()
        GetProManagementStyleData()
        GetClientCompanyData()
        GetProStateData()
        GetProBasicsTreeListData()
        GetProjectStatusOption()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetProjectClassData,
        GetBusinesstypeData,
        GetUrgentGradeData,
        GetProManagementStyleData,
        GetClientCompanyData,
        GetProStateData,
        handleParent,
        GetProBasicsTreeListData,
        remoteMethod,
        GetProjectStatusOption,
      }
    },
  })
</script>
