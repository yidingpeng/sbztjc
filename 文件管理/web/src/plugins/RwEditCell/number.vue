<template>
  <span v-if="!edit">{{ number }}</span>
  <el-input
    v-else
    ref="editRef"
    v-model="editNumber"
    autofocus="true"
    :max="max"
    :min="min"
    oninput="value=value.replace(/[^0-9.]/g,'')"
    width="80"
    @blur="handleBlur"
    @keydown="inputHandler"
  />
</template>

<script>
  export default defineComponent({
    name: 'RwEditNumber',
    props: {
      number: { type: Number, default: 0 },
      max: { type: Number, default: 100 },
      min: { type: Number, default: 0 },
      edit: { type: Boolean, default: false },
    },
    emits: ['update:number', 'update:edit', 'change'],
    data() {
      return { editNumber: this.number.toString(), canEdit2: false }
    },
    watch: {
      edit(newValue) {
        if (newValue) {
          this.editNumber = parseInt(this.number)
          nextTick(() => {
            this.$refs.editRef.focus()
            this.$refs.editRef.select()
          })
        }
      },
    },
    methods: {
      endEdit() {
        this.$emit('update:edit', false)
      },
      handleBlur() {
        console.log(this.number, this.editNumber)
        if (this.number == this.editNumber) {
          this.endEdit()
          return
        }
        //this.text = this.editText
        this.$emit('update:number', this.editNumber)
        this.$emit('change')
        this.endEdit()
      },
      inputHandler(e) {
        if (this.editNumber > this.max) this.editNumber = this.max
        if (this.editNumber < this.min) this.editNumber = this.min

        if (e.key == 'Enter') this.handleBlur()
        else if (e.key == 'Escape') this.endEdit()
      },
      formatterHandler(e) {
        console.log('format', e)
      },
      parserHandler(e) {
        console.log('parser', e)
      },
    },
  })
</script>
