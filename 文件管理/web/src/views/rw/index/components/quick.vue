<template>
  <el-col :span="24">
    <el-row :gutter="20">
      <el-col
        v-for="(item, index) in iconList"
        :key="index"
        :lg="3"
        :md="6"
        :sm="6"
        :xl="3"
        :xs="12"
      >
        <vab-card class="icon-panel" shadow="hover" @click="handleQuick(item)">
          <vab-icon :icon="item.icon" :style="{ color: item.color }" />
          <p>{{ item.title }}</p>
        </vab-card>
      </el-col>
      <el-col :lg="3" :md="6" :sm="6" :xl="3" :xs="12">
        <vab-card class="icon-panel" @click="handleAddItem">
          <vab-icon icon="add-line" :style="{ color: '#484848' }" />
          <p>管理快捷项</p>
        </vab-card>
      </el-col>
    </el-row>
  </el-col>

  <el-dialog v-model="addItemVisible">
    <template #title>快捷方式管理</template>
    <el-row :gutter="10">
      <el-col
        v-for="(item, index) in iconList"
        :key="index"
        :lg="4"
        :md="6"
        :sm="6"
        :xl="3"
        :xs="12"
      >
        <vab-card class="icon-panel edit" @click="selectEdit(item)">
          <vab-icon :icon="item.icon" :style="{ color: item.color }" />
          <p>{{ item.title }}</p>
        </vab-card>
      </el-col>
      <el-col :lg="4" :md="6" :sm="6" :xl="3" :xs="12">
        <vab-card class="icon-panel" @click="handleNew">
          <vab-icon icon="add-line" />
          <p>添加新项</p>
        </vab-card>
      </el-col>
    </el-row>
    <h2 style="text-align: center">编辑快捷项</h2>
    <el-row>
      <el-col :offset="4" :span="16">
        <el-form :label-width="70">
          <el-form-item label="快捷名称">
            <el-input
              v-model="form.title"
              link
              placeholder="输入名称"
              type="primary"
            />
          </el-form-item>
          <el-form-item label="图标">
            <el-popover
              ref="popoverRef"
              placement="top-start"
              popper-class="icon-selector-popper"
              trigger="click"
              :width="400"
            >
              <template #reference>
                <el-input
                  v-model="form.icon"
                  placeholder="点击选择图标"
                  readonly
                >
                  <template v-if="form.icon" #prefix>
                    <vab-icon :icon="form.icon" />
                  </template>
                </el-input>
              </template>
              <vab-icon-selector @handle-icon="handleIcon" />
            </el-popover>
          </el-form-item>
          <el-form-item label="颜色">
            <el-input v-model="form.color" placeholder="选择或输入颜色值">
              <template #append>
                <el-color-picker v-model="form.color" />
              </template>
            </el-input>
          </el-form-item>
          <el-form-item label="链接方式">
            <el-radio-group v-model="form.type">
              <el-radio :label="0">站内链接</el-radio>
              <el-radio :label="1">自定义链接</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="链接地址">
            <el-input
              v-if="form.type == 1"
              v-model="form.link"
              link
              type="primary"
            />
            <rw-module v-if="form.type != 1" v-model="form.link" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="handleSaveQuick(false)">
              保存
            </el-button>
            <el-button type="primary" @click="handleSaveQuick(true)">
              保存并继续
            </el-button>
            <el-button
              v-if="form != null && form.id > 0"
              type="danger"
              @click="handleRemove(form)"
            >
              删除该项
            </el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
  </el-dialog>
</template>

<script>
  import VabIconSelector from '@/plugins/VabIconSelector'
  import RwModule from '@/plugins/RwModule'
  import { getList, doDelete, doAdd, doEdit } from '@/api/user/quick'

  export default defineComponent({
    components: { VabIconSelector, RwModule },
    inject: ['$baseMessage'],
    setup() {},
    data() {
      // 卡片图标
      const iconList = []

      return {
        iconList,
        addItemVisible: false,
        form: {
          id: 0,
          title: '',
          icon: '',
          type: 0,
          link: '',
          color: '#0b0b0b',
        },
      }
    },
    mounted() {
      this.fetchData()
    },
    methods: {
      async fetchData() {
        const {
          data: { list },
        } = await getList()
        this.iconList = list
      },
      handleQuick(item) {
        console.log('click item :', item)
        //$baseAlert('开发中...' + JSON.stringify(item))
        if (item.click) {
          this[item.click]()
        } else if (item.link) {
          if (item.link.indexOf('http') == 0) window.open(item.link)
          else this.$router.push(item.link)
        }
      },
      handleAddItem() {
        //console.log('add item clicked.')
        this.addItemVisible = true
        console.log(this.addItemVisible)
      },
      handleIcon(item) {
        console.log('this is .', this)
        this.form.icon = item
        this.$refs['popoverRef'].hide()
      },
      async handleSaveQuick(noClose) {
        console.log('saved', this.form)
        if (this.form.id == 0) {
          this.iconList.splice(this.iconList.length, 0, this.form)
          const { msg } = await doAdd(this.form)
          this.$baseMessage(msg, 'success', 'vab-hey-message-success')
        } else if (this.form.id > 0) {
          const id = this.form.id
          const index = this.iconList.findIndex((x) => x.id == id)
          this.iconList.splice(index, 1, this.form)
          const { msg } = await doEdit(id, this.form)
          this.$baseMessage(msg, 'success', 'vab-hey-message-success')
        }
        this.fetchData()
        if (!noClose) this.addItemVisible = false
        this.handleNew()
      },
      handleNew() {
        this.form = { id: 0 }
      },
      async handleRemove(item) {
        const id = item.id
        const index = this.iconList.findIndex((x) => x == item)
        this.iconList.splice(index, 1)
        const { msg } = await doDelete(id)
        this.$baseMessage(msg, 'success', 'vab-hey-message-success')
        this.fetchData()
      },
      selectEdit(item) {
        console.log(item)
        this.form = item
      },
    },
  })
</script>

<style lang="scss" scoped>
  @use 'sass:math';
  .icon-panel {
    margin-bottom: 20px;
    text-align: center;
    cursor: pointer;

    :deep() {
      .el-card__body {
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        height: 120px;
        min-height: 80px;
        max-height: 100px;
        padding: #{math.div($base-padding, 1.4)};
        cursor: pointer;

        i {
          margin-top: 10px;
          font-size: 45px;
          color: var(--el-color-grey);
          text-align: center;
          pointer-events: none;
          cursor: pointer;
          transition: $base-transition;
        }
      }
    }
  }

  .edit {
    :deep() {
      .el-card__body {
        &::after {
          position: absolute;
          bottom: -30px;
          width: 100%;
          padding: 4px 0;
          font-size: $base-font-size-small;
          color: rgb(255, 255, 255);
          text-align: center;
          content: '编辑';
          background-color: var(--el-color-primary);
          transition: $base-transition;
        }

        &:hover {
          .close {
            display: flex;
          }
          i:not(class) {
            margin-top: -#{math.div($base-margin, 1.2)};
          }

          &::after {
            bottom: 0;
          }
        }
        .close {
          position: absolute;
          top: 20px;
          right: 0;
          display: none;
        }
        .close:hover {
          background-color: #464646;
        }
      }
    }
  }
</style>
