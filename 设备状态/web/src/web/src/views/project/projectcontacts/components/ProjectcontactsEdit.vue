<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="110px" :model="form" :rules="rules">
      <el-form-item label="父级项目" prop="pid">
        <rw-project
          v-model="form.pid"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="成员：" prop="contactsID">
        <rw-user
          v-model="form.contactsID"
          filterable
          placeholder="请选择成员"
          style="width: 100%"
          @change="ContactsSelectChange"
        />
      </el-form-item>
      <el-form-item label="部门：" prop="dept">
        <el-select
          v-model="form.dept"
          disabled="true"
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
      <el-form-item label="项目角色：" prop="fzbkId">
        <el-select
          v-model="form.fzbkId"
          filterable
          placeholder="请选择项目角色"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in respPlatesOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
    </el-form>

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
    projectList,
    projectContactsList,
    GetConListById,
    respPlatesList,
    getProjectCode,
  } from '@/api/projectcontacts'
  import { GetOrganizationNameList } from '@/api/personalcenter'
  import RwUser from '@/plugins/RwUser'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'ProjectcontactsEdit',
    components: { RwUser, RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      let isAdd = false
      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        title: '',
        loading: false,
        ptname: '',
        projectOptions: [],
        projectContactsOptions: [],
        //contactsTypeOptions: [],
        Searchoptions: [],
        respPlatesOptions: [],
        GetOrganizationNameOption: [],
        rules: {
          pid: [{ required: true, trigger: 'blur', message: '请选择项目名称' }],
          contactsID: [
            { required: true, trigger: 'blur', message: '请选择项目联系人' },
          ],
          // contactsType: [
          //   { required: true, trigger: 'blur', message: '请选择联系人类别' },
          // ],
          fzbkId: [
            { required: true, trigger: 'blur', message: '请选择负责板块' },
          ],
          dept: [{ required: true, trigger: 'blur', message: '请选择部门' }],
        },
        dialogFormVisible: false,
      })

      const fetchProCodeData = async () => {
        const SearchoptionsData = await getProjectCode()
        state.Searchoptions = SearchoptionsData.data
        state.Searchoptions.forEach((item) => {
          item.projectCode = `${item.projectCode} ${item.projectName}`
          if (item.children != undefined) {
            item.children.forEach((item) => {
              item.projectCode = `${item.projectCode} ${item.projectName}`
            })
          }
        })
      }

      const ContactsSelectChange = async (Id) => {
        if (Id != '') {
          const GetListData = await GetConListById({ id: Id })
          //state.form.contactsType = GetListData.data[0].contactsCategory
          state.form.dept = GetListData.data.orgId
        } else {
          //state.form.contactsType = null
          state.form.dept = null
        }
      }

      const showEdit = (row) => {
        //console.log(row.length)
        if (!row) {
          state.title = '添加项目团队'
          isAdd = true
        } else {
          if (row > 0) {
            state.title = '添加项目团队'
            isAdd = true
            state.form.pid = row
          } else {
            isAdd = false
            state.title = '编辑项目团队'
            state.form = JSON.parse(JSON.stringify(row))
            state.projectOptions = [
              {
                label: row.pName,
                value: row.pid,
              },
            ]
            state.projectContactsOptions = [
              {
                label: row.contactsName,
                value: row.contactsID,
              },
            ]
          }
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
          if (valid) {
            state.form.roles = ''
            if (state.form.pid.length != undefined) {
              state.form.pid = state.form.pid[state.form.pid.length - 1]
            }
            const submit = isAdd ? doAdd : doEdit
            const { msg } = await submit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      const respPlatesData = async () => {
        const respPlate = await respPlatesList()
        state.respPlatesOptions = respPlate.data
      }
      //部门
      const GetOrganizationNameData = async () => {
        const GetOrganizationName = await GetOrganizationNameList()
        state.GetOrganizationNameOption = GetOrganizationName.data
      }

      const remoteMethod = async (query) => {
        if (query && query.split(' ').join('').length != 0) {
          state.loading = true
          const project = await projectList({ pName: query })
          setTimeout(() => {
            state.loading = false

            state.projectOptions = project.data
          }, 200)
        } else {
          projectContactsOptions.value = []
        }
      }

      const remoteMethod2 = async (query) => {
        if (query && query.split(' ').join('').length != 0) {
          state.loading = true
          const userName = await projectContactsList({
            contactsName: query,
          })
          setTimeout(() => {
            state.loading = false

            state.projectContactsOptions = userName.data
            console.log(userName.data)
          }, 200)
        } else {
          projectContactsOptions.value = []
        }
      }
      onMounted(() => {
        respPlatesData()
        GetOrganizationNameData()
        fetchProCodeData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        respPlatesData,
        GetOrganizationNameData,
        ContactsSelectChange,
        remoteMethod,
        remoteMethod2,
        fetchProCodeData,
      }
    },
  })
</script>
