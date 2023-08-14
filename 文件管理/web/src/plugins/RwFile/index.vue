<template>
  <div class="rw-file-component">
    <span v-if="!files || files.length == 0">未上传附件</span>
    <el-table v-else-if="showType == 'table'" border :data="files">
      <el-table-column label="序号" type="index" width="70" />
      <el-table-column label="文件" prop="name">
        <template #default="{ row }">
          <el-button link type="primary" @click="handlePreview(row)">
            {{ row.name }}
          </el-button>
        </template>
      </el-table-column>
      <el-table-column label="预览">
        <template #default="{ row }">
          <img
            v-if="isImg(row.name)"
            class="preview-img"
            :src="getImage(row.id)"
          />
          <span v-else>仅能预览图片</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="80">
        <template #default="{ row }">
          <el-button
            alt="点击下载"
            link
            type="primary"
            @click="handleDownload(row)"
          >
            下载
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <div v-else-if="showType == 'link'">
      <el-button
        v-for="file in files"
        :key="file.id"
        link
        type="primary"
        @click="handlePreview(file)"
      >
        {{ file.name }}
      </el-button>
    </div>
    <el-dialog v-model="previewVisible" title="预览">
      <img :src="previewImg" />
    </el-dialog>
  </div>
</template>

<script>
  import { doDownload } from '@/api/system/upload'

  const network = require('@/config/net.config')

  export default defineComponent({
    name: 'RwFile',
    props: {
      files: { type: Array, default: () => [] },
      showType: { type: String, default: () => 'table' },
    },
    data() {
      return { previewVisible: false, previewImg: '' }
    },
    methods: {
      handlePreview(row) {
        const { id, name } = row

        if (this.isImg(name)) {
          this.previewImg = this.getImage(id)
          this.previewVisible = true
        } else {
          this.handleDownload(row)
        }
      },
      handleDownload(row) {
        const { id } = row
        doDownload(id, row.name)
      },
      isImg(name) {
        const type = name.substring(name.lastIndexOf('.') + 1)
        const picType = ['jpg', 'jpeg', 'png', 'bmp']
        return picType.indexOf(type) != -1
      },
      getImage(id) {
        return `${network.uploadURL}/image/${id}`
      },
    },
  })
</script>

<style scoped>
  .preview-img {
    width: 200px;
    height: 100px;
  }
</style>
