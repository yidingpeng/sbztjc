<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="140px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="12">
          <el-form-item label="工具编码" prop="toolCode">
            <el-input v-model="form.toolCode" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="工具名称" prop="toolName">
            <el-input v-model="form.toolName" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="工具类别" prop="toolClassID">
            <el-select
              v-model="form.toolClassID"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ToolClassRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="工具类型" prop="toolTypeID">
            <el-select
              v-model="form.toolTypeID"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ToolTypeRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="规格型号" prop="toolModel">
            <el-input v-model="form.toolModel" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="品牌" prop="toolBrand">
            <el-input v-model="form.toolBrand" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="证书编号" prop="toolCertificate">
            <el-input v-model="form.toolCertificate" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="购买日期" prop="purchaseTime">
            <el-date-picker
              v-model="form.purchaseTime"
              placeholder="购买日期"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="12">
          <el-form-item label="生效时间" prop="effectTime">
            <!-- <el-date-picker
              v-model="queryForm.feedbackTimeS"
              end-placeholder="生效时间"
              range-separator="~"
              start-placeholder="失效时间"
              type="daterange"
            /> -->
            <el-date-picker
              v-model="form.effectTime"
              placeholder="生效时间"
              style="width: 100%"
              type="date"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="失效时间" prop="failuretime">
            <el-date-picker
              v-model="form.failuretime"
              placeholder="失效时间"
              style="width: 100%"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="12">
          <el-form-item label="是否要录工具编码" prop="toolIsHasCode">
            <el-radio-group v-model="form.toolIsHasCode">
              <el-radio border :label="0" size="large">是</el-radio>
              <el-radio border :label="1" size="large">否</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="状态" prop="usingFlag">
            <el-select
              v-model="form.usingFlag"
              allow-create13
              placeholder="选择状态"
              :style="{ width: '100%' }"
            >
              <el-option key="0" label="启用" :value="0" />
              <el-option key="1" label="禁用" :value="1" />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="工具图片" prop="picId">
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
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
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
    GetToolClassList,
    GetToolTypeList,
    getInsertFileUrl,
    GetFilesByPid,
    DeleteFilesByPid,
    GetRepeat,
  } from '@/api/basics/tool'
  import { ref } from 'vue'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'BasicsToolEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const upload = ref()
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const uploadHeaders = () => {
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      }

      const state = reactive({
        headers: { authorization: '' },
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
          imgFilesId: '',
        },
        ToolClassRadio: [],
        ToolTypeRadio: [],
        fileList: [],
        dialogVisible: false,
        dialogImageUrl: '',
        editFiles: [],
        fileUrl: '',
        rules: {
          toolCode: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同工具编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入工具编码'))
                }
              },
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          toolName: [
            { required: true, trigger: 'blur', message: '请输入工具名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          toolBrand: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          toolCertificate: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          toolModel: [
            {
              validator: (rule, value, callback) => {
                if (value) {
                  if (!Number.isInteger(parseFloat(value))) {
                    callback(new Error('请输入数字'))
                  }
                  callback()
                } else {
                  state.form.toolModel = null
                  callback()
                }
              },
              trigger: 'blur',
            },
          ],
          toolClassID: [
            { required: true, trigger: 'blur', message: '请选择工具类别' },
          ],
          toolTypeID: [
            { required: true, trigger: 'blur', message: '请选择工具类型' },
          ],
          purchaseTime: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择购买日期',
            },
          ],
          effectTime: [
            { required: true, trigger: 'blur', message: '请选择生效时间' },
          ],
          failuretime: [
            { required: true, trigger: 'blur', message: '请选择失效时间' },
          ],
          toolIsHasCode: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否要录工具编码',
            },
          ],
          usingFlag: [
            { required: true, trigger: 'blur', message: '请选择状态' },
          ],
          remark: [
            { trigger: 'blur', message: '长度不能超过255个字符' },
            {
              max: 255,
              trigger: 'blur',
              message: '长度不能超过255个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })

      const fetchDataM = async () => {
        GetToolClassData()
        GetToolTypeData()
        state.fileUrl = await getInsertFileUrl()
      }

      //工具类别
      const GetToolClassData = async () => {
        const ToolClass = await GetToolClassList()
        state.ToolClassRadio = ToolClass.data
      }

      //工具类型
      const GetToolTypeData = async () => {
        const ToolType = await GetToolTypeList()
        state.ToolTypeRadio = ToolType.data
      }

      const GetProFilesByPid = async (id) => {
        state.editFiles = await GetFilesByPid(id)
        state.editFiles.data.forEach((item) => {
          state.fileList.push({
            name: item.fileName,
            url: item.rootPath,
          })
        })
      }

      const handleRemove = (file, files) => {
        state.form.imgFilesId = files.map((item) => item.response.id).join()
      }

      const handlePictureCardPreview = (file) => {
        state.dialogImageUrl = file.url
        state.dialogVisible = true
      }

      const successUpload = (response) => {
        if (state.form.imgFilesId != '') {
          state.form.imgFilesId = `${state.form.imgFilesId + response.id},`
        } else {
          state.form.imgFilesId = `${response.id},`
        }
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

      const DeleteFiles = async (pid) => {
        $baseConfirm('你确定要删除当前项吗', null, async () => {
          const { msg } = await DeleteFilesByPid({
            Pid: pid,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          state.form.picId = -1
          state.fileList = []
        })
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
          row.imgFilesId = ''
          if (row.picId) {
            //调用获取文件信息方法
            GetProFilesByPid({ Id: row.id })
          }
          row.toolModel == 0 ? (row.toolModel = null) : ''
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
        state.dialogFormVisible = false
        state.fileList = []
        upload.value.clearFiles()
        emit('fetch-data')
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.pt_ID != undefined) {
              state.form.pt_ID = state.form.pt_ID[state.form.pt_ID.length - 1]
            }
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
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
        GetToolClassData,
        GetToolTypeData,
        uploadHeaders,
        beforeUpload,
        DeleteFiles,
        handleRemove,
        handlePictureCardPreview,
        successUpload,
        GetProFilesByPid,
        upload,
      }
    },
  })
</script>
