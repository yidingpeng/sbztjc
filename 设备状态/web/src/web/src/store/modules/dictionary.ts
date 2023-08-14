/**
 * @description 全局字典缓存
 */
import { getAll } from '@/api/system/dictionary'

declare interface UseDictionaryType {
  all: any
}

export const useDictionaryStore = defineStore('dictionary', {
  state: (): UseDictionaryType => ({
    all: {},
  }),
  actions: {
    async initData() {
      const { data } = await getAll()
      console.log('init dictionary:', data)
      this.all = data
    },
    getByType(type: any) {
      for (const key in this.all) {
        if (key == type) return this.all[type]
      }
      return {}
    },
    getValue(type: any, key: any, defaultValue: string) {
      defaultValue = defaultValue || key
      const types = this.getByType(type)
      for (const i in types) if (types[i].key == key) return types[i].value
      console.log(
        `cannot find the value [${key}] in [${JSON.stringify(types)}]`
      )
      return defaultValue
    },
    getKey(type: any, value: any, defaultValue: string) {
      defaultValue = defaultValue || value
      const types = this.getByType(type)
      for (const i in types) if (types[i].value == value) return types[i].key
      return defaultValue
    },
  },
})
