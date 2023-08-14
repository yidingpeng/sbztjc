<template>
  <div class="purchase-container">
    <el-page-header :content="'详情页面'" @back="goBack" />
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="物料名称">
            <el-input
              v-model.trim="queryForm.materialCode"
              clearable
              placeholder="请输入物料编码或名称"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="12">
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
          @click="handleMaterial(2)"
        >
          选择供应商
        </el-button>
        <el-button
          v-permissions="{ permission: ['purchaseDetail:txcbjg'] }"
          type="primary"
          @click="handlePurdasingEdit"
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
        <el-button
          v-permissions="{ permission: ['purchaseDetail:print'] }"
          type="primary"
          @click="handlePrint"
        >
          批量打印
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column
        align="center"
        fixed="left"
        show-overflow-tooltip
        type="selection"
      />
      <el-table-column
        align="center"
        fixed="left"
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
        width="120"
      />
      <el-table-column
        align="center"
        label="要求到货时间"
        prop="yqArrivalTime"
        show-overflow-tooltip
        width="160"
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
        label="预计到货时间"
        prop="yjArrivalTime"
        show-overflow-tooltip
        width="160"
      />
      <el-table-column
        align="center"
        label="实际到货时间"
        prop="sjArrivalTime"
        show-overflow-tooltip
        width="160"
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
        label="数量"
        prop="count"
        show-overflow-tooltip
        width="60"
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
        label="图纸代号"
        prop="drawingCode"
        show-overflow-tooltip
        width="160"
      />
      <el-table-column
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
      />
      <el-table-column
        align="center"
        label="状态"
        prop="stateText"
        show-overflow-tooltip
        width="80"
      />
      <el-table-column align="center" fixed="right" label="操作" width="150">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['purchaseDetail:print'] }"
            title="预计、实际到货时间编辑"
            :underline="false"
            @click="handlePurdasingEdit(row)"
          >
            <vab-icon icon="time-line" />
          </el-link>
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
            <el-table :data="gridData1">
              <el-table-column
                align="center"
                label="物料名称"
                property="materialName"
                width="100"
              />
              <el-table-column
                align="center"
                label="供应商"
                property="supplierText"
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
            <el-table :data="gridData">
              <el-table-column
                align="center"
                label="物料名称"
                property="materialName"
                width="100"
              />
              <el-table-column
                align="center"
                label="成本价格"
                property="costPrice"
                width="90"
              />
              <el-table-column
                align="center"
                label="供应商"
                property="supplierText"
                width="100"
              />
            </el-table>
          </el-popover>
          <el-link
            v-permissions="{ permission: ['materialinfo:print'] }"
            title="打印"
            :underline="false"
            @click="handlePrint(row)"
          >
            <vab-icon icon="printer-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['purchaseDetail:addQrcode'] }"
            title="添加二维码"
            :underline="false"
            @click="handleQrcode(row)"
          >
            <vab-icon icon="qr-code-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" />
    <OptSupEdit ref="OptSupEditRef" @fetch-data="fetchData" />
    <CostPriceEdit ref="CostPriceEditRef" @fetch-data="fetchData" />
    <edit2 ref="editRef2" @fetch-data="fetchData" />
    <edit3 ref="editRef3" @fetch-data="fetchData" />
    <edit4 ref="editRef4" @fetch-data="fetchData" />
    <edit5 ref="editRef5" @fetch-data="fetchData" />
    <edit6 ref="editRef6" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import {
    getList,
    doDelete,
    assignStaffFeed,
    QrCodeAdd,
    GetByIdQrCode,
    GetByIdList,
    GetByIdSupplier,
    GetBomDataByCode,
    GetProDataByCode,
  } from '@/api/purchase/purchase'
  import Edit from './components/PurchaseEdit'
  import OptSupEdit from './components/OptSupplier'
  import Edit2 from './components/POYQArrivalTime'
  import Edit3 from './components/POAssignStaff'
  import CostPriceEdit from './components/CostPrice'
  import Edit4 from './components/PurchasingPersonEdit'
  import Edit5 from './components/supplierTimeEdit'
  import Edit6 from './components/QrCodeEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import { getLodop } from '@/utils/lodop.js'
  import { handleActivePath } from '@/utils/routes'

  export default defineComponent({
    name: 'Purchase',
    components: {
      Edit,
      OptSupEdit,
      CostPriceEdit,
      Edit2,
      Edit3,
      Edit4,
      Edit5,
      Edit6,
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const route = useRoute()
      const router = useRouter()
      const state = reactive({
        editRef: null,
        OptSupEditRef: null,
        editRef2: null,
        editRef3: null,
        CostPriceEditRef: null,
        editRef4: null,
        editRef5: null,
        editRef6: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
          bomId: route.query.id,
        },
        bomCode: route.query.bomCode,
        bomNme: '',
        projectCode: route.query.projectCode,
        projectName: '',
        gridData: [],
        gridData1: [],
      })
      const goBack = async () => {
        const detailPath = await handleActivePath(route, true)
        await router.push('/purchase/indent')
        await delVisitedRoute(detailPath)
      }

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }

      //批量输入要求到货时间
      const handleYQArrivalTime = () => {
        if (state.selectRows.length > 0) {
          state.editRef2.showEdit(state.selectRows)
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }

      //批量输入分配人员
      const handleAssignStaff = () => {
        if (state.selectRows.length > 0) {
          state.editRef3.showEdit(state.selectRows)
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }

      //采购人员编辑预计到货时间和实际到货时间
      const handlePurdasingEdit = (row) => {
        if (row.id) {
          state.editRef4.showEdit(row)
        } else {
          if (state.selectRows.length > 0) {
            state.editRef4.showEdit(state.selectRows)
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }

      //供应商编辑预计完成时间、实际完成时间、预计到货时间
      const handleSupplierEdit = (row) => {
        if (row.id) {
          state.editRef5.showEdit(row)
        } else {
          if (state.selectRows.length > 0) {
            state.editRef5.showEdit(state.selectRows)
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }

      //手动生成二维码
      const handleQrcode = async (row) => {
        // if (row.count > 1) {
        //   //生生成多个二维码
        // }
        // state.editRef6.showEdit(
        //   row,
        //   state.bomCode + row.materialCode,
        //   state.projectCode,
        //   state.bomCode
        // )

        if (row.count > 1) {
          for (let j = 0; j < row.count; j++) {
            //保存二维码数据
            let qrData = {}
            qrData = {
              qrCode: `${state.bomCode + row.materialCode}-${j + 1}`,
              supplier: row.supplier,
              materialCode: row.materialCode,
              projectCode: state.projectCode,
              bomCode: state.bomCode,
              count: row.count,
              state: 1,
              remark: '',
              qualified: 0,
              bomId: row.bomId,
            }
            await QrCodeAdd(qrData)
          }
        } else {
          //保存二维码数据
          let qrData = {}
          qrData = {
            qrCode: state.bomCode + row.materialCode,
            supplier: row.supplier,
            materialCode: row.materialCode,
            projectCode: state.projectCode,
            bomCode: state.bomCode,
            count: row.count,
            state: 1,
            remark: '',
            qualified: 0,
            bomId: row.bomId,
          }
          await QrCodeAdd(qrData)
        }
        $baseMessage(
          `已打印完成${row.count}条二维码`,
          'success',
          'vab-hey-message-success'
        )
      }

      //发货按钮
      const handleDeliverGoods = async (row) => {
        $baseConfirm('确定发货吗?', null, async () => {
          const { msg } = await assignStaffFeed({
            bomcode: row.bomCode,
            materialcode: row.materialCode,
          })
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        })
      }

      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
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
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        list.forEach((item) => {
          item.yqArrivalTime = changeDate(item.yqArrivalTime)
          item.yjArrivalTime = changeDate(item.yjArrivalTime)
          item.sjArrivalTime = changeDate(item.sjArrivalTime)
          item.yjFinishTime = changeDate(item.yjFinishTime)
          item.sjFinishTime = changeDate(item.sjFinishTime)
          item.gysArrivalTime = changeDate(item.gysArrivalTime)
        })
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const changeDate = (val) => {
        if (!val) return ''
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
      const handleSupplier = () => {
        if (state.selectRows.length > 0) {
          const ids = state.selectRows.map((item) => item.id).join()
          state.OptSupEditRef.showEdit(ids)
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      const handleMaterial = (mid) => {
        if (state.selectRows.length > 0) {
          const ids = state.selectRows.map((item) => item.id).join()
          if (mid == 1) {
            state.OptSupEditRef.showEdit(ids)
          } else if (mid == 2) {
            state.CostPriceEditRef.showEdit(ids)
          }
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
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
      //打印方法
      const DoPrint = async (row, count, mCode) => {
        console.log(row)
        //单条数据进行标签打印Statr
        LODOP.PRINT_INIT('') //初始化
        LODOP.SET_PRINTER_INDEX('Xprinter XP-450B') //选择名称是“Xprinter XP-450B”的打印机
        //设置横向(四个参数：打印方向及纸张类型（0(或其它)：打印方向由操作者自行选择或按打印机缺省设置；
        //1：纵向打印,固定纸张；
        //2：横向打印，固定纸张；
        //3：纵向打印，宽度固定，高度按打印内容的高度自适应。），纸张宽(mm)，纸张高(mm),纸张名(必须纸张宽等于零时本参数才有效。))
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 700, '')
        LODOP.ADD_PRINT_BARCODE(0, 0, 100, 100, 'QRCode', mCode) // 条码（六个参数：Top,Left,Width,Height,BarCodeType,BarCodeValue）
        LODOP.SET_PRINT_STYLEA(0, 'QRCodeErrorLevel', 'H')
        LODOP.ADD_PRINT_TEXT(
          5,
          90,
          850,
          15,
          `物料名称:${row.materialName == null ? '' : row.materialName}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          23,
          90,
          850,
          15,
          `数\xa0\xa0\xa0\xa0量:${count}` // (j + 1) + '/' + row.count
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          38,
          90,
          850,
          15,
          `供\xa0应\xa0商:${row.supplierText == null ? '' : row.supplierText}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          53,
          90,
          850,
          15,
          `项\xa0目\xa0号:${row.projectCode == null ? '' : row.projectCode}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          68,
          90,
          850,
          15,
          `项\xa0目\xa0名:${row.projectName == null ? '' : row.projectName}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        //二维码号
        LODOP.ADD_PRINT_TEXT(85, 0, 850, 15, mCode)
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 10)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)

        // LODOP.SET_PREVIEW_WINDOW(2, 0, 0, 800, 600, '')  // 设置预览窗口模式和大小
        //LODOP.PREVIEW() // 预览。（这里可以进行预览，注意这里打开时记得把下面的print先注释。）另外，这里的预览只显示单个商品 打印出来的效果即该预览效果。
        LODOP.PRINT() // 打印
        //单条数据进行标签打印End
        const qrinfo = await GetByIdQrCode({ QrCode: mCode })
        if (qrinfo.data == null) {
          //保存二维码数据
          let qrData = {}
          qrData = {
            qrCode: mCode,
            supplier: row.supplier,
            materialCode: row.materialCode,
            projectCode: state.projectCode,
            bomCode: state.bomCode,
            count: row.count,
            state: 1,
            remark: '',
            qualified: 0,
            bomId: row.bomId,
          }
          await QrCodeAdd(qrData)
        }
      }

      //获取ID进行打印
      const handlePrint = (row) => {
        const LODOP = getLodop()
        if (LODOP) {
          if (row.materialCode) {
            $baseConfirm('确定打印当前项?', null, async () => {
              if (row.count > 1) {
                for (let j = 0; j < row.count; j++) {
                  DoPrint(
                    row,
                    `${j + 1}/${row.count}`,
                    `${state.bomCode + row.materialCode}-${j + 1}`
                  )
                }
              } else {
                DoPrint(row, row.count, state.bomCode + row.materialCode)
              }
              $baseMessage('打印完成', 'success', 'vab-hey-message-success')
            })
          } else {
            if (state.selectRows.length > 0) {
              const ids = state.selectRows.map((item) => item.materialCode)

              $baseConfirm('确定打印选中项?', null, () => {
                ids.forEach((item) => {
                  state.list.forEach(async (item1) => {
                    if (item == item1.materialCode) {
                      if (item1.count > 1) {
                        for (let i = 0; i < item1.count; i++) {
                          DoPrint(
                            item1,
                            `${i + 1}/${item1.count}`,
                            `${state.bomCode + item1.materialCode}-${i + 1}`
                          )
                        }
                      } else {
                        DoPrint(
                          item1,
                          item1.count,
                          state.bomCode + item1.materialCode
                        )
                      }
                    }
                  })
                })
                $baseMessage('打印完成', 'success', 'vab-hey-message-success')
              })
            } else {
              $baseMessage('未选中任何行!', 'error', 'vab-hey-message-error')
            }
          }
        }
      }

      //根据项目编号获取项目名称
      const getProData = async () => {
        const projectData = await GetProDataByCode({
          proCode: state.projectCode,
        })
        state.projectName =
          projectData.data == null ? '' : projectData.data.projectName
      }

      //根据bom编号获取bom名称
      const getBomData = async () => {
        const bomData = await GetBomDataByCode({ bomCode: state.bomCode })
        state.bomNme = bomData.data == null ? '' : bomData.data.bomNme
      }

      onMounted(() => {
        fetchData()
        getProData()
        getBomData()
      })

      return {
        ...toRefs(state),
        getProData,
        getBomData,
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleSupplier,
        handleYQArrivalTime,
        handleAssignStaff,
        handlePurdasingEdit,
        handleSupplierEdit,
        handleQrcode,
        handleDeliverGoods,
        handleMaterial,
        handlePrint,
        goBack,
        PriceClick,
        SupplierClick,
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
</style>
