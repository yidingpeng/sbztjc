const List = [
  {
    Id: 1,
    ProjectName: '项目名称1',
    ProjectCode: '项目编号1',
    ClientCompany: '客户公司名称1',
    ProjectClass: '项目分类1',
    BusinessType: '业务类型1',
    UrgentGrade: '重要紧急等级1',
    ProjectReceiveDate: '2022-03-07',
    ContractDeliveryDate: '2022-03-07',
    ExpectedUseDate: '2022-03-07',
    IsContract: '有',
    IsTechnicalAgreement: '有',
    ProManagementStyle: '项目管理方式1',
    ProjectBackground: '背景',
    ProjectSummary: '概括',
    ProState: '状态',
    Remark: '备注1',
    BiddingNo: '招标编号',
    detailDate: [],
  },
  {
    Id: 2,
    ProjectName: '项目名称1',
    ProjectCode: '项目编号1',
    ClientCompany: '客户公司名称1',
    ProjectClass: '项目分类1',
    BusinessType: '业务类型1',
    UrgentGrade: '重要紧急等级1',
    ProjectReceiveDate: '2022-03-07',
    ContractDeliveryDate: '2022-03-07',
    ExpectedUseDate: '2022-03-07',
    IsContract: '有',
    IsTechnicalAgreement: '有',
    ProManagementStyle: '项目管理方式1',
    ProjectBackground: '背景',
    ProjectSummary: '概括',
    Remark: '备注1',
    BiddingNo: '招标编号',
    detailDate: [
      {
        Id: 1,
        SysEquipmentCode: '系统设备编号',
        ProjectId: '项目ID',
        ProductSeries: '产品系列',
        EquipmentCode: '设备编号',
        EquipmentName: '设备名称',
        Quantity: '数量',
        OwnerUnit: '业主单位',
        DrawingCode: '图纸代号',
        EquipmentPlanCode: '计划单据号',
        Remark: '备注',
      },
      {
        Id: 2,
        SysEquipmentCode: '系统设备编号',
        ProjectId: '项目ID',
        ProductSeries: '产品系列',
        EquipmentCode: '设备编号',
        EquipmentName: '设备名称',
        Quantity: '数量',
        OwnerUnit: '业主单位',
        DrawingCode: '图纸代号',
        EquipmentPlanCode: '计划单据号',
        Remark: '备注',
      },
    ],
  },
  {
    Id: 3,
    ProjectName: '项目名称1',
    ProjectCode: '项目编号1',
    ClientCompany: '客户公司名称1',
    ProjectClass: '项目分类1',
    BusinessType: '业务类型1',
    UrgentGrade: '重要紧急等级1',
    ProjectReceiveDate: '2022-03-07',
    ContractDeliveryDate: '2022-03-07',
    ExpectedUseDate: '2022-03-07',
    IsContract: '有',
    IsTechnicalAgreement: '有',
    ProManagementStyle: '项目管理方式1',
    ProjectBackground: '背景',
    ProjectSummary: '概括',
    Remark: '备注1',
    BiddingNo: '招标编号',
    detailDate: [
      {
        Id: 1,
        SysEquipmentCode: '系统设备编号',
        ProjectId: '项目ID',
        ProductSeries: '产品系列',
        EquipmentCode: '设备编号',
        EquipmentName: '设备名称',
        Quantity: '数量',
        OwnerUnit: '业主单位',
        DrawingCode: '图纸代号',
        EquipmentPlanCode: '计划单据号',
        Remark: '备注',
      },
      {
        Id: 2,
        SysEquipmentCode: '系统设备编号',
        ProjectId: '项目ID',
        ProductSeries: '产品系列',
        EquipmentCode: '设备编号',
        EquipmentName: '设备名称',
        Quantity: '数量',
        OwnerUnit: '业主单位',
        DrawingCode: '图纸代号',
        EquipmentPlanCode: '计划单据号',
        Remark: '备注',
      },
      {
        Id: 2,
        SysEquipmentCode: '系统设备编号',
        ProjectId: '项目ID',
        ProductSeries: '产品系列',
        EquipmentCode: '设备编号',
        EquipmentName: '设备名称',
        Quantity: '数量',
        OwnerUnit: '业主单位',
        DrawingCode: '图纸代号',
        EquipmentPlanCode: '计划单据号',
        Remark: '备注',
      },
    ],
  },
]
const ProjectClassList = [
  { value: '1', label: '潜在项目' },
  { value: '2', label: '在产项目' },
  { value: '3', label: '维保项目' },
]
const BusinesstypeList = [
  { value: '1', label: '城轨市场' },
  { value: '2', label: '实验设备' },
  { value: '3', label: '车载产品' },
  { value: '4', label: '产线及信息化' },
  { value: '5', label: '维保市场' },
]
const UrgentGradeList = [
  { value: '1', label: '一般' },
  { value: '2', label: '紧急' },
  { value: '3', label: '非常紧急' },
]
const ProManagementStyleList = [
  { value: '1', label: '专项项目管理制' },
  { value: '2', label: '订单式项目管理制' },
]
const ClientCompanyList = [
  { value: '1', label: '测试公司100000' },
  { value: '2', label: '测试公司100001' },
]
const ProStateList = [
  { value: '1', label: '在产' },
  { value: '2', label: '已发货' },
  { value: '3', label: '终验收' },
  { value: '4', label: '待验收' },
  { value: '5', label: '开口项验收' },
  { value: '6', label: '质保期' },
]
module.exports = [
  {
    url: '/project/getList',
    type: 'get',
    response: (config) => {
      const { pt_Name, pageNo = 1, pageSize = 20 } = config.query
      let mockList = List.filter((item) => {
        return !(pt_Name && item.pt_Name.indexOf(pt_Name) < 0)
      })
      const list = mockList.filter(
        (item, index) =>
          index < pageSize * pageNo && index >= pageSize * (pageNo - 1)
      )
      return {
        code: 200,
        msg: 'success',
        data: { list, ...{ total: mockList.length } },
      }
    },
  },
  {
    url: '/project/doEdit',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟保存成功',
      }
    },
  },
  {
    url: '/project/doDelete',
    type: 'post',
    response: () => {
      return {
        code: 200,
        msg: '模拟删除成功',
      }
    },
  },
  //项目分类
  {
    url: '/project/ProjectClass',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ProjectClassList },
      }
    },
  },
  //业务类型
  {
    url: '/project/Businesstype',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { BusinesstypeList },
      }
    },
  },
  //重要等级
  {
    url: '/project/UrgentGrade',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { UrgentGradeList },
      }
    },
  },
  //管理方式
  {
    url: '/project/ProManagementStyle',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ProManagementStyleList },
      }
    },
  },
  //客户公司
  {
    url: '/project/ClientCompany',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ClientCompanyList },
      }
    },
  },
  //状态
  {
    url: '/project/ProState',
    type: 'get',
    response: () => {
      return {
        code: 200,
        msg: 'success',
        data: { ProStateList },
      }
    },
  },
  //ProState
]
