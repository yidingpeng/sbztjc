<template>
  <div class="role-management-container">
    <el-row><span class="item" style="display: block;width: 200px; text-align:right;margin-left: -40px; float: left;">
        <el-button type="primary" @click="ReFresh()"> 时间刷新</el-button>
      </span></el-row>
    <vab-card class="order-card1" shadow="hover">
      <template #header>
        <vab-icon icon="shopping-bag-2-line" />
        台架状态
      </template>
      <el-row class="order-card1-content">
        <el-col :span="5">
          <p>试验中数量</p>
          <h1>
            {{ statusnuber.testrunNumber }}
          </h1>
        </el-col>
        <el-col :span="5">
          <p>试验暂停数量</p>
          <h1>
            {{ statusnuber.teststopNumber }}
          </h1>
        </el-col>

        <el-col :span="5">
          <p>故障数量</p>
          <h1>{{ statusnuber.faultNumber }}</h1>
        </el-col>
        <el-col :span="5">
          <p>待机数量</p>
          <h1>
            {{ statusnuber.standbyNumber }}
          </h1>
        </el-col>
        <el-col :span="4">
          <p>调试运行数量</p>
          <h1>
            {{ statusnuber.debugrunNumber }}
          </h1>
        </el-col>
      </el-row>
    </vab-card>
    <el-row :gutter="40">

      <el-card class="box-card" v-for="w in   list  " :key="w.id">

        <template #header>


          <span v-if="w.operationStatus.includes('故障') ? isActive = 1 : isActive = 0">
            <div class="ss2">
              <span style="display:block;width: 100%; text-align:center;" v-if="w.roomName.includes('555')">试验台架117-1
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('666')">试验台架117-2
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('777')">试验台架117-3
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('888')">试验台架117-4
                MES状态显示</span>
              <span style="display:block;width: 100%; text-align:center;" v-else>
                {{ w.roomName }}MES状态显示</span>
            </div>
          </span>
          <span v-else-if="w.operationStatus.includes('故障') != true">
            <div class="ss1">
              <span style="display:block;width: 100%; text-align:center;" v-if="w.roomName.includes('555')">试验台架117-1
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('666')">试验台架117-2
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('777')">试验台架117-3
                MES状态显示</span>

              <span style="display:block;width: 100%; text-align:center;" v-else-if="w.roomName.includes('888')">试验台架117-4
                MES状态显示</span>
              <span style="display:block;width: 100%; text-align:center;" v-else>
                {{ w.roomName }}MES状态显示</span>
            </div>
          </span>


        </template>
        <!-- 停止运行 -->
        <span v-if="w.operationStatus.includes('故障') == true">
          <div class="text">
            <div>

              <span style="display: block; float: left;padding-left: 320px;">
                <el-button class="custom-button" type="primary" size="mini" @click="dataclear(w.id)">数据清零</el-button>
              </span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('试验中')">
                  台架状态：任务中--试验运行中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：任务中--试验运行中
                </el-tag></span>

              <!-- <span style="display: block; float: left;"><el-button
                  style=" border-radius:50%;width:30px;height:30px; margin-top: -7px; margin-left:50px ;"
                  icon="el-icon-refresh-right" class="b1" />

              </span> -->
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 120px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('试验暂停中')">
                  台架状态：任务中--试验生产组建中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：任务中--试验生产组建中
                </el-tag></span>

              <!-- <span style="display: block; float: left;"><el-button
                  style=" border-radius:50%;width:30px;height:30px; margin-top: -7px; margin-left:50px ;"
                  icon="el-icon-refresh-right" class="b1" />

              </span> -->
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 120px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('故障')">
                  台架状态：故障中--设备故障停机中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：故障中--设备故障停机中
                </el-tag></span>


            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 130px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('待机')">
                  台架状态：等待中--台架等待试验中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：等待中--台架等待试验中
                </el-tag>
              </span>
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 110px; ">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('设备调试运行')">
                  台架状态：非任务中——设备调试运行
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：非任务中——设备调试运行
                </el-tag>
              </span>
            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架有效运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalEffectiveRunningTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">试验暂停时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalTestStopTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架故障时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFaultTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架待机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalStandbyTime) }}</span>


            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架闲置时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFreeTime) }}</span>


            </div> -->
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">台架调试运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalDeviceDebugRunTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">台架例行点检/维护计量时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalweibaoTime) }}</span>


            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">稼动率统计：</span><span
                style="display: block;float: left;">{{ w.totalUtilizationRate }}</span>


            </div> -->
            <div>

              <span style="display: block; float: left;margin-top: -20px; padding-left: 220px;">
                <el-button type="primary" @click="weibaoAdd(w.roomName, 'on')" class="custom-button"
                  style="margin-left: -100px;">开始</el-button>
                <el-button type="primary" @click="weibaoAdd(w.roomName, 'off')" class="custom-button"
                  style="margin-left:  100px;">结束</el-button>
              </span>
            </div>
            <div style="margin-top: 30px;margin-left: 130px;">
              <span class="item"
                style="display: block;width: 180px; text-align:right;padding-left: 0px; float: left;"></span><span
                style="display: block;float: left;">
                <el-button class="custom-button" v-permissions="{ permission: ['role:edit'] }" type="primary"
                  @click="handleEdit(w)" size="mini">
                  时间输入
                </el-button>
              </span>

            </div>

          </div>
        </span>
        <!-- 运行 -->
        <span v-else>
          <div class="text1">
            <div>

              <span style="display: block; float: left;padding-left: 320px;">
                <el-button class="custom-button" type="primary" @click="dataclear(w.id)" size="mini">数据清零</el-button>
              </span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('试验中')">
                  台架状态：任务中--试验运行中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：任务中--试验运行中
                </el-tag></span>
              <!-- <span style="display: block; float: left;"><el-button
                  style=" border-radius:50%;width:30px;height:30px; margin-top: -7px; margin-left:50px ;"
                  icon="el-icon-refresh-right" class="b1" />

              </span> -->
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 120px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('试验暂停中')">
                  台架状态：任务中--试验生产组建中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：任务中--试验生产组建中
                </el-tag></span>

              <!-- <span style="display: block; float: left;"><el-button
                  style=" border-radius:50%;width:30px;height:30px; margin-top: -7px; margin-left:50px ;"
                  icon="el-icon-refresh-right" class="b1" />

              </span> -->
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 120px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('故障')">
                  台架状态：故障中--设备故障停机中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：故障中--设备故障停机中
                </el-tag></span>


            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 130px; ">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('待机')">
                  台架状态：等待中--台架等待试验中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：等待中--台架等待试验中
                </el-tag>
              </span>
            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 110px; ">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('设备调试运行')">
                  台架状态：非任务中——设备调试运行
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  台架状态：非任务中——设备调试运行
                </el-tag>
              </span>
            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架有效运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalEffectiveRunningTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">试验暂停时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalTestStopTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架故障时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFaultTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架待机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalStandbyTime) }}</span>


            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">台架闲置时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFreeTime) }}</span>


            </div> -->
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">台架调试运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalDeviceDebugRunTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">台架例行点检/维护计量时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalweibaoTime) }}</span>


            </div>
            <div>

              <span style="display: block; float: left;margin-top: -20px; padding-left: 220px;">
                <el-button type="primary" @click="weibaoAdd(w.roomName, 'on')" class="custom-button"
                  style="margin-left: -100px;">开始</el-button>
                <el-button type="primary" @click="weibaoAdd(w.roomName, 'off')" class="custom-button"
                  style="margin-left:  100px;">结束</el-button>
              </span>
            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">稼动率统计：</span><span
                style="display: block;float: left;">{{ w.totalUtilizationRate }}</span>


            </div> -->
            <div style="margin-top: 30px;margin-left: 130px;">
              <span class="item"
                style="display: block;width: 180px; text-align:right;padding-left: 100px; float: left;"></span><span
                style="display: block;float: left;">
                <el-button class="custom-button" v-permissions="{ permission: ['role:edit'] }" type="primary"
                  @click="handleEdit(w)" size="mini">
                  时间输入
                </el-button>
              </span>


            </div>

          </div>

        </span>

      </el-card>
      <edit ref="editRef" @fetch-data="fetchData" />
    </el-row>
  </div>
</template>

<script>

import { GetDeviceStatusNameAllList, doDelete, GetAllList, updateClear, roomstauscount } from '@/api/DeviceStatus/DeviceBaseRoom'
import { DoAddweibao, DoAddholiday, DoAddzhidu } from '@/api/DeviceStatus/TestRoomTimes'
import { doReFresh } from '@/api/DeviceStatus/DRoomReFreshTime'
import { DoAddWeibao } from '@/api/DeviceStatus/DRoomWeiBaoTimeOnOff'
import dayjs from 'dayjs'
import duration from 'dayjs/plugin/duration'
import edit from './roomDateTimeEdit'
import { Refresh } from '@element-plus/icons-vue'
export default defineComponent({
  name: 'RoleManagement',
  components: {
    edit
  },
  setup() {
    // const stopTimer = () => {
    //   clearInterval(timer.value)
    // }
    const $baseMessage = inject('$baseMessage')
    const state = reactive({
      editRef: null,
      permissionRef: null,
      list: [],
      timeList: [],
      deviceStatusNametotal: 0,
      deviceNametotal: 0,
      timer: null,
      layout: 'total, sizes, prev, pager, next, jumper',
      statusnuber: {},
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        role: '',
      }, weibaoModel: {
        onOrOff: '',
        roomName: '',
      },
      isActive: 0,
      sstt: [
        "ss1",
        "ss2",

      ],

      timeForm: {
        roomid: 0,
        startTime: '',
        endTime: '',
      },
    })
    const handleEdit = (row) => {
      if (row.id) {
        console.log(row.id)
        state.editRef.showEdit(row)
      } else {
        state['editRef'].showEdit()
      }
    }
    const weibaoAdd = async (roomName, onOrOff) => {
      state.weibaoModel.roomName = roomName
      state.weibaoModel.onOrOff = onOrOff
      const { msg } = await DoAddWeibao(state.weibaoModel)
      $baseMessage(msg, 'success', 'vab-hey-message-success')

    }
    const nian = (val) => {
      //  console.log(val)
      const years = Math.floor(val / (3600 * 24 * 365))
      const months = Math.floor(val % (3600 * 24 * 365) / (3600 * 24 * 30))
      const days = Math.floor(val % (3600 * 24 * 30) / (3600 * 24))
      const hours = Math.floor(val % (3600 * 24) / 3600)
      const minutes = Math.floor(val % 3600 / 60)

      return `${years}年 ${months}月 ${days}日 ${hours}小时 ${minutes}分钟`


    }
    const handleSizeChange = (val) => {
      state.queryForm.pageSize = val
      fetchData()
    }
    const handleCurrentChange = (val) => {
      state.queryForm.pageNo = val
      fetchData()
    }
    const fetchData = async () => {
      state.listLoading = true
      // const {
      //   data: { list, total },
      // } = await getList(state.queryForm)
      // state.list = list
      // console.log(list)
      // state.total = total
      // const {
      //   data: { list, total },
      // } = await getAllList(state.queryForm)
      // state.list = list
      // state.total = total
      const {
        data: { list, total },
      } = await GetAllList()
      state.list = list
      const statuss = await roomstauscount()
      state.statusnuber = statuss
      state.deviceStatusNametotal = total
      state.listLoading = false
    }
    const ReFresh = async () => {
      state.listLoading = true
      const { msg } = await doReFresh()
      $baseMessage(msg, 'success', 'vab-hey-message-success')
      state.listLoading = false
    }
    const dataclear = async (val) => {

      ElMessageBox.confirm(
        '是否数据清零?请联系领导再操作！',
        '警告',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
        }
      )
        .then(async () => {
          state.listLoading = true
          console.log(val)
          const { msg } = await updateClear(val)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          state.listLoading = false
        })
        .catch(() => {
          ElMessage({
            type: 'info',
            message: '取消',
          })
        })
    }
    const change = async (s, leibei, roombh) => {
      // console.log(s)
      const startTime = dayjs(s[0]).format('YYYY-MM-DDTHH:mm:ss')
      const endTime = dayjs(s[1]).format('YYYY-MM-DDTHH:mm:ss')
      state.timeForm.startTime = startTime
      state.timeForm.endTime = endTime

      state.timeForm.roomid = roombh
      if (leibei == ('维保')) {
        await DoAddweibao(state.timeForm)
      } if (leibei == ('制度')) {
        await DoAddzhidu(state.timeForm)
      }
      if (leibei == ('节假日')) {
        await DoAddholiday(state.timeForm)
      }
      // const diffInMilliseconds = endTime.diff(startTime)
      // const years = Math.floor(diffInMilliseconds / (365 * 24 * 60 * 60 * 1000));
      // const months = Math.floor(diffInMilliseconds / (30 * 24 * 60 * 60 * 1000));
      // const days = Math.floor(diffInMilliseconds / (24 * 60 * 60 * 1000)) % 30;
      // const hours = Math.floor(diffInMilliseconds / (60 * 60 * 1000)) % 24;
      // const minutes = Math.floor(diffInMilliseconds / (60 * 1000)) % 60;

      // // 格式化显示差异
      // const formattedDiff = `${years}年${months}月${days}日${hours}时${minutes}分`;

      //console.log(formattedDiff);
      ElMessageBox.alert('输入时间成功', '提示', {
        // if you want to disable its autofocus
        // autofocus: false,
        confirmButtonText: 'OK',


      })
      //console.log(years + '年' + months + '月' + days + '日' + hours + '时' + minutes + '分')
    }
    const save = (id) => {

      // state['formRef'].validate(async (valid) => {
      //   if (valid) {
      //     const submit = isAdd ? doAdd : doEdit
      //     const { msg } = await submit(state.form)
      //     $baseMessage(msg, 'success', 'vab-hey-message-success')
      //     emit('fetch-data')
      //     close()
      //   }
      // })
    }

    onMounted(async () => {
      fetchData(),




        state.value1 = '',
        state.value2 = '',
        state.value3 = '',
        // const newtatol = state.total
        // console.log(newtatol)
        setInterval(() => {
          fetchData()

          // console.log(state.total)
          // if (newtatol != state.total) window.confirm('来新订单嗯咯')
        }, 2000)
    })
    return {
      ...toRefs(state),
      handleSizeChange,
      handleEdit,
      handleCurrentChange,
      fetchData,
      save,
      change,
      nian,
      ReFresh,
      dataclear, weibaoAdd,
    }
  },
  beforeUnmount() { },
})

</script> 
<style lang="scss" scoped>
.el-card {
  padding: 0;
}

.active {
  background-color: red;
}

.custom-button {
  font-size: 0.8rem;
  /* 设置字体大小为0.8rem */
  /* 设置字体大小 */
  width: 55px;
  height: 10px;
  /* 设置按钮的内边距，调整按钮大小 */
}

.el-row {
  padding-left: 120px;

  &:last-child {
    margin-bottom: 0;

  }
}

.ss1 {
  margin-left: -20px;
  margin-top: -20px;
  margin-bottom: -19px;
  margin-right: -20px;
  padding: -100px -100px -100px -100px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 50px;
  background-color: rgb(49, 172, 243);
  //float: ;
}

.ss2 {
  margin-left: -20px;
  margin-top: -20px;
  margin-bottom: -19px;
  margin-right: -20px;
  padding: -100px -100px -100px -100px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 50px;
  background-color: rgb(231, 17, 17);


  //float: ;
}

.text {
  padding-top: 20px;
  margin-left: -20px;
  margin-top: -20px;
  margin-bottom: -19px;
  margin-right: -20px;
  font-size: 14px;
  width: 500px;
  height: 830px;
  background-color: rgb(236, 149, 149);
  //column-count: 3;
  display: flex;
  flex-wrap: wrap;
  // justify-content: center;
  flex-direction: column;
}

.text1 {
  padding-top: 20px;
  margin-left: -20px;
  margin-top: -20px;
  margin-bottom: -19px;
  margin-right: -20px;
  font-size: 14px;
  width: 500px;
  height: 830px;
  display: flex;
  flex-wrap: wrap;
  //justify-content: center;
  flex-direction: column;


}

.item {
  margin-bottom: 40px;
}


.box-card {
  padding: 0;
  margin: 10px;
  margin-bottom: 10px;
  width: 440px;
}




.b1 {
  border-radius: 50px;
  //color: #e9e3e3;

  background-color: rgba(48, 236, 23, 0.7);

  border-color: #1b80d3;
  box-sizing: 10px;

}

.b2 {
  border-radius: 50px;
  //color: #e9e3e3;
  background-color: rgba(250, 8, 8, 0.7);
  border-color: #da6935;
  box-sizing: 10px;
}
</style>

