<template>
  <div class="contact-container">
    <vab-query-form>
      <vab-query-form-left-panel :span="24">
        <el-form :inline="true" :model="queryForm" @submit.prevent>
          <el-form-item label="干系人姓名">
            <el-input
              v-model.trim="queryForm.ContactsName"
              clearable
              placeholder="请输入干系人姓名"
            />
          </el-form-item>
          &nbsp;
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
          <el-form-item label="客户名称：" label-width="90px">
            <el-input
              v-model.trim="queryForm.clientName"
              clearable
              placeholder="请输入客户名称"
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
            <el-button :icon="Search" type="primary" @click="queryData">
              查询
            </el-button>
          </el-form-item>
        </el-form>
      </vab-query-form-left-panel>
      <vab-query-form-left-panel :span="12">
        <el-button
          v-permissions="{ permission: ['contact:add'] }"
          :icon="Plus"
          type="primary"
          @click="handleEdit($event)"
        >
          添加
        </el-button>
        <el-button
          v-permissions="{ permission: ['contact:delete'] }"
          :icon="Delete"
          type="danger"
          @click="handleDelete($event)"
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
        fixed
        label="姓名"
        prop="contactsName"
        show-overflow-tooltip
        width="80"
      />
      <el-table-column
        align="center"
        fixed
        label="现任职公司"
        prop="curCompany"
        show-overflow-tooltip
        width="250px"
      />
      <el-table-column
        align="center"
        label="单位编号"
        prop="comCode"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="部门"
        prop="departmenttext"
        show-overflow-tooltip
        width="105"
      />
      <el-table-column
        align="center"
        label="类别"
        prop="contactsCategorytext"
        show-overflow-tooltip
        width="80"
      />
      <el-table-column
        align="center"
        label="岗位"
        prop="post"
        show-overflow-tooltip
        width="108"
      />
      <el-table-column
        align="center"
        label="办公地址"
        prop="officeAddress"
        show-overflow-tooltip
        width="115"
      />
      <el-table-column
        align="center"
        label="客户名称"
        prop="clientName"
        show-overflow-tooltip
        width="118"
      />
      <el-table-column
        align="center"
        label="市场片区"
        prop="marketAreaTxt"
        show-overflow-tooltip
        width="115"
      />
      <el-table-column
        align="center"
        label="客户级别"
        prop="clientRankTxt"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="性别"
        prop="sex"
        show-overflow-tooltip
        width="65"
      >
        <template #default="scope">
          <span v-if="scope.row.sex == 1">男</span>
          <span v-if="scope.row.sex == 2">女</span>
          <span v-if="scope.row.sex == 3">保密</span>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="年龄"
        prop="age"
        show-overflow-tooltip
        width="85"
      >
        <template #default="scope">
          {{ scope.row.birthday != null ? scope.row.age : '暂未录入' }}
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="电话1"
        prop="telephone1"
        show-overflow-tooltip
        width="125"
      />
      <el-table-column
        align="center"
        label="电话2"
        prop="telephone2"
        show-overflow-tooltip
        width="125"
      />
      <el-table-column
        align="center"
        label="历史任职履历"
        prop="historyJob"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="办公电话"
        prop="tel"
        show-overflow-tooltip
        width="125"
      />
      <el-table-column
        align="center"
        label="传真"
        prop="fax"
        show-overflow-tooltip
        width="105"
      />
      <el-table-column
        align="center"
        label="民族"
        prop="nation"
        show-overflow-tooltip
        width="65"
      />
      <el-table-column
        align="center"
        label="负责业务"
        prop="responsibleBusiness"
        show-overflow-tooltip
        width="105"
      />
      <el-table-column
        align="center"
        label="邮箱"
        prop="email"
        show-overflow-tooltip
        width="135"
      />
      <el-table-column
        align="center"
        label="微信"
        prop="wechat"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="QQ"
        prop="qq"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="生日"
        prop="birthday"
        show-overflow-tooltip
        width="110"
      />
      <el-table-column
        align="center"
        label="爱好"
        prop="hobby"
        show-overflow-tooltip
        width="120"
      />
      <el-table-column
        align="center"
        label="本科学校"
        prop="college"
        show-overflow-tooltip
        width="102"
      />
      <el-table-column
        align="center"
        label="本科毕业时间"
        prop="collegeAt"
        show-overflow-tooltip
        width="110"
      />
      <el-table-column
        align="center"
        label="硕博学校"
        prop="thurberSchool"
        show-overflow-tooltip
        width="95"
      />
      <el-table-column
        align="center"
        label="硕博别业时间"
        prop="thurberSchoolAt"
        show-overflow-tooltip
        width="110"
      />
      <el-table-column
        align="center"
        label="行业人脉背景"
        prop="contactsBackground"
        show-overflow-tooltip
        width="100"
      />
      <el-table-column
        align="center"
        label="备注"
        prop="remark"
        show-overflow-tooltip
        width="135"
      />
      <el-table-column align="center" fixed="right" label="操作" width="70">
        <template #default="{ row }">
          <el-link
            v-permissions="{ permission: ['contact:edit'] }"
            title="编辑"
            :underline="false"
            @click="handleEdit(row)"
          >
            <vab-icon icon="edit-2-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['contact:delete'] }"
            title="删除"
            :underline="false"
            @click="handleDelete(row)"
          >
            <vab-icon icon="delete-bin-5-line" />
          </el-link>
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
    <edit ref="editRef" @fetch-data="fetchData" />
  </div>
</template>

<script>
  import {
    getList,
    doDelete,
    GetOrganizationNameList,
    GetSalseAreaSelect,
    GetDicClient,
  } from '@/api/contact'
  import { Delete, Plus, Search } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'Contact',
    components: {
      Edit: defineAsyncComponent(() => import('./components/ContactEdit')),
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        editRef: null,
        isShow: false,
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
          title: '',
        },
        SexOption: [
          {
            value: 1,
            label: '男',
          },
          {
            value: 2,
            label: '女',
          },
          {
            value: 3,
            label: '保密',
          },
        ],
        GetOrganizationNameOption: [],
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

      //部门
      const GetOrganizationNameData = async () => {
        const GetOrganizationName = await GetOrganizationNameList()
        console.log(GetOrganizationName)
        state.GetOrganizationNameOption = GetOrganizationName.data
      }

      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleEdit = (row) => {
        if (row.id) {
          state['editRef'].showEdit(row)
        } else {
          state['editRef'].showEdit()
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
        state.queryForm.sex =
          state.queryForm.sexvalue == '' ? 0 : state.queryForm.sexvalue
        state.queryForm.department =
          state.queryForm.departmentValue == ''
            ? 0
            : state.queryForm.departmentValue
        state.queryForm.pageNo = 1
        fetchData()
      }
      const changeDate = (val) => {
        if (val == '0001-01-01 00:00:00') {
          return null
        }
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
        } = await getList(state.queryForm)
        state.list = list
        state.list.forEach((item) => {
          item.birthday = changeDate(item.birthday)
          item.collegeAt = changeDate(item.collegeAt)
          item.thurberSchoolAt = changeDate(item.thurberSchoolAt)
        })
        state.total = total
        state.listLoading = false
      }
      onMounted(() => {
        fetchData()
        GetOrganizationNameData()
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
        GetOrganizationNameData,
        fetchMarketArea,
        fetchClientRantData,
        queryData,
        fetchData,
        changeDate,
        Delete,
        Plus,
        Search,
      }
    },
  })
</script>
<style lang="scss">
  html body .el-scrollbar__thumb:hover,
  html body[class*='vab-theme-'] .el-scrollbar__thumb:hover {
    background-color: #252525;
  }
  html body .el-scrollbar__thumb,
  html body[class*='vab-theme-'] .el-scrollbar__thumb {
    background-color: #424242;
  }
</style>
