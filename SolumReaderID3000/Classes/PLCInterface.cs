﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LModbus;

namespace SolumReaderID3000.Classes
{
    public class PLCInterface
    {
        public ModbusTCPIPMASTER LX5S;
        public bool IsTestNoPLC = false;
        public bool IsConnectPLC = false;
        private string PLCHostIP = "107.105.42.58";
        private int PLCPort = 502;
        private Thread trd;

        public PLCInterface()
        {
        }


        public void ConnectToPLC()
        {
            LX5S = new ModbusTCPIPMASTER()
            {
                ID = 1,
                Mode_TCP_Serial = false
            };
            if (!LX5S.ConnectTCP(PLCHostIP, PLCPort))
            {
                IsConnectPLC = false;
            }
            else
            {
                IsConnectPLC = true;
            }
        }

        private int ReadPLCCountException = 0;

        public string ReadDataPLC()
        {
            try
            {
                int varCount = 31;
                int[] arr = LX5S.ReadHoldingRegistersTCPIP(4096 + 350, varCount);
                if (LX5S.ExceptionCode != "")
                {
                    ReadPLCCountException++;
                    return "";
                }
                if (arr.Length == varCount)
                {
                    return arr[0].ToString();
                    //bool[] D1000 = _WordConvert.WordTo16Bit(arr[0]);
                    //bool[] D1001 = _WordConvert.WordTo16Bit(arr[1]);
                    //bool[] D1002 = _WordConvert.WordTo16Bit(arr[2]);
                    //bool[] D1003 = _WordConvert.WordTo16Bit(arr[3]);
                    //bool[] D1005 = _WordConvert.WordTo16Bit(arr[5]);
                    //bool[] D1006 = _WordConvert.WordTo16Bit(arr[6]);
                }
                else
                {
                    return "";
                }

               bool[] arr2 = LX5S.ReadCoilsTCPIP(4096+170, varCount);    
            }

            catch (Exception ex)
            {
                return "";
                //this.Invoke(new Action(() => {
                //    WriteLogPC(LogType.Main, "PLC", ex.ToString());
                //}));
            }
        }
        public void WriteDataPLC()
        {
            int[] DataWritePLC = new int[72];
            ushort NumberTransfer = (ushort)DataWritePLC.Length;
            LX5S.WriteMultipleRegisters(5596, DataWritePLC);

        }
    }
}