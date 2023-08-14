<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="项目名称" prop="projectID">
        <rw-project
          v-model="form.projectID"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="交付编号：" prop="deliveryID">
        <el-select
          v-model="form.deliveryID"
          class="m-2"
          placeholder="请选择"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in DeliveryOptions"
            :key="item.id"
            :label="item.deliveryCode"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="设备编号：" prop="deviceID">
        <el-select
          v-model="form.deviceID"
          class="m-2"
          placeholder="请选择"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in DeviceOptions"
            :key="item.id"
            :label="item.deviceNo"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="文件类型：" prop="deliveryType">
        <el-select
          v-model="form.deliveryType"
          class="m-2"
          placeholder="请选择"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in DeliveryTypeOptions"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="数量：" prop="qty">
        <el-input-number
          v-model="form.qty"
          :max="999999999"
          :min="0"
          :step="1"
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
    doAdd,
    doEdit,
    getProjectCode,
    getProDeliveryCode,
    getProDeviceCode,
    getDeliFileTypeCode,
  } from '@/api/projectDelivefile'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'ProjectDelivefileEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          deliveryID: [
            { required: true, trigger: 'blur', message: '请选择交付编号' },
          ],
          projectID: [
            { required: true, trigger: 'blur', message: '请选择项目编号' },
          ],
          deviceID: [
            { required: true, trigger: 'blur', message: '请选择设备编号' },
          ],
          deliveryType: [
            { required: true, trigger: 'blur', message: '请选择文件类型' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        Searchoptions: [],
        DeliveryOptions: [],
        DeviceOptions: [],
        DeliveryTypeOptions: [],
      })
      //项目编号下拉框
      const ProfetchData = async () => {
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
      //项目交付下拉框
      const DeliveryfetchData = async () => {
        const projectDelivery = await getProDeliveryCode({ Id: 0 })
        state.DeliveryOptions = projectDelivery.data
      }
      //设备信息下拉框
      const DevicefetchData = async () => {
        const projectContacts = await getProDeviceCode({ Id: 0 })
        state.DeviceOptions = projectContacts.data
        state.DeviceOptions.forEach((item) => {
          item.deviceNo = `${item.deviceNo} ${item.deviceName}`
        })
      }
      //项目下拉框change事件
      const ProSelectChange = async (proId) => {
        if (proId == null) {
          const projectContacts = await getProDeviceCode({
            Id: 0,
          })
          state.DeviceOptions = projectContacts.data
          state.DeviceOptions.forEach((item) => {
            item.deviceNo = `${item.deviceNo} ${item.deviceName}`
          })

          const projectDelivery = await getProDeliveryCode({ Id: 0 })
          state.DeliveryOptions = projectDelivery.data
        } else {
          const projectContacts = await getProDeviceCode({
            Id: proId[proId.length - 1],
          })
          state.DeviceOptions = projectContacts.data
          state.DeviceOptions.forEach((item) => {
            item.deviceNo = `${item.deviceNo} ${item.deviceName}`
          })

          const projectDelivery = await getProDeliveryCode({
            Id: proId[proId.length - 1],
          })
          state.DeliveryOptions = projectDelivery.data
        }
      }
      //项目交付文件类型下拉框
      const DeliveryTypefetchData = async () => {
        const projectContacts = await getDeliFileTypeCode()
        state.DeliveryTypeOptions = projectContacts.data
      }

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
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.projectID.length != undefined) {
              state.form.projectID =
                state.form.projectID[state.form.projectID.length - 1]
            }
            console.log(state.form)
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            close()
            emit('fetch-data')
          }
        })
      }
      onMounted(() => {
        ProfetchData()
        DeliveryfetchData()
        DevicefetchData()
        DeliveryTypefetchData()
      })
      return {
        ...toRefs(state),
        showEdit,
        ProfetchData,
        DeliveryfetchData,
        DevicefetchData,
        DeliveryTypefetchData,
        close,
        save,
        ProSelectChange,
      }
    },
  })
</script>
