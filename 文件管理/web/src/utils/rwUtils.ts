import router from '../router'

/**
 * @description 将一个时间字符串转换成距今多久，如1小时前，1天前等
 * @param time 一个时间字符串，参考格式为yyyy-MM-dd HH:mm:ss
 * @returns {string}
 */
export function toTimeString(time: string) {
  const d = new Date(time)
  const tick = (Date.now() - d.getTime()) / 1000

  const months = 30 * 7 * 24 * 60 * 60
  const weeks = 7 * 24 * 60 * 60
  const days = 24 * 60 * 60
  const hours = 60 * 60
  const minuts = 60

  console.log(weeks, days, hours)
  if (tick / months > 1) {
    const t2 = Math.floor(tick / months)
    return `${t2}月前`
  } else if (tick / weeks > 1) {
    const t2 = Math.floor(tick / weeks)
    return `${t2}周前`
  } else if (tick / days > 1) {
    const t2 = Math.floor(tick / days)
    return `${t2}天前`
  } else if (tick / hours > 1) {
    const t2 = Math.floor(tick / hours)
    return `${t2}时前`
  } else if (tick / minuts > 1) {
    const t2 = Math.floor(tick / minuts)
    return `${t2}分钟前`
  } else return `${Math.round(tick)}秒前`
}

export function getMessageUrl(item: any, toDetail: boolean) {
  //console.log(`跳转到详情页[${type}-${id}]`)
  if (!toDetail) {
    router.push(`/message/${item.id}`)
  } else {
    if (item.type == 'workflow') router.push(`/workflow/detail/${item.dataId}`)
    else if (item.type == 'inform') router.push(`/inform/detail/${item.dataId}`)
  }
}
