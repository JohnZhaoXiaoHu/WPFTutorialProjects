using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADA.Demo.Base;

namespace Zhaoxi.SCADA.Demo.Models
{
    public class DeviceModel : NotifyPropertyBase
    {
		private double _value;

		public double Value
		{
			get { return _value; }
			set { SetProperty<double>(ref _value, value); }
		}

	}
}
