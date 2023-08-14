<template>
  <el-dialog v-model="dialogFormVisible" :title="title" width="1000px" @close="close">
    <el-form ref="formRef" label-width="150px" label-height="140px" :model="form" :rules="rules" >
      <el-form-item label="台架名称：" prop="roomName" >
        <el-select :data="list" v-model="form.roomName" allow-create13 placeholder="选择台架">
          <el-option v-for="item in list" :key="item" :label="item.roomName" :value="item.roomName" />

        </el-select>
        <el-form-item label="试验编号" prop="testNumber">
        <el-input v-model="form.testNumber" />
      </el-form-item>
      </el-form-item>
      <el-form-item label="任务单：" prop="orderType">
        <el-select  v-model="form.orderType" allow-create13 placeholder="选择类型">
          <el-option label="市场类(A) A1" value="A1" />
          <el-option label="科研类(B) B1" value="B1" />
          <el-option label="保供类 (C) C1" value="C1" />
          <el-option label="故障排查类(D) D1" value="D1" />
        </el-select>
        开始时间：
        <el-date-picker
        v-model="form.orderTypeStartTime"
        type="datetime"
        placeholder="开始时间"  :value-format="startTimePlaceholder(form.orderTypeStartTime)"
      />
      结束时间：
        <el-date-picker
        v-model="form.orderTypeEndTime"
        type="datetime"
        placeholder="结束时间"  :value-format="endTimePlaceholder(form.orderTypeEndTime)"
      />
      </el-form-item>
      <el-form-item label="运行试验：">
        开始时间：
        <el-date-picker
        v-model="form.operationTestStartTime"
        type="datetime"
        placeholder="开始时间"  :value-format="startTimePlaceholder(form.operationTestStartTime)"
      />
      结束时间：
        <el-date-picker
        v-model="form.operationTestEndTime"
        type="datetime"
        placeholder="结束时间"  :value-format="endTimePlaceholder(form.operationTestEndTime)"
      />
      </el-form-item>
      <el-form-item label="异常：" >
        开始时间：
        <el-date-picker
        v-model="form.abnormalStartTime"
        type="datetime"
        placeholder="开始时间" :value-format="startTimePlaceholder(form.abnormalStartTime)"
      />
      结束时间：
        <el-date-picker
        v-model="form.abnormalEndTime"
        type="datetime"
        placeholder="结束时间"  :value-format="endTimePlaceholder(form.abnormalEndTime)"
      />
      </el-form-item>
      <el-form-item label="等待物料：" >
        开始时间：
        <el-date-picker
        v-model="form.waitMaterialStartTime"
        type="datetime"
        placeholder="开始时间"  :value-format="startTimePlaceholder(form.waitMaterialStartTime)"
      />
      结束时间：
        <el-date-picker
        v-model="form.waitMaterialEndTime"
        type="datetime"
        placeholder="结束时间"  :value-format="endTimePlaceholder(form.waitMaterialEndTime)"
      />
      </el-form-item>
      <el-form-item label="等待相关科研人员：" >
        开始时间：
        <el-date-picker
        v-model="form.waitManageStartTime"
        type="datetime"
        placeholder="开始时间"  :value-format="startTimePlaceholder(form.waitManageStartTime)"
      />
      结束时间：
        <el-date-picker
        v-model="form.waitManageEndTime"
        type="datetime"
        placeholder="结束时间"  :value-format="endTimePlaceholder(form.waitManageEndTime)"
      />
      </el-form-item>
      
      <el-form-item label="备注：" prop="remark">
        <el-input v-model="form.remark" rows="3" type="textarea" />
      </el-form-item>
    </el-form>
    <el-form ref="formRef" label-width="150px" label-height="140px" :model="dataform" :rules="datarules" v-if="iscount">
      <el-form-item label="试验台架" prop="testName">
        <el-input v-model="dataform.testName" :style="{ width: '200px' }"/>
      </el-form-item>
      <el-form-item label="试验编号" prop="testNumber" >
        <el-input v-model="dataform.testNumber" :style="{ width: '200px' }"/>
      </el-form-item>
      <el-form-item label="试验类型" prop="testType">
        <el-input v-model="dataform.testType" :style="{ width: '200px' }"/>
      </el-form-item>
      <el-form-item label="任务单总时间：" prop="orderTypeTime">
        <el-input v-model="dataform.orderTypeTime" :style="{ width: '200px' }" step="0.1" />
      </el-form-item>
      <el-form-item label="运行试验总时间：" prop="operationTestTime">
        <el-input v-model="dataform.operationTestTime" :style="{ width: '200px' }"  step="0.1" />
      </el-form-item>
      <el-form-item label="异常总时间：" prop="abnormalTime">
        <el-input v-model="dataform.abnormalTime" :style="{ width: '200px' }"  step="0.1" />
      </el-form-item>
      <el-form-item label="等待物料总时间：" prop="waitMaterialTime">
        <el-input v-model="dataform.waitMaterialTime" :style="{ width: '200px' }"  step="0.1"/>
      </el-form-item>
      <el-form-item label="等待相关科研人员总时间：" prop="waitManageTime">
        <el-input v-model="dataform.waitManageTime" :style="{ width: '200px' }"  step="0.1"  />
      </el-form-item>
    </el-form>
    <template #footer>
      <el-button type="primary" @click="datacount" v-if="!isAdd">数据统计</el-button>
      <el-button @click="close">取 消</el-button>
      <el-button type="primary" @click="save">确 定</el-button>
    </template>
  </el-dialog>
</template>

<script>

import { GetDeviceTestRoomAllList, GetDeviceNameConditionList } from '@/api/DeviceStatus/DeviceStatusTag'
import { getroomList } from '@/api/devicefile/room'
import { doAdd ,doEdit,GetRepeat,Docountdataone} from '@/api/devicefile/testsheet'
import { watch } from 'vue';
import dayjs from 'dayjs';
import duration from 'dayjs/plugin/duration';

dayjs.extend(duration);
export default defineComponent({
  name: 'BasicsProductionEdit',
  emits: ['fetch-data'],
  setup(props, { emit }) {
    const $baseMessage = inject('$baseMessage')

    const state = reactive({
      formRef: null,
      list: [],
      devicelist: [],
      isAdd: false,
      iscount:false,
      form: {
        roles: [],
      },
      dataform:{
        datarules:[],
      },
      value1: '',
      value2: '',
      rules: {
        roomName: [
          { required: true, trigger: 'blur', message: '请选择试验台架' },
          {
            max: 150,
            trigger: 'blur',
            message: '长度不能超过150个字符',
          },
        ],
        testNumber: [
          {
            required: true,
            trigger: 'blur',
            validator: async (rule, value, callback) => {
              if (value) {
                const model = await GetRepeat(state.form)
                if (model.data) {
                  callback(new Error('已存在相同试验编码'))
                } else {
                  callback()
                }
              } else {
                callback(new Error('请输入试验编码'))
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
        testEngineer: [
          { required: true, trigger: 'blur', message: '请实验工程师' },
        ],
        startTime: [
          { required: true, trigger: 'blur', message: '输入开始时间' },
        ],

        orderType: [
          { required: true, trigger: 'blur', message: '请选择试验类型' },
        ],
        deviceRoom: [
          { required: true, trigger: 'blur', message: '请选择试验间' },
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
    const formatEndTime = (row,column) => {
      if (row[column.property] === '1901-01-01 00:00:00') {
        return '';
      } else {
        return row[column.property];
      }
    }
    
    const showEdit = (row) => {
      if (!row) {
        state.title = '试验单添加'
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
      state.dataform={
        datarules:[],
      }
      state.dialogFormVisible = false
      state.iscount=false
    }
    const handleChange = async (val) => {

     
      const data = await GetDeviceNameConditionList({ roomName: val })

      console.log(data.data)
      state.devicelist = data.data
      state.listLoading = false
    }
   const datacount=()=>{
   state.iscount=true
   state.dataform.testName=state.form.roomName
   state.dataform.testNumber=state.form.testNumber
   state.dataform.testType=state.form.orderType
state.dataform.orderTypeTime = dataTimexianjianzhognwen(state.form.orderTypeStartTime,state.form.orderTypeEndTime);
state.dataform.operationTestTime = dataTimexianjianzhognwen(state.form.operationTestStartTime,state.form.operationTestEndTime);
state.dataform.abnormalTime = dataTimexianjianzhognwen(state.form.abnormalStartTime,state.form.abnormalEndTime);
state.dataform.waitMaterialTime = dataTimexianjianzhognwen(state.form.waitMaterialStartTime,state.form.waitMaterialEndTime);
state.dataform.waitManageTime = dataTimexianjianzhognwen(state.form.waitManageStartTime,state.form.waitManageEndTime);
   }
    const save = () => {
      state['formRef'].validate(async (valid) => {
        if (valid) {
         
          if(state.iscount){
          state.dataform.orderTypeTime = dataTimexianjianmiao(state.form.orderTypeStartTime,state.form.orderTypeEndTime);
          state.dataform.operationTestTime = dataTimexianjianmiao(state.form.operationTestStartTime,state.form.operationTestEndTime);
        state.dataform.abnormalTime = dataTimexianjianmiao(state.form.abnormalStartTime,state.form.abnormalEndTime);
        state.dataform.waitMaterialTime = dataTimexianjianmiao(state.form.waitMaterialStartTime,state.form.waitMaterialEndTime);
          state.dataform.waitManageTime = dataTimexianjianmiao(state.form.waitManageStartTime,state.form.waitManageEndTime);
     await Docountdataone(state.dataform)
        }
          if (state.form.orderTypeStartTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
             state.form.orderTypeStartTime ='0001-01-01 00:00:00';
          }
          if (state.form.orderTypeEndTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.form.orderTypeEndTime = '0001-01-01 00:00:00';
          }
          if (state.form.operationTestStartTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.form.operationTestStartTime ='0001-01-01 00:00:00';
          }
          if (state.form.operationTestEndTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.form.operationTestEndTime = '0001-01-01 00:00:00';
          }
          if (state.form.abnormalStartTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state. form.abnormalStartTime ='0001-01-01 00:00:00';
          }
          if (state.form.abnormalEndTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.  form.abnormalEndTime = '0001-01-01 00:00:00';
          }
          if (state.form.waitMaterialStartTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.  form.waitMaterialStartTime ='0001-01-01 00:00:00';
          }
          if (state.form.waitMaterialEndTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state. form.waitMaterialEndTime ='0001-01-01 00:00:00';
          }
          if (state.form.waitManageEndTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.form.waitManageEndTime ='0001-01-01 00:00:00';
          }
          if (state.form.waitManageStartTime === null) {
          // 将 form.waitManageEndTime 的值设置为默认时间（0001-01-01 00:00:00）
          state.form.waitManageStartTime = '0001-01-01 00:00:00';
          }
          console.log(11111)
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
    const dataTimexianjianzhognwen=(start,end)=>{
      const startTime = dayjs(start);
const endTime = dayjs(end);
const cellValue= endTime.diff(startTime, 'second');
const hours = parseInt(cellValue / 3600) < 10 ? 0 + parseInt(cellValue / 3600) : parseInt(cellValue / 3600);
  const min = parseInt(cellValue % 3600 / 60) < 10 ? 0 + parseInt(cellValue % 3600 / 60) : parseInt(cellValue % 3600 / 60);
  const sec = parseInt(cellValue % 3600 % 60) < 10 ? 0 + parseInt(cellValue % 3600 % 60) : parseInt(cellValue % 3600 % 60);
      return `${hours}小时 ${min}分钟 ${sec}秒`;
 //return endTime.diff(startTime, 'second');
//return (end - start) / (1000 * 60 * 60)
    }
    const dataTimexianjianmiao=(start,end)=>{
      const startTime = dayjs(start);
      const endTime = dayjs(end);
 return endTime.diff(startTime, 'second');
//return (end - start) / (1000 * 60 * 60)
    }
    const fetchData = async () => {
      state.listLoading = true
    
      const {
       data: { list, total },
} = await getroomList()
     state.list = list
     state.total = total
      state.listLoading = false
    }
    const startTimePlaceholder = (time) => {
      if (time  === '0001-01-01 00:00:00') {
        
        return '开始时间';
      } else {
        console.log(time)
        return new Date(time);
      }
    };
    const endTimePlaceholder = (time) => {
      if (time  === '0001-01-01 00:00:00') {
       
        return '结束时间';
      } else {
        console.log(time)
        return new Date(time);
      }
    };
    onMounted(() => {
      fetchData()
    })

    return {
      ...toRefs(state),
      showEdit,
      close,
      save,
      fetchData,
      handleChange,formatEndTime,startTimePlaceholder,endTimePlaceholder,datacount,dataTimexianjianzhognwen,dataTimexianjianmiao
    }
  },
})
</script>
<style>

.el-form-item{
  line-height: 150px;
}
</style>
