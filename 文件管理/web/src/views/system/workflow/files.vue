<template>
  <div class="workflow-file-container">
    <h3>我的附件</h3>
    <el-row :gutter="20">
      <el-col v-for="file in files" :key="file.id" :md="4" :xs="24">
        <vab-card shadow="hover">
          <div class="item-detail" @click="handleDownload(file)">
            <el-image class="image" :src="getImage(file)" />
            <div style="padding: 10px">
              <div class="card-title">
                <el-link @click="handleDownload(file)">
                  {{ file.filename }}
                </el-link>
              </div>
              <div class="card-description">{{ getSize(file.fileSize) }}</div>
              <div class="card-datetime">{{ file.uploadTime }}</div>
            </div>
          </div>
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import { getFiles } from '@/api/workflow/userFlow'
  import { doDownload } from '@/api/system/upload'
  import { uploadURL } from '@/config/net.config'

  export default {
    name: 'WorkflowFiles',
    components: {},
    setup() {
      const state = reactive({ files: [] })

      const fetchData = async function () {
        const { data } = await getFiles()
        state.files = data
      }

      onMounted(function () {
        fetchData()
      })

      const getSize = (size) => {
        const mb = Math.round((size / 1024 / 1024) * 100) / 100
        const kb = Math.round((size / 1024) * 100) / 100
        const bb = size % 1024
        if (mb > 1) return `${mb}MB`
        else if (kb > 1) return `${kb}KB`
        else return `${bb}byte`
      }

      const getImage = (file) => {
        if (file.contentType.indexOf('image') != -1) {
          return `${uploadURL}/image/${file.id}`
        }
        return require('@/assets/file.png')
      }

      const handleDownload = (file) => {
        doDownload(file.id, file.filename)
      }

      return { ...toRefs(state), getImage, getSize, handleDownload }
    },
  }
</script>

<style type="scss" scoped>
  .item-detail {
    position: relative;
    cursor: pointer;
  }
  .image {
    display: block;
    max-width: 100%;
    max-height: 120px;
  }
</style>
