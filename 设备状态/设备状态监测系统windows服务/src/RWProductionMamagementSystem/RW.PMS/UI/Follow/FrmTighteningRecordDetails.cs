using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.Model.graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RW.PMS.WinForm.UI.Follow
{
    
    public partial class FrmTighteningRecordDetails : Form
    {
        class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        private int ID;
        string prodNo;
        string pmodel;
        double torqueValue2;
        double angleValue1;
        int boltNo;
        string operatorValue;
        DateTime tighteningDate;
        string isQualified;
        IBLL_Graph graphbll = DIService.GetService<IBLL_Graph>();
        public ChartValues<double> ChartValues { get; set; }
        public ChartValues<double> ChartValues2 { get; set; }
        public FrmTighteningRecordDetails(int ID, string prodNo, string pmodel, double torqueValue2, double angleValue1, int boltNo, string operatorValue, DateTime tighteningDate, string isQualified)
        {
            InitializeComponent();
            this.ID = ID;
            this.prodNo = prodNo;
            this.pmodel= pmodel;
            this.torqueValue2= torqueValue2;
            this.angleValue1= angleValue1;
            this.boltNo = boltNo;
            this.operatorValue = operatorValue;
            this.tighteningDate = tighteningDate;
            this.isQualified = isQualified;
            this.txtprodNo.Text = prodNo;
            this.txtPmodel.Text = pmodel;
            
           
            this.txtBoltNo.Text = boltNo.ToString();
            this.txtOperatorValue.Text = operatorValue;
            this.txtTighteningDate.Text = tighteningDate.ToString();
            this.txtIsQualified.Text = isQualified;
            this.lblAngle.Text = angleValue1.ToString();
           this.lblTorqueShow.Text = torqueValue2.ToString();
           List<GraphModel> pointList= graphbll.GetGraphData(boltNo, prodNo, pmodel);
            InitCurve1();
            TorOnTick1(pointList);
        }

        private void FrmTighteningRecordDetails_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
        /// <summary>
        /// 曲线初始化
        /// </summary>
        public void InitCurve1()
        {
            var mapper = Mappers.Xy<MeasureModel>()
               .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
               .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the ChartValues property will store our values array
            ChartValues = new ChartValues<double>();
            ChartValues2 = new ChartValues<double>();
            cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title="扭矩",
                    Values = ChartValues,
                    PointGeometrySize = 8,
                    StrokeThickness = 2,
                    ScalesYAt=0
                },
                new LineSeries
                {
                    Title="角度",
                    Values = ChartValues2,
                    PointGeometrySize = 8,
                    StrokeThickness = 2,
                    ScalesYAt=1
                }
            };

            //cartesianChart1.AxisX.Add(new Axis
            //{
            //    DisableAnimations = true,
            //    LabelFormatter = value => new DateTime((long)value).ToString("mm:ss"),
            //    Separator = new Separator
            //    {
            //        Step = TimeSpan.FromSeconds(1).Ticks
            //    }
            //});

            cartesianChart1.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.DodgerBlue,
                Title = "扭矩 Nm"
            });
            cartesianChart1.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.IndianRed,
                Title = "角度 °",
                Position = AxisPosition.RightTop
            });

            // SetAxisLimits1(DateTime.Now);
        }

        private void TorOnTick1(List<GraphModel> pointList)
        {
            var dt = DateTime.Now;

            //一次添加多个
            List<double> list1 = new List<double>();
            List<double> list2 = new List<double>();

            int ii = 0;
            foreach (var item in pointList)
            {
                dt = DateTime.Now;

                list1.Add(item.torque);
                list2.Add(item.angle);
                // SetAxisLimits1(now);

                // dt.AddMilliseconds(1);

                //写入数据库
               // graphbll.AddGraphData(new GraphModel(DateTime.Now, item.qTorque, item.qAngle, "曲线一"));

            }
            ChartValues.Clear();
            ChartValues2.Clear();

            ChartValues.AddRange(list1);
            //cartesianChart1.Series[0].Values = ChartValues;


            // ChartValues.AddRange(list1);

            ChartValues2.AddRange(list2);


            //  SetAxisLimits1(dt);


            //只使用最后30个值
            //if (ChartValues.Count > 500) ChartValues.Clear();
            // if (ChartValues2.Count > 500) ChartValues2.Clear();


        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打印内容 为 局部的 this.panel1
            Bitmap _NewBitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(_NewBitmap, new Rectangle(80, 0, _NewBitmap.Width-152, _NewBitmap.Height));
            e.Graphics.DrawImage(_NewBitmap, 0, _NewBitmap.Height/3, _NewBitmap.Width, _NewBitmap.Height);

            System.Windows.Forms.Control.ControlCollection control = this.Controls;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
             PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }
    }
}
