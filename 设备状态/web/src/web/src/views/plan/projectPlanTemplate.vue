<template>
  <div class="project-plan-template-container">
    <div class="row">
      <el-button :icon="ArrowLeft" type="default" @click="goBack">
        返回
      </el-button>
      <div class="right">
        <el-dropdown split-button @command="handleMore">
          更多
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="edit">
                <vab-icon icon="edit-line" />
                修改计划名称
              </el-dropdown-item>
              <el-dropdown-item command="delete">
                <vab-icon icon="delete-bin-5-line" />
                删除计划名称
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
      <div class="right">
        <el-button :icon="Plus" type="primary" @click="OpenMessageBox('add')">
          添加计划
        </el-button>
      </div>
    </div>
    <div class="row" style="padding-top: 10px">
      <el-tabs
        v-model="activeName"
        style="height: 600px"
        @tab-click="handleClick"
      >
        <el-tab-pane
          v-for="item in list"
          :key="item.id"
          :label="item.planName"
          :name="item.id"
        >
          <el-table
            border
            :data="sondata(item.wbsPlan)"
            :default-expand-all="true"
            max-height="540"
            row-key="id"
            @cell-dblclick="handleCellClick"
          >
            <el-table-column align="center" label="序号" width="80">
              <template #default="{ $index }">
                {{ $index + 1 }}
              </template>
            </el-table-column>
            <el-table-column align="center" label="任务" prop="wbsName">
              <template #default="{ row }">
                <div class="step-item">
                  <span
                    v-if="!row.isEdit"
                    style="cursor: pointer"
                    @click="handleSpanClick(row, 'wbsName')"
                  >
                    {{ row.wbsName }}
                  </span>
                  <el-input
                    v-else
                    v-model="row.originalwbsName"
                    :autofocus="true"
                    style="width: 90%"
                    @blur="handleBlur('wbsName', row)"
                    @focus="row.originalwbsName = row.wbsName"
                    @keydown="handleEnterEsc($event, row, 'wbsName')"
                  />
                  <span
                    style="float: right; margin-top: 5px; cursor: pointer"
                    @click="handleSonTask(row)"
                  >
                    <vab-icon icon="add-fill" style="font-size: 26px" />
                    <!-- <el-button :icon="Plus" link title="新增任务" /> -->
                  </span>
                </div>
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="里程碑"
              prop="milestone"
              width="120px"
            >
              <template #default="{ row }">
                <el-checkbox
                  :checked="row.milestone == 0 ? false : true"
                  size="large"
                  @change="handleChange(row, 'milestone')"
                />
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="工期"
              prop="duration"
              width="120"
            >
              <template #default="{ row }">
                <span
                  v-if="!row.isEdit1"
                  style="cursor: pointer"
                  @click="handleSpanClick(row, 'duration')"
                >
                  {{ row.duration }}
                </span>
                <el-input
                  v-else
                  v-model="row.oDuration"
                  autofocus="true"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="handleBlur('duration', row)"
                  @focus="row.oDuration = row.duration"
                  @keydown="handleEnterEsc($event, row, 'duration')"
                />
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="标准工时"
              prop="workingHours"
              width="120"
            >
              <template #default="{ row }">
                <span
                  v-if="!row.isEdit2"
                  style="cursor: pointer"
                  @click="handleSpanClick(row, 'workingHours')"
                >
                  {{ row.workingHours }}
                </span>
                <el-input
                  v-else
                  v-model="row.oWorkingHours"
                  autofocus="true"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="handleBlur('workingHours', row)"
                  @focus="row.oWorkingHours = row.workingHours"
                  @keydown="handleEnterEsc($event, row, 'workingHours')"
                />
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="关联阶段"
              prop="correlationStage"
              width="120"
            >
              <template #default="{ row }">
                <span
                  v-if="!row.isEdit3"
                  @click="handleSpanClick(row, 'correlationStage')"
                >
                  {{ row.correlationStage }}
                </span>
                <el-select
                  v-else
                  v-model="row.oCorrelationStage"
                  @change="handleChange(row, 'correlationStage')"
                  @visible-change="
                    (val) => handleVisibleChange(val, 'correlationStage', row)
                  "
                >
                  <el-option
                    v-for="stage in stageOption"
                    :key="stage.id"
                    :label="stage.stageName"
                    :value="stage.stageName"
                  />
                </el-select>
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="责任角色"
              prop="dutyRole"
              width="120"
            >
              <template #default="{ row }">
                <span
                  v-if="row.dutyRole == '' && !row.isEdit4"
                  style="margin-top: 5px; cursor: pointer"
                  @click="handleSpanClick(row, 'dutyRole')"
                >
                  <vab-icon icon="add-fill" style="font-size: 26px" />
                </span>
                <rw-role
                  v-if="row.isEdit4"
                  v-model="row.oDutyRole"
                  :multiple="false"
                  @change="handleChange(row, 'dutyRole')"
                  @visible-change="
                    (val) => handleVisibleChange(val, 'dutyRole', row)
                  "
                />
                <span
                  v-else
                  style="cursor: pointer"
                  @click="handleSpanClick(row, 'dutyRole')"
                >
                  {{ row.dutyRole }}
                </span>
              </template>
            </el-table-column>
            <el-table-column
              align="center"
              label="任务类型"
              prop="taskType"
              width="120"
            />
            <el-table-column
              align="center"
              label="审核人"
              prop="auditor"
              width="120"
            >
              <template #default="{ row }">
                <span
                  v-if="row.auditor == '' && !row.isEdit6"
                  style="margin-top: 5px; cursor: pointer"
                  @click="handleSpanClick(row, 'auditor')"
                >
                  <vab-icon icon="add-fill" style="font-size: 26px" />
                </span>
                <rw-user
                  v-if="row.isEdit6"
                  v-model="row.oAuditor"
                  @change="handleChange(row, 'auditor')"
                  @visible-change="
                    (val) => handleVisibleChange(val, 'auditor', row)
                  "
                />
                <span
                  v-else
                  style="cursor: pointer"
                  @click="handleSpanClick(row, 'auditor')"
                >
                  {{ row.auditor }}
                </span>
              </template>
            </el-table-column>
            <el-table-column align="center" label="操作" width="120">
              <template #default="{ row }">
                <el-dropdown trigger="click" @command="handleCommand">
                  <span class="el-dropdown-link">
                    <vab-icon icon="more-2-fill" />
                  </span>
                  <template #dropdown>
                    <el-dropdown-menu>
                      <el-dropdown-item :command="composeValue(row, 'up')">
                        <vab-icon icon="arrow-up-line" />
                        上移
                      </el-dropdown-item>
                      <el-dropdown-item :command="composeValue(row, 'down')">
                        <vab-icon icon="arrow-down-line" />
                        下移
                      </el-dropdown-item>
                      <el-dropdown-item :command="composeValue(row, 'delete')">
                        <vab-icon icon="delete-bin-6-line" />
                        删除
                      </el-dropdown-item>
                    </el-dropdown-menu>
                  </template>
                </el-dropdown>
              </template>
            </el-table-column>
          </el-table>
        </el-tab-pane>
      </el-tabs>
      <div style="padding-top: 5px">
        <el-input
          v-model="addName"
          placeholder="添加任务,按Enter确定,ESC取消"
          :prefix-icon="Plus"
          @keydown="handleKeyDown"
        />
      </div>
    </div>
  </div>
</template>

<script>
  import {
    ArrowLeft,
    CaretBottom,
    CaretTop,
    DeleteFilled,
    Expand,
    Fold,
    Plus,
  } from '@element-plus/icons-vue'

  import {
    //addTask,
    // setTaskAction,
    // editTaskFiled,
    // removeTask,
    GetTempLateList,
    WBSTabsDoAddOrEdit,
    WBSDoAddOrEdit,
    WBSMoveEdit,
    WBSPlanGetSort,
    WBSTabsDelete,
    WBSPlanDelete,
  } from '@/api/projectTemplate/task'
  import { getList } from '@/api/projectTemplate/index'
  import { handleActivePath } from '@/utils/routes'
  import { useTabsStore } from '@/store/modules/tabs'
  import arrayToTree from 'array-to-tree'
  import RwRole from '@/plugins/RwRole'
  import RwUser from '@/plugins/RwUserName'

  export default defineComponent({
    name: '项目计划模板',
    components: { RwRole, RwUser },
    setup() {
      const route = useRoute()
      const router = useRouter()
      const tabsStore = useTabsStore()
      const { delVisitedRoute } = tabsStore
      const state = reactive({
        addName: '',
        plan: { title: '军工产品模板' },
        tasks: [],
        editTask: { editFiled: '' },
        list: [],
        pid: 0,
        activeName: '',
        stageOption: [],
        roleOption: [],
      })
      onMounted(async () => {
        await GetData()
        await StageList()
      })
      const OpenMessageBox = (type) => {
        let inputValue = ''
        state.list.forEach((item) => {
          if (state.pid == item.id) {
            inputValue = item.planName
          }
        })
        ElMessageBox.prompt(
          '请输入计划名称:',
          type == 'add' ? '新增计划名称' : '修改计划名称',
          {
            confirmButtonText: '提交',
            inputPattern: /^[\u4E00-\u9FA5A-Za-z0-9]+$/,
            inputErrorMessage: '可由汉字、字母、数字组成',
            inputValue: type == 'add' ? '' : inputValue,
          }
        )
          .then(async ({ value }) => {
            const array = state.list.map((item) => item.id)
            const min = Math.min.apply(null, array)
            if (state.pid > min || type == 'add') {
              const data = {
                Id: type == 'add' ? 0 : state.pid,
                TemplateId: route.query.id,
                PlanName: value,
                State: 0,
              }
              const msg = await WBSTabsDoAddOrEdit(data)
              ElMessage({
                type: msg.msg.includes('成功') ? 'success' : 'error',
                message: msg.msg,
              })
              await GetData()
            } else {
              ElMessage({
                type: 'error',
                message: '不支持此操作!',
              })
            }
          })
          .catch(() => {})
      }
      //
      const handleKeyDown = async (e) => {
        if (e.key == 'Enter') {
          if (state.pid > 0) {
            const data = {
              Id: 0,
              TemplateId: state.pid,
              ParentId: 0,
              WbsName: state.addName,
              Milestone: 0,
              Duration: 1,
              WorkingHours: 8,
              CorrelationStage: '',
              DutyRole: '',
              TaskType: '',
              Auditor: '',
              State: 0,
            }
            const msg = await WBSDoAddOrEdit(data)
            ElMessage({
              type: msg.msg.includes('成功') ? 'success' : 'error',
              message: msg.msg,
            })
            //console.log(msg.msg)
            await GetData()
            state.addName = ''
          }
        } else if (e.key == 'Escape') {
          state.addName = ''
        }
      }
      //新增任务子项
      const AddWbsPlan = async (row) => {
        const data = {
          Id: 0,
          TemplateId: state.pid,
          ParentId: row.id,
          WbsName: row.originalwbsName,
          Milestone: 0,
          Duration: 1,
          WorkingHours: 8,
          CorrelationStage: '',
          DutyRole: '',
          TaskType: '',
          Auditor: '',
          State: 0,
        }
        const msg = await WBSDoAddOrEdit(data)
        ElMessage({
          type: msg.msg.includes('成功') ? 'success' : 'error',
          message: msg.msg,
        })
        await GetData()
      }
      //更多操作
      const handleMore = async (command) => {
        console.log('the command is : ', command)
        let msg = null
        const array = state.list.map((item) => item.id)
        const min = Math.min.apply(null, array)
        switch (command) {
          case 'edit':
            if (state.pid > min) {
              OpenMessageBox('edit')
            } else {
              ElMessage({
                type: 'error',
                message: '不支持此操作!',
              })
            }
            break
          case 'delete':
            if (state.pid > min) {
              msg = await WBSTabsDelete({ Id: state.pid })
              ElMessage({
                type: msg.msg.includes('成功') ? 'success' : 'error',
                message: msg.msg,
              })
              await GetData()
            } else {
              ElMessage({
                type: 'error',
                message: '不支持此操作!',
              })
            }
            break
        }
      }
      //执行菜单命令
      const handleCommand = async (command) => {
        console.log('the command is : ', command)
        let msg = null,
          sort = null
        switch (command.type) {
          case 'add':
            break
          case 'upgrade':
          case 'degrade':
          case 'up':
            sort = await WBSPlanGetSort({
              TemplateId: command.row.templateId,
              Type: 'up',
            })
            console.log(sort.data)
            if (sort.data == command.row.sort) {
              ElMessage({
                type: 'warning',
                message: '已经到顶啦!',
              })
            } else {
              msg = await WBSMoveEdit({
                Id: command.row.id,
                TemplateId: command.row.templateId,
                Type: 'up',
              })
              ElMessage({
                type: msg.msg.includes('成功') ? 'success' : 'error',
                message: msg.msg,
              })
              await GetData()
            }
            break
          case 'down':
            sort = await WBSPlanGetSort({
              TemplateId: command.row.templateId,
              Type: 'down',
            })
            console.log(sort.data)
            if (sort.data == command.row.sort) {
              ElMessage({
                type: 'warning',
                message: '已经到底啦!',
              })
            } else {
              msg = await WBSMoveEdit({
                Id: command.row.id,
                TemplateId: command.row.templateId,
                Type: 'down',
              })
              ElMessage({
                type: msg.msg.includes('成功') ? 'success' : 'error',
                message: msg.msg,
              })
              await GetData()
            }
            break
          case 'delete':
            msg = await WBSPlanDelete({ Id: command.row.id })
            ElMessage({
              type: msg.msg.includes('成功') ? 'success' : 'error',
              message: msg.msg,
            })
            await GetData()
            break
        }
      }
      //任务名称参数方法
      const composeValue = (row, type) => {
        return {
          row: row,
          type: type,
        }
      }
      //添加
      // const handleAdd = (name) => {
      //   addTask({ name })
      // }

      //开始编辑
      const handleStartEdit = (row, editFiled) => {
        for (const index in state.tasks) state.tasks[index].isEdit = false

        row.isEdit = true
        state.editTask = JSON.parse(JSON.stringify(row))
        state.editTask.editFiled = editFiled
      }

      //结束编辑
      const handleEditFiled = (row, filedName, value) => {
        if (row[filedName] == value) {
          row.isEdit = false
          return
        }
        editTaskFiled({ id: row.id, filedName, value })
        row.isEdit = false
      }
      const goBack = async () => {
        const detailPath = handleActivePath(route, true)
        await router.push('/plan/projectTemplate')
        delVisitedRoute(detailPath)
      }
      //加载列表数据
      const GetData = async () => {
        const data = await GetTempLateList({
          TemplateId: route.query.id,
        })
        if (state.pid == 0) {
          state.activeName = data.data[0].id
          state.pid = data.data[0].id
        } else {
          data.data.forEach((item) => {
            if (state.pid == item.id) {
              state.activeName = item.id
            }
          })
        }
        data.data.forEach((item) => {
          item.wbsPlan.map((v) => {
            v.isEdit = false
            v.isEdit1 = false
            v.isEdit2 = false
            v.isEdit3 = false
            v.isEdit4 = false
            v.isEdit5 = false
            v.isEdit6 = false
            v.originalwbsName = v.wbsName
            v.omilestone = v.milestone
            v.oDuration = v.duration
            v.oWorkingHours = v.workingHours
            v.oCorrelationStage = v.correlationStage
            v.oDutyRole = v.dutyRole
            v.oTaskType = v.taskType
            v.oAuditor = v.auditor
            return v
          })
        })
        state.list = data.data
        //console.log(data.data)
      }

      const sondata = (data) => {
        return arrayToTree(data, {
          parentProperty: 'parentId',
          customID: 'id',
        })
      }
      const handleSpanClick = (row, name) => {
        if (name == 'wbsName') {
          row.isEdit = !row.isEdit
          if (!row.originalwbsName) row.originalwbsName = row.wbsName
        } else if (name == 'duration') {
          row.isEdit1 = !row.isEdit1
          if (!row.oDuration) row.oDuration = row.duration
        } else if (name == 'workingHours') {
          row.isEdit2 = !row.isEdit2
          if (!row.oWorkingHours) row.oWorkingHours = row.workingHours
        } else if (name == 'correlationStage') {
          row.isEdit3 = !row.isEdit3
          if (!row.oCorrelationStage)
            row.oCorrelationStage = row.correlationStage
        } else if (name == 'dutyRole') {
          row.isEdit4 = !row.isEdit4
          if (!row.oDutyRole) row.oDutyRole = row.dutyRole
        } else if (name == 'auditor') {
          row.isEdit6 = !row.isEdit6
          if (!row.oAuditor) row.oAuditor = row.auditor
        }
      }
      //列表格双击事件(单击事件可能和列表中的控件产生冲突,使用双击)
      const handleCellClick = async (row, column) => {
        if (column.property == 'wbsName') {
          row.isEdit = !row.isEdit
          if (!row.originalwbsName) row.originalwbsName = row.wbsName
        } else if (column.property == 'duration') {
          row.isEdit1 = !row.isEdit1
          if (!row.oDuration) row.oDuration = row.duration
        } else if (column.property == 'workingHours') {
          row.isEdit2 = !row.isEdit2
          if (!row.oWorkingHours) row.oWorkingHours = row.workingHours
        } else if (column.property == 'correlationStage') {
          row.isEdit3 = !row.isEdit3
          if (!row.oCorrelationStage)
            row.oCorrelationStage = row.correlationStage
        }
      }
      //input失焦事件
      const handleBlur = async (name, row) => {
        if (name == 'wbsName') {
          row.isEdit = false
          if (row.wbsName == row.originalwbsName) return
          row.wbsName = row.originalwbsName
        } else if (name == 'duration') {
          row.isEdit1 = false
          if (row.duration == row.oDuration || !isNumber(row.oDuration)) return
          row.duration = row.oDuration
        } else if (name == 'workingHours') {
          row.isEdit2 = false
          if (
            row.workingHours == row.oWorkingHours ||
            !isNumber(row.oWorkingHours)
          )
            return
          row.workingHours = row.oWorkingHours
        }
        const { msg } = await WBSDoAddOrEdit(row)
        ElMessage({
          type: msg.includes('成功') ? 'success' : 'error',
          message: msg,
        })
        await GetData()
      }

      //责任角色下拉框出现/隐藏时触发
      const handleVisibleChange = async (val, name, row) => {
        //下拉框隐藏时如果值相等则隐藏下拉框
        if (
          val == false &&
          name == 'correlationStage' &&
          row.correlationStage == row.oCorrelationStage
        ) {
          row.isEdit3 = false
        } else if (
          val == false &&
          name == 'dutyRole' &&
          row.dutyRole == row.oDutyRole
        ) {
          row.isEdit4 = false
        } else if (
          val == false &&
          name == 'auditor' &&
          row.auditor == row.oAuditor
        ) {
          row.isEdit6 = false
        }
      }
      //input回车Esc事件
      const handleEnterEsc = async (e, row, name) => {
        if (e.key == 'Enter') {
          await handleBlur(name, row)
        } else if (e.key == 'Escape') {
          if (name == 'wbsName') {
            row.isEdit = false
            row.originalwbsName = row.wbsName
          } else if (name == 'duration') {
            row.isEdit1 = false
            row.oDuration = row.duration
          } else if (name == 'workingHours') {
            row.isEdit2 = false
            row.oWorkingHours = row.workingHours
          }
        }
      }
      const handleClick = (TabsPaneContext) => {
        state.pid = TabsPaneContext.paneName
      }
      //下拉列表值改变事件
      const handleChange = async (row, name) => {
        if (name == 'milestone') {
          const omilestone = row.milestone == 1 ? 0 : 1
          row.milestone = omilestone
        } else if (name == 'correlationStage') {
          if (row.correlationStage == row.oCorrelationStage) return
          row.correlationStage = row.oCorrelationStage
          row.isEdit3 = false
        } else if (name == 'dutyRole') {
          if (row.dutyRole == row.oDutyRole) return
          row.dutyRole = row.oDutyRole
          row.isEdit4 = false
        } else if (name == 'auditor') {
          if (row.auditor == row.oAuditor) return
          row.auditor = row.oAuditor
          row.isEdit6 = false
        }
        const { msg } = await WBSDoAddOrEdit(row)
        ElMessage({
          type: msg.includes('成功') ? 'success' : 'error',
          message: msg,
        })
        await GetData()
      }
      //表格列悬浮进入事件
      const handleMouseEnter = async (row, column, cell, event) => {
        console.log(row, column, cell, event)
        if (column.property == 'wbsName') {
          row.isEdit5 = true
        }
      }
      //表格列悬浮退出事件
      const handleMouseLeave = (row, column) => {
        if (column.property == 'wbsName') {
          row.isEdit = false
          row.isEdit5 = false
        } else if (column.property == 'duration') {
          row.isEdit1 = false
        } else if (column.property == 'workingHours') {
          row.isEdit2 = false
        } else if (column.property == 'correlationStage') {
          row.isEdit3 = false
        }
        console.log(row)
      }
      //新增任务列表子项
      const handleSonTask = async (row) => {
        ElMessageBox.prompt(
          '请输入子任务名称:',
          `新增 ${row.wbsName} 任务子项`,
          {
            confirmButtonText: '提交',
            inputPattern: /^[\u4E00-\u9FA5A-Za-z0-9]+$/,
            inputErrorMessage: '可由汉字、字母、数字组成',
          }
        )
          .then(async ({ value }) => {
            row.originalwbsName = value
            await AddWbsPlan(row)
          })
          .catch(() => {})
      }
      //获取阶段列表
      const StageList = async () => {
        const list = await getList({ TemplateId: route.query.id })
        state.stageOption = list.data
        //console.log(list.data)
      }
      //判断是否是数字
      const isNumber = (val) => {
        const regPos = /^\d+(\.\d+)?$/ //非负浮点数
        const regNeg =
          /^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$/ //负浮点数
        if (regPos.test(val) || regNeg.test(val)) {
          return true
        } else {
          return false
        }
      }
      return {
        ...toRefs(state),
        ArrowLeft,
        Plus,
        CaretBottom,
        CaretTop,
        DeleteFilled,
        Expand,
        Fold,
        handleCommand,
        handleStartEdit,
        handleEditFiled,
        goBack,
        handleClick,
        OpenMessageBox,
        handleKeyDown,
        composeValue,
        handleMore,
        handleChange,
        isNumber,
        handleMouseEnter,
        handleSonTask,
        AddWbsPlan,
        sondata,
        handleCellClick,
        StageList,
        handleVisibleChange,
        handleMouseLeave,
        handleSpanClick,
        handleBlur,
        handleEnterEsc,
      }
    },
  })
</script>

<style scoped>
  .project-plan-template-container {
    padding: 10px;
  }
  .project-title {
    margin: 5px;
    margin-left: 20px;
    font-size: 20px;
    font-weight: bold;
    vertical-align: middle;
  }
  .task-title {
    margin: 10px;
    font-size: 18px;
  }
  .right {
    float: right;
    padding-right: 10px;
  }
</style>
