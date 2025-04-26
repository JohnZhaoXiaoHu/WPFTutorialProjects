using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zhaoxi.SCADA.Demo.Components
{
    /// <summary>
    /// WaterTank.xaml 的交互逻辑
    /// </summary>
    public partial class WaterTank : UserControl
    {
        public double WaterHeight
        {
            get { return (double)GetValue(WaterHeightProperty); }
            set { SetValue(WaterHeightProperty, value); }
        }
        public static readonly DependencyProperty WaterHeightProperty =
            DependencyProperty.Register("WaterHeight", typeof(double), typeof(WaterTank), new PropertyMetadata(0.0));


        public WaterTank()
        {
            InitializeComponent();
        }
    }
}
