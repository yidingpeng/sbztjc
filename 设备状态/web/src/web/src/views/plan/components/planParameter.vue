<template>
  <el-row :getter="24">
    <el-col :offset="4" :span="16">
      <el-form :model="model">
        <el-form-item label="计划名称">
          <el-input v-model="inModel.planName" />
        </el-form-item>
        <el-form-item label="计划时间">
          <el-date-picker v-model="inModel.planDate" />
        </el-form-item>
        <el-form-item label="日历模式">
          <el-select v-model="inModel.calendarMode">
            <el-option label="标准日历" :value="0">标准日历</el-option>
            <el-option label="无休日历" :value="1">无休日历</el-option>
            <el-option label="单休日历" :value="2">单休日历</el-option>
            <el-option label="大小周（小周休）" :value="3">
              大小周（小周休）
            </el-option>
            <el-option label="大小周（大周休）" :value="4">
              大小周（大周休）
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="自动排程">
          <el-switch
            v-model="inModel.isAutoAps"
            active-text="开启"
            inactive-text="关闭"
          />
          <el-alert
            effect="light"
            title="自动排程会根据上一个任务和工期自动生成任务【开始时间】和【结束时间】。"
            type="info"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="saveHandler">保存</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<script>
  import { PlanAddOrEdit } from '@/api/plan/plan'

  export default {
    name: 'PlanParameter',
    inject: ['$baseMessage'],
    props: { model: { type: Object, default: () => {} } },
    emits: ['update:model', 'submit'],

    computed: {
      inModel: {
        get() {
          return this.model
        },
        set(model) {
          this.$emit('update:model', model)
        },
      },
    },
    methods: {
      saveHandler() {
        //this.$emit('submit', pageModel)
        PlanAddOrEdit(this.inModel).then((x) => {
          this.$baseMessage(x.msg, 'success', 'vab-hey-message-success')
          console.log(x)
        })
      },
    },
  }
</script>
