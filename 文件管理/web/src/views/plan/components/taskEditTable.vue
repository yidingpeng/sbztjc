<template>
  <div>
    <el-table
      v-loading="loading"
      border
      :cell-class-name="
        ({ row, column }) => {
          return column.label == '任务' ? 'add-icon' : ''
        }
      "
      :data="tasks"
      :default-expand-all="true"
      max-height="540"
      row-key="id"
      @cell-click="handleCellClick"
    >
      <el-table-column fixed="left" type="selection" width="55" />
      <el-table-column align="center" fixed="left" label="序号" width="70">
        <template #default="{ row }">
          {{ row.order }}
        </template>
      </el-table-column>
      <el-table-column fixed="left" label="任务" prop="name" width="300">
        <template #default="{ row }">
          <el-link
            class="add-child-task"
            :underline="false"
            @click="
              () => {
                $baseMessage('工程师紧张开发中')
                // tasks = tasks
                //   .splice(0, $index + 1)
                //   .concat({ wbsName: 'emtpy' })
                //   .concat(tasks)
              }
            "
          >
            <vab-icon icon="add-line" />
          </el-link>
          <rw-edit-input
            v-model:edit="cellEdit[`name${row.id}`]"
            v-model:text="row.name"
            @change="savePropHandler(row, 'name')"
          />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="里程碑"
        prop="isMilestone"
        width="75"
      >
        <template #default="{ row }">
          <rw-edit-checkbox
            v-model:checked="row.isMilestone"
            @change="savePropHandler(row, 'isMilestone')"
          />
        </template>
      </el-table-column>
      <el-table-column align="center" label="工期" prop="duration" width="100">
        <template #default="{ row }">
          <rw-edit-number
            v-model:edit="cellEdit[`duration${row.id}`]"
            v-model:number="row.duration"
            @change="savePropHandler(row, 'duration')"
          />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="标准工时"
        prop="workingHours"
        width="100"
      >
        <template #default="{ row }">
          <rw-edit-number
            v-model:edit="cellEdit[`workingHours${row.id}`]"
            v-model:number="row.workingHours"
            @change="savePropHandler(row, 'workingHours')"
          />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="计划开始"
        prop="planStartDate"
        width="140"
      >
        <template #default="{ row }">
          <rw-edit-date
            v-model:edit="cellEdit[`planStartDate${row.id}`]"
            v-model:text="row.planStartDate"
            @change="savePropHandler(row, 'planStartDate')"
          />
        </template>
      </el-table-column>
      <el-table-column
        align="center"
        label="计划结束"
        prop="planEndDate"
        width="140"
      >
        <template #default="{ row }">
          <rw-edit-date
            v-model:edit="cellEdit[`planEndDate${row.id}`]"
            v-model:text="row.planEndDate"
            @change="savePropHandler(row, 'planEndDate')"
          />
        </template>
      </el-table-column>
      <el-table-column align="center" label="责任人" prop="owner" width="120">
        <template #default="{ row }">
          <rw-edit-user
            v-model="row.owner"
            v-model:edit="cellEdit[`owner${row.id}`]"
            @change="savePropHandler(row, 'owner')"
          />
        </template>
      </el-table-column>
      <el-table-column align="center" fixed="right" label="操作" width="120">
        <template #default="{ row }">
          <el-dropdown
            cursor="hand"
            trigger="click"
            @command="(cmd) => handleCommand(cmd, row)"
          >
            <span class="el-dropdown-link">
              <vab-icon icon="more-2-fill" />
            </span>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item command="up">
                  <vab-icon icon="arrow-up-line" />
                  上移
                </el-dropdown-item>
                <el-dropdown-item command="down">
                  <vab-icon icon="arrow-down-line" />
                  下移
                </el-dropdown-item>
                <el-dropdown-item command="delete">
                  <vab-icon icon="delete-bin-6-line" />
                  删除
                </el-dropdown-item>
                <el-dropdown-item command="clear" divided>
                  <vab-icon icon="delete-bin-6-fill" />
                  删除全部任务
                </el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        </template>
      </el-table-column>
    </el-table>

    <el-input
      v-model="addTaskName"
      class="quick-add"
      placeholder="快速输入,回车键提交,按Esc取消。"
      :prefix-icon="Plus"
      @keydown="handleInputTask"
    />
  </div>
</template>

<script>
  import {
    MoveTask,
    AddTask,
    DeleteTask,
    ClearTask,
    EditTaskFiled,
  } from '@/api/plan/plan'
  //import { doDelete } from '@/api/plan/plan'
  import RwEditInput from '@/plugins/RwEditCell/input'
  import RwEditCheckbox from '@/plugins/RwEditCell/checkbox'
  import RwEditNumber from '@/plugins/RwEditCell/number'
  import RwEditDate from '@/plugins/RwEditCell/date'
  import RwEditUser from '@/plugins/RwEditCell/user'

  import { Plus } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'TaskEditTable',
    components: {
      RwEditInput,
      RwEditCheckbox,
      RwEditNumber,
      RwEditDate,
      RwEditUser,
    },
    props: {
      planId: { type: Number, default: 0 },
      tasks: { type: Array, default: () => [] },
      loading: { type: Boolean, default: false },
    },
    emits: ['loadTaskData'],
    setup(props, { emit }) {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      //const emit = defineEmits(['loadTaskData'])

      const loadData = () => {
        // console.log('load', emit)
        // console.log('load', emit('loadTaskData'))
        emit('loadTaskData')
      }

      const state = reactive({
        tasks: props.tasks,
        cellEdit: [],
        editMode: false,
        addTaskName: '',
      })

      // const loadTaskData = async () => {
      //   console.log('loadTaskData')
      //   const { data: data2 } = await GetTaskList({
      //     planId: state.currentPlan.id,
      //   })
      //   state.tasks = data2
      // }

      const savePropHandler = (row, name) => {
        console.log('save', row.id, name, row[name])

        EditTaskFiled({ id: row.id, filedName: name, value: row[name] })
          .then((x) =>
            $baseMessage(x.msg, 'success', 'vab-hey-message-success')
          )
          .then(loadData)
      }

      const handleViewCommand = (cmd, row) => {
        console.log(cmd, row)
        $baseMessage('工程师正在开发中。。。')
      }

      const handleCellClick = (row, column) => {
        //console.log('cell click', state.editMode, row, column)
        //if (!state.editMode) return
        //console.log('edit', column.property + row.id)
        state.cellEdit[column.property + row.id] = true
      }

      const handleCommand = (cmd, row) => {
        const index = state.tasks.findIndex((x) => x == row)
        if (index == -1) {
          $baseMessage(
            '原始数据未找到，请刷新后重试！',
            'error',
            'vab-hey-message-error'
          )
          return
        }
        switch (cmd) {
          case 'up':
            if (index == 0)
              $baseMessage(
                '已经是第一条数据了，无法上移！',
                'error',
                'vab-hey-message-error'
              )
            else {
              const upId = state.tasks[index - 1].id
              const currentId = row.id

              MoveTask({ from: currentId, dest: upId })
                .then((x) => {
                  $baseMessage(x.msg, 'success', 'vab-hey-message-success')
                })
                .then(() => {
                  loadData()
                })
            }
            break
          case 'down':
            if (index == state.tasks.length - 1)
              $baseMessage(
                '已经是最后一条数据了，无法下移！',
                'error',
                'vab-hey-message-error'
              )
            else {
              const downId = state.tasks[index + 1].id
              const currentId = row.id

              MoveTask({ from: currentId, dest: downId })
                .then((x) => {
                  $baseMessage(x.msg, 'success', 'vab-hey-message-success')
                })
                .then(() => {
                  loadData()
                })
            }

            break
          case 'delete':
            DeleteTask({ taskId: row.id }).then((x) => {
              $baseMessage(x.msg, 'success', 'var-hey-message-success')
              loadData()
            })
            break
          case 'clear':
            $baseConfirm('确定要删除该计划下的所有任务吗？', null, () => {
              console.log('confirm ok clear')
              ClearTask({ planId: props.planId }).then((x) => {
                $baseMessage(x.msg, 'success', 'var-hey-message-success')
                loadData()
              })
            })

            break
        }
      }

      const handleInputTask = (e) => {
        if (e.key == 'Enter') {
          addTask()
        } else if (e.key == 'ESC') {
          exitEditTask()
        }
      }

      const addTask = async () => {
        if (!state.addTaskName) {
          $baseMessage(
            '名称不能为空，请输入名称后添加。',
            'error',
            'vab-hey-message-error'
          )

          return
        }
        const { msg } = await AddTask({
          planId: props.planId,
          taskName: state.addTaskName,
        })

        $baseMessage(msg, 'success', 'vab-hey-message-success')
        await loadData()
        state.addTaskName = ''
      }

      const exitEditTask = () => {
        state.editMode = false
      }

      return {
        ...toRefs(state),
        savePropHandler,
        handleViewCommand,
        handleCellClick,
        handleCommand,
        handleInputTask,
        loadData,
        Plus,
      }
    },
  })
</script>

<style scoped>
  .add-child-task {
    position: absolute;
    right: 15px;
    display: none;
    float: right;
    margin-top: 0;
    margin-bottom: 0;
    text-decoration: none;
  }
  .add-child-task i {
    font-size: 25px;
  }
  .add-icon:hover .add-child-task {
    display: inline-block;
  }
  .quick-add {
    margin-top: 10px;
  }
</style>
