<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="60%"
    @close="save"
  >
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
            <el-form-item>
              <el-button
                :icon="Search"
                native-type="submit"
                type="primary"
                @click="queryData"
              >
                查询
              </el-button>
            </el-form-item>
          </el-form>
        </vab-query-form-top-panel>
        <div style="width: 100%">
          <el-checkbox
            v-model="chooseAll"
            class="selectAll"
            label="全选"
            @change="selectAllChange"
          />
        </div>
      </vab-query-form>
      <el-table
        v-loading="listLoading"
        border
        :data="list"
        style="max-height: 450px; overflow: auto"
        @selection-change="setSelectRows"
      >
        <!-- <el-table-column
          align="center"
          show-overflow-tooltip
          type="selection"
        /> -->
        <el-table-column
          align="center"
          label="物料编码"
          prop="code"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="物料名称"
          prop="name"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="规格型号"
          prop="model"
          show-overflow-tooltip
        />
        <el-table-column
          align="center"
          label="基本单位"
          prop="basicUnit"
          show-overflow-tooltip
        />
        <el-table-column align="center" label="添加" width="85">
          <template #default="{ row }">
            <el-link title="选择" :underline="false" @click="handleChoose(row)">
              <span v-if="!row.selectState">+</span>
              <span v-else>√</span>
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
    </div>
    <template #footer>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { getList, GetMaterialSortList } from '@/api/materialMaterialInfo'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'MaterialMaterialInfo',
    emits: ['get-purdetail'],
    setup(props, { emit }) {
      // const $baseConfirm = inject('$baseConfirm')
      // const $baseMessage = inject('$baseMessage')
      const state = reactive({
        treeRef: null,
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
        materialCodeArr: [],
        title: '',
        dialogFormVisible: false,
        selectedData: [],
        chooseAll: false,
      })
      const setSelectRows = (val) => {
        state.selectRows = val
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
        fetchData()
      }

      //选择物料数据，如已选数据中不存在当前所点击的数据，则添加到已选数组中；如已存在，则从已选数组中删除该项
      const handleChoose = (row) => {
        if (state.selectedData.findIndex((i) => i === row.cid) !== -1) {
          const index = state.selectedData.indexOf(row.cid)
          state.selectedData.splice(index, 1)
          row.selectState = false
        } else {
          state.selectedData.push(row.cid)
          row.selectState = true
        }
        for (let i = 0; i < state.list.length; i++) {
          if (state.list[i].selectState == false) {
            state.chooseAll = false
            break
          }
          state.chooseAll = true
        }
      }

      //全选或取消全选功能
      const selectAllChange = () => {
        if (state.chooseAll) {
          state.list = state.list.map((v) => {
            v.selectState = true
            return v
          })

          state.list.forEach((item) => {
            if (state.selectedData.findIndex((i) => i === item.cid) === -1)
              state.selectedData.push(item.cid)
          })
        } else {
          state.list = state.list.map((v) => {
            v.selectState = false
            return v
          })
          state.list.forEach((item) => {
            const index = state.selectedData.indexOf(item.cid)
            state.selectedData.splice(index, 1)
          })
        }
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
        list.forEach((item) => {
          if (state.selectedData.findIndex((i) => i === item.cid) === -1) {
            //所选物料在该列表中不存在
            item.selectState = false
          } else item.selectState = true
        })
        state.list = list
        //判断是否全选
        for (let i = 0; i < state.list.length; i++) {
          if (state.list[i].selectState == false) {
            state.chooseAll = false
            break
          }
          state.chooseAll = true
        }
        state.total = total
        state.listLoading = false
      }

      const showEdit = (selectData) => {
        state.selectedData = selectData
        state.title = '添加采购明细'
        state.dialogFormVisible = true
        fetchData()
      }

      const save = () => {
        state.dialogFormVisible = false
        emit('get-purdetail', state.selectedData)
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        save,
        showEdit,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        lazyLoad,
        handleNodeClick,
        platForm,
        setSelectRows,
        handleChoose,
        selectAllChange,
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
  .selectAll {
    float: right;
    margin-right: 8px;
  }
</style>
