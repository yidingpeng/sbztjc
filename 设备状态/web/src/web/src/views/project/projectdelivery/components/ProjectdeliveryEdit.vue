<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="110px" :model="form" :rules="rules">
      <el-form-item label="交付单据号" prop="deliveryCode">
        <el-input
          v-model.trim="form.deliveryCode"
          placeholder="请输入交付单据号"
        />
      </el-form-item>
      <el-form-item label="项目名称" prop="projectID">
        <rw-project
          v-model="form.projectID"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="计划交付日期" prop="jhjhDate">
        <el-date-picker
          v-model="form.jhjhDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="实际交付日期" prop="sjjhDate">
        <el-date-picker
          v-model="form.sjjhDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="计划验收日期" prop="jhysDate">
        <el-date-picker
          v-model="form.jhysDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="实际验收日期" prop="sjysDate">
        <el-date-picker
          v-model="form.sjysDate"
          :disabled-date="disabledDate"
          placeholder="选择日期"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="验收阶段" prop="acceptancePhase">
        <el-select
          v-model="form.acceptancePhase"
          placeholder="请选择验收阶段"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="(item, index) in acceptancePhaseOptions"
            :key="index"
            :disabled="item.disabled"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="是否终验收" prop="isZYS">
        <el-select
          v-model="form.isZYS"
          placeholder="请选择是否终验收"
          :style="{ width: '100%' }"
        >
          <el-option
            :disabled="
              form.acceptancePhase == 197 || form.acceptancePhase == 16
            "
            label="是"
            :value="1"
          />
          <el-option label="否" :value="2" />
        </el-select>
      </el-form-item>
      <el-form-item label="验收凭证" prop="acceptanceCertificate">
        <el-upload
          ref="upload"
          :action="fileUrl"
          :auto-upload="false"
          :before-upload="beforeUpload"
          class="upload-demo"
          :headers="headers"
          :limit="1"
          name="UploadFile"
          :on-change="changeUpload"
          :on-exceed="handleExceed"
          :on-remove="removeUpload"
          :on-success="successUpload"
          :style="{ width: '100%' }"
        >
          <template #trigger>
            <el-button type="primary">选择文件</el-button>
          </template>
          &nbsp;&nbsp;
          <el-button class="ml-3" type="success" @click="submitUpload">
            上传文件
          </el-button>
          <template #tip>
            <div class="el-upload__tip" style="color: red">
              限制上传一个pdf文件，且文件不能超过20MB&nbsp;
              <span
                v-if="
                  form.acceptanceCertificate != 0 &&
                  form.acceptanceCertificate != null
                "
              >
                （已有文件）
              </span>
            </div>
          </template>
        </el-upload>
      </el-form-item>
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
  import { defineComponent, reactive, toRefs, ref } from 'vue'
  import {
    doEdit,
    getProjectCode,
    doAdd,
    GetListById,
    AcceptancePhase,
  } from '@/api/projectdelivery'
  import { getUrl } from '@/api/system/uploadFile'
  import { useUserStore } from '@/store/modules/user'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'ProjectdeliveryEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const fetchDataA = async () => {
        const SearchoptionsData = await AcceptancePhase()
        console.log(SearchoptionsData.data)
        state.acceptancePhaseOptions = SearchoptionsData.data
      }
      const upload = ref()
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        fileUrl: '',
        projectCodeOptions: [],
        formRef: null,
        form: {
          // deliveryCode: '',
          // jhjhDate: '',
          // sjjhDate: '',
          // jhysDate: '',
          // sjysDate: '',
          // acceptancePhase: '',
          // isZYS: 1,
          // remark: '',
        },
        acceptancePhaseOptions: [],
        rules: {
          deliveryCode: [
            {
              required: true,
              message: '请输入交付单据号',
              trigger: 'blur',
            },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          projectID: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择项目名称',
            },
          ],
          jhjhDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择计划交付日期',
            },
          ],
          sjjhDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择实际交付日期',
            },
          ],
          jhysDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择计划验收日期',
            },
          ],
          sjysDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择实际验收日期',
            },
          ],
          acceptancePhase: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择验收阶段',
            },
          ],
          isZYS: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否终验收',
            },
          ],
          remark: [],
        },
        title: '',
        dialogFormVisible: false,
        Searchoptions: [],
        headers: { authorization: '' },
        disabledDate(time) {
          return time.getTime() <= new Date(state.form.projectReceiveDate)
        },
      })
      const ProSelectChange = async (Id) => {
        if (Id == null) {
          state.form.projectReceiveDate = '0001-01-01'
        } else {
          const getProData = await GetListById({ Id: Id[Id.length - 1] })
          state.form.projectReceiveDate = getProData.data[0].projectReceiveDate
        }
      }
      const removeUpload = () => {
        state.form.acceptanceCertificate = null
      }
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

      const beforeUpload = (file) => {
        if (file.type != 'application/pdf') {
          ElMessage.error('上传文件格式有误!')
          return false
        }
        const fileSizeLimit = file.size / 1024 / 1024
        if (fileSizeLimit > 20) {
          ElMessage.error('文件不能超过20MB!')
          return false
        }
      }
      const fetchUrlData = async () => {
        state.fileUrl = `${await getUrl()}/File/InsertFile`
      }

      const successUpload = (response) => {
        state.form.acceptanceCertificate = response.id
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
        upload.value.clearFiles()
        state.form = {
          roles: [],
          projectID: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          console.log(state.form.projectID)
          if (valid) {
            if (state.form.projectID.length != undefined) {
              state.form.projectID =
                state.form.projectID[state.form.projectID.length - 1]
            }
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      const changeUpload = (file) => {
        if (file) {
          if (file.name.length > 25) {
            const skin = file.name.split('.')[file.name.split('.').length - 1]
            file.name = `${file.name.substr(0, 20)}....${skin}`
          }
        }
      }
      const handleExceed = (files) => {
        upload.value.clearFiles()
        upload.value.handleStart(files[0])
        state.form.acceptanceCertificate = null
      }
      const submitUpload = () => {
        upload.value.submit()
      }
      onMounted(() => {
        fetchDataA()
        fetchUrlData()
        fetchProCodeData()
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        upload,
        handleExceed,
        submitUpload,
        successUpload,
        removeUpload,
        fetchDataA,
        fetchUrlData,
        beforeUpload,
        changeUpload,
        fetchProCodeData,
        ProSelectChange,
      }
    },
  })
</script>
