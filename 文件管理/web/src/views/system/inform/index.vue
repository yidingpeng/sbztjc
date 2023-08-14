<template>
  <div class="system-inform-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="6">
        <el-link href="#/inform/publish" type="primary">新建公告</el-link>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="18">
        <el-form
          :inline="true"
          label-width="70px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="标题">
            <el-input
              v-model.trim="queryForm.key"
              clearable
              placeholder="请输入标题名称"
            />
          </el-form-item>
          <el-form-item label="类型">
            <rw-dictionary
              ref="rwdict"
              v-model="queryForm.type"
              default-value="所有类型"
              type="NoticeType"
            />
            <!-- <el-select
              v-model="queryForm.type"
              clearable
              placeholder="为空表示全部"
            >
              <el-option
                v-for="item in types"
                :key="item.code"
                :value="item.value"
              />
            </el-select> -->
          </el-form-item>
          <el-form-item label="发布时间">
            <el-date-picker
              v-model="searchDate"
              end-placeholder="结束日期"
              format="YYYY-MM-DD"
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
      </vab-query-form-right-panel>
    </vab-query-form>

    <el-table v-loading="listLoading" :data="list">
      <el-table-column
        align="center"
        label="标题"
        prop="title"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="类型"
        prop="type"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="发布时间"
        prop="time"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="修改时间"
        prop="updateTime"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="发布状态"
        prop="status"
        show-overflow-tooltip
        width="80"
      />
      <el-table-column
        align="center"
        label="已阅人数"
        prop="readCount"
        show-overflow-tooltip
        width="80"
      />
      <el-table-column
        align="center"
        label="操作"
        prop="operation"
        :width="200"
      >
        <template #default="{ row }">
          <el-button link type="primary" @click="handlePreview(row)">
            预览
          </el-button>
          <el-button
            v-if="row.status == 0"
            link
            type="primary"
            @click="handlePublish(row)"
          >
            发布
          </el-button>
          <el-button
            v-if="row.status == 0"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-if="row.status == 1"
            link
            type="primary"
            @click="handleCancel(row)"
          >
            撤回
          </el-button>
          <el-button link type="primary" @click="handleDelete(row)">
            删除
          </el-button>
        </template>
      </el-table-column>
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
  import {
    getList,
    doDelete,
    getTypeList,
    doPublish,
  } from '@/api/system/inform'
  import { Search } from '@element-plus/icons-vue'
  import router from '@/router'
  import rwDictionary from '@/plugins/RwDictionary'

  export default defineComponent({
    name: 'InformList',
    components: { rwDictionary },
    setup() {
      const state = reactive({
        list: [],
        total: 0,
        types: [],
        layout: 'total, sizes, prev, pager, next, jumper',
        listLoading: false,
        searchDate: [],
        queryForm: {
          key: '',
          type: '',
          startTime: '',
          endTime: '',
          pageNo: 1,
          pageSize: 20,
        },
      })
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

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
      const handleDelete = async (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete(row.id)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            $baseConfirm('你确定要删除选中项吗', null, async () => {
              const { msg } = await doDelete(row.id)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }

      const handleEdit = (row) => {
        if (row.id) {
          router.push(`/inform/publish/${row.id}`)
        } else {
          router.push('publish')
        }
      }

      const handlePublish = (row) => {
        $baseConfirm(`确定要发布公告[${row.title}]吗？`, null, async () => {
          const { msg } = await doPublish(row.id)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        })
      }

      const handlePreview = (row) => {
        router.push(`/inform/detail/${row.id}`)
      }

      const handleCancel = function (row) {
        console.log(row)
      }

      const queryData = () => {
        state.queryForm.pageNo = 1
        //debugger
        if (state.searchDate) {
          if (state.searchDate.length > 0)
            state.queryForm.startTime = foramtDate(state.searchDate[0])
          if (state.searchDate.length > 1)
            state.queryForm.endTime = foramtDate(state.searchDate[1])
        } else {
          state.queryForm.startTime = null
          state.queryForm.endTime = null
        }
        fetchData()
      }

      const foramtDate = (date) => {
        return `${1900 + date.getYear()}-${
          date.getMonth() + 1
        }-${date.getDate()}`
      }

      onMounted(async () => {
        fetchData()

        const { data } = await getTypeList()
        state.types = data
      })
      return {
        ...toRefs(state),
        fetchData,
        handleSizeChange,
        handleCurrentChange,
        handleDelete,
        handleEdit,
        handlePublish,
        handlePreview,
        handleCancel,
        queryData,
        Search,
      }
    },
  })
</script>
