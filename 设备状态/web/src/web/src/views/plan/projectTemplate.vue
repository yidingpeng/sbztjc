<template>
  <div class="plan-protemplate-container">
    <el-row :gutter="20">
      <vab-query-form>
        <vab-query-form-top-panel :span="24">
          <el-form inline :model="queryForm" @submit.prevent>
            <el-form-item>
              <el-input
                v-model.trim="queryForm.title"
                clearable
                placeholder="请输入标题"
              />
            </el-form-item>
            <el-form-item>
              <el-button :icon="Search" type="primary" @click="queryData">
                查询
              </el-button>
            </el-form-item>
          </el-form>
        </vab-query-form-top-panel>
        <vab-query-form-left-panel :span="24">
          <el-button :icon="Plus" type="primary" @click="handleAdd">
            添加
          </el-button>
        </vab-query-form-left-panel>
      </vab-query-form>
      <el-col v-if="emptyShow" :span="24">
        <el-empty class="vab-data-empty" description="暂无数据" />
      </el-col>
      <el-col :span="24">
        <ul v-loading="listLoading">
          <li v-for="(item, index) in list" :key="index" class="list-item">
            <el-descriptions size="large">
              <template #title>
                <span>{{ item.name }}</span>
                <el-divider direction="vertical" />
                <el-button link type="primary" @click="handleEdit(item)">
                  <vab-icon icon="edit-2-line" />
                  编辑
                </el-button>
                <el-divider direction="vertical" />
                <el-button link type="primary" @click="handleDelete(item)">
                  <vab-icon icon="delete-bin-5-line" />
                  删除
                </el-button>
                <el-divider direction="vertical" />
                <el-popconfirm
                  cancel-button-text="取消"
                  confirm-button-text="确定"
                  icon-color="#626AEF"
                  :title="
                    item.state == true ? '是否确认停用?' : '是否确认启用?'
                  "
                  @cancel="cancelEvent(item.id)"
                  @confirm="confirmEvent(item.id)"
                >
                  <template #reference>
                    <el-button link type="primary">
                      <vab-icon v-if="item.state == true" icon="album-fill" />
                      <vab-icon v-else icon="album-line" />
                      {{ item.state == true ? '停用' : '启用' }}
                    </el-button>
                  </template>
                </el-popconfirm>
              </template>
              <template #extra>
                <el-link
                  :icon="Operation"
                  :underline="false"
                  @click="handleEditStep(item)"
                >
                  配置阶段
                </el-link>
                <el-link
                  :icon="Operation"
                  :underline="false"
                  @click="handleWBS(item)"
                >
                  配置WBS计划
                </el-link>
              </template>
              <el-descriptions-item label="项目类型" width="150px">
                {{ item.projectTypeName }}
              </el-descriptions-item>
              <el-descriptions-item label="创建日期">
                {{ item.createdAt }}
              </el-descriptions-item>
            </el-descriptions>
            <el-steps align-center>
              <el-step
                v-for="step in item.steps"
                :key="step"
                status="success"
                :title="step.stepName"
              />
              <!-- <el-step title="Step 1" />
              <el-step title="Step 2" />
              <el-step title="Step 3" />
              <el-step title="Step 3" />
              <el-step title="Step 3" />
              <el-step title="Step 3" />
              <el-step title="Step 3" />
              <el-step title="Step 3" /> -->
            </el-steps>
          </li>
        </ul>
      </el-col>
      <el-col :span="24">
        <el-pagination
          background
          :current-page="queryForm.pageNo"
          :layout="layout"
          :page-size="queryForm.pageSize"
          :total="total"
          @current-change="handleCurrentChange"
          @size-change="handleSizeChange"
        />
      </el-col>
    </el-row>
    <edit ref="editRef" @fetch-data="fetchData" />
    <edit-step ref="editStepRef" />
    <!-- <edit2 ref="stageEditRef" @fetch-data="fetchData" /> -->
  </div>
</template>

<script>
  import { MBgetList, MBdoDelete, DoEditState } from '@/api/task'
  import {
    Plus,
    Search,
    Operation,
    Setting,
    Edit as IconEdit,
  } from '@element-plus/icons-vue'

  import EditStep from './components/TemplateStepEdit'

  export default defineComponent({
    name: 'List',
    components: {
      Edit: defineAsyncComponent(() => import('./components/TemplateEdit')),
      EditStep,
      // Edit2: defineAsyncComponent(() => import('./components/StageEdit')),
    },
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const router = useRouter()
      const state = reactive({
        editRef: null,
        stageEditRef: null,
        editStepRef: null,
        list: [],
        total: 0,
        queryForm: { pageNo: 1, pageSize: 10, title: '' },
        layout: 'total, sizes, prev, pager, next, jumper',
        listLoading: true,
        switchloading: false,
        emptyShow: true,
      })
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await MBgetList(state.queryForm)
        //console.log(list)
        state.list = list
        state.total = total
        state.listLoading = false
        state.emptyShow = false
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
      const handleAdd = () => {
        state['editRef'].showEdit()
      }
      const handleEdit = (row) => {
        state['editRef'].showEdit(row)
      }
      const handleEdit2 = (row) => {
        state['stageEditRef'].showEdit(row)
      }
      const handleEditStep = (row) => {
        console.log('----------以上是EditStep-----------')
        state['editStepRef'].showEdit(row)
      }
      const handleDelete = (row) => {
        const msgTxt = '确定删除当前项?'
        if (row.id) {
          $baseConfirm(msgTxt, null, async () => {
            const { msg } = await MBdoDelete({ ids: row.id })
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            await fetchData()
          })
        } else {
          if (state.selectRows.length > 0) {
            const ids = state.selectRows.map((item) => item.id).join()
            $baseConfirm(msgTxt, null, async () => {
              const { msg } = await MBdoDelete({ ids })
              $baseMessage(msg, 'success', 'vab-hey-message-success')
              await fetchData()
            })
          } else {
            $baseMessage('未选中任何行', 'error', 'vab-hey-message-error')
          }
        }
      }
      const confirmEvent = async (item) => {
        const { msg } = await DoEditState({ Id: item })
        $baseMessage(msg, 'success', 'vab-hey-message-success')
        await fetchData()
      }
      const cancelEvent = (item) => {
        console.log(item)
      }

      const handleWBS = (row) => {
        if (row.id)
          router.push({
            path: '/plan/projectPlanTemplate',
            query: {
              id: row.id,
            },
          })
        else {
          $baseMessage('请选择一行进行跳转', 'error', 'vab-hey-message-error')
        }
      }
      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleAdd,
        handleEdit,
        handleEdit2,
        handleDelete,
        handleEditStep,
        MBdoDelete,
        DoEditState,
        //handleCommand,
        confirmEvent,
        cancelEvent,
        handleWBS,
        Search,
        Operation,
        Setting,
        IconEdit,
        Plus,
      }
    },
  })
</script>

<style lang="scss" scoped>
  .plan-protemplate-container {
    ul {
      padding: 0;
      margin: 0;
      list-style: none;
      outline: none;

      .list-item {
        padding: $base-padding;
        border-bottom: 1px solid $base-border-color;

        &-meta {
          display: flex;
          flex: 1 1;
          align-items: flex-start;

          &-avatar {
            margin-right: 16px;

            :deep() {
              .el-image {
                width: 61px;
                height: 61px;
              }
            }
          }

          &-content {
            flex: 1 0;
            width: 0;
            color: rgba(0, 0, 0, 0.85);
          }

          &-title {
            margin-top: 11px;
            margin-bottom: 4px;
            font-size: 14px;
            color: rgba(0, 0, 0, 0.85);
          }

          &-description {
            font-size: 14px;
            color: rgba(0, 0, 0, 0.45);
          }

          &-item {
            display: inline-block;
            height: 61px;
            margin-left: 40px;
            font-size: 14px;
            color: rgba(0, 0, 0, 0.45);
            vertical-align: middle;

            > span {
              line-height: 30px;
            }

            > p {
              margin-top: 4px;
              margin-bottom: 0;
            }
          }

          :deep() {
            .el-progress {
              margin-top: 21px;
            }
          }
        }
      }
    }
  }
</style>
