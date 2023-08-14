<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="520px"
    @close="close"
  >
    <el-form ref="formRef" label-width="130px" :model="form" :rules="rules">
      <el-form-item label="项目编号：" prop="projectID">
        <rw-project
          v-model="form.projectID"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="handleParent"
        />
      </el-form-item>
      <el-form-item label="设备编码：" prop="deviceID">
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
      <el-form-item label="验收类别：" prop="acceptCategory">
        <el-select
          v-model="form.acceptCategory"
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in GetAcceptCategoryOption"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="验收日期：" prop="acceptDate">
        <el-date-picker
          v-model="form.acceptDate"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="验收人员：" prop="acceptancerName">
        <rw-user v-model="form.acceptancer" filterable style="width: 100%" />
        <!-- <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.acceptancerName"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <el-button @click="AcceptancerOpenDig">选择</el-button>
            </template>
          </el-input>
        </div> -->
      </el-form-item>
      <el-form-item label="验收数量：" prop="qty">
        <el-input-number
          v-model.number="form.qty"
          class="mx-4"
          controls-position="right"
          :max="10000000"
          :min="0"
          :precision="0"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="竣工日期：" prop="completedDate">
        <el-date-picker
          v-model="form.completedDate"
          :disabled-date="disabledCompleted"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="验收结论：" prop="acceptResult">
        <el-select
          v-model="form.acceptResultVAlue"
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in CheckAcceptCategoryOption"
            :key="item.id"
            :label="item.dictionaryText"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="最终完成日期：" prop="finalAbarbeitungDate">
        <el-date-picker
          v-model="form.finalAbarbeitungDate"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="确认签字附件：" prop="signConfirmFile">
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
              限制上传一个png/pdf文件，且文件不能超过20MB&nbsp;
              <span
                v-if="form.signConfirmFile != 0 && form.signConfirmFile != null"
              >
                （已有文件）
              </span>
            </div>
          </template>
        </el-upload>
      </el-form-item>
      <el-form-item label="备注：" prop="remark">
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
    <!-- 联系人选择对话框 -->
    <el-dialog
      v-model="dialogTableVisible"
      title="联系人选择"
      @closed="DigClosed"
      @opened="DigOpened"
    >
      <vab-query-form-left-panel :span="20">
        <el-form
          :inline="true"
          label-width="100px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="联系人名称">
            <el-input
              v-model.trim="queryForm.contactsName"
              clearable
              placeholder="请输入联系人名称："
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <el-table
        ref="multipleTableRef"
        v-loading="listLoading"
        :data="list"
        max-height="450"
        :row-key="getRowKeys"
        @selection-change="setSelectRows"
      >
        <el-table-column
          align="center"
          :reserve-selection="true"
          show-overflow-tooltip
          type="selection"
        />
        <el-table-column
          align="center"
          label="联系人姓名"
          prop="contactsName"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="联系人类别"
          prop="contactsCategorytext"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="负责业务"
          prop="responsibleBusiness"
          show-overflow-tooltip
        />
      </el-table>
      <el-pagination
        background
        :current-page="queryForm.pageNo"
        :layout="layout"
        :page-size="queryForm.pageSize"
        :total="total"
        @current-change="handleCurrentChange"
        @size-change="handleSizeChange"
      />
      <template #footer>
        <el-button @click="closeTabDlg">取 消</el-button>
        <el-button type="primary" @click="saveTabDlg">确 定</el-button>
      </template>
    </el-dialog>
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
    GetAcceptCategory,
    getInsertFileUrl,
    getProjectCode,
    getProDeviceCode,
    getContactsList,
    GetCheckAcceptCategory,
  } from '@/api/projectAcceptance'
  import { defineComponent, reactive, toRefs, ref } from 'vue'
  import RwProject from '@/plugins/RwProject'
  import RwUser from '@/plugins/RwUser'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'ProjectAcceptanceEdit',
    components: { RwProject, RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      //const $baseConfirm = inject('$baseConfirm')
      const upload = ref()

      const fetchUrlData = async () => {
        state.fileUrl = `${await getInsertFileUrl()}/File/InsertFile`
      }
      const beforeUpload = (file) => {
        if (
          file.type != 'image/jpeg' &&
          file.type != 'application/pdf' &&
          file.type != 'image/png'
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
      const changeUpload = (file) => {
        if (file) {
          if (file.name.length > 25) {
            const skin = file.name.split('.')[file.name.split('.').length - 1]
            file.name = `${file.name.substr(0, 19)}....${skin}`
          }
        }
      }
      const handleExceed = (files) => {
        upload.value.clearFiles()
        upload.value.handleStart(files[0])
      }
      const removeUpload = () => {
        state.form.signConfirmFile = null
      }
      const successUpload = (response) => {
        state.form.signConfirmFile = response.id
      }
      const submitUpload = () => {
        upload.value.submit()
      }

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          deviceID: [
            {
              required: true,
              message: '请选择设备编号',
              trigger: 'blur',
            },
          ],
          projectID: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择项目名称',
            },
          ],
          acceptCategory: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择验收类别',
            },
          ],
          acceptDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择验收日期',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
        GetAcceptCategoryOption: [],
        CheckAcceptCategoryOption: [],
        fileUrl: '',
        Searchoptions: [],
        DeviceOptions: [],
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          contactsName: '',
          rolId: 0,
        },
        layout: 'total, sizes, prev, pager, next, jumper',
        list: [],
        CSearchoptions: [],
        dialogTableVisible: false,
        total: 0,
        selectRows: '',
        multipleTableRef: null,
        AcceptancerFirst: true,
        disabledCompleted(time) {
          return time.getTime() >= new Date(state.form.acceptDate)
        },
        headers: { authorization: '' },
      })
      //--------------------------对话框表格--------------------------------------
      const AcceptancerOpenDig = async () => {
        state.dialogTableVisible = true
      }
      const fetchListData = async () => {
        state.listLoading = true
        state.queryForm.rolId = state.form.acceptancer
        state.queryForm.QueryType = null
        const {
          data: { list, total },
        } = await getContactsList(state.queryForm)
        state.list = list
        state.total = total
        toggleSelection(state.list)
        state.listLoading = false
      }
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchListData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchListData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        fetchListData()
      }
      const getRowKeys = (row) => {
        //返回参数必须是唯一键
        return row.id
      }
      //对话框选择行时记录数据
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const closeTabDlg = () => {
        state.dialogTableVisible = false
        fetchListData()
      }
      //对话框保存按钮
      const saveTabDlg = () => {
        if (state.selectRows.length > 0) {
          if (state.selectRows.length > 1) {
            $baseMessage('只能选择一条数据', 'error', 'vab-hey-message-error')
            return
          } else {
            //记录已选行所需要的数据
            const ids = state.selectRows.map((item) => item.id).join()
            const Names = state.selectRows
              .map((item) => item.contactsName)
              .join()
            state.form.acceptancer = ''
            state.form.acceptancerName = ''
            state.form.acceptancer = ids
            state.form.acceptancerName = Names
            closeTabDlg()
          }
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      //设置表格默认选择行
      const toggleSelection = (rows) => {
        if (rows) {
          state.list.forEach((row) => {
            //表格选中行条件
            if (row.id === parseInt(state.form.acceptancer)) {
              if (state.AcceptancerFirst) {
                state['multipleTableRef'].toggleRowSelection(row)
                state.AcceptancerFirst = false
              }
            } else {
              state['multipleTableRef'].toggleRowSelection(row, false)
            }
          })
        } else {
          //清除表格选中
          state['multipleTableRef'].clearSelection()
        }
      }
      //对话框已打开事件
      const DigOpened = () => {
        fetchListData()
      }
      //对话框已关闭事件
      const DigClosed = () => {
        state.dialogTableVisible = false
        state.AcceptancerFirst = true
        state['multipleTableRef'].clearSelection()
      }
      //--------------------------------------------------------------------------

      //项目编号
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
        } else {
          const projectContacts = await getProDeviceCode({
            Id: proId[proId.length - 1],
          })
          state.DeviceOptions = projectContacts.data
          state.DeviceOptions.forEach((item) => {
            item.deviceNo = `${item.deviceNo} ${item.deviceName}`
          })
        }
      }

      //设备信息下拉框
      const DevicefetchData = async () => {
        const projectContacts = await getProDeviceCode({ Id: 0 })
        state.DeviceOptions = projectContacts.data
        state.DeviceOptions.forEach((item) => {
          item.deviceNo = `${item.deviceNo} ${item.deviceName}`
        })
      }

      //验收类别
      const GetAcceptCategoryFetch = async () => {
        const GetOptions = await GetAcceptCategory()
        console.log(GetOptions)
        state.GetAcceptCategoryOption = GetOptions.data
      }

      //验收结论
      const GetCheckAcceptCategoryFetch = async () => {
        const GetOptions = await GetCheckAcceptCategory()
        console.log(GetOptions)
        state.CheckAcceptCategoryOption = GetOptions.data
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
        } else {
          state.title = '编辑'
          state.form = Object.assign({}, row)
          state.form.acceptResultVAlue =
            state.form.acceptResult == 0 ? '' : state.form.acceptResult
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        upload.value.clearFiles()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const changeNullDate = (val) => {
        if (val == null) {
          return '0001-01-01 00:00:00'
        }
        return val
      }
      const save = () => {
        //state.form.qty = state.form.qty == null ? 0 : state.form.qty
        console.log(state.form)
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.projectID.length != undefined) {
              state.form.projectID =
                state.form.projectID[state.form.projectID.length - 1]
            }
            state.form.acceptResult =
              state.form.acceptResultVAlue == ''
                ? 0
                : state.form.acceptResultVAlue
            state.form.completedDate = changeNullDate(state.form.completedDate)
            state.form.finalAbarbeitungDate = changeNullDate(
              state.form.finalAbarbeitungDate
            )
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }
      onMounted(() => {
        GetAcceptCategoryFetch()
        GetCheckAcceptCategoryFetch()
        fetchUrlData()
        ProfetchData()
        DevicefetchData()
        const userStore = useUserStore()
        const { token } = storeToRefs(userStore)
        state.headers.authorization = `Bearer ${token.value}`
      })

      return {
        ...toRefs(state),
        showEdit,
        GetAcceptCategoryFetch,
        GetCheckAcceptCategoryFetch,
        close,
        save,
        upload,
        submitUpload,
        handleExceed,
        successUpload,
        removeUpload,
        fetchUrlData,
        beforeUpload,
        changeUpload,
        ProfetchData,
        DevicefetchData,
        fetchListData,
        handleSizeChange,
        handleCurrentChange,
        getRowKeys,
        setSelectRows,
        queryData,
        closeTabDlg,
        saveTabDlg,
        toggleSelection,
        DigOpened,
        DigClosed,
        AcceptancerOpenDig,
        changeNullDate,
        ProSelectChange,
      }
    },
  })
</script>
