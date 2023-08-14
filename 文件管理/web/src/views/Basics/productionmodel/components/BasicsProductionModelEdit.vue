<template>
  <el-dialog
    v-model="dialogFormVisible"
    :title="title"
    width="700px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="12">
          <el-form-item label="产品名称" prop="pid">
            <el-select
              v-model="form.pid"
              clearable
              placeholder="请选择产品名称"
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ProductionList"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="产品型号编码" prop="pmodelCode">
            <el-input v-model="form.pmodelCode" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="产品型号名称" prop="pname">
            <el-input v-model="form.pname" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="产品型号简称" prop="pnameShort">
            <el-input v-model="form.pnameShort" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="图号" prop="drawNo">
            <el-input v-model="form.drawNo" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="物料号" prop="materialNo">
            <el-input v-model="form.materialNo" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="产品型号类型" prop="productionModelType">
            <el-select
              v-model="form.productionModelType"
              clearable
              filterable
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ProductionModelRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="产品类型" prop="productionType">
            <el-select
              v-model="form.productionType"
              clearable
              filterable
              :style="{ width: '100%' }"
            >
              <el-option
                v-for="item in ProductionTypeRadio"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="线路号" prop="trainLine">
            <el-input v-model="form.trainLine" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="车型号" prop="traiNModel">
            <el-input v-model="form.traiNModel" />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row v-if="isAdd == false">
        <el-col
          v-for="(item, index) in ProductExtendList"
          :key="index"
          :span="12"
        >
          <el-form-item
            :label="item.textName"
            :prop="item.colName"
            :rules="[
              { trigger: 'blur', message: '长度不能超过255个字符' },
              {
                max: 150,
                trigger: 'blur',
                message: '长度不能超过150个字符',
              },
            ]"
          >
            <el-input v-model="item.extendValue" />
          </el-form-item>
        </el-col>
      </el-row>

      <el-row>
        <el-col :span="12">
          <el-form-item label="排序号" prop="orderIndex">
            <el-input v-model="form.orderIndex" />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="状态" prop="pstatus">
            <el-select
              v-model="form.pstatus"
              allow-create13
              placeholder="选择状态"
            >
              <el-option key="0" label="启用" :value="0" />
              <el-option key="1" label="禁用" :value="1" />
            </el-select>
          </el-form-item>
        </el-col>
      </el-row>

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
    GetProductionModelTypeList,
    GetProductionTypeList,
    GetProductionList,
    GetProductExtendList,
    GetRepeat,
  } from '@/api/basics/productionmodel'

  export default defineComponent({
    name: 'BasicsProductionModelEdit',
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')

      const state = reactive({
        formRef: null,
        isAdd: true,
        form: {
          roles: [],
        },
        ProductionModelRadio: [],
        ProductionList: [],
        ProductionTypeRadio: [],
        ProductExtendList: [],
        rules: {
          pmodelCode: [
            {
              required: true,
              trigger: 'blur',
              validator: async (rule, value, callback) => {
                if (value) {
                  const model = await GetRepeat(state.form)
                  if (model.data) {
                    callback(new Error('已存在相同产品型号编码'))
                  } else {
                    callback()
                  }
                } else {
                  callback(new Error('请输入产品型号编码'))
                }
              },
            },
            {
              min: 3,
              trigger: 'blur',
              message: '编码长度不能小于3个字符',
            },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          pname: [
            { required: true, trigger: 'blur', message: '请输入产品型号名称' },
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          pnameShort: [
            {
              max: 150,
              trigger: 'blur',
              message: '长度不能超过150个字符',
            },
          ],
          productionModelType: [
            { required: true, trigger: 'blur', message: '请选择产品型号类型' },
          ],
          pid: [{ required: true, trigger: 'blur', message: '请选择产品名称' }],
          productionType: [
            { required: true, trigger: 'blur', message: '请选择产品类型' },
          ],
          drawNo: [
            { required: true, trigger: 'blur', message: '请输入图号' },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          materialNo: [
            {
              required: true,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          trainLine: [
            { trigger: 'blur', message: '长度不能超过50个字符' },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          traiNModel: [
            { trigger: 'blur', message: '长度不能超过50个字符' },
            {
              max: 50,
              trigger: 'blur',
              message: '长度不能超过50个字符',
            },
          ],
          pstatus: [{ required: true, trigger: 'blur', message: '请选择状态' }],
          orderIndex: [
            {
              validator: (rule, value, callback) => {
                if (value) {
                  if (!Number.isInteger(parseFloat(value))) {
                    callback(new Error('请输入数字'))
                  }
                  callback()
                } else {
                  state.form.toolModel = null
                  callback()
                }
              },
              trigger: 'blur',
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

      //额外字段信息
      const GetProductExtendData = async () => {
        const ProductExtend = await GetProductExtendList(state.form)
        state.ProductExtendList = ProductExtend.data
      }

      //产品信息
      const GetProductionData = async () => {
        const Production = await GetProductionList()
        state.ProductionList = Production.data
      }

      //产品型号类型
      const GetProductionModelTypeData = async () => {
        const ProductionModelType = await GetProductionModelTypeList()
        state.ProductionModelRadio = ProductionModelType.data
      }

      //产品类型
      const GetProductionTypeData = async () => {
        const ProductionType = await GetProductionTypeList()
        state.ProductionTypeRadio = ProductionType.data
      }

      const showEdit = (row) => {
        if (!row) {
          state.title = '添加'
          state.isAdd = true
        } else {
          state.title = '编辑'
          state.isAdd = false
          state.form = Object.assign({}, row)
          GetProductExtendData()
        }
        state.dialogFormVisible = true
      }

      const inquireData = (id) => {
        state.form.pid = id
      }

      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        if (!state.isAdd) {
          state.form.extendList = state.ProductExtendList
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
      }

      onMounted(() => {
        GetProductionModelTypeData()
        GetProductionTypeData()
        GetProductionData()
      })

      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        GetProductionModelTypeData,
        GetProductionTypeData,
        GetProductionData,
        inquireData,
        GetProductExtendData,
      }
    },
  })
</script>
