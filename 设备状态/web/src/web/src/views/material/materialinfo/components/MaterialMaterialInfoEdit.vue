<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="物料编码" prop="materialCode">
        <el-input v-model="form.materialCode" />
      </el-form-item>
      <el-form-item label="物料名称" prop="materialName">
        <el-input v-model="form.materialName" />
      </el-form-item>
      <el-form-item label="规格型号" prop="specific">
        <el-input v-model="form.specific" />
      </el-form-item>
      <el-form-item label="技术要求" prop="requirements">
        <el-input v-model="form.requirements" />
      </el-form-item>
      <el-form-item label="物料分组" prop="group">
        <el-select v-model="form.group" filterable :style="{ width: '100%' }">
          <el-option
            v-for="item in groupSelection"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="计量单位" prop="unit">
        <el-select v-model="form.unit" filterable :style="{ width: '100%' }">
          <el-option
            v-for="item in unitSelection"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="重量（克）" prop="weight">
        <el-input-number
          v-model="form.weight"
          :max="999999999"
          :min="0"
          :precision="2"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="物料类别" prop="category">
        <el-select
          v-model="form.category"
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in categorySelection"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="产品编码" prop="model">
        <el-input v-model="form.model" />
      </el-form-item>
      <el-form-item label="项目编码" prop="project">
        <el-cascader
          v-model="form.project"
          filterable
          :options="Searchoptions"
          :props="{ label: 'projectCode', value: 'id', checkStrictly: true }"
          :style="{ width: '100%' }"
        />
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
    doEdit,
    getProjectCode,
    doAdd,
    GetDicMaterialG,
    GetDicMaterialU,
    GetDicMaterialC,
  } from '@/api/materialMaterialInfo'

  export default defineComponent({
    name: 'MaterialMaterialInfoEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        Searchoptions: [],
        groupSelection: [],
        unitSelection: [],
        categorySelection: [],
        form: {
          roles: [],
        },
        rules: {
          materialCode: [
            { required: true, trigger: 'blur', message: '请输入物料编码' },
          ],
          materialName: [
            { required: true, trigger: 'blur', message: '请输入物料名称' },
          ],
          model: [{ required: true, trigger: 'blur', message: '请选择产品' }],
          project: [{ required: true, trigger: 'blur', message: '请选择项目' }],
          group: [
            { required: true, trigger: 'blur', message: '请选择物料分组' },
          ],
          unit: [{ required: true, trigger: 'blur', message: '请选择单位' }],
          category: [
            { required: true, trigger: 'blur', message: '请选择类别' },
          ],
        },
        title: '',
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

      //物料分组
      const GetMaterialGData = async () => {
        const ReturnData = await GetDicMaterialG()
        state.groupSelection = ReturnData.data
      }
      //物料单位
      const GetMaterialUData = async () => {
        const ReturnData = await GetDicMaterialU()
        state.unitSelection = ReturnData.data
      }
      //物料类别
      const GetMaterialCData = async () => {
        const ReturnData = await GetDicMaterialC()
        state.categorySelection = ReturnData.data
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
        } else {
          state.title = '编辑'
          state.form = Object.assign({}, row)
          state.form.group = state.form.group == 0 ? '' : state.form.group
          state.form.unit = state.form.unit == 0 ? '' : state.form.unit
          state.form.category = state.form.group == 0 ? '' : state.form.category
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
            if (state.form.project.length != undefined) {
              state.form.project =
                state.form.project[state.form.project.length - 1]
            }
            state.form.group = state.form.group == '' ? 0 : state.form.group
            state.form.unit = state.form.unit == '' ? 0 : state.form.unit
            state.form.category =
              state.form.group == '' ? 0 : state.form.category
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {
        fetchProCodeData()
        GetMaterialGData()
        GetMaterialUData()
        GetMaterialCData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchProCodeData,
        GetMaterialGData,
        GetMaterialUData,
        GetMaterialCData,
      }
    },
  })
</script>
