<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="二维码" prop="qrCode">
        <el-input v-model="form.qrCode" />
      </el-form-item>
      <el-form-item label="供应商" prop="supplier">
        <el-input v-model="form.supplier" />
      </el-form-item>
      <el-form-item label="物料编码" prop="materialCode">
        <el-input v-model="form.materialCode" />
        <!-- <el-select
          v-model="form.materialCode"
          placeholder="请选择物料编码"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in materialCodeOptions"
            :key="item.code"
            :label="item.name"
            :value="item.code"
          />
        </el-select> -->
      </el-form-item>
      <el-form-item label="项目编码" prop="projectCode">
        <el-input v-model="form.projectCode" />
        <!-- <rw-project
          v-model="form.ProjectCode"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        /> -->
      </el-form-item>
      <el-form-item label="BOM编码" prop="bomCode">
        <!-- <el-input v-model="form.bomCode" /> -->
        <el-select
          v-model="form.bomCode"
          placeholder="请选择BOM编码"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in bomCodeOptions"
            :key="item.code"
            :label="item.name"
            :value="item.code"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="数量" prop="count">
        <!-- <el-input v-model="form.count" /> -->
        <el-input-number
          v-model="form.count"
          :max="999999999"
          :min="1"
          :precision="0"
          :step="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="状态" prop="state">
        <el-input v-model="form.state" />
      </el-form-item>
      <el-form-item label="是否合格" prop="qualified">
        <!-- <el-input v-model="form.qualified" /> -->
        <el-radio-group v-model="form.qualified">
          <el-radio :label="0" select>待质检</el-radio>
          <el-radio :label="1">是</el-radio>
          <el-radio :label="2">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" />
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
    QrCodeAdd,
    Materialdropdown,
    BOMdropdown,
  } from '@/api/purchase/purchase'

  export default defineComponent({
    name: 'QrCodeEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          qualified: 0,
        },
        rules: {
          qrCode: [
            { required: true, trigger: 'blur', message: '请输入二维码' },
          ],
          supplier: [
            { required: true, trigger: 'blur', message: '请选择供应商' },
          ],
          materialCode: [
            { required: true, trigger: 'blur', message: '请选择物料编码' },
          ],
          ProjectCode: [
            { required: true, trigger: 'blur', message: '请选择项目编码' },
          ],
          bomCode: [
            { required: true, trigger: 'blur', message: '请选择BOM编码' },
          ],
          count: [{ required: true, trigger: 'blur', message: '请输入数量' }],
          state: [{ required: true, trigger: 'blur', message: '请选择状态' }],
        },
        title: '',
        dialogFormVisible: false,
        materialCodeOptions: null,
        bomCodeOptions: null,
        mcode: '',
        projectCode: '',
        bomCode: '',
      })

      const showEdit = (row, mcode, projectCode, bomCode) => {
        state.title = '添加'
        state.form = Object.assign({}, row)
        state.form.qrCode = mcode
        state.form.projectCode = projectCode
        state.form.bomCode = bomCode
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }

      //物料编码
      const GetmaterialCode = async () => {
        const resultList = await Materialdropdown()
        state.materialCodeOptions = resultList.data
      }
      //BOM编码
      const GetbomCode = async () => {
        const resultList = await BOMdropdown()
        state.bomCodeOptions = resultList.data
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            //保存二维码数据
            let qrData = {}
            qrData = {
              qrCode: state.form.qrCode,
              supplier: state.form.supplier,
              materialCode: state.form.materialCode,
              projectCode: state.form.projectCode,
              bomCode: state.form.bomCode,
              count: state.form.count,
              state: 1,
              remark: '',
              qualified: 0,
              bomId: state.form.bomId,
            }
            const { msg } = await QrCodeAdd(qrData)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {
        GetbomCode()
        //GetmaterialCode()
      })

      return {
        ...toRefs(state),
        showEdit,
        GetmaterialCode,
        GetbomCode,
        close,
        save,
      }
    },
  })
</script>
