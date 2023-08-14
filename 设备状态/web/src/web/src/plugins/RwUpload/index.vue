<template>
  <div class="rw-upload-component">
    <el-upload
      :action="uploadPath"
      class="readonly"
      :file-list="fileList"
      :headers="headers"
      multiple
      @change="handleChange"
      @preview="handlePreview"
      @remove="handleRemove"
    >
      <template #trigger v-if="!readonly">
        <el-button type="primary">上传文件</el-button>
      </template>
      <el-button
        v-if="!readonly"
        class="ml-3"
        type="success"
        @click="showSelectFiles"
      >
        选择历史文件
      </el-button>
      <template #tip>
        <p v-if="!readonly">
          图片格式推荐为：jpg/jpeg/png/bmp;文档格式可使用：pdf/doc/docs/xls/xlsx
        </p>
        <p v-else>附件列表</p>
      </template>
    </el-upload>
    <el-dialog v-model="previewVisible" title="预览">
      <el-image :src="previewImg" style="max-width: 1300px" />
    </el-dialog>
    <el-dialog
      v-model="selectFileVisible"
      destroy-on-close
      title="选择历史文件"
    >
      <div class="files">
        <div v-for="file in selectFiles" :key="file.id" class="file-item">
          <img class="image" :src="getImage(file)" />
          <el-checkbox
            v-model="file.selected"
            style="max-width: 130px; overflow: hidden"
          >
            {{ file.filename }}
          </el-checkbox>
        </div>
      </div>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="selectFileVisible = false">关闭</el-button>
          <el-button type="primary" @click="handleSaveFiles">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script>
  const network = require('@/config/net.config')
  import { useUserStore } from '@/store/modules/user'
  import { getList } from '@/api/system/upload'
  import { uploadURL } from '@/config/net.config'

  export default defineComponent({
    name: 'RwUpload',
    props: {
      files: { type: Array, default: () => [] },
      readonly: { type: Boolean, default: false },
      type: { type: String, default: () => '' },
    },
    emits: ['onChangeFiles'],
    setup() {
      onMounted(() => {
        console.log('setup on mounted:')
      })
    },
    data() {
      return {
        uploadPath: '/',
        headers: { authorization: '' },
        previewImg: '',
        previewVisible: false,
        selectFileVisible: false,
        network,
        //从files转换过来的，编辑完后最后再合并到files中去
        fileList: [],
        selectFiles: [],
      }
    },
    watch: {
      selectFiles() {
        console.log('selectFiles changed.')
      },
      files(newValue, oldValue) {
        //debugger
        console.log('newvalue,oldValue:', newValue, oldValue)
        this.fileToList()
      },
    },
    mounted() {
      const userStore = useUserStore()
      const { token } = storeToRefs(userStore)
      console.log('token is : ', token)
      this.headers.authorization = `Bearer ${token.value}`
      console.log(`type:${this.type}`, network.uploadURL)
      this.uploadPath = `${network.uploadURL}/${this.type}`
      console.log('files is :', this.files)
      this.fileToList()
    },
    methods: {
      fileToList() {
        this.fileList = []
        for (let i = 0; i < this.files.length; i++) {
          const file = this.files[i]
          this.fileList.push({ id: file.id, name: file.name })
        }
      },
      listToFile() {
        const list = []
        console.log('change fileList:', this.fileList)
        for (let i = 0; i < this.fileList.length; i++) {
          const file = this.fileList[i]
          const { id, name } = file.response ? file.response.data : file
          console.log('id,name', id, name)
          list.push({ id: id, name: name })
        }
        console.log('before change', list)
        console.log('$emit', this.$emit)
        this.$emit('onChangeFiles', list)
        return list
      },
      handlePreview(uploadFile) {
        console.log(uploadFile)
        const { id, name } = uploadFile.response
          ? uploadFile.response.data
          : uploadFile

        if (this.isImg(name)) {
          this.previewImg = this.getImage({ id, contentType: 'image' })
          this.previewVisible = true
        } else {
          window.open(this.getDownload(id))
        }
      },
      handleChange(uploadFile) {
        if (uploadFile.status == 'success') {
          console.log('handle change:', uploadFile)
          this.listToFile()
        }
      },
      handleRemove(uploadFile) {
        console.log('handle remove:', uploadFile)
        this.listToFile()
      },
      isImg(name) {
        const type = name.substring(name.lastIndexOf('.') + 1)
        const picType = ['jpg', 'jpeg', 'png', 'bmp']
        return picType.indexOf(type) != -1
      },
      getImage(file) {
        if (file.contentType && file.contentType.indexOf('image') != -1) {
          return `${uploadURL}/image/${file.id}`
        }
        return require('@/assets/file.png')
      },
      getDownload(id) {
        return `${network.uploadURL}/download/${id}`
      },
      async showSelectFiles() {
        this.selectFileVisible = true
        const {
          data: { list },
        } = await getList({ isMine: true, type: this.type })
        this.selectFiles = list.map((x) => {
          x.selected = false
          return x
        })
      },
      handleSaveFiles() {
        this.selectFileVisible = false
        const items = this.selectFiles.filter((x) => x.selected)
        console.log('items', items)
        for (const item in items) {
          this.fileList.push({ id: items[item].id, name: items[item].filename })
        }
        console.log('this.fileList', this.fileList)
        this.listToFile()
        console.log('this.files', this.files)
      },
    },
  })
</script>

<style type="scss" scoped>
  .ml-3 {
    margin-left: 0.75rem;
  }
  .readonly .el-upload-list__item:hover .el-icon--close {
    display: none;
  }
  .image {
    max-width: 100px;
    max-height: 100px;
  }

  .files .file-item {
    display: inline-block;
  }
  .files .file-item img {
    display: block;
  }
</style>
