<template>
  <div class="bom-detail-container">
    <div style="margin-bottom: 10px">
      <el-button @click="router.push('/BOM')">返回</el-button>
    </div>

    <el-row style="margin-bottom: 10px">
      <el-col :span="24">
        <el-descriptions border :column="3">
          <template #title>{{ table.bomCode }} {{ table.bomName }}</template>
          <template #extra>
            <el-button
              v-if="table.canApprove"
              type="primary"
              @click="approveBOM()"
            >
              审核
            </el-button>
            <el-button
              v-if="table.canUndo && table.canApprove"
              type="danger"
              @click="undoApprove()"
            >
              撤回
            </el-button>
          </template>
          <el-descriptions-item label="BOM名称">
            {{ table.bomName }}
          </el-descriptions-item>
          <el-descriptions-item label="BOM编号">
            {{ table.bomCode }}
          </el-descriptions-item>
          <el-descriptions-item label="版本号">
            {{ table.version }}
          </el-descriptions-item>
          <el-descriptions-item label="项目号">
            {{ table.projectCode }}
          </el-descriptions-item>
          <el-descriptions-item label="项目名称">
            {{ table.projectName }}
          </el-descriptions-item>
          <el-descriptions-item label="提交时间">
            {{ table.createTime }}
          </el-descriptions-item>
          <el-descriptions-item label="编制">
            {{ table.comitBy }}
          </el-descriptions-item>
          <el-descriptions-item label="审核">
            {{ table.auditBy }}
          </el-descriptions-item>
          <el-descriptions-item label="采购">
            {{ table.purchaseBy }}
          </el-descriptions-item>
          <el-descriptions-item label="批准">
            {{ table.approvalBy }}
          </el-descriptions-item>
          <el-descriptions-item label="BOM状态">
            <stautsTag :status="table.status" :text="table.statusText" />
          </el-descriptions-item>
          <el-descriptions-item label="子项数">
            {{ items.length }}
          </el-descriptions-item>
          <el-descriptions-item label="备注" :span="4">
            {{ table.remark }}
          </el-descriptions-item>

          <el-descriptions-item label="审批记录" :span="4">
            <el-table
              border
              :data="comments"
              style="width: 80%; min-width: 600px"
            >
              <el-table-column label="序号" type="index" width="55" />
              <el-table-column label="审批人" prop="user" width="120" />
              <el-table-column label="审批节点" prop="node" width="120" />
              <el-table-column label="审批内容" prop="comment" />
              <el-table-column label="审批时间" prop="commentTime" />
              <el-table-column label="审批结果" prop="result" width="100">
                <template #default="{ row }">
                  <el-tag v-if="row.result" effect="dark" type="success">
                    通过
                  </el-tag>
                  <el-tag v-else efflect="dark" type="danger">驳回</el-tag>
                </template>
              </el-table-column>
            </el-table>
          </el-descriptions-item>
        </el-descriptions>
      </el-col>
    </el-row>
    <el-row class="bom-table-items" :gutter="20">
      <el-col class="left-tree" :lg="6" :md="8" :sm="24" :xl="6" :xs="24">
        <vab-card shadow="hover">
          <h3>
            BOM结构树
            <el-button @click="getFullItems()">总表</el-button>
          </h3>
          <el-tree
            :data="tree"
            default-expand-all
            :expand-on-click-node="false"
            node-key="id"
            :props="{ class: customNodeClass }"
            @node-click="handleNodeClick"
          />
        </vab-card>
      </el-col>
      <el-col class="right-items" :lg="18" :md="16" :sm="24" :xl="18">
        <el-table border :data="items" :row-class-name="tableRowClassName">
          <el-table-column label="序号" prop="order" width="55" />
          <el-table-column label="父编码" prop="parent" />
          <el-table-column label="编码" prop="code" />
          <el-table-column label="数量" prop="count" width="55" />
          <el-table-column label="物料名称" prop="material.name" />
          <el-table-column label="代号" prop="material.alias" />
          <el-table-column label="材质" prop="material.material" />
          <el-table-column label="型号规格" prop="material.model" />
          <el-table-column label="参数" prop="material.specification" />
          <el-table-column label="重量" prop="material.weight" />
          <el-table-column label="重要度" prop="material.important" />
          <el-table-column label="创建时间" prop="createTime" />
        </el-table>
      </el-col>
    </el-row>
    <approve ref="approveRef" @fetch-data="getFullItems" />
  </div>
</template>

<script>
  import { getDetail, undoBOM } from '@/api/bom/index'
  // import { useUserStore } from '~/src/store/modules/user'
  import approve from './components/approve'
  import stautsTag from '@/plugins/BOMStatusTag/index'

  export default defineComponent({
    name: 'BOMDetail',
    components: { approve, stautsTag },
    setup() {
      const state = reactive({
        bomId: 0,
        approveRef: null,
        table: { bomName: '' },
        items: [],
        comments: [],
        tree: [],
        //treeProps: { class: customNodeClass },
        //canUndo: false,
        //canApprove: false,
      })
      const route = useRoute()
      const router = useRouter()
      //const user = useUserStore()
      const $baseMessage = inject('$baseMessage')
      const $confirm = inject('$baseConfirm')

      //获取详情
      const toGetDetail = async (id) => {
        state.bomId = id
        const { data } = await getDetail(id)
        state.table = data.table
        state.items = data.items
        state.comments = data.comments
        // state.canUndo =
        //   data.table.createBy == user.username ||
        //   data.table.comitBy == user.username
        // state.canApprove =
        //   data.table.status == 'Auditing' ||
        //   data.table.status == 'Purchasing' ||
        //   data.table.status == 'Approving'
        //genTree2(data.items, null)
        genTree3(data.items)
      }

      //树控件删除线样式
      const customNodeClass = (data) => {
        // console.log(data)
        // console.log(node)
        // console.log(data.data.removeVersionIndex > data.data.versionIndex)
        if (data.data.removeVersionIndex > data.data.versionIndex)
          return 'is-removed'
        return null
      }

      //表格删除线样式
      const tableRowClassName = ({ row }) => {
        if (row.removeVersionIndex > row.versionIndex) return 'is-removed'
        return null
      }

      // const genTree2 = (items) => {
      //   var parents = items.filter((x) => !x.parent)
      //   var arr = []
      //   //console.log(parents)
      //   for (let index in parents) {
      //     const item = parents[index]
      //     const node = getNode(item)
      //     //const children = items.filter((x) => x.parent == item.code)
      //     getChild(node, items, item.code)
      //     arr.push(node)
      //   }
      //   console.log('node', arr)
      //   //state.tree = arr
      // }

      const genTree3 = (items) => {
        // var parents = items.filter((x) => x.level == level)
        // var arr = []
        // for (let index in parents) {
        //   const item = parents[index]
        //   const node = getNode(item)
        //   getChild(node, items, item.code)
        //   arr.push(node)
        // }
        // state.tree = arr

        //每个根节点的最终节点
        const levelRoot = {}
        //let lastNode = null
        let lastLevel = 0
        const arr = []

        for (const index in items) {
          const item = items[index]
          const node = getNode(item)
          if (lastLevel == 0 || item.level == 1) {
            //console.log('add root')
            //console.log(item)
            arr.push(node)
          } else {
            levelRoot[item.level - 1].children.push(node)
            //console.log('node', item.length)
            //console.log(node)
          }

          levelRoot[item.level] = node

          lastLevel = item.level
          //lastNode = node
        }
        state.tree = arr
      }

      // const getChild = (node, items, parent) => {
      //   const children = items.filter((x) => x.parent == parent)
      //   console.log('parent', parent)
      //   //console.log('children', children)
      //   for (let index in children) {
      //     const item = children[index]
      //     const nodeItem = getNode(item)
      //     node.children.push(nodeItem)
      //     getChild(nodeItem, items, item.code)
      //   }
      // }

      //生成一个树节点
      const getNode = (item) => {
        return {
          code: item.code,
          label: getLabel(item),
          data: item,
          level: item.level,
          children: [],
        }
      }

      const getLabel = (item) => {
        return `${item.code + item.material.name}(${item.versionText})`
      }

      //树节点点击事件
      const handleNodeClick = (node) => {
        //alert(node.label)
        const items = []
        setChildData(node, items)
        state.items = items
      }

      const setChildData = (node, items) => {
        items.push(node.data)
        for (const item in node.children) {
          setChildData(node.children[item], items)
        }
      }

      //页面激活
      onActivated(() => {
        const { id } = route.query
        toGetDetail(id)
      })

      //页面加载
      onMounted(() => {
        // console.log('mounted..')
        // const { id } = route.query
        // toGetDetail(id)
      })

      //重新获取所有详情
      const getFullItems = () => {
        toGetDetail(state.bomId)
      }

      //审核BOM
      const approveBOM = () => {
        state.approveRef.showApprove(state.table)
      }

      //撤回BOM
      const undoApprove = () => {
        $confirm(
          '撤回后，已审批的节点需重新审批，您还可以通过BOM系统再次提交，确定要撤回吗？',
          null,
          async () => {
            const { msg } = await undoBOM(state.bomId)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            getFullItems()
          }
        )
      }

      return {
        ...toRefs(state),
        router,
        handleNodeClick,
        getFullItems,
        approveBOM,
        undoApprove,
        customNodeClass,
        tableRowClassName,
      }
    },
  })
</script>

<style>
  .is-removed {
    text-decoration: line-through;
  }
</style>
