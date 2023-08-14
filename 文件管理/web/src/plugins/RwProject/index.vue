<template>
  <el-cascader
    clearable
    filterable
    :options="list"
    :props="{ label: 'projectName', value: 'id', checkStrictly: true }"
  />
</template>

<script>
  import { GetProBasicsTreeList } from '@/api/project'

  export default defineComponent({
    name: 'RwProject',
    data() {
      return {
        list: [
          {
            id: 0,
            projectName: '空',
          },
        ],
      }
    },
    async created() {
      //项目信息
      const list = await GetProBasicsTreeList()
      list.data.forEach((item) => {
        item.projectName = `${item.projectCode}（${item.projectName}）`
        if (item.children != undefined) {
          item.children.forEach((item1) => {
            item1.projectName = `${item.projectCode}（${item1.projectName}）`
          })
        }
        this.list.push(item)
      })
    },
  })
</script>
