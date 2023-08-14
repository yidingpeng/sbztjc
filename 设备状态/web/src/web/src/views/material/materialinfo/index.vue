<template>
  <div class="material-material-info-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="物料编号" style="width: 320px">
            <el-input
              v-model.trim="queryForm.code"
              clearable
              placeholder="请输入物料编号"
              style="width: 212px"
            />
            <!-- <el-input placeholder="请输入物料编码" style="width: 212px" /> -->
            <el-tooltip effect="light" placement="right" trigger="click">
              <template #content>
                <el-tree
                  ref="treeRef"
                  class="filter-tree"
                  :data="MaterialSortOption"
                  highlight-current
                  lazy
                  :load="lazyLoad"
                  node-key="crid"
                  :props="{ children: 'children', label: 'dname' }"
                  @node-click="handleNodeClick"
                />
              </template>
              <el-button
                size="small"
                style="
                  height: 30px;
                  margin-top: -3px;
                  margin-left: 2px;
                  background: rgb(223 225 229);
                "
              >
                >
              </el-button>
            </el-tooltip>
          </el-form-item>
          &nbsp;
          <el-form-item label="物料名称">
            <el-input
              v-model.trim="queryForm.name"
              clearable
              placeholder="请输入物料名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="物料代号">
            <el-input
              v-model.trim="queryForm.alias"
              clearable
              placeholder="请输入物料代号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="物料来源" prop="source">
            <el-select
              v-model="queryForm.source"
              clearable
              placeholder="选择物料来源"
            >
              <el-option label="自制件" value="自制件" />
              <el-option label="外协件" value="外协件" />
              <el-option label="外购件" value="外购件" />
              <el-option label="定制件" value="定制件" />
              <el-option label="标准件" value="标准件" />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="当前状态" prop="codingStatus">
            <el-select
              v-model="queryForm.codingStatus"
              clearable
              placeholder="选择当前状态"
            >
              <el-option label="已编码" value="已编码" />
              <el-option label="提交审核" value="提交审核" />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item v-show="!fold" label="重 要 度" prop="important">
            <el-select
              v-model="queryForm.important"
              clearable
              placeholder="选择重要度"
            >
              <el-option label="一般件" value="一般件" />
              <el-option label="重要件" value="重要件" />
              <el-option label="关键件" value="关键件" />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item>
            <el-button
              :icon="Search"
              native-type="submit"
              type="primary"
              @click="queryData"
            >
              查询
            </el-button>
            <el-button link type="primary" @click="handleFold">
              <span v-if="fold">展开</span>
              <span v-else>合并</span>
              <vab-icon
                class="vab-dropdown"
                :class="{ 'vab-dropdown-active': fold }"
                icon="arrow-up-s-line"
              />
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-top-panel>
      <vab-query-form-left-panel :span="24">
        <el-button
          v-permissions="{ permission: ['materialinfo:print'] }"
          type="primary"
          @click="handlePrint"
        >
          <vab-icon icon="printer-line" />
          &nbsp;批量打印
        </el-button>
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
        label="物料编码"
        prop="code"
        show-overflow-tooltip
        width="110px"
      />
      <el-table-column
        align="center"
        label="物料名称"
        prop="name"
        show-overflow-tooltip
        width="110px"
      />
      <el-table-column
        align="center"
        label="代号"
        prop="alias"
        show-overflow-tooltip
        width="110px"
      />
      <el-table-column
        align="center"
        label="规格型号"
        prop="model"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="技术参数"
        prop="specification"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="材质"
        prop="material"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="规则路径"
        prop="categoryPath"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料来源"
        prop="source"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="基本单位"
        prop="basicUnit"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="当前状态"
        prop="codingStatus"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="重量"
        prop="weight"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="重量单位"
        prop="weightUnit"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="参考价格"
        prop="refPrice"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="价格单位"
        prop="priceUnit"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="重要度"
        prop="important"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="创建时间"
        prop="createTime"
        show-overflow-tooltip
        width="150px"
      />
      <!-- <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      /> -->
      <el-table-column align="center" label="操作" width="85">
        <template #default="{ row }">
          <!-- <el-link
            v-permissions="{ permission: ['client:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['client:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link> -->
          <el-link
            v-permissions="{ permission: ['materialinfo:print'] }"
            title="打印"
            :underline="false"
            @click="handlePrint(row)"
          >
            <vab-icon icon="printer-line" />
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
  </div>
</template>

<script>
  import { getList, GetMaterialSortList } from '@/api/materialMaterialInfo'
  import Edit from './components/MaterialMaterialInfoEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import { getLodop } from '@/utils/lodop.js'

  export default defineComponent({
    name: 'MaterialMaterialInfo',
    components: { Edit },
    setup() {
      const $baseTableHeight = inject('$baseTableHeight')
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        treeRef: null,
        editRef: null,
        MaterialSortOption: [],
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
        },
        fold: true,
        height: $baseTableHeight(3) - 30,
        materialCodeArr: [],
        hes: 0,
      })
      const handleFold = () => {
        state.fold = !state.fold
        handleHeight()
      }
      const handleHeight = () => {
        if (state.fold) state.height = $baseTableHeight(2) - 47
        else state.height = $baseTableHeight(3) - 30
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
        if (state.queryForm.code == '') {
          delete state.queryForm.code
        }
        if (state.queryForm.name == '') {
          delete state.queryForm.name
        }
        if (state.queryForm.source == '') {
          delete state.queryForm.source
        }
        if (state.queryForm.important == '') {
          delete state.queryForm.important
        }
        if (state.queryForm.alias == '') {
          delete state.queryForm.alias
        }
        if (state.queryForm.codingStatus == '') {
          delete state.queryForm.codingStatus
        }
        fetchData()
      }

      //物料编码懒加载
      const lazyLoad = async (node, resolve) => {
        if (node.level == 0) {
          const { data } = await GetMaterialSortList({ fid: 0 })
          data.forEach((item) => {
            item.dname = `${item.code} ${item.dname}`
            item.children = []
          })
          state.MaterialSortOption = data
          return resolve(data)
        }
        setTimeout(async () => {
          const nodes = await GetMaterialSortList({ fid: node.data.crid })
          nodes.data.forEach((item) => {
            item.dname = `${item.code} ${item.dname}`
            item.children = []
          })
          resolve(nodes.data)
        }, 0)
      }

      //树形控件点击节点时事件
      const handleNodeClick = async (val, treeNod, prop) => {
        console.log(val, treeNod, prop)
        state.materialCodeArr = []
        state.queryForm.code = ''
        platForm(state.treeRef.getNode(val))
        state.queryForm.code = ''
        state.materialCodeArr.forEach((item) => {
          state.queryForm.code += item.code
        })
        queryData()
      }

      //递归获取父节点信息函数
      const platForm = (node) => {
        if (!node.parent) {
          return
        }
        state.materialCodeArr.unshift(node.data)
        platForm(node.parent)
      }

      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      //获取ID进行打印
      const handlePrint = (row) => {
        const LODOP = getLodop()
        if (LODOP) {
          if (row.code) {
            $baseConfirm('确定打印当前项?', null, () => {
              //单条数据进行标签打印Statr
              LODOP.PRINT_INIT('') //初始化
              //SET_PRINTER_INDEX('Xprinter XP-450B') //按序号或名称指定打印机，选定后禁止手工重选；
              LODOP.SET_PRINTER_INDEX('Xprinter XP-450B') //选择名称是“Xprinter XP-450B”的打印机
              //SET_PRINTER_INDEXA('') //按序号或名称指定打印机，选定后允许手工重选；
              //设置横向(四个参数：打印方向及纸张类型（0(或其它)：打印方向由操作者自行选择或按打印机缺省设置；
              //1：纵向打印,固定纸张；
              //2：横向打印，固定纸张；
              //3：纵向打印，宽度固定，高度按打印内容的高度自适应。），纸张宽(mm)，纸张高(mm),纸张名(必须纸张宽等于零时本参数才有效。))
              LODOP.SET_PRINT_PAGESIZE(3, 1000, 700, '')
              //LODOP.ADD_PRINT_HTM('1%', '1%', '98%', '98%', 'strFormHtml') // 设置打印内容
              //LODOP.ADD_PRINT_TEXT('1%', '1%', '98%', '98%', 'strFormHtml')    // 设置打印内容
              LODOP.ADD_PRINT_BARCODE(10, 10, 150, 150, 'QRCode', row.code) // 条码（六个参数：Top,Left,Width,Height,BarCodeType,BarCodeValue）
              LODOP.SET_PRINT_STYLEA(0, 'QRCodeErrorLevel', 'H')
              //判断项目名称是否超过15个字符，超出做换行处理
              if (row.categoryPath.length > 10) {
                state.hes = 21
                row.categoryPath = `${row.categoryPath.slice(
                  0,
                  10
                )}\n${row.categoryPath.slice(10)}`
              } else {
                state.hes = 26
              }
              LODOP.ADD_PRINT_TEXT(15, 150, 210, 40, `物料名称：${row.name}`)
              LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
              LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
              LODOP.ADD_PRINT_TEXT(
                15 + state.hes,
                150,
                210,
                40,
                '数\xa0\xa0\xa0\xa0量：1(1/5)'
              )
              LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
              LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
              LODOP.ADD_PRINT_TEXT(
                15 + state.hes * 2,
                150,
                210,
                40,
                '供\xa0应\xa0商：湖南润伟'
              )
              LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
              LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
              LODOP.ADD_PRINT_TEXT(
                15 + state.hes * 3,
                150,
                280,
                40,
                `项目编码：${row.alias}`
              )
              LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
              LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
              LODOP.ADD_PRINT_TEXT(
                15 + state.hes * 4,
                150,
                420,
                40,
                `项目名称：${row.categoryPath}`
              )
              LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
              LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
              // LODOP.SET_PREVIEW_WINDOW(2, 0, 0, 800, 600, '')  // 设置预览窗口模式和大小
              LODOP.PREVIEW() // 预览。（这里可以进行预览，注意这里打开时记得把下面的print先注释。）另外，这里的预览只显示单个商品 打印出来的效果即该预览效果。
              //LODOP.PRINT() // 打印
              //单条数据进行标签打印End

              //const { msg } = await doDelete({ ids: row.id })
              $baseMessage('打印完成', 'success', 'vab-hey-message-success')
            })
          } else {
            if (state.selectRows.length > 0) {
              const ids = state.selectRows.map((item) => item.code)

              $baseConfirm('确定打印选中项?', null, () => {
                ids.forEach((item) => {
                  state.list.forEach((item1) => {
                    if (item == item1.code) {
                      LODOP.PRINT_INIT('') //初始化
                      //设置横向(四个参数：打印方向及纸张类型（0(或其它)：打印方向由操作者自行选择或按打印机缺省设置；
                      //1：纵向打印,固定纸张；
                      //2：横向打印，固定纸张；
                      //3：纵向打印，宽度固定，高度按打印内容的高度自适应。），纸张宽(mm)，纸张高(mm),纸张名(必须纸张宽等于零时本参数才有效。))
                      LODOP.SET_PRINT_PAGESIZE(3, 420, 120, '')
                      //LODOP.ADD_PRINT_HTM('1%', '1%', '98%', '98%', 'strFormHtml') // 设置打印内容
                      //LODOP.ADD_PRINT_TEXT('1%', '1%', '98%', '98%', 'strFormHtml')    // 设置打印内容
                      LODOP.ADD_PRINT_BARCODE(
                        10,
                        10,
                        150,
                        150,
                        'QRCode',
                        item1.code
                      ) // 条码（六个参数：Top,Left,Width,Height,BarCodeType,BarCodeValue）
                      LODOP.SET_PRINT_STYLEA(0, 'QRCodeErrorLevel', 'H')
                      //判断项目名称是否超过15个字符，超出做换行处理
                      if (item1.categoryPath.length > 15) {
                        state.hes = 21
                        item1.categoryPath = `${item1.categoryPath.slice(
                          0,
                          15
                        )}\n${item1.categoryPath.slice(15)}`
                      } else {
                        state.hes = 26
                      }
                      LODOP.ADD_PRINT_TEXT(
                        15,
                        150,
                        210,
                        40,
                        `物料名称：${item1.name}`
                      )
                      LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
                      LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
                      LODOP.ADD_PRINT_TEXT(
                        15 + state.hes,
                        150,
                        210,
                        40,
                        '数\xa0\xa0\xa0\xa0量：1(1/5)'
                      )
                      LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
                      LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
                      LODOP.ADD_PRINT_TEXT(
                        15 + state.hes * 2,
                        150,
                        210,
                        40,
                        '供\xa0应\xa0商：湖南润伟'
                      )
                      LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
                      LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
                      LODOP.ADD_PRINT_TEXT(
                        15 + state.hes * 3,
                        150,
                        280,
                        40,
                        `项目编码：${item1.alias}`
                      )
                      LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
                      LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
                      LODOP.ADD_PRINT_TEXT(
                        15 + state.hes * 4,
                        150,
                        420,
                        40,
                        `项目名称：${item1.categoryPath}`
                      )
                      LODOP.SET_PRINT_STYLEA(0, 'FontSize', 12)
                      LODOP.SET_PRINT_STYLEA(0, 'Bold', 1)
                      // LODOP.SET_PREVIEW_WINDOW(2, 0, 0, 800, 600, '')  // 设置预览窗口模式和大小
                      LODOP.PREVIEW() // 预览。（这里可以进行预览，注意这里打开时记得把下面的print先注释。）另外，这里的预览只显示单个商品 打印出来的效果即该预览效果。
                      //LODOP.PRINT() // 打印
                      //console.log(item1)
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
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        handleEdit,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleFold,
        lazyLoad,
        handleNodeClick,
        platForm,
        setSelectRows,
        handlePrint,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>

<style lang="scss" scoped>
  .el-tree {
    position: relative;
    min-width: 200px;
    max-height: 600px;
    overflow: auto;
    color: var(--el-tree-text-color);
    cursor: default;
    box-shadow: var(--el-box-shadow-light);
  }
  .el-tree-node__content {
    height: 30px;
  }
</style>
