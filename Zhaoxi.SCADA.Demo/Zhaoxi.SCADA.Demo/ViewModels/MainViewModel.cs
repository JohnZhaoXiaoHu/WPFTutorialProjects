using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADA.Demo.Base;
using Zhaoxi.SCADA.Demo.Communication;
using Zhaoxi.SCADA.Demo.Models;

namespace Zhaoxi.SCADA.Demo.ViewModels
{
    public class MainViewModel
    {
        SiemensComm siemensComm = new SiemensComm();

        public List<ControlModel> ControlList { get; set; } =
            new List<ControlModel>();

        public List<ValueModel> ValueList { get; set; } =
            new List<ValueModel>();

        public Command SwitchCommand { get; set; }

        public MainViewModel()
        {
            ControlList.Add(new ControlModel() { DeviceName = "进水阀门", OpenState = true });
            ControlList.Add(new ControlModel() { DeviceName = "浓排阀门", OpenState = true });
            ControlList.Add(new ControlModel() { DeviceName = "原水泵机", OpenState = true });
            ControlList.Add(new ControlModel() { DeviceName = "高压泵机", OpenState = true });
            ControlList.Add(new ControlModel() { DeviceName = "阻垢剂", OpenState = true });
            ControlList.Add(new ControlModel() { DeviceName = "紫外线", OpenState = true });

            ValueList.Add(new ValueModel { Header = "进水流量", Value = 2.34, Unit = "L/h", Address = "DB1.DBW100", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水流量", Value = 6.71, Unit = "L/h", Address = "DB1.DBW102", StateColor = "#FF7D53" });
            ValueList.Add(new ValueModel { Header = "膜前压力", Value = 8.62, Unit = "MPa", Address = "DB1.DBW104", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "浓水压力", Value = 10.21, Unit = "MPa", Address = "DB1.DBW106", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水", Value = 10.98, Unit = "Ph", Address = "DB1.DBW108", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水电导", Value = 2.34, Unit = "us/cm", Address = "DB1.DBW110", StateColor = "#FF7D53" });
            ValueList.Add(new ValueModel { Header = "产水库", Value = 6.51, Unit = "", Address = "DB1.DBW112", StateColor = "#FF7D53" });

            SwitchCommand = new Command(OnSwitch);

            siemensComm.Open();
            ReadEntity re = new ReadEntity
            {
                DataType = S7.Net.DataType.DataBlock,
                StartAddr = 99,
                VarType = S7.Net.VarType.Byte,
                Count = 15
            };
            siemensComm.Monitor(re, obj =>
            {
                var values = obj as byte[];

                // 从第0个字节读取状态
                for (int i = 0; i < 6; i++)
                {
                    ControlList[i].OpenState = (values[0] & (1 << i)) > 0;
                }

                for (int i = 1; i < values.Length; i += 2)
                {
                    var value = BitConverter.ToUInt16(new byte[] {
                        values[i+1],values[i]
                    });
                    ValueList[(i - 1) / 2].Value = Math.Round(value * 0.01, 2);
                }
            });
        }

        private void OnSwitch(object? arg)
        {
            try
            {
                var model = arg as ControlModel;
                byte value = 0;
                for (int i = 0; i < ControlList.Count; i++)
                {
                    if (ControlList[i].OpenState)
                        value ^= (byte)(1 << i);
                }
                siemensComm.Set(value);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
