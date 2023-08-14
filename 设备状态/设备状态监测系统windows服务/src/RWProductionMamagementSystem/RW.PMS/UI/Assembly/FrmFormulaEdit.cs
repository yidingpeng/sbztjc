using Newtonsoft.Json;
using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.Model.BaseInfo;
using RW.PMS.Utils;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RW.PMS.WinForm.UI.Assembly
{
    public partial class FrmFormulaEdit : Form
    {
        private string frm_actionType = "";
        private int frm_ID = 0;
        private static IBLL_BaseInfo BLL = DIService.GetService<IBLL_BaseInfo>();
        public FrmFormulaEdit(string _actionType, int ID)
        {
           
            InitializeComponent();

            frm_actionType = _actionType;
            frm_ID = ID;
            if (frm_ID != 0)
            {
                BindData();
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                if (frm_actionType == EDAEnums.ActionType.Add.ToString())
                {

                    var model = ConvertToJson();
                    string jsonstr = JsonConvert.SerializeObject(model);
                    BaseFormulaModel baseFormula = new BaseFormulaModel
                    { ID = frm_ID, formulaCode = txtFormulaCode.Text.Trim(), formulaName = txtFormulaName.Text.Trim(), pointInfoDes = jsonstr };
                    int Result = BLL.EditFormula(baseFormula);
                    if (Result > 0)
                    {
                        MessageBox.Show("保存成功！");
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (Result == -1)
                    {
                        MessageBox.Show("已存在该配方编号记录！");
                    }
                    else
                    {
                        MessageBox.Show("保存失败！");
                    }
                }
                else if (frm_actionType == EDAEnums.ActionType.Edit.ToString())
                {
                    var model = ConvertToJson();
                    string jsonstr = JsonConvert.SerializeObject(model);
                    BaseFormulaModel baseFormula = new BaseFormulaModel
                    { ID = frm_ID, formulaCode = txtFormulaCode.Text.Trim(), formulaName = txtFormulaName.Text.Trim(), pointInfoDes = jsonstr };
                    baseFormula.ID = Convert.ToInt32(frm_ID);
                    int Result = BLL.EditFormula(baseFormula);
                    if (Result > 0)
                    {
                        MessageBox.Show("修改成功！");
                        this.DialogResult = DialogResult.OK;
                    }
                    else if (Result == -1)
                    {
                        MessageBox.Show("已存在该配方编号记录！");
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
            } 
        }


        public FormulaParam ConvertToJson()
        {
            FormulaParam para = new FormulaParam
            {
                BoltsNum = txtBoltsNum.Text.Trim(),
                TighteningNum = txtTighteningNum.Text.Trim(),
                
                TightenSJSpeed = txtTightenSJSpeed.Text.Trim(),
                TightenSJReady = txtTightenSJReady.Text.Trim(),
                TightenSJFinal = txtTightenSJFinal.Text.Trim(),
                CenteringReady = txtCenteringReady.Text.Trim(),
                CenteringPosition = txtCenteringPosition.Text.Trim(),
                CenteringSpeed = txtCenteringSpeed.Text.Trim(),
                YReady = txtYReady.Text.Trim(),
                YAssembly = txtYAssembly.Text.Trim(),
                YSpeed = txtYSpeed.Text.Trim(),
                XReady = txtXReady.Text.Trim(),
                XAssembly = txtXAssembly.Text.Trim(),
                XSpeed = txtXSpeed.Text.Trim(),
                ZReady = txtZReady.Text.Trim(),
                ZAssembly = txtZAssembly.Text.Trim(),
                ZSpeed = txtZSpeed.Text.Trim(),
                OneReady = txtOneReady.Text.Trim(),
                OneFinal = txtOneFinal.Text.Trim(),
                OneSpeed = txtOneSpeed.Text.Trim(),
                TwoReady = txtTwoReady.Text.Trim(),
                TwoFinal = txtTwoFinal.Text.Trim(),
                TwoSpeed = txtTwoSpeed.Text.Trim(),
                RotateReady = txtRotateReady.Text.Trim(),
                //RotateFinal = txtRotateFinal.Text.Trim(),
                RotateSpeed = txtRotateSpeed.Text.Trim(),
                Support = txtSupport.Text.Trim(),
                SpacingAngle = txtSpacingAngle.Text.Trim(),
                ZSJInPlace = txtZSJInPlace.Text.Trim(),
                YSJprepare = txtYSJprepare.Text.Trim(),
                Zbolt = txtZbolt.Text.Trim(),
                ZboltSpeed = txtZboltSpeed.Text.Trim(),
                OneBlotPosition=txtOneBlotPosition.Text.Trim(),
            };
            return para;
        }
        
        /// <summary>
        /// 文本框输入验证
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            //foreach (Control item in this.groupBox1.Controls)
            //{
            //    if (item is TextBox)
            //    {
            //        if (string.IsNullOrEmpty(((TextBox)item).Text.Trim()))
            //        {
            //            MessageBox.Show("请输入必填文本");
            //            return false;
            //        }
            //        if (item.Name!= "txtFormulaCode"&&item.Name!= "txtFormulaName"&&item.Name!= "txtTightenSJReady"&&item.Name!= "txtRotateFinal")
            //        {
            //            //Regex r = new Regex(@"^([0-9]{1,}[.][0-9]*)$");
            //            //if (!r.IsMatch(((TextBox)item).Text.Trim()))
            //            //{
            //            //    MessageBox.Show("除配方编号、配方名称之外、旋转伺服最终位、拧紧升降变频准备位，其余信息必须填写数值！");
            //            //}
            //            //return false;
            //        }
            //    }
            //}
            return true;
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindData()
        {
            var list= BLL.GetFormula(null, frm_ID);
            txtFormulaCode.Text = list[0].formulaCode;
            txtFormulaName.Text = list[0].formulaName;
            var result = JsonConvert.DeserializeObject<FormulaParam>(list[0].pointInfoDes);
            if (result != null)
            {
                txtBoltsNum.Text = result.BoltsNum;
                txtTighteningNum.Text = result.TighteningNum;
                
                txtTightenSJSpeed.Text = result.TightenSJSpeed;
                txtTightenSJReady.Text = result.TightenSJReady;
                txtTightenSJFinal.Text = result.TightenSJFinal;
                txtCenteringReady.Text = result.CenteringReady;
                txtCenteringPosition.Text = result.CenteringPosition;
                txtCenteringSpeed.Text = result.CenteringSpeed;
                txtYReady.Text = result.YReady;
                txtYAssembly.Text = result.YAssembly;
                txtYSpeed.Text = result.YSpeed;
                txtXReady.Text = result.XReady;
                txtXAssembly.Text = result.XAssembly;
                txtXSpeed.Text = result.XSpeed;
                txtZReady.Text = result.ZReady;
                txtZAssembly.Text = result.ZAssembly;
                txtZSpeed.Text = result.ZSpeed;
                txtOneReady.Text = result.OneReady;
                txtOneFinal.Text = result.OneFinal;
                txtOneSpeed.Text = result.OneSpeed;
                txtTwoReady.Text = result.TwoReady;
                txtTwoFinal.Text = result.TwoFinal;
                txtTwoSpeed.Text = result.TwoSpeed;
                txtRotateReady.Text = result.RotateReady;
                //txtRotateFinal.Text = result.RotateFinal;
                txtRotateSpeed.Text = result.RotateSpeed;
                txtSupport.Text = result.Support;
                txtSpacingAngle.Text = result.SpacingAngle;
                txtZSJInPlace.Text = result.ZSJInPlace;

                txtYSJprepare.Text = result.YSJprepare;
                txtZbolt.Text = result.Zbolt;
                txtZboltSpeed.Text = result.ZboltSpeed;
                txtOneBlotPosition.Text = result.OneBlotPosition;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtTightenSJFinal_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmFormulaEdit_Load(object sender, EventArgs e)
        {
            if (PublicVariable.IsAdmin == false)
            {
                //  toolbtnPoint.Visible = false;
                // txtBoltsNum.Text = result.BoltsNum;
                txtTighteningNum.Enabled = false;

                txtTightenSJSpeed.Enabled = false;
                txtTightenSJReady.Enabled = false;
                txtTightenSJFinal.Enabled = false;
                txtCenteringReady.Enabled = false;
                txtCenteringPosition.Enabled = false;
                txtCenteringSpeed.Enabled = false;
                txtYReady.Enabled = false;
                txtYAssembly.Enabled = false;
                txtYSpeed.Enabled = false;
                txtXReady.Enabled = false;
                txtXAssembly.Enabled = false;
                txtXSpeed.Enabled = false;
                txtZReady.Enabled = false;
                txtZAssembly.Enabled = false;
                txtZSpeed.Enabled = false;
                txtOneReady.Enabled = false;
                txtOneFinal.Enabled = false;
                txtOneSpeed.Enabled = false;
                txtTwoReady.Enabled = false;
                txtTwoFinal.Enabled = false;
                txtTwoSpeed.Enabled = false;
                txtRotateReady.Enabled = false;
                //txtRotateFinal.Text = result.RotateFinal;
                txtRotateSpeed.Enabled = false;
                txtSupport.Enabled = false;
                txtSpacingAngle.Enabled = false;
                txtZSJInPlace.Enabled = false;

                txtYSJprepare.Enabled = false;
                txtZbolt.Enabled = false;
                txtZboltSpeed.Enabled = false;
                txtOneBlotPosition.Enabled = false;
            }
        }

        private void txtTightenSJSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {

                e.Handled = true;
                MessageBox.Show("请输入数字");

            }
        }

        private void txtTightenSJSpeed_TextChanged(object sender, EventArgs e)
        {
            //if (txtTightenSJSpeed.Text == "")
            //    txtTightenSJSpeed.Text = 0.ToString();
            if (txtTightenSJSpeed.Text != "" && !txtTightenSJSpeed.Text.Equals(""))
            {
                double number=0;
                if (txtTightenSJSpeed.Text.IndexOf('.')!=0)
                {
                     number = double.Parse(txtTightenSJSpeed.Text);
                }
                var i = txtTightenSJSpeed.Text.IndexOf('.');
                var j = txtTightenSJSpeed.Text.LastIndexOf('.');
                if (txtTightenSJSpeed.Text.IndexOf('.') == txtTightenSJSpeed.Text.Length-1&& txtTightenSJSpeed.Text.IndexOf('.')!=-1)
                    txtTightenSJSpeed.Text = number.ToString()+'.';
                else
                    txtTightenSJSpeed.Text = number.ToString();
                if (number <= 100)
                {
                    return;
                }
                if (txtTightenSJSpeed.Text.Length>3)
                txtTightenSJSpeed.Text = txtTightenSJSpeed.Text.Remove(3);
                MessageBox.Show("请输入0-100以内的数字");
                txtTightenSJSpeed.SelectionStart = txtTightenSJSpeed.Text.Length;
            }
        }
    }
}
