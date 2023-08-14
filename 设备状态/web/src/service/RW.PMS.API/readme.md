

# 更新日志

## 2022-09-15
1. 新增BOM审核、BOM详情、我的BOM、BOM撤回等功能。

## 2022-03-09
1. 完整的CRUD前后端实践。
   前端：src/views/roleManagement、api/roleManagement.js、mock/roleManagement.js
   后端：controller/roleManagementController.cs、Application/System/RoleService.cs、Contracts/Dto/Role、Contracts/System/IRoleService.cs
待优化：
DDD设计中，Application层及Domain层目前是有问题的，Domain层应该包含相关的业务逻辑，而不是把业务逻辑些在Application层中。Application只负责调度、缓存、生命周期等功能。
目前的设计更像一个三层。


## 2022-03-08 v1.0.1
1. 调整了List返回结构。
2. 完成了部分权限管理功能。