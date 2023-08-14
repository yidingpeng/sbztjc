<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="780px"
    @close="close"
  >
    <el-row :gutter="10">
      <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
        <el-col :span="12">
          <el-form-item label="姓名：" prop="contactsName">
            <el-input
              v-model.trim="form.contactsName"
              maxlength="10"
              placeholder="请输入姓名"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="类别：" prop="contactsCategory">
            <el-select
              v-model="form.contactsCategory"
              filterable
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ContactsCategoryOption"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="性别：" prop="sex">
            <el-select v-model="form.sex" filterable :style="{ width: '100%' }">
              <el-option
                v-for="item in SexOption"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="电话1：" prop="telephone1">
            <el-input
              v-model.trim="form.telephone1"
              maxlength="13"
              placeholder="请输入电话1"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="办公地址：" prop="officeAddress">
            <el-input
              v-model.trim="form.officeAddress"
              maxlength="20"
              placeholder="请输入办公地址"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="岗位：" prop="post">
            <el-input
              v-model.trim="form.post"
              maxlength="15"
              placeholder="请输入岗位"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="传真：" prop="fax">
            <el-input
              v-model.trim="form.fax"
              maxlength="15"
              placeholder="请输入传真"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="民族：" prop="nation">
            <el-input
              v-model.trim="form.nation"
              maxlength="10"
              placeholder="请输入民族"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="负责业务：" prop="responsibleBusiness">
            <el-input
              v-model.trim="form.responsibleBusiness"
              maxlength="15"
              placeholder="请输入负责业务"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="邮箱：" prop="email">
            <el-input
              v-model.trim="form.email"
              maxlength="15"
              placeholder="请输入邮箱"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="微信：" prop="wechat">
            <el-input
              v-model.trim="form.wechat"
              maxlength="20"
              placeholder="请输入微信"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="QQ：" prop="qq">
            <el-input
              v-model.trim="form.qq"
              maxlength="15"
              placeholder="请输入QQ"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="历史任职履历：" prop="historyJob">
            <!-- <el-input v-model.trim="form.historyJob" /> -->
            <el-input
              v-model.trim="form.historyJob"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              placeholder="请输入历史任职履历"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="现任职公司：" prop="companyId">
            <!-- :remote-method="remoteMethod" -->
            <el-select
              v-model="form.companyId"
              filterable
              :loading="loading"
              remote
              reserve-keyword
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in CompanyNameOption"
                :key="item.id"
                :label="item.clientName"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="部门：" prop="department">
            <!-- allow-create -->
            <el-select
              v-model="form.departmentV"
              clearable
              filterable
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in GetOrganizationNameOption"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <!-- <el-form-item label="年龄：" prop="age">
            <el-input-number
              v-model.number="form.age"
              class="mx-4"
              controls-position="right"
              :min="1"
            />
          </el-form-item> -->
          <el-form-item label="办公电话：" prop="tel">
            <el-input
              v-model.trim="form.tel"
              maxlength="13"
              placeholder="请输入办公电话"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="电话2：" prop="telephone2">
            <el-input
              v-model.trim="form.telephone2"
              maxlength="13"
              placeholder="请输入电话2"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="出生日期：" prop="birthday">
            <el-date-picker
              v-model="form.birthday"
              :disabled-date="disabledDate"
              placeholder="请选择出生日期"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
          <el-form-item label="爱好：" prop="hobby">
            <el-input
              v-model.trim="form.hobby"
              maxlength="15"
              placeholder="请输入爱好"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="本科学校：" prop="college">
            <el-input
              v-model.trim="form.college"
              maxlength="15"
              placeholder="请输入本科学校"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="本科毕业时间：" prop="collegeAt">
            <el-date-picker
              v-model="form.collegeAt"
              :disabled-date="disabledDate"
              placeholder="请选择本科毕业时间"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
          <el-form-item label="硕博学校：" prop="thurberSchool">
            <el-input
              v-model.trim="form.thurberSchool"
              maxlength="15"
              placeholder="请输入硕博学校"
              show-word-limit
            />
          </el-form-item>
          <el-form-item label="硕博毕业时间：" prop="thurberSchoolAt">
            <el-date-picker
              v-model="form.thurberSchoolAt"
              :disabled-date="disabledDate"
              placeholder="请选择硕博毕业时间"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
          <el-form-item label="行业人脉背景：" prop="contactsBackground">
            <!-- <el-input v-model.trim="form.contactsBackground" /> -->
            <el-input
              v-model.trim="form.contactsBackground"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              placeholder="请输入行业人脉背景"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
          <el-form-item label="备注" prop="Remark">
            <el-input
              v-model.trim="form.remark"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              placeholder="请输入备注"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-form>
    </el-row>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import {
    doAdd,
    doEdit,
    ContactsCategoryList,
    CompanyNameList,
    GetCompanyAllList,
  } from '@/api/contact'
  import { GetOrganizationNameList } from '@/api/personalcenter'
  export default defineComponent({
    name: 'ContactEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      let isAdd = false

      const state = reactive({
        ContactsCategoryOption: [],
        SexOption: [
          {
            value: 1,
            label: '男',
          },
          {
            value: 2,
            label: '女',
          },
          {
            value: 3,
            label: '保密',
          },
        ],
        title: '',
        CompanyNameOption: [],
        GetOrganizationNameOption: [],
        formRef: null,
        loading: false,
        curCompany: '',
        form: {
          roles: [],
        },
        rules: {
          contactsName: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入姓名',
            },
          ],
          sexName: [{ required: true, trigger: 'blur', message: '请选择性别' }],
          companyId: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择单位编号',
            },
          ],
          contactsCategory: [
            { required: true, trigger: 'blur', message: '请选择类别' },
          ],
          // department: [
          //   { required: true, trigger: 'blur', message: '请选择联系人部门' },
          // ],
          sex: [{ required: true, trigger: 'blur', message: '请选择性别' }],
        },
        dialogFormVisible: false,
        disabledDate(time) {
          return time.getTime() > Date.now()
        },
      })

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加干系人'
          isAdd = true
          state.form.contactsCategory = 27
        } else {
          isAdd = false
          state.title = '编辑干系人'
          state.form = JSON.parse(JSON.stringify(row))
          state.form.departmentV =
            state.form.department == 0 ? '' : state.form.department
        }
        state.dialogFormVisible = true
      }

      const GetCompanyOption = async () => {
        const companyList = await GetCompanyAllList()
        state.CompanyNameOption = companyList.data
      }

      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
        GetOrganizationNameData()
      }
      const changeNullDate = (val) => {
        if (val == null) {
          return '0001-01-01 00:00:00'
        }
        return val
      }
      const save = () => {
        const IsInsertDep = Object.prototype.toString.call(
          state.form.department
        )
        if (IsInsertDep == '[object String]') {
          state.form.departmenttext = state.form.department
          state.form.department = -1
        }
        state['formRef'].validate(async (valid) => {
          if (valid) {
            state.form.birthday = changeNullDate(state.form.birthday)
            state.form.thurberSchoolAt = changeNullDate(
              state.form.thurberSchoolAt
            )
            state.form.collegeAt = changeNullDate(state.form.collegeAt)
            state.form.department =
              state.form.departmentV == '' ? 0 : state.form.departmentV
            const submit = isAdd ? doAdd : doEdit
            const { msg } = await submit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      //联系人类别
      const ContactsCategoryData = async () => {
        const ContactsCategory = await ContactsCategoryList()
        state.ContactsCategoryOption = ContactsCategory.data
      }
      //部门
      const GetOrganizationNameData = async () => {
        const GetOrganizationName = await GetOrganizationNameList()
        console.log(GetOrganizationName)
        state.GetOrganizationNameOption = GetOrganizationName.data
      }

      const remoteMethod = async (query) => {
        //判断输入是否为空或是否输入全是空格
        if (query && query.split(' ').join('').length != 0) {
          state.loading = true
          const CompanyName = await CompanyNameList({ curCompany: query })
          setTimeout(() => {
            state.loading = false
            state.CompanyNameOption = CompanyName.data
            console.log(state.CompanyNameOption)
          }, 200)
        } else {
          CompanyNameOption.value = []
        }
      }
      onMounted(() => {
        ContactsCategoryData()
        GetOrganizationNameData()
        GetCompanyOption()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        $baseMessage,
        ContactsCategoryData,
        changeNullDate,
        GetCompanyOption,
        GetOrganizationNameData,
        remoteMethod,
      }
    },
  })
</script>
