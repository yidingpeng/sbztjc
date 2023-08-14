<template>
  <el-select clearable placeholder="请选择用户">
    <el-option
      v-for="item in list"
      :key="item.id"
      :label="item[labelKey]"
      :value="item[valueKey]"
    />
  </el-select>
</template>

<script>
  import { GetUserList } from '@/api/system/user'

  export default defineComponent({
    name: 'RwUser',
    props: {
      labelKey: { type: String, default: 'realName' },
      valueKey: { type: String, default: 'id' },
    },
    data() {
      return {
        list: [],
      }
    },
    async created() {
      const data = await GetUserList()
      this.list = data.data
    },
  })
</script>
