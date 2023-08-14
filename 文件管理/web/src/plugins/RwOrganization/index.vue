<template>
  <el-cascader
    clearable
    :empty="0"
    :options="organization"
    placeholder="选择用户所在的部门"
    :props="orgProps"
  />
</template>

<script>
  import { getList } from '@/api/system/organization'

  export default defineComponent({
    name: 'RwOrganization',
    data() {
      return {
        organization: [],
        orgProps: {
          value: 'id',
          label: 'name',
          checkStrictly: true,
          emitPath: false,
        },
      }
    },
    async created() {
      const { data } = await getList()
      this.organization = data
    },
  })
</script>
