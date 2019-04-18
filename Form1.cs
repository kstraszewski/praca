using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Windows.Forms;
using ToupTek;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace demowinformcs1
{
    public partial class Form1 : Form
    {
        private ToupCam cam_ = null;
        private Bitmap bmp_ = null;
        private uint MSG_CAMEVENT = 0x8001; // WM_APP = 0x8000

        private void OnEventError()
        {
            if (cam_ != null)
            {
                cam_.Close();
                cam_ = null;
            }
            MessageBox.Show("Error");
        }

        private void OnEventDisconnected()
        {
            if (cam_ != null)
            {
                cam_.Close();
                cam_ = null;
            }
            MessageBox.Show("The camera is disconnected, maybe has been pulled out.");
        }

        private void OnEventExposure()
        {
            if (cam_ != null)
            {
                uint nTime = 0;
                if (cam_.get_ExpoTime(out nTime))
                {
                    trackBar1.Value = (int)nTime;
                    label1.Text = (nTime / 1000).ToString() + " ms";
                }
            }
        }

        private async void OnEventImage()
        {
            if (bmp_ != null)
            {
                result.Text = $@"result:";
                BitmapData bmpdata = bmp_.LockBits(new Rectangle(0, 0, bmp_.Width, bmp_.Height), ImageLockMode.WriteOnly, bmp_.PixelFormat);

                ToupCam.FrameInfoV2 info = new ToupCam.FrameInfoV2();
                cam_.PullImageV2(bmpdata.Scan0, 24, out info);

                bmp_.UnlockBits(bmpdata);

                pictureBox1.Image = bmp_;
                pictureBox1.Invalidate();
                pictureBox1.Refresh();

                await ProcessResult();
            }

        }
        private Task ProcessResult()
        {
            var x = Task.Factory.StartNew( () => {
                Do(result, label =>
                {
                    var sharpnessLevel = GetSharpnessLevel(bmp_, xSobel, ySobel, 1.0, 0, true);
                    result.Text = $@"result: {sharpnessLevel}";
                } );
            });
            return x;
        }

        public static void Do<TControl>(TControl control, Action<TControl> action) where TControl : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, control);
            }
            else
            {
                action(control);
            }
        }

        private void OnEventStillImage()
        {
            ToupCam.FrameInfoV2 info = new ToupCam.FrameInfoV2();
            if (cam_.PullStillImageV2(IntPtr.Zero, 24, out info))   /* peek the width and height */
            {
                Bitmap sbmp = new Bitmap((int)info.width, (int)info.height, PixelFormat.Format24bppRgb);

                BitmapData bmpdata = sbmp.LockBits(new Rectangle(0, 0, sbmp.Width, sbmp.Height), ImageLockMode.WriteOnly, sbmp.PixelFormat);
                cam_.PullStillImageV2(bmpdata.Scan0, 24, out info);
                sbmp.UnlockBits(bmpdata);

                sbmp.Save("demowinformcs1.jpg");
            }
        }

        private readonly SerialPort currentPort;
        public Form1()
        {
            InitializeComponent();
            const string portName = "COM4";
            currentPort = new SerialPort(portName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            checkBox1.Enabled = false;
            comboBox1.Enabled = false;
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (MSG_CAMEVENT == m.Msg)
            {
                switch ((ToupCam.eEVENT)m.WParam.ToInt32())
                {
                    case ToupCam.eEVENT.EVENT_ERROR:
                        OnEventError();
                        break;
                    case ToupCam.eEVENT.EVENT_DISCONNECTED:
                        OnEventDisconnected();
                        break;
                    case ToupCam.eEVENT.EVENT_EXPOSURE:
                        OnEventExposure();
                        break;
                    case ToupCam.eEVENT.EVENT_IMAGE:
                        OnEventImage();
                        break;
                    case ToupCam.eEVENT.EVENT_STILLIMAGE:
                        OnEventStillImage();
                        break;
                    case ToupCam.eEVENT.EVENT_TEMPTINT:
                        OnEventTempTint();
                        break;
                }
                return;
            }
            base.WndProc(ref m);
        }

        private void OnStart(object sender, EventArgs e)
        {
            if (cam_ != null)
                return;

            ToupCam.InstanceV2[] arr = ToupCam.EnumV2();
            if (arr.Length <= 0)
            {
                MessageBox.Show("no device");
            }
            else
            {
                cam_ = new ToupCam();
                if (!cam_.Open(arr[0].id))
                {
                    cam_ = null;
                }
                else
                {
                    checkBox1.Enabled = true;
                    trackBar1.Enabled = true;
                    trackBar2.Enabled = true;
                    trackBar3.Enabled = true;
                    comboBox1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button2.ContextMenuStrip = null;
                    InitSnapContextMenuAndExpoTimeRange();

                    trackBar2.SetRange(2000, 15000);
                    trackBar3.SetRange(200, 2500);
                    OnEventTempTint();

                    uint resnum = cam_.ResolutionNumber;
                    uint eSize = 0;
                    if (cam_.get_eSize(out eSize))
                    {
                        for (uint i = 0; i < resnum; ++i)
                        {
                            int w = 0, h = 0;
                            if (cam_.get_Resolution(i, out w, out h))
                                comboBox1.Items.Add(w.ToString() + "*" + h.ToString());
                        }
                        comboBox1.SelectedIndex = (int)eSize;

                        int width = 0, height = 0;
                        if (cam_.get_Size(out width, out height))
                        {
                            bmp_ = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                            if (!cam_.StartPullModeWithWndMsg(this.Handle, MSG_CAMEVENT))
                                MessageBox.Show("failed to start device");
                            else
                            {
                                bool autoexpo = true;
                                cam_.get_AutoExpoEnable(out autoexpo);
                                checkBox1.Checked = autoexpo;
                                trackBar1.Enabled = !checkBox1.Checked;
                            }
                        }
                    }
                }
            }
        }

        private void SnapClickedHandler(object sender, ToolStripItemClickedEventArgs e)
        {
            int k = button2.ContextMenuStrip.Items.IndexOf(e.ClickedItem);
            if (k >= 0)
                cam_.Snap((uint)k);
        }

        private void InitSnapContextMenuAndExpoTimeRange()
        {
            if (cam_ == null)
                return;

            uint nMin = 0, nMax = 0, nDef = 0;
            if (cam_.get_ExpTimeRange(out nMin, out nMax, out nDef))
                trackBar1.SetRange((int)nMin, (int)nMax);
            OnEventExposure();

            if (cam_.StillResolutionNumber <= 0)
                return;
            
            button2.ContextMenuStrip = new ContextMenuStrip();
            button2.ContextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SnapClickedHandler);

            if (cam_.StillResolutionNumber < cam_.ResolutionNumber)
            {
                uint eSize = 0;
                if (cam_.get_eSize(out eSize))
                {
                    if (0 == eSize)
                    {
                        StringBuilder sb = new StringBuilder();
                        int w = 0, h = 0;
                        cam_.get_Resolution(eSize, out w, out h);
                        sb.Append(w);
                        sb.Append(" * ");
                        sb.Append(h);
                        button2.ContextMenuStrip.Items.Add(sb.ToString());
                        return;
                    }
                }
            }

            for (uint i = 0; i < cam_.ResolutionNumber; ++i)
            {
                StringBuilder sb = new StringBuilder();
                int w = 0, h = 0;
                cam_.get_Resolution(i, out w, out h);
                sb.Append(w);
                sb.Append(" * ");
                sb.Append(h);
                button2.ContextMenuStrip.Items.Add(sb.ToString());
            }
        }

        //Sobel operator kernel for horizontal pixel changes
        private static double[,] xSobel
        {
            get
            {
                return new double[,]
                {
                    { -1, 0, 1 },
                    { -2, 0, 2 },
                    { -1, 0, 1 }
                };
            }
        }

        //Sobel operator kernel for vertical pixel changes
        private static double[,] ySobel
        {
            get
            {
                return new double[,]
                {
                    {  1,  2,  1 },
                    {  0,  0,  0 },
                    { -1, -2, -1 }
                };
            }
        }

        private static double GetSharpnessLevel(Bitmap sourceImage, double[,] xkernel, double[,] ykernel, double factor = 1, int bias = 0, bool grayscale = false)
        {

            //Image dimensions stored in variables for convenience
            int width = sourceImage.Width;
            int height = sourceImage.Height;

            //Lock source image bits into system memory
            BitmapData srcData = sourceImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            //Get the total number of bytes in your image - 32 bytes per pixel x image width x image height -> for 32bpp images
            int bytes = srcData.Stride * srcData.Height;

            //Create byte arrays to hold pixel information of your image
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            //Get the address of the first pixel data
            IntPtr srcScan0 = srcData.Scan0;

            //Copy image data to one of the byte arrays
            Marshal.Copy(srcScan0, pixelBuffer, 0, bytes);

            //Unlock bits from system memory -> we have all our needed info in the array
            sourceImage.UnlockBits(srcData);

            //Convert your image to grayscale if necessary
            if (grayscale == true)
            {
                float rgb = 0;
                for (int i = 0; i < pixelBuffer.Length; i += 4)
                {
                    rgb = pixelBuffer[i] * .21f;
                    rgb += pixelBuffer[i + 1] * .71f;
                    rgb += pixelBuffer[i + 2] * .071f;
                    pixelBuffer[i] = (byte)rgb;
                    pixelBuffer[i + 1] = pixelBuffer[i];
                    pixelBuffer[i + 2] = pixelBuffer[i];
                    pixelBuffer[i + 3] = 255;
                }
            }

            //Create variable for pixel data for each kernel
            double xr = 0.0;
            double xg = 0.0;
            double xb = 0.0;
            double yr = 0.0;
            double yg = 0.0;
            double yb = 0.0;
            double rt = 0.0;
            double gt = 0.0;
            double bt = 0.0;

            //This is how much your center pixel is offset from the border of your kernel
            //Sobel is 3x3, so center is 1 pixel from the kernel border
            int filterOffset = 1;
            int calcOffset = 0;
            int byteOffset = 0;

            //Start with the pixel that is offset 1 from top and 1 from the left side
            //this is so entire kernel is on your image
            for (int OffsetY = filterOffset; OffsetY < height - filterOffset; OffsetY++)
            {
                for (int OffsetX = filterOffset; OffsetX < width - filterOffset; OffsetX++)
                {
                    //reset rgb values to 0
                    xr = xg = xb = yr = yg = yb = 0;
                    rt = gt = bt = 0.0;

                    //position of the kernel center pixel
                    byteOffset = OffsetY * srcData.Stride + OffsetX * 4;

                    //kernel calculations
                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + filterX * 4 + filterY * srcData.Stride;
                            xb += (double)(pixelBuffer[calcOffset]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            xg += (double)(pixelBuffer[calcOffset + 1]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            xr += (double)(pixelBuffer[calcOffset + 2]) * xkernel[filterY + filterOffset, filterX + filterOffset];
                            yb += (double)(pixelBuffer[calcOffset]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                            yg += (double)(pixelBuffer[calcOffset + 1]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                            yr += (double)(pixelBuffer[calcOffset + 2]) * ykernel[filterY + filterOffset, filterX + filterOffset];
                        }
                    }

                    //total rgb values for this pixel
                    bt = Math.Sqrt((xb * xb) + (yb * yb));
                    gt = Math.Sqrt((xg * xg) + (yg * yg));
                    rt = Math.Sqrt((xr * xr) + (yr * yr));

                    //set limits, bytes can hold values from 0 up to 255;
                    if (bt > 255) bt = 255;
                    else if (bt < 0) bt = 0;
                    if (gt > 255) gt = 255;
                    else if (gt < 0) gt = 0;
                    if (rt > 255) rt = 255;
                    else if (rt < 0) rt = 0;

                    //set new data in the other byte array for your image data
                    resultBuffer[byteOffset] = (byte)(bt);
                    resultBuffer[byteOffset + 1] = (byte)(gt);
                    resultBuffer[byteOffset + 2] = (byte)(rt);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            //Create new bitmap which will hold the processed data
            Bitmap resultImage = new Bitmap(width, height);

            //Lock bits into system memory
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            //Copy from byte array that holds processed data to bitmap
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            //Unlock bits from system memory
            resultImage.UnlockBits(resultData);

            return (resultBuffer.ToList().Sum(b => b / 12.5) / 1000);
        }

        private void OnSnap(object sender, EventArgs e)
        {
            if (cam_ != null)
            {
                if (cam_.StillResolutionNumber <= 0)
                {
                    if (bmp_ != null)
                    {
                    }
                }
                else
                {
                    if (button2.ContextMenuStrip != null)
                        button2.ContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (cam_ != null)
            {
                cam_.Close();
                cam_ = null;
            }
        }

        private void OnSelectResolution(object sender, EventArgs e)
        {
            if (cam_ != null)
            {
                uint eSize = 0;
                if (cam_.get_eSize(out eSize))
                {
                    if (eSize != comboBox1.SelectedIndex)
                    {
                        button2.ContextMenuStrip = null;

                        cam_.Stop();
                        cam_.put_eSize((uint)comboBox1.SelectedIndex);

                        InitSnapContextMenuAndExpoTimeRange();
                        OnEventTempTint();

                        int width = 0, height = 0;
                        if (cam_.get_Size(out width, out height))
                        {
                            bmp_ = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                            cam_.StartPullModeWithWndMsg(this.Handle, MSG_CAMEVENT);
                        }
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cam_ != null)
                cam_.put_AutoExpoEnable(checkBox1.Checked);
            trackBar1.Enabled = !checkBox1.Checked;
        }

        private void OnExpoValueChange(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                if (cam_ != null)
                {
                    uint n = (uint)trackBar1.Value;
                    cam_.put_ExpoTime(n);
                    label1.Text = (n / 1000).ToString() + " ms";
                }
            }
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = ClientRectangle.Right - button1.Bounds.Right - 20;
            pictureBox1.Height = ClientRectangle.Height - 8;
        }

        private void OnEventTempTint()
        {
            if (cam_ != null)
            {
                int nTemp = 0, nTint = 0;
                if (cam_.get_TempTint(out nTemp, out nTint))
                {
                    label2.Text = nTemp.ToString();
                    label3.Text = nTint.ToString();
                    trackBar2.Value = nTemp;
                    trackBar3.Value = nTint;
                }
            }
        }

        private void OnWhiteBalanceOnePush(object sender, EventArgs e)
        {
            if (cam_ != null)
                cam_.AwbOnePush(null);
        }

        private void OnTempTintChanged(object sender, EventArgs e)
        {
            if (cam_ != null)
                cam_.put_TempTint(trackBar2.Value, trackBar3.Value);
            label2.Text = trackBar2.Value.ToString();
            label3.Text = trackBar3.Value.ToString();
        }

        private void sendMsg(string msg)
        {
            var myport = currentPort;
            myport.BaudRate = 9600;
            
            try
            {
                if (!myport.IsOpen)
                {
                    myport.PortName = "COM5";
                    myport.Open();
                }
            }
            catch (InvalidOperationException) { }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
           
            myport.WriteLine(msg);

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sendMsg("S");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sendMsg("D");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sendMsg("U");
        }
    }
}
