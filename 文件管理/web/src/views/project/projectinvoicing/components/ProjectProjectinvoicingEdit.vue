<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="520px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-form-item label="开票编号：" prop="invoiceNo">
        <el-input v-model.trim="form.invoiceNo" />
      </el-form-item>
      <el-form-item label="项目名称" prop="projectId">
        <rw-project
          v-model="form.projectId"
          placeholder="输入编号或名称模糊搜索"
          style="width: 100%"
          @change="ProSelectChange"
        />
      </el-form-item>
      <el-form-item label="合同编号：" prop="contactId">
        <el-select
          v-model="form.contactId"
          class="m-2"
          placeholder="请选择"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in CSearchoptions"
            :key="item.id"
            :label="item.ct_code"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="开票日期：" prop="invoiceDate">
        <el-date-picker
          v-model="form.invoiceDate"
          :disabled-date="disabledDate"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="开票金额(元)：" prop="invoiceAmount">
        <el-input-number
          v-model.number="form.invoiceAmount"
          class="mx-4"
          controls-position="right"
          :max="999999999"
          :min="1"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="不含税金额(元)：" prop="amounExcludingTax">
        <el-input-number
          v-model="form.amounExcludingTax"
          class="mx-4"
          controls-position="right"
          :max="999999999"
          :min="0"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="开票税率(%)：" prop="invoiceTaxRate">
        <el-input-number
          v-model="form.invoiceTaxRate"
          class="mx-4"
          controls-position="right"
          :max="999999999"
          :min="0"
          :style="{ width: '100%' }"
        />
      </el-form-item>
      <el-form-item label="开票申请人：" prop="applyMan">
        <!-- <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.applyManTxt"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <el-button @click="applyManOpenDig">选择</el-button>
            </template>
          </el-input>
        </div> -->
        <rw-user v-model="form.applyMan" filterable style="width: 100%" />
      </el-form-item>
      <el-form-item label="开票是否挂账：" prop="isCredit">
        <el-radio-group v-model="form.isCredit" @click="handClickOnIscredit">
          <el-radio :label="true">是</el-radio>
          <el-radio :label="false">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="挂账日期：" prop="creditDate">
        <el-date-picker
          v-model="form.creditDate"
          :disabled="form.isCredit == false"
          :style="{ width: '100%' }"
          type="date"
        />
      </el-form-item>
      <el-form-item label="备注" prop="Remark">
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
    getProjectCode,
    GetContractList,
    getContactsList,
    GetListById,
  } from '@/api/projectProjectinvoicing'
  import RwUser from '@/plugins/RwUser'
  import RwProject from '@/plugins/RwProject'

  export default defineComponent({
    name: 'ProjectProjectinvoicingEdit',
    components: { RwUser, RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      //const $baseConfirm = inject('$baseConfirm')

      const state = reactive({
        formRef: null,
        form: {
          roles: [],
          isCredit: false,
        },
        rules: {
          invoiceNo: [
            { required: true, trigger: 'blur', message: '请输入开票编号' },
          ],
          projectId: [
            { required: true, trigger: 'blur', message: '请输入项目编号' },
          ],
          contactId: [
            { required: true, trigger: 'blur', message: '请输入合同编号' },
          ],
          invoiceDate: [
            { required: true, trigger: 'blur', message: '请输入开票日期' },
          ],
          invoiceAmount: [
            { required: true, trigger: 'change', message: '请输入开票金额' },
          ],
        },
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          contactsName: '',
          rolId: 0,
        },
        layout: 'total, sizes, prev, pager, next, jumper',
        list: [],
        title: '',
        Searchoptions: [],
        CSearchoptions: [],
        dialogTableVisible: false,
        dialogFormVisible: false,
        total: 0,
        selectRows: '',
        multipleTableRef: null,
        applyManFirst: true,
        disabledDate(time) {
          return time.getTime() <= new Date(state.form.projectReceiveDate)
        },
      })

      //--------------------------对话框表格--------------------------------------
      const applyManOpenDig = async () => {
        state.dialogTableVisible = true
      }
      const fetchListData = async () => {
        state.listLoading = true
        state.queryForm.rolId = state.form.applyMan
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
            state.form.applyMan = ''
            state.form.applyManTxt = ''
            state.form.applyMan = ids
            state.form.applyManTxt = Names
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
            if (row.id === parseInt(state.form.applyMan)) {
              if (state.applyManFirst) {
                state['multipleTableRef'].toggleRowSelection(row)
                state.applyManFirst = false
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
        state.applyManFirst = true
        state['multipleTableRef'].clearSelection()
      }
      //--------------------------------------------------------------------------

      const fetchProjectData = async () => {
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
          AutoSelect(0)
          state.form.projectReceiveDate = '0001-01-01'
        } else {
          const getProData = await GetListById({ Id: proId[proId.length - 1] })
          state.form.projectReceiveDate = getProData.data[0].projectReceiveDate
          AutoSelect(proId[proId.length - 1])
        }
      }

      const AutoSelect = async (pid) => {
        const SearchoptionsData = await GetContractList({ Id: pid })
        state.CSearchoptions = SearchoptionsData.data
        state.CSearchoptions.forEach((item) => {
          item.ct_code = `${item.ct_code} ${item.contractName}`
        })
      }

      const fetchContractData = async () => {
        const SearchoptionsData = await GetContractList({ Id: 0 })
        state.CSearchoptions = SearchoptionsData.data
        state.CSearchoptions.forEach((item) => {
          item.ct_code = `${item.ct_code} ${item.contractName}`
        })
      }
      const handClickOnIscredit = () => {
        if (state.form.isCredit) {
          state.form.creditDate = null
        }
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          AutoSelect(0)
        } else {
          if (row > 0) {
            state.title = '添加'
            state.form.projectId = row
          } else {
            state.title = '编辑'
            row.invoiceTaxRate =
              row.invoiceTaxRate == 0 ? null : row.invoiceTaxRate
            row.amounExcludingTax =
              row.amounExcludingTax == 0 ? null : row.amounExcludingTax
            state.form = Object.assign({}, row)
            AutoSelect(state.form.projectId)
          }
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
          isCredit: false,
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
        state['formRef'].validate(async (valid) => {
          if (valid) {
            if (state.form.projectId.length != undefined) {
              state.form.projectId =
                state.form.projectId[state.form.projectId.length - 1]
            }
            state.form.invoiceTaxRate =
              state.form.invoiceTaxRate == null ? 0 : state.form.invoiceTaxRate
            state.form.amounExcludingTax =
              state.form.amounExcludingTax == null
                ? 0
                : state.form.amounExcludingTax
            state.form.creditDate = changeNullDate(state.form.creditDate)
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
        fetchProjectData()
        fetchContractData()
      })
      return {
        ...toRefs(state),
        showEdit,
        changeNullDate,
        handClickOnIscredit,
        fetchContractData,
        fetchProjectData,
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
        applyManOpenDig,
        ProSelectChange,
        close,
        save,
        AutoSelect,
      }
    },
  })
</script>
