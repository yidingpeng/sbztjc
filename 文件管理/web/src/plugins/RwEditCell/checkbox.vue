<template>
  <el-switch v-model="editChecked" size="large" @change="handleBlur" />
</template>

<script>
  export default defineComponent({
    name: 'RwEditCheckbox',
    props: {
      checked: { type: Boolean, default: false },
    },
    emits: ['update:checked', 'change'],
    data() {
      return { editChecked: this.checked }
    },
    methods: {
      endEdit() {},
      handleBlur() {
        console.log('check changed:', this.checked, this.editChecked)
        if (this.checked == this.editChecked) return
        this.$emit('update:checked', this.editChecked)
        this.$emit('change')
        this.endEdit()
      },
    },
  })
</script>
