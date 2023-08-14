<template>
  <span v-if="!canEdit2" @click="startEdit">{{ text }}</span>

  <el-select
    v-else
    clearable
    filterable
    placeholder="请选择"
    value-key="valueKey"
    @change="handleBlur"
  >
    <el-option
      v-for="item in data"
      :key="item.value"
      :label="item.label"
      :value="item.value"
    />
  </el-select>
</template>

<script>
  export default defineComponent({
    name: 'RwEditRole',
    props: {
      //绑定的数据
      data: { type: Array, default: () => [] },
      //绑定的数据为数组对象类型时，关联的value值
      valueKey: { type: String, default: '' },
      //是否允许编辑
      canEdit: { type: Boolean, default: true },
      //关联选中的值
      modelValue: { type: String, default: '' },
    },
    emits: ['update:model-value', 'change'],
    data() {
      return { selectValue: '', canEdit2: false }
    },
    methods: {
      startEdit() {
        //设置不允许编辑则返回
        if (!this.canEdit) return
        this.canEdit2 = true
        this.changeData = this.text
      },
      endEdit() {
        this.canEdit2 = false
      },
      handleBlur() {
        console.log(this.data)
        console.log(this.changeData)
        //this.text = this.editText
        this.$emit('update:model-value', this.editText)
        this.$emit('change')
        this.endEdit()
      },
    },
  })
</script>
