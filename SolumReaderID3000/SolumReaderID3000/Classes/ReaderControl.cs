using MvCodeReaderSDKNet;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using MSFactoryDLL;

namespace SolumReaderID3000
{
    public class ReaderControl
    {
        public delegate void ExceptionOccur(string exception);
        public event ExceptionOccur OnExceptionOccur;

        public delegate void ImageGrabbed(Bitmap bitmap, List<string> strCode, List<MsPolygon> listROI);
        public event ImageGrabbed OnImageGrabbed;

        MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST m_stDeviceList = new MvCodeReader.MV_CODEREADER_DEVICE_INFO_LIST();
        private MvCodeReader m_cMyDevice = new MvCodeReader();

        public ReaderControl(string serialNumber)
        {
            InitReader(serialNumber);
        }

        public bool IsConnected { get; set; } = false;
        public string SerialNo { get; set; } = "02DA4115756";
        private float exposure = 0;
        public float Exposure
        {
            get => exposure;
            set
            {
                exposure = value;
                m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("ExposureAuto", 0);
                int nRet = m_cMyDevice.MV_CODEREADER_SetFloatValue_NET("ExposureTime", exposure);
                if (nRet != MvCodeReader.MV_CODEREADER_OK)
                {
                    ShowErrorMsg("Set Exposure Time Fail!", nRet);
                }
            }
        }
        private float gain = 0;
        public float Gain
        {
            get => gain;
            set
            {
                gain = value;
                m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("GainAuto", 0);
                int nRet = m_cMyDevice.MV_CODEREADER_SetFloatValue_NET("Gain", gain);
                if (nRet != MvCodeReader.MV_CODEREADER_OK)
                {
                    ShowErrorMsg("Set Gain Fail!", nRet);
                }
            }
        }
        public float FrameRate { get; set; }

        private Thread m_hReceiveThread = null;
        private bool m_bGrabbing = false;

        private void InitReader(string serialNumber)
        {

            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            int readerNo = -1;
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MvCodeReader.MV_CODEREADER_EnumDevices_NET(ref m_stDeviceList, MvCodeReader.MV_CODEREADER_GIGE_DEVICE);
            if (0 != nRet)
            {
                OnExceptionOccur?.Invoke("Enumerate devices fail!");
                return;
            }

            if (0 == m_stDeviceList.nDeviceNum)
            {
                OnExceptionOccur?.Invoke("No Device!");
                return;
            }
            // ch:在窗体列表中显示设备名 | en:Display stDevInfo name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo = (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));
                if (stDevInfo.nTLayerType == MvCodeReader.MV_CODEREADER_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(stDevInfo.SpecialInfo.stGigEInfo, 0);
                    MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO stGigEDeviceInfo = (MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MvCodeReader.MV_CODEREADER_GIGE_DEVICE_INFO));
                    if (stGigEDeviceInfo.chSerialNumber == serialNumber)
                    {
                        SerialNo = serialNumber;
                        readerNo = i;
                        break;
                    }
                }
            }
            if (readerNo >= 0)
                OpenDevice(readerNo);
            else
                OnExceptionOccur?.Invoke("No Device!");
        }

        private void OpenDevice(int index)
        {
            // ch:获取选择的设备信息 | en:Get selected stDevInfo information
            MvCodeReader.MV_CODEREADER_DEVICE_INFO stDevInfo =
                (MvCodeReader.MV_CODEREADER_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[index],
                typeof(MvCodeReader.MV_CODEREADER_DEVICE_INFO));
            // ch:打开设备 | en:Open stDevInfo
            if (null == m_cMyDevice)
            {
                m_cMyDevice = new MvCodeReader();
                if (null == m_cMyDevice)
                {
                    return;
                }
            }

            int nRet = m_cMyDevice.MV_CODEREADER_CreateHandle_NET(ref stDevInfo);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                OnExceptionOccur?.Invoke("MV_CODEREADER_CreateHandle_NET fail!");
                return;
            }

            nRet = m_cMyDevice.MV_CODEREADER_OpenDevice_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_cMyDevice.MV_CODEREADER_DestroyHandle_NET();
                OnExceptionOccur?.Invoke("Device open fail!");
                return;
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_OFF);

            ReadParams();
            IsConnected = true;
            Task.Delay(100).Wait();
            SetTriggerHardware();
            Task.Delay(100).Wait();
            StartGrab();
        }

        public void CloseDevice()
        {
            StopGrab();
            // ch:关闭设备 | en:Close Device
            m_cMyDevice.MV_CODEREADER_CloseDevice_NET();
            m_cMyDevice.MV_CODEREADER_DestroyHandle_NET();
        }

        private void ReadParams()
        {
            MvCodeReader.MV_CODEREADER_FLOATVALUE stParam = new MvCodeReader.MV_CODEREADER_FLOATVALUE();
            int nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
                exposure = stParam.fCurValue;
            else
                OnExceptionOccur?.Invoke("Get ExposureTime Fail!");

            nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("Gain", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
                gain = stParam.fCurValue;
            else
                OnExceptionOccur?.Invoke("Get Gain Fail!");

            nRet = m_cMyDevice.MV_CODEREADER_GetFloatValue_NET("AcquisitionFrameRate", ref stParam);
            if (MvCodeReader.MV_CODEREADER_OK == nRet)
                FrameRate = stParam.fCurValue;
            else
                OnExceptionOccur?.Invoke("Get FrameRate Fail!");
        }

        public void SetTriggerHardware()
        {
            int nRet = m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("TriggerMode", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_MODE.MV_CODEREADER_TRIGGER_MODE_ON);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                OnExceptionOccur?.Invoke("Set TriggerMode On Fail!");
                return;
            }

            nRet = m_cMyDevice.MV_CODEREADER_SetEnumValue_NET("TriggerSource", (uint)MvCodeReader.MV_CODEREADER_TRIGGER_SOURCE.MV_CODEREADER_TRIGGER_SOURCE_LINE0);
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                OnExceptionOccur?.Invoke("Set TriggerMode Source Line0 Fail!");
                return;

            }
        }

        public void StartGrab()
        {
            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();
            m_bGrabbing = true;

            int nRet = m_cMyDevice.MV_CODEREADER_StartGrabbing_NET();
            if (MvCodeReader.MV_CODEREADER_OK != nRet)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                ShowErrorMsg("Start Grabbing Fail!", nRet);
                return;
            }
        }

        public void StopGrab()
        {
            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;

            // ch:停止采集 | en:Stop Grabbing
            int nRet = m_cMyDevice.MV_CODEREADER_StopGrabbing_NET();
            if (nRet != MvCodeReader.MV_CODEREADER_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
            }

            if (null != m_hReceiveThread)
            {
                m_hReceiveThread.Join();
            }
        }

        Bitmap bmp = null;
        GraphicsPath OcrShapePath = new GraphicsPath();     // 图形路径，内部变量
        Matrix stRotateM = new Matrix();
        Point[] stPointList = new Point[4];                 // 条码位置的4个点坐标
        private byte[] m_BufForDriver = new byte[1024 * 1024 * 20];
        public void ReceiveThreadProcess()
        {

            int nRet = MvCodeReader.MV_CODEREADER_OK;

            IntPtr pData = IntPtr.Zero;
            MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2 stFrameInfoEx2 = new MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2();
            IntPtr pstFrameInfoEx2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)));
            Marshal.StructureToPtr(stFrameInfoEx2, pstFrameInfoEx2, false);

            while (m_bGrabbing)
            {
                nRet = m_cMyDevice.MV_CODEREADER_GetOneFrameTimeoutEx2_NET(ref pData, pstFrameInfoEx2, 1000);
                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    stFrameInfoEx2 = (MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2)Marshal.PtrToStructure(pstFrameInfoEx2, typeof(MvCodeReader.MV_CODEREADER_IMAGE_OUT_INFO_EX2));
                }

                if (nRet == MvCodeReader.MV_CODEREADER_OK)
                {
                    if (0 >= stFrameInfoEx2.nFrameLen)
                    {
                        continue;
                    }

                    // 绘制图像
                    Marshal.Copy(pData, m_BufForDriver, 0, (int)stFrameInfoEx2.nFrameLen);
                    if (stFrameInfoEx2.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Mono8)
                    {
                        IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_BufForDriver, 0);
                        bmp = new Bitmap(stFrameInfoEx2.nWidth, stFrameInfoEx2.nHeight, stFrameInfoEx2.nWidth, PixelFormat.Format8bppIndexed, pImage);
                        ColorPalette cp = bmp.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            cp.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        bmp.Palette = cp;

                    }
                    else if (stFrameInfoEx2.enPixelType == MvCodeReader.MvCodeReaderGvspPixelType.PixelType_CodeReader_Gvsp_Jpeg)
                    {
                        GC.Collect();
                        MemoryStream ms = new MemoryStream();
                        ms.Write(m_BufForDriver, 0, (int)stFrameInfoEx2.nFrameLen);
                        bmp = (Bitmap)Image.FromStream(ms);
                    }

                    MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2 stBcrResultEx2 = (MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2)Marshal.PtrToStructure(stFrameInfoEx2.UnparsedBcrList.pstCodeListEx2, typeof(MvCodeReader.MV_CODEREADER_RESULT_BCR_EX2));

                    List<string> strCodes = new List<string>();
                    List<MsPolygon> listROI = new List<MsPolygon>();

                    for (int i = 0; i < stBcrResultEx2.nCodeNum; ++i)
                    {
                        MsPolygon polygon = new MsPolygon();
                        List<PointF> pts = new List<PointF>();
                        for (int j = 0; j < 4; ++j)
                        {
                            stPointList[j].X = (int)(stBcrResultEx2.stBcrInfoEx2[i].pt[j].x * (float)(bmp.Size.Width) / stFrameInfoEx2.nWidth);
                            stPointList[j].Y = (int)(stBcrResultEx2.stBcrInfoEx2[i].pt[j].y * (float)(bmp.Size.Height) / stFrameInfoEx2.nHeight);
                            pts.Add(new PointF(stPointList[j].X, stPointList[j].Y));
                        }
                        polygon.Points = pts.ToArray();

                        listROI.Add(polygon);
                        string barType = GetBarType((MvCodeReader.MV_CODEREADER_CODE_TYPE)stBcrResultEx2.stBcrInfoEx2[i].nBarType);
                        String strCode = System.Text.Encoding.Default.GetString(stBcrResultEx2.stBcrInfoEx2[i].chCode);
                        strCodes.Add($"{barType},{strCode}");
                    }

                    //MvCodeReader.MV_CODEREADER_WAYBILL_LIST stWayList = (MvCodeReader.MV_CODEREADER_WAYBILL_LIST)Marshal.PtrToStructure(stFrameInfoEx2.pstWaybillList, typeof(MvCodeReader.MV_CODEREADER_WAYBILL_LIST));
                    //MvCodeReader.MV_CODEREADER_OCR_INFO_LIST stOcrInfo = (MvCodeReader.MV_CODEREADER_OCR_INFO_LIST)Marshal.PtrToStructure(stFrameInfoEx2.UnparsedOcrList.pstOcrList, typeof(MvCodeReader.MV_CODEREADER_OCR_INFO_LIST));
                    OnImageGrabbed(bmp.Clone() as Bitmap, strCodes, listROI);
                }
            }
        }

        public String GetBarType(MvCodeReader.MV_CODEREADER_CODE_TYPE nBarType)
        {
            string[] type = nBarType.ToString().Split('_');
            return type[type.Length - 1];
            switch (nBarType)
            {
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_DM:
                    return "DM";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_QR:
                    return "QR";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_EAN8:
                    return "EAN8";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_UPCE:
                    return "UPCE";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_UPCA:
                    return "UPCA";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_EAN13:
                    return "EAN13";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ISBN13:
                    return "ISBN13";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODABAR:
                    return "CODABAR";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ITF25:
                    return "ITF25";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE39:
                    return "CODE39";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE93:
                    return "Code 93码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE128:
                    return "Code 128码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_PDF417:
                    return "PDF417码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_MATRIX25:
                    return "MATRIX25码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_MSI:
                    return "MSI码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CODE11:
                    return "Code 11码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_INDUSTRIAL25:
                    return "industria125码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_CHINAPOST:
                    return "中国邮政码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_BCR_ITF14:
                    return "交叉14码";
                case MvCodeReader.MV_CODEREADER_CODE_TYPE.MV_CODEREADER_TDCR_ECC140:
                    return "ECC140码";
                default:
                    return "/";
            }
        }

        private void ShowErrorMsg(string msg, int nRet)
        {
            OnExceptionOccur?.Invoke("Set TriggerMode Source Line0 Fail!");
        }
    }
}
