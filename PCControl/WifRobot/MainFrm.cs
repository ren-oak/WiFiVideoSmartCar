using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;

namespace WifiVideo
{
    public partial class Mainfrm : Form
    {
      //  private JoyKeys.Core.Joystick joystick;
        public Mainfrm()
        {
            InitializeComponent();

            pBShow.Controls.Add(label_fps);//将Label控件加入PictureBox设为它的子控件
            label_fps.BackColor = Color.Transparent;//透明底色
            label_fps.ForeColor = Color.Green;
            buttonLight.BackColor = Color.DarkSlateGray;
            
        }

        //游戏手柄部分
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
          //  joystick = new JoyKeys.Core.Joystick();
           // joystick.Click += new EventHandler<JoyKeys.Core.JoystickEventArgs>(joystick_Click);
          //  joystick.Register(base.Handle, JoyKeys.Core.API.JOYSTICKID1);
         //   joystick.Register(base.Handle, JoyKeys.Core.API.JOYSTICKID2);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //joystick.UnRegister(JoyKeys.Core.API.JOYSTICKID1);
           // joystick.UnRegister(JoyKeys.Core.API.JOYSTICKID2);
            base.OnClosing(e);
        }


        /*
        private void timer_joykeys_Tick(object sender, EventArgs e)
        {
            if (joykes_n > 0) joykes_n--;
            else
            {
                timer_joykeys.Enabled = false;

                statucs_n = 0;
                timer_cmd.Enabled = false;
                SendData(CMD_Stop);

                buttonForward.BackColor = Color.LightBlue;
                buttonBackward.BackColor = Color.LightBlue;
                buttonLeft.BackColor = Color.LightBlue;
                buttonRight.BackColor = Color.LightBlue;
                btnEngineUp.BackColor = Color.LightBlue;
                btnEngineDown.BackColor = Color.LightBlue;
                btnEngineLeft.BackColor = Color.LightBlue;
                btnEngineRight.BackColor = Color.LightBlue;
                btnEngineStop.BackColor = Color.LightBlue;
                buttonLight.BackColor = Color.LightBlue;
            }
        }
       
        void joystick_Click(object sender, JoyKeys.Core.JoystickEventArgs e)
        {
            if (e.JoystickId == JoyKeys.Core.API.JOYSTICKID1)
            {
                tB_Joykeys.Text = "1号手柄";
            }
            else if (e.JoystickId == JoyKeys.Core.API.JOYSTICKID2)
            {
                tB_Joykeys.Text = "2号手柄";
            }

            int i;

            if ((e.Buttons & JoyKeys.Core.JoystickButtons.UP) == JoyKeys.Core.JoystickButtons.UP)
            {
                buttonForward.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_Forward[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }

            if ((e.Buttons & JoyKeys.Core.JoystickButtons.Down) == JoyKeys.Core.JoystickButtons.Down)
            {
                buttonBackward.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_Backward[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.Left) == JoyKeys.Core.JoystickButtons.Left)
            {
                buttonLeft.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_TurnLeft[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.Right) == JoyKeys.Core.JoystickButtons.Right)
            {
                buttonRight.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_TurnRight[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B1) == JoyKeys.Core.JoystickButtons.B1)
            {
                btnEngineDown.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineUpDown[3] < 0xA0 && CMD_EngineUpDown[3] > 0x65 + step)
                    {
                        CMD_EngineUpDown[3] -= step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineUpDown[i];
                statucs_n = 2;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B2) == JoyKeys.Core.JoystickButtons.B2)
            {
                btnEngineRight.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineLeftRight[3] < 0xB0 && CMD_EngineLeftRight[3] > 0x10 + step)
                    {
                        CMD_EngineLeftRight[3] -= step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineLeftRight[i];
                statucs_n = 4;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B3) == JoyKeys.Core.JoystickButtons.B3)
            {
                btnEngineUp.BackColor = Color.DarkGray;

                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineUpDown[3] < 0xA0 - step && CMD_EngineUpDown[3] > 0x65)
                    {
                        CMD_EngineUpDown[3] += step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineUpDown[i];
                statucs_n = 1;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B4) == JoyKeys.Core.JoystickButtons.B4)
            {
                btnEngineLeft.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineLeftRight[3] < 0xB0 && CMD_EngineLeftRight[3] > 0x10 + step)
                    {
                        CMD_EngineLeftRight[3] += step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineLeftRight[i];
                statucs_n = 3;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B5) == JoyKeys.Core.JoystickButtons.B5)
            {
                if (joykes_n == 0)
                {
                    buttonLight.BackColor = Color.DarkGray;
                    buttonLight.PerformClick();
                }
            }
           
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B6) == JoyKeys.Core.JoystickButtons.B6)
            {
                if (joykes_n==0)
                {
                    btnEngineStop.BackColor = Color.DarkGray;
                    btnEngineStop.PerformClick();
                }
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B7) == JoyKeys.Core.JoystickButtons.B7)
            {
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B8) == JoyKeys.Core.JoystickButtons.B8)
            {
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B9) == JoyKeys.Core.JoystickButtons.B9)
            {
            }
            if ((e.Buttons & JoyKeys.Core.JoystickButtons.B10) == JoyKeys.Core.JoystickButtons.B10)
            {
            }

            timer_joykeys.Enabled = true;
            joykes_n = 2;
            
        }
         */



        //语音识别部分
        public delegate void StringEvent(string str);
        public StringEvent SetMessage;
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        static string FileName = Application.StartupPath + "\\Config.ini"; 
        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
      
        void SendData(byte[] data)
        {
            try
            {
                IPAddress ips = IPAddress.Parse(ControlIp);//("192.168.1.1");192.168.10.1
                IPEndPoint ipe = new IPEndPoint(ips, int.Parse(Port));//把ip和端口转化为IPEndPoint实例;1376
                UdpClient c = new UdpClient(23456);//创建一个Socket

              //  c.Connect(ipe);//连接到服务器

                //byte[] bs = Encoding.ASCII.GetBytes(data); 


                c.Send(data, data.Length, ipe);//发送测试信息
                c.Close();
                //Thread.Sleep(10);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Exit();
            }
        }

        string CameraIp = @"http://192.168.10.1:8080/?action=stream";
        string ControlIp = "192.168.10.1";
        string Port = "1376";
        byte[] CMD = new byte[5];
        byte[] CMD_Forward = new byte[5];
        byte[] CMD_Backward = new byte[5];
        byte[] CMD_TurnLeft = new byte[5];
        byte[] CMD_TurnRight = new byte[5];
        byte[] CMD_Stop = new byte[5];

        byte[] CMD_EngineUpDown = new byte[5];
        byte[] CMD_EngineLeftRight = new byte[5];

        byte[] CMD_Light = new byte[5];
 
        private void GetIni()
        {
            CameraIp = ReadIni("VideoUrl", "videoUrl", "");
            ControlIp = ReadIni("ControlUrl", "controlUrl", "");
            Port = ReadIni("ControlPort", "controlPort", "");

            CMD_Forward[0] = 0xFF;      CMD_Forward[1] = 0x00;      CMD_Forward[2] = 0x01;      CMD_Forward[3] = 0x00;      CMD_Forward[4] = 0xFF;
            CMD_Backward[0] = 0xFF;     CMD_Backward[1] = 0x00;     CMD_Backward[2] = 0x02;     CMD_Backward[3] = 0x00;     CMD_Backward[4] = 0xFF;
            CMD_TurnLeft[0] = 0xFF;     CMD_TurnLeft[1] = 0x00;     CMD_TurnLeft[2] = 0x03;     CMD_TurnLeft[3] = 0x00;     CMD_TurnLeft[4] = 0xFF;
            CMD_TurnRight[0] = 0xFF;    CMD_TurnRight[1] = 0x00;    CMD_TurnRight[2] = 0x04;    CMD_TurnRight[3] = 0x00;    CMD_TurnRight[4] = 0xFF;
            CMD_Stop[0] = 0xFF;         CMD_Stop[1] = 0x00;         CMD_Stop[2] = 0x00;         CMD_Stop[3] = 0x00;         CMD_Stop[4] = 0xFF;
            
            CMD_Light[0] = 0xFF;        
            CMD_Light[1] = 0x02; 
            CMD_Light[2] = 0x00; 
            CMD_Light[3] = 0x00; 
            CMD_Light[4] = 0xFF;


            CMD_EngineLeftRight[0] = 0xFF;
            CMD_EngineLeftRight[1] = 0x01;
            CMD_EngineLeftRight[2] = 0x01;
            CMD_EngineLeftRight[3] = 0x5A;
            CMD_EngineLeftRight[4] = 0xFF;


            CMD_EngineUpDown[0] = 0xFF; 
            CMD_EngineUpDown[1] = 0x01; 
            CMD_EngineUpDown[2] = 0x02; 
            CMD_EngineUpDown[3] = 0x7B; 
            CMD_EngineUpDown[4] = 0xFF;



        }

        System.Threading.Thread thread = null;
        void btn_show_Click(object sender, EventArgs e)
        {
            btnEngineStop.Select();
            btn_show.Enabled = false;
            btn_set.Enabled = false;
            //启动一个线程   
            thread = new System.Threading.Thread(ThreadProc);
            thread.Start();
        }

        public void ThreadProc()
        {
            GetStream(CameraIp);
        }

        private void btn_set_Click(object sender, EventArgs e)
        {
            Config cfg = new Config();
            cfg.ShowDialog();
            btnEngineStop.Select();
        }
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            GetIni();
            buttonForward.BackColor = Color.LightBlue;
            buttonBackward.BackColor = Color.LightBlue;
            buttonLeft.BackColor = Color.LightBlue;
            buttonRight.BackColor = Color.LightBlue;
            btnEngineUp.BackColor = Color.LightBlue;
            btnEngineDown.BackColor = Color.LightBlue;
            btnEngineLeft.BackColor = Color.LightBlue;
            btnEngineRight.BackColor = Color.LightBlue;
            btnEngineStop.BackColor = Color.LightBlue;
            buttonLight.BackColor = Color.LightBlue;

            btnEngineStop.Select();
           // tB_Joykeys.Text = "手柄未连接";
        }

      //  int statucs_n = 0;
        /*
        private void timer_cmd_Tick(object sender, EventArgs e)
        {
            switch (statucs_n)
            {
                case 0:
                    break;
                case 1:
                    if (CMD_EngineUpDown[3] < 0xA0 - step && CMD_EngineUpDown[3] > 0x65)
                    {
                        CMD_EngineUpDown[3] += step;
                        int i;
                        for (i = 0; i < 5; i++)
                        {
                            CMD[i] = CMD_EngineUpDown[i];
                        }
                    }
                    break;
                case 2:
                    if (CMD_EngineUpDown[3] < 0xA0 && CMD_EngineUpDown[3] > 0x65 + step)
                    {
                        CMD_EngineUpDown[3] -= step;
                        int i;
                        for (i = 0; i < 5; i++)
                        {
                            CMD[i] = CMD_EngineUpDown[i];
                        }
                    }
                    break;
                case 3:

                    if (CMD_EngineLeftRight[3] < 0xB0 - step && CMD_EngineLeftRight[3] > 0x10)
                    {
                        CMD_EngineLeftRight[3] += step;            
                        int i;
                        for (i = 0; i < 5; i++)
                        {
                            CMD[i] = CMD_EngineLeftRight[i];
                        }
                    }
                    break;
                case 4:
                    if (CMD_EngineLeftRight[3] < 0xB0 && CMD_EngineLeftRight[3] > 0x10 + step)
                    {
                        CMD_EngineLeftRight[3] -= step;                        
                        int i;
                        for (i = 0; i < 5; i++)
                        {
                            CMD[i] = CMD_EngineLeftRight[i];
                        }
                    }
                    break;

                default:
                    return;
            }
            
            SendData(CMD);


        }*/
        /*
        private void Mainfrm_KeyUp(object sender, KeyEventArgs e)
        {
            statucs_n = 0;
            timer_cmd.Enabled = false;
            SendData(CMD_Stop);

            buttonForward.BackColor = Color.LightBlue;
            buttonBackward.BackColor = Color.LightBlue;
            buttonLeft.BackColor = Color.LightBlue;
            buttonRight.BackColor = Color.LightBlue;
            btnEngineUp.BackColor = Color.LightBlue;
            btnEngineDown.BackColor = Color.LightBlue;
            btnEngineLeft.BackColor = Color.LightBlue;
            btnEngineRight.BackColor = Color.LightBlue;
            btnEngineStop.BackColor = Color.LightBlue;
            buttonLight.BackColor = Color.LightBlue;
        }*/
        /*
        private void Mainfrm_KeyDown(object sender, KeyEventArgs e)
        {
            int i;
            if (e.KeyCode == Keys.W)
            {
                buttonForward.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_Forward[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;

            }
            else if (e.KeyCode == Keys.S)
            {
                buttonBackward.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_Backward[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;

            }
            else if (e.KeyCode == Keys.A)
            {
                buttonLeft.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_TurnLeft[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;

            }
            else if (e.KeyCode == Keys.D)
            {
                buttonRight.BackColor = Color.DarkGray;
                for (i = 0; i < 5; i++) CMD[i] = CMD_TurnRight[i];
                statucs_n = 0;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;

            }
            else if (e.KeyCode == Keys.K)
            {
                btnEngineUp.BackColor = Color.DarkGray;
               
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineUpDown[3] < 0xA0 - step && CMD_EngineUpDown[3] > 0x65)
                    {
                        CMD_EngineUpDown[3] += step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineUpDown[i];
                statucs_n = 1;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            else if (e.KeyCode == Keys.I)
            {
                btnEngineDown.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineUpDown[3] < 0xA0 && CMD_EngineUpDown[3] > 0x65 + step)
                    {
                        CMD_EngineUpDown[3] -= step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineUpDown[i];
                statucs_n = 2;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;

            }
            else if (e.KeyCode == Keys.J)
            {
                btnEngineLeft.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineLeftRight[3] < 0xB0 && CMD_EngineLeftRight[3] > 0x10 + step)
                    {
                        CMD_EngineLeftRight[3] += step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineLeftRight[i];
                statucs_n = 3;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            else if (e.KeyCode == Keys.L)
            {
                btnEngineRight.BackColor = Color.DarkGray;
                if (!timer_cmd.Enabled)
                {
                    if (CMD_EngineLeftRight[3] < 0xB0 && CMD_EngineLeftRight[3] > 0x10 + step)
                    {
                        CMD_EngineLeftRight[3] -= step;
                    }
                }
                for (i = 0; i < 5; i++) CMD[i] = CMD_EngineLeftRight[i];
                statucs_n = 4;
                if (!timer_cmd.Enabled) SendData(CMD);
                timer_cmd.Enabled = true;
            }
            else if (e.KeyCode == Keys.Space)
            {
                btnEngineStop.BackColor = Color.DarkGray;
                btnEngineStop.PerformClick();
            }

            btnEngineStop.Select();
        }
        */
        bool dupflag = false;
        private void buttonForward_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            Thread t;
            t = new Thread(delegate()
            {
            SendData(CMD_Forward);
            });
            t.Start();
            */
            //SendData(CMD_Forward);
          
         
            //statucs_n = 0;
            byte[] b=new byte[1];
            b[0] =0x30+1 ;
            if (!dupflag) SendData(b);
            dupflag = true;
            
           
        }
        private void buttonForward_MouseUp(object sender, MouseEventArgs e)
        {
            dupflag = false;
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
        }

        private void buttonLeft_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 3;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void buttonLeft_MouseUp(object sender, MouseEventArgs e)
        {
            dupflag = false;
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
        }

        private void buttonRight_MouseDown(object sender, MouseEventArgs e)
        {
         
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 4;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void buttonRight_MouseUp(object sender, MouseEventArgs e)
        {
            dupflag = false;
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
        }

        private void buttonBackward_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 2;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void buttonBackward_MouseUp(object sender, MouseEventArgs e)
        {
            dupflag = false;
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
        }


       // private byte step = 2;
        private void btnEngineUp_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 6;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void btnEngineUp_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
            dupflag = false;
        }

        private void btnEngineDown_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 5;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void btnEngineDown_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
            dupflag = false;
        }

        private void btnEngineLeft_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 7;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void btnEngineLeft_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
            dupflag = false;
        }

        private void btnEngineRight_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 8;
            if (!dupflag) SendData(b);
            dupflag = true;

        }

        private void btnEngineRight_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
            dupflag = false;
        }
        
        private void btnEngineStop_Click(object sender, EventArgs e)
        {
            ;
        }

        private void Mainfrm_Deactivate(object sender, EventArgs e)
        {
            dupflag = false;
            //statucs_n = 0;
        }

        private void buttonLight_Click(object sender, EventArgs e)
        {
            if (buttonLight.BackColor ==Color.DarkSlateGray)
            {
                buttonLight.BackColor = Color.Red;
                byte[] b = new byte[1];
                b[0] = 0x61;
                SendData(b);
            }
            else
            {
                buttonLight.BackColor = Color.DarkSlateGray;
                byte[] b = new byte[1];
                b[0] = 0x62;
                SendData(b);
            }
        }

        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <param name="count">数组大小</param>
        /// <returns>图片</returns>
        private Image byteArrayToImage(byte[] Bytes, int count)
        {
            using (MemoryStream ms = new MemoryStream(Bytes, 0, count))
            {
                //Bitmap bmp = (Bitmap)Bitmap.FromStream(ms);
                Image outputImg = Image.FromStream(ms);
                return outputImg;
            }
        }

        /// <summary>
        /// 获取视频流
        /// </summary>
        /// /// <param name="UriString">控制Uri</param>
        private void GetStream(string UriString)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream stream = null;
            System.DateTime time;
            int fps = 0;

            const int bufSize = 512 * 1024;	            //视频图片缓冲
            byte[] jpg_buf = new byte[bufSize];	        // buffer to read jpg
            const int readSize = 4096;		            //每次最大获取的流
            byte[] buffer = new byte[readSize];	        // buffer to read stream            


            while (true)
            {

                try
                {
                    time = System.DateTime.Now;
                    fps = 0;

                    request = (HttpWebRequest)System.Net.WebRequest.Create(UriString);
                    response = (HttpWebResponse)request.GetResponse();

                    int read;
                    int status = 0;
                    int jpg_count = 0;                          //jpg数据下标
                    while (true)
                    {
                        stream = response.GetResponseStream();

                        if ((read = stream.Read(buffer, 0, readSize)) > 0)
                        {
                            for (int i = 0; i < read; i++)
                            {
                                switch (status)
                                {
                                    //Content-Length:
                                    case 0: if (buffer[i] == (byte)'C') status++; else status = 0; break;
                                    case 1: if (buffer[i] == (byte)'o') status++; else status = 0; break;
                                    case 2: if (buffer[i] == (byte)'n') status++; else status = 0; break;
                                    case 3: if (buffer[i] == (byte)'t') status++; else status = 0; break;
                                    case 4: if (buffer[i] == (byte)'e') status++; else status = 0; break;
                                    case 5: if (buffer[i] == (byte)'n') status++; else status = 0; break;
                                    case 6: if (buffer[i] == (byte)'t') status++; else status = 0; break;
                                    case 7: if (buffer[i] == (byte)'-') status++; else status = 0; break;
                                    case 8: if (buffer[i] == (byte)'L') status++; else status = 0; break;
                                    case 9: if (buffer[i] == (byte)'e') status++; else status = 0; break;
                                    case 10: if (buffer[i] == (byte)'n') status++; else status = 0; break;
                                    case 11: if (buffer[i] == (byte)'g') status++; else status = 0; break;
                                    case 12: if (buffer[i] == (byte)'t') status++; else status = 0; break;
                                    case 13: if (buffer[i] == (byte)'h') status++; else status = 0; break;
                                    case 14: if (buffer[i] == (byte)':') status++; else status = 0; break;
                                    case 15:
                                        if (buffer[i] == 0xFF) status++;
                                        jpg_count = 0;
                                        jpg_buf[jpg_count++] = buffer[i];
                                        break;
                                    case 16:
                                        if (buffer[i] == 0xD8)
                                        {
                                            status++;
                                            jpg_buf[jpg_count++] = buffer[i];
                                        }
                                        else
                                        {
                                            if (buffer[i] != 0xFF) status = 15;
                                        }
                                        break;
                                    case 17:
                                        jpg_buf[jpg_count++] = buffer[i];
                                        if (buffer[i] == 0xFF) status++;
                                        if (jpg_count >= bufSize) status = 0;
                                        break;
                                    case 18:
                                        jpg_buf[jpg_count++] = buffer[i];
                                        if (buffer[i] == 0xD9)
                                        {
                                            status = 0;
                                            //jpg接收完成
                                            this.Invoke((EventHandler)delegate
                                            {
                                                fps++;

                                                Image image = byteArrayToImage(jpg_buf, jpg_count);

                                                pBShow.Image = image;

                                                if (System.DateTime.Now >= time.AddSeconds(1))
                                                {
                                                    label_fps.Text = fps.ToString() + " fps";
                                                    fps = 0;
                                                    time = System.DateTime.Now;
                                                }

                                                //label_fps在图片上显示的位置 
                                                //Rectangle r = new Rectangle(pBShow.Image.Width - 42, pBShow.Image.Height - 22, 40, 20);
                                                //pBShow.DrawToBitmap((Bitmap)pBShow.Image, r);
                                                //pBShow.Refresh();




                                            });

                                        }
                                        else
                                        {
                                            if (buffer[i] != 0xFF) status = 17;
                                        }
                                        break;
                                    default:
                                        status = 0;
                                        break;

                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //textBox2.Text = ex.Message;

                    if (request != null)
                    {
                        request.Abort();
                        request = null;
                    }
                    // close response stream
                    if (response != null)
                    {
                        response.Close();
                        response = null;
                    }
                    // close response
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }

                }
            }


        }

        private void Mainfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null) thread.Abort();
        }

        private void btnEngineStop_MouseUp(object sender, MouseEventArgs e)
        {
          
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
             SendData(b);
            dupflag = false;
        }

        private void btnEngineStop_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x30 + 9;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void buttonlaba_MouseDown(object sender, MouseEventArgs e)
        {
            //statucs_n = 0;
            byte[] b = new byte[1];
            b[0] = 0x63;
            if (!dupflag) SendData(b);
            dupflag = true;
        }

        private void buttonlaba_MouseUp(object sender, MouseEventArgs e)
        {
            byte[] b = new byte[1];
            b[0] = 0x30 + 0;
            SendData(b);
            dupflag = false;
        }

        private void pBShow_Click(object sender, EventArgs e)
        {

        }

   

          

    }
}
