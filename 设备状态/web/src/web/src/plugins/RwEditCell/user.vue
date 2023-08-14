<template>
  <span v-if="!edit">{{ modelValue || '未指定' }}</span>
  <rw-user
    v-else
    ref="editRef"
    v-model="selectValue"
    @blur="handleBlur"
    @change="handleChange"
  />
</template>

<script>
  import RwUser from '@/plugins/RwUserName'

  export default defineComponent({
    name: 'RwEditUser',
    components: { RwUser },
    props: {
      modelValue: { type: String, default: '' },
      edit: { type: Boolean, default: false },
    },
    emits: ['update:model-value', 'update:edit', 'change'],
    data() {
      return { selectValue: this.modelValue, data: [] }
    },
    watch: {
      edit(newValue) {
        console.log('watch', newValue)
        if (newValue) {
          nextTick(() => {
            this.$refs.editRef.focus()
          })
        }
      },
    },
    methods: {
      endEdit() {
        this.$emit('update:edit', false)
      },
      handleChange() {
        console.log('change', this.modelValue, this.selectValue)
        if (this.modelValue == this.selectValue) {
          this.endEdit()
          return
        }
        this.$emit('update:model-value', this.selectValue)
        this.$emit('change')
        this.endEdit()
      },
      handleBlur() {
        //this.endEdit()
        //nextTick(() => this.endEdit())
        //this.handleChange()
      },
    },
  })
</script>
