<template>
  <span v-if="!canEdit2" @click="startEdit">{{ text }}</span>
  <el-input v-else v-model="editText" autofocus="true" @blur="handleBlur()" />
</template>

<script>
  export default defineComponent({
    name: 'RwEditRole',
    props: {
      text: { type: String, default: '' },
      canEdit: { type: Boolean, default: true },
    },
    emits: ['update:text', 'change'],
    // setup(props) {
    //   var state = reactive({ editText, canEdit2 })
    //   state.canEdit2 = toRefs(props).canEdit
    //   console.log('canedit2:', state.canEdit2)

    //   const startEdit = () => {
    //     console.log('start')
    //     state.canEdit2 = false
    //     console.log(state.canEdit2)
    //   }
    //   return { ...toRefs(state), startEdit }
    // },
    data() {
      return { editText: this.text, canEdit2: this.canEdit }
    },
    methods: {
      startEdit() {
        this.canEdit2 = true
        this.editText = this.text
      },
      endEdit() {
        this.canEdit2 = false
      },
      handleBlur() {
        console.log(this.text)
        console.log(this.editText)
        //this.text = this.editText
        this.$emit('update:text', this.editText)
        this.$emit('change')
        this.endEdit()
      },
    },
  })
</script>
