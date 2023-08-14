<template>
  <div class="fifomanage-container">
    <vab-query-form>
      <vab-query-form-top-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="二维码号">
            <el-input
              v-model.trim="queryForm.qrCode"
              clearable
              placeholder="请输入二维码号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="物料名称">
            <el-input
              v-model.trim="queryForm.materialCode"
              clearable
              placeholder="请输入物料编码"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目编号">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          <!-- &nbsp;
          <el-form-item label="供应商">
            <el-input
              v-model.trim="queryForm.Supplier"
              clearable
              placeholder="请输入供应商名称"
            />
          </el-form-item> -->
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleDataDerive">导出</el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel>
        <!-- <el-button
          v-permissions="{ permission: ['qrcode:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button> -->
        <el-button
          v-permissions="{ permission: ['qrcode:print'] }"
          type="primary"
          @click="handlePLPrint"
        >
          <vab-icon icon="printer-line" />
          批量打印
        </el-button>
        <el-button
          v-permissions="{ permission: ['qrcode:cd'] }"
          type="primary"
          @click="handlePLCD"
        >
          批量报检
        </el-button>
        <el-popover placement="bottom" trigger="hover" width="650px">
          <template #reference>
            <el-link :icon="Warning" type="danger" :underline="false">
              注明
            </el-link>
          </template>
          <template #default>
            <el-alert
              title="批量打印:不管数量是多少都只打印一个二维码标签,需要数量转换的请勿包含在批量打印中。"
              type="error"
            />
            <el-alert
              title="数量转换公式:总数量 / 转换数量 = 标签个数(向上取整)。"
              type="error"
            />
            <el-alert
              title="批量报检:已经存在报检记录的会进行自动忽略。"
              type="error"
            />
          </template>
        </el-popover>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="二维码号"
        prop="qrCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="BOM编码"
        prop="bomCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料编码"
        prop="materialCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="materialName"
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
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量"
        prop="count"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="供应商"
        prop="supName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="状态"
        prop="state"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作">
        <template #default="{ row }">
          <!-- <el-button
            v-permissions="{ permission: ['qrcode:edit'] }"
            link type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['qrcode:delete'] }"
            link type="primary"
            @click="handleDelete(row)"
          >
            删除
          </el-button> -->
          <el-link
            v-permissions="{ permission: ['qrcode:print'] }"
            :underline="false"
            @click="PrintLabel(row)"
          >
            打印
          </el-link>
          <el-link
            v-permissions="{ permission: ['qrcode:cd'] }"
            :underline="false"
            @click="handleCD(row)"
          >
            报检
          </el-link>
          <el-link
            v-permissions="{ permission: ['qrcode:qc'] }"
            :underline="false"
            @click="handleQC(row)"
          >
            质检
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
    <qc-edit ref="qceditRef" @fetch-data="fetchData" />
    <cd-edit ref="cdeditRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import {
    QrPageList,
    QrDelete,
    PrintQrCodeList,
    GetByQrCodeQC,
    GetByQrCodeCD,
    PLCDAdd,
  } from '@/api/purchase/purchase'
  import Edit from './components/QrCodeEdit'
  import QcEdit from './components/qcEdit.vue'
  import CdEdit from './components/cdEdit.vue'
  import { Delete, Plus, Search, Warning } from '@element-plus/icons-vue'
  import { getLodop } from '@/utils/lodop.js'

  export default defineComponent({
    name: 'QrCode',
    components: { Edit, QcEdit, CdEdit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        qceditRef: null,
        cdeditRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          title: '',
        },
      })

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
      //批量打印
      const handlePLPrint = () => {
        if (state.selectRows.length > 0) {
          state.selectRows.forEach((item) => {
            DoPrint(item, item.count)
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await QrDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await QrDelete({ ids })
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
        } = await QrPageList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      //数据导出
      const handleDataDerive = async () => {
        $baseConfirm('确定要进行导出操作吗？', null, async () => {
          const dbloading = ElLoading.service({
            lock: true,
            text: '数据导出中...',
            background: 'rgba(0, 0, 0, 0.7)',
          })
          const list = await PrintQrCodeList(state.queryForm)
          import('~/src/utils/excel').then((excel) => {
            const tHeader = [
              '项目编号',
              '项目名称',
              '物料编码',
              '物料名称',
              '二维码号',
              '数量',
              '供应商',
            ]
            const filterVal = [
              'projectCode',
              'projectName',
              'materialCode',
              'materialName',
              'qrCode',
              'count',
              'supName',
            ]
            const data = formatJson(filterVal, list.data)
            excel.export_json_to_excel({
              header: tHeader,
              data,
              filename: '二维码数据列表',
              autoWidth: true,
              bookType: 'xlsx',
            })
          })
          dbloading.close()
        })
      }

      const formatJson = (filterVal, jsonData) => {
        return jsonData.map((v) =>
          filterVal.map((j) => {
            return v[j]
          })
        )
      }
      //PrintLabel
      const PrintLabel = (row) => {
        const LODOP = getLodop()
        if (LODOP) {
          if (row.count > 1) {
            ElMessageBox.prompt(
              '是否进行换算?(填写每单位可装数量)',
              `${row.materialName}（ 数量：${row.count} ）`,
              {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                inputPattern: /^[1-9]\d*$/,
                inputErrorMessage: '请填写数量值',
                distinguishCancelAndClose: true, // 重要，设置为true才会把右上角X和取消区分开来
              }
            )
              .then(({ value }) => {
                //确认
                const hsCount = Math.ceil(row.count / value)
                for (let i = 0; i < hsCount; i++) {
                  const tolc = i + 1 == hsCount ? row.count - i * value : value
                  DoPrint(row, `${tolc}/${row.count}`)
                }
              })
              .catch((e) => {
                if (e == 'cancel') {
                  DoPrint(row, row.count)
                }
              })
          } else {
            DoPrint(row, row.count)
          }
        }
      }
      //打印方法
      const DoPrint = (row, count) => {
        const InitialHeight = 95 //文字初始高度
        //单条数据进行标签打印Statr
        LODOP.PRINT_INIT('') //初始化
        //LODOP.SET_PRINTER_INDEX('Xprinter XP-450B') //选择名称是“Xprinter XP-450B”的打印机
        LODOP.SET_PRINTER_INDEX('ZDesigner ZD888-203dpi ZPL') //选择名称是“ZDesigner ZD888-203dpi ZPL”的打印机
        //设置横向(四个参数：打印方向及纸张类型（0(或其它)：打印方向由操作者自行选择或按打印机缺省设置；
        //1：纵向打印,固定纸张；
        //2：横向打印，固定纸张；
        //3：纵向打印，宽度固定，高度按打印内容的高度自适应。），纸张宽(mm)，纸张高(mm),纸张名(必须纸张宽等于零时本参数才有效。))
        LODOP.SET_PRINT_PAGESIZE(1, 500, 500, '')
        LODOP.ADD_PRINT_BARCODE(15, 5, 100, 100, 'QRCode', row.qrCode) // 条码（六个参数：Top,Left,Width,Height,BarCodeType,BarCodeValue）
        LODOP.SET_PRINT_STYLEA(0, 'QRCodeErrorLevel', 'H')
        LODOP.ADD_PRINT_TEXT(
          InitialHeight,
          5,
          500,
          15,
          `物料名:${row.materialName == null ? '' : row.materialName}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 8)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          InitialHeight + 1 * 15,
          5,
          500,
          15,
          `数\xa0\xa0量:${count}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 8)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          InitialHeight + 2 * 15,
          5,
          500,
          15,
          `供应商:${row.supName == null ? '' : row.supName}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 8)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          InitialHeight + 3 * 15,
          5,
          500,
          15,
          `项目号:${row.projectCode == null ? '' : row.projectCode}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 8)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
        LODOP.ADD_PRINT_TEXT(
          InitialHeight + 4 * 15,
          5,
          500,
          15,
          `项目名:${row.projectName == null ? '' : row.projectName}`
        )
        LODOP.SET_PRINT_STYLEA(0, 'FontSize', 8)
        LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)

        // LODOP.SET_PREVIEW_WINDOW(2, 0, 0, 800, 600, '')  // 设置预览窗口模式和大小
        //LODOP.PREVIEW() // 预览。（这里可以进行预览，注意这里打开时记得把下面的print先注释。）另外，这里的预览只显示单个商品 打印出来的效果即该预览效果。
        LODOP.PRINT() // 打印
      }
      //报检
      const handleCD = async (row) => {
        const entity = await GetByQrCodeCD({ QrCode: row.qrCode })
        //console.log(entity.data)
        if (entity.data == null) {
          state.cdeditRef.showEdit(row)
        } else {
          $baseMessage('物料已报检!', 'error', 'vab-hey-message-error')
        }
      }
      //批量报检
      const handlePLCD = async () => {
        const cdData = []
        if (state.selectRows.length > 0) {
          //console.log(state.selectRows)
          $baseConfirm('确定要批量报检吗?', null, async () => {
            state.selectRows.forEach((item) => {
              cdData.push({
                qrCode: item.qrCode,
                remark: item.remark,
              })
            })
            const { msg } = await PLCDAdd(cdData)
            ElMessage({
              type: msg.includes('成功') ? 'success' : 'error',
              message: msg,
            })
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      //质检
      const handleQC = async (row) => {
        if (row.qualified == 1) {
          $baseMessage('物料已质检!', 'success', 'vab-hey-message-success')
        } else {
          const data = await GetByQrCodeQC({ QrCode: row.qrCode })
          const entity = await GetByQrCodeCD({ QrCode: row.qrCode })
          if (entity.data != null) {
            if (data.data != null) {
              if (data.data.qCount < row.count) {
                state.qceditRef.showEdit(row)
              } else {
                $baseMessage(
                  '物料已质检!',
                  'success',
                  'vab-hey-message-success'
                )
              }
            } else {
              state.qceditRef.showEdit(row)
            }
          } else {
            ElMessage({
              type: 'error',
              message: '请先完成报检!',
            })
          }
        }
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleDataDerive,
        formatJson,
        PrintLabel,
        DoPrint,
        handlePLPrint,
        handleCD,
        handleQC,
        handlePLCD,
        Delete,
        Plus,
        Search,
        Warning,
      }
    },
  })
</script>
