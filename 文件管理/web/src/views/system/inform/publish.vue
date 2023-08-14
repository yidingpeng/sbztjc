<template>
  <div class="inform-new-container">
    <el-form ref="formRef" label-width="80px" :model="form" :rules="rules">
      <el-form-item label="通知标题" prop="title">
        <el-input v-model.trim="form.title" />
      </el-form-item>
      <el-form-item label="类型" prop="type">
        <rw-dictionary v-model="form.type" type="NoticeType" />
        <!-- <el-select v-model="form.type" allow-create default-first-option>
          <el-option
            v-for="item in types"
            :key="item.code"
            :value="item.value"
          />
        </el-select> -->
      </el-form-item>
      <el-form-item label="通知内容" prop="content">
        <Toolbar :editor="editorRef" style="border-bottom: 1px solid #e8e8e8" />
        <Editor
          v-model="html"
          class="wang-editor-content"
          :default-config="editorConfig"
          style="height: 300px"
          @on-created="handleCreated"
        />
      </el-form-item>
      <el-form-item label="附件">
        <rw-upload
          ref="uploader"
          :files="form.files"
          type="inform"
          @on-change-files="handleUploadChange"
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
  import { doAdd, getOne, doEdit } from '@/api/system/inform'
  import router from '@/router'
  import RwDictionary from '@/plugins/RwDictionary'
  import RwUpload from '@/plugins/RwUpload'

  import '@wangeditor/editor/dist/css/style.css'
  import { Editor, Toolbar } from '@wangeditor/editor-for-vue'

  export default defineComponent({
    name: 'NewInform',
    components: { Editor, Toolbar, RwUser, RwDictionary, RwUpload },
    setup() {
      //const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')

      const editorRef = (shallowRef < IDomEditor) | (undefined > undefined)

      const state = reactive({
        model: [],
        //types: [],
        form: { title: '', content: '', files: [] },
        rules: {},
        dialogTableVisible: false,
        options: {
          theme: 'snow',
          bounds: document.body,
          debug: 'warn',
          modules: {
            toolbar: {
              container: [
                ['bold', 'italic', 'underline', 'strike'],
                [{ header: [1, 2, 3, 4, 5, 6, false] }],
                [{ size: ['small', false, 'large', 'huge'] }],
                [{ color: [] }, { background: [] }],
                ['blockquote', 'code-block'],
                [{ list: 'ordered' }, { list: 'bullet' }],
                [{ script: 'sub' }, { script: 'super' }],
                [{ indent: '-1' }, { indent: '+1' }],
                [{ align: [] }],
                [{ direction: 'rtl' }],
                [{ font: [] }],
                ['clean'],
                ['link', 'image', 'vab-upload-image'],
              ],
              handlers: {
                'vab-upload-image': () => {
                  $baseConfirm(
                    '演示环境未使用真实文件服务器，故图片上传回显不会生效，开发时请修改为正式文件服务器地址',
                    '温馨提示',
                    () => {
                      state['vabUploadRef'].handleShow()
                    },
                    () => {
                      handleAddImg()
                    },
                    '模拟打开文件上传',
                    '模拟添加一张文件服务器图片'
                  )
                },
              },
            },
          },
          placeholder: '内容...',
          readOnly: false,
        },
      })
      const handleCreated = (editor) => {
        editorRef.value = editor
      }
      onBeforeUnmount(() => {
        const editor = editorRef.value
        if (editor == null) return
        editor.destroy()
      })
      onMounted(async () => {
        const { id } = router.currentRoute.value.params
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

      const handleUploadChange = function (list) {
        console.log('parent changed:', list)
        state.form.files = list
      }

      return {
        ...toRefs(state),
        save,
        preview,
        handleUploadChange,
        handleCreated,
      }
    },
  })
</script>

<style type="scss" scoped>
  .news-title {
    text-align: center;
  }
</style>
