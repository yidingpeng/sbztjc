<template>
  <div class="system-log-container">
    <vab-query-form>
      <vab-query-form-top-panel>
        <el-form
          :inline="true"
          label-width="70px"
          :model="queryForm"
          @submit.prevent
        >
          <el-form-item label="标题">
            <el-input
              v-model.trim="queryForm.title"
              clearable
              placeholder="请输入标题关键字"
            />
          </el-form-item>
          <el-form-item label="类型">
            <rw-dictionary
              ref="myDict"
              v-model="queryForm.messageType"
              default-value="所有类型"
              type="MessageType"
            />
            <!-- <el-select
              v-model="queryForm.messageType"
              clearable
              placeholder="消息类型筛选"
            >
              <el-option
                v-for="type in types"
                :key="type.value"
                :label="type.value"
                :value="type.key"
              />
            </el-select> -->
          </el-form-item>
          <el-form-item label="是否已读">
            <el-select
              v-model="queryForm.read"
              clearable
              placeholder="默认查询全部"
            >
              <el-option label="已读" :value="true">已读</el-option>
              <el-option label="未读" :value="false">未读</el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="发布时间">
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
      <el-table-column label="id" prop="id" />
      <el-table-column label="消息类型" prop="type" show-overflow-tooltip />
      <el-table-column label="标题" prop="title" show-overflow-tooltip>
        <template #default="{ row }">
          <a :href="'#/message/' + row.id" target="_self">
            {{ row.title }}
          </a>
        </template>
      </el-table-column>
      <el-table-column label="是否已读" prop="Read" show-overflow-tooltip>
        <template #default="{ row }">
          <span v-if="row.read">
            <span class="vab-dot vab-dot-success"><span></span></span>
            已读
          </span>
          <span v-else>
            <span class="vab-dot vab-dot-error"><span></span></span>
            未读
          </span>
        </template>
      </el-table-column>
      <el-table-column label="发布时间" prop="time" show-overflow-tooltip />
      <el-table-column label="操作">
        <template #default="{ row }">
          <el-button link target="_self" type="primary" @click="toDataUrl(row)">
            查看详情
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
  import { getList, doDelete } from '@/api/system/message'
  import { Search } from '@element-plus/icons-vue'
  import rwDictionary from '@/plugins/RwDictionary'
  import router from '~/src/router'

  export default defineComponent({
    name: 'SystemLog',
    components: { rwDictionary },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        queryForm: {
          messageType: '',
          pageNo: 1,
          pageSize: 20,
        },
      })
      // const { getByType, getValue } = useDictionaryStore()
      // var types = getByType('MessageType')
      // const getType = (key) => {
      //   return getValue('MessageType', key, '(null)')
      // }
      // state.types = types

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
        console.log('size changed')
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        console.log('current changed')
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        console.log('querydata load.')
        fetchData()
      }
      onMounted(async () => {
        console.log('mounted load.')
        fetchData()
      })

      const handleDelete = (row) => {
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await doDelete(row.id)
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

      const toDataUrl = (row) => {
        let url = ''
        switch (row.type) {
          case 'workflow':
            url = `/workflow/detail/${row.dataId}`
            break
          case 'inform':
            url = `/inform/detail/${row.dataId}`
            break
          default:
            break
        }
        console.log(`toDataUrl${row.type}${row.dataId}`)
        router.push(url)
      }

      return {
        ...toRefs(state),
        fetchData,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        Search,
        handleDelete,
        toDataUrl,
      }
    },
  })
</script>
