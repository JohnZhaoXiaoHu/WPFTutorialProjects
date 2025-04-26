using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADA.Demo.Base;

namespace Zhaoxi.SCADA.Demo.Models
{
    public class ControlModel : NotifyPropertyBase
    {
        public string DeviceName { get; set; }

        private bool _openState;

        public bool OpenState
        {
            get { return _openState; }
            set { SetProperty<bool>(ref _openState, value); }
        }
    }
}
