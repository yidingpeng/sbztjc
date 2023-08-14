<template>
  <div class="dict-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="12" :md="12" :sm="12" :xl="12" :xs="12">
        <vab-query-form>
          <el-button
            :icon="FolderOpened"
            :loading="btnloading"
            type="primary"
            @click="handleUpdateFolders"
          >
            更新项目数据
          </el-button>
        </vab-query-form>
        <br />
        <vab-card
          shadow="hover"
          style="width: 100%; height: 650px; overflow: auto"
        >
          <vab-query-form>
            <vab-query-form-top-panel>
              <el-form :inline="true" :model="queryForm" @submit.prevent>
                <el-form-item>
                  <el-input
                    v-model.trim="queryForm.key"
                    clearable
                    placeholder="请输入关键字"
                    style="width: 200px"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button :icon="Search" type="primary" @click="queryData">
                    查询
                  </el-button>
                </el-form-item>
              </el-form>
            </vab-query-form-top-panel>
          </vab-query-form>
          <el-collapse ref="collapseRef" accordion @change="handleNodeClick">
            <el-collapse-item
              v-for="pro in Filedata"
              :key="pro.id"
              :name="pro.projectCode + '_' + pro.projectName"
              :title="pro.projectCode + '_' + pro.projectName"
            >
              <div>
                <el-descriptions :column="2">
                  <el-descriptions-item label="业务类型：">
                    {{ pro.businessTypeName }}
                  </el-descriptions-item>
                  <el-descriptions-item label="客户公司：">
                    {{ pro.clientCompanyName }}
                  </el-descriptions-item>
                  <el-descriptions-item label="市场片区：">
                    {{ pro.marketAreaTxt }}
                  </el-descriptions-item>
                  <el-descriptions-item
                    label="阶&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;段："
                  >
                    {{ pro.proStateName }}
                  </el-descriptions-item>
                  <el-descriptions-item label="项目类型：">
                    {{ pro.projectClassName }}
                  </el-descriptions-item>
                </el-descriptions>
              </div>
            </el-collapse-item>
          </el-collapse>
          <el-pagination
            :current-page="queryForm.pageNo"
            :layout="layout"
            :page-size="queryForm.pageSize"
            :pager-count="3"
            small
            :total="total"
            @current-change="handleCurrentChange"
            @size-change="handleSizeChange"
          />
        </vab-card>
      </el-col>

      <!-- 右侧 子级 -->
      <el-col
        :lg="12"
        :md="12"
        :sm="12"
        style="margin-top: 47px"
        :xl="12"
        :xs="12"
      >
        <vab-card
          shadow="hover"
          style="width: 100%; height: 655px; overflow: auto"
        >
          <!-- @tab-click="handleTabsClick" -->
          <el-tabs
            ref="TabsRef"
            v-model="firstTab"
            v-loading="FileLoading"
            class="demo-tabs"
            tab-position="left"
            @tab-click="handleTabsClick"
          >
            <el-tab-pane
              v-for="(folderItme, index) in folderNArr"
              :key="folderItme.id"
              :label="folderItme.dictionaryText"
              :name="index"
            >
              <div v-for="fileItem in FileDetailList" :key="fileItem.fileName">
                <div style="float: left; width: 90%; text-align: left">
                  <el-link
                    type="primary"
                    @click="pdfView(fileItem.fileFullName)"
                  >
                    {{ fileItem.fileName }}
                  </el-link>
                  <el-divider />
                </div>
                <!-- <el-button
                  size="small"
                  style="margin-right: 0px"
                  type="primary"
                  @click="remotePrint(fileItem.fileFullName)"
                >
                  <vab-icon icon="printer-line" />
                  打印
                </el-button> -->
              </div>
            </el-tab-pane>
          </el-tabs>
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import {
    getProjectList,
    GetFolderNames,
    GetDocPagedList,
    UpdateDocManage,
    GetFileInfo,
  } from '@/api/documentmanagerProdocumentation'
  import { getUrl } from '@/api/system/uploadFile'
  import { GetFileByPath } from '@/api/system/uploadFile'
  import { Delete, Plus, Search, FolderOpened } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'DocumentmanagerProdocumentation',
    setup() {
      const $baseConfirm = inject('$baseConfirm')
      const $baseMessage = inject('$baseMessage')
      const state = reactive({
        TabsRef: null,
        collapseRef: null,
        url: '',
        editRef: null,
        Filedata: [],
        FileDetailList: [],
        listLoading: true,
        layout: 'total, sizes, prev, pager, next',
        total: 0,
        selectRows: '',
        queryForm: {
          pageNo: 1,
          pageSize: 10,
          key: '',
        },
        authbook_url: '',
        Isauthbook_url: false,
        folderNArr: null,
        firstTab: '',
        btnloading: false,
        FileLoading: false,
        proPath: '',
        //printObj: { id: val, preview: true, popTitle: 'good print' },
      })

      //更新项目文件路径
      const handleUpdateFolders = async () => {
        $baseConfirm('你确定要更新项目数据吗?', null, async () => {
          state.btnloading = true
          const { msg } = await UpdateDocManage()
          $baseMessage(msg, 'success', 'vab-hey-message-success')
          state.btnloading = false
        })
      }

      //获取文件夹数据字典
      const GetFolderName = async () => {
        const FolderData = await GetFolderNames()
        state.folderNArr = FolderData.data
        state.firstTab = '0'
      }

      //给filedetailList赋值
      const fetchFileDetailList = (listData) => {
        for (let i = 0; i < listData.filesName.length; i++) {
          state.FileDetailList.push({
            fileName: listData.filesName[i],
            fileFullName: listData.filesFullName[i],
            fileSize: `${parseInt(listData.filesSize[i] / 1024)}KB`,
          })
        }
      }

      //collapse点击事件
      const handleNodeClick = async (val) => {
        state.firstTab = '0'
        state.FileDetailList = []
        state.proPath = ''
        if (val != '') {
          state.proPath = await GetDocPagedList({ key: val })
          const filePath = state.folderNArr[0].description
          if (state.proPath.data.length > 0) {
            const SCresult = await GetFileInfo({
              FolderName: state.proPath.data[0].folderAddressSC + filePath,
              IsPdf: true,
            })
            fetchFileDetailList(SCresult)
            const GLresult = await GetFileInfo({
              FolderName: state.proPath.data[0].folderAddressGL + filePath,
              IsPdf: true,
            })
            fetchFileDetailList(GLresult)
          }
        }
      }

      //tabs点击事件
      const handleTabsClick = async (val) => {
        state.FileDetailList = []
        if (state.proPath != '') {
          const filePath = state.folderNArr[val.props.name].description
          if (state.proPath.data.length > 0) {
            const SCresult = await GetFileInfo({
              FolderName: state.proPath.data[0].folderAddressSC + filePath,
              IsPdf: true,
            })
            fetchFileDetailList(SCresult)
            const GLresult = await GetFileInfo({
              FolderName: state.proPath.data[0].folderAddressGL + filePath,
              IsPdf: true,
            })
            fetchFileDetailList(GLresult)
          }
        }
      }

      //文件查看事件
      const pdfView = async (val) => {
        state.FileLoading = true
        const url = await getUrl()
        let blobData = ''
        blobData = await GetFileByPath(`${url}/File/GetFileByPath`, val)

        const binaryData = []
        binaryData.push(blobData)
        const fileURL = window.URL.createObjectURL(
          new Blob(binaryData, { type: 'application/pdf' })
        )
        window.open(
          `pdfjs-2.14.305-dist/web/viewer.html?file=${encodeURIComponent(
            fileURL
          )}&.pdf`
        )
        state.FileLoading = false
      }

      const handleSizeChange = (val) => {
        state.queryForm.pageSize = val
        fetchData()
      }
      const handleCurrentChange = (val) => {
        state.queryForm.pageNo = val
        fetchData()
      }
      const queryData = () => {
        state.queryForm.pageNo = 1
        fetchData()
      }
      const fetchData = async () => {
        state.listLoading = true
        const {
          data: { list, total },
        } = await getProjectList({
          key: state.queryForm.key,
          PageNo: state.queryForm.pageNo,
          PageSize: state.queryForm.pageSize,
        })
        state.Filedata = list
        state.total = total
        state.listLoading = false
      }

      onMounted(() => {
        fetchData()
        GetFolderName()
      })

      return {
        ...toRefs(state),
        handleSizeChange,
        handleCurrentChange,
        queryData,
        fetchData,
        handleNodeClick,
        pdfView,
        GetFolderName,
        handleUpdateFolders,
        handleTabsClick,
        Delete,
        Plus,
        Search,
        FolderOpened,
      }
    },
  })
</script>

<style lang="scss" scoped>
  body[class*='vab-theme-'] a {
    font-size: 15px;
    color: rgb(77, 77, 77);
  }
  .el-collapse-item {
    //display: inline-block;
    //max-width: 333px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
</style>
