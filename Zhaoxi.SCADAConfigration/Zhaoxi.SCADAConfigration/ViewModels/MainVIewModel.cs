using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SCADAConfigration.Base;
using Zhaoxi.SCADAConfigration.Models;

namespace Zhaoxi.SCADAConfigration.ViewModels
{
    public class MainVIewModel
    {
        public List<ControlModel> ControlList { get; set; } =
            new List<ControlModel>();

        public List<ValueModel> ValueList { get; set; } =
            new List<ValueModel>();


        public Command SwitchCommand { get; set; }

        S7.Net.Plc siemens;
        public MainVIewModel()
        {
            // DB1.DBB99    99.0   99.1   99.2 .........99.5
            ControlList.Add(new ControlModel() { Header = "进水阀门", SwitchState = true });
            ControlList.Add(new ControlModel() { Header = "浓排阀门", SwitchState = false });
            ControlList.Add(new ControlModel() { Header = "原水泵机", SwitchState = true });
            ControlList.Add(new ControlModel() { Header = "高压泵机", SwitchState = false });
            ControlList.Add(new ControlModel() { Header = "阻垢剂", SwitchState = true });
            ControlList.Add(new ControlModel() { Header = "紫外线", SwitchState = true });


            // Word
            // DB1.DBW100  102  104 106  108  110  112
            ValueList.Add(new ValueModel { Header = "进水流量", Value = 2.34, Unit = "L/h", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水流量", Value = 6.71, Unit = "L/h", StateColor = "#FF7D53" });
            ValueList.Add(new ValueModel { Header = "膜前压力", Value = 8.62, Unit = "MPa", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "浓水压力", Value = 10.21, Unit = "MPa", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水", Value = 10.98, Unit = "Ph", StateColor = "#7ED17E" });
            ValueList.Add(new ValueModel { Header = "产水电导", Value = 2.34, Unit = "us/cm", StateColor = "#FF7D53" });
            ValueList.Add(new ValueModel { Header = "产水库", Value = 6.51, Unit = "", StateColor = "#FF7D53" });


            SwitchCommand = new Command(OnSwitch);

            // 通信方式和地址根据自己的实际情况进行配置
            // 这个库只能是S7协议使用    Modbus协议请使用其对应的库
            siemens = new S7.Net.Plc(S7.Net.CpuType.S7200Smart, "192.168.2.1", 0, 0);
            siemens.Open();

            Task.Run(async () =>
            {
                while (true)
                {
                    var result = siemens.Read(S7.Net.DataType.DataBlock, 1, 99, S7.Net.VarType.Byte, 15);
                    byte[] values = result as byte[];

                    for (int i = 0; i < 6; i++)
                    {
                        // 46  ->  2E   
                        // 2#    ->  0010  1100
                        //           0000  0100
                        //           0000  0100 >0
                        ControlList[i].SwitchState =
                            (values[0] & (1 << i)) > 0;
                    }
                    for (int i = 1; i < values.Length; i += 2)
                    {
                        var value = BitConverter.ToUInt16(new byte[] {
                            values[i+1],values[i]
                        });
                        ValueList[(i - 1) / 2].Value = value;
                    }


                    await Task.Delay(500);
                }
            });
        }

        private void OnSwitch(object? arg)
        {
            byte value = 0;
            for (int i = 0; i < ControlList.Count; i++)
            {
                if (ControlList[i].SwitchState)
                    value ^= (byte)(1 << i);
            }
            siemens.Write(S7.Net.DataType.DataBlock, 1, 99, value);
        }
    }
}
