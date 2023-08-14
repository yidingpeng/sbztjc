<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="530px"
    @close="close"
  >
    <el-form>
      <el-form-item label="项目计划文件">
        <el-upload
          ref="pdfUpload"
          accept=".xlsx,.xls"
          :action="fileUrl"
          :auto-upload="false"
          :before-upload="BeforeUpload"
          class="upload-demo"
          :data="{ Filepid: pid }"
          :headers="headers"
          :limit="1"
          name="UploadFile"
          :on-change="changeUpload"
          :on-exceed="HandleExceed"
          :on-remove="RemoveUpload"
          :on-success="SuccessUpload"
          :style="{ width: '100%' }"
        >
          <template #trigger>
            <el-button type="primary">选择文件</el-button>
            &nbsp;&nbsp;
          </template>
          <el-button class="ml-3" type="success" @click="SubmitUpload">
            上传文件
          </el-button>
          <template #tip>
            <div class="el-upload__tip" style="color: red">
              请上传一个Excel文件，且该文件不能超过20MB
            </div>
          </template>
        </el-upload>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import { GetInsertFilePath } from '@/api/project'
  import { ref } from 'vue'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'ProjectFilesUpload',
    components: {},
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const pdfUpload = ref()

      const state = reactive({
        pid: 0,
        fileUrl: '',
        dialogFormVisible: false,
        headers: { authorization: '' },
        title: '',
      })
      const showEdit = (row) => {
        state.pid = row.id
        state.title = '添加项目计划'
        state.dialogFormVisible = true
      }
      const fetchUrlData = async () => {
        state.fileUrl = await GetInsertFilePath()
      }
      const BeforeUpload = (file) => {
        if (
          file.type !=
            'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' &&
          file.type != 'application/vnd.ms-excel'
        ) {
          ElMessage.error('上传文件格式有误!')
          return false
        }
        const fileSizeLimit = file.size / 1024 / 1024
        if (fileSizeLimit > 20) {
          ElMessage.error('文件不能超过20MB!')
          return false
        }
      }
      const HandleExceed = (files) => {
        pdfUpload.value.clearFiles()
        pdfUpload.value.handleStart(files[0])
      }
      const RemoveUpload = () => {}

      const SubmitUpload = () => {
        pdfUpload.value.submit()
      }
      const SuccessUpload = () => {}
      const changeUpload = (file) => {
        if (file) {
          if (file.name.length > 25) {
            const skin = file.name.split('.')[file.name.split('.').length - 1]
            file.name = `${file.name.substr(0, 20)}....${skin}`
          }
        }
      }

      const close = () => {
        state.dialogFormVisible = false
        emit('fetch-data')
        pdfUpload.value.clearFiles()
      }

      onMounted(() => {
        fetchUrlData()
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      })
      return {
        ...toRefs(state),
        close,
        SubmitUpload,
        SuccessUpload,
        RemoveUpload,
        BeforeUpload,
        HandleExceed,
        showEdit,
        fetchUrlData,
        changeUpload,
        pdfUpload,
      }
    },
  })
</script>
