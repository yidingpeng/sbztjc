<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="130px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="24">
          <el-form-item label="项目编号" prop="pt_idsTxt">
            <el-tag
              v-for="tag in dynamicTags"
              :key="tag.key"
              class="mx-1"
              closable
              :disable-transitions="false"
              @close="handleClose(tag)"
            >
              {{ tag.name }}
            </el-tag>
            &nbsp;
            <el-icon class="avatar-uploader-icon">
              <plus :size="50" @click="ProChooseOpenDig" />
            </el-icon>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="内部合同编号" prop="ct_code">
            <el-input
              v-model.trim="form.ct_code"
              disabled
              placeholder="请输入内部合同编号"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="外部合同编号" prop="externalCt_code">
            <el-input
              v-model.trim="form.externalCt_code"
              placeholder="请输入外部合同编号"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="合同名称" prop="contractName">
            <el-input
              v-model.trim="form.contractName"
              placeholder="请输入合同名称"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="合同总金额(元)" prop="ct_cash">
            <el-input-number
              v-model.number="form.ct_cash"
              class="mx-4"
              controls-position="right"
              :max="99999999"
              :min="0"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <!-- <el-row>
        <el-col :span="12">
          <el-form-item label="项目数量" prop="proCount">
            <el-input-number
              v-model="form.proCount"
              :max="10"
              :min="1"
              @change="handleChange"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="项目合同单价" prop="proUnitPrice">
            <el-input-number
              v-model="form.proUnitPrice"
              :min="1"
              :precision="2"
              @change="handleChange"
            />
          </el-form-item>
        </el-col>
      </el-row> -->
      <el-row>
        <el-col :span="12">
          <el-form-item label="合同税率(%)" prop="ctTaxRate">
            <el-input-number
              v-model.number="form.ctTaxRate"
              class="mx-4"
              controls-position="right"
              :min="0"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="合同签订日期" prop="ct_signingDate">
            <el-date-picker
              v-model="form.ct_signingDate"
              :disabled-date="disabledDate2"
              placeholder="请选择合同签订日期"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <!-- <el-row>
        <el-col :span="12">
          <el-form-item label="要求交付日期" prop="ct_deliveryDate">
            <el-date-picker
              v-model="form.ct_deliveryDate"
              placeholder="请选择合同交货日期"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
      </el-row> -->
      <el-row>
        <el-col :span="24">
          <el-form-item label="回款条款" prop="payment_collection">
            <!-- <el-input v-model.trim="form.payment_collection" /> -->
            <el-input
              v-model="form.payment_collection"
              :autosize="{ minRows: 4, maxRows: 4 }"
              :maxlength="500"
              placeholder="请输入回款条款"
              show-word-limit
              :style="{ width: '100%' }"
              type="textarea"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="合同扫描件" prop="ct_attachmentPdfUrl">
            <el-upload
              ref="pdfUpload"
              v-model="form.ct_attachmentPdfUrl"
              :action="fileUrl"
              :auto-upload="false"
              :before-upload="pdfBeforeUpload"
              class="upload-demo"
              :data="{ Filepid: form.id }"
              :headers="headers"
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
              <el-button
                v-if="form.ct_attachmentPdfUrl.length > 0"
                class="ml-3"
                link
                type="primary"
                @click="DeleteFiles(form.id, form.ct_attachmentPdfUrl)"
              >
                删除原文件
              </el-button>
              <template #tip>
                <div class="el-upload__tip" style="color: red">
                  请上传pdf文件,且单个文件不能超过20MB
                  <span v-if="form.ct_attachmentPdfUrl.length > 0">
                    （已有文件）
                  </span>
                </div>
              </template>
            </el-upload>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="合同可编辑件" prop="ct_attachmentWordUrl">
            <el-upload
              ref="wordUpload"
              v-model="form.ct_attachmentWordUrl"
              :action="fileUrl"
              :auto-upload="false"
              :before-upload="wordBeforeUpload"
              class="upload-demo"
              :data="{ Filepid: form.id }"
              :headers="headers"
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
              <el-button
                v-if="form.ct_attachmentWordUrl.length > 0"
                class="ml-3"
                link
                type="primary"
                @click="DeleteFiles(form.id, form.ct_attachmentWordUrl)"
              >
                删除原文件
              </el-button>
              <template #tip>
                <div class="el-upload__tip" style="color: red">
                  请上传word文件,且单个文件不能超过20MB
                  <span v-if="form.ct_attachmentWordUrl.length > 0">
                    （已有文件）
                  </span>
                </div>
              </template>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
      <!-- <el-row>
        <el-col :span="24">
          <el-form-item label="回款计划" prop="noprop">
            <el-button @click="PPHandleClick">点击编辑</el-button>
          </el-form-item>
        </el-col>
      </el-row> -->
      <el-row>
        <el-col :span="24">
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
        </el-col>
      </el-row>
    </el-form>
    <!--选择项目信息对话框 -->
    <el-dialog
      v-model="dialogTableVisible"
      title="项目选择"
      @closed="DigClosed"
      @open="DigOpen"
    >
      <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.key"
              clearable
              placeholder="请输入项目名称或项目编号"
            />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="ClickQueryData">查询</el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel>
      <el-table
        ref="multipleTableRef"
        v-loading="listLoading"
        border
        :data="list"
        default-expand-all
        max-height="400px"
        :row-key="getRowKeys"
        @selection-change="setSelectRows"
      >
        <el-table-column
          align="center"
          :reserve-selection="true"
          type="selection"
        />
        <el-table-column
          label="项目名称"
          prop="projectName"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="项目编号"
          prop="projectCode"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="项目接受日"
          prop="projectReceiveDate"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="市场片区"
          prop="marketAreaTxt"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="客户名称"
          prop="clientCompanyName"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="营销经理"
          prop="personInChargeName"
          show-overflow-tooltip
        />
      </el-table>
      <el-pagination
        :current-page="queryForm.pageNo"
        layout="total, sizes, prev, pager, next, jumper"
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
    <!--编辑回款计划对话框 -->
    <!-- <el-dialog
      v-model="PPdialogTableVisible"
      title="编辑回款计划"
      @open="PPDigOpen"
    >
      <el-table
        ref="multipleTableRef"
        v-loading="listLoading"
        :data="PPlist"
        default-expand-all
        max-height="400px"
        @cell-click="cellClick"
      >
        <el-table-column
          align="center"
          label="回款类型"
          prop="paymentCTypeID"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="比例（%）"
          prop="paymentCProportion"
          show-overflow-tooltip
        >
          <template #default="scope">
            <el-input
              v-if="scope.row.edit"
              ref="tableInput"
              v-model="scope.row.paymentCProportion"
              size="small"
            />
            <el-button
              v-if="scope.row.edit"
              link type="primary"
              @click="removeClass(scope.row)"
            >
              确认
            </el-button>
            <span v-else>{{ scope.row.paymentCProportion }}</span>
          </template>
        </el-table-column>
        <el-table-column
          align="center"
          label="合同金额回款计划(元)"
          prop="conAmountCPlan"
          show-overflow-tooltip
        >
          <template #default="scope">
            {{ formatCurrency(scope.row.conAmountCPlan) }}
          </template>
        </el-table-column>
      </el-table>
      <div style="margin-top: 5px; color: darkblue; text-align: right">
        总合计：{{ paymentCTotalTxt }}元
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </div>
      <template #footer>
        <el-button @click="closePPTabDlg">取 消</el-button>
        <el-button type="primary" @click="SavePPTabDlg">确 定</el-button>
      </template>
    </el-dialog> -->
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
    //getProjectCode,
    getInsertFileUrl,
    DeleteFilesByPid,
    GetTheLastCtCode,
    ProBasicsTreeList,
    GetPaymentCListByConId,
    DoEditOrAdd,
    DoDeleteCtIdZero,
  } from '@/api/contract'
  import { ref } from 'vue'
  import { Plus } from '@element-plus/icons-vue'
  import { useUserStore } from '@/store/modules/user'

  export default defineComponent({
    name: 'ContractEdit',
    components: { Plus },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const pdfUpload = ref()
      const wordUpload = ref()
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        pdfFile: [],
        wordFile: [],
        fileUrl: '',
        formRef: null,
        fileList: [],
        dynamicTags: [],
        form: {
          pt_idsTxt: '',
          proCount: 1,
          roles: [],
          ct_attachmentPdfUrl: [],
          ct_attachmentWordUrl: [],
          ct_cash: 0,
        },
        total: 0,
        queryForm: {
          key: '',
          pageNo: 1,
          pageSize: 10,
        },
        //Searchoptions: [],
        rules: {
          pt_idsTxt: [
            { required: true, trigger: 'blur', message: '项目编号不能为空' },
          ],
          ct_code: [
            { required: true, trigger: 'blur', message: '合同编号不能为空' },
          ],
          contractName: [
            { required: true, trigger: 'blur', message: '合同名称不能为空' },
          ],
          ct_cash: [
            { required: true, trigger: 'blur', message: '合同总金额不能为空' },
          ],
          ct_signingDate: [
            { required: true, trigger: 'blur', message: '签订日期不能为空' },
          ],
          // ct_deliveryDate: [
          //   { required: true, trigger: 'blur', message: '交货日期不能为空' },
          // ],
          pay_mode: [
            { required: true, trigger: 'blur', message: '支付方式不能为空' },
          ],
          ctTaxRate: [
            { required: true, trigger: 'blur', message: '合同税率不能为空' },
          ],
          proCount: [
            { required: true, trigger: 'blur', message: '项目数量不能为空' },
          ],
          proUnitPrice: [
            {
              required: true,
              trigger: 'blur',
              message: '项目合同单价不能为空',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
        disabledDate(time) {
          return time.getTime() < Date.now()
        },
        disabledDate2(time) {
          return time.getTime() >= Date.now()
        },
        list: [],
        dialogTableVisible: false,
        selectRows: '',
        multipleTableRef: null,
        PPdialogTableVisible: false,
        PPlist: [],
        inputVisible: false,
        paymentCTotalTxt: '',
        paymentCTotal: 0,
        headers: { authorization: '' },
        listLoading: true,
      })
      const PPHandleClick = () => {
        state.PPdialogTableVisible = true
      }
      //---------------------------回款计划对话框----------------------------------------
      const fetchPPListData = async () => {
        state.listLoading = true
        const result = await GetPaymentCListByConId({
          contrctid: state.form.id,
        })
        if (result.data.length > 0) {
          state.PPlist = result.data
        } else {
          const amount = 0
          const amount2 = 0
          // if (state.form.ct_cash != null) {
          //   amount = state.form.ct_cash * 0.2
          //   amount2 = state.form.ct_cash * 0.1
          // }
          state.PPlist = [
            {
              paymentCTypeID: '预付款',
              paymentCProportion: 20,
              edit: false,
              conAmountCPlan: amount,
            },
            {
              paymentCTypeID: '进度款',
              edit: false,
              paymentCProportion: 20,
              conAmountCPlan: amount,
            },
            {
              paymentCTypeID: '验收款',
              edit: false,
              paymentCProportion: 20,
              conAmountCPlan: amount,
            },
            {
              paymentCTypeID: '质保金',
              edit: false,
              paymentCProportion: 20,
              conAmountCPlan: amount,
            },
            {
              paymentCTypeID: '结算款',
              edit: false,
              paymentCProportion: 10,
              conAmountCPlan: amount2,
            },
            {
              paymentCTypeID: '尾款',
              edit: false,
              paymentCProportion: 10,
              conAmountCPlan: amount2,
            },
          ]
        }
        state.paymentCTotal = 0
        state.paymentCTotalTxt = ''
        //computedTotal()
        state.listLoading = false
      }
      const PPDigOpen = () => {
        fetchPPListData()
      }
      const closePPTabDlg = () => {
        state.PPdialogTableVisible = false
      }

      //计算总合计
      const computedTotal = () => {
        state.PPlist.forEach((item) => {
          state.paymentCTotal += item.conAmountCPlan
        })
        state.paymentCTotalTxt = formatCurrency(state.paymentCTotal)
      }
      const removeClass = (row) => {
        row.edit = false
        // row.conAmountCPlan =
        //   ct_cash * (parseFloat(row.paymentCProportion) * 0.01)
        //document
        //  .getElementsByClassName('current-cell')[0]
        //  .classList.remove('current-cell')
        state.paymentCTotal = 0
        state.paymentCTotalTxt = ''
        //computedTotal()
      }

      const SavePPTabDlg = async () => {
        addPPListCtAndEdit()
        const result = await DoEditOrAdd(state.PPlist)
        if (result.newIds != '') {
          state.form.newConPayPlanStr = result.newIds
        }
        closePPTabDlg()
        // let a = state.form.ct_cash - 1
        // let b = state.form.ct_cash + 1
        // if (!(state.paymentCTotal > a && state.paymentCTotal < b)) {
        //   $baseConfirm(
        //     '当前合同金额回款计划总和不等合同金额，输入比例可能有误，是否继续保存？',
        //     null,
        //     async () => {
        //       addPPListCtAndEdit()
        //       let result = await DoEditOrAdd(state.PPlist)
        //       if (result.newIds != '') {
        //         state.form.newConPayPlanStr = result.newIds
        //       }
        //       closePPTabDlg()
        //     }
        //   )
        // } else {
        //   closePPTabDlg()
        //   addPPListCtAndEdit()
        //   let result = await DoEditOrAdd(state.PPlist)
        //   if (result.newIds != '') {
        //     state.form.newConPayPlanStr = result.newIds
        //   }
        // }
      }

      const addPPListCtAndEdit = () => {
        state.PPlist.forEach((item) => {
          item.edit = false
          item.ct_ID = state.form.id
        })
      }

      //--------------------------------------------------------------------------------

      //--------------------------项目选择对话框表格--------------------------------------
      const ProChooseOpenDig = async () => {
        state.dialogTableVisible = true
      }
      const fetchListData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await ProBasicsTreeList({
          ProjectKey: state.queryForm.key,
          pt_idsTxt: state.form.pt_idsTxt,
          PageNo: state.queryForm.pageNo,
          PageSize: state.queryForm.pageSize,
        })
        state.list = list
        state.total = total
        if (state.list != null) {
          state.list.forEach((item) => {
            changeDataFun(item)
          })
          toggleSelection(state.list)
        }
        state.listLoading = false
      }

      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchListData()
      }

      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchListData()
      }

      //设置表格日期格式
      const changeDataFun = (row) => {
        if (row.children != null) {
          row.children.forEach((crow) => {
            changeDataFun(crow)
          })
        }
        row.projectReceiveDate = changeDate(row.projectReceiveDate)
      }

      const ClickQueryData = () => {
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
          //记录已选行所需要的数据
          const ids = state.selectRows.map((item) => item.id).join()
          const Names = state.selectRows.map((item) => item.projectCode).join()
          state.form.pt_idsTxt = ''
          state.form.pt_codesTxt = ''
          state.form.pt_idsTxt = ids
          state.form.pt_codesTxt = Names
          state.dynamicTags = []
          state.selectRows.forEach((item) => {
            state.dynamicTags.push({ key: item.id, name: item.projectCode })
          })
          closeTabDlg()
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      //设置表格默认选择行
      const toggleSelection = (rows) => {
        if (rows) {
          state.list.forEach((row) => {
            selectItems(row)
          })
        } else {
          //清除表格选中
          state['multipleTableRef'].clearSelection()
        }
      }
      const selectItems = (row) => {
        //表格选中行条件
        if (row.children != null) {
          row.children.forEach((crow) => {
            selectItems(crow)
          })
        }
        if (state.form.pt_idsTxt != undefined) {
          if (state.form.pt_idsTxt.length > 0) {
            const idlist = state.form.pt_idsTxt.split(',')
            idlist.forEach((item) => {
              if (parseInt(item) == row.id) {
                state['multipleTableRef'].toggleRowSelection(row)
              }
            })
          } else {
            if (row.id === parseInt(state.form.pt_idsTxt)) {
              state['multipleTableRef'].toggleRowSelection(row)
            } else {
              state['multipleTableRef'].toggleRowSelection(row, false)
            }
          }
        }
      }
      const DigOpen = () => {
        fetchListData()
      }
      //对话框已关闭事件
      const DigClosed = () => {
        state.dialogTableVisible = false
        state.queryForm.pageNo = 1
        state.selectRows = ''
        state['multipleTableRef'].clearSelection()
      }
      //--------------------------------------------------------------------------

      //金额转换为千分位
      const formatCurrency = (value, str) => {
        if (!str) str = ''
        // str 规定货币类型
        if (value == '0') return '0.00'
        if (value == '.') return ''
        if (!value) return ''
        let val = Number(value).toFixed(2) // 提前保留两位小数
        const intPart = parseInt(val) // 获取整数部分
        const intPartFormat = intPart
          .toString()
          .replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断 ？？？
        let floatPart = '.00' // 预定义小数部分
        val = val.toString() // 将number类型转为字符串类型，方便操作
        const value2Array = val.split('.')
        if (value2Array.length === 2) {
          // =2表示数据有小数位
          floatPart = value2Array[1].toString() // 拿到小数部分
          if (floatPart.length === 1) {
            // 补0,实际上用不着
            return `${str + intPartFormat}.${floatPart}0`
          } else {
            return `${str + intPartFormat}.${floatPart}`
          }
        } else {
          return str + intPartFormat + floatPart
        }
      }
      //tag删除事件
      const handleClose = (tag) => {
        //去除该tag
        state.dynamicTags.splice(state.dynamicTags.indexOf(tag), 1)
        //把原先保存到的该id去除
        if (state.form.pt_idsTxt.length > 0) {
          const idlist = state.form.pt_idsTxt.split(',')
          idlist.forEach((item) => {
            if (parseInt(item) == tag.key) {
              const index = idlist.indexOf(String(tag.key))
              idlist.splice(index, 1)
            }
          })
          state.form.pt_idsTxt = idlist.join(',')
        } else state.form.pt_idsTxt = null
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加合同'
          state.form.id = 0
          EditTheCtCode()
        } else {
          //console.log(row)
          state.title = '编辑合同'
          let codelist = []
          let idlist = []
          if (row.pt_codesTxt != null) {
            codelist = row.pt_codesTxt.split(',')
          }
          if (row.pt_idsTxt != null) {
            idlist = row.pt_idsTxt.split(',')
          }
          for (let i = 0; i < codelist.length; i++) {
            state.dynamicTags.push({
              name: String(codelist[i]),
              key: idlist[i],
            })
          }
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      // const fetchData = async () => {
      //   const SearchoptionsData = await getProjectCode()
      //   state.Searchoptions = SearchoptionsData.data
      //   state.Searchoptions.forEach((item) => {
      //     item.projectCode = item.projectCode + ' ' + item.projectName
      //     if (item.children != undefined) {
      //       item.children.forEach((item) => {
      //         item.projectCode = item.projectCode + ' ' + item.projectName
      //       })
      //     }
      //   })
      // }
      //获取本次添加需要填写的内部合同编码
      const EditTheCtCode = async () => {
        const result = await GetTheLastCtCode()
        let ctCode = result.ctCode
        let Code = ''
        ctCode = parseInt(ctCode.substr(ctCode.length - 3, ctCode.length - 1))
        if (String(ctCode).length == 2) {
          Code = `0${parseInt(ctCode) + 1}`
        } else if (String(ctCode).length == 1) {
          Code = `00${parseInt(ctCode) + 1}`
        } else if (
          String(ctCode).length == 3 &&
          ctCode != null &&
          ctCode.toString() != 'NaN'
        ) {
          Code = parseInt(ctCode) + 1
        } else Code = '001'
        state.form.ct_code = `RWHT${new Date().getFullYear()}${Code}`
      }

      const fetchUrlData = async () => {
        state.fileUrl = await getInsertFileUrl()
      }
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
        console.log(file)
        state.pdfFile.push(file)
      }
      const pdfHandleExceed = (files) => {
        pdfUpload.value.clearFiles()
        pdfUpload.value.handleStart(files[0])
      }
      const pdfRemoveUpload = () => {
        state.form.ct_attachmentPdf = null
      }
      const pdfSubmitUpload = () => {
        pdfUpload.value.submit()
      }
      const pdfSuccessUpload = (response) => {
        state.form.ct_attachmentPdf = response.id
        if (state.form.conPdfFilesId != undefined) {
          state.form.conPdfFilesId = state.form.conPdfFilesId + response.id
        } else {
          state.form.conPdfFilesId = response.id
        }
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
        const fileSizeLimit2 = file.size / 1024 / 1024
        if (fileSizeLimit2 > 20) {
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
        state.form.ct_attachmentWord = null
      }
      const wordSubmitUpload = () => {
        wordUpload.value.submit()
      }
      const wordSuccessUpload = (response) => {
        state.form.ct_attachmentWord = response.id
        if (state.form.conWordFilesId != undefined) {
          state.form.conWordFilesId = state.form.conWordFilesId + response.id
        } else {
          state.form.conWordFilesId = response.id
        }
      }

      const DeleteFiles = async (pid, FilesUrl) => {
        $baseConfirm('你确定要删除当前项吗', null, async () => {
          const filetype = FilesUrl[0].substring(FilesUrl[0].length - 3)
          const { msg } = await DeleteFilesByPid({
            Pid: pid,
            filesType: filetype == 'pdf' ? 1 : 2,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          filetype == 'pdf'
            ? (state.form.ct_attachmentPdfUrl = [])
            : (state.form.ct_attachmentWordUrl = [])
        })
      }
      const deleteData = async () => {
        await DoDeleteCtIdZero({
          newIdsStr:
            state.form.newConPayPlanStr == ''
              ? null
              : state.form.newConPayPlanStr,
        })
      }
      const close = () => {
        if (!state.form.id) {
          deleteData()
        }
        closeDig()
      }
      const closeDig = () => {
        state['formRef'].resetFields()
        state.dynamicTags = []
        pdfUpload.value.clearFiles()
        wordUpload.value.clearFiles()
        state.form = {
          roles: [],
          ct_attachmentPdfUrl: [],
          ct_attachmentWordUrl: [],
        }
        state.dialogFormVisible = false
        emit('fetch-data')
      }

      const changeDate = (val) => {
        if (val == null) {
          return null
        }
        const newDate = new Date(val)
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return `${y}-${m}-${d}`
      }
      const save = () => {
        // var FinallySignDate = changeDate(state.form.ct_signingDate)
        // var FinallydeliDate = changeDate(state.form.ct_deliveryDate)
        // if (FinallySignDate > FinallydeliDate) {
        //   $baseMessage(
        //     '交货日期不能小于签订日期',
        //     'error',
        //     'vab-hey-message-error'
        //   )
        //   return
        // }
        state['formRef'].validate(async (valid) => {
          if (valid) {
            //console.log(state.form)
            const { msg } = state.form.id
              ? await doEdit(state.form)
              : await doAdd(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            closeDig()
          }
        })
      }
      //数量或价格值改变事件
      const handleChange = () => {
        let Num = 0
        if (state.form.proCount > 0 && state.form.proUnitPrice > 0) {
          Num = state.form.proCount * state.form.proUnitPrice
        }
        state.form.ct_cash = Num
      }
      onMounted(() => {
        //fetchData()
        fetchUrlData()
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
        changeDate,
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
        //fetchData,
        DeleteFiles,
        fetchUrlData,
        changeUpload,
        EditTheCtCode,
        ProChooseOpenDig,
        fetchListData,
        ClickQueryData,
        getRowKeys,
        setSelectRows,
        saveTabDlg,
        DigOpen,
        DigClosed,
        selectItems,
        closeTabDlg,
        handleClose,
        changeDataFun,
        PPHandleClick,
        fetchPPListData,
        PPDigOpen,
        closePPTabDlg,
        formatCurrency,
        computedTotal,
        removeClass,
        SavePPTabDlg,
        deleteData,
        closeDig,
        handleCurrentChange,
        handleSizeChange,
        handleChange,
      }
    },
    methods: {
      handleEdit() {},
      cellClick(row, column, cell, event) {
        if (column.label == '比例（%）') {
          row.edit = true
          console.log(row, column, event)
          for (
            let i = 0;
            i < document.getElementsByClassName('current-cell').length;
            i++
          ) {
            document
              .getElementsByClassName('current-cell')
              [i].classList.remove('current-cell')
          }
          cell.classList.add('current-cell')
        }
      },
    },
  })
</script>
<style lang="scss">
  .el-input--small .el-input__inner {
    width: 150px;
    text-align: center;
  }
  .el-input--small {
    width: 160px;
  }
</style>
