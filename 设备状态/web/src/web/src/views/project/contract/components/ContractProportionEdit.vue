<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    :close-on-press-escape="false"
    :title="title"
    width="500px"
    @close="close"
  >
    <el-table :data="list" style="width: 100%" @cell-click="cellClick">
      <el-table-column align="center" label="回款类型" prop="paymentCTypeID">
        <template #default="{ row }">
          {{ row.paymentCTypeID }}
        </template>
      </el-table-column>
      <el-table-column align="center" label="比例(%)" prop="paymentCProportion">
        <template #default="{ row }">
          <el-input-number
            v-model="row.paymentCProportion"
            :max="9999999"
            :min="0"
            :step="1"
            style="width: 90%"
            type="number"
          />
        </template>
      </el-table-column>
      <el-table-column align="center" label="占比金额" prop="conAmountCPlan">
        <template #default="{ row }">
          {{ row.conAmountCPlan }}
        </template>
      </el-table-column>
    </el-table>
    <template #footer>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">保 存</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import { defineComponent, reactive, toRefs } from 'vue'
  import {
    //DoEditOrAdd,
    // ContractDetailAdd,
    // ContractDetailEdit,
    GetPaymentCListByConId,
    BiLiEditOrAdd,
  } from '@/api/contract'

  export default defineComponent({
    name: 'ContractProportionEdit',
    emits: ['fetch-data'],
    setup() {
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        formRef: null,
        id: 0,
        totalPrice: 0,
        form: {},
        title: '',
        initialData: [
          {
            paymentCTypeID: '预付款',
            paymentCProportion: 0,
            edit: false,
            conAmountCPlan: 0,
          },
          {
            paymentCTypeID: '进度款',
            edit: false,
            paymentCProportion: 0,
            conAmountCPlan: 0,
          },
          {
            paymentCTypeID: '验收款',
            edit: false,
            paymentCProportion: 0,
            conAmountCPlan: 0,
          },
          {
            paymentCTypeID: '质保金',
            edit: false,
            paymentCProportion: 0,
            conAmountCPlan: 0,
          },
          {
            paymentCTypeID: '结算款',
            edit: false,
            paymentCProportion: 0,
            conAmountCPlan: 0,
          },
          {
            paymentCTypeID: '尾款',
            edit: false,
            paymentCProportion: 0,
            conAmountCPlan: 0,
          },
        ],
        list: [],
        dialogFormVisible: false,
      })

      const showEdit = async (Id, TotalPrice) => {
        state.title = '编辑回款比例信息'
        state.id = Id
        state.totalPrice = TotalPrice
        const list = await GetPaymentCListByConId({ contrctid: Id })
        if (list.data.length > 0) {
          list.data.forEach((item) => {
            item.conAmountCPlan = parseFloat(
              TotalPrice * (item.paymentCProportion / 100)
            ).toFixed(2)
          })
          state.list = list.data
        } else {
          state.list = state.initialData
          state.list.forEach((item) => {
            item.ct_ID = state.id
            item.conAmountCPlan = parseFloat(
              TotalPrice * (item.paymentCProportion / 100)
            ).toFixed(2)
          })
        }
        //console.log(state.list)
        state.dialogFormVisible = true
      }
      const close = () => {
        state.list = state.initialData
        state.dialogFormVisible = false
      }
      const save = async () => {
        let bili = 0
        state.list.forEach((item) => {
          bili += item.paymentCProportion
        })
        if (bili != 100) {
          $baseMessage(
            '请注意比例(%)数据的准确性!',
            'warning',
            'vab-hey-message-warning'
          )
        } else {
          //BiLiEditOrAdd
          //console.log(state.list)
          const { msg } = await BiLiEditOrAdd(state.list)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          close()
        }
        // state['formRef'].validate(async (valid) => {
        //   if (valid) {
        //     const submit =
        //       state.form.id > 0 ? ContractDetailEdit : ContractDetailAdd
        //     const { msg } = await submit(state.form)
        //     $baseMessage(msg, 'success', 'vab-hey-message-success')
        //     emit('fetch-data')
        //     close()
        //   }
        // })
      }
      const cellClick = (row) => {
        state.list.forEach((item) => {
          if (item.paymentCTypeID == row.paymentCTypeID) {
            item.conAmountCPlan = parseFloat(
              state.totalPrice * (row.paymentCProportion / 100)
            ).toFixed(2)
          }
        })
        //row.conAmountCPlan = totalPrice / (row.paymentCProportion / 100)
        // console.log(row)
        // console.log(column)
        // console.log(cell)
        // console.log(event)
      }
      return {
        ...toRefs(state),
        showEdit,
        close,
        save,
        cellClick,
      }
    },
  })
</script>
