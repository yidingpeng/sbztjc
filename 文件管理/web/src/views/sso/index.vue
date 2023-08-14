<template>
  <div class="login-container">
    <el-row>
      <el-col :lg="14" :md="11" :sm="24" :xl="14" :xs="24">
        <div style="color: transparent">占位符</div>
      </el-col>
      <el-col :lg="9" :md="12" :sm="24" :xl="9" :xs="24">
        <el-form class="login-form" label-position="left">
          <div class="title">SSO单点登录</div>
        </el-form>
      </el-col>
      <el-col :lg="1" :md="1" :sm="24" :xl="1" :xs="24">
        <div style="color: transparent">占位符</div>
      </el-col>
    </el-row>
  </div>
</template>
<script>
  import { useUserStore } from '@/store/modules/user'
  import { ElLoading } from 'element-plus'
  import { removeToken } from '@/utils/token'
  export default defineComponent({
    name: 'Sso',
    setup() {
      const route = useRoute()
      const router = useRouter()
      const userStore = useUserStore()
      const state = reactive({
        loginFormData: {
          username: '',
          rememberMe: true,
        },
        url: '',
        loginName: '',
        //redirect: undefined,
      })
      const handleSSO = async () => {
        if (state.loginName != '') {
          const loading = ElLoading.service({
            lock: true,
            text: 'SSO单点登录',
            background: 'rgba(0, 0, 0, 0.7)',
          })
          await removeToken()
          await userStore.ssologin(state.loginFormData)
          loading.close()
          //如果是全路径地址,则需要进行截取
          // let strurl = window.location.href.split('?')[1]
          // let strhttp = strurl.split('#')[1]
          // console.log(strhttp)
          // if (strhttp == undefined) {
          //   router.push('/index')
          // } else {
          //   router.push(strhttp)
          // }
          //如果是全路径地址,则需要进行截取
          router.push(state.url)
        } else {
          router.push('/login')
        }
      }
      onBeforeMount(() => {
        state.url = route.query.url
        state.loginName = route.query.loginName
        state.loginFormData.username = route.query.loginName
        localStorage['rememberMe'] = state.loginFormData.rememberMe
        handleSSO()
      })
      return {
        ...toRefs(state),
      }
    },
  })
</script>
<style lang="scss" scoped>
  .login-container {
    height: 100vh;
    background: url('~@/assets/login_images/background.jpg') center center fixed
      no-repeat;
    background-size: cover;
  }
  .login-form {
    position: relative;
    max-width: 100%;
    padding: 4.5vh;
    margin: calc((100vh - 475px) / 2) 5vw 5vw;
    overflow: hidden;
    background-size: 100% 100%;

    .title {
      font-size: 54px;
      font-weight: 500;
      color: var(--el-color-white);
    }
  }
</style>
