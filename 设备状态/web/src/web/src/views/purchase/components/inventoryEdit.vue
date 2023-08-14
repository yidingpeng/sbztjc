<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="项目名称" prop="projectCode">
        <rw-project-code
          v-model="form.projectCode"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        />
      </el-form-item>
      <el-form-item label="物料编码" prop="materialCode">
        <el-input v-model="form.materialCode" />
      </el-form-item>
      <el-form-item label="物料名称" prop="materialName">
        <el-input v-model="form.materialName" />
      </el-form-item>
      <el-form-item label="型号" prop="model">
        <el-input v-model="form.model" />
      </el-form-item>
      <el-form-item label="参数" prop="parameter">
        <el-input v-model="form.parameter" />
      </el-form-item>
      <el-form-item label="品牌" prop="brand">
        <el-input v-model="form.brand" />
      </el-form-item>
      <el-form-item label="单位" prop="unit">
        <el-input v-model="form.unit" />
      </el-form-item>
      <el-form-item label="库存数量" prop="warehousCount">
        <el-input-number v-model="form.warehousCount" :min="1" />
      </el-form-item>
      <!-- <el-form-item label="项目号" prop="projectCode">
        <el-input v-model="form.projectCode" />
      </el-form-item>
      <el-form-item label="项目名" prop="projectName">
        <el-input v-model="form.projectName" />
      </el-form-item> -->
      <el-form-item label="供应商" prop="supplier">
        <el-select
          v-model="form.supplier"
          filterable
          placeholder="请选择供应商"
        >
          <el-option
            v-for="(item, index) in supplierOptions"
            :key="index"
            :label="item.supName"
            :value="item.supCode"
          />
        </el-select>
      </el-form-item>

      <el-form-item label="存放位置" prop="storageLocation">
        <el-input v-model="form.storageLocation" />
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" type="textarea" />
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
    InventoryAdd,
    InventoryEdit,
  } from '@/api/purchase/purchase'
  import RwProjectCode from '@/plugins/RwProjectCode'
  export default defineComponent({
    name: 'InventoryEdit',
    components: { RwProjectCode },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          materialCode: [
            { required: true, trigger: 'blur', message: '请输入编码' },
          ],
          materialName: [
            { required: true, trigger: 'blur', message: '请输入名称' },
          ],
          model: [{ required: true, trigger: 'blur', message: '请输入型号' }],
          parameter: [
            { required: true, trigger: 'blur', message: '请输入参数' },
          ],
          brand: [{ required: true, trigger: 'blur', message: '请输入品牌' }],
          unit: [{ required: true, trigger: 'blur', message: '请输入单位' }],
          warehousCount: [
            { required: true, trigger: 'blur', message: '请输入库存数量' },
          ],
          supplier: [
            { required: true, trigger: 'blur', message: '请选择供应商' },
          ],
          storageLocation: [
            { required: true, trigger: 'blur', message: '请输入存放位置' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        supplierOptions: [],
      })
      const supplierList = async () => {
        const data = await supplierAllList()
        state.supplierOptions = data.data
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.form.warehousCount = 1
        } else {
          state.title = '编辑'
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
            if (state.form.projectCode.length != undefined) {
              state.form.projectCode =
                state.form.projectCode[state.form.projectCode.length - 1]
            }
            const { msg } = state.form.id
              ? await InventoryEdit(state.form)
              : await InventoryAdd(state.form)
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
      }
    },
  })
</script>
