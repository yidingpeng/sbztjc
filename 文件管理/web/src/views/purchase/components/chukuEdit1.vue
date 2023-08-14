<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    title="分量出库"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="领料人员" prop="fifoPersonnel">
        <el-select
          v-model="form.fifoPersonnel"
          clearable
          filterable
          placeholder="请选择领料人员"
          @change="handleChange"
        >
          <el-option
            v-for="item in list"
            :key="item.value"
            :label="item.label"
            :value="`${item.value},${item.label}`"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="出库数量" prop="count">
        <el-input-number v-model="form.count" :min="1" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { FenLiangCK } from '@/api/purchase/purchase'
  import { GetUserList } from '@/api/system/user'

  export default defineComponent({
    name: 'ChukuEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          deliverySignature: '',
          fifoPersonnel: '',
          count: 1,
          roles: [],
        },
        rules: {
          fifoPersonnel: [
            { required: true, trigger: 'change', message: '请选择领料人员' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        list: [],
        row: [],
      })

      const showEdit = (row) => {
        //console.log(row)
        state.row = row
        state.form.count = row.count
        state.form.deliverySignature = ''
        state.form.fifoPersonnel = ''
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
            if (state.row.kuCun - state.form.count >= 0) {
              const data = {
                type: 2,
                bomCode: state.row.bomCode,
                bomName: state.row.bomName,
                materialCode: state.row.materialCode,
                materialName: state.row.materialName,
                projectCode: state.row.projectCode,
                projectName: state.row.projectName,
                qrCode: state.row.qrCode,
                count: state.row.count,
                fifoDateTime: state.row.fifoDateTime,
                fifoPersonnel: state.form.fifoPersonnel,
                deliverySignature: state.form.deliverySignature,
                remark: '',
              }
              const { msg } = await FenLiangCK(data)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              emit('fetch-data')
              close()
            } else {
              $baseMessage('库存不足', 'error')
            }
          }
        })
      }
      const loadUserList = async () => {
        const list = await GetUserList()
        if (list.data.length > 0) {
          list.data.forEach((item) => {
            if (item.orgId > 3) {
              state.list.push({
                label: item.account,
                value: item.id,
              })
            }
          })
        }
      }
      const handleChange = (val) => {
        if (val.length > 0) {
          const [value, label] = val.split(',')
          state.form.fifoPersonnel = label
          state.form.deliverySignature = value
        }
      }
      onMounted(() => {
        loadUserList()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        loadUserList,
        handleChange,
      }
    },
  })
</script>
