<template>
  <div class="personal-center-container">
    <el-row :gutter="20">
      <el-col :lg="8" :md="12" :sm="24" :xl="8" :xs="24">
        <vab-card shadow="hover">
          <div class="personal-center-user-info">
            <el-avatar :size="100" :src="avatar" @click="openDialog" />
            <div class="personal-center-user-info-full-name">
              {{ form.fullName }}
            </div>
            <div class="personal-center-user-info-description">
              {{ form.description }}
            </div>
            <!-- <div class="personal-center-user-info-follow">
              <a href="https://github.com/chuzhixin" target="_blank">
                <el-button round type="primary">
                  <vab-icon icon="group-line" />
                  Follow me
                </el-button>
              </a>
            </div> -->

            <ul class="personal-center-user-info-list">
              <li>
                <vab-icon icon="user-3-line" />
                {{ info.account }}
              </li>
              <li>
                <vab-icon icon="magic-line" />
                {{ form.fullname }}
              </li>
              <li>
                <vab-icon icon="women-line" />
                {{ form.sex }}
              </li>
              <li>
                <vab-icon icon="community-line" />
                {{ info.organization }}
              </li>
              <li>
                <vab-icon icon="phone-line" />
                {{ form.telnum }}
              </li>
            </ul>
          </div>
        </vab-card>
      </el-col>
      <el-col :lg="16" :md="12" :sm="24" :xl="16" :xs="24">
        <vab-card shadow="hover">
          <el-tabs v-model="activeName">
            <el-tab-pane label="基本信息" name="first">
              <el-col :lg="12" :md="16" :sm="24" :xl="12" :xs="24">
                <el-form label-width="80px" :model="form">
                  <el-form-item label="姓名">
                    <el-input
                      v-model="form.fullname"
                      maxlength="12"
                      show-word-limit
                    />
                  </el-form-item>
                  <el-form-item label="联系电话">
                    <el-input
                      v-model="form.telnum"
                      maxlength="11"
                      show-word-limit
                    />
                  </el-form-item>
                  <el-form-item label="性别">
                    <el-select v-model="form.sex" style="width: 100%">
                      <el-option label="保密" value="0" />
                      <el-option label="男" value="男" />
                      <el-option label="女" value="女" />
                    </el-select>
                  </el-form-item>
                  <el-form-item label="个人简介">
                    <el-input
                      v-model="form.description"
                      :autosize="{ minRows: 2, maxRows: 4 }"
                      maxlength="200"
                      show-word-limit
                      type="textarea"
                    />
                  </el-form-item>
                  <el-form-item>
                    <el-button type="primary" @click="onSubmit">保存</el-button>
                  </el-form-item>
                </el-form>
              </el-col>
            </el-tab-pane>
            <el-tab-pane label="修改密码" name="changepwd">
              <el-col :lg="12" :md="16" :sm="24" :xl="12" :xs="24">
                <el-form label-width="80px" :model="pwd">
                  <el-form-item label="当前账号">
                    <span>{{ info.account }}</span>
                  </el-form-item>
                  <el-form-item label="原始密码">
                    <el-input v-model="pwd.old" type="password" />
                  </el-form-item>
                  <el-form-item label="新密码">
                    <el-input v-model="pwd.new" type="password" />
                  </el-form-item>
                  <el-form-item label="确认密码">
                    <el-input v-model="pwd.confirm" type="password" />
                  </el-form-item>
                  <el-form-item>
                    <el-button type="primary" @click="onChangePwd">
                      确认修改
                    </el-button>
                  </el-form-item>
                </el-form>
              </el-col>
            </el-tab-pane>
          </el-tabs>
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import { useUserStore } from '@/store/modules/user'
  import { doEditPersonal, getPersonal, doChangePwd } from '@/api/system/user'

  export default defineComponent({
    name: 'PersonalCenter',
    setup() {
      const $baseMessage = inject('$baseMessage')

      const userStore = useUserStore()
      const { avatar } = storeToRefs(userStore)

      const _description = unescape(
        '\u5bcc\u5728\u672f\u6570\uff0c\u4e0d\u5728\u52b3\u8eab\uff1b\u5229\u5728\u52bf\u5c45\uff0c\u4e0d\u5728\u529b\u8015\u3002'
      )

      const state = reactive({
        activeName: 'first',
        avatar: avatar.value,
        info: {
          account: 'admin',
          organization: '湖南润伟-信息化部',
        },
        form: {
          fullname: '湖南润伟智能机器有限公司',
          sex: '男',
          age: 30,
          telnum: '13000000000',
          description: _description,
        },
        pwd: {
          old: '',
          new: '',
          confirm: '',
          reset: () => {
            this.old = ''
            this.new = ''
            this.confirm = ''
          },
        },
        inputRef: null,
        inputVisible: false,
        inputValue: '',
        rules: {
          pwd: [{ required: true }],
        },
      })

      onMounted(async () => {
        const {
          data: { userInfo, personal },
        } = await getPersonal()
        state.info = userInfo
        state.form = personal
      })

      const openDialog = () => {
        //state['vabCropperRef'].dialogVisible = true
      }
      const onSubmit = async () => {
        const { msg } = await doEditPersonal(state.form)
        $baseMessage(msg, 'success', 'vab-hey-message-success')
      }
      const onChangePwd = async () => {
        if (state.pwd.new != state.pwd.confirm) {
          $baseMessage(
            '两次输入的密码必须相同！',
            'error',
            'vab-hey-message-error'
          )
          pwd.confirm = ''
        } else if (state.pwd.new.length < 6 || state.pwd.confirm < 6) {
          $baseMessage('密码不能小于6位！', 'error', 'vab-hey-message-error')
          pwd.confirm = ''
        } else {
          const { msg } = await doChangePwd(state.pwd)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          state.pwd.reset()
        }
      }

      const handleClose = (tag) => {
        state.dynamicTags.splice(state.dynamicTags.indexOf(tag), 1)
      }

      const showInput = () => {
        state.inputVisible = true
        nextTick(() => {
          state.inputRef.focus()
        })
      }

      const handleInputConfirm = () => {
        if (state.inputValue) {
          state.dynamicTags.push(state.inputValue)
        }
        state.inputVisible = false
        state.inputValue = ''
      }

      return {
        ...toRefs(state),
        openDialog,
        onSubmit,
        onChangePwd,
        showInput,
        handleClose,
        handleInputConfirm,
      }
    },
  })
</script>

<style lang="scss" scoped>
  $base: '.personal-center';
  #{$base}-container {
    padding: 0 !important;
    background: $base-color-background !important;

    #{$base}-user-info {
      padding: $base-padding;
      text-align: center;

      :deep() {
        .el-avatar {
          img {
            cursor: pointer;
          }
        }
      }

      &-full-name {
        margin-top: 15px;
        font-size: 24px;
        font-weight: 500;
        color: #262626;
      }

      &-description {
        margin-top: 8px;
      }

      &-follow {
        margin-top: 15px;
      }

      &-list {
        margin-top: 18px;
        line-height: 30px;
        text-align: left;
        list-style: none;

        h5 {
          margin: -20px 0 5px 0;
        }

        :deep() {
          .el-tag {
            margin-right: 10px !important;
          }

          .el-tag + .el-tag {
            margin-left: 0;
          }
        }
      }
    }

    #{$base}-item {
      display: flex;

      i {
        font-size: 40px;
      }

      &-content {
        box-sizing: border-box;
        flex: 1;
        margin-left: $base-margin;

        &-second {
          margin-top: 8px;
        }
      }
    }
  }
</style>
