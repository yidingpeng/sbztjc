<template>
  <div class="workflow-user-component">
    <el-button @click="select(1, users)">选择用户</el-button>
    <div class="tags-list">
      <el-tag
        v-for="(user, index) in users"
        :key="user.id"
        closable
        @close="removeUser(index)"
      >
        {{ user.name }}
      </el-tag>
    </div>
  </div>
</template>

<script>
  export default defineComponent({
    name: 'SendCompnent',
    inject: ['select'],
    props: { modelValue: { type: Array, default: () => [] } },
    data() {
      return { users: [] }
    },
    mounted() {
      this.users = this.modelValue
    },
    methods: {
      removeUser(index) {
        this.users.splice(index, 1)
      },
    },
  })
</script>

<style type="scss" scoped>
  .tags-list {
    margin-top: 15px;
    width: 100%;
  }
  .tags-list:empty {
    margin-top: 0px;
  }
</style>
