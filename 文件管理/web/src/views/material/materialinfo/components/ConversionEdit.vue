<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    draggable
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="物料编码" prop="code">
        <el-input v-model="form.code" style="width: 90%" />
        &nbsp;
        <el-link
          title="搜索物料并带出信息"
          :underline="false"
          @click="handleSeleteMat(form.code)"
        >
          查
        </el-link>
      </el-form-item>
      <el-form-item label="物料名称" prop="name">
        <el-input v-model="form.name" />
      </el-form-item>
      <el-form-item label="规格型号" prop="model">
        <el-input v-model="form.model" />
      </el-form-item>
      <el-form-item label="基本单位" prop="basicUnit">
        <el-input v-model="form.basicUnit" />
      </el-form-item>
      <el-form-item label="换算单位" prop="hsUnit">
        <el-input v-model="form.hsUnit" />
      </el-form-item>
      <el-form-item label="换算数量" prop="count">
        <el-input-number v-model="form.count" :max="999" :min="1" />
      </el-form-item>
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
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import {
    MatHsDoEdit,
    MatHsDoAdd,
    MatListByCode,
  } from '@/api/materialMaterialInfo'

  export default defineComponent({
    name: 'ConversionEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          code: [
            { required: true, trigger: 'blur', message: '请输入物料编码' },
          ],
          name: [
            { required: true, trigger: 'blur', message: '请输入物料名称' },
          ],
          model: [
            { required: true, trigger: 'blur', message: '请输入规格型号' },
          ],
          count: [{ required: true, trigger: 'blur', message: '请输入数量' }],
          basicUnit: [
            { required: true, trigger: 'blur', message: '请输入基本单位' },
          ],
          hsUnit: [
            { required: true, trigger: 'blur', message: '请输入换算单位' },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
        } else {
          state.title = '编辑'
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      //查询物料信息
      const handleSeleteMat = async (code) => {
        if (code != '' && code != null) {
          const { data } = await MatListByCode({ code: code })
          if (data != null && data != undefined) {
            state.form.name = data.name
            state.form.model = data.model
            state.form.basicUnit = data.basicUnit
          } else {
            $baseMessage('无效编码', 'error', 'vab-hey-message-error')
          }
        } else {
          $baseMessage('无效的物料编码', 'error', 'vab-hey-message-error')
        }
      }
      //关闭
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      //保存
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = state.form.id
              ? await MatHsDoEdit(state.form)
              : await MatHsDoAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        handleSeleteMat,
      }
    },
  })
</script>
