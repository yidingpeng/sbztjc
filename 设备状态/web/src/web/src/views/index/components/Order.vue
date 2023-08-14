<template>
  <div class="order">
    <vab-card class="order-card2" shadow="hover" skeleton :skeleton-rows="10">
      <template #header>
        <span>
          <vab-icon icon="list-unordered" />
          订单
        </span>
      </template>

      <el-row class="order-card2-content">
        <el-col :span="24">
          <el-table
            v-loading="listLoading"
            border
            :data="list"
            highlight-current-row
          >
            <el-table-column
              align="center"
              label="id"
              prop="id"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="订单编号"
              prop="axleNumber"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="订单名字"
              prop="axleModel"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="RFID"
              prop="rfid"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="现在状态"
              prop="currentState"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="创建时间"
              prop="creationTime"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="操作人员"
              prop="operator"
              show-overflow-tooltip
            />
            <el-table-column
              align="center"
              label="是否删除"
              prop="isDeleted"
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
        </el-col>
      </el-row>
    </vab-card>
  </div>
</template>

<script>
  import { getAllList } from '@/api/orders/order'

  export default defineComponent({
    setup() {
      // const stopTimer = () => {
      //   clearInterval(timer.value)
      // }
      const state = reactive({
        list: [],
        total: 0,
        timer: null,
        layout: 'total, sizes, prev, pager, next, jumper',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          role: '',
        },
      })
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
        // const {
        //   data: { list, total },
        // } = await getList(state.queryForm)
        // state.list = list
        // console.log(list)
        // state.total = total
        const {
          data: { list, total },
        } = await getAllList(state.queryForm)
        state.list = list
        state.total = total

        state.listLoading = false
      }

      onMounted(() => {
        fetchData()
        // const newtatol = state.total
        // console.log(newtatol)
        setInterval(() => {
          fetchData()
          // console.log(state.total)
          // if (newtatol != state.total) window.confirm('来新订单嗯咯')
        }, 2000)
      })
      return {
        ...toRefs(state),
        handleSizeChange,
        handleCurrentChange,
        fetchData,
      }
    },
    beforeUnmount() {},
  })
</script>

<style lang="scss" scoped>
  .order {
    &-card1 {
      &-content {
        text-align: center;
      }

      :deep() {
        .el-card {
          &__header,
          &__body {
            color: var(--el-color-white) !important;
            background: linear-gradient(to right, #60b2fb, #6485f6);
          }
        }
      }
    }

    &-card2 {
      height: 705px;

      &-content {
        text-align: center;

        .order-chart {
          width: 100%;
          height: 296px;
        }
      }
    }

    margin-bottom: $base-margin;
  }
</style>
