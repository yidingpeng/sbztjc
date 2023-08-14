<template>
  <div style="height: 40px; padding: 10px 0px 0px 10px">
    <span
      style="font-size: 14px; font-weight: bold; color: black; cursor: pointer"
    >
      {{ project.projectName }}
    </span>
    &nbsp;&nbsp;
    <span
      v-if="!edit"
      style="font-size: 14px; color: #1890ff; cursor: pointer"
      @click="handleSwtichProject"
    >
      <vab-icon icon="exchange-box-line" style="font-size: 16px" />
      切换项目
    </span>
    <rw-project-code
      v-else
      ref="cascaderRef"
      v-model="project.projectCode"
      @change="handeleChange"
      @visible-change="handleVChange"
    />
  </div>
  <div class="container">
    <el-tabs v-model="activeName" style="height: 690px">
      <el-tab-pane name="xmjh">
        <template #label>
          <vab-icon icon="calendar-check-line" />
          <span class="fontClass">项目计划</span>
        </template>
        <el-row>
          <el-col :span="24">
            <div class="col-left" style="margin-top: 5px">
              <el-link
                :icon="Warning"
                size="large"
                style="font-size: 18px"
                :underline="false"
              />
              <el-dropdown @command="handlerSelectPlan">
                <el-link
                  :icon="Star"
                  style="padding: 4px 5px 0px 5px; font-size: 15px"
                  :underline="false"
                >
                  {{ currentPlan.planName }}
                </el-link>
                <template #dropdown>
                  <el-dropdown-menu>
                    <!-- <el-dropdown-item>
                      主计划
                      <el-link :icon="Star" :underline="false" />
                    </el-dropdown-item>
                    <el-dropdown-item divided>制造子计划</el-dropdown-item>
                    <el-dropdown-item>设计子计划</el-dropdown-item> -->

                    <el-dropdown-item
                      v-for="plan in project.plans"
                      :key="plan.id"
                      :command="plan.id"
                    >
                      {{ plan.planName }} ({{ plan.status }})
                      <el-link
                        v-if="plan.isMainPlan"
                        :icon="Star"
                        :underline="false"
                      />
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
              <el-button size="small">{{ currentPlan.status }}</el-button>

              <el-link
                v-if="editMode"
                :icon="Edit"
                :underline="false"
                @click="handleEditPlans"
              >
                编辑计划集
              </el-link>
            </div>

            <div class="col-right">
              <!--状态-->
              <el-dropdown v-if="!editMode" class="dropdown col-dropdown">
                <span class="el-dropdown-link">
                  状态
                  <!-- <el-icon class="el-icon--right"><arrow-down /></el-icon> -->
                  <vab-icon icon="arrow-down-s-line" />
                </span>
                <!-- 状态
                <vab-icon icon="arrow-drop-down-line" /> -->
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item>所有任务</el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle red" />
                      逾期任务
                    </el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle yellow" />
                      即将到期
                    </el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle blue" />
                      进行中
                    </el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle green1" />
                      未完成
                    </el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle green2" />
                      已完成
                    </el-dropdown-item>
                    <el-dropdown-item>
                      <span class="circle green3" />
                      未开始
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
              <!--显示-->
              <el-dropdown v-if="!editMode" class="dropdown col-dropdown">
                <span class="el-dropdown-link">
                  显示
                  <vab-icon icon="arrow-down-s-line" />
                </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item :icon="Plus">Action 1</el-dropdown-item>
                    <el-dropdown-item>Action 2</el-dropdown-item>
                    <el-dropdown-item>Action 3</el-dropdown-item>
                    <el-dropdown-item>Action 4</el-dropdown-item>
                    <el-dropdown-item>Action 5</el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
              <!--分组-->
              <el-dropdown v-if="!editMode" class="dropdown col-dropdown">
                <span class="el-dropdown-link">
                  分组
                  <vab-icon icon="arrow-down-s-line" />
                </span>
                <template #dropdown>
                  <el-dropdown-menu>
                    <el-dropdown-item :icon="Plus">Action 1</el-dropdown-item>
                    <el-dropdown-item>Action 2</el-dropdown-item>
                    <el-dropdown-item>Action 3</el-dropdown-item>
                    <el-dropdown-item>Action 4</el-dropdown-item>
                    <el-dropdown-item>Action 5</el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>

              <!--搜索-->
              <div v-if="!editMode" class="col-search">
                <el-input
                  v-model="filter.key"
                  placeholder="标题、人员搜索"
                  :suffix-icon="Search"
                />
              </div>
              <!--发布、导入导出-->
              <div v-if="editMode" class="col-publish">
                <el-button type="primary">发布计划</el-button>
                &nbsp;
                <el-dropdown plain split-button @command="handleImport">
                  导入导出
                  <template #dropdown>
                    <el-dropdown-menu>
                      <el-dropdown-item command="excel">
                        从Excel导入
                      </el-dropdown-item>
                      <el-dropdown-item command="template">
                        从模板导入
                      </el-dropdown-item>
                      <el-dropdown-item command="project">
                        从其他项目导入
                      </el-dropdown-item>
                      <el-dropdown-item command="outputExcel" divided>
                        导出Excel
                      </el-dropdown-item>
                    </el-dropdown-menu>
                  </template>
                </el-dropdown>
              </div>

              <!--显示模式-->
              <div class="col-mode">
                <el-radio-group v-model="editModeComputed">
                  <el-tooltip content="跟踪模式" effect="dark" placement="top">
                    <el-radio-button :label="false">
                      <vab-icon icon="list-check" />
                    </el-radio-button>
                  </el-tooltip>
                  <el-tooltip content="编辑模式" effect="dark" placement="top">
                    <el-radio-button :label="true">
                      <vab-icon icon="edit-line" />
                    </el-radio-button>
                  </el-tooltip>
                </el-radio-group>
              </div>
            </div>

            <div class="col-content-right2" style="display: none">
              &nbsp; &nbsp; &nbsp;
              <el-input
                placeholder="标题、人员搜索"
                style="width: 150px"
                :suffix-icon="Search"
              />
            </div>
          </el-col>
          <el-col :span="24" style="margin-top: 10px">
            <task-view-table
              v-if="!editMode"
              :loading="loading"
              :tasks="tasks"
            />
            <task-edit-table
              v-else
              :loading="loading"
              :plan-id="currentPlan.id"
              :tasks="tasks"
              @load-task-data="loadTaskData()"
            />
            <!-- <el-input
              v-if="editMode"
              v-model="addTaskName"
              placeholder="快速输入,回车键提交,按Esc取消。"
              :prefix-icon="Plus"
              @keydown="handleInputTask"
            /> -->
          </el-col>
        </el-row>
      </el-tab-pane>
      <!-- <el-tab-pane name="gdjh">
        <template #label>
          <vab-icon icon="calendar-check-line" />
          <span class="fontClass">滚动计划</span>
        </template>
        &nbsp;建设中...
      </el-tab-pane>
      <el-tab-pane name="ddjh">
        <template #label>
          <vab-icon icon="calendar-check-line" />
          <span class="fontClass">迭代计划</span>
        </template>
        &nbsp;建设中...
      </el-tab-pane>
      <el-tab-pane name="gantt">
        <template #label>
          <vab-icon icon="bar-chart-horizontal-line" />
          <span class="fontClass">甘特图</span>
        </template>
        &nbsp;建设中...
      </el-tab-pane>
      <el-tab-pane name="version">
        <template #label>
          <vab-icon icon="file-search-line" />
          <span class="fontClass">计划版本</span>
        </template>
        &nbsp;建设中...
      </el-tab-pane>
      <el-tab-pane name="stage">
        <template #label>
          <vab-icon icon="equalizer-line" />
          <span class="fontClass">项目阶段</span>
        </template>
        &nbsp;建设中...
      </el-tab-pane> -->
      <el-tab-pane name="parameter">
        <template #label>
          <vab-icon icon="file-settings-line" />
          <span class="fontClass">计划参数</span>
        </template>
        <plan-parameter v-model:model="currentPlan" />
      </el-tab-pane>
    </el-tabs>
  </div>
  <PlanEdit ref="PlanEditRef" @fetch-data="fetchData" />
  <import-plan
    ref="importPlanRef"
    :plan-id="currentPlan.id"
    @fetch-data="loadTaskData"
  />
</template>

<script>
  //import { doDelete } from '@/api/plan/plan'
  // import RwEditInput from '@/plugins/RwEditCell/input.vue'
  // import RwEditCheckbox from '@/plugins/RwEditCell/checkbox.vue'
  // import RwEditNumber from '@/plugins/RwEditCell/number.vue'
  // import RwEditDate from '@/plugins/RwEditCell/date.vue'
  // import RwEditUser from '@/plugins/RwEditCell/user.vue'

  import TaskViewTable from './components/taskViewTable.vue'
  import TaskEditTable from './components/taskEditTable.vue'
  import { GetProDataByCode } from '@/api/purchase/purchase'
  import {
    GetPlanList,
    GetTaskList,
    //MoveTask,
    //AddTask,
    //DeleteTask,
    //ClearTask,
    //EditTaskFiled,
  } from '@/api/plan/plan'
  import PlanEdit from './components/PlanEdit.vue'
  import ImportPlan from './import/index.vue'
  import PlanParameter from './components/planParameter.vue'
  import storeLocal from 'storejs'
  import {
    Delete,
    Edit,
    Plus,
    Search,
    Setting,
    Star,
    Warning,
  } from '@element-plus/icons-vue'
  import RwProjectCode from '@/plugins/RwProjectCode'
  import arrayToTree from 'array-to-tree'

  export default defineComponent({
    name: 'CustomTable',
    components: {
      RwProjectCode,
      PlanEdit,
      //RwEditInput,
      // RwEditCheckbox,
      // RwEditNumber,
      // RwEditDate,
      // RwEditUser,
      ImportPlan,
      PlanParameter,
      TaskViewTable,
      TaskEditTable,
    },

    setup() {
      //const $baseConfirm = inject('$baseConfirm')
      //const $baseMessage = inject('$baseMessage')
      const $baseTableHeight = inject('$baseTableHeight')

      const state = reactive({
        loading: false,
        PlanEditRef: null,
        importPlanRef: null,
        tableSortRef: null,
        editRef: null,
        cascaderRef: null,
        border: true,
        height: $baseTableHeight(1),
        stripe: false,
        lineHeight: 'small',
        filter: { key: '' },
        checkList: ['标题', '作者', '评级', '点击量', '时间'],
        columns: [
          {
            label: '标题',
            prop: 'title',
            sortable: true,
            disableCheck: true,
          },
          {
            label: '作者',
            prop: 'author',
            sortable: true,
          },
          {
            label: '评级',
            prop: 'rate',
            sortable: true,
          },
          {
            label: '点击量',
            prop: 'pageViews',
            sortable: true,
          },
          {
            label: '时间',
            prop: 'datetime',
            sortable: true,
          },
        ],
        list: [],
        imageList: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next, jumper',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 20,
          title: '',
        },
        cellEdit: [],
        editMode: false,
        project: {
          projectCode: storeLocal.get('projectCode'),
          projectName: storeLocal.get('projectName'),
          plans: [],
        },
        currentPlan: { id: 0, planName: '', status: '' },
        tasks: [],
        // projectName: '',
        // projectCode: storeLocal.get('projectCode'),
        edit: false,
        activeName: 'xmjh',
        addTaskName: '',
        //   radio1: '',
      })

      const editModeComputed = computed({
        get: () => state.editMode,
        set: (value) => {
          state.loading = true
          console.log(value, state.loadding)
          //加入动态加载效果
          setTimeout(() => {
            state.editMode = value
            state.loading = false
            console.log('completed')
          }, 500)
        },
      })

      const dragOptions = computed(() => {
        return {
          animation: 600,
          group: 'description',
        }
      })
      const finallyColumns = computed(() => {
        return state.columns.filter((item) =>
          state.checkList.includes(item.label)
        )
      })

      const fetchData = async () => {
        const { data } = await GetPlanList({
          projectCode: state.project.projectCode,
        })
        console.log(data)
        state.project.plans = data
        if (data.length > 0) {
          state.currentPlan = data[0]
          const { data: data2 } = await GetTaskList({
            planId: state.currentPlan.id,
          })
          state.tasks = data2
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

      const sondata = (data) => {
        return arrayToTree(data, {
          parentProperty: 'parentId',
          customID: 'id',
        })
      }

      const containerRef = ref()
      const { toggle, isFullscreen } = useFullscreen(containerRef)
      const clickFullScreen = () => {
        toggle().then(() => {
          handleHeight()
          state['tableSortRef'].doLayout()
        })
      }
      const handleHeight = () => {
        if (isFullscreen.value) state.height = $baseTableHeight(1) + 200
        else state.height = $baseTableHeight(1)
      }
      const setSelectRows = (val) => {
        state.selectRows = val
      }
      const handleAdd = () => {
        state['editRef'].showEdit()
      }
      const handleEdit = (row) => {
        state['editRef'].showEdit(row)
      }

      const handleImport = (cmd) => {
        console.log(cmd)
        state.importPlanRef.showEdit()
      }

      const handleSwtichProject = () => {
        // state.loading = true
        state.edit = !state.edit
        // state.loading = false
      }
      const handleVChange = (val) => {
        if (!val) {
          state.editMode = false
          storeLocal.set('projectCode', state.project.projectCode)
        }
      }
      const handeleChange = async (value) => {
        const data = await GetProDataByCode({ proCode: value[0] })
        const { projectCode, projectName } = data.data
        state.project.projectCode = projectCode
        state.project.projectName = projectName
        storeLocal.set('projectCode', projectCode)
        storeLocal.set('projectName', projectName)
        //console.log(data.data)
      }

      const handleRadioChange = (val) => {
        console.log(val)
      }

      const handleEditPlans = () => {
        state.PlanEditRef.showEdit()
      }

      const loadTaskData = async () => {
        const { data: data2 } = await GetTaskList({
          planId: state.currentPlan.id,
        })
        //state.tasks = data2

        state.tasks.splice(0, state.tasks.length)
        state.tasks.push(...data2)
      }

      onMounted(async () => {
        await fetchData()
      })

      // const handleInputTask = (e) => {
      //   if (e.key == 'Enter') {
      //     addTask()
      //   } else if (e.key == 'ESC') {
      //     exitEditTask()
      //   }
      // }

      // const addTask = async () => {
      //   var { msg } = await AddTask({
      //     planId: state.currentPlan.id,
      //     taskName: state.addTaskName,
      //   })

      //   $baseMessage(msg, 'success', 'vab-hey-message-success')
      //   await loadTaskData()
      //   state.addTaskName = ''
      // }

      // const exitEditTask = () => {
      //   state.editMode = false
      // }

      const columnDisplay = (row, column) => {
        console.log(row, column)
      }

      const handlerSelectPlan = (planId) => {
        const plan = state.project.plans.find((x) => x.id == planId)
        console.log('change plan', plan)
        state.currentPlan = plan
        loadTaskData()
      }

      return {
        ...toRefs(state),
        editModeComputed,
        dragOptions,
        containerRef,
        isFullscreen,
        finallyColumns,
        columnDisplay,
        handleSizeChange,
        handleCurrentChange,
        queryData,
        loadTaskData,
        sondata,
        handleHeight,
        setSelectRows,
        clickFullScreen,
        fetchData,
        handleAdd,
        handleEdit,
        handleImport,
        //handleDelete,
        handleSwtichProject,
        handleVChange,
        handeleChange,
        handleEditPlans,
        handleRadioChange,
        //handleInputTask,
        handlerSelectPlan,
        Plus,
        Delete,
        Search,
        Setting,
        Star,
        Edit,
        Warning,
      }
    },
  })
</script>
<style scoped>
  .fontClass {
    font-size: 16px;
  }
  .col-left {
    float: left;
  }
  .col-right {
    float: right;
    vertical-align: middle;
  }

  .col-dropdown {
    margin-top: 10px;
    margin-right: 10px;
  }

  .col-search {
    display: inline-flex;
    width: 150px;
    margin-right: 10px;
    font-size: 0;
  }
  .col-mode {
    display: inline-flex;
  }
  .col-publish {
    display: inline-flex;
    margin-right: 10px;
  }

  .col-mode .el-radio-group {
    font-size: 16px;
  }

  .col-content-left {
    float: left;
    width: 100px;
    padding-top: 3px;
    margin-left: 20px;
  }
  .col-content-right {
    float: right;
    width: 260px;
  }
  .col-content-right2 {
    float: right;
    width: 350px;
  }
  .dropdown {
    cursor: pointer;
  }

  .el-dropdown-link {
    cursor: pointer;
  }

  .circle {
    width: 15px;
    height: 15px;
    margin-right: 5px;
    border-radius: 50%;
  }
  .red {
    background-color: rgb(219, 58, 58);
  }
  .yellow {
    background-color: gold;
  }
  .blue {
    background-color: rgb(122, 122, 197);
  }
  .green1 {
    background-color: #56df6d;
  }
  .green2 {
    background-color: #1c8a2e;
  }
  .green3 {
    background-color: #038118;
  }
</style>
