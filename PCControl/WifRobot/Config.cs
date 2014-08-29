using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WifiVideo
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }
        public string FileName; //INI文件名
        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        private void Config_Load(object sender, EventArgs e)
        {
            GetIni();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteIni("VideoUrl","videourl",this.textBoxVideo.Text);
            WriteIni("ControlUrl", "controlUrl", this.textControlURL.Text);
            WriteIni("ControlPort", "controlPort", this.textBoxControlPort.Text);
           

            MessageBox.Show("配置成功！请重启程序以使配置生效。", "配置信息", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            this.Close();
        }
        //写INI文件
        public void WriteIni(string Section, string Ident, string Value)
        {
            if (!WritePrivateProfileString(Section, Ident, Value, FileName))
            {

                throw (new ApplicationException("写入配置文件出错"));
            }
          
        }
        //读取INI文件指定
        public string ReadIni(string Section, string Ident, string Default)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);
            return s.Trim();
        }
        private void GetIni()
        {
            FileName = Application.StartupPath + "\\Config.ini";
            this.textBoxVideo.Text = ReadIni("VideoUrl", "videourl", "");
            this.textControlURL.Text = ReadIni("ControlUrl", "controlUrl", "");
            this.textBoxControlPort.Text = ReadIni("ControlPort", "controlPort", "");
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
