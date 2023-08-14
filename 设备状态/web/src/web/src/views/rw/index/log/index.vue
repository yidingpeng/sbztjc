<template>
  <div class="system-log-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form :inline="true" label-width="60px" :model="queryForm" @submit.prevent>
          <!-- <el-form-item label="账号">
            <el-input v-model.trim="queryForm.account" clearable placeholder="请输入账号" />
          </el-form-item> -->
          <el-form-item label="类型">

            <el-select v-model="queryForm.type">
              <el-option label="所有日志" value="" />
              <el-option label="登录日志" value="Login" />
              <el-option label="操作日志" value="Operation" />
              <el-option label="错误日志" value="Error" />
              <el-option label="故障日志" value="故障" />
              <el-option label="占用日志" value="占用" />
              <el-option label="设备日志" value="设备" />
              <el-option label="接口日志" value="接口" />
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
      <el-table-column label="日志详情" prop="desc" show-overflow-tooltip />
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
  mounted() {
    this.fetchData();
    setInterval(() => {
      this.fetchData();
      // 进行其他操作
    }, 2000);
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

      const data = await getAllList()

      const blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.setAttribute('download', `${this.filename}.xlsx`);
      document.body.appendChild(link);
      link.click();
      // 打印Blob对象的大小和类型
    },
    queryData() {
      this.queryForm.pageNo = 1
      this.fetchData()
    }

  }


})
</script>
