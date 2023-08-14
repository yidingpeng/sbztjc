<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-table :key="toggleIndex" ref="tableRef" border :data="steps">
      <el-table-column align="center" label="序号" width="80">
        <template #default="{ $index }">
          {{ $index + 1 }}
        </template>
      </el-table-column>
      <el-table-column label="阶段">
        <template #default="{ row }">
          <div class="step-item">
            <span
              v-if="!row.isEdit"
              class="edit-label"
              style="cursor: pointer"
              @click="handleStartEdit(row)"
            >
              {{ row.stageName }}
            </span>
            <el-input
              v-else
              v-model="row.originalStageName"
              :autofocus="true"
              class="edit-input"
              @blur="handleEdit(row)"
              @focus="row.originalStageName = row.stageName"
              @keydown="handleInputEdit($event, row)"
            />
          </div>
        </template>
      </el-table-column>
      <el-table-column align="center" label="操作" width="120">
        <template #default="{ row, $index }">
          <el-popconfirm
            cancel-button-text="取消"
            confirm-button-text="确定"
            title="确定要删除此项？"
            @confirm="handleRemove(row, $index)"
          >
            <template #reference>
              <el-link :underline="false">删除</el-link>
            </template>
          </el-popconfirm>
          <el-link
            v-permissions="{ permission: ['projectTemplate:move'] }"
            title="上移"
            :underline="false"
            @click="handleMove(row, 'down')"
          >
            <vab-icon icon="arrow-up-circle-line" />
          </el-link>
          <el-link
            v-permissions="{ permission: ['projectTemplate:move'] }"
            title="下移"
            :underline="false"
            @click="handleMove(row, 'up')"
          >
            <vab-icon icon="arrow-down-circle-line" />
          </el-link>
        </template>
      </el-table-column>
    </el-table>
    <el-input
      v-model="addName"
      placeholder="快速输入,回车键提交,按Esc取消。"
      :prefix-icon="Plus"
      @keydown="handleInput"
    />
  </el-dialog>
</template>

<script>
  import { Plus } from '@element-plus/icons-vue'
  //import Sortable from 'sortablejs'
  import {
    StageAddOrEdit,
    StageDelete,
    getList,
    //StageSortEdit,
    GetSort,
    MoveEdit,
  } from '@/api/projectTemplate/index'

  export default defineComponent({
    name: 'TemplateStepEdit',
    setup() {
      const state = reactive({
        tableRef: null,
        dialogFormVisible: false,
        title: '阶段配置',
        addName: '',
        steps: [],
        toggleIndex: 0,
        pid: null,
      })

      //let tableRef1 = ref(null)

      onMounted(() => {
        //initDrag()
        //console.log('mounted', tableRef1.value)
      })

      onActivated(() => {
        //console.log('actived', tableRef1.value)
      })

      // const initDrag = () => {
      //   //console.log('initdrag=============')
      //   //console.log(state.tableRef)
      //   if (!state.tableRef) return
      //   const tbody = state.tableRef.$el.querySelector(
      //     '.el-table__body-wrapper tbody'
      //   )
      //   Sortable.create(tbody, {
      //     handle: '.vab-rank',
      //     animation: 300,
      //     async onEnd({ newIndex, oldIndex }) {
      //       console.log('change', oldIndex, newIndex)
      //       if (oldIndex == newIndex) return
      //       const tableData = state.steps
      //       console.log(tableData)
      //       const oldRow = tableData[oldIndex]
      //       const newRow = tableData[newIndex]

      //       console.log('moved', oldRow, newRow)
      //       let data = []
      //       data.push(oldRow)
      //       data.push(newRow)
      //       const msg = await StageSortEdit(data)
      //       ElMessage({
      //         type: msg.msg.includes('成功') ? 'success' : 'error',
      //         message: `${msg.msg}`,
      //       })
      //       //await moveTemplate({ fromId: oldRow.id, toId: newRow.id })

      //       const currRow = tableData.splice(oldIndex, 1)[0]
      //       tableData.splice(newIndex, 0, currRow)

      //       state.toggleIndex += 1
      //       nextTick(() => {
      //         initDrag()
      //       })
      //     },
      //   })
      // }

      //输入框输入时触发，Enter确定，ESC取消
      const handleInput = (e) => {
        if (e.key == 'Enter') {
          addNewName()
        } else if (e.key == 'Escape') {
          state.addName = ''
        }
      }

      const handleInputEdit = (e, row) => {
        if (e.key == 'Enter') handleEdit(row)
        else if (e.key == 'Escape') {
          row.isEdit = false
          row.originalStageName = row.stageName
        }
      }

      const addNewName = async () => {
        console.log(`invoke add new name is {${state.addName}}`)
        const msg = await StageAddOrEdit({
          id: 0,
          TemplateId: state.pid,
          StageName: state.addName,
        })
        ElMessage({
          type: msg.msg.includes('成功') ? 'success' : 'error',
          message: `${msg.msg}`,
        })
        await GetList(state.pid)
        //state.steps.push({ id: 0, stepName: state.addName, orderIndex: 0 })
        state.addName = ''
      }

      const handleRemove = async (row, index) => {
        console.log(`remove step ${index}`)
        console.log(row)
        const msg = await StageDelete({ ids: row.id })
        ElMessage({
          type: msg.msg.includes('成功') ? 'success' : 'error',
          message: `${msg.msg}`,
        })
        await GetList(state.pid)
        //state.steps.splice(index, 1)
      }

      const handleStartEdit = (row) => {
        row.isEdit = !row.isEdit
        if (!row.originalStageName) row.originalStageName = row.stageName
      }
      //失焦事件
      const handleEdit = async (row) => {
        row.isEdit = false
        console.log('edit', row.stageName, row.originalStageName)
        if (row.stageName == row.originalStageName) return
        row.stageName = row.originalStageName
        const msg = await StageAddOrEdit(row)
        ElMessage({
          type: msg.msg.includes('成功') ? 'success' : 'error',
          message: `${msg.msg}`,
        })
        await GetList(state.pid)
        row.stageName = row.originalStageName
      }

      const showEdit = async (row) => {
        console.log('ready data==================')
        console.log(row)
        await GetList(row.id)
        //state.steps = row.steps
        state.pid = row.id
        state.dialogFormVisible = true

        //initDrag()
      }

      const GetList = async (id) => {
        const data = await getList({ TemplateId: id })
        state.steps = data.data.map((v) => {
          v.isEdit = false
          v.originalStageName = v.stageName
          return v
        })
      }
      const close = () => {
        state.steps = []
        state.dialogFormVisible = false
      }
      const handleMove = async (row, type) => {
        const sort = await GetSort({ ParentId: row.templateId, Type: type })
        console.log(sort.data, row.sort)
        if (sort.data == row.sort) {
          ElMessage({
            type: 'warning',
            message: type == 'up' ? '已经到底啦!' : '已经到顶啦!',
          })
        } else {
          const { msg } = await MoveEdit({
            Id: row.id,
            ParentId: row.templateId,
            Type: type,
          })
          ElMessage({
            type: msg.includes('成功') ? 'success' : 'error',
            message: `${msg}`,
          })
          await GetList(state.pid)
        }
      }
      return {
        ...toRefs(state),
        Plus,
        //initDrag,
        handleInput,
        showEdit,
        close,
        handleRemove,
        handleInputEdit,
        handleEdit,
        handleStartEdit,
        GetList,
        handleMove,
      }
    },
  })
</script>
