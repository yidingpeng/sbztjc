<template>
  <div class="inform-new-container">
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="通知标题" prop="title">
        <el-input v-model.trim="form.title" />
      </el-form-item>
      <el-form-item label="类型" prop="type">
        <el-select v-model="form.type" allow-create default-first-option>
          <el-option
            v-for="item in types"
            :key="item.code"
            :value="item.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item class="vab-quill-content" label="通知内容" prop="content">
        <Toolbar :editor="editorRef" style="border-bottom: 1px solid #e8e8e8" />
        <Editor
          v-model="form.content"
          class="wang-editor-content"
          :default-config="editorConfig"
          style="height: 300px"
          @on-created="handleCreated"
        />
      </el-form-item>
      <el-form-item label="通知用户" prop="users">
        <rw-user v-model="form.users" multiple />
      </el-form-item>
      <el-form-item label="">
        <el-button type="primary" @click="save">确 定</el-button>
        <el-button type="warning" @click="preview">预 览</el-button>
      </el-form-item>
    </el-form>
    <el-dialog v-model="dialogTableVisible" title="预览效果">
      <h1 class="news-title">{{ form.title }}</h1>
      <div class="news-content" v-html="form.content" />
    </el-dialog>
  </div>
</template>

<script>
  import RwUser from '@/plugins/RwUser'
  import { doAdd, getTypeList, getOne, doEdit } from '@/api/system/inform'
  import router from '~/src/router'

  import '@wangeditor/editor/dist/css/style.css'
  import { Editor, Toolbar } from '@wangeditor/editor-for-vue'

  export default defineComponent({
    name: 'NewInform',
    components: { Editor, Toolbar, RwUser },
    setup() {
      //const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const editorRef = (shallowRef < IDomEditor) | (undefined > undefined)
      const handleCreated = (editor) => {
        editorRef.value = editor
      }
      const state = reactive({
        model: [],
        types: [],
        form: { title: '', content: '' },
        rules: {},
        dialogTableVisible: false,
      })

      const editorConfig = ref({
        placeholder: '请输入内容...',
        MENU_CONF: {
          uploadImage: {
            server: '', // 你的服务器地址，注意：当前接口格式特殊与其他vab接口不同，请查看vip文档
            fieldName: 'vab-file-name',
            allowedFileTypes: ['image/*'],
            headers: {}, // 如需传递token请写到在这里
          },
        },
      })

      onMounted(async () => {
        const { data } = await getTypeList()
        state.types = data

        const { id } = router.currentRoute.value.query
        console.log(router.currentRoute.value)
        if (id) {
          const { data: data2 } = await getOne(id)
          state.form = data2
        }
      })

      const save = async () => {
        if (state.form.id) {
          const { msg } = await doEdit(state.form)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        } else {
          const { msg } = await doAdd(state.form)
          $baseMessage(msg, 'success', 'vab-hey-message-success')
        }
      }

      const preview = () => {
        state.dialogTableVisible = true
      }

      return { ...toRefs(state), save, preview, handleCreated, editorConfig }
    },
  })
</script>

<style>
  .news-title {
    text-align: center;
  }
</style>
