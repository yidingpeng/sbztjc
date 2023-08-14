<template>
  <div class="problem-problemfeedback-container">
    <el-table
      v-loading="listLoading"
      border
      :data="list"
      scrollbar-always-on
      stripe
      @selection-change="setSelectRows"
    >
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
        width="200"
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
        width="150"
      />
      <!-- <el-table-column
        align="center"
        label="发送地址"
        prop="addressId"
        show-overflow-tooltip
        width="90"
      >
        <template #default="scope">
          <span v-if="scope.row.addressId == '1'">厂内</span>
          <span v-else-if="scope.row.addressId == '2'">厂外</span>
        </template>
      </el-table-column> -->
      <el-table-column
        align="center"
        label="问题类型"
        prop="problemTypeText"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="判定原因"
        prop="pfb_ExceptionDesc"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="反馈人"
        prop="userName"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="接收人"
        prop="solutionName"
        show-overflow-tooltip
        width="120"
      />
      <!-- <el-table-column
        align="center"
        label="处理动态"
        prop="dealWithDynamicTxt"
        show-overflow-tooltip
        width="200"
      /> -->
      <el-table-column align="center" label="处理动态" width="180">
        <template #default="{ row }">
          <el-tag
            v-if="row.dealWithDynamic == 'ProblemFBStatus1'"
            effect="light"
            style="color: red"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus2'"
            effect="light"
            style="color: #ff571a"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus3'"
            effect="light"
            style="color: #ff7c4d"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus4'"
            effect="light"
            style="color: green"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus5'"
            effect="light"
            style="color: #7ba428"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
          <el-tag
            v-else-if="row.dealWithDynamic == 'ProblemFBStatus6'"
            effect="light"
            style="color: #7ba428"
          >
            {{ row.dealWithDynamicTxt }}
          </el-tag>
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
  import { getList } from '@/api/problemProblemfeedback'

  export default defineComponent({
    name: 'ProblemProblemfeedback',
    setup() {
      const state = reactive({
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 15,
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
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.total = total

        state.listLoading = false
      }
      const statusFilter = (status) => {
        const statusMap = {
          1: 'warning',
          2: 'success',
          3: 'danger',
        }
        return statusMap[status]
      }
      onMounted(() => {
        fetchData()
        window.setInterval(() => {
          const xyy = Math.ceil(state.total / state.queryForm.pageSize)
          if (xyy == state.queryForm.pageNo) {
            state.queryForm.pageNo = 1
          } else {
            state.queryForm.pageNo = state.queryForm.pageNo + 1
          }
          setTimeout(fetchData, 0)
        }, 15000)
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleSizeChange,
        handleCurrentChange,
        fetchData,
        statusFilter,
      }
    },
  })
</script>
