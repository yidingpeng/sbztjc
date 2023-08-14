
<template>
  <div class="role-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="24" :md="24" :sm="24" :xl="24" :xs="24">
        <vab-card shadow="hover">
          <template #header>
            <div class="card-header">
              <div>台架周统计</div>
            </div>
          </template>
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" label-width="60px" :model="queryForm" @submit.prevent>
                <!-- <el-form-item label="账号">
            <el-input v-model.trim="queryForm.account" clearable placeholder="请输入账号" />
          </el-form-item> -->
                <el-form-item label="周">

                  <el-select v-model="queryForm.week">
                    <el-option v-for="week in weeks" :key="week" :label="week" :value="week" />
                  </el-select>
                </el-form-item>
                <el-form-item label="月">

                  <el-select v-model="queryForm.month">
                    <el-option v-for="month in months" :key="month" :label="month" :value="month" />
                  </el-select>
                </el-form-item>
                <el-form-item label="年">
                  <el-date-picker v-model="queryForm.year" type="year" placeholder="选择年" />

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
                <el-form-item>
                  <el-button type="primary" @click="handleDownload">
                    导出 Excel
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-top-panel>
          </vab-query-form>

          <el-table v-loading="listLoading" border :data="list" highlight-current-row @selection-change="setSelectRows"
            style="width: 100%">

            <el-table-column align="center" fixed label="试验台架" prop="roomName" show-overflow-tooltip width="120" />
            <!-- <el-table-column align="center" fixed label="试验编号" prop="testNumber" show-overflow-tooltip width="120" /> -->
            <el-table-column align="center" label="有效运行时间" prop="totalEffectiveRunningTime" show-overflow-tooltip
              :formatter="formatEndTime" width="120" />
            <el-table-column align="center" label="实验暂停时间" prop="totalTestStopTime" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="故障时间" prop="totalFaultTime" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="待机时间" prop="totalStandbyTime" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="闲置时间" prop="totalFreeTime" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="维保时间" prop="totalweibaoTime" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="调试时间" prop="deviceDebugRun" show-overflow-tooltip
              :formatter="formatEndTime" width="220" />
            <el-table-column align="center" label="稼动率" prop="totalUtilizationRate" show-overflow-tooltip fixed="right"
              :formatter="formatEndTime" width="220" />
           
          </el-table>
          <edit ref="editRef" @fetch-data="fetchData" />
          <el-pagination background :current-page="queryForm.pageNo" :layout="layout" :page-size="queryForm.pageSize"
            :total="total" @current-change="handleCurrentChange" @size-change="handleSizeChange" />
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { Delete, Plus, Search } from '@element-plus/icons-vue'
import { getAllList, doDelete } from '@/api/orders/sprayingAmount'
import { getweek, importweekcount } from '@/api/DeviceStatus/DRoomDataCount'
import dayjs from 'dayjs';
import { color } from 'echarts'
export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit: defineAsyncComponent(() =>
      import('./components/weekedit')
    ),
    // adds: defineAsyncComponent(() =>
    //   import('./Faulteadd')
    // ),
  },
  setup() {
    const $baseConfirm = inject('$baseConfirm')
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      editRef: null,
      adds: null,
      permissionRef: null,
      list: [],
      listLoading: true,
      saveLoading: false,
      layout: 'total, sizes, prev, pager, next, jumper',
      total: 0,
      selectRows: '',
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        week: '',
        month: '',
        year: ''
      },
      months: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
      weeks: [1, 2, 3, 4, 5]
    })

    const setSelectRows = (val) => {
      state.selectRows = val
    }

    const handleEdit = (row, addoredit) => {

      state['editRef'].showEdit(row, 'edits')

    }
    const handleadd = (row, addoredit) => {

      state['adds'].showadd(row, 'edit')

    }

    const handleDelete = (row) => {
      console.log(row.id)
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
    const formatEndTime = (row, column, cellValue) => {
      const years = Math.floor(cellValue / (3600 * 24 * 365));
      const months = Math.floor(cellValue % (3600 * 24 * 365) / (3600 * 24 * 30));
      const days = Math.floor(cellValue % (3600 * 24 * 30) / (3600 * 24));
      const hours = Math.floor(cellValue % (3600 * 24) / 3600);
      const minutes = Math.floor(cellValue % 3600 / 60);

      return `${years}年 ${months}月 ${days}日 ${hours}小时 ${minutes}分钟`;
    }
    const fetchData = async () => {
      state.listLoading = false
      const {
        data: { list, total },
      } = await getweek(state.queryForm)
      state.list = list
      state.total = total
      //state.listLoading = false
    }
    const handleDownload = async () => {

      const data = await importweekcount(state.queryForm)

      const blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      const date = dayjs();

      const formattedDateTime = date.format('YYYY-MM-DD HH:mm:ss');
      link.setAttribute('download', `台架周统计${formattedDateTime}.xlsx`);
      document.body.appendChild(link);
      link.click();
      // 打印Blob对象的大小和类型
    }

    onMounted(() => {
      fetchData()
      // setInterval(() => {
      //   fetchData()
      // }, 3000)
    })



    return {
      ...toRefs(state),
      setSelectRows,
      handleEdit, handleadd,
      handleDelete,
      handleSizeChange,
      handleCurrentChange,
      queryData,
      fetchData,
      Delete,
      Plus,
      Search, formatEndTime, handleDownload,
    }
  },
})
</script>
<style lang="scss" scoped>
.card-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>
