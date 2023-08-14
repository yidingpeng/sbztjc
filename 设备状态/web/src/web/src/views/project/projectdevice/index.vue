<template>
  <div class="project-projectretmoney-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="设备名称：">
            <el-input
              v-model.trim="queryForm.deviceName"
              clearable
              placeholder="请输入设备名称"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="设备编号：">
            <el-input
              v-model.trim="queryForm.deviceNo"
              clearable
              placeholder="请输入设备编号"
            />
          </el-form-item>
          &nbsp;
          <el-form-item label="项目编号：">
            <el-input
              v-model.trim="queryForm.projectCode"
              clearable
              placeholder="请输入项目编号"
            />
          </el-form-item>
          <el-form-item>
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['projectdevice:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['projectdevice:delete'] }"
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
      :data="list"
      @selection-change="setSelectRows"
    >
      <el-table-column align="center" show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="设备编号"
        prop="deviceNo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="设备名称"
        prop="deviceName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目编号"
        prop="projectCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="项目名称"
        prop="projectName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="产品系列"
        prop="productLineName"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="数量"
        prop="qty"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="单价"
        prop="devicePriceShow"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="单位"
        prop="deviceUnit"
        show-overflow-tooltip
        width="200"
      />
      <el-table-column
        align="center"
        label="要求交付日期"
        prop="deliverDate"
        show-overflow-tooltip
        width="200"
      />
      <el-table-column
        align="center"
        label="设备主要功能"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="设备使用地点"
        prop="devicePlaceOfUse"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['projectdevice:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['projectdevice:delete'] }"
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
  import { getList, doDelete } from '@/api/projectdevice'
  import Edit from './components/ProjectDeviceEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  // import moment from 'moment'

  export default defineComponent({
    name: 'Projectdevice',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          deviceName: '',
        },
      })

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
        state.queryForm.pageNo = 1
        fetchData()
      }
      //金额转换为千分位
      const formatCurrency = (value, str) => {
        if (!str) str = ''
        // str 规定货币类型
        if (value == '0') return '0.00'
        if (value == '.') return ''
        if (!value) return ''
        let val = Number(value).toFixed(2) // 提前保留两位小数
        const intPart = parseInt(val) // 获取整数部分
        const intPartFormat = intPart
          .toString()
          .replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断 ？？？
        let floatPart = '.00' // 预定义小数部分
        val = val.toString() // 将number类型转为字符串类型，方便操作
        const value2Array = val.split('.')
        if (value2Array.length === 2) {
          // =2表示数据有小数位
          floatPart = value2Array[1].toString() // 拿到小数部分
          if (floatPart.length === 1) {
            // 补0,实际上用不着
            return `${str + intPartFormat}.${floatPart}0`
          } else {
            return `${str + intPartFormat}.${floatPart}`
          }
        } else {
          return str + intPartFormat + floatPart
        }
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.devicePriceShow = formatCurrency(item.devicePrice)
        })
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        setSelectRows,
        handleEdit,
        handleDelete,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        formatCurrency,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
