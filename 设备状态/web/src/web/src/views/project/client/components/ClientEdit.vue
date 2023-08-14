<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="550px"
    @close="close"
  >
    <el-form ref="formRef" label-width="105px" :model="form" :rules="rules">
      <el-form-item label="单位编号" prop="companyCode">
        <el-input v-model.trim="form.companyCode" />
      </el-form-item>
      <el-form-item label="客户简称" prop="clientName">
        <el-input v-model.trim="form.clientName" />
      </el-form-item>
      <el-form-item label="客户全称" prop="clientFullName">
        <el-input v-model.trim="form.clientFullName" />
      </el-form-item>
      <el-form-item label="市场片区" prop="marketArea">
        <el-select v-model="form.marketArea" filterable style="width: 100%">
          <el-option
            v-for="item in marketAreaOptions"
            :key="item.id"
            :label="item.areaNameAndPlaceName"
            :value="item.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="地址" prop="address">
        <el-input v-model="form.address" />
      </el-form-item>
      <el-form-item label="客户级别" prop="clientRank">
        <el-select v-model="form.clientRank" filterable style="width: 100%">
          <el-option
            v-for="item in clientOptions"
            :key="item.code"
            :disabled="item.disabled"
            :label="item.name"
            :value="item.code"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="合作业务" prop="cooperativeBusiness">
        <el-select
          v-model="form.cooperativeBusiness"
          filterable
          style="width: 100%"
        >
          <el-option
            v-for="item in cooperaOptions"
            :key="item.code"
            :disabled="item.disabled"
            :label="item.name"
            :value="item.code"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="公司董事长" prop="ceoName">
        <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.ceoName"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <!-- 弹框触发 -->
              <el-button @click="CEOOPenDig">选择</el-button>
            </template>
          </el-input>
        </div>
      </el-form-item>
      <el-form-item label="公司总经理" prop="gmName">
        <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.gmName"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <!-- 弹框触发 -->
              <el-button @click="GMOPenDig">选择</el-button>
            </template>
          </el-input>
        </div>
      </el-form-item>
      <el-form-item label="公司副总经理" prop="deputyGMName">
        <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.deputyGMName"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <!-- 弹框触发 -->
              <el-button @click="deputyGMOPenDig">选择</el-button>
            </template>
          </el-input>
        </div>
      </el-form-item>
      <el-form-item label="营销负责人" prop="personInChargeName">
        <div class="mt-4" :style="{ width: '100%' }">
          <el-input
            v-model="form.personInChargeName"
            class="input-with-select"
            :disabled="true"
          >
            <template #append>
              <!-- 弹框触发 -->
              <el-button @click="PersonInChargeOPenDig">选择</el-button>
            </template>
          </el-input>
        </div>
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
  import { defineComponent, reactive, toRefs } from 'vue'
  import {
    doEdit,
    doAdd,
    GetDicClient,
    GetDicBusiness,
    getContactsList,
    GetSalseAreaSelect,
  } from '@/api/client'

  export default defineComponent({
    name: 'ClientEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      //const $baseConfirm = inject('$baseConfirm')

      const state = reactive({
        title: '',
        formRef: null,
        form: {
          companyCode: '',
          clientName: '',
          clientFullName: '',
          ownerName: '',
          address: '',
          remark: '',
          clientRank: [],
          cooperativeBusiness: [],
        },
        clientOptions: [],
        cooperaOptions: [],
        marketAreaOptions: [],
        rules: {
          companyCode: [
            { required: true, trigger: 'blur', message: '请输入单位编号' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          clientName: [
            { required: true, trigger: 'blur', message: '请输入客户名称' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          clientFullName: [
            { required: true, trigger: 'blur', message: '请输入客户全称' },
            { max: 32, trigger: 'blur', message: '限制输入32个字符' },
          ],
          address: [
            //{ required: true, trigger: 'blur', message: '请输入地址' },
            { max: 50, trigger: 'blur', message: '限制输入50个字符' },
          ],
          clientRank: [
            { required: true, trigger: 'blur', message: '请选择客户级别' },
          ],
          // cooperativeBusiness: [
          //   { required: true, trigger: 'blur', message: '请选择合作业务' },
          // ],
          // marketArea: [
          //   { required: true, trigger: 'blur', message: '请选择市场片区' },
          // ],
        },
        dialogFormVisible: false,

        multipleTableRef: null,
        dialogTableVisible: false,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        ceoFirst: true,
        gmFirst: true,
        deputyGMFirst: true,
        personInChargeFirst: true,
        CEOOPen: false,
        GMOPen: false,
        deputyGMOPen: false,
        PersonInChargeOPen: false,
        InitialMarketID: 0,
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          contactsName: '',
          rolId: 0,
        },
      })
      const fetchData = async () => {
        const clientlist = await GetDicClient()
        state.clientOptions = clientlist.data
      }
      const feData = async () => {
        const cooperalist = await GetDicBusiness()
        state.cooperaOptions = cooperalist.data
      }

      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }

      //--------------------------对话框表格--------------------------------------
      const CEOOPenDig = async () => {
        state.dialogTableVisible = true
        state.CEOOPen = true
      }
      const GMOPenDig = async () => {
        state.dialogTableVisible = true
        state.GMOPen = true
      }
      const deputyGMOPenDig = async () => {
        state.dialogTableVisible = true
        state.deputyGMOPen = true
      }
      const PersonInChargeOPenDig = async () => {
        state.dialogTableVisible = true
        state.PersonInChargeOPen = true
      }
      const fetchListData = async () => {
        state.listLoading = true
        if (state.CEOOPen === true) {
          state.queryForm.rolId = state.form.ceo
        }
        if (state.GMOPen === true) {
          state.queryForm.rolId = state.form.gm
        }
        if (state.deputyGMOPen === true) {
          state.queryForm.rolId = state.form.deputyGM
        }
        if (state.PersonInChargeOPen === true) {
          state.queryForm.rolId = state.form.personInCharge
          state.queryForm.QueryType = '内部'
        } else {
          state.queryForm.QueryType = '客户'
        }
        const {
          data: { list, total },
        } = await getContactsList(state.queryForm)
        state.list = list
        if (state.PersonInChargeOPen == true) {
          const AfterQueryList = state.list.filter((item) => {
            return (
              item.contactsCategorytext && item.contactsCategorytext === '内部'
            )
          })
          state.list = AfterQueryList
        } else {
          const AfterQueryList = state.list.filter((item) => {
            return (
              item.contactsCategorytext && item.contactsCategorytext === '客户'
            )
          })
          state.list = AfterQueryList
        }
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
        state.CEOOPen = false
        state.GMOPen = false
        state.deputyGMOPen = false
        state.PersonInChargeOPen = false
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
            if (state.CEOOPen === true) {
              state.form.ceo = ''
              state.form.ceoName = ''
              state.form.ceo = ids
              state.form.ceoName = contactsNames
            } else if (state.GMOPen === true) {
              state.form.gm = ''
              state.form.gmName = ''
              state.form.gm = ids
              state.form.gmName = contactsNames
            } else if (state.deputyGMOPen === true) {
              state.form.deputyGM = ''
              state.form.deputyGMName = ''
              state.form.deputyGM = ids
              state.form.deputyGMName = contactsNames
            } else if (state.PersonInChargeOPen === true) {
              state.form.personInCharge = ''
              state.form.personInChargeName = ''
              state.form.personInCharge = ids
              state.form.personInChargeName = contactsNames
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
            if (state.CEOOPen === true) {
              if (row.id === parseInt(state.form.ceo)) {
                if (state.ceoFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.ceoFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
            } else if (state.GMOPen === true) {
              if (row.id === parseInt(state.form.gm)) {
                if (state.gmFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.gmFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
            } else if (state.deputyGMOPen === true) {
              if (row.id === parseInt(state.form.deputyGM)) {
                if (state.deputyGMFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.deputyGMFirst = false
                }
              } else {
                state['multipleTableRef'].toggleRowSelection(row, false)
              }
            } else if (state.PersonInChargeOPen === true) {
              if (row.id === parseInt(state.form.personInCharge)) {
                if (state.personInChargeFirst) {
                  state['multipleTableRef'].toggleRowSelection(row)
                  state.personInChargeFirst = false
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
        state.CEOOPen = false
        state.GMOPen = false
        state.deputyGMOPen = false
        state.PersonInChargeOPen = false
        state.ceoFirst = true
        state.gmFirst = true
        state.deputyGMFirst = true
        state.personInChargeFirst = true
        queryData()
        state['multipleTableRef'].clearSelection()
      }
      //---------------------------------------------------------------------------
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
          state.form.companyCode = `CM${GetNowDate()}${Math.floor(
            Math.random() * 10000
          )}`
        } else {
          row.cooperativeBusiness =
            row.cooperativeBusiness == 0 ? '' : row.cooperativeBusiness
          state.title = '编辑'
          state.InitialMarketID = row.marketArea
          row.marketArea = row.marketAreaTxt == null ? '' : row.marketArea
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
            state.form.cooperativeBusiness =
              state.form.cooperativeBusiness == ''
                ? 0
                : state.form.cooperativeBusiness
            state.form.marketArea =
              state.form.marketArea == ''
                ? state.InitialMarketID
                : state.form.marketArea
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
      onMounted(() => {
        fetchData()
        feData()
        fetchMarketArea()
      })
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetNowDate,
        fetchData,
        feData,
        fetchMarketArea,
        handleSizeChange,
        handleCurrentChange,
        getRowKeys,
        setSelectRows,
        toggleSelection,
        saveTabDlg,
        queryData,
        DigOpened,
        DigClosed,
        closeTabDlg,
        CEOOPenDig,
        GMOPenDig,
        deputyGMOPenDig,
        PersonInChargeOPenDig,
      }
    },
  })
</script>
