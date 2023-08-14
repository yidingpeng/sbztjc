<template>
  <div class="indent-container">
    <el-divider content-position="left">BOM订单</el-divider>
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="项目名称">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="项目模糊搜索"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="申 请 人">
            <el-input
              v-model.trim="queryForm.applicant"
              clearable
              placeholder="请输入申请人"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="申请日期">
            <el-date-picker
              v-model="queryForm.applicationDate"
              clearable
              :disabled-date="disabledDate"
              placeholder="请选择申请日期"
              type="date"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <!-- <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['indent:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['indent:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel> -->
    </vab-query-form>
    <el-table v-loading="listLoading" border :data="list">
      <!-- <el-table-column
        align="center"
        fixed="left"
        show-overflow-tooltip
        type="selection"
      /> -->
      <el-table-column
        align="center"
        fixed="left"
        label="项目编码"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="图纸代号"
        prop="drawingCode"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="申请人员"
        prop="applicant"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="申请日期"
        prop="applicationDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="齐套率"
        prop="completeSetRate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="当前版本号"
        prop="currentVersion"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="状态"
        prop="status"
        show-overflow-tooltip
      /> -->
      <el-table-column align="center" fixed="right" label="操作" width="80">
        <template #default="{ row }">
          <el-link
            title="明细"
            :underline="false"
            @click="setdblclickRows(row)"
          >
            <vab-icon icon="book-read-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['purchaseDetail:addQrcode'] }"
            title="生成二维码"
            :underline="false"
            @click="handleBatchGeneration(row)"
          >
            <vab-icon icon="qr-code-line" />
          </el-link>
          <!-- <el-link
            v-permissions="{ permission: ['indent:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['indent:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link> -->
        </template>
      </el-table-column>
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
    <!-- <edit ref="editRef" @fetch-data="fetchData" /> -->
    <el-divider content-position="left">{{ bomProName }} 订单明细</el-divider>
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['purchaseDetail:yqdhsj'] }"
          type="primary"
          @click="handleYQArrivalTime"
        >
          要求到货时间
        </el-button>
        <el-button
          v-permissions="{ permission: ['purchaseDetail:fpcgry'] }"
          type="primary"
          @click="handleAssignStaff"
        >
          分配采购人员
        </el-button>
        <el-button
          v-permissions="{ permission: ['purchaseDetail:xzgys'] }"
          type="primary"
          @click="handleMaterial(1)"
        >
          选择供应商
        </el-button>
        <el-button
          v-permissions="{ permission: ['purchaseDetail:txcbjg'] }"
          type="primary"
          @click="handleMaterial(2)"
        >
          填写成本价格
        </el-button>
        <el-button
          v-permissions="{ permission: ['purchaseDetail:yjsjdhsj'] }"
          type="primary"
          @click="handlePurdasingEdit"
        >
          预计、实际到货时间
        </el-button>
        <!-- <el-button
          v-permissions="{ permission: ['purchaseDetail:print'] }"
          type="primary"
          @click="handlePrint"
        >
          批量打印
        </el-button> -->
        <!-- <el-button
          v-permissions="{ permission: ['purchaseDetail:addQrcode'] }"
          type="primary"
          @click="handleBatchGeneration"
        >
          一键生成二维码
        </el-button> -->
      </vab-query-form-left-panel>
    </vab-query-form>
    <!-- <el-tabs v-model="activeName" type="card" @tab-click="handleTabsClick">
      <el-tab-pane label="标准件" name="TechnicalStandard">标准件</el-tab-pane>
      <el-tab-pane label="外协件" name="Outsourcing">外协件</el-tab-pane>
    </el-tabs> -->
    <el-button-group>
      <el-button
        :type="groupType == false ? 'primary' : ''"
        @click="handleGroupClick('BZ')"
      >
        标准件
      </el-button>
      <el-button
        :type="groupType == true ? 'primary' : ''"
        @click="handleGroupClick('WX')"
      >
        外协件
      </el-button>
    </el-button-group>
    <el-table
      ref="multipleTable"
      v-loading="listLoading2"
      border
      :data="Sonlist"
      row-key="id"
      :scrollbar-always-on="true"
      @select="select"
      @select-all="selectAll"
      @selection-change="setSelectRows2"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column label="#" type="expand">
        <template #default="props">
          <el-descriptions :column="8">
            <el-descriptions-item label="材质：">
              {{ props.row.materialQuality }}
            </el-descriptions-item>
            <el-descriptions-item label="品牌：">
              {{ props.row.brand }}
            </el-descriptions-item>
            <el-descriptions-item label="单位：">
              {{ props.row.unit }}
            </el-descriptions-item>
            <el-descriptions-item label="重量：">
              {{ props.row.weight }}
            </el-descriptions-item>
            <el-descriptions-item label="单价：">
              {{ props.row.unitPrice }}
            </el-descriptions-item>
            <el-descriptions-item label="代号：">
              {{ props.row.designation }}
            </el-descriptions-item>
            <el-descriptions-item label="送检单位：">
              {{ props.row.submittedUnit }}
            </el-descriptions-item>
            <el-descriptions-item label="下发时间：">
              {{ props.row.distributionTime }}
            </el-descriptions-item>
            <el-descriptions-item label="规格型号：" :span="2">
              {{ props.row.specification }}
            </el-descriptions-item>
            <el-descriptions-item label="技术参数：" :span="3">
              {{ props.row.technicalParameters }}
            </el-descriptions-item>
            <el-descriptions-item label="备注：" :span="3">
              {{ props.row.remark }}
            </el-descriptions-item>
          </el-descriptions>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="物料编码"
        prop="materialCode"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="materialName"
        show-overflow-tooltip
        width="160"
      />
      <!-- <el-table-column
        align="center"
        label="代号"
        prop="designation"
        show-overflow-tooltip
        width="180"
      />
      <el-table-column
        align="center"
        label="规格型号"
        prop="specification"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="技术参数"
        prop="technicalParameters"
        show-overflow-tooltip
        width="120"
      /> -->
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="要求到货时间"
        prop="yqArrivalTime"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="采购人员"
        prop="assignStaffTxt"
        show-overflow-tooltip
        width="90"
      />
      <el-table-column
        align="center"
        label="数量"
        prop="count"
        show-overflow-tooltip
        width="60"
      />
      <el-table-column
        align="center"
        label="金额"
        prop="amount"
        show-overflow-tooltip
        width="90"
      />
      <el-table-column
        align="center"
        label="成本价格"
        prop="costPrice"
        show-overflow-tooltip
        width="90"
      />
      <el-table-column
        align="center"
        label="供应商"
        prop="supplierText"
        show-overflow-tooltip
        width="140"
      />
      <el-table-column
        align="center"
        label="物料来源"
        prop="drawingCode"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="预计到货时间"
        prop="yjArrivalTime"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="实际到货时间"
        prop="sjArrivalTime"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="质检结果"
        prop="qcResult"
        width="100"
      >
        <template #default="{ row }">
          <el-tag v-if="row.qcResult == '0' || row.qcResult == ''" type="info">
            未质检
          </el-tag>
          <el-tag v-else-if="row.qcResult == '1'" type="success">合格</el-tag>
          <el-tag v-else-if="row.qcResult == '2'" type="danger">不合格</el-tag>
        </template>
      </el-table-column>
      <!--<el-table-column
        align="center"
        label="预计完成时间"
        prop="yjFinishTime"
        show-overflow-tooltip
        width="160"
      />
      <el-table-column
        align="center"
        label="实际完成时间"
        prop="sjFinishTime"
        show-overflow-tooltip
        width="160"
      />
      <el-table-column
        align="center"
        label="预计入库时间"
        prop="gysArrivalTime"
        show-overflow-tooltip
        width="160"
      /> -->
      <!-- <el-table-column
        align="center"
        label="状态"
        prop="stateText"
        show-overflow-tooltip
        width="80"
      /> -->
      <el-table-column align="center" label="操作">
        <template #default="{ row }">
          <el-link
            v-if="row.isQrCode == false"
            v-permissions="{ permission: ['purchaseDetail:addQrcode'] }"
            title="添加二维码"
            :underline="false"
            @click="handleQrcode(row)"
          >
            <vab-icon icon="qr-code-line" />
          </el-link>
          <!-- <el-link
            v-permissions="{ permission: ['purchaseDetail:yjsjdhsj'] }"
            title="预计、实际到货时间编辑"
            :underline="false"
            @click="handlePurdasingEdit(row)"
          >
            <vab-icon icon="time-line" />
          </el-link> -->
          <el-popover placement="left" trigger="click" :width="200">
            <template #reference>
              <el-link
                v-permissions="{ permission: ['purchaseDetail:zjgys'] }"
                title="最近供应商"
                :underline="false"
                @click="SupplierClick(row.id)"
              >
                供
              </el-link>
            </template>
            <el-table>
              <el-table-column
                align="center"
                label="物料名称"
                prop="materialName"
                width="100"
              />
              <el-table-column
                align="center"
                label="供应商"
                prop="supplierText"
                width="100"
              />
            </el-table>
          </el-popover>
          <el-popover placement="left" trigger="click" :width="300">
            <template #reference>
              <el-link
                v-permissions="{ permission: ['purchaseDetail:lsjg'] }"
                title="历史价格"
                :underline="false"
                @click="PriceClick(row.id)"
              >
                价
              </el-link>
            </template>
            <el-table>
              <el-table-column
                align="center"
                label="物料名称"
                prop="materialName"
                width="100"
              />
              <el-table-column
                align="center"
                label="成本价格"
                prop="costPrice"
                width="90"
              />
              <el-table-column
                align="center"
                label="供应商"
                prop="supplierText"
                width="100"
              />
            </el-table>
          </el-popover>
          <!-- <el-link
            v-permissions="{ permission: ['materialinfo:print'] }"
            title="打印"
            :underline="false"
            @click="handlePrint(row)"
          >
            <vab-icon icon="printer-line" />
          </el-link> -->
        </template>
      </el-table-column>
    </el-table>
  </div>
  <OptSupEdit ref="OptSupEditRef" @fetch-data="fetchSonData" />
  <CostPriceEdit ref="CostPriceEditRef" @fetch-data="fetchSonData" />
  <edit2 ref="editRef2" @fetch-data="fetchSonData" />
  <edit3 ref="editRef3" @fetch-data="fetchSonData" />
  <edit4 ref="editRef4" @fetch-data="fetchSonData" />
  <edit5 ref="editRef5" @fetch-data="fetchSonData" />
  <edit6 ref="editRef6" @fetch-data="fetchSonData" />
</template>

<script>
  import {
    IMPageList,
    //IMdoDelete,
    GetOperateList,
    //GetByIdQrCode,
    QrCodeAdd,
    QrCodeAddPL,
    GetByIdList,
    GetByIdSupplier,
  } from '@/api/purchase/purchase'
  //import { GetByCode } from '@/api/materialMaterialInfo'
  //import arrayToTree from 'array-to-tree'
  import OptSupEdit from './components/OptSupplier'
  import Edit2 from './components/POYQArrivalTime'
  import Edit3 from './components/POAssignStaff'
  import CostPriceEdit from './components/CostPrice'
  import Edit4 from './components/PurchasingPersonEdit'
  import Edit5 from './components/supplierTimeEdit'
  import Edit6 from './components/QrCodeEdit'
  //import { getLodop } from '@/utils/lodop.js'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import moment from 'moment'
  //import stautsTag from '@/plugins/BOMStatusTag/index'

  export default defineComponent({
    name: 'Purchase',
    components: {
      OptSupEdit,
      CostPriceEdit,
      Edit2,
      Edit3,
      Edit4,
      Edit5,
      Edit6,
      //stautsTag,
    },
    setup() {
      //const $baseConfirm = inject('$baseConfirm')
      //const $baseMessage = inject('$baseMessage')
      //const router = useRouter()
      const state = reactive({
        multipleTable: null,
        OptSupEditRef: null,
        editRef2: null,
        editRef3: null,
        CostPriceEditRef: null,
        editRef4: null,
        editRef5: null,
        editRef6: null,
        list: [],
        Sonlist: [],
        Standardlist: [],
        Outsourcinglist: [],
        initialList: [],
        listLoading: true,
        listLoading2: false,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        selectRows2: '',
        row: null,
        bomCode: '',
        bomProName: '',
        proCode: '',
        proName: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
        hes: 26,
        checkedKeys: false,
        activeName: 'TechnicalStandard',
        groupType: false,
        disabledDate(time) {
          return time.getTime() > Date.now()
        },
      })
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const setSelectRows2 = (val) => {
        state.selectRows2 = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }
      //删除
      // const handleDelete = (row) => {
      //   if (row.id) {
      //     $baseConfirm('你确定要删除当前项吗', null, async () => {
      //       const { msg } = await IMdoDelete({ ids: row.id })
      //       $baseMessage(msg, 'success', 'vab-hey-message-success')
      //       await fetchData()
      //     })
      //   } else {
      //     if (state.selectRows.length > 0) {
      //       const ids = state.selectRows.map((item) => item.id).join()
      //       $baseConfirm('你确定要删除选中项吗', null, async () => {
      //         const { msg } = await IMdoDelete({ ids })
      //         $baseMessage(msg, 'success', 'vab-hey-message-success')
      //         await fetchData()
      //       })
      //     } else {
      //       $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
      //     }
      //   }
      // }
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        if (state.queryForm.projectCode == '') {
          delete state.queryForm.projectCode
        }
        if (state.queryForm.applicant == '') {
          delete state.queryForm.applicant
        }
        if (state.queryForm.applicationDate == '') {
          delete state.queryForm.applicationDate
        }
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await IMPageList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const PriceClick = async (id) => {
        const data = await GetByIdList({
          Id: id,
        })
        state.gridData = data.data
      }
      const SupplierClick = async (id) => {
        const data = await GetByIdSupplier({
          Id: id,
        })
        state.gridData1 = data.data
      }
      //批量输入要求到货时间
      const handleYQArrivalTime = () => {
        if (state.selectRows2.length > 0) {
          state.editRef2.showEdit(state.selectRows2)
        } else {
          ElMessage({
            type: 'error',
            message: '未选中任何行!',
          })
        }
      }

      //批量输入分配人员
      const handleAssignStaff = () => {
        console.log(state.selectRows2.length)
        if (state.selectRows2.length > 0) {
          state.editRef3.showEdit(
            state.selectRows2,
            state.proCode,
            state.proName
          )
        } else {
          ElMessage({
            type: 'error',
            message: '未选中任何行!',
          })
        }
      }

      //采购人员编辑预计到货时间和实际到货时间
      const handlePurdasingEdit = (row) => {
        if (row.id) {
          state.editRef4.showEdit(row)
        } else {
          if (state.selectRows2.length > 0) {
            state.editRef4.showEdit(state.selectRows2)
          } else {
            ElMessage({
              type: 'error',
              message: '未选中任何行!',
            })
          }
        }
      }
      //选择供应商
      const handleSupplier = () => {
        //console.log(state.selectRows2.length)
        if (state.selectRows2.length > 0) {
          const ids = state.selectRows2.map((item) => item.id).join()
          state.OptSupEditRef.showEdit(ids)
        } else {
          ElMessage({
            type: 'error',
            message: '未选中任何行!',
          })
        }
      }
      //选择供应商
      const handleMaterial = (mid) => {
        console.log(state.selectRows2.length)
        if (state.selectRows2.length > 0) {
          const ids = state.selectRows2.map((item) => item.id).join()
          if (mid == 1) {
            state.OptSupEditRef.showEdit(ids)
          } else if (mid == 2) {
            state.CostPriceEditRef.showEdit(ids)
          }
        } else {
          ElMessage({
            type: 'error',
            message: '未选中任何行!',
          })
        }
      }
      //供应商编辑预计完成时间、实际完成时间、预计到货时间
      const handleSupplierEdit = (row) => {
        if (row.id) {
          state.editRef5.showEdit(row)
        } else {
          if (state.selectRows2.length > 0) {
            state.editRef5.showEdit(state.selectRows2)
          } else {
            ElMessage({
              type: 'error',
              message: '未选中任何行!',
            })
          }
        }
      }

      //手动生成二维码
      const handleQrcode = async (row) => {
        if (row.supplier != null) {
          //保存二维码数据
          let qrData = {}
          qrData = {
            qrCode: row.materialCode + row.id,
            supplier: row.supplier,
            materialCode: row.materialCode,
            materialName: row.materialName,
            projectCode: state.proCode,
            projectName: state.proName,
            bomCode: state.bomCode,
            count: row.count,
            state: 1,
            remark: '',
            qualified: 0,
            bomId: row.bomId,
          }
          const { msg } = await QrCodeAdd(qrData)
          ElMessage({
            type: msg.includes('成功') ? 'success' : 'error',
            message: msg,
          })
        } else {
          ElMessage({
            type: 'error',
            message: '请先选择供应商!',
          })
        }
      }
      //双击订单表行的点击事件
      const setdblclickRows = async (row) => {
        state.listLoading2 = true
        state.row = row
        state.bomCode = row.bomCode
        state.bomProName = `${row.projectName}(${row.projectCode})`
        state.proCode = row.projectCode
        state.proName = row.projectName
        state.groupType = false
        const list = await GetOperateList(state.row)
        if (list.data.length > 0) {
          state.Sonlist = []
          state.Standardlist = []
          state.Outsourcinglist = []
          state.initialList = list.data
          state.initialList.forEach((item) => {
            if (item.matSource == '标准件') {
              state.Standardlist.push(item) //标准件
            } else {
              state.Outsourcinglist.push(item) //外协件
            }
          })
        }
        state.Sonlist = state.Standardlist
        //生成树形数据
        // const treedata = arrayToTree(list.data, {
        //   parentProperty: 'parentCode',
        //   customID: 'materialCode',
        // })
        console.log(list.data)
        //state.Sonlist = treedata
        state.listLoading2 = false
      }
      //修改数据后刷新子集表数据
      const fetchSonData = async () => {
        state.listLoading2 = true
        const list = await GetOperateList(state.row)
        //层级数据
        // const treedata = arrayToTree(list.data, {
        //   parentProperty: 'parentCode',
        //   customID: 'materialCode',
        // })
        //state.Sonlist = treedata
        //state.initialList = list.data
        if (list.data.length > 0) {
          state.Standardlist = []
          state.Outsourcinglist = []
          state.initialList = list.data
          state.initialList.forEach((item) => {
            if (item.matSource == '标准件') {
              state.Standardlist.push(item) //标准件
            } else {
              state.Outsourcinglist.push(item) //外协件
            }
          })

          if (state.groupType) state.Sonlist = state.Outsourcinglist
          else state.Sonlist = state.Standardlist
        }
        state.listLoading2 = false
      }
      //批量生成二維碼
      const handleBatchGeneration = async (row) => {
        if (state.initialList.length > 0) {
          if (row.bomCode == state.bomCode) {
            let isSup = false
            for (let i = 0; i < state.initialList.length; i++) {
              if (
                state.initialList[i].isDrawing == false &&
                state.initialList[i].supplier == null
              ) {
                isSup = true
                break
              }
            }
            if (isSup) {
              ElMessage({
                type: 'error',
                message: '包含无供应商的数据!',
              })
            } else {
              const loading = ElLoading.service({
                lock: true,
                text: 'Loading',
                background: 'rgba(0, 0, 0, 0.7)',
              })
              const { msg } = await QrCodeAddPL(row)
              loading.close()
              ElMessage({
                type: msg.includes('成功') ? 'success' : 'error',
                message: msg,
              })
            }
          } else {
            ElMessage({
              type: 'error',
              message: '操作的订单和订单明细不一致!',
            })
          }
          //const ids = state.selectRows2.map((item) => item.id)
        } else {
          ElMessage({
            type: 'error',
            message: '请先加载订单明细数据!',
          })
        }
      }
      //全选/取消
      const selectAll = (selection) => {
        //第一层只要有在selection里面就是全选
        const isSelect = selection.some((el) => {
          const tableDataIds = state.Sonlist.map((j) => j.id)
          return tableDataIds.includes(el.id)
        })
        //第一层只要有不在selection里面就是全不选
        const isCancel = !state.Sonlist.every((el) => {
          const selectIds = selection.map((j) => j.id)
          return selectIds.includes(el.id)
        })
        if (isSelect) {
          selection.map((el) => {
            if (el.children) {
              // 解决子组件没有被勾选到
              setChildren(el.children, true)
            }
          })
        }
        if (isCancel) {
          state.Sonlist.map((el) => {
            if (el.children) {
              // 解决子组件没有被勾选到
              setChildren(el.children, false)
            }
          })
        }
      }
      const setChildren = (children, type) => {
        // 编辑多个子层级
        children.map((j) => {
          toggleSelection(j, type)
          if (j.children) {
            setChildren(j.children, type)
          }
        })
      }
      //当用户手动勾选数据行的 Checkbox 时触发的事件
      const select = (selection, row) => {
        const hasSelect = selection.some((el) => {
          return row.id === el.id
        })
        if (hasSelect) {
          if (row.children) {
            // 解决子组件没有被勾选到
            setChildren(row.children, true)
          }
        } else {
          if (row.children) {
            setChildren(row.children, false)
          }
        }
      }
      const toggleSelection = (row, select) => {
        if (row) {
          nextTick(() => {
            state['multipleTable'] &&
              state['multipleTable'].toggleRowSelection(row, select)
          })
        }
      }

      const dateFormat = (row, column) => {
        let date = null
        if (column.property == 'applicationDate') {
          date = row.applicationDate
        } else if (column.property == 'yqArrivalTime') {
          date = row.yqArrivalTime
        } else if (column.property == 'yjArrivalTime') {
          date = row.yjArrivalTime
        } else if (column.property == 'sjArrivalTime') {
          date = row.sjArrivalTime
        }
        if (date == null) {
          return ''
        }
        return moment(new Date(date)).format('YYYY-MM-DD')
      }
      const handleGroupClick = (name) => {
        if (name == 'BZ') {
          state.groupType = false
          state.Sonlist = state.Standardlist
        } else if (name == 'WX') {
          state.groupType = true
          state.Sonlist = state.Outsourcinglist
        }
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        setSelectRows2,
        handleEdit,
        //handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        setdblclickRows,
        GetOperateList,
        handleYQArrivalTime,
        handleAssignStaff,
        handlePurdasingEdit,
        handleSupplierEdit,
        handleQrcode,
        handleSupplier,
        handleMaterial,
        fetchSonData,
        //handlePrint,
        handleBatchGeneration,
        selectAll,
        //handleSelectionChange,
        select,
        setChildren,
        toggleSelection,
        PriceClick,
        SupplierClick,
        dateFormat,
        handleGroupClick,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
<style lang="scss">
  html body .el-scrollbar__thumb:hover,
  html body[class*='vab-theme-'] .el-scrollbar__thumb:hover {
    background-color: #252525;
  }
  html body .el-scrollbar__thumb,
  html body[class*='vab-theme-'] .el-scrollbar__thumb {
    background-color: #424242;
  }
  .el-scrollbar__bar.is-horizontal {
    height: 12px;
  }
</style>
