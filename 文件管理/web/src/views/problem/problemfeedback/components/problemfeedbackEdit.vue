<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="24">
          <el-form-item label="项目编号" prop="pt_ID">
            <rw-project v-model="form.pt_ID" :style="{ width: '100%' }" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="发生地址" prop="addressId">
            <el-select
              v-model="form.addressId"
              class="m-2"
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in addresssOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="问题来源" prop="source">
            <el-select
              v-model="form.source"
              class="m-2"
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in sourceOptions"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="问题描述" prop="problemDescription">
            <el-input
              v-model="form.problemDescription"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="附加图片" prop="picId">
            <el-upload
              ref="upload"
              :action="fileUrl"
              :before-upload="beforeUpload"
              :file-list="fileList"
              :headers="headers"
              list-type="picture-card"
              name="UploadFile"
              :on-preview="handlePictureCardPreview"
              :on-remove="handleRemove"
              :on-success="successUpload"
            >
              <vab-icon icon="add-line" />
            </el-upload>
            <div v-if="form.picId && form.picId != -1">
              &nbsp;
              <el-button
                class="ml-3"
                link
                type="primary"
                @click="DeleteFiles(form.id)"
              >
                删除原文件
              </el-button>
              <div style="color: red">
                <span>（已有文件）</span>
              </div>
            </div>
            <div :style="{ width: '400px' }">
              <span style="color: red">
                请上传png/jpg文件，且单个文件不能超过20MB
              </span>
            </div>
            <el-dialog v-model="dialogVisible">
              <img alt="Preview Image" :src="dialogImageUrl" w-full />
            </el-dialog>
          </el-form-item>
        </el-col>
      </el-row>
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
    doAdd,
    getProblemType,
    getInsertFileUrl,
    GetFilesByPid,
    DeleteFilesByPid,
  } from '@/api/problemProblemfeedback'
  import { ref } from 'vue'
  import RwProject from '@/plugins/RwProject'
  import { useUserStore } from '@/store/modules/user'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'ProblemfeedbackEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const upload = ref()
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const handleRemove = (file, files) => {
        state.form.imgFilesId = files.map((item) => item.response.id).join()
      }
      const handlePictureCardPreview = (file) => {
        state.dialogImageUrl = file.url
        state.dialogVisible = true
      }
      const beforeUpload = (file) => {
        if (file.type != 'image/png' && file.type != 'image/jpeg') {
          ElMessage.error('上传文件格式有误!请上传（png或者jpg格式图片）')
          console.log(state.form)
          return false
        }
        const fileSizeLimit = file.size / 1024 / 1024
        if (fileSizeLimit > 5) {
          ElMessage.error('文件不能超过5MB!')
          return false
        }
      }
      const successUpload = (response) => {
        if (state.form.imgFilesId != '') {
          state.form.imgFilesId = `${state.form.imgFilesId + response.id},`
        } else {
          state.form.imgFilesId = `${response.id},`
        }
      }
      const DeleteFiles = async (pid) => {
        $baseConfirm('你确定要删除当前项吗', null, async () => {
          const { msg } = await DeleteFilesByPid({
            Pid: pid,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          state.form.picId = -1
        })
      }

      const uploadHeaders = () => {
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      }

      const state = reactive({
        headers: { authorization: '' },
        formRef: null,
        form: {
          roles: [],
          imgFilesId: '',
        },
        rules: {
          pt_ID: [
            { required: true, trigger: 'blur', message: '请选择项目编号' },
          ],
          addressId: [
            { required: true, trigger: 'blur', message: '请选择发生地址' },
          ],
          source: [
            { required: true, trigger: 'blur', message: '请选择问题来源' },
          ],
          problemDescription: [
            { required: true, trigger: 'blur', message: '请输入问题描述' },
          ],
        },
        title: '',
        fileUrl: '',
        dialogFormVisible: false,
        dialogVisible: false,
        dialogImageUrl: '',
        editFiles: [],
        options: [],
        addresssOptions: [
          {
            label: '厂内',
            value: '1',
          },
          {
            label: '厂外',
            value: '2',
          },
        ],
        sourceOptions: [],
        reasonTypeOptions: [],
        disabledDate(time) {
          return time.getTime() <= new Date(state.form.feedbackTime)
        },
        disabledDate1(time) {
          return time.getTime() < Date.now()
        },
        fileList: [],
      })
      const fetchDataM = async () => {
        const SearchoptionsData = await getProblemType()
        state.options = SearchoptionsData.data
        const dictStore = useDictionaryStore()
        state.reasonTypeOptions = dictStore.getByType('ReasonType')
        state.sourceOptions = dictStore.getByType('ProblemSource')
        state.fileUrl = await getInsertFileUrl()
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.form.imgFilesId = ''
        } else {
          state.title = '编辑'
          row.imgFilesId = ''
          if (row.picId) {
            //调用获取文件信息方法
            GetProFilesByPid({ Id: row.id })
          }
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }

      const GetProFilesByPid = async (id) => {
        state.editFiles = await GetFilesByPid(id)
        state.editFiles.data.forEach((item) => {
          console.log(item.rootPath)
          state.fileList.push({ name: item.fileName, url: item.rootPath })
        })
      }

      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
        state.fileList = []
        upload.value.clearFiles()
        emit('fetch-data')
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            //console.log(state.form)
            if (state.form.pt_ID.length != undefined) {
              state.form.pt_ID = state.form.pt_ID[state.form.pt_ID.length - 1]
            }
            state.form.problemTypeID =
              state.form.problemTypeID == '' || state.form.problemTypeID == null
                ? 0
                : state.form.problemTypeID
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            close()
          }
        })
      }
      onMounted(() => {
        fetchDataM()
        uploadHeaders()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        fetchDataM,
        handleRemove,
        handlePictureCardPreview,
        beforeUpload,
        successUpload,
        GetProFilesByPid,
        DeleteFiles,
        uploadHeaders,
        upload,
      }
    },
  })
</script>
