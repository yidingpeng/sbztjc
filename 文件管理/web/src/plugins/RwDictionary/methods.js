export function getTypes(type) {
  const { getValue } = useDictionaryStore()
  console.log(type, this.props.type)
  return getValue(this.props.type, type)
}
