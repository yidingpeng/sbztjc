<template>
  <div class="bom-container">
    <el-row :gutter="20">
      <el-col :lg="8" :md="8" :sm="24" :xl="8" :xs="24">
        <vab-card shadow="hover">
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <!-- <el-form-item>
                  <el-input
                    v-model.trim="queryForm.bomCode"
                    clearable
                    placeholder="输入BOM编码或名称"
                    style="width: 180px"
                  />
                </el-form-item>
                &nbsp; -->
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm.projectId"
                    clearable
                    placeholder="输入项目编码或名称"
                    style="width: 180px"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-top-panel>
          </vab-query-form>

          <el-table
            v-loading="listLoading"
            :data="list"
            highlight-current-row
            row-key="id"
            @current-change="selectedRowChange"
          >
            <el-table-column
              align="center"
              label="项目编码"
              prop="projectId"
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
              label="状态"
              prop="status"
              show-overflow-tooltip
            /> -->
          </el-table>
          <el-pagination
            background
            :current-page="queryForm.pageNo"
            :layout="layout"
            :page-size="queryForm.pageSize"
            small
            :total="total"
            @current-change="handleCurrentChange"
            @size-change="handleSizeChange"
          />
        </vab-card>
      </el-col>
      <el-col :lg="16" :md="16" :sm="24" :xl="16" :xs="24">
        <vab-card shadow="hover">
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" :model="queryForm2" @submit.prevent>
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm2.materialName"
                    clearable
                    placeholder="输入物料名称"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button
                    :icon="Search"
                    type="primary"
                    @click="queryDataChildren"
                  >
                    查询
                  </el-button>
                </el-form-item>
                <!-- <el-form-item>
                  <el-button type="primary" @click="handleShop">
                    <vab-icon icon="shopping-cart-line" />
                    &nbsp;批量下单
                  </el-button>
                </el-form-item> -->
              </el-form>
            </vab-query-form-top-panel>
          </vab-query-form>
          <el-table
            v-loading="listLoadingChildren"
            border
            :data="listChildren"
            row-key="id"
            @selection-change="setSelectRows"
          >
            <el-table-column
              align="center"
              show-overflow-tooltip
              type="selection"
            />
            <el-table-column
              align="center"
              label="物料编码"
              prop="materialCode"
              width="160px"
            />
            <el-table-column
              align="center"
              label="物料名称"
              prop="materialName"
              width="150px"
            />
            <el-table-column
              align="center"
              label="规格型号"
              prop="model"
              width="100px"
            />
            <el-table-column
              align="center"
              label="技术参数"
              prop="specification"
              width="100px"
            />
            <el-table-column
              align="center"
              label="材料"
              prop="cailiao"
              width="100px"
            />
            <el-table-column
              align="center"
              label="基本单位"
              prop="basicUnit"
              width="100px"
            />
            <el-table-column
              align="center"
              label="重量"
              prop="weight"
              width="90px"
            />
            <el-table-column
              align="center"
              label="重量单位"
              prop="weightUnit"
              width="100px"
            />
            <el-table-column
              align="center"
              label="数量"
              prop="count"
              width="60px"
            />
            <!-- <el-table-column
              align="center"
              label="状态"
              prop="orderFormState"
              width="100px"
            /> -->
          </el-table>
        </vab-card>
      </el-col>
    </el-row>
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { BOMPage, ByBomIdList, doShop } from '@/api/purchase/purchase'
  import Edit from './components/PurchaseEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import arrayToTree from 'array-to-tree'

  export default defineComponent({
    name: 'Purchase',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listChildren: [],
        listLoading: true,
        listLoadingChildren: false,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        totalChildren: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
        },
        queryForm2: {
          pageNo: 1,
          pageSize: 10,
          bomID: 0,
        },
        BomOptions: [],
        bomID: 0,
        BomItemKist: [],
        bomCode: '',
        expanded: [],
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
      const handleShop = (row) => {
        if (row.materialCode) {
          $baseConfirm('确定要操作当前项吗?', null, async () => {
            //console.log(row.materialCode)
            const { msg } = await doShop({
              ids: row.materialCode,
              bomCode: state.bomCode,
            })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            // await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.materialCode).join()
            $baseConfirm('确定要操作选中项吗?', null, async () => {
              console.log(ids)
              const { msg } = await doShop({
                ids: ids,
                bomCode: state.bomCode,
              })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              // await fetchData()
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
      const handleSizeChange2 = (val) => {
        state.queryForm2.pageSize = val
        fetchDataChildren()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const handleCurrentChange2 = (val) => {
        state.queryForm2.pageNo = val
        fetchDataChildren()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        if (state.queryForm.bomID == '') {
          delete state.queryForm.bomID
        }
        if (state.queryForm.projectId == '') {
          delete state.queryForm.projectId
        }
        fetchData()
      }
      //queryDataChildren
      const queryDataChildren = () => {
        // const arr = []
        // state.listChildren.forEach((item) => {
        //   if (item.indexOf(state.queryForm2.materialName) != -1) {
        //     arr.push(item)
        //   }
        // })
        // if (state.queryForm.materialName == '') {
        //   delete state.queryForm.materialName
        // }
        // state.listChildren = arr
      }

      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await BOMPage(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const fetchDataChildren = async (BomId) => {
        state.listLoadingChildren = true
        const list = await ByBomIdList({ bomId: BomId })
        // const list2 = await ByBomToTreeList()
        const treedata = arrayToTree(list.data, {
          parentProperty: 'parentCode',
          customID: 'materialCode',
        })
        state.listChildren = treedata
        // state.expanded.push(treedata[0].id)
        // console.log(treedata[0].id)
        state.listLoadingChildren = false
      }
      const handleSupplier = () => {
        if (state.selectRows.length > 0) {
          const ids = state.selectRows.map((item) => item.id).join()
          $baseConfirm('你确定要操作选中项吗', null, async () => {
            state.OptSupEditRef.showEdit(ids)
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      const handleMaterial = (mid) => {
        if (state.selectRows.length > 0) {
          const ids = state.selectRows.map((item) => item.id).join()
          $baseConfirm('你确定要操作选中项吗', null, async () => {
            if (mid == 1) {
              state.OptSupEditRef.showEdit(ids)
            } else if (mid == 2) {
              state.CostPriceEditRef.showEdit(ids)
            }
          })
        } else {
          $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
        }
      }
      //表格行点击事件
      const selectedRowChange = (val) => {
        // state.queryForm2.bomID = val.id
        fetchDataChildren(val.id)
        state.bomCode = val.bomCode
      }
      onMounted(() => {
        fetchData()
        //bomSet()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleShop,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        queryDataChildren,
        fetchData,
        handleSupplier,
        handleMaterial,
        selectedRowChange,
        handleCurrentChange2,
        handleSizeChange2,
        //bomSet,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
