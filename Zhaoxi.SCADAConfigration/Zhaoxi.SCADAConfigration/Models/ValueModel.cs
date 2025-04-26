using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADAConfigration.Base;

namespace Zhaoxi.SCADAConfigration.Models
{
    public class ValueModel : NotifyPropertyBase
    {
        public string Header { get; set; }
        public string Unit { get; set; }
        private double _value;

        public double Value
        {
            get { return _value; }
            set { SetProperty<double>(ref _value, value); }
        }

        public string StateColor { get; set; }
    }
}
