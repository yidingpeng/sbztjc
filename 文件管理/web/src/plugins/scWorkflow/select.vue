<template>
  <el-dialog
    v-model="dialogVisible"
    append-to-body
    destroy-on-close
    :title="titleMap[type - 1]"
    :width="type == 1 ? 680 : 460"
    @closed="$emit('closed')"
  >
    <template v-if="type == 1">
      <div class="sc-user-select">
        <div class="sc-user-select__left">
          <div class="sc-user-select__search">
            <el-input
              v-model="keyword"
              placeholder="搜索成员"
              :prefix-icon="Search"
            >
              <template #append>
                <el-button :icon="Search" @click="search" />
              </template>
            </el-input>
          </div>
          <div class="sc-user-select__select">
            <div v-loading="showGrouploading" class="sc-user-select__tree">
              <el-scrollbar>
                <el-tree
                  ref="groupTree"
                  class="menu"
                  :current-node-key="groupId"
                  :data="group"
                  :expand-on-click-node="false"
                  highlight-current
                  :node-key="groupProps.key"
                  :props="groupProps"
                  @node-click="groupClick"
                />
              </el-scrollbar>
            </div>
            <div v-loading="showUserloading" class="sc-user-select__user">
              <div class="sc-user-select__user__list">
                <el-scrollbar ref="userScrollbar">
                  <el-tree
                    ref="userTree"
                    check-on-click-node
                    class="menu"
                    :data="user"
                    :default-checked-keys="selectedIds"
                    :node-key="userProps.key"
                    :props="userProps"
                    show-checkbox
                    @check-change="userClick"
                  />
                </el-scrollbar>
              </div>
              <footer>
                <el-pagination
                  v-model:currentPage="currentPage"
                  background
                  layout="prev,next"
                  :page-size="pageSize"
                  small
                  :total="total"
                  @current-change="paginationChange"
                />
              </footer>
            </div>
          </div>
        </div>
        <div class="sc-user-select__toicon">
          <vab-icon icon="arrow-right-s-line" />
        </div>
        <div class="sc-user-select__selected">
          <header>已选 ({{ selected.length }})</header>
          <ul>
            <el-scrollbar>
              <li v-for="(item, index) in selected" :key="item.id">
                <span class="name">
                  <el-avatar size="small">
                    {{ item.name.substring(0, 1) }}
                  </el-avatar>
                  <label>{{ item.name }}</label>
                </span>
                <span class="delete">
                  <el-button
                    circle
                    icon="delete"
                    link
                    size="small"
                    type="primary"
                    @click="deleteSelected(index)"
                  />
                </span>
              </li>
            </el-scrollbar>
          </ul>
        </div>
      </div>
    </template>

    <template v-if="type == 2">
      <div class="sc-user-select sc-user-select-role">
        <div class="sc-user-select__left">
          <div class="sc-user-select__select">
            <div v-loading="showGrouploading" class="sc-user-select__tree">
              <el-scrollbar>
                <el-tree
                  ref="groupTree"
                  check-on-click-node
                  check-strictly
                  class="menu"
                  :data="role"
                  :default-checked-keys="selectedIds"
                  :expand-on-click-node="false"
                  :node-key="roleProps.key"
                  :props="roleProps"
                  show-checkbox
                  @check-change="roleClick"
                />
              </el-scrollbar>
            </div>
          </div>
        </div>
        <div class="sc-user-select__toicon">
          <vab-icon icon="arrow-right-s-line" />
        </div>
        <div class="sc-user-select__selected">
          <header>已选 ({{ selected.length }})</header>
          <ul>
            <el-scrollbar>
              <li v-for="(item, index) in selected" :key="item.id">
                <span class="name">
                  <label>{{ item.name }}</label>
                </span>
                <span class="delete">
                  <el-button
                    circle
                    icon="delete"
                    link
                    size="mini"
                    type="primary"
                    @click="deleteSelected(index)"
                  />
                </span>
              </li>
            </el-scrollbar>
          </ul>
        </div>
      </div>
    </template>

    <template #footer>
      <el-button @click="dialogVisible = false">取 消</el-button>
      <el-button type="primary" @click="save">确 认</el-button>
    </template>
  </el-dialog>
</template>

<script>
  import * as ElIcon from '@element-plus/icons-vue'
  import config from '@/config/workflow'

  export default {
    props: {
      modelValue: { type: Boolean, default: false },
    },
    emits: ['closed'],
    data() {
      return {
        groupProps: config.group.props,
        userProps: config.user.props,
        roleProps: config.role.props,

        titleMap: ['人员选择', '角色选择'],
        dialogVisible: false,
        showGrouploading: false,
        showUserloading: false,
        keyword: '',
        groupId: '',
        pageSize: config.user.pageSize,
        total: 0,
        currentPage: 1,
        group: [],
        user: [],
        role: [],
        type: 1,
        selected: [],
        value: [],
        ...ElIcon,
      }
    },
    computed: {
      selectedIds() {
        return this.selected.map((t) => t.id)
      },
    },
    mounted() {},
    methods: {
      //打开赋值
      open(type, data) {
        this.type = type
        this.value = data || []
        this.selected = JSON.parse(JSON.stringify(data || []))
        this.dialogVisible = true

        if (this.type == 1) {
          this.getGroup()
          this.getUser()
        } else if (this.type == 2) {
          this.getRole()
        }
      },
      //获取组织
      async getGroup() {
        this.showGrouploading = true

        const res = await config.group.apiObj()
        this.showGrouploading = false
        const allNode = {
          [config.group.props.key]: '',
          [config.group.props.label]: '所有',
        }
        res.data.unshift(allNode)
        this.group = config.group.parseData(res).rows
        //console.log(this.group)
      },
      //获取用户
      async getUser() {
        this.showUserloading = true
        const params = {
          [config.user.request.keyword]: this.keyword || null,
          [config.user.request.groupId]: this.groupId || null,
          [config.user.request.page]: this.currentPage,
          [config.user.request.pageSize]: this.pageSize,
        }
        const res = await config.user.apiObj(params)
        this.showUserloading = false
        this.user = config.user.parseData(res).rows
        this.total = config.user.parseData(res).total || 0
        this.$refs.userScrollbar.setScrollTop(0)
      },
      //获取角色
      async getRole() {
        this.showGrouploading = true
        const res = await config.role.apiObj()
        this.showGrouploading = false
        this.role = config.role.parseData(res).rows
      },
      //组织点击
      groupClick(data) {
        this.keyword = ''
        this.currentPage = 1
        this.groupId = data[config.group.props.key]
        this.getUser()
      },
      //用户点击
      userClick(data, checked) {
        if (checked) {
          this.selected.push({
            id: data[config.user.props.key],
            name: data[config.user.props.label],
          })
        } else {
          this.selected = this.selected.filter(
            (item) => item.id != data[config.user.props.key]
          )
        }
      },
      //用户分页点击
      paginationChange() {
        this.getUser()
      },
      //用户搜索
      search() {
        this.groupId = ''
        this.$refs.groupTree.setCurrentKey(this.groupId)
        this.currentPage = 1
        this.getUser()
      },
      //删除已选
      deleteSelected(index) {
        this.selected.splice(index, 1)
        if (this.type == 1) {
          this.$refs.userTree.setCheckedKeys(this.selectedIds)
        } else if (this.type == 2) {
          this.$refs.groupTree.setCheckedKeys(this.selectedIds)
        }
      },
      //角色点击
      roleClick(data, checked) {
        if (checked) {
          this.selected.push({
            id: data[config.role.props.key],
            name: data[config.role.props.label],
          })
        } else {
          this.selected = this.selected.filter(
            (item) => item.id != data[config.role.props.key]
          )
        }
      },
      //提交保存
      save() {
        this.value.splice(0, this.value.length)
        this.selected.map((item) => {
          this.value.push(item)
        })
        this.dialogVisible = false
      },
    },
  }
</script>

<style scoped>
  .sc-user-select {
    display: flex;
  }
  .sc-user-select__left {
    width: 400px;
  }
  .sc-user-select__right {
    flex: 1;
  }

  .sc-user-select__search {
    padding-bottom: 10px;
  }

  .sc-user-select__select {
    display: flex;
    background: var(--el-color-white);
    border: 1px solid var(--el-border-color-light);
  }
  .sc-user-select__tree {
    width: 200px;
    height: 300px;
    border-right: 1px solid var(--el-border-color-light);
  }
  .sc-user-select__user {
    display: flex;
    flex-direction: column;
    width: 200px;
    height: 300px;
  }
  .sc-user-select__user__list {
    flex: 1;
    overflow: auto;
  }
  .sc-user-select__user footer {
    height: 36px;
    padding-top: 5px;
    border-top: 1px solid var(--el-border-color-light);
  }

  .sc-user-select__toicon {
    display: flex;
    align-items: center;
    justify-content: center;
    margin: 0 10px;
  }
  .sc-user-select__toicon i {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 20px;
    height: 20px;
    line-height: 20px;
    color: #fff;
    text-align: center;
    background: #ccc;
    border-radius: 50%;
  }

  .sc-user-select__selected {
    width: 200px;
    height: 345px;
    background: var(--el-color-white);
    border: 1px solid var(--el-border-color-light);
  }
  .sc-user-select__selected header {
    height: 43px;
    padding: 0 15px;
    font-size: 12px;
    line-height: 43px;
    border-bottom: 1px solid var(--el-border-color-light);
  }
  .sc-user-select__selected ul {
    height: 300px;
    overflow: auto;
  }
  .sc-user-select__selected li {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 38px;
    padding: 5px 5px 5px 15px;
  }
  .sc-user-select__selected li .name {
    display: flex;
    align-items: center;
  }
  .sc-user-select__selected li .name .el-avatar {
    margin-right: 10px;
    background: #409eff;
  }
  .sc-user-select__selected li .name label {
  }
  .sc-user-select__selected li .delete {
    display: none;
  }
  .sc-user-select__selected li:hover {
    background: var(--el-color-primary-light-9);
  }
  .sc-user-select__selected li:hover .delete {
    display: inline-block;
  }

  .sc-user-select-role .sc-user-select__left {
    width: 200px;
  }
  .sc-user-select-role .sc-user-select__tree {
    height: 343px;
    border: none;
  }
  .sc-user-select-role .sc-user-select__selected {
  }

  [data-theme='dark'] .sc-user-select__selected li:hover {
    background: rgba(0, 0, 0, 0.2);
  }
  [data-theme='dark'] .sc-user-select__toicon i {
    background: #383838;
  }
</style>
