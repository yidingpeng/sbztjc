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
          <el-form-item label="物料编码" prop="mtlCode">
            <el-input v-model="form.mtlCode" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="物料/零件名称" prop="mtlName">
            <el-input v-model="form.mtlName" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="规格型号" prop="mtlModel">
            <el-input v-model="form.mtlModel" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="图号" prop="wlFigureNo">
            <el-input v-model="form.wlFigureNo" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="物料分类" prop="mtlClassID">
            <el-select
              v-model="form.mtlClassID"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in MaterialClassRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="物料类型" prop="mtlTypeID">
            <el-select
              v-model="form.mtlTypeID"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in MaterialTypeRadio"
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
          <el-form-item label="基本单位" prop="baseUnitID">
            <el-select
              v-model="form.baseUnitID"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in MaterialUnitRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="重要度" prop="importance">
            <el-select
              v-model="form.importance"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in MeasuringimportanceRadio"
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
          <el-form-item label="材质" prop="texture">
            <el-select
              v-model="form.texture"
              clearable
              placeholder="请选择"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in MeasuringTextureRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
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
          <el-form-item label="重量" prop="weight">
            <el-input v-model="form.weight" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="尺寸" prop="size">
            <el-input v-model="form.size" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="是否要录物料编码" prop="mtlIsHasCode">
            <el-radio-group v-model="form.mtlIsHasCode">
              <el-radio border :label="0" size="large">是</el-radio>
              <el-radio border :label="1" size="large">否</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="状态" prop="mtlStatus">
            <el-select
              v-model="form.mtlStatus"
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
          <el-form-item label="物料图片" prop="picId">
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
    GetMaterialClassList,
    GetMaterialTypeList,
    GetMaterialUnitList,
    GetMeasuringimportanceList,
    GetMeasuringTextureList,
    getInsertFileUrl,
    GetFilesByPid,
    DeleteFilesByPid,
    GetRepeat,
  } from '@/api/basics/material'
  import { ref } from 'vue'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'BasicsMaterialEdit',
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
        MaterialClassRadio: [],
        MaterialTypeRadio: [],
        MaterialUnitRadio: [],
        MeasuringimportanceRadio: [],
        MeasuringTextureRadio: [],
        fileList: [],
        dialogVisible: false,
        dialogImageUrl: '',
        editFiles: [],
        fileUrl: '',
        rules: {
          mtlCode: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同物料编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入物料编码'))
                }
              },
            },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
          ],
          mtlName: [
            { required: true, trigger: 'blur', message: '请输入物料/零件名' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          mtlModel: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          wlFigureNo: [
            { required: true, trigger: 'blur', message: '请输入图号' },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          mtlClassID: [
            { required: true, trigger: 'blur', message: '请选择物料分类' },
          ],
          mtlTypeID: [
            { required: true, trigger: 'blur', message: '请选择物料类型' },
          ],
          baseUnitID: [
            { required: true, trigger: 'blur', message: '请选择基本单位' },
          ],
          importance: [
            { required: true, trigger: 'blur', message: '请选择重要度' },
          ],
          texture: [{ required: true, trigger: 'blur', message: '请选择材质' }],
          purchaseTime: [
            { required: true, trigger: 'blur', message: '请选择购买日期' },
          ],
          effectTime: [
            { required: true, trigger: 'blur', message: '请选择生效时间' },
          ],
          failuretime: [
            { required: true, trigger: 'blur', message: '请选择失效时间' },
          ],
          mtlIsHasCode: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否要录物料编码',
            },
          ],
          mtlStatus: [
            { required: true, trigger: 'blur', message: '请选择状态' },
          ],
          weight: [
            {
              message: '请输入数字',
              validator: (rule, value, callback) => {
                if (value && !Number.isInteger(parseFloat(value))) {
                  callback(new Error('请输入数字'))
                } else {
                  callback()
                }
              },
              trigger: 'blur',
            },
          ],
          size: [
            { trigger: 'blur', message: '长度不能超过150个字符' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
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
        GetMaterialClassData()
        GetMaterialTypeData()
        GetMaterialUnitData()
        GetMeasuringimportanceData()
        GetMeasuringTextureData()
        state.fileUrl = await getInsertFileUrl()
      }

      //物料类别
      const GetMaterialClassData = async () => {
        const MaterialClass = await GetMaterialClassList()
        state.MaterialClassRadio = MaterialClass.data
      }

      //物料类型
      const GetMaterialTypeData = async () => {
        const MaterialType = await GetMaterialTypeList()
        state.MaterialTypeRadio = MaterialType.data
      }

      //基本单位
      const GetMaterialUnitData = async () => {
        const MaterialUnit = await GetMaterialUnitList()
        state.MaterialUnitRadio = MaterialUnit.data
      }

      //重要度
      const GetMeasuringimportanceData = async () => {
        const Measuringimportance = await GetMeasuringimportanceList()
        state.MeasuringimportanceRadio = Measuringimportance.data
      }

      //材质
      const GetMeasuringTextureData = async () => {
        const MeasuringTexture = await GetMeasuringTextureList()
        state.MeasuringTextureRadio = MeasuringTexture.data
      }

      const GetProFilesByPid = async (id) => {
        state.editFiles = await GetFilesByPid(id)
        state.editFiles.data.forEach((item) => {
          state.fileList.push({ name: item.fileName, url: item.rootPath })
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
        GetMaterialClassData,
        GetMaterialTypeData,
        GetMaterialUnitData,
        GetMeasuringimportanceData,
        GetMeasuringTextureData,
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
