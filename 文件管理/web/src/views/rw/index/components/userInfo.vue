<template>
  <el-col :span="24">
    <vab-card class="user-info" shadow="hover">
      <template #header>基本信息</template>
      <el-row>
        <el-col :md="6" :sm="12">
          <div class="item">
            <el-avatar :size="60" :src="personal.avatar" />
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>用户名</div>
            <div class="data">{{ userInfo.account }}</div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>姓名</div>
            <div class="data">{{ personal.fullname }}</div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>部门</div>
            <div class="data">{{ userInfo.organization }}</div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>角色</div>
            <div class="data">管理员</div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>性别</div>
            <div class="data">{{ personal.sex }}</div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>上次登录</div>
            <div class="data">
              {{
                userInfo.lastLogin == '0001-01-01 00:00:00'
                  ? '从未登录'
                  : userInfo.lastLogin
              }}
            </div>
          </div>
        </el-col>
        <el-col :md="6" :sm="12">
          <div class="item">
            <div>登录次数</div>
            <div class="data">{{ userInfo.loginCount }}次</div>
          </div>
        </el-col>
      </el-row>
    </vab-card>
  </el-col>
</template>

<script>
  import { getPersonal } from '@/api/system/user'

  export default defineComponent({
    name: 'IndexUser',
    setup() {},
    data() {
      return { personal: {}, userInfo: {} }
    },
    async mounted() {
      const {
        data: { personal, userInfo },
      } = await getPersonal()
      this.personal = personal
      this.userInfo = userInfo
    },
  })
</script>

<style lang="scss" scoped>
  .user-info {
    .item {
      width: 100%;
      text-align: center;

      .data {
        margin: 20px 0;
        font-size: 25px;
        font-weight: bold;
      }
    }
  }
</style>
