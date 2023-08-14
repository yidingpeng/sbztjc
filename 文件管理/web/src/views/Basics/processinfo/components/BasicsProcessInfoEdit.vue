<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="700px"
    @close="close"
  >
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <el-form-item label="工序编号" prop="pcsNo">
        <el-input v-model="form.pcsNo" />
      </el-form-item>
      <el-form-item label="工序名称" prop="pcsName">
        <el-input v-model="form.pcsName" />
      </el-form-item>
      <el-form-item label="父级工序" prop="parentId">
        <el-select
          v-model="form.parentId"
          clearable
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in ParentProcessRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="是否是总装/总拆工序" prop="isFinishGW">
        <el-radio-group v-model="form.isFinishGW">
          <el-radio border :label="0" size="large">是</el-radio>
          <el-radio border :label="1" size="large">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="状态" prop="usingFlag">
        <el-select
          v-model="form.usingFlag"
          allow-create13
          placeholder="选择状态"
        >
          <el-option key="0" label="保存" :value="0" />
          <el-option key="1" label="下发" :value="1" />
          <el-option key="2" label="禁用" :value="-1" />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
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
    doAdd,
    doEdit,
    GetParentProcessList,
    GetRepeat,
  } from '@/api/basics/processinfo'

  export default defineComponent({
    name: 'BasicsProcessInfoEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
        },
        ParentProcessRadio: [],
        rules: {
          pcsNo: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同工序编号'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入工序编号'))
                }
              },
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
          ],
          pcsName: [
            { required: true, trigger: 'blur', message: '请输入工序名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          isFinishGW: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否是总装/总拆工序',
            },
          ],
          usingFlag: [
            { required: true, trigger: 'blur', message: '请选择状态' },
          ],
          remark: [
            {
              max: 255,
              trigger: 'blur',
              message: '长度不能超过255个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })
      //父级工作中心
      const GetParentProcessData = async () => {
        const ParentProcess = await GetParentProcessList(state.form)
        state.ParentProcessRadio = ParentProcess.data
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false

          row.parentId == 0 ? (row.parentId = null) : ''
          state.form = Object.assign({}, row)
        }
        GetParentProcessData()
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
        state.form.parentId == 0 ? (state.form.parentId = null) : ''
        state['formRef'].validate(async (valid) => {
          if (valid) {
            console.log(state.form)
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {})

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetParentProcessData,
      }
    },
  })
</script>
