<template>
  <div class="branch-wrap">
    <div class="branch-box-wrap">
      <div class="branch-box">
        <el-button
          v-if="edit"
          class="add-branch"
          plain
          round
          type="success"
          @click="addTerm"
        >
          添加条件
        </el-button>
        <div
          v-for="(item, nIndex) in nodeConfig.conditionNodes"
          :key="nIndex"
          class="col-box"
        >
          <div class="condition-node">
            <div class="condition-node-box">
              <div class="auto-judge" @click="show(nIndex)">
                <div
                  v-if="edit && nIndex != 0"
                  class="sort-left"
                  @click.stop="arrTransfer(nIndex, -1)"
                >
                  <vab-icon icon="arrow-left-s-fill" />
                </div>
                <div class="title">
                  <span class="node-title">{{ item.nodeName }}</span>
                  <span class="priority-title">
                    优先级{{ item.priorityLevel }}
                  </span>
                  <vab-icon
                    v-if="edit"
                    class="close"
                    icon="close-fill"
                    @click.stop="delTerm(nIndex)"
                  />
                </div>
                <div class="content">
                  <span v-if="toText(nodeConfig, nIndex)">
                    {{ toText(nodeConfig, nIndex) }}
                  </span>
                  <span v-else class="placeholder">请设置条件</span>
                </div>
                <div
                  v-if="edit && nIndex != nodeConfig.conditionNodes.length - 1"
                  class="sort-right"
                  @click.stop="arrTransfer(nIndex)"
                >
                  <vab-icon icon="arrow-right-s-fill" />
                </div>
              </div>
              <add-node v-model="item.childNode" />
            </div>
          </div>
          <slot v-if="item.childNode" :node="item"></slot>
          <div v-if="nIndex == 0" class="top-left-cover-line"></div>
          <div v-if="nIndex == 0" class="bottom-left-cover-line"></div>
          <div
            v-if="nIndex == nodeConfig.conditionNodes.length - 1"
            class="top-right-cover-line"
          ></div>
          <div
            v-if="nIndex == nodeConfig.conditionNodes.length - 1"
            class="bottom-right-cover-line"
          ></div>
        </div>
      </div>
      <add-node v-model="nodeConfig.childNode" />
    </div>
    <el-drawer
      v-model="drawer"
      append-to-body
      destroy-on-close
      :size="600"
      title="条件设置"
    >
      <template #title>
        <div class="node-wrap-drawer__title">
          <label v-if="!isEditTitle" @click="editTitle">
            {{ form.nodeName }}
            <vab-icon class="node-wrap-drawer__title-edit" icon="edit-fill" />
          </label>
          <el-input
            v-if="isEditTitle"
            ref="nodeTitle"
            v-model="form.nodeName"
            clearable
            @blur="saveTitle"
            @keyup.enter="saveTitle"
          />
        </div>
      </template>
      <el-container>
        <el-main style="padding: 0 20px 20px 20px">
          <el-form label-position="top">
            <el-form-item label="条件关系">
              <el-radio-group v-model="form.conditionMode">
                <el-radio :label="1">且</el-radio>
                <el-radio :label="2">或</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-divider />
            <el-form-item>
              <el-table :data="form.conditionList">
                <el-table-column label="描述" prop="label">
                  <template #default="scope">
                    <el-input v-model="scope.row.label" placeholder="描述" />
                  </template>
                </el-table-column>
                <el-table-column label="条件字段" prop="field" width="130">
                  <template #default="scope">
                    <el-input
                      v-model="scope.row.field"
                      placeholder="条件字段"
                    />
                  </template>
                </el-table-column>
                <el-table-column label="运算符" prop="operator" width="130">
                  <template #default="scope">
                    <el-select
                      v-model="scope.row.operator"
                      placeholder="Select"
                    >
                      <el-option label="等于" value="=" />
                      <el-option label="不等于" value="!=" />
                      <el-option label="大于" value=">" />
                      <el-option label="大于等于" value=">=" />
                      <el-option label="小于" value="<" />
                      <el-option label="小于等于" value="<=" />
                      <el-option label="包含" value="include" />
                      <el-option label="不包含" value="notinclude" />
                    </el-select>
                  </template>
                </el-table-column>
                <el-table-column label="值" prop="value" width="100">
                  <template #default="scope">
                    <el-input v-model="scope.row.value" placeholder="值" />
                  </template>
                </el-table-column>
                <el-table-column label="移除" prop="value" width="55">
                  <template #default="scope">
                    <el-button
                      link
                      size="small"
                      type="primary"
                      @click="deleteConditionList(scope.$index)"
                    >
                      移除
                    </el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-form-item>
            <p>
              <el-button
                :icon="Plus"
                round
                type="primary"
                @click="addConditionList"
              >
                增加条件
              </el-button>
            </p>
          </el-form>
        </el-main>
        <el-footer>
          <el-button type="primary" @click="save">保存</el-button>
          <el-button @click="drawer = false">取消</el-button>
        </el-footer>
      </el-container>
    </el-drawer>
  </div>
</template>

<script>
  import * as ElIcon from '@element-plus/icons-vue'
  import addNode from './addNode'

  export default {
    components: {
      addNode,
    },
    inject: ['edit'],
    props: {
      modelValue: { type: Object, default: () => {} },
    },
    emits: ['update:modelValue'],
    data() {
      return {
        nodeConfig: {},
        drawer: false,
        isEditTitle: false,
        index: 0,
        form: {},
        ...ElIcon,
      }
    },
    watch: {
      modelValue() {
        this.nodeConfig = this.modelValue
      },
    },
    mounted() {
      this.nodeConfig = this.modelValue
    },
    methods: {
      show(index) {
        if (!this.edit) return
        this.index = index
        this.form = {}
        this.form = JSON.parse(
          JSON.stringify(this.nodeConfig.conditionNodes[index])
        )
        this.drawer = true
      },
      editTitle() {
        this.isEditTitle = true
        this.$nextTick(() => {
          this.$refs.nodeTitle.focus()
        })
      },
      saveTitle() {
        this.isEditTitle = false
      },
      save() {
        this.nodeConfig.conditionNodes[this.index] = this.form
        this.$emit('update:modelValue', this.nodeConfig)
        this.drawer = false
      },
      addTerm() {
        const len = this.nodeConfig.conditionNodes.length + 1
        this.nodeConfig.conditionNodes.push({
          nodeName: `条件${len}`,
          type: 3,
          priorityLevel: len,
          conditionMode: 1,
          conditionList: [],
        })
      },
      delTerm(index) {
        this.nodeConfig.conditionNodes.splice(index, 1)
        if (this.nodeConfig.conditionNodes.length == 1) {
          if (this.nodeConfig.childNode) {
            if (this.nodeConfig.conditionNodes[0].childNode) {
              this.reData(
                this.nodeConfig.conditionNodes[0].childNode,
                this.nodeConfig.childNode
              )
            } else {
              this.nodeConfig.conditionNodes[0].childNode =
                this.nodeConfig.childNode
            }
          }
          this.$emit(
            'update:modelValue',
            this.nodeConfig.conditionNodes[0].childNode
          )
        }
      },
      reData(data, addData) {
        if (!data.childNode) {
          data.childNode = addData
        } else {
          this.reData(data.childNode, addData)
        }
      },
      arrTransfer(index, type = 1) {
        this.nodeConfig.conditionNodes[index] =
          this.nodeConfig.conditionNodes.splice(
            index + type,
            1,
            this.nodeConfig.conditionNodes[index]
          )[0]
        this.nodeConfig.conditionNodes.map((item, index) => {
          item.priorityLevel = index + 1
        })
        this.$emit('update:modelValue', this.nodeConfig)
      },
      addConditionList() {
        this.form.conditionList.push({
          label: '',
          field: '',
          operator: '=',
          value: '',
        })
      },
      deleteConditionList(index) {
        this.form.conditionList.splice(index, 1)
      },
      toText(nodeConfig, index) {
        const { conditionList } = nodeConfig.conditionNodes[index]
        if (conditionList && conditionList.length == 1) {
          const text = conditionList
            .map((item) => `${item.label}${item.operator}${item.value}`)
            .join(' 和 ')
          return text
        } else if (conditionList && conditionList.length > 1) {
          const conditionModeText =
            nodeConfig.conditionNodes[index].conditionMode == 1
              ? '且行'
              : '或行'
          return `${conditionList.length}个条件，${conditionModeText}`
        } else {
          if (index == nodeConfig.conditionNodes.length - 1) {
            return '其他条件进入此流程'
          } else {
            return false
          }
        }
      },
    },
  }
</script>

<style></style>
