<template>
  <div class="system-configuration-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['material:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['material:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete"
        >
          批量删除
        </el-button>
      </vab-query-form-left-panel>
      <vab-query-form-right-panel :span="12">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item>
            <el-input
              v-model.trim="queryForm.MtlName"
              clearable
              placeholder="请输入物料/零件名称"
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

    <el-table
      v-loading="listLoading"
      border
      :data="list"
      @row-click="tabRowClick"
      @row-dblclick="handleEdit"
      @selection-change="setSelectRows"
    >
      <el-table-column show-overflow-tooltip type="selection" />
      <el-table-column
        align="center"
        label="物料编码"
        prop="mtlCode"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料/零件名称"
        prop="mtlName"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="规格型号"
        prop="mtlModel"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="图号"
        prop="wlFigureNo"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料分类"
        prop="mtlClassTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="物料类型"
        prop="mtlTypeTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="基本单位"
        prop="baseUnitTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="重要度"
        prop="importanceTxt"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="是否录入物料编码"
        prop="mtlIsHasCode"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.mtlIsHasCode == 0" effect="dark" type="success">
            是
          </el-tag>
          <el-tag v-else effect="dark" type="danger">否</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="购买日期"
        prop="purchaseTime"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="生效时间"
        prop="effectTime"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        :formatter="dateFormat"
        label="失效时间"
        prop="failuretime"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="图片" prop="picId" width="60">
        <template #default="scope">
          <!-- @click="fetchInsertFileData" -->
          <el-link v-if="scope.row.picId">查看</el-link>
          <span v-else></span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="材质"
        prop="textureTxt"
        show-overflow-tooltip
      />
      <!-- <el-table-column
        align="center"
        label="重量"
        prop="weight"
        show-overflow-tooltip
      />
      <el-table-column
        align="center"
        label="尺寸"
        prop="size"
        show-overflow-tooltip
      /> -->
      <el-table-column
        align="center"
        label="状态"
        prop="mtlStatus"
        show-overflow-tooltip
      >
        <template #default="{ row }">
          <el-tag v-if="row.mtlStatus == 0" effect="dark" type="success">
            启用
          </el-tag>
          <el-tag v-else effect="dark" type="danger">禁用</el-tag>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
      />
      <el-table-column align="center" label="操作" width="180">
        <template #default="{ row }">
          <el-button
            v-permissions="{ permission: ['material:edit'] }"
            link
            type="primary"
            @click="handleEdit(row)"
          >
            编辑
          </el-button>
          <el-button
            v-permissions="{ permission: ['material:delete'] }"
            link
            type="primary"
            @click="handleDelete(row)"
          >
            删除
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
    <edit ref="editRef" @fetch-data="fetchData" />

    <el-dialog
      v-model="dialogPicVisible"
      style="width: 70%"
      @close="PicDigClose"
    >
      <img
        v-for="item in imgUrls"
        :key="item.value"
        class="prewImg"
        :src="item"
        style="width: 100%"
      />
    </el-dialog>
  </div>
</template>

<script>
  import {
    getList,
    doDelete,
    GetImageByPtid,
    getMaterialImg,
  } from '@/api/basics/material'
  import Edit from './components/BasicsMaterialEdit'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'
  import moment from 'moment'

  export default defineComponent({
    name: 'BasicsMaterial',
    components: { Edit },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        dialogPicVisible: false,
        url: '',
        imgUrls: [],
        editRef: null,
        list: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          MtlName: '',
        },
      })
      //根据id获取反馈图片ids
      const GetPicIds = async (param) => {
        const picData = await GetImageByPtid({
          pid: param,
        })
        state.picId = picData.ids
        state.picId.forEach((item) => {
          GetPic(item)
        })
      }
      //获取图片
      const GetPic = async (param) => {
        const picurl = await getMaterialImg({
          id: param,
        })
        state.imgUrls.push(picurl.url)
        state.dialogPicVisible = true
      }

      const PicDigClose = () => {
        state.imgUrls = []
      }

      const tabRowClick = async (row, column) => {
        if (row.picId) {
          if (column.label == '图片') {
            GetPicIds(row.id)
          }
        }
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
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getList(state.queryForm)
        state.list = list
        state.total = total
        state.listLoading = false
      }
      const dateFormat = (row, column) => {
        let date = null
        if (column.property == 'purchaseTime') {
          date = row.purchaseTime
        } else if (column.property == 'effectTime') {
          date = row.effectTime
        } else if (column.property == 'failuretime') {
          date = row.failuretime
        }
        if (date == null) {
          return ''
        }
        return moment(date).format('YYYY-MM-DD')
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
        PicDigClose,
        tabRowClick,
        dateFormat,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
