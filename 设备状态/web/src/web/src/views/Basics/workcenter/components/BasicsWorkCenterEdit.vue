<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="700px"
    @close="close"
  >
    <el-form ref="formRef" label-width="150px" :model="form" :rules="rules">
      <el-form-item label="工作中心编码" prop="workCode">
        <el-input v-model="form.workCode" />
      </el-form-item>
      <el-form-item label="工作中心名称" prop="workName">
        <el-input v-model="form.workName" />
      </el-form-item>
      <el-form-item label="工作中心类型" prop="workType">
        <el-select
          v-model="form.workType"
          clearable
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in WorkCenterTypeRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="终端IP" prop="gwIP">
        <el-input v-model="form.gwIP" />
      </el-form-item>
      <el-form-item label="MAC地址" prop="gwMac">
        <el-input v-model="form.gwMac" />
      </el-form-item>
      <el-form-item label="父级工作中心" prop="atAreaID">
        <el-select
          v-model="form.atAreaID"
          clearable
          filterable
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in GongWeiAreaRadio"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="是否有追溯系统" prop="isHasFollow">
        <el-radio-group v-model="form.isHasFollow">
          <el-radio border :label="0" size="large">是</el-radio>
          <el-radio border :label="1" size="large">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="是否有装配管理系统" prop="isHasAms">
        <el-radio-group v-model="form.isHasAms">
          <el-radio border :label="0" size="large">是</el-radio>
          <el-radio border :label="1" size="large">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="是否有光影引导系统" prop="isHasGuangying">
        <el-radio-group v-model="form.isHasGuangying">
          <el-radio border :label="0" size="large">是</el-radio>
          <el-radio border :label="1" size="large">否</el-radio>
        </el-radio-group>
      </el-form-item>
      <el-form-item label="状态" prop="gwStatus">
        <el-select
          v-model="form.gwStatus"
          allow-create13
          placeholder="选择状态"
        >
          <el-option key="0" label="启用" :value="0" />
          <el-option key="1" label="禁用" :value="1" />
        </el-select>
      </el-form-item>
      <el-form-item label="备注" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import {
    doAdd,
    doEdit,
    GetWorkCenterTypeList,
    GetGongWeiAreaList,
    GetRepeat,
  } from '@/api/basics/workcenter'

  export default defineComponent({
    name: 'BasicsWorkCenterEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        form: {
          roles: [],
        },
        WorkCenterTypeRadio: [],
        GongWeiAreaRadio: [],
        rules: {
          workCode: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同工作中心编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入工作中心编码'))
                }
              },
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
          ],
          workName: [
            { required: true, trigger: 'blur', message: '请输入工作中心名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          workType: [
            { required: true, trigger: 'blur', message: '请选择工作中心类型' },
          ],
          gwIP: [
            {
              required: true,
              message: '请输入正确的IP地址',
              validator: (rule, value, callback) => {
                const regexp =
                  /^((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}$/
                if (value === '') {
                  callback(new Error('请输入正确的IP地址'))
                } else if (value !== '' && !regexp.test(value)) {
                  callback(new Error('请输入正确的IP地址'))
                } else {
                  callback()
                }
              },
              trigger: 'blur',
            },
          ],
          gwMac: [
            {
              trigger: 'blur',
              message: '请输入正确的MAC地址',
              validator: (rule, value, callback) => {
                const regexp =
                  /[A-F\d]{2}:[A-F\d]{2}:[A-F\d]{2}:[A-F\d]{2}:[A-F\d]{2}:[A-F\d]{2}/
                if (value && !regexp.test(value)) {
                  callback(new Error('请输入正确的MAC地址'))
                } else {
                  callback()
                }
              },
            },
          ],
          isHasFollow: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否有追溯系统',
            },
          ],
          isHasAms: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否有装配管理系统',
            },
          ],
          isHasGuangying: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择是否有光影引导系统',
            },
          ],
          gwStatus: [
            { required: true, trigger: 'blur', message: '请选择状态' },
          ],
          remark: [
            { trigger: 'blur', message: '长度不能超过255个字符' },
            {
              max: 255,
              trigger: 'blur',
              message: '长度不能超过255个字符',
            },
          ],
        },
        title: '',
        dialogFormVisible: false,
      })

      //工作中心类型
      const GetWorkCenterTypeData = async () => {
        const WorkCenterType = await GetWorkCenterTypeList()
        state.WorkCenterTypeRadio = WorkCenterType.data
      }

      //父级工作中心
      const GetGongWeiAreaData = async () => {
        const GongWeiArea = await GetGongWeiAreaList()
        state.GongWeiAreaRadio = GongWeiArea.data
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        state['formRef'].validate(async (valid) => {
          if (valid) {
            console.log(state.form)
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      onMounted(() => {
        GetWorkCenterTypeData()
        GetGongWeiAreaData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetWorkCenterTypeData,
        GetGongWeiAreaData,
      }
    },
  })
</script>
