<template>
  <div class="dict-management-container">
    <el-row :gutter="20">
      <!-- 左侧 字典父级 -->
      <el-col :lg="9" :md="9" :sm="9" :xl="9" :xs="9">
        <vab-card
          v-loading="listLoading"
          shadow="hover"
          style="width: 100%; height: 650px; overflow: auto"
        >
          <el-tree
            ref="treeRef"
            :data="Filedata"
            default-expand-all
            highlight-current
            node-key="id"
            :props="{ children: 'children', label: 'label' }"
            style="height: 680px; overflow: auto"
            @node-click="handleNodeClick"
          >
            <!-- @node-click="handleNodeClick" -->
            <!-- :default-expand-all="IsExpandAll" -->
            <template #default="scope">
              <div class="custom-node">
                <el-link :icon="FolderOpened" :underline="false">
                  <span>
                    &nbsp;
                    {{ scope.node.label }}
                  </span>
                </el-link>
              </div>
            </template>
          </el-tree>
        </vab-card>
      </el-col>

      <!-- 右侧 子级 -->
      <el-col :lg="15" :md="15" :sm="15" :xl="15" :xs="15">
        <vab-card
          shadow="hover"
          style="width: 100%; height: 650px; overflow: auto"
        >
          <div>
            <el-link
              v-for="item in topfilePathArr"
              :key="item.id"
              :underline="false"
              @click="barClick(item.fullPaht)"
            >
              <label class="bartxt">{{ item.name }}</label>
              &nbsp;>>
            </el-link>
          </div>

          <div
            v-loading="folderDetailLoading"
            style="width: 100%; margin-top: 20px"
          >
            <div
              v-for="item in FolderDetailList"
              :key="item.label"
              class="folderdiv"
            >
              <div style="width: 100%">
                <el-link :underline="false" @click="folderClick(item.fullPath)">
                  <img
                    alt="文件夹名称"
                    :src="folderIconUrl"
                    style="width: 75px; height: 75px; object-fit: contain"
                  />
                </el-link>
              </div>
              <span class="foldertxt">{{ item.label }}</span>
            </div>
            <div
              v-for="item in FileDetailList"
              :key="item.fileName"
              class="folderdiv"
            >
              <div style="width: 100%">
                <el-link :underline="false">
                  <img
                    alt="文件名称"
                    :src="fileIconUrl"
                    style="width: 75px; height: 75px; object-fit: contain"
                  />
                </el-link>
              </div>
              <span class="foldertxt">{{ item.fileName }}</span>
            </div>
            <el-empty
              v-show="
                FileDetailList.length == 0 && FolderDetailList.length == 0
              "
              class="vab-data-empty"
              description="暂无数据"
            />
          </div>

          <!-- @click="FileViewClick(item.fullPath)" -->
          <!-- <div style="width: 100%">
            
          </div> -->
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import {
    GetAllFolders,
    GetFileInfo,
    GetGLFolders,
  } from '@/api/documentmanagerProdocumentationall'
  import { Delete, Plus, Search, FolderOpened } from '@element-plus/icons-vue'

  export default defineComponent({
    name: 'DocumentmanagerProdocumentation',
    setup() {
      // const $baseConfirm = inject('$baseConfirm')
      // const $baseMessage = inject('$baseMessage')

      const state = reactive({
        url: '',
        Filedata: [],
        FolderDetailList: [],
        FileDetailList: [],
        listLoading: false,
        folderDetailLoading: false,
        folderIconUrl: '',
        fileIconUrl: '',
        topfilePathArr: [],
      })

      //获取需要显示的文件夹名称
      const GetFiledata = async () => {
        const listdata = await GetGLFolders()
        listdata.data.forEach((item) => {
          state.Filedata.push({
            label: item.dictionaryText,
            path: item.description,
            children: [],
          })
        })
      }

      //树形控件点击节点时事件
      const handleNodeClick = async (val) => {
        state.folderDetailLoading = true
        state.FolderDetailList = []
        state.FileDetailList = []
        if (val.children.length == 0) {
          //获取文件夹路径，转换为数组
          state.topfilePathArr = []
          changeBarPath(val.fullPath)

          const result = await GetAllFolders({ path: val.fullPath })
          if (result) {
            state.FolderDetailList = result.fileFolders
          }
        }
        state.folderDetailLoading = false
      }
      //文件夹点击事件
      const folderClick = async (val) => {
        state.FileDetailList = []
        state.FolderDetailList = []
        changeBarPath(val)
        const result = await GetAllFolders({ path: val })
        if (result) {
          state.FolderDetailList = result.fileFolders
        }
        fetchFileDetailListList(val)
      }

      //地址点击事件
      const barClick = async (val) => {
        state.folderDetailLoading = true
        for (let i = 0; i < state.topfilePathArr.length; i++) {
          if (state.topfilePathArr[i].fullPaht == val) {
            state.topfilePathArr.splice(i + 1, state.topfilePathArr.length - 1)
            break
          }
        }
        const result = await GetAllFolders({ path: val })
        if (result) {
          state.FolderDetailList = result.fileFolders
        }
        fetchFileDetailListList(val)
        state.folderDetailLoading = false
      }

      //改变导航文件地址路径
      const changeBarPath = (val) => {
        //获取文件夹路径，转换为数组
        const filePath = val.split('\\')
        state.topfilePathArr.push({
          name: filePath[filePath.length - 1],
          fullPaht: val,
        })
      }

      //给FileDetailList赋值
      const fetchFileDetailListList = async (val) => {
        const result = await GetFileInfo({
          FolderName: val,
          IsPdf: false,
        })
        state.FileDetailList = []
        for (let i = 0; i < result.filesName.length; i++) {
          state.FileDetailList.push({
            fileName: result.filesName[i],
            fileFullName: result.filesFullName[i],
            fileSize: `${parseInt(result.filesSize[i] / 1024)}KB`,
          })
        }
      }

      const fetchData = async () => {
        state.listLoading = true

        const listdata = await GetGLFolders()
        listdata.data.forEach((item) => {
          state.Filedata.push({
            label: item.dictionaryText,
            path: item.description,
            children: [],
          })
        })

        state.folderIconUrl = 'img/黄色文件夹.png'
        state.fileIconUrl = 'img/文件.png'
        state.Filedata.forEach(async (item) => {
          const childList = await GetAllFolders({ path: item.path })
          childList.fileFolders.forEach((item) => {
            item.children = []
          })
          item.children = childList.fileFolders
        })
        state.listLoading = false
      }

      onMounted(() => {
        fetchData()
      })

      return {
        ...toRefs(state),
        fetchData,
        handleNodeClick,
        folderClick,
        barClick,
        fetchFileDetailListList,
        GetFiledata,
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
  .folderdiv {
    float: left;
    width: 20%;
    height: 130px;
    text-align: center;
  }
  .foldertxt {
    display: -webkit-box;
    width: 90%;
    height: 50px;
    overflow: hidden;
    word-break: break-all;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
  }
  .foldertxt:hover {
    overflow: visible;
    text-overflow: inherit;
    white-space: pre-line;
  }
  .bartxt {
    max-width: 205px;
    overflow: hidden;
    line-height: 20px;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
  .bartxt:hover {
    overflow: visible;
    text-overflow: inherit;
    white-space: pre-line;
  }
</style>
