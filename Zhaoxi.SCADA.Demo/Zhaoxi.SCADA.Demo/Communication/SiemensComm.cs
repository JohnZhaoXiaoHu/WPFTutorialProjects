using S7.Net.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.SCADA.Demo.Communication
{
    public class SiemensComm
    {
        S7.Net.Plc Plc;
        CancellationTokenSource cts = new CancellationTokenSource();

        public void Open()
        {
            try
            {
                Plc = new S7.Net.Plc(S7.Net.CpuType.S7200Smart, "192.168.2.1", 0, 0);
                Plc.Open();
            }
            catch
            {
                throw new Exception("PLC连接失败！请检查通信接口是否正常");
            }
        }

        public void Monitor(ReadEntity entity, Action<object> callback)
        {
            Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    int db = (entity.DataType == S7.Net.DataType.DataBlock ? 1 : 0);

                    var result = Plc.Read(entity.DataType, db, entity.StartAddr, entity.VarType, entity.Count, 0);
                    callback?.Invoke(result);

                    await Task.Delay(500);
                }
            }, cts.Token);
        }

        public void Set(object value)
        {
            if (Plc == null)
            {
                throw new Exception("请先执行Open方法");
            }

            Plc.Write(S7.Net.DataType.DataBlock, 1, 99, value);
        }

        public void Stop()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
        }
    }
}
