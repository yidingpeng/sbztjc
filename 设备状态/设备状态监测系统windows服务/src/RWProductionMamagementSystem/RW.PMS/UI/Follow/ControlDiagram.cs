using LiveCharts;
using LiveCharts.Wpf;
using RW.PMS.Common;
using RW.PMS.IBLL;
using RW.PMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RW.PMS.WinForm.UI.Follow
{
    public partial class ControlDiagram : Form
    {
        public ChartValues<double> ChartValues { get; set; }
        public ChartValues<double> ChartValues2 { get; set; }
        public ChartValues<double> ChartValues3 { get; set; }
        private static IBLL_BaseInfo BLL = DIService.GetService<IBLL_BaseInfo>();
        
        public ControlDiagram()
        {
            InitializeComponent();
        }

        private void ControlDiagram_Leave(object sender, EventArgs e)
        {

        }

        private void ControlDiagram_Load(object sender, EventArgs e)
        {
            ChartValues = new ChartValues<double>();
            ChartValues2 = new ChartValues<double>();
          
            ChartValues3 = new ChartValues<double>();
            cartesianChart1.Series.Add(new LineSeries
            {
                Title = "控制下限",
                Values =ChartValues,
                StrokeThickness = 4,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(107, 185, 69)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0,
                PointGeometrySize = 0
            });
            cartesianChart1.Series.Add(new LineSeries
            {
                Title = "扭矩",
                Values = ChartValues2,
                StrokeThickness = 2,
               Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(28, 142, 196)),
                 Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 1,
                PointGeometrySize = 15,
                PointForeground =
                  new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49))
            }) ;
            cartesianChart1.Series.Add(new LineSeries
            {
                Title = "控制上限",
                Values = ChartValues3,
                StrokeThickness = 4,
                 
                  StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(107, 185, 69)),
                Fill = System.Windows.Media.Brushes.Transparent,
                LineSmoothness = 0,
                PointGeometrySize = 0
            });
            cartesianChart1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(34, 46, 49));
            cartesianChart1.DefaultLegend.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255,255,255));
            cartesianChart1.AxisX.Add(new Axis
            {
                IsMerged = true,
                Separator = new Separator
                {
                    StrokeThickness = 1,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                IsMerged = true,
                Separator = new Separator
                {
                    StrokeThickness = 1.5,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 4 }),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86))
                }
            });
            cartesianChart1.LegendLocation = LegendLocation.Right;
            List<TighteningRecordModel> list = BLL.GetTighteningRecord(new TighteningRecordModel());
            
            List<double> list1 = new List<double>();
            List<double> list2 = new List<double>();
            List<double> list3 = new List<double>();

            list.ForEach(x=>{ list1.Add(0); });
            list.ForEach(x => { list2.Add(x.TorqueValue.ToDouble()); });
            list.ForEach(x => { list3.Add(100); });
            ChartValues.AddRange(list1);
            ChartValues2.AddRange(list2);
            ChartValues3.AddRange(list3);
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
