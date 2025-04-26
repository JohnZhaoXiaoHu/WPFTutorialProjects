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
    /// TitleArea.xaml 的交互逻辑
    /// </summary>
    public partial class TitleArea : UserControl
    {

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(TitleArea), new PropertyMetadata(""));


        public TitleArea()
        {
            InitializeComponent();
        }
    }
}
