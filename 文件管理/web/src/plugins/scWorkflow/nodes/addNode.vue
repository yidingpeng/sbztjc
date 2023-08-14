<template>
  <div class="add-node-btn-box">
    <div class="add-node-btn">
      <el-popover
        v-if="edit"
        :hide-after="0"
        placement="right-start"
        :show-after="0"
        trigger="click"
        :width="350"
      >
        <template #reference>
          <el-button circle :icon="Plus" type="primary" />
        </template>
        <div class="add-node-popover-body">
          <ul>
            <li>
              <!-- <el-button
                :icon="UserFilled"
                style="color: #ff943e"
                typeof="primary"
                @click="addType(1)"
              />
              <el-icon style="color: #ff943e" @click="addType(1)">
                <user-filled />
              </el-icon> -->
              <vab-icon
                icon="user-fill"
                style="color: #ff943e"
                @click="addType(1)"
              />
              <p>审批节点</p>
            </li>
            <li>
              <vab-icon
                icon="send-plane-fill"
                style="color: #3296fa"
                @click="addType(2)"
              />
              <p>抄送节点</p>
            </li>
            <li>
              <vab-icon
                icon="share-fill"
                style="color: #15bc83"
                @click="addType(4)"
              />
              <p>条件分支</p>
            </li>
          </ul>
        </div>
      </el-popover>
    </div>
  </div>
</template>

<script>
  import { Plus } from '@element-plus/icons-vue'

  export default {
    inject: ['edit'],
    props: {
      modelValue: { type: Object, default: () => {} },
    },
    emits: ['update:modelValue'],
    data() {
      return { Plus }
    },
    mounted() {},
    methods: {
      addType(type) {
        let node = {}
        if (type == 1) {
          node = {
            nodeName: '审核人',
            type: 1, //节点类型
            setType: 1, //审核人类型
            nodeUserList: [], //审核人成员
            nodeRoleList: [], //审核角色
            examineLevel: 1, //指定主管层级
            directorLevel: 1, //自定义连续主管审批层级
            selectMode: 1, //发起人自选类型
            termAuto: false, //审批期限超时自动审批
            term: 0, //审批期限
            termMode: 1, //审批期限超时后执行类型
            examineMode: 1, //多人审批时审批方式
            directorMode: 0, //连续主管审批方式
            childNode: this.modelValue,
          }
        } else if (type == 2) {
          node = {
            nodeName: '抄送人',
            type: 2,
            userSelectFlag: true,
            nodeUserList: [],
            childNode: this.modelValue,
          }
        } else if (type == 4) {
          node = {
            nodeName: '条件路由',
            type: 4,
            conditionNodes: [
              {
                nodeName: '条件1',
                type: 3,
                priorityLevel: 1,
                conditionMode: 1,
                conditionList: [],
              },
              {
                nodeName: '条件2',
                type: 3,
                priorityLevel: 2,
                conditionMode: 1,
                conditionList: [],
              },
            ],
            childNode: this.modelValue,
          }
        }
        this.$emit('update:modelValue', node)
      },
    },
  }
</script>

<style></style>
