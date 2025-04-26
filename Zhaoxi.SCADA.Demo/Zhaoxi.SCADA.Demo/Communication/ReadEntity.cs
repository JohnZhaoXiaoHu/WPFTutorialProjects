using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.SCADA.Demo.Communication
{
    public class ReadEntity
    {
        public DataType DataType { get; set; }
        public int StartAddr { get; set; }
        public VarType VarType { get; set; }
        public int Count { get; set; }
    }
}
