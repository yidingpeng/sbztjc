<template>
  <el-cascader
    clearable
    :model-value="modelValue"
    :options="optParent"
    :props="propParent"
    @change="handleParent"
  />
</template>

<script>
  import { getList } from '@/api/system/module'
  export default defineComponent({
    props: { modelValue: { type: String, default: () => '' } },
    emits: ['update:modelValue'],
    data() {
      const propParent = { value: 'path', label: 'title', checkStrictly: true }
      return { propParent, optParent: [] }
    },

    async mounted() {
      const { data } = await getList()
      this.optParent = data
    },
    methods: {
      handleParent(value) {
        console.log(value)
        console.log(this.modelValue)
        const val = value.length > 0 ? value[value.length - 1] : ''
        this.$emit('update:modelValue', val)
      },
    },
  })
</script>

<style scoped></style>
