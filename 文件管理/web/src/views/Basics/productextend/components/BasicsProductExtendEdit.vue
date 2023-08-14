<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="700px"
    @close="close"
  >
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <el-form-item label="产品型号" prop="pModelID">
        <el-select
          v-model="form.pModelID"
          clearable
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in ProductionModelRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="字段名称" prop="colName">
        <el-input v-model="form.colName" />
      </el-form-item>
      <el-form-item label="界面列名称" prop="textName">
        <el-input v-model="form.textName" />
      </el-form-item>
      <el-form-item label="字段值" prop="extendValue">
        <el-input v-model="form.extendValue" />
      </el-form-item>
      <el-form-item label="界面列说明" prop="textMemo">
        <el-input v-model="form.textMemo" rows="3" type="textarea" />
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
    GetProductionModelList,
    GetRepeat,
  } from '@/api/basics/productextend'

  export default defineComponent({
    name: 'BasicsProductExtendEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
        },
        ProductionModelRadio: [],
        rules: {
          colName: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同字段名称'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入字段名称'))
                }
              },
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          textName: [
            { required: true, trigger: 'blur', message: '请输入界面列名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          pModelID: [
            { required: true, trigger: 'blur', message: '请选择产品型号' },
          ],
          textMemo: [
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过255个字符',
            },
          ],
          extendValue: [
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })

      //产品型号
      const GetProductionModelData = async () => {
        const ProductionModel = await GetProductionModelList()
        state.ProductionModelRadio = ProductionModel.data
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
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
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
      const inquireData = (id) => {
        state.form.pModelID = id
      }
      onMounted(() => {
        GetProductionModelData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetProductionModelData,
        inquireData,
      }
    },
  })
</script>
