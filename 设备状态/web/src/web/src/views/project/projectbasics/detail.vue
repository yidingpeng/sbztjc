<template>
  <div class="detail-container">
    <el-page-header
      :content="'【' + ProListData.projectName + '】详情页面'"
      @back="goBack"
    />
    <el-collapse v-model="CollactiveNames">
      <el-collapse-item name="1" title="【基础信息】">
        <div>
          <el-descriptions border :column="3">
            <el-descriptions-item width="200px">
              <template #label>项目名称</template>
              {{ ProListData.projectName }}
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>项目编号</template>
              {{ ProListData.projectCode }}
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>潜项编号</template>
              {{ ProListData.initialProjectCode }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>客户公司</template>
              {{ ProListData.clientCompanyName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>业主名称</template>
              {{ ProListData.ownerUnit }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>项目分类</template>
              {{ ProListData.projectClassName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>业务类型</template>
              {{ ProListData.businessTypeName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>紧急程度</template>
              <el-rate v-model="ProListData.urgentGrade" disabled />
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>项目接收日</template>
              {{ ProListData.projectReceiveDates }}
            </el-descriptions-item>
            <!-- <el-descriptions-item>
              <template #label>合同交货日</template>
              {{ ProListData.contractDeliveryDates }}
            </el-descriptions-item> -->
            <el-descriptions-item>
              <template #label>预计使用日</template>
              {{ ProListData.expectedUseDates }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>有无技术协议</template>
              {{ ProListData.isTechnicalAgreement }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>有无合同</template>
              {{ ProListData.isContract }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>管理方式</template>
              {{ ProListData.proManagementStyleName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>招标编号</template>
              {{ ProListData.biddingNo }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>图纸代号</template>
              {{ ProListData.attr1 }}
            </el-descriptions-item>
            <!-- <el-descriptions-item>
              <template #label>合同名称</template>
              {{ ProListData.contractName }}
            </el-descriptions-item> -->
            <el-descriptions-item>
              <template #label>阶段</template>
              <el-tag :type="statusFilter(ProListData.proStateName)">
                {{ ProListData.proStateName }}
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>项目状态</template>
              {{ ProListData.projectStatusName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>产线类型</template>
              {{ ProListData.proLine }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>营销经理</template>
              {{ ProListData.personInChargeName }}
            </el-descriptions-item>
            <el-descriptions-item span="2">
              <template #label>总价</template>
              {{ ProListData.salesPrice }}
            </el-descriptions-item>
            <el-descriptions-item span="3">
              <template #label>项目背景</template>
              {{ ProListData.projectBackground }}
            </el-descriptions-item>
            <el-descriptions-item span="3">
              <template #label>项目概括</template>
              {{ ProListData.projectSummary }}
            </el-descriptions-item>
            <el-descriptions-item span="3">
              <template #label>备注</template>
              {{ ProListData.remark }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="2" title="【销售数据信息】">
        <div>
          <el-descriptions border :column="3">
            <el-descriptions-item>
              <template #label>项目总金额</template>
              {{ ProListData.salesPrice }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>项目未签合同金额</template>
              {{ ProListData.proSignedCtAmount }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>合同应开票总金额</template>
              {{ ProListData.ctInvoicedAmount }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>合同未开票金额</template>
              {{ ProListData.ctUnInvoicedAmount }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>已回款总金额</template>
              {{ ProListData.amountPaymentCollection }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>项目已回款比（%）</template>
              {{ ProListData.proPaymentCollectionRatio }}%
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>质保金比例（%）</template>
              {{ ProListData.retentionRatio }}%
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>质保金金额</template>
              {{ ProListData.retentionAmount }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>已到期质保金金额</template>
              {{ ProListData.periodMoney }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="3" title="【客户信息】">
        <div>
          <el-descriptions border :column="3">
            <el-descriptions-item width="200px">
              <template #label>单位编号</template>
              <el-button link type="primary" @click="handleClientRouter()">
                {{
                  ProClientList == undefined ? '' : ProClientList.companyCode
                }}
              </el-button>
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>客户简称</template>
              {{ ProClientList == undefined ? '' : ProClientList.clientName }}
            </el-descriptions-item>
            <el-descriptions-item width="200px">
              <template #label>客户全称</template>
              {{
                ProClientList == undefined ? '' : ProClientList.clientFullName
              }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>市场片区</template>
              {{
                ProClientList == undefined ? '' : ProClientList.marketAreaTxt
              }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>地址</template>
              {{ ProClientList == undefined ? '' : ProClientList.address }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>客户级别</template>
              {{
                ProClientList == undefined ? '' : ProClientList.clientRankText
              }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>合作业务</template>
              {{
                ProClientList == undefined
                  ? ''
                  : ProClientList.cooperativeBusinessText
              }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司董事长</template>
              {{ ProClientList == undefined ? '' : ProClientList.ceoName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司总经理</template>
              {{ ProClientList == undefined ? '' : ProClientList.gmName }}
            </el-descriptions-item>
            <el-descriptions-item>
              <template #label>公司副总经理</template>
              {{ ProClientList == undefined ? '' : ProClientList.deputyGMName }}
            </el-descriptions-item>
            <el-descriptions-item span="3">
              <template #label>备注</template>
              {{ ProClientList == undefined ? '' : ProClientList.remark }}
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="4" title="【项目进度】">
        <div>
          <el-descriptions :column="3" style="margin-top: 20px" title="">
            <el-descriptions-item>
              <el-steps
                :active="num"
                align-center
                finish-status="success"
                process-status="finish"
              >
                <el-step
                  v-for="item in ProStateData"
                  :key="item.value"
                  :title="item.key"
                />
                <!-- <el-step description="Some description" title="待签订合同" />
                <el-step description="Some description" title="待项目启动" />
                <el-step description="Some description" title="生产制造中" />
                <el-step description="Some description" title="已发货" />
                <el-step description="Some description" title="待验收" />
                <el-step description="Some description" title="终验收" />
                <el-step description="Some description" title="开口项验收" />
                <el-step description="Some description" title="质保期" /> -->
              </el-steps>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
      <el-collapse-item name="5" title="【立项总览】">
        <div>
          <el-descriptions :column="3">
            <el-descriptions-item>
              <el-tabs v-model="activeName" class="demo-tabs" type="card">
                <el-tab-pane label="项目设备信息" name="one">
                  <el-table
                    v-loading="listLoading"
                    :data="ProDeviceList"
                    highlight-current-row
                    @row-dblclick="ProDeviceDoubleClic"
                  >
                    <el-table-column
                      align="center"
                      label="设备编号"
                      prop="deviceNo"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="设备名称"
                      prop="deviceName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="产品系列"
                      prop="productLineName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="数量"
                      prop="qty"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="单位"
                      prop="deviceUnit"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="单价"
                      prop="devicePriceShow"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="设备主要功能"
                      prop="remark"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="合计（元）"
                      prop="totalShow"
                      show-overflow-tooltip
                    />
                  </el-table>
                  <div
                    style="margin-top: 5px; color: darkblue; text-align: right"
                  >
                    总合计：{{
                      ProDevicePriceTotal
                    }}元&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </div>
                </el-tab-pane>
                <el-tab-pane label="项目团队" name="two">
                  <el-button
                    :icon="Plus"
                    style="margin-bottom: 10px"
                    type="primary"
                    @click="ProTeamhandleEdit"
                  >
                    添加
                  </el-button>
                  <el-table
                    v-loading="listLoading"
                    :data="ProContactsList"
                    @row-dblclick="ProContactsDoubleClic"
                  >
                    <el-table-column
                      align="center"
                      label="联系人姓名"
                      prop="contactsName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="部门"
                      prop="deptName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="性别"
                      prop="sex"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="联系人电话"
                      prop="telephone1"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="项目角色"
                      prop="fzbkName"
                      show-overflow-tooltip
                    />
                    <el-table-column align="center" label="操作" width="170">
                      <template #default="{ row }">
                        <el-link
                          v-permissions="{
                            permission: ['projectcontacts:edit'],
                          }"
                          title="编辑"
                          :underline="false"
                          @click="ProTeamhandleEdit(row)"
                        >
                          <vab-icon icon="edit-2-line" />
                        </el-link>
                      </template>
                    </el-table-column>
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="合同信息" name="three">
                  <div
                    v-permissions="{
                      permission: ['prodetail:ContPane'],
                      mode: 'except',
                    }"
                  >
                    <h2 class="NopermissionTxt">无权限</h2>
                  </div>
                  <div v-permissions="{ permission: ['prodetail:ContPane'] }">
                    <el-table
                      v-loading="listLoading"
                      :data="ProContractList"
                      @row-dblclick="ProContractDoubleClic"
                    >
                      <el-table-column
                        align="center"
                        label="合同编号"
                        prop="ct_code"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="合同名称"
                        prop="contractName"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="合同签订日期"
                        prop="ct_signingDate"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="合同扫描件"
                        prop="ct_attachmentPdfUrl"
                        show-overflow-tooltip
                      >
                        <template #default="scope">
                          <el-link
                            v-if="scope.row.ct_attachmentPdfUrl.length > 0"
                            target="_blank"
                            @click="
                              downloadFile(
                                scope.row.ct_attachmentPdfUrl,
                                scope.row.filePdfName
                              )
                            "
                          >
                            点击查看
                          </el-link>
                        </template>
                      </el-table-column>
                      <el-table-column
                        align="center"
                        label="合同可编辑件"
                        prop="ct_attachmentWordUrl"
                        show-overflow-tooltip
                      >
                        <template #default="scope">
                          <el-link
                            v-if="scope.row.ct_attachmentWordUrl.length > 0"
                            target="_blank"
                            @click="
                              downloadFile(
                                scope.row.ct_attachmentWordUrl,
                                scope.row.fileWordName
                              )
                            "
                          >
                            下载查看
                          </el-link>
                        </template>
                      </el-table-column>
                      <el-table-column
                        align="center"
                        label="合同金额"
                        prop="ct_cash"
                        show-overflow-tooltip
                      />
                    </el-table>
                    <div
                      style="
                        margin-top: 5px;
                        color: darkblue;
                        text-align: right;
                      "
                    >
                      总合计：{{ ContractInfoPriceTotal }}元
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                  </div>
                </el-tab-pane>
                <el-tab-pane label="项目质保金" name="four">
                  <div
                    v-permissions="{
                      permission: ['prodetail:MoneyPane'],
                      mode: 'except',
                    }"
                  >
                    <h2 class="NopermissionTxt">无权限</h2>
                  </div>
                  <div v-permissions="{ permission: ['prodetail:MoneyPane'] }">
                    <el-table
                      v-loading="listLoading"
                      :data="ProMoneyList"
                      @row-dblclick="ProRetMoneyDoubleClic"
                    >
                      <el-table-column
                        align="center"
                        label="质保金比例（%）"
                        prop="retMoneyProportion"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="质保金金额"
                        prop="retMoneyAmount"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="质保期限（月）"
                        prop="expirationDate"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="质保期范围"
                        prop="warrantyPeriod"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="已到期质保金金额"
                        prop="alrExpirationMoney"
                        show-overflow-tooltip
                      />
                    </el-table>
                  </div>
                </el-tab-pane>
                <el-tab-pane label="项目开票明细信息" name="five">
                  <div
                    v-permissions="{
                      permission: ['prodetail:InvoicingPane'],
                      mode: 'except',
                    }"
                  >
                    <h2 class="NopermissionTxt">无权限</h2>
                  </div>
                  <div
                    v-permissions="{ permission: ['prodetail:InvoicingPane'] }"
                  >
                    <el-button
                      :icon="Plus"
                      style="margin-bottom: 10px"
                      type="primary"
                      @click="ProInvoicinghandleEdit"
                    >
                      添加
                    </el-button>
                    <el-table
                      v-loading="listLoading"
                      :data="ProInvoicingList"
                      @row-dblclick="ProRetMoneyDoubleClic"
                    >
                      <el-table-column
                        align="center"
                        label="开票编号"
                        prop="invoiceNo"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="开票日期"
                        prop="invoiceDate"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="开票申请人"
                        prop="applyManTxt"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="开票税率"
                        prop="invoiceTaxRate"
                        show-overflow-tooltip
                      >
                        <template #default="scope">
                          <span v-if="scope.row.invoiceTaxRate == 0"></span>
                          <span v-else>{{ scope.row.invoiceTaxRate }}</span>
                        </template>
                      </el-table-column>
                      <el-table-column
                        align="center"
                        label="开票是否挂账"
                        prop="isCredit"
                        show-overflow-tooltip
                      >
                        <template #default="scope">
                          <span v-if="scope.row.isCredit == 1">是</span>
                          <span v-if="scope.row.isCredit == 0">否</span>
                        </template>
                      </el-table-column>
                      <el-table-column
                        align="center"
                        label="挂账日期"
                        prop="creditDate"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="开票金额"
                        prop="invoiceAmountShow"
                        show-overflow-tooltip
                      />
                      <el-table-column align="center" label="操作" width="170">
                        <template #default="{ row }">
                          <el-link
                            v-permissions="{
                              permission: ['projectinvoicing:edit'],
                            }"
                            title="编辑"
                            :underline="false"
                            @click="ProInvoicinghandleEdit(row)"
                          >
                            <vab-icon icon="edit-2-line" />
                          </el-link>
                        </template>
                      </el-table-column>
                    </el-table>
                    <div
                      style="
                        margin-top: 5px;
                        color: darkblue;
                        text-align: right;
                      "
                    >
                      总合计：{{ PriInvoicingPriceTotal }}元
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                  </div>
                </el-tab-pane>
                <el-tab-pane label="项目交付" name="six">
                  <el-table
                    v-loading="listLoading"
                    :data="ProDeliveryList"
                    @row-dblclick="ProDeliveryDoubleClic"
                  >
                    <el-table-column
                      align="center"
                      label="交付单据号"
                      prop="deliveryCode"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="计划交付日期"
                      prop="jhjhDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="实际交付日期"
                      prop="sjjhDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="计划验收日期"
                      prop="jhysDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="实际验收日期"
                      prop="sjysDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="验收凭证"
                      prop="acceptanceCertificate"
                      show-overflow-tooltip
                    >
                      <template #default="scope">
                        <el-link
                          v-if="scope.row.acceptanceCertificate != 0"
                          @click="
                            downloadFile2(
                              scope.row.acceptanceCertificateUrl,
                              scope.row.fileName
                            )
                          "
                        >
                          下载查看
                        </el-link>
                        <span v-else></span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      align="center"
                      label="验收阶段"
                      prop="acceptancePhaseName"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="项目交付文件" name="seven">
                  <el-table
                    v-loading="listLoading"
                    :data="ProDeliveryFileList"
                    @row-dblclick="ProDeliveryFileDoubleClic"
                  >
                    <!-- <el-table-column
                      align="center"
                      label="项目编号"
                      prop="projectCode"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="项目名称"
                      prop="projectName"
                      show-overflow-tooltip
                    /> -->
                    <el-table-column
                      align="center"
                      label="项目交付信息单据号"
                      prop="deliveryNo"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="设备编号"
                      prop="deviceCode"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="文件类别"
                      prop="deliveryTypeText"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="数量"
                      prop="qty"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="备注"
                      prop="remark"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="项目验收" name="eight">
                  <el-table
                    v-loading="listLoading"
                    :data="ProAcceptanceList"
                    @row-dblclick="ProAcceptDoubleClic"
                  >
                    <el-table-column
                      align="center"
                      label="设备编号"
                      prop="deviceNo"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="设备名称"
                      prop="deviceName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="验收类别"
                      prop="acceptCategoryName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="验收日期"
                      prop="acceptDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="验收人员"
                      prop="acceptancerName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="数量"
                      prop="qty"
                      show-overflow-tooltip
                    >
                      <template #default="scope">
                        <span v-if="scope.row.qty == 0"></span>
                        <span v-else>{{ scope.row.qty }}</span>
                      </template>
                    </el-table-column>
                    <el-table-column
                      align="center"
                      label="竣工日期"
                      prop="completedDate"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="验收结论"
                      prop="acceptResultTxt"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="最终整改完成日期"
                      prop="finalAbarbeitungDate"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
                <el-tab-pane label="项目回款" name="nine">
                  <div
                    v-permissions="{
                      permission: ['prodetail:PaymentPane'],
                      mode: 'except',
                    }"
                  >
                    <h2 class="NopermissionTxt">无权限</h2>
                  </div>
                  <div
                    v-permissions="{ permission: ['prodetail:PaymentPane'] }"
                  >
                    <el-button
                      :icon="Plus"
                      style="margin-bottom: 10px"
                      type="primary"
                      @click="PaymenthandleEdit"
                    >
                      添加
                    </el-button>
                    <el-table
                      v-loading="listLoading"
                      :data="ProPaymentList"
                      @row-dblclick="ProPaymentDoubleClic()"
                    >
                      <el-table-column
                        align="center"
                        label="回款日期"
                        prop="returnDate"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="回款类型"
                        prop="returnTypeText"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="回款方式"
                        prop="returnWayText"
                        show-overflow-tooltip
                      />
                      <el-table-column
                        align="center"
                        label="回款金额(元)"
                        prop="payment_CashShow"
                        show-overflow-tooltip
                      />
                      <el-table-column align="center" label="操作">
                        <template #default="{ row }">
                          <el-link
                            v-permissions="{ permission: ['payment:edit'] }"
                            title="编辑"
                            :underline="false"
                            @click="PaymenthandleEdit(row)"
                          >
                            <vab-icon icon="edit-2-line" />
                          </el-link>
                        </template>
                      </el-table-column>
                    </el-table>
                    <div
                      style="
                        margin-top: 5px;
                        color: darkblue;
                        text-align: right;
                      "
                    >
                      总合计：{{ PaymentPriceTotal }}元
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                  </div>
                </el-tab-pane>
                <el-tab-pane label="项目动态" name="ten">
                  <el-table v-loading="listLoading" :data="ProDynamicList">
                    <el-table-column
                      align="center"
                      label="操作内容"
                      min-width="75%"
                      prop="operationContent"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="操作人"
                      min-width="10%"
                      prop="createdByName"
                      show-overflow-tooltip
                    />
                    <el-table-column
                      align="center"
                      label="操作时间"
                      min-width="15%"
                      prop="createdAt"
                      show-overflow-tooltip
                    />
                  </el-table>
                </el-tab-pane>
              </el-tabs>
            </el-descriptions-item>
          </el-descriptions>
        </div>
      </el-collapse-item>
    </el-collapse>
    <EditPayment ref="PaymentEditRef" @fetch-data="ProPaymentDetail" />
    <EditInvoicing ref="InvicingEditRef" @fetch-data="ProInvoicingeDetail" />
    <EditProTeam ref="TeamEditRef" @fetch-data="ProContactsFetail" />
  </div>
</template>

<script>
  import { useTabsStore } from '@/store/modules/tabs'
  import { handleActivePath } from '@/utils/routes'
  import { Refresh, Plus } from '@element-plus/icons-vue'
  import {
    GetProState,
    GetByClientList,
    GetByDeliveryList,
    ContractDetailList,
    PaymentDetailList,
    MoneyDetailList,
    GetDeviceList,
    GetDownloadFilePath,
    GetDownloadFilePath2,
    GetProInvoicingList,
    GetBydeliveryFileList,
    GetByAcceptanceList,
    GetByContactsList,
    GetDataById,
    DTgetList,
  } from '@/api/project'
  import EditPayment from '@/views/project/payment/components/PaymentEdit'
  import EditInvoicing from '@/views/project/projectinvoicing/components/ProjectProjectinvoicingEdit'
  import EditProTeam from '@/views/project/projectcontacts/components/ProjectcontactsEdit'
  import { useDictionaryStore } from '@/store/modules/dictionary'

  import { ref } from 'vue'

  export default defineComponent({
    name: 'Detail',
    components: { EditPayment, EditInvoicing, EditProTeam },
    setup() {
      const CollactiveNames = ref(['1', '2', '3', '4', '5'])
      const activeName = ref('one')
      const route = useRoute()
      const router = useRouter()

      const $pub = inject('$pub')

      const tabsStore = useTabsStore()
      const { changeTabsMeta, delVisitedRoute } = tabsStore

      const state = reactive({
        PaymentEditRef: null,
        InvicingEditRef: null,
        TeamEditRef: null,
        route: { query: { title: '加载中' } },
        rate: 0,
        form: {
          text: '',
        },
        num: 0,
        url: '',
        title: '加载中',
        ProListData: [],
        ProStateOption: [],
        ProClientList: [],
        ProContractList: [],
        ProDeliveryList: [],
        ProPaymentList: [],
        ProMoneyList: [],
        ProDeviceList: [],
        ProInvoicingList: [],
        ProDeliveryFileList: [],
        ProAcceptanceList: [],
        ProContactsList: [],
        ProDynamicList: [],
        ProDevicePriceTotal: 0,
        ContractInfoPriceTotal: 0,
        PaymentPriceTotal: 0,
        PriInvoicingPriceTotal: 0,
        ProStateData: null,
        listLoading: true,
      })
      //获取项目信息
      const getProInfo = async () => {
        const listData = await GetDataById({ Id: route.query.id })
        listData.data.forEach((item) => {
          item.retentionRatio = parseFloat(item.retentionRatio).toFixed(2)
          item.proPaymentCollectionRatio = parseFloat(
            item.proPaymentCollectionRatio
          ).toFixed(2)
          item.salesPrice = formatCurrency(item.salesPrice)
          item.proSignedCtAmount = formatCurrency(item.proSignedCtAmount)
          item.ctInvoicedAmount = formatCurrency(item.ctInvoicedAmount)
          item.ctUnInvoicedAmount = formatCurrency(item.ctUnInvoicedAmount)
          item.retentionAmount = formatCurrency(item.retentionAmount)
          item.periodMoney = formatCurrency(item.periodMoney)
          item.amountPaymentCollection = formatCurrency(
            item.amountPaymentCollection
          )
        })
        state.ProListData = listData.data[0]
        //activeNum(state.ProListData.proState)
        const dictStore = useDictionaryStore()
        const proline = dictStore.getKey('ProLine', state.ProListData.proLine)
        state.ProListData.proLine = proline
        getProstate()
      }
      //金额转换为千分位
      const formatCurrency = (value, str) => {
        if (!str) str = ''
        // str 规定货币类型
        if (value == '0') return '0.00'
        if (value == '.') return ''
        if (!value) return ''
        let val = Number(value).toFixed(2) // 提前保留两位小数
        const intPart = parseInt(val) // 获取整数部分
        const intPartFormat = intPart
          .toString()
          .replace(/(\d)(?=(?:\d{3})+$)/g, '$1,') // 将整数部分逢三一断 ？？？
        let floatPart = '.00' // 预定义小数部分
        val = val.toString() // 将number类型转为字符串类型，方便操作
        const value2Array = val.split('.')
        if (value2Array.length === 2) {
          // =2表示数据有小数位
          floatPart = value2Array[1].toString() // 拿到小数部分
          if (floatPart.length === 1) {
            // 补0,实际上用不着
            return `${str + intPartFormat}.${floatPart}0`
          } else {
            return `${str + intPartFormat}.${floatPart}`
          }
        } else {
          return str + intPartFormat + floatPart
        }
      }
      const goBack = async () => {
        const detailPath = handleActivePath(route, true)
        await router.push('/project/projectbasics')
        delVisitedRoute(detailPath)
      }

      const handleRefreshMainPage = () => {
        $pub('reload-router-view', 'Projectbasics')
      }
      const statusFilter = (status) => {
        const statusMap = {
          已发货: 'success',
          待验收: 'info',
          终验收: 'warning',
          开口项验收: 'danger',
          质保期: 'primary',
        }
        return statusMap[status]
      }
      //回款添加、编辑框
      const PaymenthandleEdit = (row) => {
        if (row.id) {
          state.PaymentEditRef.showEdit(row)
        } else {
          state.PaymentEditRef.showEdit(state.ProListData.id)
        }
      }
      //开票添加、编辑框
      const ProInvoicinghandleEdit = (row) => {
        if (row.id) {
          state.InvicingEditRef.showEdit(row)
        } else {
          state.InvicingEditRef.showEdit(state.ProListData.id)
        }
      }

      //项目团队添加、编辑框
      const ProTeamhandleEdit = (row) => {
        if (row.id) {
          state.TeamEditRef.showEdit(row)
        } else {
          state.TeamEditRef.showEdit(state.ProListData.id)
        }
      }
      //ProStateOption
      //状态
      const GetProStateData = async () => {
        const ProStateList = await GetProState()
        state.ProStateOption = ProStateList.data
      }
      //客户信息
      const ProClientDetail = async () => {
        const ProClientList = await GetByClientList({
          Id: route.query.clientCompany,
        })
        state.ProClientList = ProClientList.data[0]
      }
      //跳转到客户信息页面
      const handleClientRouter = () => {
        router.push({
          path: '/project/client',
        })
      }
      const changeDate = (val) => {
        if (val == '0001-01-01 00:00:00') {
          return null
        }
        const newDate = new Date(val)
        const y = newDate.getFullYear()
        const m =
          newDate.getMonth() + 1 < 10
            ? `0${newDate.getMonth() + 1}`
            : newDate.getMonth() + 1
        const d =
          newDate.getDate() < 10 ? `0${newDate.getDate()}` : newDate.getDate()
        return `${y}-${m}-${d}`
      }
      //合同信息
      const ProContractDetail = async () => {
        state.ContractInfoPriceTotal = 0
        const ContractList = await ContractDetailList({
          Id: route.query.id,
        })
        state.ProContractList = ContractList.data
        state.ProContractList.forEach((item) => {
          item.ct_signingDate = changeDate(item.ct_signingDate)
          state.ContractInfoPriceTotal += item.ct_cash
          item.ct_cash = formatCurrency(item.ct_cash)
        })
        state.ContractInfoPriceTotal = formatCurrency(
          state.ContractInfoPriceTotal
        )
        //console.log(ProContractList.data)
      }
      //交付信息
      const ProDeliveryDetail = async () => {
        const List = await GetByDeliveryList({
          Id: route.query.id,
        })
        state.ProDeliveryList = List.data
        state.ProDeliveryList.forEach((item) => {
          item.jhjhDate = changeDate(item.jhjhDate)
          item.sjjhDate = changeDate(item.sjjhDate)
          item.jhysDate = changeDate(item.jhysDate)
          item.sjysDate = changeDate(item.sjysDate)
        })
        //console.log(List.data)
      }
      //PaymentDetailList
      //回款信息
      const ProPaymentDetail = async () => {
        state.PaymentPriceTotal = 0
        const List = await PaymentDetailList({
          Id: route.query.id,
        })
        state.ProPaymentList = List.data
        state.ProPaymentList.forEach((item) => {
          item.ct_signingDate = changeDate(item.ct_signingDate)
          state.PaymentPriceTotal += item.payment_Cash
          item.payment_CashShow = formatCurrency(item.payment_Cash)
        })
        state.PaymentPriceTotal = formatCurrency(state.PaymentPriceTotal)
        //getProInfo()
        //console.log(List.data)
      }
      //MoneyDetailList
      //质保金信息
      const ProMoneyDetail = async () => {
        const List = await MoneyDetailList({
          Id: route.query.id,
        })
        state.ProMoneyList = List.data
        state.ProMoneyList.forEach((item) => {
          item.retMoneyAmount = formatCurrency(item.retMoneyAmount)
          item.alrExpirationMoney = formatCurrency(item.alrExpirationMoney)
        })
        //console.log(List.data)
      }
      //项目设备
      const ProDeviceDetail = async () => {
        state.listLoading = true
        state.ProDevicePriceTotal = 0
        const List = await GetDeviceList({
          Id: route.query.id,
        })
        state.ProDeviceList = List.data
        state.ProDeviceList.forEach((item) => {
          item.totalShow = formatCurrency(item.devicePrice * item.qty)
          item.total = item.devicePrice * item.qty
          state.ProDevicePriceTotal += item.total
          item.devicePriceShow = formatCurrency(item.devicePrice)
        })
        state.ProDevicePriceTotal = formatCurrency(state.ProDevicePriceTotal)
        state.listLoading = false
        //console.log(List.data)ContractInfoPriceTotal
      }
      //项目开票
      const ProInvoicingeDetail = async () => {
        state.listLoading = true
        state.PriInvoicingPriceTotal = 0
        const List = await GetProInvoicingList({
          Id: route.query.id,
        })
        state.ProInvoicingList = List.data
        state.ProInvoicingList.forEach((item) => {
          item.creditDate = changeDate(item.creditDate)
          state.PriInvoicingPriceTotal += item.invoiceAmount
          item.invoiceDate = changeDate(item.invoiceDate)
          item.ct_InvoicedAmount = parseFloat(item.ct_InvoicedAmount).toFixed(2)
          item.invoiceAmountShow = formatCurrency(item.invoiceAmount)
        })
        state.PriInvoicingPriceTotal = formatCurrency(
          state.PriInvoicingPriceTotal
        )
        //getProInfo()
        state.listLoading = false
      }
      //项目交付文件
      const ProDeliveryFileDetail = async () => {
        state.listLoading = true
        const List = await GetBydeliveryFileList({
          Id: route.query.id,
        })
        state.ProDeliveryFileList = List.data
        state.listLoading = false
      }
      //项目验收
      const ProAccepranceDetail = async () => {
        state.listLoading = true
        const List = await GetByAcceptanceList({ Id: route.query.id })
        state.ProAcceptanceList = List.data
        state.ProAcceptanceList.forEach((item) => {
          item.acceptDate = changeDate(item.acceptDate)
          item.completedDate = changeDate(item.completedDate)
          item.finalAbarbeitungDate = changeDate(item.finalAbarbeitungDate)
        })
        state.listLoading = false
      }
      //项目联系人
      const ProContactsFetail = async () => {
        state.listLoading = true
        const List = await GetByContactsList({ Id: route.query.id })
        state.ProContactsList = List.data
        state.listLoading = false
      }
      //项目动态
      const ProDynamicDetail = async () => {
        state.listLoading = true
        const List = await DTgetList({ Id: route.query.id })
        state.ProDynamicList = List.data
        //console.log(List.data)
        state.listLoading = false
      }
      //项目设备表格双击事件
      const ProDeviceDoubleClic = () => {
        router.push({
          path: '/project/projectdevice',
        })
      }
      //合同信息表格双击事件
      const ProContractDoubleClic = () => {
        router.push({
          path: '/project/contract',
        })
      }
      //项目交付表格双击事件
      const ProDeliveryDoubleClic = () => {
        router.push({
          path: '/project/projectdelivery',
        })
      }
      //项目回款表格双击事件
      const ProPaymentDoubleClic = () => {
        router.push({
          path: '/project/payment',
        })
      }
      //项目质保金表格双击事件
      const ProRetMoneyDoubleClic = () => {
        router.push({
          path: '/project/project/projectretmoney',
        })
      }
      //项目开票明细表格双击事件
      const ProInvoicingDoubleClic = () => {
        router.push({
          path: '/project/projectinvoicing',
        })
      }
      //项目交付文件表格双击事件
      const ProDeliveryFileDoubleClic = () => {
        router.push({
          path: '/project/delivefile',
        })
      }
      //项目验收表格双击事件
      const ProAcceptDoubleClic = () => {
        router.push({
          path: '/project/acceptance',
        })
      }
      //项目联系人表格双击事件
      const ProContactsDoubleClic = () => {
        router.push({
          path: '/project/projectcontacts',
        })
      }

      //获取数据字典项目进度
      const getProstate = () => {
        const dictStore = useDictionaryStore()
        const result = dictStore.getByType('ProState')
        state.ProStateData = result
        //给里程碑控件赋值需要到达的值
        for (let i = 0; i < state.ProStateData.length; i++) {
          if (state.ProStateData[i].key == state.ProListData.proStateName) {
            //将获取的value值减一赋值到num变量上
            state.num = i
          }
        }
      }

      // const activeNum = (proStateShow) => {
      //   if (proStateShow == 169) {
      //     state.num = 0
      //   } else if (proStateShow == 170) {
      //     state.num = 1
      //   } else if (proStateShow == 171) {
      //     state.num = 2
      //   } else if (proStateShow == 45) {
      //     state.num = 3
      //   } else if (proStateShow == 46) {
      //     state.num = 4
      //   } else if (proStateShow == 47) {
      //     state.num = 5
      //   } else if (proStateShow == 48) {
      //     state.num = 6
      //   } else {
      //     state.num = 7
      //   }
      // }
      const fetchInsertFileData = async () => {
        state.url = await GetDownloadFilePath()
      }
      const downloadFile = async (FileUrl, FileName) => {
        //fetchInsertFileData()
        state.url = await GetDownloadFilePath()
        for (let i = 0; i < FileUrl.length; i++) {
          window.open(
            `${state.url}?fileUrl=${FileUrl[i]}&fileName=${FileName[i]}`
          )
        }
      }
      const downloadFile2 = async (FileUrl, FileName) => {
        state.url = await GetDownloadFilePath2()
        window.open(`${state.url}?fileUrl=${FileUrl}&fileName=${FileName}`)
      }
      onMounted(() => {
        changeTabsMeta({
          title: '详情页',
        })
        state.title = route.query.title
        state.route = {
          path: route.path,
          params: route.params,
          query: route.query,
          name: route.name,
          meta: route.meta,
        }
      })
      onMounted(() => {
        getProInfo()
        GetProStateData()
        ProClientDetail()
        ProContractDetail()
        ProDeliveryDetail()
        ProPaymentDetail()
        ProMoneyDetail()
        ProDeviceDetail()
        ProInvoicingeDetail()
        ProDeliveryFileDetail()
        ProAccepranceDetail()
        ProContactsFetail()
        ProDynamicDetail()
      })
      return {
        ...toRefs(state),
        goBack,
        handleRefreshMainPage,
        statusFilter,
        //dateFormat,
        Refresh,
        Plus,
        ProInvoicinghandleEdit,
        PaymenthandleEdit,
        getProInfo,
        GetProStateData,
        ProClientDetail,
        ProContractDetail,
        ProInvoicingeDetail,
        downloadFile,
        downloadFile2,
        fetchInsertFileData,
        changeDate,
        ProTeamhandleEdit,
        activeName,
        CollactiveNames,
        ProDeliveryDetail,
        ProPaymentDetail,
        ProMoneyDetail,
        ProDeviceDetail,
        ProDeliveryFileDetail,
        ProAccepranceDetail,
        ProContactsFetail,
        handleClientRouter,
        ProDeviceDoubleClic,
        ProContractDoubleClic,
        ProDeliveryDoubleClic,
        ProPaymentDoubleClic,
        ProRetMoneyDoubleClic,
        ProInvoicingDoubleClic,
        ProDeliveryFileDoubleClic,
        ProAcceptDoubleClic,
        ProContactsDoubleClic,
        // activeNum,
        formatCurrency,
        ProDynamicDetail,
      }
    },
  })
</script>
<style lang="scss">
  .NopermissionTxt {
    font-family: '黑体';
    color: #cdd0d6;
    text-align: center;
  }
</style>
