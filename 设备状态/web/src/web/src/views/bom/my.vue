<template>
  <div class="bom-container">
    <vab-query-form>
      <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.key"
              clearable
              placeholder="请输入关键字"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel>
    </vab-query-form>

    <el-table v-loading="listLoading" :data="list">
      <el-table-column
        align="center"
        label="编号"
        prop="id"
        show-overflow-tooltip
        width="60"
      />
      <el-table-column
        align="center"
        label="BOM编号"
        prop="bomCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="BOM名称"
        prop="bomName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目号"
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
        label="版本(序)号"
        prop="version"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <div>
            <el-tag effect="dark" type="success">{{ row.version }}</el-tag>
            <el-tag effect="dark" style="margin-left: 0" type="danger">
              {{ row.versionIndex }}
            </el-tag>
          </div>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="创建时间"
        min-width="120"
        prop="createTime"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="编制"
        prop="comitBy"
        show-overflow-tooltip
        width="70"
      >
        <template #default="{ row }">
          <el-tag v-if="row.comitBy" effect="dark">
            {{ row.comitBy }}
          </el-tag>
          <div v-else>无</div>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="审核"
        prop="auditBy"
        show-overflow-tooltip
        width="70"
      >
        <template #default="{ row }">
          <el-tag v-if="row.auditBy" effect="dark">
            {{ row.auditBy }}
          </el-tag>
          <div v-else>无</div>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="采购"
        prop="purchaseBy"
        show-overflow-tooltip
        width="70"
      >
        <template #default="{ row }">
          <el-tag v-if="row.purchaseBy" effect="dark">
            {{ row.purchaseBy }}
          </el-tag>
          <div v-else>无</div>
        </template>
      </el-table-column>

      <el-table-column
        align="center"
        label="批准"
        prop="approvalBy"
        show-overflow-tooltip
        width="70"
      >
        <template #default="{ row }">
          <el-tag v-if="row.approvalBy" effect="dark">
            {{ row.approvalBy }}
          </el-tag>
          <div v-else>无</div>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="当前状态"
        prop="statusText"
        show-overflow-tooltip
        width="100"
      >
        <template #default="{ row }">
          <!--
            颜色1：cadetblue
            颜色2：coral
            颜色3：goldenrod
            颜色4：teal
            颜色5：peru
          -->
          <stautsTag :status="row.status" :text="row.statusText" />
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="110">
        <template #default="{ row }">
          <el-link :href="'/#/bom/detail?id=' + row.id">详情</el-link>
          <el-button
            link
            style="margin-left: 5px"
            type="primary"
            @click="handleApprove(row)"
          >
            审核
          </el-button>
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
    <approve ref="approveRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { getMyBOM } from '@/api/bom/index'
  import approve from './components/approve'
  import { Delete, Plus, Comment, Search } from '@element-plus/icons-vue'
  import router from '@/router'
  import stautsTag from '@/plugins/BOMStatusTag/index'

  export default defineComponent({
    name: 'MyBOM',
    components: { approve, stautsTag },
    //onMounted: () => {},
    setup() {
      //const $baseMessage = inject('$baseMessage')
      //$baseMessage('test')
      const state = reactive({
        approveRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          key: '',
          version: '',
        },
      })

      const fetchData = async () => {
        const {
          data: { list, total },
        } = await getMyBOM(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }

      onMounted(async () => {
        await fetchData()
      })

      const handleDetail = (row) => {
        router.go(`/bom/detail?id=${row.id}`)
      }

      const handleApprove = (row) => {
        state.approveRef.showApprove(row)
      }
      const handleAudit = () => {}
      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        fetchData()
      }

      return {
        ...toRefs(state),
        Search,
        Delete,
        Plus,
        Comment,
        fetchData,
        queryData,
        handleDetail,
        handleAudit,
        handleApprove,
        handleSizeChange,
        handleCurrentChange,
      }
    },
  })
</script>
