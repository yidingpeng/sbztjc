<template>
  <el-select multiple placeholder="选择一种或多种角色">
    <el-option
      v-for="item in list"
      :key="item.id"
      :label="item.role"
      :value="item.role"
    />
  </el-select>
</template>

<script>
  import { getAllList } from '@/api/system/role'

  export default defineComponent({
    name: 'RwRole',

    data() {
      return {
        list: [],
      }
    },
    async created() {
      const list = await getAllList()
      this.list = list.data
    },
  })
</script>
