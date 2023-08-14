<template>
  <span v-if="!edit">
    {{ getDate(text) }}
  </span>
  <el-date-picker
    v-else
    ref="editRef"
    v-model="editText"
    autofocus="true"
    :disabled-date="disabledDate"
    size="small"
    style="width: 115px"
    @blur="handleBlur"
    @change="handleBlur"
  />
</template>

<script>
  import moment from 'moment'

  export default defineComponent({
    name: 'RwEditDate',
    props: {
      text: { type: String, default: '' },
      edit: { type: Boolean, default: false },
      disabledFrom: { type: String, default: '' },
      disableTo: { type: String, default: '' },
      dateFormat: { type: String, default: 'yyyy-MM-DD' },
    },
    emits: ['update:text', 'update:edit', 'change'],
    data() {
      return {
        editText: this.text,
      }
    },
    watch: {
      edit(newValue) {
        if (newValue) {
          this.editText = this.text
          nextTick(() => {
            this.$refs.editRef.focus()
          })
        }
      },
    },
    methods: {
      getDate(date) {
        if (date == null || date == '' || date == '-') return '-'
        return moment(date).format(this.dateFormat)
      },
      // startEdit() {
      //   if (!this.canEdit) return
      //   this.editText = this.text
      // },
      endEdit() {
        this.$emit('update:edit', false)
      },
      handleBlur() {
        const orgDate = new Date(this.text)
        const editDate = new Date(this.editText)

        console.log('blur', orgDate, editDate)

        if (orgDate.getTime() == editDate.getTime()) {
          this.endEdit()
        } else {
          const date = moment(editDate).format(this.format)
          this.$emit('update:text', date)
          this.endEdit()
          this.$emit('change', date)
        }
      },
      disabledDate(date) {
        if (this.disabledFrom != '' && new Date(this.disabledFrom) > date) {
          return true
        } else if (this.disableTo != '' && new Date(this.disableTo) <= date) {
          return true
        }
        return false
      },
    },
  })
</script>
