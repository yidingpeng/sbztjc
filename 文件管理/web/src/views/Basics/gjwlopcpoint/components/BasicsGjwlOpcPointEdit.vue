<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="550px"
    @close="close"
  >
    <el-form ref="formRef" label-width="110px" :model="form" :rules="rules">
      <el-form-item label="工作中心" prop="gwID">
        <el-select
          v-model="form.gwID"
          clearable
          placeholder="请选择工作中心"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in WorkCenterList"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="工具名称" prop="gjID">
        <el-select
          v-model="form.gjID"
          clearable
          placeholder="请选择工具名称"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in ToolList"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="物料名称" prop="wlID">
        <el-select
          v-model="form.wlID"
          clearable
          placeholder="请选择物料名称"
          :style="{ width: '100%' }"
        >
          <el-option
            v-for="item in MaterialList"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="OPC设备名称" prop="opcDeviceName">
        <el-input v-model="form.opcDeviceName" />
      </el-form-item>
      <el-form-item
        v-for="(item, index) in GwOPCTypeList"
        :key="index"
        :label="item.label"
        prop="GwOPC"
      >
        <el-input
          v-model="form.opclist[index].value"
          @blur="form.opclist[index].Id = item.value"
        />
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
    GetGwOPCTypeList,
    GetWorkCenterList,
    GetToolList,
    GetMaterialList,
    GetRepeat,
  } from '@/api/basics/gjwlopcpoint'

  export default defineComponent({
    name: 'BasicsGjwlOpcPointEdit',
    inject: ['reload'],
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: false,
        GwOPCTypeList: [],
        WorkCenterList: [],
        ToolList: [],
        MaterialList: [],
        form: {
          roles: [],
          opclist: [],
        },
        rules: {
          gwID: [
            { required: true, trigger: 'blur', message: '请选择工作中心' },
          ],
          gjID: [
            {
              trigger: 'change',
              validator: (rule, value, callback) => {
                if (state.form.wlID && value) {
                  state.form.wlID = null
                } else {
                  callback()
                }
              },
            },
            {
              trigger: 'blur',
              validator: (rule, value, callback) => {
                if (!state.form.wlID && !value) {
                  callback(new Error('请选择工具或物料'))
                } else {
                  callback()
                }
              },
            },
          ],
          wlID: [
            {
              trigger: 'change',
              validator: (rule, value, callback) => {
                if (state.form.gjID && value) {
                  state.form.gjID = null
                } else {
                  callback()
                }
              },
            },
            {
              trigger: 'blur',
              validator: (rule, value, callback) => {
                if (!state.form.gjID && !value) {
                  callback(new Error('请选择工具或物料'))
                } else {
                  callback()
                }
              },
            },
          ],
          opcDeviceName: [
            { required: true, trigger: 'blur', message: '请输入OPC设备名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
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

      //Opc点位类型
      const GetGwOPCTypeData = async () => {
        const GwOPCType = await GetGwOPCTypeList()
        state.GwOPCTypeList = GwOPCType.data
        state.form = {
          roles: [],
          opclist: [],
        }
        state.GwOPCTypeList.forEach((item) => {
          state.form.opclist.push({ Id: 0, code: item.name, value: '' })
        })
      }
      //工作中心信息
      const GetWorkCenterData = async () => {
        const WorkCenter = await GetWorkCenterList()
        state.WorkCenterList = WorkCenter.data
      }
      //工具信息
      const GetToolData = async () => {
        const ToolList = await GetToolList()
        state.ToolList = ToolList.data
      }
      //物料信息
      const GetMaterialData = async () => {
        const MaterialList = await GetMaterialList()
        state.MaterialList = MaterialList.data
      }
      const showEdit = (row) => {
        if (!row) {
          GetGwOPCTypeData()
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
          row.gjID == 0 ? (row.gjID = null) : ''
          row.wlID == 0 ? (row.wlID = null) : ''
          state.form = Object.assign({}, row)
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        /*   state.form = {
          roles: [],
          opclist: [],
        } */
        state['formRef'].resetFields()

        state.dialogFormVisible = false
      }
      const judgmentepeat = async () => {
        const model = await GetRepeat(state.form)

        return model.data
      }
      const save = async () => {
        if (state.isAdd) {
          if (state.form.gjID || state.form.wlID) {
            const model = await GetRepeat(state.form)
            if (model.data) {
              if (state.form.gjID)
                $baseMessage(
                  '该工作中心已经存在此工具信息',
                  'error',
                  'vab-hey-message-error'
                )
              else
                $baseMessage(
                  '该工作中心已经存在此物料信息',
                  'error',
                  'vab-hey-message-error'
                )
              return
            }
          }
        }

        state['formRef'].validate(async (valid) => {
          if (valid) {
            const { msg } = state.isAdd
              ? await doAdd(state.form)
              : await doEdit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })

        /* judgmentepeat().then((res) => {}) */
      }

      onMounted(() => {
        GetGwOPCTypeData()
        GetWorkCenterData()
        GetToolData()
        GetMaterialData()
      })

      return {
        ...toRefs(state),
        GetGwOPCTypeData,
        GetWorkCenterData,
        GetToolData,
        GetMaterialData,
        showEdit,
        close,
        save,
        judgmentepeat,
      }
    },
  })
</script>
