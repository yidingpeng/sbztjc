<template>
  <div class="system-log-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form :inline="true" label-width="60px" :model="queryForm" @submit.prevent>
          <!-- <el-form-item label="账号">
            <el-input v-model.trim="queryForm.account" clearable placeholder="请输入账号" />
          </el-form-item> -->
          <el-form-item label="类型">
            <!-- <rw-dictionary ref="rwdict" v-model="queryForm.type" default-value="所有类型" type="LogType" /> -->
            <el-select v-model="queryForm.type">
              <el-option label="所有日志" value="" />
              <el-option label="登录日志" value="Login" />
              <el-option label="操作日志" value="Operation" />
              <el-option label="错误日志" value="Error" />
              <el-option label="设备停机日志" value="4" />
              <el-option label="设备运行日志" value="4" />
            </el-select>
          </el-form-item>
          <!-- <el-form-item label="周期">
            <el-date-picker v-model="queryForm.searchDate" end-placeholder="结束日期" start-placeholder="开始日期"
              type="daterange" />
          </el-form-item> -->
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
          <el-form-item label="文件名">
            <el-input v-model="filename" placeholder="请输出要导出文件的名称" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleDownload">
              导出 Excel
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
            <!-- <p>
              <span class="vab-table-expand-title">账号:</span>
              {{ row.account }}
            </p> -->
            <p>
              <span class="vab-table-expand-title">IP:</span>
              {{ row.ip }}
            </p>
            <p>
              <span class="vab-table-expand-title">时间:</span>
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
      <!-- <el-table-column label="账号" prop="account" show-overflow-tooltip /> -->
      <el-table-column label="执行结果" prop="executeResult" show-overflow-tooltip>
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
      <el-table-column label="IP" prop="ip" />
      <el-table-column label="时间" prop="datetime" show-overflow-tooltip width="160" />
      <template #empty>
        <el-empty class="vab-data-empty" description="暂无数据" />
      </template>
    </el-table>
    <el-pagination background :current-page="queryForm.pageNo" :layout="layout" :page-size="queryForm.pageSize"
      :total="total" @current-change="handleCurrentChange" @size-change="handleSizeChange" />
  </div>
</template>

<script>
import { getList, getAllList } from '@/api/system/log'
import { Search } from '@element-plus/icons-vue'
import rwDictionary from '@/plugins/RwDictionary'

export default defineComponent({
  name: 'SystemLog',
  // components: { rwDictionary },
  data() {
    return {

      list: [],
      listLoading: true,
      layout: 'total, sizes, prev, pager, next, jumper',
      total: 0, filename: '',
      autoWidth: true,
      bookType: 'xlsx',
      queryForm: {
        type: '',
        account: '',
        searchDate: '',
        pageNo: 1,
        pageSize: 50,
      },
    }
  },
  created() {
    this.fetchData()
  },
  methods: {
    async fetchData() {
      this.listLoading = true
      const {
        data: { list, total },
      } = await getList(this.queryForm)
      console.log(list)
      this.list = list
      this.total = total
      this.listLoading = false
    },
    handleSizeChange(val) {
      this.queryForm.pageSize = val
      this.fetchData()
    },
    handleCurrentChange(val) {
      this.queryForm.pageNo = val
      this.fetchData()
    },
    async handleDownload() {

      const {
        data: { list, total },
      } = await getAllList()

      import('@/utils/excel').then((excel) => {


        const tHeader = ['ID', '日志类型', '执行结果', 'IP', '日志详情', '时间']
        const filterVal = ['id', 'type', 'executeResult', 'ip', 'desc', 'datetime']
        const list1 = list
        console.log(list1);
        const data = this.formatJson(filterVal, list1)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: this.filename,
          autoWidth: this.autoWidth,
          bookType: this.bookType,
        })
        this.downloadLoading = false
      })
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map((v) =>
        filterVal.map((j) => {
          return v[j]
        })
      )
    },
    queryData() {
      this.queryForm.pageNo = 1
      fetchData()
    }

  }



})
</script>
