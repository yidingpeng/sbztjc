/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.0.53
 Source Server Type    : MySQL
 Source Server Version : 50727
 Source Host           : 192.168.0.53:3306
 Source Schema         : rw.centralcontrol.sbztjc

 Target Server Type    : MySQL
 Target Server Version : 50727
 File Encoding         : 65001

 Date: 07/08/2023 14:00:19
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for DeviceFaultTimesDto
-- ----------------------------
DROP TABLE IF EXISTS `DeviceFaultTimesDto`;
CREATE TABLE `DeviceFaultTimesDto`  (
  `id` int(11) NOT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for attachmentupload
-- ----------------------------
DROP TABLE IF EXISTS `attachmentupload`;
CREATE TABLE `attachmentupload`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件类型',
  `FileId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件ID',
  `FileName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件名称',
  `FileSize` bigint(20) NOT NULL COMMENT '文件大小',
  `RootPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件根目录',
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件相对路径',
  `ExpireDate` datetime(3) NOT NULL COMMENT '过期时间',
  `IsView` int(11) NOT NULL COMMENT '是否查看',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 110 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '附件实体' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for axleorders
-- ----------------------------
DROP TABLE IF EXISTS `axleorders`;
CREATE TABLE `axleorders`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `AxleNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `AxleModel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RFID` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CurrentState` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreationTime` datetime(3) NOT NULL,
  `Operator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDeleted` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 47 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_device
-- ----------------------------
DROP TABLE IF EXISTS `base_device`;
CREATE TABLE `base_device`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `devName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `devNo` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `devIP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `createDate` datetime(3) NOT NULL,
  `devStatus` int(11) NOT NULL,
  `devCardno` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_gjwlopcpoint
-- ----------------------------
DROP TABLE IF EXISTS `base_gjwlopcpoint`;
CREATE TABLE `base_gjwlopcpoint`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `gwID` int(11) NOT NULL,
  `gwname` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `gjID` int(11) NOT NULL,
  `wlID` int(11) NOT NULL,
  `opcTypeID` int(11) NOT NULL,
  `opcTypeCode` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `opcDeviceName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `opcValue` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_gongwei
-- ----------------------------
DROP TABLE IF EXISTS `base_gongwei`;
CREATE TABLE `base_gongwei`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `gwcode` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `gwname` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `gwStatus` int(11) NULL DEFAULT NULL,
  `IP` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `areaID` int(11) NULL DEFAULT NULL,
  `gwOrder` int(11) NULL DEFAULT NULL,
  `isFinishGW` int(11) NULL DEFAULT NULL,
  `isHasFollow` int(11) NULL DEFAULT NULL,
  `isHasAms` int(11) NULL DEFAULT NULL,
  `isOPC` int(11) NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `agvAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 178 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_gongweiopc
-- ----------------------------
DROP TABLE IF EXISTS `base_gongweiopc`;
CREATE TABLE `base_gongweiopc`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `gwID` int(11) NULL DEFAULT NULL,
  `gwname` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `opcTypeID` int(11) NULL DEFAULT NULL,
  `opcTypeCode` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `opcValue` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_material
-- ----------------------------
DROP TABLE IF EXISTS `base_material`;
CREATE TABLE `base_material`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MtlCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MtlName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MtlModel` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `wlFigureNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MtlClassID` int(11) NOT NULL,
  `MtlTypeID` int(11) NOT NULL,
  `baseUnitID` int(11) NOT NULL,
  `importance` int(11) NOT NULL,
  `MtlIsHasCode` int(11) NOT NULL,
  `purchaseTime` datetime(3) NOT NULL,
  `effectTime` datetime(3) NOT NULL,
  `failuretime` datetime(3) NOT NULL,
  `texture` int(11) NOT NULL,
  `weight` int(11) NOT NULL,
  `size` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MtlStatus` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_material_detail
-- ----------------------------
DROP TABLE IF EXISTS `base_material_detail`;
CREATE TABLE `base_material_detail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_processinfo
-- ----------------------------
DROP TABLE IF EXISTS `base_processinfo`;
CREATE TABLE `base_processinfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `pcsNo` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `pcsName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ParentId` int(11) NOT NULL,
  `isFinishGW` int(11) NOT NULL,
  `usingFlag` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_product
-- ----------------------------
DROP TABLE IF EXISTS `base_product`;
CREATE TABLE `base_product`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `PNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品物料编号',
  `Pname` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品名称',
  `Pstatus` int(11) NULL DEFAULT NULL COMMENT '状态',
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_productextend
-- ----------------------------
DROP TABLE IF EXISTS `base_productextend`;
CREATE TABLE `base_productextend`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PModelID` int(11) NOT NULL,
  `colName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `textName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `textType` int(11) NOT NULL,
  `textMemo` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `extendValue` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_production
-- ----------------------------
DROP TABLE IF EXISTS `base_production`;
CREATE TABLE `base_production`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Pname` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `PCode` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Size` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Category` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Pstatus` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_productionmodel
-- ----------------------------
DROP TABLE IF EXISTS `base_productionmodel`;
CREATE TABLE `base_productionmodel`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `PmodelCode` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Pname` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `PnameShort` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DrawNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProductionModelType` int(11) NOT NULL,
  `ProductionType` int(11) NOT NULL,
  `TrainLine` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `TraiNModel` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `OrderIndex` int(11) NOT NULL,
  `Pstatus` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_productmodel
-- ----------------------------
DROP TABLE IF EXISTS `base_productmodel`;
CREATE TABLE `base_productmodel`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `PID` int(11) NULL DEFAULT NULL COMMENT '产品ID',
  `Pmodel` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品型号名称',
  `PmodelNo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '产品型号',
  `DrawingNo` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品图号',
  `beatMinite` int(11) NULL DEFAULT NULL COMMENT '生产节拍',
  `threshold` int(11) NULL DEFAULT NULL COMMENT '产品型号总物料阀值（%）百分比',
  `Pstatus` int(11) NULL DEFAULT NULL COMMENT '状态',
  `sapMaterialNumber` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'SAP物料编号',
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_tool
-- ----------------------------
DROP TABLE IF EXISTS `base_tool`;
CREATE TABLE `base_tool`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `toolCode` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `toolName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `toolClassID` int(11) NOT NULL,
  `toolTypeID` int(11) NOT NULL,
  `toolModel` int(11) NOT NULL,
  `toolBrand` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `toolCertificate` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `purchaseTime` datetime(3) NOT NULL,
  `effectTime` datetime(3) NOT NULL,
  `failuretime` datetime(3) NOT NULL,
  `toolIsHasCode` int(11) NOT NULL,
  `usingFlag` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_tool_detail
-- ----------------------------
DROP TABLE IF EXISTS `base_tool_detail`;
CREATE TABLE `base_tool_detail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for base_workcenter
-- ----------------------------
DROP TABLE IF EXISTS `base_workcenter`;
CREATE TABLE `base_workcenter`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `workCode` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `workName` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `workType` int(11) NOT NULL,
  `gwStatus` int(11) NOT NULL,
  `gwIP` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `gwMac` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `atAreaID` int(11) NOT NULL,
  `isHasFollow` int(11) NOT NULL,
  `isHasAms` int(11) NOT NULL,
  `isHasGuangying` int(11) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bd_contacts
-- ----------------------------
DROP TABLE IF EXISTS `bd_contacts`;
CREATE TABLE `bd_contacts`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ContactsName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系人姓名',
  `Sex` int(11) NULL DEFAULT NULL COMMENT '性别',
  `Telephone1` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话1',
  `Telephone2` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话2',
  `ContactsCategory` int(11) NOT NULL COMMENT '联系人类别',
  `Department` int(11) NOT NULL COMMENT '部门',
  `Post` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '岗位',
  `HistoryJob` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '历史任职履历',
  `Tel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '办公电话',
  `Fax` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '传真',
  `Nation` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '民族',
  `ResponsibleBusiness` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '负责业务',
  `Email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Wechat` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信号',
  `QQ` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `OfficeAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '办公地址',
  `Birthday` datetime(3) NOT NULL COMMENT '生日',
  `Hobby` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '爱好',
  `Age` int(11) NULL DEFAULT NULL COMMENT '年龄',
  `CompanyId` int(11) NOT NULL COMMENT '单位编号',
  `College` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '本科学校',
  `CollegeAt` datetime(3) NOT NULL COMMENT '本科毕业年份',
  `ThurberSchool` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '硕博学校',
  `ThurberSchoolAt` datetime(3) NOT NULL COMMENT '硕博毕业年份',
  `ContactsBackground` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '行业人脉背景',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 109 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '联系人表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bd_processinfo
-- ----------------------------
DROP TABLE IF EXISTS `bd_processinfo`;
CREATE TABLE `bd_processinfo`  (
  `Id` int(11) NOT NULL COMMENT '主键ID',
  `ParentId` int(11) NULL DEFAULT NULL COMMENT '上级父ID',
  `Step` int(11) NULL DEFAULT NULL COMMENT '层级',
  `Sort` int(11) NULL DEFAULT NULL COMMENT '同级排序号',
  `OperationCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工序编码',
  `OperationName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工序名称',
  `JobContent` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工作内容',
  `IsMilepost` bit(1) NULL DEFAULT NULL COMMENT '是否里程碑',
  `
IsKeyProcess` bit(1) NULL DEFAULT NULL COMMENT '是否关键工序',
  `IsReview` bit(11) NULL DEFAULT NULL COMMENT '是否需要评审',
  `
InputCondition` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '输出条件',
  `OutputFile` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '输出文件',
  `IsSelect` bit(1) NULL DEFAULT NULL COMMENT '是否必选',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bd_salesarea_info
-- ----------------------------
DROP TABLE IF EXISTS `bd_salesarea_info`;
CREATE TABLE `bd_salesarea_info`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `AreaCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '市场片区编码',
  `AreaName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '市场片区名称',
  `AreaGM` int(11) NOT NULL COMMENT '片区营销经理',
  `AreaCharger` int(11) NOT NULL COMMENT '片区负责人',
  `AreaSalesman` int(11) NOT NULL COMMENT '片区营销人员',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `PlaceName` int(11) NOT NULL COMMENT '板块名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 69 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '市场片区信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bd_salesareainfo
-- ----------------------------
DROP TABLE IF EXISTS `bd_salesareainfo`;
CREATE TABLE `bd_salesareainfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `AreaCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AreaName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `PlaceName` int(11) NOT NULL,
  `AreaGM` int(11) NOT NULL,
  `AreaCharger` int(11) NOT NULL,
  `AreaSalesman` int(11) NOT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bd_taskprocess_info
-- ----------------------------
DROP TABLE IF EXISTS `bd_taskprocess_info`;
CREATE TABLE `bd_taskprocess_info`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `SeqNo` int(11) NOT NULL,
  `ProjectClassID` int(11) NOT NULL,
  `TaskCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TaskName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `WorkMemo` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsMilestone` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsKey` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsReview` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `InputCondition` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OutPutFile` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Remark` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 73 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for bom_approval_log
-- ----------------------------
DROP TABLE IF EXISTS `bom_approval_log`;
CREATE TABLE `bom_approval_log`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BomId` int(11) NOT NULL,
  `User` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ApprovedTime` datetime(3) NOT NULL,
  `Desc` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `Result` bit(1) NOT NULL,
  `Node` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 62 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for client_company
-- ----------------------------
DROP TABLE IF EXISTS `client_company`;
CREATE TABLE `client_company`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ClientName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ClientFullName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ClientRank` int(11) NOT NULL,
  `CooperativeBusiness` int(11) NOT NULL,
  `CEO` int(11) NOT NULL,
  `GM` int(11) NOT NULL,
  `DeputyGM` int(11) NOT NULL,
  `MarketArea` int(11) NOT NULL,
  `PersonInCharge` int(11) NOT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for contractdetail
-- ----------------------------
DROP TABLE IF EXISTS `contractdetail`;
CREATE TABLE `contractdetail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PID` int(11) NOT NULL COMMENT '父级ID',
  `ProjectId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编号',
  `EquipmentCount` int(11) NOT NULL COMMENT '设备数量',
  `EquipmentUnitPrice` decimal(10, 2) NOT NULL COMMENT '设备合同单价',
  `EquipmentTotalPrice` decimal(10, 2) NOT NULL COMMENT '设备合同总价',
  `DeliverDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '要求交付日期',
  `AdvanceCharge` decimal(10, 2) NOT NULL COMMENT '预付款',
  `ProgressPayment` decimal(10, 2) NOT NULL COMMENT '进度款',
  `AcceptancePayment` decimal(10, 2) NOT NULL COMMENT '验收款',
  `RetentionMoney` decimal(10, 2) NOT NULL COMMENT '质保金',
  `SettlementFunds` decimal(10, 2) NOT NULL COMMENT '结算款',
  `BalancePayment` decimal(10, 2) NOT NULL COMMENT '尾款',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '合同信息子表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for customlogtable
-- ----------------------------
DROP TABLE IF EXISTS `customlogtable`;
CREATE TABLE `customlogtable`  (
  `Id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `Message` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Level` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TimeStamps` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Result` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for devcie_runtime
-- ----------------------------
DROP TABLE IF EXISTS `devcie_runtime`;
CREATE TABLE `devcie_runtime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 184 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for devcie_totalOperationTime
-- ----------------------------
DROP TABLE IF EXISTS `devcie_totalOperationTime`;
CREATE TABLE `devcie_totalOperationTime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_FaultNumberCount
-- ----------------------------
DROP TABLE IF EXISTS `device_FaultNumberCount`;
CREATE TABLE `device_FaultNumberCount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `CountNumber` int(11) NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `week` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `month` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `year` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_StopTimeCount
-- ----------------------------
DROP TABLE IF EXISTS `device_StopTimeCount`;
CREATE TABLE `device_StopTimeCount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `CountTime` datetime NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `week` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `month` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `year` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_TestRoom
-- ----------------------------
DROP TABLE IF EXISTS `device_TestRoom`;
CREATE TABLE `device_TestRoom`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `equipmentOperationStatus` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `equipmentTestStatus` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `holidayTime` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `maintenanceTime` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `dispensationTime` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_apinumber
-- ----------------------------
DROP TABLE IF EXISTS `device_apinumber`;
CREATE TABLE `device_apinumber`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `deviceNumber` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_datacount
-- ----------------------------
DROP TABLE IF EXISTS `device_datacount`;
CREATE TABLE `device_datacount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `runTimeCount` double NULL DEFAULT NULL,
  `stopTimeCount` double NULL DEFAULT NULL,
  `faultNumberCount` int(11) NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `week` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `month` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `year` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 56 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_devicemonthcount
-- ----------------------------
DROP TABLE IF EXISTS `device_devicemonthcount`;
CREATE TABLE `device_devicemonthcount`  (
  `id` int(10) UNSIGNED NOT NULL,
  `totalRunTime` double NULL DEFAULT NULL,
  `totalStopTime` double NULL DEFAULT NULL,
  `totalFaultDownTime` double NULL DEFAULT NULL,
  `toalFaultNumber` int(11) NULL DEFAULT NULL,
  `totalFreeTime` double NULL DEFAULT NULL,
  `weibaoTime` double NULL DEFAULT NULL,
  `months` int(11) NULL DEFAULT NULL,
  `years` int(11) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `trainningTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_devicestatus
-- ----------------------------
DROP TABLE IF EXISTS `device_devicestatus`;
CREATE TABLE `device_devicestatus`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `operationStatus` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `totalRunTime` double NOT NULL,
  `totalStopTime` double NOT NULL,
  `totalFaultDownTime` double NOT NULL,
  `toalFaultNumber` int(11) NOT NULL,
  `totalFreeTime` double NOT NULL,
  `weibaoTime` double NOT NULL,
  `roomId` int(11) NOT NULL,
  `isClear` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 59 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_devicestatusname
-- ----------------------------
DROP TABLE IF EXISTS `device_devicestatusname`;
CREATE TABLE `device_devicestatusname`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `DevicestatusTagName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_deviceweekcount
-- ----------------------------
DROP TABLE IF EXISTS `device_deviceweekcount`;
CREATE TABLE `device_deviceweekcount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `totalRunTime` double NULL DEFAULT NULL,
  `totalStopTime` double NULL DEFAULT NULL,
  `totalFaultDownTime` double NULL DEFAULT NULL,
  `toalFaultNumber` int(11) NULL DEFAULT NULL,
  `totalFreeTime` double NULL DEFAULT NULL,
  `weibaoTime` double NULL DEFAULT NULL,
  `weeks` int(11) NULL DEFAULT NULL,
  `months` int(11) NULL DEFAULT NULL,
  `years` int(11) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `trainningTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_deviceyearcount
-- ----------------------------
DROP TABLE IF EXISTS `device_deviceyearcount`;
CREATE TABLE `device_deviceyearcount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `totalRunTime` double NULL DEFAULT NULL,
  `totalStopTime` double NULL DEFAULT NULL,
  `totalFaultDownTime` double NULL DEFAULT NULL,
  `toalFaultNumber` int(11) NULL DEFAULT NULL,
  `totalFreeTime` double NULL DEFAULT NULL,
  `weibaoTime` double NULL DEFAULT NULL,
  `years` int(11) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `trainningTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_effectiveOperationTime
-- ----------------------------
DROP TABLE IF EXISTS `device_effectiveOperationTime`;
CREATE TABLE `device_effectiveOperationTime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_faultTime
-- ----------------------------
DROP TABLE IF EXISTS `device_faultTime`;
CREATE TABLE `device_faultTime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_faultdowntime
-- ----------------------------
DROP TABLE IF EXISTS `device_faultdowntime`;
CREATE TABLE `device_faultdowntime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 48 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_faultnumbertime
-- ----------------------------
DROP TABLE IF EXISTS `device_faultnumbertime`;
CREATE TABLE `device_faultnumbertime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_faultreporting
-- ----------------------------
DROP TABLE IF EXISTS `device_faultreporting`;
CREATE TABLE `device_faultreporting`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `testRoom` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `faultDateTime` datetime(3) NOT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `faultReason` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `reportingStatus` int(11) NOT NULL,
  `faultTimeid` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 25 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_freetime
-- ----------------------------
DROP TABLE IF EXISTS `device_freetime`;
CREATE TABLE `device_freetime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_holidaytime
-- ----------------------------
DROP TABLE IF EXISTS `device_holidaytime`;
CREATE TABLE `device_holidaytime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_stoptime
-- ----------------------------
DROP TABLE IF EXISTS `device_stoptime`;
CREATE TABLE `device_stoptime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 177 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_testStopTime
-- ----------------------------
DROP TABLE IF EXISTS `device_testStopTime`;
CREATE TABLE `device_testStopTime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_testTime
-- ----------------------------
DROP TABLE IF EXISTS `device_testTime`;
CREATE TABLE `device_testTime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_testsheet
-- ----------------------------
DROP TABLE IF EXISTS `device_testsheet`;
CREATE TABLE `device_testsheet`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceRoom` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `projectName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `employeeId` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `testEngineer` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `startTime` datetime(3) NOT NULL,
  `endTime` datetime(3) NOT NULL,
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_totalhalttime
-- ----------------------------
DROP TABLE IF EXISTS `device_totalhalttime`;
CREATE TABLE `device_totalhalttime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_trainingtime
-- ----------------------------
DROP TABLE IF EXISTS `device_trainingtime`;
CREATE TABLE `device_trainingtime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_weibaotime
-- ----------------------------
DROP TABLE IF EXISTS `device_weibaotime`;
CREATE TABLE `device_weibaotime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_weibaotimeonoff
-- ----------------------------
DROP TABLE IF EXISTS `device_weibaotimeonoff`;
CREATE TABLE `device_weibaotimeonoff`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for device_zhiduTime
-- ----------------------------
DROP TABLE IF EXISTS `device_zhiduTime`;
CREATE TABLE `device_zhiduTime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `startTime` datetime(3) NOT NULL,
  `endTime` datetime(3) NOT NULL,
  `roomid` int(11) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for devicetrainningtimedto
-- ----------------------------
DROP TABLE IF EXISTS `devicetrainningtimedto`;
CREATE TABLE `devicetrainningtimedto`  (
  `id` int(11) NOT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for deviceweibaotimedto
-- ----------------------------
DROP TABLE IF EXISTS `deviceweibaotimedto`;
CREATE TABLE `deviceweibaotimedto`  (
  `id` int(11) NOT NULL,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for dictionary
-- ----------------------------
DROP TABLE IF EXISTS `dictionary`;
CREATE TABLE `dictionary`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `DictionaryText` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DictionaryValue` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_TestRoomStatus
-- ----------------------------
DROP TABLE IF EXISTS `droom_TestRoomStatus`;
CREATE TABLE `droom_TestRoomStatus`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `operationStatus` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `totalEffectiveRunningTime` double NOT NULL,
  `totalTestStopTime` double NOT NULL,
  `totalFaultTime` double NOT NULL,
  `totalStandbyTime` double NOT NULL,
  `totalFreeTime` double NOT NULL,
  `totalweibaoTime` double NOT NULL,
  `totalUtilizationRate` double NOT NULL,
  `isClear` int(11) NOT NULL,
  `totaldeviceDebugRunTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_devicedebugruntime
-- ----------------------------
DROP TABLE IF EXISTS `droom_devicedebugruntime`;
CREATE TABLE `droom_devicedebugruntime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_droommonthcount
-- ----------------------------
DROP TABLE IF EXISTS `droom_droommonthcount`;
CREATE TABLE `droom_droommonthcount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `totalEffectiveRunningTime` double NOT NULL,
  `totalTestStopTime` double NOT NULL,
  `totalFaultTime` double NOT NULL,
  `totalStandbyTime` double NOT NULL,
  `totalFreeTime` double NOT NULL,
  `totalweibaoTime` double NOT NULL,
  `totalUtilizationRate` double NOT NULL,
  `deviceDebugRun` double NULL DEFAULT NULL,
  `months` int(4) NULL DEFAULT NULL,
  `years` int(4) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `jiaozhunTime` double NULL DEFAULT NULL,
  `zhiduTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_droomweekcount
-- ----------------------------
DROP TABLE IF EXISTS `droom_droomweekcount`;
CREATE TABLE `droom_droomweekcount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `totalEffectiveRunningTime` double NOT NULL,
  `totalTestStopTime` double NOT NULL,
  `totalFaultTime` double NOT NULL,
  `totalStandbyTime` double NOT NULL,
  `totalFreeTime` double NOT NULL,
  `totalweibaoTime` double NOT NULL,
  `totalUtilizationRate` double NOT NULL,
  `deviceDebugRun` double NULL DEFAULT NULL,
  `weeks` int(4) NULL DEFAULT NULL,
  `months` int(4) NULL DEFAULT NULL,
  `years` int(4) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `jiaozhunTime` double NULL DEFAULT NULL,
  `zhiduTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_droomyearcount
-- ----------------------------
DROP TABLE IF EXISTS `droom_droomyearcount`;
CREATE TABLE `droom_droomyearcount`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `totalEffectiveRunningTime` double NOT NULL,
  `totalTestStopTime` double NOT NULL,
  `totalFaultTime` double NOT NULL,
  `totalStandbyTime` double NOT NULL,
  `totalFreeTime` double NOT NULL,
  `totalweibaoTime` double NOT NULL,
  `totalUtilizationRate` double NOT NULL,
  `deviceDebugRun` double NULL DEFAULT NULL,
  `years` int(4) NULL DEFAULT NULL,
  `holidayTime` double NULL DEFAULT NULL,
  `jiaozhunTime` double NULL DEFAULT NULL,
  `zhiduTime` double NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_effectiverunningtime
-- ----------------------------
DROP TABLE IF EXISTS `droom_effectiverunningtime`;
CREATE TABLE `droom_effectiverunningtime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_faulttime
-- ----------------------------
DROP TABLE IF EXISTS `droom_faulttime`;
CREATE TABLE `droom_faulttime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 42 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_freetime
-- ----------------------------
DROP TABLE IF EXISTS `droom_freetime`;
CREATE TABLE `droom_freetime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_holidaytime
-- ----------------------------
DROP TABLE IF EXISTS `droom_holidaytime`;
CREATE TABLE `droom_holidaytime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_jiaozhuntime
-- ----------------------------
DROP TABLE IF EXISTS `droom_jiaozhuntime`;
CREATE TABLE `droom_jiaozhuntime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_standbytime
-- ----------------------------
DROP TABLE IF EXISTS `droom_standbytime`;
CREATE TABLE `droom_standbytime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 110 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_testoccupytime
-- ----------------------------
DROP TABLE IF EXISTS `droom_testoccupytime`;
CREATE TABLE `droom_testoccupytime`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 231 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_teststoptime
-- ----------------------------
DROP TABLE IF EXISTS `droom_teststoptime`;
CREATE TABLE `droom_teststoptime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_weibaotime
-- ----------------------------
DROP TABLE IF EXISTS `droom_weibaotime`;
CREATE TABLE `droom_weibaotime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_weibaotimeonoff
-- ----------------------------
DROP TABLE IF EXISTS `droom_weibaotimeonoff`;
CREATE TABLE `droom_weibaotimeonoff`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `onOrOff` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `alarmTime` datetime(3) NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for droom_zhidutime
-- ----------------------------
DROP TABLE IF EXISTS `droom_zhidutime`;
CREATE TABLE `droom_zhidutime`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `deviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `roomName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `createTime` datetime(3) NOT NULL,
  `alarmTime` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 21 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for file
-- ----------------------------
DROP TABLE IF EXISTS `file`;
CREATE TABLE `file`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ContentType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件类型',
  `FileName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件名称',
  `FileSize` bigint(20) NOT NULL COMMENT '文件大小',
  `RootPath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件根目录',
  `RelativePath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件相对路径',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 73 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '附件实体' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_abnormal
-- ----------------------------
DROP TABLE IF EXISTS `follow_abnormal`;
CREATE TABLE `follow_abnormal`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `fa_orderCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消息反馈流水号',
  `fa_areaID` int(11) NULL DEFAULT NULL COMMENT '区域编号',
  `fa_areaName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区域名称',
  `fa_operID` int(11) NULL DEFAULT NULL COMMENT '操作员，employee.ID',
  `fa_oper` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作员，employee.Name',
  `fa_createtime` timestamp NULL DEFAULT NULL COMMENT '反馈时间',
  `fa_feedMemo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '反馈内容',
  `fa_feedType` int(11) NULL DEFAULT NULL COMMENT '反馈类型',
  `fa_remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `fa_isok` int(11) NULL DEFAULT NULL COMMENT '反馈结果',
  `fa_result` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '处理方案',
  `frm_orderNo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单编号',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_abnormal_image
-- ----------------------------
DROP TABLE IF EXISTS `follow_abnormal_image`;
CREATE TABLE `follow_abnormal_image`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `fa_orderCode` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '消息反馈流水号',
  `ImgUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL COMMENT '图片地址',
  `photos` varchar(200) CHARACTER SET utf8 COLLATE utf8_bin NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_detail
-- ----------------------------
DROP TABLE IF EXISTS `follow_detail`;
CREATE TABLE `follow_detail`  (
  `fd_guid` int(11) NOT NULL AUTO_INCREMENT COMMENT '追溯明细GUID',
  `fp_guid` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品生产主表GUID',
  `pInfo_ID` int(11) NULL DEFAULT NULL COMMENT '产品生产基础信息ID',
  `fd_areaID` int(11) NULL DEFAULT NULL COMMENT '区域ID',
  `fd_areaName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区域名称',
  `fd_barcode` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '条码卡号',
  `fd_stampNo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '物料钢印号/RFID',
  `fd_operID` int(11) NULL DEFAULT NULL COMMENT '操作员ID',
  `fd_oper` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作员名称',
  `fd_starttime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '开始检修时间',
  `fd_finishtime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '完成检修时间',
  `fd_followStatus` int(11) NULL DEFAULT NULL COMMENT '追溯状态:0：未开始，1：清洗完毕，2：喷漆完成，3：烘干完成，4：检测完成',
  `fd_checkResult` int(11) NULL DEFAULT NULL COMMENT '检验结果:-1:空；0:不合格；1：合格；',
  `fd_resultMemo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '结果说明',
  `fd_isNextScan` int(11) NULL DEFAULT NULL COMMENT '下一区域扫码状态：0未到；1已到',
  `fd_isCancel` int(11) NULL DEFAULT NULL COMMENT '是否撤销，0：不撤销；1：已撤销',
  `fd_remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`fd_guid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_feedback
-- ----------------------------
DROP TABLE IF EXISTS `follow_feedback`;
CREATE TABLE `follow_feedback`  (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `fb_orderCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消息反馈流水号',
  `fb_areaID` int(11) NULL DEFAULT NULL COMMENT '区域编号',
  `fb_areaName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区域名称',
  `fb_operID` int(11) NULL DEFAULT NULL COMMENT '操作员，employee.ID',
  `fb_oper` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作员，employee.Name',
  `fb_createtime` timestamp NULL DEFAULT NULL COMMENT '反馈时间',
  `fb_feedMemo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '反馈内容',
  `fb_feedType` int(11) NULL DEFAULT NULL COMMENT '反馈类型',
  `fb_remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `fb_isok` int(11) NULL DEFAULT NULL COMMENT '反馈结果',
  `fb_result` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '处理方案',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_feedback_detail
-- ----------------------------
DROP TABLE IF EXISTS `follow_feedback_detail`;
CREATE TABLE `follow_feedback_detail`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fdb_type` int(11) NULL DEFAULT NULL COMMENT '反馈类型 0.信息反馈 1.故障报修',
  `fId` int(11) NULL DEFAULT NULL COMMENT '主表ID',
  `fdb_img` longblob NULL COMMENT '信息反馈图片',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_main
-- ----------------------------
DROP TABLE IF EXISTS `follow_main`;
CREATE TABLE `follow_main`  (
  `fm_guid` int(11) NOT NULL AUTO_INCREMENT COMMENT '追溯主表GUID',
  `fp_guid` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '生产主表GUID',
  `pInfo_ID` int(11) NULL DEFAULT NULL COMMENT '产品信息表ID',
  `fm_inHouseTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '入场时间',
  `fm_starttime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '开始检修时间',
  `fm_finishtime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '完成检修时间',
  `fm_finishStatus` int(11) NULL DEFAULT NULL COMMENT '追溯状态:0：未开始，1：清洗完毕，2：喷漆完成，3：烘干完成，4：检测完成',
  `fm_curAreaID` int(11) NULL DEFAULT NULL COMMENT '当前生产区域，数据字典区域ID',
  `fm_creatorID` int(11) NULL DEFAULT NULL COMMENT '信息创建人ID',
  `fm_creator` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '信息创建人',
  `fm_resultIsOK` int(11) NULL DEFAULT NULL COMMENT '质量结果',
  `fm_resultMemo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '结果说明',
  `fm_remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `fm_uploadFlag` int(11) NULL DEFAULT NULL COMMENT '上传标志',
  PRIMARY KEY (`fm_guid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for follow_production
-- ----------------------------
DROP TABLE IF EXISTS `follow_production`;
CREATE TABLE `follow_production`  (
  `fp_guid` int(11) NOT NULL AUTO_INCREMENT COMMENT '生产GUID',
  `pp_guid` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单表ID',
  `fp_pInfo_ID` int(11) NULL DEFAULT NULL COMMENT '产品信息ID',
  `fp_prodNo_sys` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '系统产品编号',
  `fp_startTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '开始时间',
  `fp_finishTime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' COMMENT '完成时间',
  `fp_finishStatus` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品完成状态：0：未开始，1：清洗完毕，2：喷漆完成，3：烘干完成，4：检测完成',
  `fp_resultIsOK` int(11) NULL DEFAULT NULL COMMENT '质量结果',
  `fp_resultMemo` int(11) NULL DEFAULT NULL COMMENT '结果说明',
  `fp_report` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '产品生产线完成情况报表存放路径',
  `fp_remark` longblob NULL COMMENT '备注',
  `fp_uploadFlag` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上传标志',
  PRIMARY KEY (`fp_guid`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for logdetails
-- ----------------------------
DROP TABLE IF EXISTS `logdetails`;
CREATE TABLE `logdetails`  (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `LogDate` datetime NULL DEFAULT NULL,
  `LogThread` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LogLevel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LogLogger` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LogActionClick` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LogMessage` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserIP` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_cd
-- ----------------------------
DROP TABLE IF EXISTS `mat_cd`;
CREATE TABLE `mat_cd`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `QrCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 26 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_conversion
-- ----------------------------
DROP TABLE IF EXISTS `mat_conversion`;
CREATE TABLE `mat_conversion`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Model` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BasicUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `HsUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Count` int(11) NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_fifo
-- ----------------------------
DROP TABLE IF EXISTS `mat_fifo`;
CREATE TABLE `mat_fifo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Type` int(11) NOT NULL,
  `BomCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Count` int(11) NOT NULL,
  `FifoDateTime` datetime(3) NOT NULL,
  `FifoPersonnel` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `QrCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DeliverySignature` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 86 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_indentprimary
-- ----------------------------
DROP TABLE IF EXISTS `mat_indentprimary`;
CREATE TABLE `mat_indentprimary`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CurrentVersion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Applicant` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ApplicationDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CompleteSetRate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DrawingCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomId` int(11) NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 37 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_inventory
-- ----------------------------
DROP TABLE IF EXISTS `mat_inventory`;
CREATE TABLE `mat_inventory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Model` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Parameter` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Unit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomCount` int(11) NOT NULL,
  `WarehousCount` int(11) NOT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Supplier` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `StorageLocation` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 20 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_operate
-- ----------------------------
DROP TABLE IF EXISTS `mat_operate`;
CREATE TABLE `mat_operate`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `BomId` int(11) NOT NULL COMMENT 'BOMID',
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料编码',
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料名称',
  `YQArrivalTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '要求到货时间',
  `AssignStaff` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '采购人员',
  `YJArrivalTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '预计到货时间',
  `SJArrivalTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '实际到货时间',
  `Supplier` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '供应商',
  `Count` int(11) NOT NULL COMMENT '数量',
  `CostPrice` decimal(10, 2) NOT NULL COMMENT '成本价格',
  `DrawingCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '图纸代号',
  `YJFinishTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '预计完成时间',
  `SJFinishTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '实际完成时间',
  `GYSArrivalTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '预计入库时间（供应商）',
  `State` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '状态',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `BOMCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `TraceabilityCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `qualified` int(11) NOT NULL DEFAULT 0,
  `YjShipTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '预计发货时间',
  `ParentCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '父级编码',
  `SublevelCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '子级编码',
  `SerialNumber` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '序列号',
  `IsDrawing` bit(1) NOT NULL COMMENT '是否图纸',
  `Tag` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '标记 initial edit del',
  `Designation` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '代号',
  `Specification` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '规格型号',
  `TechnicalParameters` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '技术参数',
  `MaterialQuality` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '材质',
  `Brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '品牌',
  `Unit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '单位',
  `Weight` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '重量',
  `SubmittedUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '送检单位',
  `MatSource` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料来源',
  `DistributionTime` datetime(3) NOT NULL COMMENT '下发时间',
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `UnitPrice` decimal(10, 2) NOT NULL COMMENT '单价',
  `Amount` decimal(10, 2) NOT NULL COMMENT '金额',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 802 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = 'BOM订单子表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_productissu
-- ----------------------------
DROP TABLE IF EXISTS `mat_productissu`;
CREATE TABLE `mat_productissu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `QrCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `LLMan` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Count` int(11) NOT NULL,
  `State` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_purchase_detail
-- ----------------------------
DROP TABLE IF EXISTS `mat_purchase_detail`;
CREATE TABLE `mat_purchase_detail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ApplicationNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Model` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BasicUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Count` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 71 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_purchase_order
-- ----------------------------
DROP TABLE IF EXISTS `mat_purchase_order`;
CREATE TABLE `mat_purchase_order`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ApplicationNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Reason` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `status` int(11) NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 29 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_qc
-- ----------------------------
DROP TABLE IF EXISTS `mat_qc`;
CREATE TABLE `mat_qc`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `QrCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '二维码',
  `QCount` int(11) NOT NULL COMMENT '合格数量',
  `Qualified` int(11) NOT NULL COMMENT '是否合格',
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 25 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '质检表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for mat_supplier
-- ----------------------------
DROP TABLE IF EXISTS `mat_supplier`;
CREATE TABLE `mat_supplier`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MatProperty` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SupCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SupName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SupplyBrand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SupplyType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CompanyType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SupPrincipal` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ContactDetails` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 154 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for materialinfo
-- ----------------------------
DROP TABLE IF EXISTS `materialinfo`;
CREATE TABLE `materialinfo`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料编码',
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料名称',
  `Specific` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '规格型号',
  `requirements` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '技术要求',
  `Unit` int(11) NOT NULL COMMENT '计量单位',
  `Weight` double NOT NULL COMMENT '重量',
  `Category` int(11) NOT NULL COMMENT '物料类别',
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `CodeName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '代号',
  `material` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '材质',
  `brand` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '品牌',
  `RulePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '规则路径',
  `MaterialSource` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料来源',
  `CurrentState` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '当前状态',
  `OperatingType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '操作类型',
  `disabled` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '停用状态',
  `Tyreason` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '停用原因',
  `applicant` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '申请人',
  `ApplicationTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '申请时间',
  `editor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改者',
  `EditDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '修改日期',
  `Inspector` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核者',
  `InspectorData` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核日期',
  `ReferencePrice` decimal(10, 2) NOT NULL DEFAULT 0.00 COMMENT '参考价',
  `PurchasingCycle` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '采购周期',
  `Importance` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '重要度',
  `PurchasingUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '采购单位',
  `ErpState` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'ERP状态',
  `ErpEditer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'ERP更新者',
  `ErpEditTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'ERP更新时间',
  `MaterialOriginalCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '物料原码',
  `SynchronisedTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '同步时间',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编码',
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目名称',
  `Group` int(11) NOT NULL DEFAULT 0 COMMENT '物料分组',
  `Model` int(11) NOT NULL DEFAULT 0 COMMENT '产品',
  `Project` int(11) NOT NULL DEFAULT 0 COMMENT '项目',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '物料信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for module
-- ----------------------------
DROP TABLE IF EXISTS `module`;
CREATE TABLE `module`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Hidden` bit(1) NOT NULL,
  `Icon` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Target` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Path` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Component` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ModuleType` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ModuleCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ParentId` int(11) NOT NULL,
  `Required` bit(1) NOT NULL,
  `OrderIndex` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for msg_content
-- ----------------------------
DROP TABLE IF EXISTS `msg_content`;
CREATE TABLE `msg_content`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ConfigId` int(11) NOT NULL,
  `DataId` int(11) NULL DEFAULT NULL,
  `MessageLevel` int(11) NOT NULL,
  `Type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Title` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for msg_receiver
-- ----------------------------
DROP TABLE IF EXISTS `msg_receiver`;
CREATE TABLE `msg_receiver`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ContentId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `RemindTime` datetime(3) NOT NULL,
  `IsRead` bit(1) NOT NULL,
  `ReadTime` datetime(3) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for operation
-- ----------------------------
DROP TABLE IF EXISTS `operation`;
CREATE TABLE `operation`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleId` int(11) NOT NULL,
  `OperationCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for organization
-- ----------------------------
DROP TABLE IF EXISTS `organization`;
CREATE TABLE `organization`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `OrganizationName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizationCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizationType` int(11) NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Leader` int(11) NULL DEFAULT NULL,
  `SyncId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '同步Id，用于与不同系统间进行同步的唯一ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 101 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pdm_bom
-- ----------------------------
DROP TABLE IF EXISTS `pdm_bom`;
CREATE TABLE `pdm_bom`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BOMCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BOMName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RequiredTime` datetime(3) NULL DEFAULT NULL,
  `Status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Version` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `VersionIndex` int(11) NOT NULL,
  `CreateTime` datetime(3) NOT NULL,
  `CreateBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ComitTime` datetime(3) NULL DEFAULT NULL,
  `ComitBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AuditTime` datetime(3) NULL DEFAULT NULL,
  `AuditBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `PurchaseBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ApprovalTime` datetime(3) NULL DEFAULT NULL,
  `ApprovalBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IsOld` bit(1) NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pdm_material
-- ----------------------------
DROP TABLE IF EXISTS `pdm_material`;
CREATE TABLE `pdm_material`  (
  `Cid` int(11) NOT NULL,
  `Code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Alias` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Model` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Specification` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Material` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Manufacturer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BasicUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Source` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Important` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CodingStatus` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Weight` double NOT NULL,
  `WeightUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CategoryPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CategoryMaster` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CategoryLast` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CategoryCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreateTime` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RefPrice` double NULL DEFAULT NULL,
  `PriceUnit` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Cid`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pdm_material_type
-- ----------------------------
DROP TABLE IF EXISTS `pdm_material_type`;
CREATE TABLE `pdm_material_type`  (
  `Id` int(11) NOT NULL,
  `Code` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Parent` int(11) NOT NULL,
  `IsRoot` bit(1) NOT NULL,
  `Path` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_acceptance
-- ----------------------------
DROP TABLE IF EXISTS `pro_acceptance`;
CREATE TABLE `pro_acceptance`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProjectID` int(11) NOT NULL COMMENT '项目id',
  `DeviceID` int(11) NOT NULL COMMENT '设备id',
  `AcceptCategory` int(11) NOT NULL COMMENT '验收类别',
  `AcceptDate` datetime(3) NOT NULL COMMENT '验收日期',
  `Acceptancer` int(11) NOT NULL COMMENT '验收人员',
  `Qty` int(11) NULL DEFAULT NULL COMMENT '验收数量',
  `CompletedDate` datetime(3) NOT NULL COMMENT '竣工日期',
  `AcceptResult` int(11) NOT NULL COMMENT '验收结论',
  `FinalAbarbeitungDate` datetime(3) NOT NULL COMMENT '最终整改完成日期',
  `SignConfirmFile` int(11) NOT NULL COMMENT '签字确认附件',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 23 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目交付信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_basics
-- ----------------------------
DROP TABLE IF EXISTS `pro_basics`;
CREATE TABLE `pro_basics`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProjectClass` int(11) NOT NULL,
  `BusinessType` int(11) NOT NULL,
  `UrgentGrade` int(11) NOT NULL,
  `ClientCompany` int(11) NOT NULL,
  `ProjectReceiveDate` datetime(3) NOT NULL,
  `ContractDeliveryDate` datetime(3) NOT NULL,
  `ExpectedUseDate` datetime(3) NOT NULL,
  `ProjectBackground` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProjectSummary` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsContract` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsTechnicalAgreement` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BiddingNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProManagementStyle` int(11) NOT NULL,
  `ProState` int(11) NOT NULL,
  `Attr1` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Attr2` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Attr3` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Attr4` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `TechnicalAgreementPath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SalesPrice` decimal(10, 2) NOT NULL,
  `ContractID` int(11) NOT NULL,
  `InitialProjectCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProjectStatus` int(11) NOT NULL,
  `OwnerUnit` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OwnerUnitID` int(11) NOT NULL,
  `ProLine` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 233 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_basics_files
-- ----------------------------
DROP TABLE IF EXISTS `pro_basics_files`;
CREATE TABLE `pro_basics_files`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PID` int(11) NOT NULL COMMENT '父级ID',
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件类型',
  `FileName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件名称',
  `FileSize` bigint(20) NOT NULL COMMENT '文件大小',
  `RootPath` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件根目录',
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '文件相对路径',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 18 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_client_company
-- ----------------------------
DROP TABLE IF EXISTS `pro_client_company`;
CREATE TABLE `pro_client_company`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `CompanyCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '单位编号',
  `ClientName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '客户名称',
  `ClientFullName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '客户全称',
  `Address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
  `ClientRank` int(11) NOT NULL COMMENT '客户级别',
  `CEO` int(11) NOT NULL COMMENT '公司董事长',
  `GM` int(11) NOT NULL COMMENT '公司总经理',
  `DeputyGM` int(11) NOT NULL COMMENT '公司副总经理',
  `MarketArea` int(11) NOT NULL COMMENT '市场片区',
  `PersonInCharge` int(11) NOT NULL COMMENT '营销负责人',
  `CooperativeBusiness` int(11) NOT NULL COMMENT '合作业务',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `OwnerName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 89 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '客户公司信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_contacts
-- ----------------------------
DROP TABLE IF EXISTS `pro_contacts`;
CREATE TABLE `pro_contacts`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PID` int(11) NOT NULL COMMENT '项目ID',
  `FZBKId` int(11) NOT NULL COMMENT '负责板块ID',
  `ContactsID` int(11) NOT NULL COMMENT '联系人ID',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `TeamMembersName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系人名称',
  `Dept` int(11) NOT NULL COMMENT '部门',
  `ContactsType` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 336 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目联系人信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_contract
-- ----------------------------
DROP TABLE IF EXISTS `pro_contract`;
CREATE TABLE `pro_contract`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `pt_id` int(11) NOT NULL COMMENT '项目Id',
  `ct_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '内部合同编号',
  `contractName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '合同名称',
  `ct_cash` decimal(10, 2) NOT NULL COMMENT '合同金额',
  `ct_signingDate` datetime(3) NOT NULL COMMENT '合同签订日期',
  `ct_deliveryDate` datetime(3) NOT NULL COMMENT '合同交货日期',
  `payment_collection` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '回款条款',
  `ct_attachmentPdf` int(11) NOT NULL COMMENT '合同扫描件',
  `ct_attachmentWord` int(11) NOT NULL COMMENT '合同可编辑文件',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `ct_attachment` int(11) NOT NULL DEFAULT 0 COMMENT '合同附件',
  `ctTaxRate` decimal(10, 2) NOT NULL COMMENT '合同税率',
  `ExternalCt_code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '外部合同编号',
  `pt_idsTxt` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '项目Id文本',
  `pt_codesTxt` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProCount` int(11) NOT NULL COMMENT '项目数量',
  `ProUnitPrice` decimal(10, 2) NOT NULL COMMENT '项目单价',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 65 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '合同信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_contract_files
-- ----------------------------
DROP TABLE IF EXISTS `pro_contract_files`;
CREATE TABLE `pro_contract_files`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PID` int(11) NOT NULL COMMENT '父级ID',
  `ContentType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件类型',
  `FileName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件名称',
  `FileSize` bigint(20) NOT NULL COMMENT '文件大小',
  `RootPath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件根目录',
  `RelativePath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '文件相对路径',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 319 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '附件实体' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_delivery
-- ----------------------------
DROP TABLE IF EXISTS `pro_delivery`;
CREATE TABLE `pro_delivery`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeliveryCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '交付信息单据号',
  `ProjectID` int(11) NOT NULL COMMENT '项目编号',
  `JHJHDate` datetime(3) NOT NULL COMMENT '计划交货日期',
  `SJJHDate` datetime(3) NOT NULL COMMENT '实际交货日期',
  `JHYSDate` datetime(3) NOT NULL COMMENT '计划验收日期',
  `SJYSDate` datetime(3) NOT NULL COMMENT '实际验收日期',
  `AcceptancePhase` int(11) NOT NULL COMMENT '验收阶段',
  `IsZYS` int(11) NOT NULL COMMENT '是否终验收',
  `AcceptanceCertificate` int(11) NOT NULL COMMENT '验收凭证',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 52 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目交付信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_delivery_file
-- ----------------------------
DROP TABLE IF EXISTS `pro_delivery_file`;
CREATE TABLE `pro_delivery_file`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeliveryID` int(11) NOT NULL COMMENT '项目交付id',
  `ProjectID` int(11) NOT NULL COMMENT '项目id',
  `DeviceID` int(11) NOT NULL COMMENT '设备id',
  `DeliveryFileId` int(11) NOT NULL COMMENT '交付id',
  `DeliveryType` int(11) NOT NULL COMMENT '文件类别',
  `Qty` int(11) NOT NULL COMMENT '数量',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 29 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目交付信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_device_details
-- ----------------------------
DROP TABLE IF EXISTS `pro_device_details`;
CREATE TABLE `pro_device_details`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeviceNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备编号',
  `DeviceName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备名称',
  `ProjectID` int(11) NOT NULL COMMENT '项目信息ID',
  `ProductLine` int(11) NOT NULL COMMENT '产品系列',
  `Qty` int(11) NOT NULL COMMENT '数量',
  `DeviceUnit` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '单位',
  `DevicePrice` decimal(10, 2) NOT NULL COMMENT '单价',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注--设备主要功能',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `DevicePlaceOfUse` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备使用地点',
  `DeliverDate` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '要求交付日期',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 47 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目设备信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_doc_management
-- ----------------------------
DROP TABLE IF EXISTS `pro_doc_management`;
CREATE TABLE `pro_doc_management`  (
  `ProjectCN` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编号_项目名称',
  `FolderAddressSC` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '生产员项目文件夹地址',
  `FolderAddressGL` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '管理员项目文件夹地址'
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目文档地址存放表格' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_dynamic
-- ----------------------------
DROP TABLE IF EXISTS `pro_dynamic`;
CREATE TABLE `pro_dynamic`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `dyType` int(11) NOT NULL COMMENT '动态类型',
  `projectID` int(11) NOT NULL COMMENT '项目ID',
  `operationContent` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作内容',
  `attr1` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'attr1',
  `remark` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 127 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目动态' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_invoicing
-- ----------------------------
DROP TABLE IF EXISTS `pro_invoicing`;
CREATE TABLE `pro_invoicing`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `InvoiceNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '开票编号',
  `ProjectId` int(11) NOT NULL COMMENT '项目id',
  `ContactId` int(11) NOT NULL COMMENT '合同id',
  `InvoiceDate` datetime(3) NOT NULL COMMENT '开票日期',
  `InvoiceAmount` decimal(10, 2) NOT NULL COMMENT '开票金额',
  `InvoiceTaxRate` decimal(10, 2) NOT NULL COMMENT '开票税率',
  `ApplyMan` int(11) NOT NULL COMMENT '开票申请人',
  `IsCredit` bit(1) NOT NULL COMMENT '开票是否挂账',
  `CreditDate` datetime(3) NOT NULL COMMENT '挂账日期',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `AmounExcludingTax` decimal(10, 2) NOT NULL COMMENT '不含税金额',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 49 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '合同信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_payment_collection
-- ----------------------------
DROP TABLE IF EXISTS `pro_payment_collection`;
CREATE TABLE `pro_payment_collection`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Pt_Id` int(11) NOT NULL COMMENT '项目编号',
  `Payment_Time` datetime(3) NOT NULL COMMENT '开票日期',
  `Payment_Cash` decimal(10, 2) NOT NULL COMMENT '已回款总额',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `ReturnType` int(11) NOT NULL COMMENT '回款类型',
  `ReturnWay` int(11) NOT NULL COMMENT '回款方式',
  `ReturnDate` datetime(3) NOT NULL COMMENT '回款日期',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 120 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目回款' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_payment_collection_plan
-- ----------------------------
DROP TABLE IF EXISTS `pro_payment_collection_plan`;
CREATE TABLE `pro_payment_collection_plan`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Ct_ID` int(11) NOT NULL,
  `PaymentCTypeID` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PaymentCProportion` decimal(10, 2) NOT NULL,
  `ConAmountCPlan` decimal(10, 2) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Edit` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 183 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_plan
-- ----------------------------
DROP TABLE IF EXISTS `pro_plan`;
CREATE TABLE `pro_plan`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PlanId` int(11) NOT NULL COMMENT '计划ID',
  `WBSVersion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'WBS版本号',
  `Role` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '角色',
  `Owner` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '责任人',
  `Auditor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核人',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编码',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名称',
  `Type` int(11) NOT NULL COMMENT '计划类型',
  `IsMainPlan` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '是否主计划',
  `Duration` decimal(10, 2) NOT NULL COMMENT '工期(计划工期)',
  `ActualDuration` decimal(10, 2) NOT NULL COMMENT '实际工期',
  `PlanManHour` int(11) NOT NULL COMMENT '计划工时',
  `ActualManHour` int(11) NOT NULL COMMENT '实际工时',
  `WorkingHours` decimal(10, 2) NOT NULL COMMENT '标准工时',
  `Progress` int(11) NOT NULL COMMENT '进度',
  `PlanStartDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划开始时间',
  `PlanEndDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划结束时间',
  `ActualStart` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '实际开始时间',
  `ActualFinish` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '实际结束时间',
  `BaselineStart` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '基线开始',
  `BaselineFinish` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '基线结束',
  `FluctuateTime` int(11) NOT NULL COMMENT '浮动时间',
  `PlanState` int(11) NOT NULL COMMENT '状态',
  `AssigneeIds` int(11) NOT NULL COMMENT '任务分配的用户ID',
  `Order` int(11) NOT NULL COMMENT '排序',
  `ParentId` int(11) NOT NULL COMMENT '父级ID',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目计划' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_plan_logs
-- ----------------------------
DROP TABLE IF EXISTS `pro_plan_logs`;
CREATE TABLE `pro_plan_logs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `PlanId` int(11) NOT NULL COMMENT '计划ID',
  `PlanName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名称',
  `TaskId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务Id',
  `TaskName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务名称',
  `LogAction` enum('Unknow','Add','Delete','Update','UpdateFiled','Clear') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '计划动作类型',
  `LogActionSource` enum('Unknow','Plan','Task','Document') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '计划动作对象',
  `Content` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '操作内容\r\n            如：\r\n            【管理员】 导入了【计划模板】的【计划】，共【5】个任务。\r\n            【管理员】将任务【任务名称】移动到索引【1】\r\n            【管理员】添加了任务【任务名称】\r\n            \r\n            后期考虑加入链接快速链接到相关信息，前期只考虑文字日志。',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 34 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '计划的行为日志，任何操作都应该被记录在此信息表中' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_plan_tasks
-- ----------------------------
DROP TABLE IF EXISTS `pro_plan_tasks`;
CREATE TABLE `pro_plan_tasks`  (
  `Id` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '主键',
  `PlanId` int(11) NOT NULL COMMENT '计划ID',
  `WBSVersion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'WBS版本号',
  `IsMilestone` bit(1) NOT NULL COMMENT '是否里程碑',
  `Role` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '角色',
  `Owner` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '责任人',
  `Auditor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核人',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编码',
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名称',
  `Type` int(11) NOT NULL COMMENT '计划类型',
  `IsMainPlan` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '是否主计划',
  `Duration` decimal(10, 2) NOT NULL COMMENT '工期(计划工期)',
  `ActualDuration` decimal(10, 2) NOT NULL COMMENT '实际工期',
  `PlanManHour` int(11) NOT NULL COMMENT '计划工时',
  `ActualManHour` int(11) NOT NULL COMMENT '实际工时',
  `WorkingHours` decimal(10, 2) NOT NULL COMMENT '标准工时',
  `Progress` int(11) NOT NULL COMMENT '进度',
  `PlanStartDate` datetime(3) NOT NULL COMMENT '计划开始时间',
  `PlanEndDate` datetime(3) NULL DEFAULT NULL COMMENT '计划结束时间',
  `ActualStart` datetime(3) NULL DEFAULT NULL COMMENT '实际开始时间',
  `ActualFinish` datetime(3) NULL DEFAULT NULL COMMENT '实际结束时间',
  `BaselineStart` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '基线开始',
  `BaselineFinish` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '基线结束',
  `FluctuateTime` int(11) NOT NULL COMMENT '浮动时间',
  `PlanState` int(11) NOT NULL COMMENT '状态',
  `AssigneeIds` int(11) NOT NULL COMMENT '任务分配的用户ID',
  `ParentId` char(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '父级ID',
  `OrderIndex` int(11) NOT NULL COMMENT '排序号',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目计划' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_planname
-- ----------------------------
DROP TABLE IF EXISTS `pro_planname`;
CREATE TABLE `pro_planname`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编码',
  `PlanName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名',
  `PersonLiable` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '责任人',
  `Order` int(11) NOT NULL COMMENT '排序号',
  `IsMainPlan` int(11) NOT NULL COMMENT '是否主计划',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目名信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_plans
-- ----------------------------
DROP TABLE IF EXISTS `pro_plans`;
CREATE TABLE `pro_plans`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编码',
  `PlanName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名',
  `PersonLiable` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '责任人',
  `OrderIndex` int(11) NOT NULL COMMENT '排序号',
  `IsMainPlan` int(11) NOT NULL COMMENT '是否主计划',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `PlanDate` datetime(3) NOT NULL DEFAULT '2023-02-08 11:35:07.088' COMMENT '计划日期',
  `CalendarMode` enum('Normal','Round','Single','OddWeek','EvenWeek','Custom') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'Normal' COMMENT '日历模式',
  `PlanStatus` enum('Changging','Published','Destroied') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'Changging' COMMENT '计划状态',
  `VersionNum` int(11) NOT NULL DEFAULT 0 COMMENT '计划的版本号',
  `IsAutoAps` bit(1) NOT NULL DEFAULT b'1' COMMENT '是否自动排程',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目名信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_problem_feedback
-- ----------------------------
DROP TABLE IF EXISTS `pro_problem_feedback`;
CREATE TABLE `pro_problem_feedback`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `pt_ID` int(11) NOT NULL,
  `ProblemTypeID` int(11) NOT NULL,
  `pfb_ExceptionDesc` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  `addressId` int(11) NOT NULL,
  `FeedbackTime` datetime(3) NOT NULL,
  `EstimatedSettlementDate` datetime(3) NOT NULL,
  `SolutionId` int(11) NOT NULL,
  `ActualSettlementDate` datetime(3) NOT NULL,
  `CauseAnalysis` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Improvement` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `InfluenceDate` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DealWithDynamic` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `ReasonType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProblemDescription` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PlanStartTime` datetime(3) NOT NULL,
  `PlanEndTime` datetime(3) NOT NULL,
  `IsQualified` int(11) NOT NULL,
  `UnqualifiedReason` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Source` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 332 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_problem_feedback_detail
-- ----------------------------
DROP TABLE IF EXISTS `pro_problem_feedback_detail`;
CREATE TABLE `pro_problem_feedback_detail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `ContentType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 534 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_ret_money
-- ----------------------------
DROP TABLE IF EXISTS `pro_ret_money`;
CREATE TABLE `pro_ret_money`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProjectID` int(11) NOT NULL COMMENT '项目编号',
  `RetMoneyProportion` decimal(10, 2) NOT NULL COMMENT '质保金比例',
  `RetMoneyAmount` decimal(10, 2) NOT NULL COMMENT '质保金金额',
  `WarrantyPeriod` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '质保期限',
  `ExpirationDate` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '质保到期日',
  `AlrExpirationMoney` decimal(10, 2) NOT NULL COMMENT '已到期质保金金额',
  `Remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 76 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目质保金信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_stage
-- ----------------------------
DROP TABLE IF EXISTS `pro_stage`;
CREATE TABLE `pro_stage`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TemplateId` int(11) NOT NULL COMMENT '项目模板ID',
  `Sort` int(11) NOT NULL COMMENT '排序',
  `StageName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '阶段名',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '配置阶段表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_template
-- ----------------------------
DROP TABLE IF EXISTS `pro_template`;
CREATE TABLE `pro_template`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板名称',
  `ProjectType` int(11) NOT NULL COMMENT '项目类型',
  `Attr1` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'attr1',
  `Attr2` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'attr2',
  `State` bit(1) NOT NULL COMMENT '状态 1启用  0停用',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目模板信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_template_step
-- ----------------------------
DROP TABLE IF EXISTS `pro_template_step`;
CREATE TABLE `pro_template_step`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TemplateId` int(11) NOT NULL COMMENT '项目模板ID',
  `StepName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '阶段名称',
  `OrderIndex` int(11) NOT NULL COMMENT '排序号',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '项目模块的阶段配置' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_wbsplan
-- ----------------------------
DROP TABLE IF EXISTS `pro_wbsplan`;
CREATE TABLE `pro_wbsplan`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TemplateId` int(11) NOT NULL COMMENT '计划ID',
  `WbsName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务',
  `Milestone` int(11) NOT NULL COMMENT '里程碑（0否 1是）',
  `Duration` decimal(10, 2) NOT NULL COMMENT '工期',
  `WorkingHours` decimal(10, 2) NOT NULL COMMENT '标准工时',
  `CorrelationStage` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '关联阶段',
  `DutyRole` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '责任角色',
  `TaskType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务类型',
  `Auditor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '审核人',
  `State` int(11) NOT NULL COMMENT '状态',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  `Sort` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  `ParentId` int(11) NOT NULL DEFAULT 0 COMMENT '父级ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 28 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = 'WBS计划信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for pro_wbstabs
-- ----------------------------
DROP TABLE IF EXISTS `pro_wbstabs`;
CREATE TABLE `pro_wbstabs`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `TemplateId` int(11) NOT NULL COMMENT '项目模板ID',
  `PlanName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '计划名称',
  `State` int(11) NOT NULL COMMENT '状态',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = 'WBS主表名' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for problem_msg_send
-- ----------------------------
DROP TABLE IF EXISTS `problem_msg_send`;
CREATE TABLE `problem_msg_send`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ProblemId` int(11) NOT NULL COMMENT '问题反馈ID',
  `Toa` datetime(3) NULL DEFAULT NULL COMMENT '到达处理人员的时间',
  `Content` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '发送消息内容',
  `TxdTime` datetime(3) NULL DEFAULT NULL COMMENT '发送消息时间',
  `Mobile` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '手机号：消息发送用户ID',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编号',
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目名称',
  `IsToa` int(11) NOT NULL COMMENT '是否已发送消息0:否,1是',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '问题反馈消息发送记录信息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for project_delivery
-- ----------------------------
DROP TABLE IF EXISTS `project_delivery`;
CREATE TABLE `project_delivery`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `DeliveryCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectID` int(11) NOT NULL,
  `JHJHDate` datetime(3) NOT NULL,
  `SJJHDate` datetime(3) NOT NULL,
  `JHYSDate` datetime(3) NOT NULL,
  `SJYSDate` datetime(3) NOT NULL,
  `AcceptancePhase` int(11) NOT NULL,
  `IsZYS` int(11) NOT NULL,
  `AcceptanceCertificate` int(11) NOT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for projectbasics
-- ----------------------------
DROP TABLE IF EXISTS `projectbasics`;
CREATE TABLE `projectbasics`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `InitialProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectClass` int(11) NOT NULL,
  `ProjectStatus` int(11) NOT NULL,
  `BusinessType` int(11) NOT NULL,
  `UrgentGrade` int(11) NOT NULL,
  `ClientCompany` int(11) NOT NULL,
  `OwnerUnitID` int(11) NOT NULL,
  `ProjectReceiveDate` datetime(3) NOT NULL,
  `ContractDeliveryDate` datetime(3) NOT NULL,
  `ExpectedUseDate` datetime(3) NOT NULL,
  `ProjectBackground` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectSummary` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IsContract` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IsTechnicalAgreement` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BiddingNo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProManagementStyle` int(11) NOT NULL,
  `ProState` int(11) NOT NULL,
  `Attr1` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Attr2` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Attr3` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Attr4` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `TechnicalAgreementPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `SalesPrice` decimal(10, 2) NOT NULL,
  `ContractID` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for projectproblemfeedback
-- ----------------------------
DROP TABLE IF EXISTS `projectproblemfeedback`;
CREATE TABLE `projectproblemfeedback`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `pt_ID` int(11) NOT NULL,
  `ProblemTypeID` int(11) NOT NULL,
  `pfb_ExceptionDesc` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  `addressId` int(11) NOT NULL,
  `FeedbackTime` datetime(3) NOT NULL,
  `EstimatedSettlementDate` datetime(3) NOT NULL,
  `SolutionId` int(11) NOT NULL,
  `ActualSettlementDate` datetime(3) NOT NULL,
  `CauseAnalysis` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Improvement` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `InfluenceDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DealWithDynamic` int(11) NOT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ReasonType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for projectproblemfeedbackdetail
-- ----------------------------
DROP TABLE IF EXISTS `projectproblemfeedbackdetail`;
CREATE TABLE `projectproblemfeedbackdetail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for projectretmoney
-- ----------------------------
DROP TABLE IF EXISTS `projectretmoney`;
CREATE TABLE `projectretmoney`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProjectID` int(11) NOT NULL,
  `RetMoneyProportion` decimal(10, 2) NOT NULL,
  `RetMoneyAmount` decimal(10, 2) NOT NULL,
  `WarrantyPeriod` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ExpirationDate` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AlrExpirationMoney` decimal(10, 2) NOT NULL,
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qr_code
-- ----------------------------
DROP TABLE IF EXISTS `qr_code`;
CREATE TABLE `qr_code`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `QrCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Supplier` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MaterialCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Count` int(11) NOT NULL,
  `State` int(11) NOT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `qualified` int(11) NOT NULL,
  `BomName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BomId` int(11) NOT NULL,
  `MaterialName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 280 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for qr_code_pic
-- ----------------------------
DROP TABLE IF EXISTS `qr_code_pic`;
CREATE TABLE `qr_code_pic`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `PID` int(11) NOT NULL,
  `ContentType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 125 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  `BtnRolesCheckedList` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for rolemodule
-- ----------------------------
DROP TABLE IF EXISTS `rolemodule`;
CREATE TABLE `rolemodule`  (
  `RoleId` int(11) NOT NULL,
  `ModuleId` int(11) NOT NULL,
  PRIMARY KEY (`RoleId`, `ModuleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for roleoperation
-- ----------------------------
DROP TABLE IF EXISTS `roleoperation`;
CREATE TABLE `roleoperation`  (
  `RoleId` int(11) NOT NULL,
  `OperationId` int(11) NOT NULL,
  PRIMARY KEY (`RoleId`, `OperationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for schedulelog
-- ----------------------------
DROP TABLE IF EXISTS `schedulelog`;
CREATE TABLE `schedulelog`  (
  `Id` int(11) NOT NULL COMMENT '主键ID',
  `DetailGuid` int(11) NULL DEFAULT NULL COMMENT '计划明细GUID',
  `OperationId` int(11) NULL DEFAULT NULL COMMENT '工序ID',
  `OperationName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工序名称',
  `OperationParentId` int(11) NULL DEFAULT NULL COMMENT '父工序ID',
  `OriginalTasker` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '原主体责任人',
  `OriginalPlanStartDay` datetime(3) NULL DEFAULT NULL COMMENT '原计划开始时间',
  `OriginalPlanFinishDay` datetime(3) NULL DEFAULT NULL COMMENT '原计划完成时间',
  `OriginalBeforeTaskID` int(11) NULL DEFAULT NULL COMMENT '原前置任务ID',
  `OriginalAfterTaskID` int(11) NULL DEFAULT NULL COMMENT '原后续任务ID',
  `CurrentTasker` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '现主体责任人',
  `CurrentPlanStartDay` datetime(3) NULL DEFAULT NULL COMMENT '现计划开始时间',
  `CurrentPlanFinishDay` datetime(3) NOT NULL COMMENT '现计划完成时间',
  `CurrentBeforeTaskID` int(11) NULL DEFAULT NULL COMMENT '现前置任务ID',
  `CurrentAfterTaskID` int(11) NULL DEFAULT NULL COMMENT '现后续任务ID',
  `ChangeReason` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '变更原因',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sprayingamount
-- ----------------------------
DROP TABLE IF EXISTS `sprayingamount`;
CREATE TABLE `sprayingamount`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `oAxleModel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `sprayingParameter` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stat_data
-- ----------------------------
DROP TABLE IF EXISTS `stat_data`;
CREATE TABLE `stat_data`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime(3) NOT NULL,
  `Type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Key` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Value` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 255 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_basedata
-- ----------------------------
DROP TABLE IF EXISTS `sys_basedata`;
CREATE TABLE `sys_basedata`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `bdtypeID` int(11) NULL DEFAULT NULL,
  `bdtypeCode` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bdcode` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bdname` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bdvalue` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `bdParentID` int(11) NULL DEFAULT NULL,
  `isDeleted` int(11) NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 375 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_conf
-- ----------------------------
DROP TABLE IF EXISTS `sys_conf`;
CREATE TABLE `sys_conf`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ConfigType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ConfigCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ConfigValue` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 17 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_config
-- ----------------------------
DROP TABLE IF EXISTS `sys_config`;
CREATE TABLE `sys_config`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `cfg_code` varchar(150) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `cfg_value` longtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `desp` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `isDeleted` int(11) NULL DEFAULT NULL,
  `remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1039 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_dict
-- ----------------------------
DROP TABLE IF EXISTS `sys_dict`;
CREATE TABLE `sys_dict`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `DictionaryText` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DictionaryValue` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 375 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_file
-- ----------------------------
DROP TABLE IF EXISTS `sys_file`;
CREATE TABLE `sys_file`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ContentType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FileSize` bigint(20) NOT NULL,
  `RootPath` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RelativePath` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 130 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_log`;
CREATE TABLE `sys_log`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Account` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Result` bit(1) NOT NULL,
  `ExecuteResult` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Desc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Ip` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Datetime` datetime(3) NOT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2023 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_logalarm
-- ----------------------------
DROP TABLE IF EXISTS `sys_logalarm`;
CREATE TABLE `sys_logalarm`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `excuteResult` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '执行结果',
  `description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `datetime` datetime NULL DEFAULT NULL COMMENT '日志时间',
  `tagname` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标记名称',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1288 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_menu
-- ----------------------------
DROP TABLE IF EXISTS `sys_menu`;
CREATE TABLE `sys_menu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Icon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Target` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Path` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Component` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModuleType` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModuleCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Hidden` bit(1) NOT NULL,
  `Required` bit(1) NOT NULL,
  `ParentId` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `OrderIndex` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 116 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_notice
-- ----------------------------
DROP TABLE IF EXISTS `sys_notice`;
CREATE TABLE `sys_notice`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `ReadCount` int(11) NOT NULL,
  `Status` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Type` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Users` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_operation
-- ----------------------------
DROP TABLE IF EXISTS `sys_operation`;
CREATE TABLE `sys_operation`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ModuleId` int(11) NOT NULL,
  `OperationCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 266 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_organization
-- ----------------------------
DROP TABLE IF EXISTS `sys_organization`;
CREATE TABLE `sys_organization`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NOT NULL,
  `OrganizationName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizationCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrganizationType` int(11) NOT NULL,
  `Sort` int(11) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Leader` int(11) NULL DEFAULT NULL,
  `OrderIndex` int(11) NOT NULL,
  `OrgNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 73 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_pointinfo
-- ----------------------------
DROP TABLE IF EXISTS `sys_pointinfo`;
CREATE TABLE `sys_pointinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TagName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ExplainInfo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 123 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_post
-- ----------------------------
DROP TABLE IF EXISTS `sys_post`;
CREATE TABLE `sys_post`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderIndex` int(11) NOT NULL,
  `PostName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrgId` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Desc` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `PostNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 191 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `BtnRolesCheckedList` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 82 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_role_module
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_module`;
CREATE TABLE `sys_role_module`  (
  `RoleId` int(11) NOT NULL,
  `ModuleId` int(11) NOT NULL,
  PRIMARY KEY (`RoleId`, `ModuleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_role_operation
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_operation`;
CREATE TABLE `sys_role_operation`  (
  `RoleId` int(11) NOT NULL,
  `OperationId` int(11) NOT NULL,
  PRIMARY KEY (`RoleId`, `OperationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_user_extender
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_extender`;
CREATE TABLE `sys_user_extender`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `Sex` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Department` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Telnum` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Born` datetime(3) NULL DEFAULT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Avatar` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 436 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_user_operation
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_operation`;
CREATE TABLE `sys_user_operation`  (
  `UserId` int(11) NOT NULL,
  `OperationCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '操作编码',
  `OperationId` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`UserId`, `OperationCode`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_user_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`  (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_users
-- ----------------------------
DROP TABLE IF EXISTS `sys_users`;
CREATE TABLE `sys_users`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrgId` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `LastLogin` datetime(3) NULL DEFAULT NULL,
  `CurrentLoginTime` datetime(3) NULL DEFAULT NULL,
  `LoginCount` int(11) NOT NULL,
  `OrderIndex` int(11) NOT NULL,
  `PostId` int(11) NOT NULL,
  `UserNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 407 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_users_bak
-- ----------------------------
DROP TABLE IF EXISTS `sys_users_bak`;
CREATE TABLE `sys_users_bak`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrgId` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `LastLogin` datetime(3) NULL DEFAULT NULL,
  `CurrentLoginTime` datetime(3) NULL DEFAULT NULL,
  `LoginCount` int(11) NOT NULL,
  `OrderIndex` int(11) NOT NULL,
  `PostId` int(11) NOT NULL,
  `UserNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 255 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for taskplan
-- ----------------------------
DROP TABLE IF EXISTS `taskplan`;
CREATE TABLE `taskplan`  (
  `Id` int(11) NOT NULL COMMENT '计划主任务id',
  `PlanCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '计划单据号',
  `ProjectId` int(11) NULL DEFAULT NULL COMMENT '项目Id',
  `RequiredDeliveryDate` datetime(3) NULL DEFAULT NULL COMMENT '要求交付日期',
  `EquipmentCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '设备编号',
  `ProductionSeries` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '生产系列',
  `Taskmode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '任务模式',
  `Tasker` int(11) NULL DEFAULT NULL COMMENT '主体负责人',
  `PlanStartDay` datetime(3) NULL DEFAULT NULL COMMENT '计划开始时间',
  `PlanFinishDay` datetime(3) NULL DEFAULT NULL COMMENT '计划完成时间',
  `ActualStartDay` datetime(3) NULL DEFAULT NULL COMMENT '实际开始时间',
  `ActualFinishDay` datetime(3) NULL DEFAULT NULL COMMENT '实际结束时间',
  `TaskStatus` int(11) NULL DEFAULT NULL COMMENT '任务状态',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for taskplandetail
-- ----------------------------
DROP TABLE IF EXISTS `taskplandetail`;
CREATE TABLE `taskplandetail`  (
  `Id` int(11) NOT NULL COMMENT '任务计划明细表id',
  `TaskPlanId` int(11) NULL DEFAULT NULL COMMENT '任务计划主表id',
  `processInfoId` int(11) NULL DEFAULT NULL COMMENT '工艺信息表id',
  `TaskCategoryId` int(11) NULL DEFAULT NULL COMMENT '工序名称',
  `TaskDays` int(11) NULL DEFAULT NULL COMMENT '工期（天）',
  `TaskHours` int(11) NULL DEFAULT NULL COMMENT '工时（小时）',
  `BeforeTaskId` int(11) NULL DEFAULT NULL COMMENT '前置任务id',
  `AfterTaskId` int(11) NULL DEFAULT NULL COMMENT '后续任务id',
  `TaskCost` decimal(10, 2) NULL DEFAULT NULL COMMENT '人工成本',
  `TaskStatus` int(11) NULL DEFAULT NULL COMMENT '子任务状态',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `OrgId` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 73 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for user_quick
-- ----------------------------
DROP TABLE IF EXISTS `user_quick`;
CREATE TABLE `user_quick`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Icon` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Color` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `Link` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `OrderIndex` int(11) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for userextender
-- ----------------------------
DROP TABLE IF EXISTS `userextender`;
CREATE TABLE `userextender`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `Sex` varchar(2) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Department` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Telnum` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Avator` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Avatar` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Born` datetime(3) NOT NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 68 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for userrole
-- ----------------------------
DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole`  (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wbsplandto
-- ----------------------------
DROP TABLE IF EXISTS `wbsplandto`;
CREATE TABLE `wbsplandto`  (
  `Id` int(11) NOT NULL,
  `TemplateId` int(11) NOT NULL,
  `WbsName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Milestone` int(11) NOT NULL,
  `Duration` decimal(10, 2) NOT NULL,
  `WorkingHours` decimal(10, 2) NOT NULL,
  `CorrelationStage` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `DutyRole` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `TaskType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Auditor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Sort` int(11) NOT NULL,
  `State` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wbstabsdto
-- ----------------------------
DROP TABLE IF EXISTS `wbstabsdto`;
CREATE TABLE `wbstabsdto`  (
  `Id` int(11) NOT NULL,
  `TemplateId` int(11) NOT NULL,
  `PlanName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `State` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wf_main
-- ----------------------------
DROP TABLE IF EXISTS `wf_main`;
CREATE TABLE `wf_main`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Enabled` bit(1) NOT NULL,
  `WorkFlowData` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wf_user_data
-- ----------------------------
DROP TABLE IF EXISTS `wf_user_data`;
CREATE TABLE `wf_user_data`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Type` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `WorkflowId` int(11) NOT NULL,
  `FlowData` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `SN` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Status` enum('Saved','Approving','Countersigning','Completed','Closed') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `AuditUserId` int(11) NOT NULL,
  `AuditUsername` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `OverTime` datetime(3) NULL DEFAULT NULL,
  `CurrentNode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AllowUserSend` bit(1) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wf_user_node
-- ----------------------------
DROP TABLE IF EXISTS `wf_user_node`;
CREATE TABLE `wf_user_node`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NodeId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `UserFlowId` int(11) NOT NULL,
  `NodeName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NodeType` enum('Default','Promoter','Approver','Send','Condition','Completed') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ExamineMode` enum('OneByOne','AllOf','AnyOne') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Handler` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `UserIds` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AuditUserIds` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NotifyUserIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `NextNodeId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NextType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Status` enum('Default','Completed','Approving','Reject','Skip','Canceled') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Timeout` int(11) NOT NULL,
  `TimeoutHandle` bit(1) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `CreatedAt` datetime(3) NOT NULL,
  `LastModifiedBy` int(11) NOT NULL,
  `LastModifiedAt` datetime(3) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wh_detail
-- ----------------------------
DROP TABLE IF EXISTS `wh_detail`;
CREATE TABLE `wh_detail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` int(11) NOT NULL COMMENT '用户Id',
  `ProjectCode` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目编号',
  `ProjectName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '项目名称',
  `JobContent` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '工作内容',
  `TaskContent` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务内容',
  `Location` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '工作地点',
  `OffsiteLocation` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '厂外地点',
  `CompleteStatus` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '完成状态 0已完成 1未完成',
  `Duration` decimal(10, 2) NOT NULL COMMENT '工作时长',
  `WorkOvertimeDuration` decimal(10, 2) NOT NULL COMMENT '加班时长',
  `CreatedBy` int(11) NOT NULL COMMENT '创建人',
  `CreatedAt` datetime(3) NOT NULL COMMENT '创建时间',
  `LastModifiedBy` int(11) NOT NULL COMMENT '最后修改人',
  `LastModifiedAt` datetime(3) NOT NULL COMMENT '最后修改时间',
  `IsDeleted` bit(1) NOT NULL COMMENT '是否删除',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '工时明细' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
