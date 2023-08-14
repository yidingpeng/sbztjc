<template>
  <el-select clearable placeholder="请选择类型">
    <el-option
      v-for="item in list"
      :key="item.key"
      :label="item.value"
      :value="item.key"
    />
  </el-select>
</template>

<script>
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'RwDictionary',
    props: {
      type: { type: String, default: '' },
      defaultValue: { type: String, default: '' },
      defaultKey: { type: String, default: '' },
    },
    setup(props) {
      const state = reactive({ list: [] })
      onMounted(function () {
        const { getByType } = useDictionaryStore()
        console.log('getByType:', props.type)
        const types = getByType(props.type)
        //克隆一个新数组，避免修改时，影响到原始数组
        const list = JSON.parse(JSON.stringify(types))
        console.log('list', types, list)
        state.list = list
        if (typeof list == Array && props.defaultValue)
          state.list.unshift({
            key: props.defaultKey,
            value: props.defaultValue,
          })
      })
      return { ...toRefs(state) }
    },
    methods: {
      getType: function (name) {
        const type = this.type
        const { getValue } = useDictionaryStore()
        console.log(name, type)
        return getValue(type, name)
      },
    },
  })
</script>
