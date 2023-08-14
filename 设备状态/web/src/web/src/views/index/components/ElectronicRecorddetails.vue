<template>
  <div class="con" style="">
    <div class="con1">
      <div class="print-button" style="text-align: right">
        <!-- <el-button
          v-print="printSetting"
          size="mini"
          type="primary"
          @click="printObj"
        >
          打印
        </el-button> -->
        <el-button size="mini" type="primary" @click="exportExcel">
          导出
        </el-button>
      </div>
      <!-- v-print指令可以直接绑定到对应的打印区域-->

      <div
        id="printMe"
        ref="excelContent"
        style="text-align: center; margin: 0 auto"
      >
        <div class="card-header" style="text-align: center">
          <h2>电子履历单</h2>
        </div>
        <el-table
          border
          :data="tableData"
          style="width: 1200px; margin: 0 auto; border: 1px solid #ccc"
        >
          <el-table-column label="姓名" prop="name" width="150" />
          <el-table-column label="日期" prop="address" width="150" />
          <el-table-column label="姓名" prop="name" width="150" />
          <el-table-column label="姓名" prop="name" width="150" />
          <el-table-column label="日期" prop="address" width="150" />
          <el-table-column label="姓名" prop="name" width="150" />
          <el-table-column label="地址" prop="address" />
        </el-table>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios'

  import { ToExcel, getAllList } from '@/api/orders/order'
  import * as XLSX from 'xlsx'
  import { saveAs } from 'file-saver'
  export default defineComponent({
    setup() {
      // 导出事件
      const state = reactive({
        tableData: [
          { name: '张三', age: 18, address: '北京市' },
          { name: '李四', age: 20, address: '上海市' },
          { name: '王五', age: 22, address: '广州市' },
        ],
        list: [],
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          oAxleModel: '',
        },
        total: 0,
        html: '',
        css: '',
      })

      const excelContent = ref(null)

      // const exportExcel = () => {
      //   const wb = XLSX.utils.table_to_book(excelContent.value)

      //   XLSX.writeFile(wb, 'excel_file.xlsx')
      // }
      const exportExcel = async () => {
        const {
          data: { list, total },
        } = await getAllList(state.queryForm)
        console.log(list)
        state.list = list
        state.total = total
        const data = await ToExcel(list)
        console.log(new Blob([data]))
        const xlsx = 'application/vnd.ms-excel'
        const blob = new Blob([data], { type: xlsx }) //转换数据类型
        const a = document.createElement('a') // 转换完成，创建一个a标签用于下载
        a.download = `管道列表${new Date().getTime()}.xlsx`
        a.href = window.URL.createObjectURL(blob)
        a.click()
        a.remove()
      }
      const route = useRoute()
      console.log(route.query.setid)
      const vPrint = print
      return {
        ...toRefs(state),
        exportExcel,
        excelContent,
      }
    },
  })
</script>
<style>
  @media print {
    html,
    body,
    .con {
      width: 1000px;
    }
  }
</style>
