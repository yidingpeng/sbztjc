<template>
  <issue-detail v-if="type == 'issue'" :edit="edit" />
</template>

<script>
  import issueDetail from './issueDetail'

  export default {
    components: { issueDetail },
    props: {
      type: { type: String, default: '' },
      edit: { type: Boolean, default: false },
      // detail: {
      //   type: Object,
      //   default: () => {
      //     //必须返回默认值，否则在输出端会报错‘找不到projectNo属性’
      //     return { projectNo: '', projectName: '', desc: '', technician: '' }
      //   },
      // },
    },
    setup() {},
  }
</script>
