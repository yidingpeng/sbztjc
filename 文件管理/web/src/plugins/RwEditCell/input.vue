<template>
  <span v-if="!edit">
    {{ text }}
  </span>
  <el-input
    v-else
    ref="editRef"
    v-model="editText"
    autofocus="true"
    placeholder="输入Enter确定，按ESC取消编辑。"
    @blur="handleBlur"
    @keyup="handlerInput"
  />
</template>

<script>
  export default defineComponent({
    name: 'RwEditInput',
    props: {
      text: { type: String, default: '' },
      edit: { type: Boolean, default: false },
    },
    emits: ['update:text', 'update:edit', 'change'],

    setup(props, { emit }) {
      const editRef = ref(null)
      const state = reactive({
        editText: props.text,
      })

      watch(
        () => props.edit,
        (newValue) => {
          if (newValue) {
            state.editText = props.text
            nextTick(() => {
              editRef.value.focus()
            })
          }
        }
      )

      const editMode = computed({
        get: () => props.edit,
        set: (value) => emit('update:edit', value),
      })

      const endEdit = () => {
        editMode.value = false
        console.log('end edit')
        if (props.text != state.editText) emit('change')
      }

      const handlerInput = (e) => {
        if (e.key == 'Enter') handleBlur()
        else if (e.key == 'Escape') editMode.value = false
      }

      const handleBlur = () => {
        if (editMode.value) {
          emit('update:text', state.editText)
          endEdit()
        }
      }

      return {
        ...toRefs(state),
        endEdit,
        handlerInput,
        handleBlur,
        editRef,
        editMode,
      }
    },
  })
</script>
