<template>
  <el-dialog
    v-model="dialogFormVisible"
    :close-on-click-modal="false"
    title="项目计划"
    width="70%"
    @close="close"
  >
    <div class="excel-view-container">
      <el-tabs v-model="activeName" class="demo-tabs">
        <el-tab-pane
          v-for="sheet in excelTabsList"
          :key="sheet.excelViewId"
          :label="sheet.excelSheetName"
          :name="sheet.excelViewId"
        >
          <div :id="sheet.excelViewId" v-html="sheet.excelViewHtml"></div>
        </el-tab-pane>
      </el-tabs>
    </div>
  </el-dialog>
</template>

<script>
  import { GetFile } from '@/api/project'
  import { defineComponent, reactive, toRefs } from 'vue'
  import { nextTick } from 'vue'
  import * as XLSX from 'xlsx/xlsx.mjs'

  export default defineComponent({
    name: 'ShowFile',
    components: {},
    emits: ['fetch-data'],
    setup() {
      const state = reactive({
        pid: 0,
        fileUrl: '',
        dialogFormVisible: false,
        excelView: null,
        excelTabsList: [],
        activeName: 'excelView1',
      })

      const close = () => {
        state.dialogFormVisible = false
      }

      const showEdit = (row) => {
        handleShowProWorkPlan(row)
        state.dialogFormVisible = true
      }

      //查看最新项目工作计划
      const handleShowProWorkPlan = async (row) => {
        let res = ''
        res = await GetFile({ Pid: row.id })
        state.excelTabsList = []
        state.activeName = 'excelView1'

        //解析数据
        const workbook = XLSX.read(new Uint8Array(res), { type: 'array' })
        //获取所有工作表
        const AllWorkSheet = workbook.Sheets
        //渲染所有工作表
        for (let i = 0; i < workbook.Workbook.Sheets.length; i++) {
          state.excelTabsList.push({
            excelViewId: `excelView${i + 1}`,
            excelViewHtml: XLSX.utils.sheet_to_html(
              AllWorkSheet[workbook.SheetNames[i]]
            ), //渲染
            excelSheetName: workbook.SheetNames[i],
          })
        }

        //const worksheet = workbook.Sheets[workbook.SheetNames[0]] //workbook.SheetNames获取每个工作表名字，这里取出第一个工作表
        //state.excelView = XLSX.utils.sheet_to_html(worksheet) //渲染
        nextTick(() => {
          //DOM加载完毕后执行，解决HTMLConnection有内容但是length为0的问题
          state.excelTabsList.forEach((item) => {
            setStyle4ExcelHtml(item)
          })
        })
        state.dialogExcelVisible = true
      }

      // 设置Excel转成HTML后的样式
      const setStyle4ExcelHtml = (sheetData) => {
        const excelViewDOM = document.getElementById(sheetData.excelViewId)
        if (excelViewDOM) {
          const excelViewTDNodes = excelViewDOM.getElementsByTagName('td') // 获取的是HTMLConnection
          if (excelViewTDNodes) {
            const excelViewTDArr = Array.prototype.slice.call(excelViewTDNodes)
            for (const i in excelViewTDArr) {
              const id = excelViewTDArr[i].id // 默认生成的id格式为sjs-A1、sjs-A2......
              if (id) {
                const idNum = id.replace(/[^0-9]/gi, '') // 提取id中的数字，即行号
                if (idNum && (idNum === '1' || idNum === 1)) {
                  // 第一行标题行
                  excelViewTDArr[i].classList.add('class4Title')
                }
                if (idNum && (idNum === '2' || idNum === 2)) {
                  // 第二行表头行
                  excelViewTDArr[i].classList.add('class4TableTh')
                }
              }
            }
          }
        }
      }

      onMounted(() => {})
      return {
        ...toRefs(state),
        close,
        showEdit,
        handleShowProWorkPlan,
        setStyle4ExcelHtml,
      }
    },
  })
</script>

<style lang="scss" scoped>
  :v-deep(table) {
    width: 100%;
    overflow-x: auto;
    text-align: center;
    border-spacing: 0;
    border-collapse: collapse;
  }

  :v-deep(table tr td) {
    padding: 0.5rem;
    text-align: center;
    // border-right: 1px solid gray;
    // border-bottom: 1px solid gray;
    white-space: nowrap;
    border: 1px solid gray;
  }
  /**标题样式 */
  :v-deep(.class4Title) {
    padding: 10px;
    font-size: 22px;
    font-weight: bold;
  }
  /**表格表头样式 */
  :v-deep(.class4TableTh) {
    padding: 2px;
    /* font-size: 14px !important; */
    font-weight: bold;
    background-color: #ccc;
  }
</style>
<style lang="scss">
  .excel-view-container {
    width: 3000px;
    overflow: auto;
    background-color: #ffffff;
  }
  .el-dialog {
    max-height: 85%;
    overflow: auto;
  }
</style>
