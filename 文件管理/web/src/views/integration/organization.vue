<template>
  <div>
    <el-row :gutter="20">
      <el-col :span="12">
        <vab-card shadow="hover">
          <template #header>
            <h2>组织架构导入</h2>
          </template>
          <div class="integration-org">
            <div class="description">
              组织架构导入可将部门信息、岗位信息、人员信息、导入到系统中，如需使用此功能，请使用
              <a href="/template/组织架构导入标准模版.xlsx">导入模板</a>
              文件进行导入。
              <p>数据会以ID作为主键进行修改，当ID为空时，则表示为添加数据。</p>
              <p>
                用户所属部门，使用名称进行关联，最终数据的存储为ID，请确保部门名称唯一。
              </p>
            </div>
            <div class="tools">
              <rw-upload
                file-type="excel"
                :files="files"
                :max-size="2"
                type="import"
                @on-change-files="handleChanged"
              />
              <div class="tips">
                导入前请先导出备份，防止数据错误发生意外，如第一次使用，建议使用清空数据功能
              </div>
            </div>
            <div>
              <div>
                <el-checkbox v-model="form.truncateOrganization">
                  清空部门表
                </el-checkbox>
                <el-checkbox v-model="form.truncatePost">
                  清空职位表
                </el-checkbox>
                <el-checkbox v-model="form.truncateUser">
                  清空用户表
                </el-checkbox>
              </div>
              <div class="form-item">
                <span class="label">默认密码：（导入后统一的默认密码）</span>
                <el-input
                  v-model="form.password"
                  link
                  placeholder="输入默认密码"
                  type="primary"
                />
              </div>
              <div class="form-item">
                <el-button type="primary" @click="handleImport">
                  开始导入
                </el-button>
              </div>
            </div>
          </div>
        </vab-card>
      </el-col>
      <el-col :span="12">
        <vab-card shadow="hover">
          <template #header><h2>组织机构导出</h2></template>
          <div class="integration-org">
            <div class="description">
              <p>
                组织架构导出可将系统中的部门信息、岗位信息、用户信息导出成excel格式，您还可以将导出的数据进行修改后重新导入。
              </p>
              <p>
                为了保证数据的准确性，导出后，请勿修改ID列，或将某个用户与其他用户位置调换。这样的后果会导致数据发生错乱。
              </p>
              <P>如需与其他系统数据集成，请按照本系统中的规则进行导入。</P>
            </div>

            <div>
              <el-button type="primary" @click="handleExport">
                导出备份
              </el-button>
            </div>
          </div>
        </vab-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
  import {
    importOrganization,
    exportOrganization,
  } from '@/api/integration/index'
  import RwUpload from '@/plugins/RwUpload'

  export default defineComponent({
    components: { RwUpload },
    inject: ['$baseMessage'],
    setup() {},
    data() {
      return {
        form: {
          password: '',
          truncateOrganization: true,
          truncatePost: true,
          truncateUser: true,
          file: {},
        },
        files: [],
      }
    },
    methods: {
      async handleImport() {
        console.log('导入 files:', this.files)
        if (this.files.length == 0) {
          alert('请上传需要还原的文件。')
        } else {
          this.form.file = this.files[0]
          const { msg } = await importOrganization(this.form)
          this.$baseMessage(msg, 'success', 'vab-hey-message-success')
        }
      },
      handleExport() {
        const filename = '导出.xls'
        exportOrganization(filename)
      },
      handleChanged(list) {
        console.log('files', list)
        this.files = list
      },
    },
  })
</script>

<style lang="scss" scoped>
  .integration-org {
    margin-top: 10px;
    .description {
      margin: 5px 5px;
      color: gray;
    }
    .tips {
      margin: 5px;
      font-size: 14px;
      color: red;
    }
    .tips::before {
      content: '*';
    }
    .form-item {
      margin-top: 10px;
      .label {
        margin-bottom: 5px;
      }
    }
  }
</style>
