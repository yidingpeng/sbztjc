<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="供应商" prop="supCode">
        <el-select v-model="form.supCode" filterable placeholder="请选择供应商">
          <el-option
            v-for="(item, index) in supplierOptions"
            :key="index"
            :label="item.supName"
            :value="item.supCode"
          />
        </el-select>
        &nbsp;&nbsp;
        <el-popover placement="bottom" trigger="click" :width="200">
          <template #reference>
            <el-link
              title="历史供应商"
              :underline="false"
              @click="SupplierClick()"
            >
              历史供应商
            </el-link>
          </template>
          <el-table :data="gridData">
            <el-table-column
              align="center"
              label="物料名称"
              property="materialName"
              width="100"
            />
            <el-table-column
              align="center"
              label="供应商"
              property="supplierText"
              width="100"
            />
          </el-table>
        </el-popover>
      </el-form-item>
      <el-form-item>
        <p style="color: red">注：批量操作默认查询第一个物料的最近供应商</p>
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
    supplierAllList,
    supplierOpt,
    GetByIdSupplier,
  } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'OptSupplier',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          supCode: [{ required: true, trigger: 'blur', message: '请输入编码' }],
        },
        title: '',
        supplierOptions: [],
        dialogFormVisible: false,
        ids: '',
        gridData: [],
      })

      const supplierList = async () => {
        const data = await supplierAllList()
        // console.log(data.data)
        state.supplierOptions = data.data
      }
      const showEdit = (ids) => {
        state.title = '选择供应商'
        state.ids = ids
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }

      const SupplierClick = async () => {
        const company = (state.ids || '').split(',')
        const data = await GetByIdSupplier({
          Id: company[0],
        })
        state.gridData = data.data
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          console.log(state.ids)
          if (valid) {
            console.log(state.form.supCode)
            const { msg } = await supplierOpt({
              ids: state.ids,
              supCode: state.form.supCode,
            })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      onMounted(() => {
        supplierList()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        supplierList,
        supplierOpt,
        SupplierClick,
      }
    },
  })
</script>
