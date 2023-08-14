<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-form ref="formRef" label-width="130px" :model="form" :rules="rules">
      <el-form-item label="市场片区编码：" prop="areaCode">
        <el-input v-model.trim="form.areaCode" />
      </el-form-item>
      <el-form-item label="市场片区名称：" prop="areaName">
        <el-input v-model.trim="form.areaName" />
      </el-form-item>
      <el-form-item label="片区营销经理：" prop="areaGM">
        <rw-user v-model="form.areaGM" filterable style="width: 100%" />
        <!-- <el-input v-model="form.AreaGM" /> -->
        <!-- <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.areaGMText"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <el-button @click="AreaGMPenDig">选择</el-button>
            </template>
          </el-input>
        </div> -->
      </el-form-item>
      <el-form-item label="片区负责人：" prop="areaCharger">
        <rw-user v-model="form.areaCharger" filterable style="width: 100%" />
        <!-- <el-input v-model="form.AreaCharger" /> -->
        <!-- <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.areaChargerText"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <el-button @click="AreaChargerPenDig">选择</el-button>
            </template>
          </el-input>
        </div> -->
      </el-form-item>
      <el-form-item label="片区营销人员：" prop="areaSalesman">
        <rw-user v-model="form.areaSalesman" filterable style="width: 100%" />
        <!-- <el-input v-model="form.AreaSalesman" /> -->
        <!-- <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.areaSalesmanText"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <el-button @click="AreaSalesmanPenDig">选择</el-button>
            </template>
          </el-input>
        </div> -->
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
  import { doEdit, doAdd, getContactsList } from '@/api/basicsSalesareinfo'
  import RwUser from '@/plugins/RwUser'
  export default defineComponent({
    name: 'BasicsSalesareinfoEdit',
    components: { RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      //const $baseConfirm = inject('$baseConfirm')

      const state = reactive({
        list: [],
        formRef: null,
        form: {
          roles: [],
        },
        rules: {
          areaCode: [
            { required: true, trigger: 'blur', message: '请输入市场片区编码' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          areaName: [
            { required: true, trigger: 'blur', message: '请输入市场片区名称' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          placeName: [
            { required: true, trigger: 'blur', message: '请输入板块名称' },
          ],
        },
        layout: 'total, sizes, prev, pager, next, jumper',
        title: '',
        total: 0,
        selectRows: '',
        multipleTableRef: null,
        dialogFormVisible: false,
        dialogTableVisible: false,
        AreaGMPen: false,
        AreaChargerPen: false,
        AreaSalesmanPen: false,
        areaGMFirst: true,
        areaChargerFirst: true,
        areaSalesmanFirst: true,
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          contactsName: '',
          rolId: 0,
        },
      })
      //--------------------------对话框表格--------------------------------------
      const AreaGMPenDig = async () => {
        state.dialogTableVisible = true
        state.AreaGMPen = true
      }
      const AreaChargerPenDig = async () => {
        state.dialogTableVisible = true
        state.AreaChargerPen = true
      }
      const AreaSalesmanPenDig = async () => {
        state.dialogTableVisible = true
        state.AreaSalesmanPen = true
      }
      const fetchListData = async () => {
        state.listLoading = true
        if (state.AreaGMPen === true) {
          state.queryForm.rolId = state.form.areaGM
        }
        if (state.AreaChargerPen === true) {
          state.queryForm.rolId = state.form.areaCharger
        }
        if (state.AreaSalesmanPen === true) {
          state.queryForm.rolId = state.form.areaSalesman
        }
        state.queryForm.QueryType = '内部'
        const {
          data: { list, total },
        } = await getContactsList(state.queryForm)
        const AfterQueryList = list.filter((item) => {
          return (
            item.contactsCategorytext && item.contactsCategorytext === '内部'
          )
        })
        state.list = AfterQueryList
        console.log(total)
        state.total = state.list.length
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
        state.AreaChargerPen = false
        state.AreaSalesmanPen = false
        state.AreaGMPen = false
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
            const contactsNames = state.selectRows
              .map((item) => item.contactsName)
              .join()
            if (state.AreaGMPen === true) {
              state.form.areaGM = ''
              state.form.areaGMText = ''
              state.form.areaGM = ids
              state.form.areaGMText = contactsNames
            } else if (state.AreaChargerPen === true) {
              state.form.areaCharger = ''
              state.form.areaChargerText = ''
              state.form.areaCharger = ids
              state.form.areaChargerText = contactsNames
            } else if (state.AreaSalesmanPen === true) {
              state.form.areaSalesman = ''
              state.form.areaSalesmanText = ''
              state.form.areaSalesman = ids
              state.form.areaSalesmanText = contactsNames
            }
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
            if (state.AreaGMPen === true) {
              if (row.id === parseInt(state.form.areaGM)) {
                if (state.areaGMFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.areaGMFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
            } else if (state.AreaChargerPen === true) {
              if (row.id === parseInt(state.form.areaCharger)) {
                if (state.areaChargerFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.areaChargerFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
            } else if (state.AreaSalesmanPen === true) {
              if (row.id === parseInt(state.form.areaSalesman)) {
                if (state.areaSalesmanFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.areaSalesmanFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
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
        state.AreaGMPen = false
        state.AreaChargerPen = false
        state.AreaSalesmanPen = false
        state.areaGMFirst = true
        state.areaChargerFirst = true
        state.areaSalesmanFirst = true
        queryData()
        state['multipleTableRef'].clearSelection()
      }
      //--------------------------------------------------------------------------
      const GetNowDate = () => {
        const newDate = new Date()
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return y + m + d
      }
      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.form.areaCode = `MA${GetNowDate()}${Math.floor(
            Math.random() * 10000
          )}`
        } else {
          state.title = '编辑'
          row.areaGM = row.areaGM == 0 ? null : row.areaGM
          row.areaCharger = row.areaCharger == 0 ? null : row.areaCharger
          row.areaSalesman = row.areaSalesman == 0 ? null : row.areaSalesman
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
            state.form.areaGM =
              state.form.areaGM == null ? 0 : state.form.areaGM
            state.form.areaCharger =
              state.form.areaCharger == null ? 0 : state.form.areaCharger
            state.form.areaSalesman =
              state.form.areaSalesman == null ? 0 : state.form.areaSalesman
            if (state.form.id > 0) {
              const { msg } = await doEdit(state.form)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              emit('fetch-data')
              close()
            } else {
              const { msg } = await doAdd(state.form)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              emit('fetch-data')
              close()
            }
          }
        })
      }
      onMounted(() => {})
      return {
        ...toRefs(state),
        showEdit,
        AreaGMPenDig,
        AreaChargerPenDig,
        AreaSalesmanPenDig,
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
        GetNowDate,
        close,
        save,
      }
    },
  })
</script>
