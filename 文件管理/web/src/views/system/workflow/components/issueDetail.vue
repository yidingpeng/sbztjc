<template>
  <el-descriptions
    v-if="detail"
    border
    :column="3"
    size="default"
    title="问题反馈详情"
  >
    <!-- <template #extra>
      <el-button size="small" type="primary" @click="test">测试</el-button>
    </template> -->
    <el-descriptions-item :span="2">
      <template #label>项目号</template>
      <el-input v-if="edit" v-model="detail.projectNo" />
      <span v-else>{{ detail.projectNo }}</span>
    </el-descriptions-item>
    <el-descriptions-item :span="2">
      <template #label>项目名称</template>
      <el-input v-if="edit" v-model="detail.projectName" />
      <span v-else>{{ detail.projectName }}</span>
    </el-descriptions-item>
    <el-descriptions-item :pan="2">
      <template #label>问题类型</template>
      <rw-dictionary
        v-if="edit"
        ref="rwdict"
        v-model="detail.type"
        type="IssueType"
      />
      <span v-else>{{ getValue('IssueType', detail.type) }}</span>
    </el-descriptions-item>
    <el-descriptions-item :span="2">
      <template #label>工艺员</template>
      <rw-user v-if="edit" v-model="detail.technician" value-key="fullname" />
      <span v-else>{{ detail.technician }}</span>
    </el-descriptions-item>
    <el-descriptions-item :span="3">
      <template #label>问题描述</template>
      <el-input
        v-if="edit"
        v-model="detail.desc"
        maxlength="2000"
        rows="5"
        show-word-limit
        type="textarea"
      />
      <span v-else>
        <pre>{{ detail.desc }}</pre>
      </span>
    </el-descriptions-item>
  </el-descriptions>
</template>

<script>
  import rwUser from '@/plugins/RwUser'
  import rwDictionary from '@/plugins/RwDictionary'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  export default defineComponent({
    name: 'IssueDetail',
    components: { rwUser, rwDictionary },
    inject: ['getDetail'],
    props: {
      modelValue: { type: Object, default: () => {} },
      edit: { type: Boolean, default: false },
    },
    data() {
      //确保最少有一个对象
      const defaultModel = {
        projectNo: '',
        projectName: '',
        type: '',
        desc: '',
        technician: '',
      }

      //const detail = Object.assign(defaultModel, this.getDetail())
      let detail = ref(defaultModel)
      const fetchData = async () => {
        detail.value = await this.getDetail()
        if (detail == null) detail = defaultModel
        //使用Object.Assign貌似会破坏响应对象，还是老老实实手写吧
        for (const key in defaultModel) {
          if (!detail[key]) detail[key] = defaultModel[key]
        }
      }
      fetchData()

      const { getValue } = useDictionaryStore()
      return { detail, getValue }
    },
  })
</script>
