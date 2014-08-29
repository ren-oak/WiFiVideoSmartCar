namespace WifiVideo
{
    partial class Mainfrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainfrm));
            this.gBShow = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonlaba = new System.Windows.Forms.Button();
            this.buttonLight = new System.Windows.Forms.Button();
            this.btnEngineRight = new System.Windows.Forms.Button();
            this.btnEngineLeft = new System.Windows.Forms.Button();
            this.btnEngineStop = new System.Windows.Forms.Button();
            this.btnEngineDown = new System.Windows.Forms.Button();
            this.btnEngineUp = new System.Windows.Forms.Button();
            this.btn_set = new System.Windows.Forms.Button();
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.btn_show = new System.Windows.Forms.Button();
            this.label_fps = new System.Windows.Forms.Label();
            this.pBShow = new System.Windows.Forms.PictureBox();
            this.gBShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBShow)).BeginInit();
            this.SuspendLayout();
            // 
            // gBShow
            // 
            this.gBShow.Controls.Add(this.label2);
            this.gBShow.Controls.Add(this.label1);
            this.gBShow.Controls.Add(this.buttonlaba);
            this.gBShow.Controls.Add(this.buttonLight);
            this.gBShow.Controls.Add(this.btnEngineRight);
            this.gBShow.Controls.Add(this.btnEngineLeft);
            this.gBShow.Controls.Add(this.btnEngineStop);
            this.gBShow.Controls.Add(this.btnEngineDown);
            this.gBShow.Controls.Add(this.btnEngineUp);
            this.gBShow.Controls.Add(this.btn_set);
            this.gBShow.Controls.Add(this.buttonBackward);
            this.gBShow.Controls.Add(this.buttonRight);
            this.gBShow.Controls.Add(this.buttonLeft);
            this.gBShow.Controls.Add(this.buttonForward);
            this.gBShow.Controls.Add(this.btn_show);
            this.gBShow.Location = new System.Drawing.Point(5, 486);
            this.gBShow.Name = "gBShow";
            this.gBShow.Size = new System.Drawing.Size(640, 167);
            this.gBShow.TabIndex = 10;
            this.gBShow.TabStop = false;
            this.gBShow.Text = "操作指令";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(499, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "云台控制";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "小车控制";
            // 
            // buttonlaba
            // 
            this.buttonlaba.Location = new System.Drawing.Point(87, 63);
            this.buttonlaba.Name = "buttonlaba";
            this.buttonlaba.Size = new System.Drawing.Size(64, 42);
            this.buttonlaba.TabIndex = 24;
            this.buttonlaba.Text = "鸣笛";
            this.buttonlaba.UseVisualStyleBackColor = true;
            this.buttonlaba.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonlaba_MouseDown);
            this.buttonlaba.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonlaba_MouseUp);
            // 
            // buttonLight
            // 
            this.buttonLight.Location = new System.Drawing.Point(265, 130);
            this.buttonLight.Name = "buttonLight";
            this.buttonLight.Size = new System.Drawing.Size(92, 31);
            this.buttonLight.TabIndex = 23;
            this.buttonLight.Text = "灯";
            this.buttonLight.UseVisualStyleBackColor = true;
            this.buttonLight.Click += new System.EventHandler(this.buttonLight_Click);
            // 
            // btnEngineRight
            // 
            this.btnEngineRight.Location = new System.Drawing.Point(558, 63);
            this.btnEngineRight.Name = "btnEngineRight";
            this.btnEngineRight.Size = new System.Drawing.Size(63, 42);
            this.btnEngineRight.TabIndex = 21;
            this.btnEngineRight.Text = "右";
            this.btnEngineRight.UseVisualStyleBackColor = true;
            this.btnEngineRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEngineRight_MouseDown);
            this.btnEngineRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEngineRight_MouseUp);
            // 
            // btnEngineLeft
            // 
            this.btnEngineLeft.Location = new System.Drawing.Point(420, 63);
            this.btnEngineLeft.Name = "btnEngineLeft";
            this.btnEngineLeft.Size = new System.Drawing.Size(63, 42);
            this.btnEngineLeft.TabIndex = 20;
            this.btnEngineLeft.Text = "左";
            this.btnEngineLeft.UseVisualStyleBackColor = true;
            this.btnEngineLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEngineLeft_MouseDown);
            this.btnEngineLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEngineLeft_MouseUp);
            // 
            // btnEngineStop
            // 
            this.btnEngineStop.Location = new System.Drawing.Point(489, 63);
            this.btnEngineStop.Name = "btnEngineStop";
            this.btnEngineStop.Size = new System.Drawing.Size(63, 42);
            this.btnEngineStop.TabIndex = 19;
            this.btnEngineStop.Text = "回位";
            this.btnEngineStop.UseVisualStyleBackColor = true;
            this.btnEngineStop.Click += new System.EventHandler(this.btnEngineStop_Click);
            this.btnEngineStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEngineStop_MouseDown);
            this.btnEngineStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEngineStop_MouseUp);
            // 
            // btnEngineDown
            // 
            this.btnEngineDown.Location = new System.Drawing.Point(489, 15);
            this.btnEngineDown.Name = "btnEngineDown";
            this.btnEngineDown.Size = new System.Drawing.Size(63, 42);
            this.btnEngineDown.TabIndex = 18;
            this.btnEngineDown.Text = "俯";
            this.btnEngineDown.UseVisualStyleBackColor = true;
            this.btnEngineDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEngineDown_MouseDown);
            this.btnEngineDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEngineDown_MouseUp);
            // 
            // btnEngineUp
            // 
            this.btnEngineUp.Location = new System.Drawing.Point(489, 111);
            this.btnEngineUp.Name = "btnEngineUp";
            this.btnEngineUp.Size = new System.Drawing.Size(63, 42);
            this.btnEngineUp.TabIndex = 17;
            this.btnEngineUp.Text = "仰";
            this.btnEngineUp.UseVisualStyleBackColor = true;
            this.btnEngineUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEngineUp_MouseDown);
            this.btnEngineUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEngineUp_MouseUp);
            // 
            // btn_set
            // 
            this.btn_set.Location = new System.Drawing.Point(338, 20);
            this.btn_set.Name = "btn_set";
            this.btn_set.Size = new System.Drawing.Size(75, 28);
            this.btn_set.TabIndex = 15;
            this.btn_set.Text = "设置";
            this.btn_set.UseVisualStyleBackColor = true;
            this.btn_set.Click += new System.EventHandler(this.btn_set_Click);
            // 
            // buttonBackward
            // 
            this.buttonBackward.Location = new System.Drawing.Point(87, 107);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(63, 42);
            this.buttonBackward.TabIndex = 14;
            this.buttonBackward.Text = "后\r\n(S)";
            this.buttonBackward.UseVisualStyleBackColor = true;
            this.buttonBackward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonBackward_MouseDown);
            this.buttonBackward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonBackward_MouseUp);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(152, 63);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(64, 42);
            this.buttonRight.TabIndex = 13;
            this.buttonRight.Text = "右\r\n(D)";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRight_MouseDown);
            this.buttonRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRight_MouseUp);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(21, 63);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(64, 42);
            this.buttonLeft.TabIndex = 12;
            this.buttonLeft.Text = "左\r\n(A)";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonLeft_MouseDown);
            this.buttonLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonLeft_MouseUp);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(87, 20);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(63, 42);
            this.buttonForward.TabIndex = 11;
            this.buttonForward.Text = "前\r\n(W)";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonForward_MouseDown);
            this.buttonForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonForward_MouseUp);
            // 
            // btn_show
            // 
            this.btn_show.Location = new System.Drawing.Point(192, 22);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(75, 28);
            this.btn_show.TabIndex = 10;
            this.btn_show.Text = "视频";
            this.btn_show.UseVisualStyleBackColor = true;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // label_fps
            // 
            this.label_fps.AutoSize = true;
            this.label_fps.Location = new System.Drawing.Point(590, 462);
            this.label_fps.Name = "label_fps";
            this.label_fps.Size = new System.Drawing.Size(35, 12);
            this.label_fps.TabIndex = 11;
            this.label_fps.Text = "0 fps";
            // 
            // pBShow
            // 
            this.pBShow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pBShow.Image = ((System.Drawing.Image)(resources.GetObject("pBShow.Image")));
            this.pBShow.Location = new System.Drawing.Point(5, 4);
            this.pBShow.Name = "pBShow";
            this.pBShow.Size = new System.Drawing.Size(640, 480);
            this.pBShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBShow.TabIndex = 0;
            this.pBShow.TabStop = false;
            this.pBShow.Click += new System.EventHandler(this.pBShow_Click);
            // 
            // Mainfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 661);
            this.Controls.Add(this.label_fps);
            this.Controls.Add(this.gBShow);
            this.Controls.Add(this.pBShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Mainfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WF-103Rb";
            this.Deactivate += new System.EventHandler(this.Mainfrm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainfrm_FormClosing);
            this.Load += new System.EventHandler(this.Mainfrm_Load);
            this.gBShow.ResumeLayout(false);
            this.gBShow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBShow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBShow;
        private System.Windows.Forms.Button btnEngineDown;
        private System.Windows.Forms.Button btnEngineUp;
        private System.Windows.Forms.Button btn_set;
        private System.Windows.Forms.Button buttonBackward;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btnEngineRight;
        private System.Windows.Forms.Button btnEngineLeft;
        private System.Windows.Forms.Button btnEngineStop;
        private System.Windows.Forms.Button buttonLight;
        private System.Windows.Forms.Label label_fps;
        private System.Windows.Forms.Button buttonlaba;
        private System.Windows.Forms.PictureBox pBShow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

