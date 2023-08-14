<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="100px" :model="form" :rules="rules">
      <el-form-item label="父级任务" prop="parentId">
        <el-cascader
          clearable
          filterable
          :model-value="form.parentId"
          :options="TreeListOptions"
          placeholder="无父级任务无需选择"
          :props="propParent"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="任务编码" prop="taskCode">
        <el-input v-model="form.taskCode" maxlength="20" show-word-limit />
      </el-form-item>
      <el-form-item label="任务名称" prop="taskName">
        <el-input v-model="form.taskName" maxlength="20" show-word-limit />
      </el-form-item>
      <el-form-item label="项目类型" prop="projectClassID">
        <el-select
          v-model="form.projectClassID"
          filterable
          placeholder="请选择项目类型"
        >
          <el-option
            v-for="(item, index) in ProjectClassOption"
            :key="index"
            :disabled="item.disabled"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="是否里程碑" prop="isMilestone">
        <el-select v-model="form.isMilestone">
          <el-option label="是" value="是" />
          <el-option label="否" value="否" />
        </el-select>
      </el-form-item>
      <el-form-item label="是否关键任务" prop="isKey">
        <el-select v-model="form.isKey">
          <el-option label="是" value="是" />
          <el-option label="否" value="否" />
        </el-select>
      </el-form-item>
      <el-form-item label="是否需审批" prop="isReview">
        <el-select v-model="form.isReview">
          <el-option label="是" value="是" />
          <el-option label="否" value="否" />
        </el-select>
      </el-form-item>
      <el-form-item label="前置条件" prop="inputCondition">
        <el-upload
          ref="pdfUpload"
          v-model="form.inputCondition"
          :action="fileUrl"
          :auto-upload="false"
          :before-upload="pdfBeforeUpload"
          class="upload-demo"
          :headers="headers"
          :limit="1"
          name="UploadFile"
          :on-change="changeUpload"
          :on-exceed="pdfHandleExceed"
          :on-remove="pdfRemoveUpload"
          :on-success="pdfSuccessUpload"
          :style="{ width: '100%' }"
        >
          <template #trigger>
            <el-button type="primary">选择文件</el-button>
            &nbsp;&nbsp;
          </template>
          <el-button class="ml-3" type="success" @click="pdfSubmitUpload">
            上传文件
          </el-button>
          <template #tip>
            <div class="el-upload__tip" style="color: red">
              限制上传一个pdf文件，且单个文件不能超过20MB
              <span
                v-if="form.inputCondition != 0 && form.inputCondition != null"
              >
                （已有文件）
              </span>
            </div>
          </template>
        </el-upload>
      </el-form-item>
      <el-form-item label="输出文件" prop="outPutFile">
        <el-upload
          ref="wordUpload"
          v-model="form.outPutFile"
          :action="fileUrl"
          :auto-upload="false"
          :before-upload="wordBeforeUpload"
          class="upload-demo"
          :headers="headers"
          :limit="1"
          name="UploadFile"
          :on-change="changeUpload"
          :on-exceed="wordHandleExceed"
          :on-remove="wordRemoveUpload"
          :on-success="wordSuccessUpload"
          :style="{ width: '100%' }"
        >
          <template #trigger>
            <el-button type="primary">选择文件</el-button>
            &nbsp;&nbsp;
          </template>
          <el-button class="ml-3" type="success" @click="wordSubmitUpload">
            上传文件
          </el-button>
          <template #tip>
            <div class="el-upload__tip" style="color: red">
              限制上传一个word文件，且单个文件不能超过20MB
              <span v-if="form.outPutFile != 0 && form.outPutFile != null">
                （已有文件）
              </span>
            </div>
          </template>
        </el-upload>
      </el-form-item>
      <el-form-item label="工作内容" prop="workMemo">
        <el-input
          v-model="form.workMemo"
          :autosize="{ minRows: 4, maxRows: 4 }"
          :maxlength="500"
          show-word-limit
          :style="{ width: '100%' }"
          type="textarea"
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
  import { defineComponent, reactive, toRefs, ref } from 'vue'
  import { doAdd, doEdit, getParentList } from '@/api/task'
  import { GetProjectClass } from '@/api/project'
  import { getUrl } from '@/api/system/uploadFile'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'TaskEdit',
    emits: ['fetch-data', 'test'],
    setup(props, { emit }) {
      const pdfUpload = ref()
      const wordUpload = ref()
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        fileUrl: '',
        pdfFile: [],
        wordFile: [],
        form: {
          isMilestone: '否',
          isKey: '否',
          isReview: '否',
          roles: [],
          inputCondition: '',
          outPutFile: '',
          remark: '',
          workMemo: '',
          fileList: [],
        },
        rules: {
          taskCode: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入任务编码',
            },
          ],
          taskName: [
            {
              required: true,
              pattern: '[^ \x22]+',
              trigger: 'blur',
              message: '请输入任务名称',
            },
          ],
          projectClassID: [
            { required: true, trigger: 'blur', message: '请选择项目类型' },
          ],
        },
        title: '',
        dialogFormVisible: false,
        ProjectClassOption: [],
        TreeListOptions: [],
        propParent: {
          multiple: false,
          label: 'taskName',
          value: 'id',
          children: 'children',
          checkStrictly: true,
        },
        headers: { authorization: '' },
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
      //获取上传文件的地址
      const fetchUrlData = async () => {
        state.fileUrl = `${await getUrl()}/File/InsertFile`
        //console.log(state.fileUrl)
      }
      //上传文件之前的钩子，参数为上传的文件， 若返回false或者返回 Promise 且被 reject，则停止上传。
      const pdfBeforeUpload = (file) => {
        if (file.type != 'application/pdf') {
          ElMessage.error('上传文件格式有误!')
          return false
        }
        const fileSizeLimit = file.size / 1024 / 1024
        if (fileSizeLimit > 20) {
          ElMessage.error('文件不能超过20MB!')
          return false
        }
        //console.log(file)
        state.pdfFile.push(file)
      }
      //当超出限制时，执行的钩子函数
      const pdfHandleExceed = (files) => {
        pdfUpload.value.clearFiles()
        pdfUpload.value.handleStart(files[0])
      }
      //删除文件之前的钩子，参数为上传的文件和文件列表， 若返回 false 或者返回 Promise 且被 reject，则停止删除。
      const pdfRemoveUpload = () => {
        state.form.inputCondition = ''
      }
      //上传文件
      const pdfSubmitUpload = () => {
        // console.log(pdfUpload.value)
        pdfUpload.value.submit()
      }
      //文件上传成功时的钩子
      const pdfSuccessUpload = (response) => {
        state.form.inputCondition = `${response.id}`
      }
      //任务信息
      const GetParentListData = async () => {
        const TreeList = await getParentList()
        state.TreeListOptions = TreeList.data
        //console.log(TreeList.data)
      }
      const changeUpload = (file) => {
        if (file) {
          if (file.name.length > 25) {
            const skin = file.name.split('.')[file.name.split('.').length - 1]
            file.name = `${file.name.substr(0, 20)}....${skin}`
          }
        }
      }

      const wordBeforeUpload = (file) => {
        if (
          file.type !=
            'application/vnd.openxmlformats-officedocument.wordprocessingml.document' &&
          file.type != 'application/msword'
        ) {
          ElMessage.error('上传文件格式有误!')
          return false
        }
        const fileSizeLimit = file.size / 1024 / 1024
        if (fileSizeLimit > 20) {
          ElMessage.error('文件不能超过20MB!')
          return false
        }
        console.log(file)
        state.wordFile.push(file)
      }
      const wordHandleExceed = (files) => {
        wordUpload.value.clearFiles()
        wordUpload.value.handleStart(files[0])
      }
      const wordRemoveUpload = () => {
        state.form.outPutFile = ''
      }
      const wordSubmitUpload = () => {
        wordUpload.value.submit()
      }
      const wordSuccessUpload = (response) => {
        state.form.outPutFile = `${response.id}`
      }
      const close = () => {
        state['formRef'].resetFields()
        pdfUpload.value.clearFiles()
        wordUpload.value.clearFiles()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('test', { Id: state.form.parentId })
            emit('fetch-data')
            close()
          }
        })
      }
      //项目分类
      const GetProjectClassData = async () => {
        const ProjectClassList = await GetProjectClass()
        state.ProjectClassOption = ProjectClassList.data
      }
      const handleParent = (value) => {
        if (value) {
          if (value.length > 5) {
            ElMessage.error('父级任务限制最高层级选择到第五层!')
            state.form.parentId = 0
            return
          } else state.form.parentId = value ? value[value.length - 1] : 0
        }
      }
      onMounted(() => {
        GetProjectClassData()
        fetchUrlData()
        GetParentListData()
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        pdfUpload,
        wordUpload,
        pdfSubmitUpload,
        pdfSuccessUpload,
        pdfRemoveUpload,
        pdfBeforeUpload,
        pdfHandleExceed,
        wordSubmitUpload,
        wordSuccessUpload,
        wordRemoveUpload,
        wordBeforeUpload,
        wordHandleExceed,
        GetProjectClassData,
        getUrl,
        fetchUrlData,
        changeUpload,
        GetParentListData,
        handleParent,
      }
    },
  })
</script>
