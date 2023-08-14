<template>
  <el-dialog v-model="visible" title="导入计划">
    <import-from-template
      :plan-id="planId"
      @fetch-data="
        () => {
          visible = false
          $emit('fetchData')
        }
      "
    />
    <template #footer>
      <span>
        <el-button @click="visible = false">取消</el-button>
        <el-button type="primary">确认</el-button>
      </span>
    </template>
  </el-dialog>
</template>

<script>
  import ImportFromTemplate from './fromTemplate.vue'

  export default {
    name: 'ImportPlan',
    components: { ImportFromTemplate },
    props: {
      planId: { type: Number, default: 0 },
    },
    emits: ['fetchData'],
    setup() {
      const state = reactive({ visible: false })

      const showEdit = () => {
        state.visible = true
      }

      return { ...toRefs(state), showEdit }
    },
  }
</script>

<style scoped></style>
