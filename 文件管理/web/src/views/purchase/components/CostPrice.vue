<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="成本价格" prop="costPrice">
        <el-input-number
          v-model="form.costPrice"
          :max="99999999"
          :min="0"
          :precision="2"
          :step="1"
        />
        &nbsp;&nbsp;
        <el-popover placement="right" trigger="click" :width="300">
          <template #reference>
            <el-link title="历史价格" :underline="false" @click="PriceClick()">
              历史价格
            </el-link>
          </template>
          <el-table :data="gridData">
            <el-table-column
              label="物料名称"
              property="materialName"
              width="100"
            />
            <el-table-column label="成本价格" property="costPrice" width="90" />
            <el-table-column
              label="供应商"
              property="supplierText"
              width="100"
            />
          </el-table>
        </el-popover>
        <!-- <el-link title="历史价格" :underline="false" @click="PriceClick(row)">
          历史价格
        </el-link> -->
      </el-form-item>
      <el-form-item>
        <p style="color: red">注：批量操作默认查询第一个物料的历史价格</p>
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { CostPrice, GetByIdList } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'CostPrice',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          costPrice: [
            { required: true, trigger: 'blur', message: '请输入成本价格' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        ids: '',
        gridData: [],
      })

      const showEdit = (ids) => {
        state.title = '填写成本价格'
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
      const PriceClick = async () => {
        const company = (state.ids || '').split(',')
        const data = await GetByIdList({
          Id: company[0],
        })
        state.gridData = data.data
        // if (company.length > 1) {
        //   $baseMessage(
        //     '批量操作无法使用此功能',
        //     'error',
        //     'vab-hey-message-error'
        //   )
        // } else {

        // }
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = await CostPrice({
              ids: state.ids,
              costPrice: state.form.costPrice,
            })
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
        PriceClick,
      }
    },
  })
</script>
