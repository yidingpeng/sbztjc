<template>
  <el-table v-loading="loading" border :data="tasks" max-height="540" stripe>
    <el-table-column
      v-for="col in columns"
      :key="col.id"
      :fixed="col.fixed"
      :label="col.name"
      :prop="col.id"
      :width="col.width"
    >
      <template #default="{ row }">
        <span v-if="!col.type">
          {{ col.callback ? col.callback(row) : row[col.prop] }}
        </span>
        <el-tag v-else-if="col.type == 'tag'" round :type="col.tagType(row)">
          {{ col.callback(row) }}
        </el-tag>
      </template>
    </el-table-column>
    <el-table-column fixed="right" label="操作">
      <template #default="{ row }">
        <el-dropdown
          cursor="hand"
          trigger="click"
          @command="(cmd) => handleViewCommand(cmd, row)"
        >
          <span class="el-dropdown-link">
            <vab-icon icon="more-2-fill" />
          </span>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item command="feedback">
                <vab-icon icon="edit-box-line" />
                任务反馈
              </el-dropdown-item>
              <el-dropdown-item command="edit">
                <vab-icon icon="edit-2-line" />
                编辑任务
              </el-dropdown-item>
              <el-dropdown-item command="decomposition">
                <vab-icon icon="calendar-todo-line" />
                分解任务
              </el-dropdown-item>
              <el-dropdown-item command="detail">
                <vab-icon icon="file-list-line" />
                查看详情
              </el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </template>
    </el-table-column>
  </el-table>
  <task-feedback ref="feedbackRef" v-model:visible="visibility" />
</template>
<script lang="js">
  import { formatDate } from '@vueuse/shared'
  import TaskFeedback from './taskFeedback.vue'
  export default {
    name: 'TaskViewTable',
    components: { TaskFeedback },
    props: {
      tasks: { type: Array, default: () => [] },
      loading: { type: Boolean, default: false },
    },
    data() {
      return {
        columns: [
          { name: '序号', prop: 'order', width: 80, fixed: 'left' },
          { name: '任务名称', prop: 'name', width: 300, fixed: 'left' },
          { name: '责任人', prop: '', width: 100 },
          { name: '计划工期', prop: 'duration', width: 90 },
          {
            name: '计划开始',
            prop: 'planStartDate',
            width: 100,
            callback: (row) =>
              formatDate(new Date(row.planStartDate), 'YYYY-MM-DD'),
          },
          {
            name: '计划结束',
            prop: 'planEndDate',
            width: 100,
            callback: (row) =>
              formatDate(new Date(row.planEndDate), 'YYYY-MM-DD'),
          },
          {
            name: '状态',
            type: 'tag',
            callback: (row) =>
              this.getDays(new Date(new Date() - new Date(row.planEndDate))),
            tagType: (row) =>
              this.getTagType(new Date(new Date() - new Date(row.planEndDate))),
            width: 100,
          },
          {
            name: '实际开始',
            prop: 'actualStart',
            width: 100,
            callback: (row) =>
              row.actualStart
                ? formatDate(new Date(row.actualStart), 'YYYY-MM-DD')
                : '-',
          },
          {
            name: '实际结束',
            prop: 'actualFinish',
            width: 100,
            callback: (row) =>
              row.actualFinish
                ? formatDate(new Date(row.actualFinish), 'YYYY-MM-DD')
                : '-',
          },
          {
            name: '进度',
            prop: 'progress',
            callback: (row) =>
              row.progress == 100 ? '已完成' : `${row.progress}%`,
            width: 80,
          },
        ],
        visibility: false,
        editData: null,
      }
    },
    methods: {
      getDays(timespan) {
        const value = Math.floor(timespan / 1000 / 60 / 60 / 24)
        if (value > 0) return `逾期${value}天`
        else if (value < 0) return `剩余${Math.abs(value)}天`
        else return '今日完成'
      },
      getTagType(timespan) {
        const value = Math.floor(timespan / 1000 / 60 / 60 / 24)
        if (value > 0) return 'danger'
        else if (value < -7) return 'info'
        else return 'warning'
      },
      handleViewCommand(cmd, row) {
        console.log('view command:', cmd, row)
        switch (cmd) {
          case 'feedback':
            this.editData = row
            console.log('edit:', this.editData)
            //this.visibility = true
            this.$refs.feedbackRef.show(row)
            break
          default:
            break
        }
      },
    },
  }
</script>
