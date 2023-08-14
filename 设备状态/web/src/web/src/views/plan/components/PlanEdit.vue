<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-table
      border
      :data="list"
      max-height="540"
      row-key="id"
      @cell-dblclick="handleCellClick"
    >
      <el-table-column label="计划" prop="planName">
        <template #default="{ row }">
          <div class="step-item">
            <span
              v-if="!row.isEdit"
              style="cursor: pointer"
              @click="handleSpanClick(row, 'planName')"
            >
              {{ row.planName }}
            </span>
            <el-input
              v-else
              v-model="row.oPlanName"
              :autofocus="true"
              style="width: 90%"
              @blur="handleBlur('planName', row)"
              @focus="row.oPlanName = row.planName"
              @keydown="handleEnterEsc($event, row, 'planName')"
            />
          </div>
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="责任人"
        prop="personLiable"
        width="120"
      >
        <template #default="{ row }">
          <span
            v-if="row.personLiable == '' && !row.isEdit1"
            style="cursor: pointer"
            @click="handleSpanClick(row, 'personLiable')"
          >
            <vab-icon icon="add-fill" style="font-size: 26px" />
          </span>
          <rw-user
            v-if="row.isEdit1"
            v-model="row.oPersonLiable"
            @change="handleChange(row, 'personLiable')"
            @visible-change="
              (val) => handleVisibleChange(val, 'personLiable', row)
            "
          />
          <span
            v-else
            style="cursor: pointer"
            @click="handleSpanClick(row, 'personLiable')"
          >
            {{ row.personLiable }}
          </span>
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row }">
          <el-button type="primary" @click="handleDelClick(row)">删</el-button>
        </template>
      </el-table-column>
    </el-table>
    <div style="padding-top: 5px">
      <el-input
        v-model="addName"
        placeholder="添加任务,按Enter确定,ESC取消"
        :prefix-icon="Plus"
        @keydown="handleKeyDown"
      />
    </div>
  </el-dialog>
</template>

<script>
  import { GetPlanList, PlanAddOrEdit, PlanDelete } from '@/api/plan/plan'
  import RwUser from '@/plugins/RwUserName'
  import storeLocal from 'storejs'
  import { Plus } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'PlanEdit',
    components: { RwUser },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseConfirm = inject('$baseConfirm')
      //const $baseMessage = inject('$baseMessage')

      const state = reactive({
        title: '编辑计划集',
        list: [],
        addName: '',
        dialogFormVisible: false,
      })

      const showEdit = (row) => {
        state.form = Object.assign({}, row)
        state.dialogFormVisible = true
      }
      //回车和Esc事件
      const handleKeyDown = async (e) => {
        if (e.key == 'Enter') {
          const data = {
            Id: 0,
            ProjectCode: storeLocal.get('projectCode'),
            PlanName: state.addName,
            PersonLiable: '',
            Order: 0,
            IsMainPlan: 1,
          }
          const msg = await PlanAddOrEdit(data)
          ElMessage({
            type: msg.msg.includes('成功') ? 'success' : 'error',
            message: msg.msg,
          })
          await fetchData()
          state.addName = ''
          emit('fetch-data')
        } else if (e.key == 'Escape') {
          state.addName = ''
        }
      }

      const handleSpanClick = (row, name) => {
        if (name == 'planName') {
          row.isEdit = !row.isEdit
          if (!row.oPlanName) row.oPlanName = row.planName
        } else if (name == 'personLiable') {
          row.isEdit1 = !row.isEdit1
          if (!row.oPersonLiable) row.oPersonLiable = row.personLiable
        }
      }

      //input失焦事件
      const handleBlur = async (name, row) => {
        if (name == 'planName') {
          row.isEdit = false
          if (row.planName == row.oPlanName) return
          row.planName = row.oPlanName
        }
        const { msg } = await PlanAddOrEdit(row)
        ElMessage({
          type: msg.includes('成功') ? 'success' : 'error',
          message: msg,
        })
        await fetchData()
      }
      //列表格双击事件(单击事件可能和列表中的控件产生冲突,使用双击)
      const handleCellClick = async (row, column) => {
        if (column.property == 'planName') {
          row.isEdit = !row.isEdit
          if (!row.oPlanName) row.oPlanName = row.planName
        } else if (column.property == 'personLiable') {
          row.isEdit1 = !row.isEdit1
          if (!row.oPersonLiable) row.oPersonLiable = row.personLiable
        }
      }
      //加载计划集
      const fetchData = async () => {
        const list = await GetPlanList({
          ProjectCode: storeLocal.get('projectCode'),
        })
        //console.log(list.data)
        list.data.forEach((item) => {
          if (item.planList) {
            item.planList.map((v) => {
              v.isEdit = false
              v.isEdit1 = false
              v.oPlanName = v.planName
              v.oPersonLiable = v.personLiable
              return v
            })
          }
        })
        state.list = list.data
      }
      //删除计划集
      const handleDelClick = async (row) => {
        //console.log(row)
        if (row.id) {
          $baseConfirm('你确定要删除当前项吗', null, async () => {
            const { msg } = await PlanDelete({ ids: row.id })
            ElMessage({
              type: msg.includes('成功') ? 'success' : 'error',
              message: msg,
            })
            await fetchData()
          })
        }
      }
      const close = () => {
        state.dialogFormVisible = false
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        Plus,
        showEdit,
        close,
        handleKeyDown,
        handleSpanClick,
        handleBlur,
        handleCellClick,
        handleDelClick,
      }
    },
  })
</script>
