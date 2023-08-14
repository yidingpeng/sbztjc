<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :title="title"
    width="800px"
    @close="close"
  >
    <el-form ref="formRef" label-width="120px" :model="form" :rules="rules">
      <el-row>
        <el-col :span="24"><el-divider>项目信息</el-divider></el-col>
      </el-row>
      <el-row>
        <el-col :span="24">
          <el-form-item label="项目名称" prop="projectId">
            <rw-project
              v-model="form.projectId"
              disabled
              placeholder="输入编号或名称模糊搜索"
              style="width: 100%"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="设备数量" prop="equipmentCount">
            <el-input-number
              v-model="form.equipmentCount"
              disabled
              :max="99"
              :min="1"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="设备合同单价" prop="equipmentUnitPrice">
            <el-input-number
              v-model="form.equipmentUnitPrice"
              disabled
              :max="9999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="要求交付日期" prop="deliverDate">
            <el-date-picker
              v-model="form.deliverDate"
              :style="{ width: '100%' }"
              type="date"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="设备合同总价" prop="equipmentTotalPrice">
            <el-input-number
              v-model="form.equipmentTotalPrice"
              disabled
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <!-- <el-row>
        <el-col :span="24"><el-divider>回款信息</el-divider></el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="预付款" prop="advanceCharge">
            <el-input-number
              v-model="form.advanceCharge"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="进度款" prop="progressPayment">
            <el-input-number
              v-model="form.progressPayment"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="验收款" prop="acceptancePayment">
            <el-input-number
              v-model="form.acceptancePayment"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="质保金" prop="retentionMoney">
            <el-input-number
              v-model="form.retentionMoney"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="12">
          <el-form-item label="结算款" prop="settlementFunds">
            <el-input-number
              v-model="form.settlementFunds"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="尾款" prop="balancePayment">
            <el-input-number
              v-model="form.balancePayment"
              :max="99999999"
              :min="0"
              :precision="2"
              :step="1"
              :style="{ width: '100%' }"
            />
          </el-form-item>
        </el-col>
      </el-row> -->
    </el-form>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import {
    ContractDetailAdd,
    ContractDetailEdit,
    GetPaymentCListByConId,
  } from '@/api/contract'
  import RwProject from '@/plugins/RwProjectCode'
  import moment from 'moment'

  export default defineComponent({
    name: 'ContractDetailEdit',
    components: { RwProject },
    emits: ['fetch-data'],
    setup(props, { emit }) {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        formRef: null,
        form: {
          id: 0,
          pid: '',
          projectId: '',
          ProjectName: '',
          equipmentCount: 1,
        },
        title: '',
        rules: {
          projectId: [
            { required: true, trigger: 'blur', message: '请选择项目名称' },
          ],
          equipmentCount: [
            { required: true, trigger: 'blur', message: '请输入设备数量' },
          ],
          equipmentUnitPrice: [
            { required: true, trigger: 'blur', message: '请输入设备合同单价' },
          ],
          // advanceCharge: [
          //   { required: true, trigger: 'blur', message: '请输入预付款' },
          // ],
          // progressPayment: [
          //   { required: true, trigger: 'blur', message: '请输入进度款' },
          // ],
          // acceptancePayment: [
          //   { required: true, trigger: 'blur', message: '请输入验收款' },
          // ],
          // retentionMoney: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '请输入质保金',
          //   },
          // ],
          // settlementFunds: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '请输入结算款',
          //   },
          // ],
          // balancePayment: [
          //   {
          //     required: true,
          //     trigger: 'blur',
          //     message: '请输入尾款',
          //   },
          // ],
          deliverDate: [
            {
              required: true,
              trigger: 'blur',
              message: '请选择要求交付日期',
            },
          ],
        },
        dialogFormVisible: false,
      })

      const showEdit = async (row, type) => {
        if (type == 'add') {
          state.title = '新增合同项目信息'
          state.form.pid = row
        } else {
          state.title = '编辑合同项目信息'
          state.form = JSON.parse(JSON.stringify(row))
        }
        state.dialogFormVisible = true
      }
      const close = () => {
        state['formRef'].resetFields()
        state.form = {
          roles: [],
          projectId: [],
        }
        state.dialogFormVisible = false
      }
      const save = () => {
        // console.log(state.form.projectId)
        // console.log(state.form.projectId.length)
        state['formRef'].validate(async (valid) => {
          if (valid) {
            state.form.deliverDate = moment(state.form.deliverDate).format(
              'YYYY-MM-DD'
            )
            //判断数据类型
            const type = Object.prototype.toString.call(state.form.projectId)
            if (type === '[object Array]') {
              state.form.projectId =
                state.form.projectId[state.form.projectId.length - 1]
            }
            const submit =
              state.form.id > 0 ? ContractDetailEdit : ContractDetailAdd
            const { msg } = await submit(state.form)
            $baseMessage(msg, 'success', 'vab-hey-message-success')
            emit('fetch-data')
            close()
          }
        })
      }

      //数量或价格值改变事件
      const handleChange = async () => {
        let Num = 0
        if (
          state.form.equipmentCount > 0 &&
          state.form.equipmentUnitPrice > 0
        ) {
          Num = state.form.equipmentCount * state.form.equipmentUnitPrice
        }
        state.form.equipmentTotalPrice = Num

        const result = await GetPaymentCListByConId({
          contrctid: state.form.pid,
        })
        console.log(result.data)
        if (result.data.length > 0 && Num > 0) {
          result.data.forEach((item) => {
            if (item.paymentCTypeID == '预付款') {
              state.form.advanceCharge = Num * (item.paymentCProportion / 100)
            } else if (item.paymentCTypeID == '进度款') {
              state.form.progressPayment = Num * (item.paymentCProportion / 100)
            } else if (item.paymentCTypeID == '验收款') {
              state.form.acceptancePayment =
                Num * (item.paymentCProportion / 100)
            } else if (item.paymentCTypeID == '质保金') {
              state.form.retentionMoney = Num * (item.paymentCProportion / 100)
            } else if (item.paymentCTypeID == '结算款') {
              state.form.settlementFunds = Num * (item.paymentCProportion / 100)
            } else if (item.paymentCTypeID == '尾款') {
              state.form.balancePayment = Num * (item.paymentCProportion / 100)
            }
          })
        }
      }
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        handleChange,
      }
    },
  })
</script>
