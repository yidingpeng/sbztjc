<template>
  <div class="role-management-container">
    <el-row><span class="item" style="display: block;width: 200px; text-align:right;margin-left: -40px; float: left;">
        <el-button type="primary" @click="ReFresh()">时间刷新</el-button>
      </span></el-row>
    <vab-card class="order-card1" shadow="hover">
      <template #header>
        <vab-icon icon="shopping-bag-2-line" />
        设备状态
      </template>
      <el-row class="order-card1-content">
        <el-col :span="8">
          <p>运行数量</p>
          <h1>
            {{ statusnuber.runNumber }}
          </h1>
        </el-col>
        <el-col :span="8">
          <p>停机数量</p>
          <h1>
            {{ statusnuber.stopNumber }}
          </h1>
        </el-col>
        <el-col :span="8">
          <p>故障数量</p>
          <h1>{{ statusnuber.faultNumber }}</h1>
        </el-col>
      </el-row>
    </vab-card>
    <el-row :gutter="40">


      <el-card class="box-card" v-for="w in  deviceNameList " :key="w.id">

        <template #header>


          <span v-if="w.operationStatus.includes('故障') ? isActive = 1 : isActive = 0">
            <div class="ss2">
              <span style="display:block;width: 100%; text-align:center;">{{
                w.deviceName }}</span>
            </div>
          </span>
          <span v-else-if="w.operationStatus.includes('故障') != true">
            <span>
              <div class="ss1">
                <span style="display:block;width: 100%; text-align:center;">{{ w.deviceName
                }}</span>
              </div>
            </span>
          </span>

        </template>
        <!-- 停止运行-->
        <span v-if="w.operationStatus.includes('故障') == true">
          <div class="text">
            <div>

              <span style="display: block; float: left;padding-left: 320px;">
                <el-button type="primary" @click="dataclear(w.id)" class="custom-button">数据清零</el-button>
              </span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="success" effect="dark" v-if="w.operationStatus.includes('运行中')">
                  设备状态：运行中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：运行中
                </el-tag></span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('停机中')">
                  设备状态：停机中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：停机中
                </el-tag></span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('故障中')">
                  设备状态：故障中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：故障中
                </el-tag></span>

            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备总运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalRunTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备总停机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalStopTime) }}</span>


            </div>

            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备故障停机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFaultDownTime) }}</span>
            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备故障次数：</span><span
                style="display: block;float: left;">{{ w.toalFaultNumber }}次</span>

            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备闲置时间
                ：</span><span style="display: block;float: left;">{{ nian(w.totalFreeTime) }}</span>

            </div> -->
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">设备例行点检/维护计量时间：</span><span
                style="display: block;float: left;">{{ nian(w.weibaoTime) }}</span>
            </div>
            <div>

              <span style="display: block; float: left;margin-top: -20px; padding-left: 220px;">
                <el-button type="primary" @click="weibaoAdd(w.deviceName, 'on')" class="custom-button"
                  style="margin-left: -100px;">开始</el-button>
                <el-button type="primary" @click="weibaoAdd(w.deviceName, 'off')" class="custom-button"
                  style="margin-left:  100px;">结束</el-button>
              </span>
            </div>
            <!-- <div style="margin-top: 30px;margin-left: 130px;">
              <span class="item"
                style="display: block;width: 180px; text-align:right;padding-left: 0px; float: left;"></span><span
                style="display: block;float: left;">
                <el-button v-permissions="{ permission: ['role:edit'] }" type="primary" @click="handleEdit(w)"
                  class="custom-button">
                  时间输入
                </el-button>
              </span>

            </div> -->
          </div>

        </span>
        <!-- 运行 -->
        <span v-else>
          <div class="text1">
            <div>

              <span style="display: block; float: left;padding-left: 320px;">
                <el-button type="primary" @click="dataclear(w.id)" class="custom-button">数据清零</el-button>
              </span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="success" effect="dark" v-if="w.operationStatus.includes('运行中')">
                  设备状态：运行中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：运行中
                </el-tag></span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="" effect="dark" v-if="w.operationStatus.includes('停机中')"
                  style="color: rgb(235, 232, 232); background-color: black;">
                  设备状态：停机中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：停机中
                </el-tag></span>

            </div>
            <div>
              <span class="item" style="display: block;width: 200px; text-align:right;padding-left: 150px; float: left;">
                <el-tag type="danger" effect="dark" v-if="w.operationStatus.includes('故障中')">
                  设备状态：故障中
                </el-tag>
                <el-tag type="info" style="color: rgb(12, 12, 12);" v-else>
                  设备状态：故障中
                </el-tag></span>

            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备总运行时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalRunTime) }}</span>


            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备总停机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalStopTime) }}</span>


            </div>

            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备故障停机时间：</span><span
                style="display: block;float: left;">{{ nian(w.totalFaultDownTime) }}</span>
            </div>
            <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备故障次数：</span><span
                style="display: block;float: left;">{{ w.toalFaultNumber }}次</span>

            </div>
            <!-- <div>
              <span class="item"
                style="display: block;width: 200px; text-align:right;padding-left: 20px; float: left;">设备闲置时间
                ：</span><span style="display: block;float: left;">{{ nian(w.totalFreeTime) }}</span>

            </div> -->
            <div>
              <span class="item"
                style="display: block;width: 220px; text-align:right;padding-left: 20px; float: left;">设备例行点检/维护计量时间：</span><span
                style="display: block;float: left;">{{ nian(w.weibaoTime) }}</span>


            </div>
            <div>

              <span style="display: block; float: left;margin-top: -20px; padding-left: 220px;">
                <el-button type="primary" @click="weibaoAdd(w.deviceName, 'on')" class="custom-button"
                  style="margin-left: -100px;">开始</el-button>
                <el-button type="primary" @click="weibaoAdd(w.deviceName, 'off')" class="custom-button"
                  style="margin-left:   100px;">结束</el-button>
              </span>
            </div>
            <!-- <div style="margin-top: 30px;margin-left: 130px;">
              <span class="item"
                style="display: block;width: 180px; text-align:right;padding-left: 0px; float: left;"></span><span
                style="display: block;float: left;">
                <el-button v-permissions="{ permission: ['role:edit'] }" type="primary" @click="handleEdit(w)"
                  class="custom-button">
                  时间输入
                </el-button>
              </span>

            </div> -->
          </div>

        </span>
      </el-card>
      <edit ref="editRef" @fetch-data="fetchData" />
    </el-row>
  </div>
</template>

<script>
import { GetDeviceStatusNameAllList, doDelete, GetDeviceNameAllList, updateClear, devicestauscount } from '@/api/DeviceStatus/DeviceStatusTag'
import { doReFresh } from '@/api/DeviceStatus/DevicereFreshTime'
import { DoAddWeibao } from '@/api/DeviceStatus/DevcieWeiBaoTimeOnOff'
import dayjs from 'dayjs'
import duration from 'dayjs/plugin/duration';
import edit from './deviceDateTimeEdit'
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
      deviceStatusNamelist: [],
      deviceNameList: [],
      deviceStatusNametotal: 0,
      deviceNametotal: 0,
      timer: null,
      layout: 'total, sizes, prev, pager, next, jumper',
      queryForm: {
        pageNo: 1,
        pageSize: 10,
        role: '',
      }, statusnuber: {},
      weibaoModel: {
        onOrOff: '',
        deviceName: '',
      },
      isActive: 0,
      sstt: [
        "ss1",
        "ss2",

      ],
    })
    const handleEdit = (row) => {
      if (row.id) {
        console.log(row.id)
        state.editRef.showEdit(row)
      } else {
        state['editRef'].showEdit()
      }
    }
    const weibaoAdd = async (deviceName, onOrOff) => {
      state.weibaoModel.deviceName = deviceName
      state.weibaoModel.onOrOff = onOrOff
      const { msg } = await DoAddWeibao(state.weibaoModel)
      $baseMessage(msg, 'success', 'vab-hey-message-success')

    }
    const handleSizeChange = (val) => {
      state.queryForm.pageSize = val
      fetchData()
    }
    const handleCurrentChange = (val) => {
      state.queryForm.pageNo = val
      fetchData()
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
          console.log(val)
          state.listLoading = true
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
    const fetchData = async () => {
      state.listLoading = true

      // const {
      //   data: { list, total },
      // } = await GetDeviceStatusNameAllList()
      // state.deviceStatusNamelist = list
      // console.log(list)
      // state.deviceStatusNametotal = total
      // state.listLoading = false

      const statuss = await devicestauscount()
      state.statusnuber = statuss
    }
    const getDeviceList = async () => {
      state.listLoading = true

      const {
        data: { list, total },
      } = await GetDeviceNameAllList()
      state.deviceNameList = list
      //console.log(list)
      state.deviceNametotal = total
      state.listLoading = false
    }
    const nian = (val) => {
      // console.log(val)
      const years = Math.floor(val / (3600 * 24 * 365))
      const months = Math.floor(val % (3600 * 24 * 365) / (3600 * 24 * 30))
      const days = Math.floor(val % (3600 * 24 * 30) / (3600 * 24))
      const hours = Math.floor(val % (3600 * 24) / 3600)
      const minutes = Math.floor(val % 3600 / 60)

      return `${years}年 ${months}月 ${days}日 ${hours}小时 ${minutes}分钟`


    }

    onMounted(() => {
      fetchData(),
        getDeviceList()
      // const newtatol = state.total
      // console.log(newtatol)
      setInterval(() => {
        fetchData()
        getDeviceList()
        // console.log(state.total)
        // if (newtatol != state.total) window.confirm('来新订单嗯咯')
      }, 2000)
    })
    return {
      ...toRefs(state),
      handleSizeChange,
      handleCurrentChange,
      fetchData,
      getDeviceList,
      nian,
      handleEdit,
      ReFresh,
      dataclear,
      weibaoAdd,
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

.el-row {
  padding-left: 120px;

  &:last-child {
    margin-bottom: 0;

  }
}

.custom-button {
  font-size: 0.8rem;
  /* 设置字体大小为0.8rem */
  /* 设置字体大小 */
  width: 55px;
  height: 10px;
  /* 设置按钮的内边距，调整按钮大小 */
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
  height: 550px;
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
  height: 550px;
  display: flex;
  flex-wrap: wrap;
  //justify-content: center;
  flex-direction: column;


}

.item {
  margin-bottom: 28px;
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
  background-color: rgba(215, 221, 213, 0.7);
  border-color: #e8ebee;
  box-sizing: 10px;
}
</style>

