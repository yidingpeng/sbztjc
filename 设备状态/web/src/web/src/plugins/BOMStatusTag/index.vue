<!--
            颜色1：cadetblue
            颜色2：coral
            颜色3：goldenrod
            颜色4：teal
            颜色5：peru
          -->
<template>
  <el-tag
    v-if="status == 'Auditing'"
    effect="light"
    style="color: white; background-color: cadetblue"
  >
    {{ text }}
  </el-tag>

  <el-tag
    v-else-if="status == 'Purchasing'"
    effect="light"
    style="color: white; background-color: coral"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Approving'"
    effect="light"
    style="color: white; background-color: goldenrod"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Completed'"
    effect="light"
    style="color: white; background-color: green"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Destroied'"
    effect="light"
    style="color: white; background-color: gray"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Rejected'"
    effect="light"
    style="color: white; background-color: indianred"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Designing'"
    effect="light"
    style="color: white; background-color: lightgreen"
  >
    {{ text }}
  </el-tag>
  <el-tag
    v-else-if="status == 'Creating'"
    effect="light"
    style="color: white; background-color: lightgrey"
  >
    {{ text }}
  </el-tag>
  <el-tag v-else effect="light" style="color: white; background-color: teal">
    {{ text }}
  </el-tag>
</template>

<script>
  export default defineComponent({
    name: 'StatusTag',
    props: {
      status: { type: String, default: '' },
      text: { type: String, default: '' },
    },
  })
</script>
