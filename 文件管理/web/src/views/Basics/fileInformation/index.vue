<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-right-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="试验间">
        <el-select :data="list" v-model="queryForm.roomName" allow-create13 placeholder="选择试验间" @change="handleChange">
          <el-option label="所有房间" value="" />
          <el-option v-for="item in roomlist" :key="item" :label="item.roomName" :value="item.roomName" />

        </el-select>
      </el-form-item>
          <el-form-item label="文件相对路径">
            <el-input
              v-model.trim="queryForm.fileRelativePath"
              clearable
              placeholder="请输入相对文件路径"
            />
          </el-form-item>
          &nbsp;&nbsp;
          <el-form-item label="文件绝对路径">
            <el-input
              v-model.trim="queryForm.filePath"
              clearable
              placeholder="请输入绝对文件路径"
            />
          </el-form-item>
          &nbsp;&nbsp;
          <el-form-item label="文件名称">
            <el-input
              v-model.trim="queryForm.fileName"
              clearable
              placeholder="请输入文件名称"
            />
          </el-form-item>
          &nbsp;&nbsp;
          <el-form-item label="上传时间">
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
              v-permissions="{ permission: ['fileInformation:download'] }"
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
        label="文件名称"
        prop="fileName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件绝对路径"
        prop="filePath"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="文件相对路径"
        prop="fileRelativePath"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="上传时间"
        prop="dateCreateTime"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['fileInformation:fileDownload'] }"
            link
            type="primary"
            @click="fileDownload(row)"
          >
            下载
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
  </div>
</template>

<script>
  import { getList,doAdd } from '@/api/basics/FileInformation'
  import { getroomList } from '@/api/devicefile/room'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'BasicsDevice',
    setup() {
      const state = reactive({
        editRef: null,
        list: [],
        roomlist: [],
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
      onMounted(() => {
      fetchData()
      setInterval(() => {
      fetchData();
      // 进行其他操作
    }, 2000);
    })
      const Download = async () => {
        import('@/utils/excel').then((excel) => {
          const tHeader = ['文件名称', '文件绝对路径', '上传时间']
          const filterVal = ['fileName', 'filePath', 'createdAt']
          const list = state.list
          const data = formatJson(filterVal, list)
          excel.export_json_to_excel({
            header: tHeader,
            data,
            filename: '文件记录导出文件',
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
      const fileDownload = async (row) => {
        if (row.fileRelativePath) {
         
          window.location.href = row.fileRelativePath
          await doAdd(row)
        }
      }
      const fetchData = async () => {
      //  state.listLoading = true
        console.log(state.queryForm)
        const {
          data: { list, total },
        } = await getList(state.queryForm)
       
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const roomdata=async()=>{
        const{ data: { list, total },}=  await getroomList()
        state.roomlist=list
        console.log(roomlist)
      }
      onMounted(() => {
        fetchData()
        roomdata()
      })
      return {
        ...toRefs(state),
        setSelectRows,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        Download,
        fileDownload,
        fetchData,
        roomdata,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
