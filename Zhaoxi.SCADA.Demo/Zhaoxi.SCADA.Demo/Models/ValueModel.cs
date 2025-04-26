using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADA.Demo.Base;

namespace Zhaoxi.SCADA.Demo.Models
{
    public class ValueModel : NotifyPropertyBase
    {
        public string Header { get; set; }
        public string Unit { get; set; }


        public string Address { get; set; }

        private double _value;

        public double Value
        {
            get { return _value; }
            set
            {
                SetProperty<double>(ref _value, value);
            }
        }
        private string _stateColor;

        public string StateColor
        {
            get { return _stateColor; }
            set { SetProperty<string>(ref _stateColor, value); }
        }
    }
}
