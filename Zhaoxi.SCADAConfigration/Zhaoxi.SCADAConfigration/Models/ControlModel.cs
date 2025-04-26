using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADAConfigration.Base;

namespace Zhaoxi.SCADAConfigration.Models
{
    public class ControlModel : NotifyPropertyBase
    {
        public string Header { get; set; }

        private bool _switchState;

        public bool SwitchState
        {
            get { return _switchState; }
            set { SetProperty<bool>(ref _switchState, value); }
        }

    }
}
