<template>
  <view class="content u-padding-0">
    <iframe
      frameborder="0"
      scrolling="no"
      :src="
        'http://localhost:15000//public/pdfjs-2.14.305-dist/web/viewer.html?file=' +
        pdfSrc +
        '&.pdf'
      "
      style="width: 100%; height: 100%; min-height: 450px"
      width="100%"
    ></iframe>
  </view>
</template>

<script>
  import { getUrl } from '@/api/system/uploadFile'
  import { GetFileByPath } from '@/api/system/uploadFile'
  export default {
    components: {},
    props: {
      src: {
        type: String,
        default: '',
        required: true,
      },
    },
    data() {
      return {
        pdfSrc: '',
        url: '',
      }
    },
    mounted: function () {
      this.loadPDF()
    },
    methods: {
      async loadPDF() {
        this.url = await getUrl()
        let blobData = ''
        blobData = await GetFileByPath(
          `${this.url}/File/GetFileByPath`,
          this.src
        )

        const binaryData = []
        binaryData.push(blobData)
        const fileURL = window.URL.createObjectURL(
          new Blob(binaryData, { type: 'application/pdf' })
        )
        this.pdfSrc = encodeURIComponent(fileURL)
      },
    },
  }
</script>

<!-- <style lang="scss" scoped>
  .order {
    /deep/.u-table {
      .u-tr,
      .u-td {
        height: 45px;
        line-height: 45px;
      }
    }
  }
</style> -->
