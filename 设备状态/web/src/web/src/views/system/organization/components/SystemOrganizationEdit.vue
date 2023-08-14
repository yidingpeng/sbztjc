<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="上级组织" prop="parent">
        <!-- <rw-organization
          v-model="form.parentId"
          placeholder="不输入表示一级部门"
        /> -->
        <el-cascader
          v-model="form.parentId"
          :empty="0"
          :options="organization"
          placeholder="选择用户所在的组织"
          :props="orgProps"
        />
      </el-form-item>
      <el-form-item label="组织编号" prop="code">
        <el-input v-model="form.code" :maxlength="20" />
      </el-form-item>
      <el-form-item label="组织名称" prop="name">
        <el-input v-model="form.name" :maxlength="20" />
      </el-form-item>
      <el-form-item label="组织类别" prop="organizationType">
        <el-select
          v-model="form.organizationType"
          filterable
          placeholder="请选择组织类别"
        >
          <el-option
            v-for="(item, index) in OrganizationTypeOption"
            :key="index"
            :disabled="item.disabled"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="组织领导" prop="leader">
        <rw-user v-model="form.leader" placeholder="组织领导可为空" />
      </el-form-item>
      <el-form-item label="组织描述" prop="desc">
        <el-input v-model="form.desc" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import {
    doEdit,
    doAdd,
    getList,
    GetOrganizationType,
  } from '@/api/system/organization'
  import RwUser from '@/plugins/RwUser'

  export default defineComponent({
    name: 'SystemOrganizationEdit',
    components: { RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        list: [],
        isAdd: false,
        rules: {
          name: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入部门名称',
            },
          ],
          code: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入部门编号',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
        organization: [],
        OrganizationTypeOption: [],
        orgProps: {
          value: 'id',
          label: 'name',
          checkStrictly: true,
          emitPath: false,
        },
      })
      const created = async () => {
        const { data } = await getList()
        state.organization = data
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        created()
        state.form = {
          orderIndex: 0,
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        if (state.form.id != undefined) {
          if (state.form.parentId == state.form.id) {
            $baseMessage(
              '上级部门不能选择自身',
              'error',
              'vab-hey-message-error'
            )
            return false
          }
        }

        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (!state.form.parentId) state.form.parentId = 0
            if (state.form.leader == '') state.form.leader = null
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      //项目分类
      const GetOrganizationTypeData = async () => {
        const OrganizationTypeList = await GetOrganizationType()
        state.OrganizationTypeOption = OrganizationTypeList.data
      }
      onMounted(() => {
        created()
        GetOrganizationTypeData()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        created,
        GetOrganizationTypeData,
      }
    },
  })
</script>
