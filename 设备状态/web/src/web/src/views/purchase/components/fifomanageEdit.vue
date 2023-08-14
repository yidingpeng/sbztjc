<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="二维码号" prop="qrCode">
        <el-input v-model="form.qrCode" disabled />
        <!-- <el-select
          v-model="form.qrCode"
          filterable
          placeholder="请选择二维码"
          :style="{ width: '100%' }"
          @change="QrCodeSelectChange"
        >
          <el-option
            v-for="item in qrCodeOptions"
            :key="item.value"
            :label="item.label"
            :value="item.label"
          />
        </el-select> -->
      </el-form-item>
      <el-form-item label="出入库类型" prop="type">
        <el-radio-group v-model="form.type" :disabled="true">
          <el-radio :label="1">入库</el-radio>
          <el-radio :label="2">出库</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="BOM编码" prop="bomCode">
        <el-input v-model="form.bomCode" :disabled="true" />
        <!-- <el-select
          v-model="form.bomCode"
          filterable
          placeholder="请选择BOM编码"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in bomCodeOptions"
            :key="item.code"
            :label="item.name"
            :value="item.code"
          />
        </el-select> -->
      </el-form-item>
      <el-form-item label="物料编码" prop="materialCode">
        <el-input v-model="form.materialCode" :disabled="true" />
        <!-- <el-select
          v-model="form.materialCode"
          filterable
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
        <el-input v-model="form.projectCode" :disabled="true" />
        <!-- <rw-project
          v-model="form.projectCode"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
        /> -->
      </el-form-item>
      <el-form-item label="出入库数量" prop="count">
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
      <!-- <el-form-item label="出入库人员" prop="fifoPersonnel">
        <el-input v-model="form.fifoPersonnel" />
        <rw-user
          v-model="form.fifoPersonnel"
          filterable
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="出入库时间" prop="fifoDateTime">
        <el-input v-model="form.fifoDateTime" />
        <el-date-picker
          v-model="form.fifoDateTime"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item> -->
      <el-form-item label="备注" prop="remark">
        <el-input
          v-model="form.remark"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          placeholder="请输入备注"
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
    FifoAdd,
    FifoEdit,
    Materialdropdown,
    BOMdropdown,
    GetQrAllList,
    GetByIdQrCode,
  } from '@/api/purchase/purchase'
  // import RwProject from '@/plugins/RwProject'
  // import RwUser from '@/plugins/RwUser'

  export default defineComponent({
    name: 'FifomanageEdit',
    //components: { RwProject, RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          type: 1,
        },
        rules: {
          qrCode: [
            { required: true, trigger: 'blur', message: '请选择二维码' },
          ],
          type: [
            { required: true, trigger: 'blur', message: '请选择出入库类型' },
          ],
          materialCode: [
            { required: true, trigger: 'blur', message: '请选择物料编码' },
          ],
          // projectCode: [
          //   { required: true, trigger: 'blur', message: '请选择项目编码' },
          // ],
          // bomCode: [
          //   { required: true, trigger: 'blur', message: '请选择BOM编码' },
          // ],
          count: [{ required: true, trigger: 'blur', message: '请输入数量' }],
          fifoPersonnel: [
            { required: true, trigger: 'blur', message: '请输入操作人员' },
          ],
          fifoDateTime: [
            { required: true, trigger: 'blur', message: '请选择出入库时间' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        materialCodeOptions: null,
        bomCodeOptions: null,
        qrCodeOptions: null,
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

      //二维码
      const GetqrCode = async () => {
        const resultList = await GetQrAllList()
        state.qrCodeOptions = resultList.data
      }

      //二维码下拉框选择自动带出其他字段
      const QrCodeSelectChange = async (qrcode) => {
        const result = await GetByIdQrCode({ QrCode: qrcode })
        if (result.data.type == 0) {
          $baseMessage('该二维码编码所属物料已存在出库信息', 'warning')
        }
        state.form.type = result.data.type != 1 ? 2 : 1
        state.form.bomCode = result.data.bomCode
        state.form.bomName = result.data.bomName
        state.form.materialCode = result.data.materialCode
        state.form.materialName = result.data.materialName
        state.form.projectCode = result.data.projectCode
        state.form.projectName = result.data.projectName
        state.form.count = result.data.count
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = state.form.id
              ? await FifoEdit(state.form)
              : await FifoAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {
        GetbomCode()
        GetmaterialCode()
        GetqrCode()
      })

      return {
        ...toRefs(state),
        showEdit,
        GetmaterialCode,
        GetbomCode,
        QrCodeSelectChange,
        close,
        save,
      }
    },
  })
</script>
