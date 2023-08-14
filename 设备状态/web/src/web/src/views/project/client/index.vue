<template>
  <div class="client-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="市场片区">
            <el-select
              v-model="queryForm.marketAreaValue"
              clearable
              filterable
              style="width: 100%"
            >
              <el-option
                v-for="item in marketAreaOptions"
                :key="item.id"
                :label="item.areaNameAndPlaceName"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          &nbsp;
          <el-form-item label="客户全程：" label-width="90px">
            <el-input
              v-model.trim="queryForm.clientFullName"
              clearable
              placeholder="请输入客户名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="营销负责人：" label-width="100px">
            <el-input
              v-model.trim="queryForm.personInChargeName"
              clearable
              placeholder="请输入营销负责人"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="客户级别：" label-width="100px">
            <el-select v-model="queryForm.clientRankValue" clearable filterable>
              <el-option
                v-for="item in clientOptions"
                :key="item.code"
                :disabled="item.disabled"
                :label="item.name"
                :value="item.code"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-button
              :icon="Search"
              native-type="submit"
              type="primary"
              @click="queryData"
            >
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['client:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['client:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
    </vab-query-form>

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="单位编号"
        prop="companyCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="客户简称"
        prop="clientName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="客户全称"
        prop="clientFullName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="市场片区"
        prop="marketAreaTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="地址"
        prop="address"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="客户级别"
        prop="clientRankText"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="合作业务"
        prop="cooperativeBusinessText"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="公司董事长"
        prop="ceoName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="公司总经理"
        prop="gmName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="公司副总经理"
        prop="deputyGMName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="营销负责人"
        prop="personInChargeName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="创建日期"
        prop="createdAt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="100">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['client:detail'] }"
            title="详情"
            :underline="false"
            @click="handleDetail(row)"
          >
            <vab-icon icon="book-read-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['client:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['client:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import { defineComponent, onMounted, reactive, toRefs } from 'vue'
  import {
    GetClientCompany,
    doDelete,
    GetSalseAreaSelect,
    GetDicClient,
  } from '@/api/client'
  import Edit from './components/ClientEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  export default defineComponent({
    name: 'Client',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const router = useRouter()

      const state = reactive({
        editRef: null,
        rowTemplate: {
          companyCode: '',
          clientName: '',
          clientFullName: '',
          ownerName: '',
          address: '',
          clientRankText: '',
          cooperativeBusinessText: '',
          createdAt: '',
        },
        list: [],
        listLoading: true,
        clientOptions: [],
        marketAreaOptions: [],
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          companyCode: '',
        },
      })
      //市场片区下拉框
      const fetchMarketArea = async () => {
        const marketAList = await GetSalseAreaSelect()
        state.marketAreaOptions = marketAList.data
      }
      //客户级别下拉框
      const fetchClientRantData = async () => {
        const clientlist = await GetDicClient()
        state.clientOptions = clientlist.data
      }
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state.editRef.showEdit(row)
        } else {
          state.editRef.showEdit()
        }
      }
      //详情页
      const handleDetail = (row) => {
        if (row.id)
          router.push({
            path: '/project/client/clientDetail',
            query: { id: row.id, projectId: row.projectId },
          })
        else {
          $baseMessage(
            '请选择一行进行详情页跳转',
            'error',
            'vab-hey-message-error'
          )
        }
      }
      const handleDelete = (row) => {
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
        state.queryForm.clientRank =
          state.queryForm.clientRankValue === ''
            ? 0
            : state.queryForm.clientRankValue
        state.queryForm.marketArea =
          state.queryForm.marketAreaValue === ''
            ? 0
            : state.queryForm.marketAreaValue
        state.queryForm.pageNo = 1
        fetchData()
      }
      const changeDate = (val) => {
        const newDate = new Date(val)
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return `${y}-${m}-${d}`
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await GetClientCompany(state.queryForm)
        list.forEach((item) => {
          item.createdAt = changeDate(item.createdAt)
        })
        state.list = list
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
        fetchMarketArea()
        fetchClientRantData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        fetchMarketArea,
        fetchClientRantData,
        queryData,
        fetchData,
        changeDate,
        handleDetail,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
