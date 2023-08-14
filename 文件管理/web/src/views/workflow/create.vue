<template>
  <div class="comprehensive-form-container">
    <el-form
      ref="formRef"
      class="demo-form"
      label-width="100px"
      :model="form"
      :rules="rules"
    >
      <el-row :gutter="24">
        <el-col
          :lg="{ span: 12, offset: 6 }"
          :md="{ span: 20, offset: 2 }"
          :sm="{ span: 20, offset: 2 }"
          :xl="{ span: 12, offset: 6 }"
          :xs="24"
        >
          <el-form-item label="流程名称" prop="title">
            <el-input v-model="form.title" />
          </el-form-item>
          <el-form-item label="流程类型" prop="type">
            <!-- <rw-dictionary
              v-model="form.type"
              placeholder="请选择流程类型"
              type="WorkflowType"
            /> -->
            <el-select v-model="form.type" placeholder="请选择流程类型">
              <el-option
                v-for="type in types"
                :key="type.key"
                :label="type.value"
                :value="type.key"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="立即启用" prop="enabled">
            <el-switch v-model="form.enabled" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <sc-workflow v-model="charts" :edit="true" />
        </el-col>
      </el-row>
      <el-row style="margin-top: 10px">
        <el-col
          :lg="{ span: 12, offset: 6 }"
          :md="{ span: 16, offset: 4 }"
          :sm="{ span: 20, offset: 2 }"
          :xl="{ span: 12, offset: 6 }"
          :xs="24"
        >
          <el-form-item>
            <el-button type="primary" @click="submitForm('formRef')">
              {{ form.id ? '保存' : '创建' }}
            </el-button>
            <el-button @click="resetForm('formRef')">重置</el-button>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>

<script>
  import { doGetOne, doAdd, doEdit } from '@/api/workflow/index'
  import scWorkflow from '@/plugins/scWorkflow'
  //import RwDictionary from '@/plugins/RwDictionary'

  export default defineComponent({
    name: 'CreateWorkflow',
    components: { scWorkflow },
    setup() {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        types: [{ key: 'issue', value: '问题反馈' }],
        form: {
          name: '',
          region: '',
          useful: false,
          type: 'issue',
          description: '',
          rate: 0,
          transfer: [],
        },
        rules: {
          title: [
            { required: true, message: '请输入流程名称', trigger: 'blur' },
            {
              min: 2,
              message: '至少 2 个字符',
              trigger: 'blur',
            },
          ],
          type: [
            { required: true, message: '请选择流程类型', trigger: 'change' },
          ],
          date: [
            {
              type: 'date',
              required: true,
              message: '请选择日期',
              trigger: 'change',
            },
          ],
          description: [
            { required: true, message: '请填写活动形式', trigger: 'blur' },
          ],
        },
        charts: {
          nodeName: '发起人',
          type: 0,
          nodeRoleList: [],
          childNode: {},
        },
      })

      const fetchData = async () => {
        console.log(useRouter().currentRoute.value.params)
        const { id } = useRouter().currentRoute.value.params
        if (id) {
          const { data } = await doGetOne(id)
          state.form = data
          if (data.workFlowData) {
            state.charts = JSON.parse(data.workFlowData)
            //this.$set('charts', JSON.parse(data.workFlowData))
          }
        }
      }

      const submitForm = (formName) => {
        state[formName].validate(async (valid) => {
          if (valid) {
            state.form.workFlowData = JSON.stringify(state.charts)
            //console.log('result.form:', state.form)
            //let result = true
            //if (result) return
            if (state.form.id > 0) {
              const { msg } = await doEdit(state.form.id, state.form)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
            } else {
              const { msg } = await doAdd(state.form)
              $baseMessage(msg, 'success', 'vab-hey-message-success')
            }
          } else {
            $baseMessage('提交失败', 'error', 'vab-hey-message-error')
          }
        })
      }
      const resetForm = (formName) => {
        state[formName].resetFields()
      }

      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        submitForm,
        resetForm,
        fetchData,
      }
    },
  })
</script>

<style lang="scss" scoped>
  .comprehensive-form-container {
    .demo-form {
      margin-top: 10px;
    }

    :deep() {
      .el-form-item__content {
        .el-rate {
          display: inline-block;
          font-size: 0;
          line-height: 1;
          vertical-align: middle;
        }

        .el-transfer__buttons {
          padding: 10px 10px 0 10px;
        }
      }
    }
  }
</style>
