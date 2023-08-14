<template>
  <div class="system-log-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form
          :inline="true"
          label-width="60px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="账号">
            <el-input
              v-model.trim="queryForm.account"
              clearable
              placeholder="请输入账号"
            />
          </el-form-item>
          <el-form-item label="类型">
            <rw-dictionary
              ref="rwdict"
              v-model="queryForm.type"
              default-value="所有类型"
              type="LogType"
            />
            <!-- <el-select v-model="queryForm.type">
              <el-option label="所有日志" value="0" />
              <el-option label="登录日志" value="1" />
              <el-option label="操作日志" value="2" />
              <el-option label="错误日志" value="3" />
              <el-option label="接口日志" value="4" />
            </el-select> -->
          </el-form-item>
          <el-form-item label="周期">
            <el-date-picker
              v-model="queryForm.searchDate"
              end-placeholder="结束日期"
              start-placeholder="开始日期"
              type="daterange"
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

    <el-table v-loading="listLoading" :data="list">
      <el-table-column type="expand">
        <template #default="{ row }">
          <div class="vab-table-expand">
            <p>
              <span class="vab-table-expand-title">日志类型:</span>
              {{ row.type }}
            </p>
            <p>
              <span class="vab-table-expand-title">账号:</span>
              {{ row.account }}
            </p>
            <p>
              <span class="vab-table-expand-title">登录IP:</span>
              {{ row.ip }}
            </p>
            <p>
              <span class="vab-table-expand-title">访问时间:</span>
              {{ row.datetime }}
            </p>
            <p>
              <span class="vab-table-expand-title">日志详情：</span>
              {{ row.desc }}
            </p>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="ID" prop="id" show-overflow-tooltip />
      <el-table-column label="日志类型" prop="type" show-overflow-tooltip />
      <el-table-column label="账号" prop="account" show-overflow-tooltip />
      <el-table-column
        label="执行结果"
        prop="executeResult"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <span v-if="row.result">
            <span class="vab-dot vab-dot-success"><span></span></span>
            {{ row.executeResult }}
          </span>
          <span v-else>
            <span class="vab-dot vab-dot-error"><span></span></span>
            {{ row.executeResult }}
          </span>
        </template>
      </el-table-column>
      <el-table-column label="登录IP" prop="ip" />
      <el-table-column
        label="访问时间"
        prop="datetime"
        show-overflow-tooltip
        width="160"
      />
      <template #empty>
        <el-empty class="vab-data-empty" description="暂无数据" />
      </template>
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
</template>

<script>
  import { getList } from '@/api/system/log'
  import { Search } from '@element-plus/icons-vue'
  import rwDictionary from '@/plugins/RwDictionary'

  export default defineComponent({
    name: 'SystemLog',
    components: { rwDictionary },
    setup() {
      const state = reactive({
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        queryForm: {
          type: '',
          account: '',
          searchDate: '',
          pageNo: 1,
          pageSize: 50,
        },
      })

      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
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
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        fetchData,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        Search,
      }
    },
  })
</script>
