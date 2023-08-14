<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-right-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="日志等级">
            <el-input
              v-model.trim="queryForm.logLevel"
              clearable
              placeholder="请输入日志等级"
            />
          </el-form-item>
          &nbsp;&nbsp;
          <el-form-item label="日志类别">
            <el-input
              v-model.trim="queryForm.requestType"
              clearable
              placeholder="请输入日志类别"
            />
          </el-form-item>
          &nbsp;&nbsp;
          <el-form-item label="日志时间">
            <el-date-picker
              v-model="searchDate"
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
          <el-form-item>
            <el-button
              v-permissions="{ permission: ['logdetails:download'] }"
              :icon="Search"
              type="primary"
              @click="Download"
            >
              导出
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-right-panel>
    </vab-query-form>
    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @row-dblclick="handleEdit"
      @selection-change="setSelectRows"
    >
      <el-table-column show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="日志时间"
        prop="logDate"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="日志线程"
        prop="logThread"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="日志等级"
        prop="logLevel"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="监听路径"
        prop="path"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="日志内容"
        prop="logMessage"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="日志类别"
        prop="requestType"
        show-overflow-tooltip
      />
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
  import { getList } from '@/api/basics/logdetails'
  // import Edit from './components/BasicsDeviceStatusEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsDevice',
    // components: { Edit },
    setup() {
      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        searchDate: [],
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          startTime: '',
          endTime: '',
        },
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
        if (state.searchDate.length > 0) {
          state.queryForm.startTime = state.searchDate[0]
          state.queryForm.endTime = state.searchDate[1]
        } else {
          state.queryForm.startTime = ''
          state.queryForm.endTime = ''
        }
        fetchData()
      }
      const Download = async () => {
        import('@/utils/excel').then((excel) => {
          const tHeader = [
            '日志时间',
            '日志线程',
            '日志等级',
            '监听路径',
            '日志内容',
            '日志类别',
          ]
          const filterVal = [
            'logDate',
            'logThread',
            'logLevel',
            'path',
            'logMessage',
            'requestType',
          ]
          const list = state.list
          const data = formatJson(filterVal, list)
          excel.export_json_to_excel({
            header: tHeader,
            data,
            filename: '文档监听日志导出文件',
            autoWidth: true,
            bookType: 'xlsx',
          })
        })
      }
      const formatJson = (filterVal, jsonData) => {
        return jsonData.map((v) =>
          filterVal.map((j) => {
            return v[j]
          })
        )
      }
      const fetchData = async () => {
        //state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
        setInterval(() => {
      fetchData();
      // 进行其他操作
    }, 2000);
      })
      return {
        ...toRefs(state),
        setSelectRows,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        Download,
        fetchData,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
