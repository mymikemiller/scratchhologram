namespace ScratchTest
{
    partial class ScratchTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mViewAngleTrack = new System.Windows.Forms.TrackBar();
            this.mArcSweepAngleTrack = new System.Windows.Forms.TrackBar();
            this.mArcCheckBox = new System.Windows.Forms.CheckBox();
            this.mPointSizeTextBox = new System.Windows.Forms.TextBox();
            this.mLineResolutionTrack = new System.Windows.Forms.TrackBar();
            this.mNormalRadioButton = new System.Windows.Forms.RadioButton();
            this.mRedBlueRadioButton = new System.Windows.Forms.RadioButton();
            this.mStereoscopicRadioButton = new System.Windows.Forms.RadioButton();
            this.mPrintRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.mDebugCheckBox = new System.Windows.Forms.CheckBox();
            this.zf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.zfTrackBar = new System.Windows.Forms.TrackBar();
            this.mSwitchCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mEyesTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.quickModeCheckBox = new System.Windows.Forms.CheckBox();
            this.mHiddenLineCheckBox = new System.Windows.Forms.CheckBox();
            this.mVectorsCheckBox = new System.Windows.Forms.CheckBox();
            this.mSwitchBackFrontCheckBox = new System.Windows.Forms.CheckBox();
            this.mOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mOpenButton = new System.Windows.Forms.Button();
            this.mFilesComboBox = new System.Windows.Forms.ComboBox();
            this.mView = new ScratchView.View();
            this.mGenerateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mViewAngleTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mArcSweepAngleTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLineResolutionTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zfTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEyesTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mViewAngleTrack
            // 
            this.mViewAngleTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mViewAngleTrack.Location = new System.Drawing.Point(13, 12);
            this.mViewAngleTrack.Maximum = 90;
            this.mViewAngleTrack.Minimum = -90;
            this.mViewAngleTrack.Name = "mViewAngleTrack";
            this.mViewAngleTrack.Size = new System.Drawing.Size(814, 45);
            this.mViewAngleTrack.TabIndex = 1;
            this.mViewAngleTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mViewAngleTrack.ValueChanged += new System.EventHandler(this.mViewAngleTrack_ValueChanged);
            this.mViewAngleTrack.Scroll += new System.EventHandler(this.mViewAngleTrack_Scroll);
            this.mViewAngleTrack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mViewAngleTrack_KeyDown);
            // 
            // mArcSweepAngleTrack
            // 
            this.mArcSweepAngleTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mArcSweepAngleTrack.Location = new System.Drawing.Point(835, 79);
            this.mArcSweepAngleTrack.Maximum = 180;
            this.mArcSweepAngleTrack.Name = "mArcSweepAngleTrack";
            this.mArcSweepAngleTrack.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mArcSweepAngleTrack.Size = new System.Drawing.Size(45, 162);
            this.mArcSweepAngleTrack.TabIndex = 2;
            this.mArcSweepAngleTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mArcSweepAngleTrack.Value = 180;
            this.mArcSweepAngleTrack.Scroll += new System.EventHandler(this.mArcSweepAngleTrack_Scroll);
            // 
            // mArcCheckBox
            // 
            this.mArcCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mArcCheckBox.AutoSize = true;
            this.mArcCheckBox.Location = new System.Drawing.Point(833, 12);
            this.mArcCheckBox.Name = "mArcCheckBox";
            this.mArcCheckBox.Size = new System.Drawing.Size(42, 17);
            this.mArcCheckBox.TabIndex = 3;
            this.mArcCheckBox.Text = "Arc";
            this.mArcCheckBox.UseVisualStyleBackColor = true;
            this.mArcCheckBox.CheckedChanged += new System.EventHandler(this.mArcCheckBox_CheckedChanged);
            // 
            // mPointSizeTextBox
            // 
            this.mPointSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mPointSizeTextBox.Location = new System.Drawing.Point(847, 658);
            this.mPointSizeTextBox.Name = "mPointSizeTextBox";
            this.mPointSizeTextBox.Size = new System.Drawing.Size(28, 20);
            this.mPointSizeTextBox.TabIndex = 5;
            this.mPointSizeTextBox.Text = "5";
            this.mPointSizeTextBox.TextChanged += new System.EventHandler(this.mPointSizeTextBox_TextChanged);
            // 
            // mLineResolutionTrack
            // 
            this.mLineResolutionTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mLineResolutionTrack.Location = new System.Drawing.Point(12, 614);
            this.mLineResolutionTrack.Maximum = 2000;
            this.mLineResolutionTrack.Minimum = 1;
            this.mLineResolutionTrack.Name = "mLineResolutionTrack";
            this.mLineResolutionTrack.Size = new System.Drawing.Size(548, 45);
            this.mLineResolutionTrack.TabIndex = 6;
            this.mLineResolutionTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mLineResolutionTrack.Value = 800;
            this.mLineResolutionTrack.Scroll += new System.EventHandler(this.mLineResolutionTrack_Scroll);
            // 
            // mNormalRadioButton
            // 
            this.mNormalRadioButton.AutoSize = true;
            this.mNormalRadioButton.Checked = true;
            this.mNormalRadioButton.Location = new System.Drawing.Point(6, 19);
            this.mNormalRadioButton.Name = "mNormalRadioButton";
            this.mNormalRadioButton.Size = new System.Drawing.Size(58, 17);
            this.mNormalRadioButton.TabIndex = 7;
            this.mNormalRadioButton.TabStop = true;
            this.mNormalRadioButton.Text = "Normal";
            this.mNormalRadioButton.UseVisualStyleBackColor = true;
            this.mNormalRadioButton.CheckedChanged += new System.EventHandler(this.mViewModeRadioButton_CheckedChanged);
            // 
            // mRedBlueRadioButton
            // 
            this.mRedBlueRadioButton.AutoSize = true;
            this.mRedBlueRadioButton.Location = new System.Drawing.Point(6, 42);
            this.mRedBlueRadioButton.Name = "mRedBlueRadioButton";
            this.mRedBlueRadioButton.Size = new System.Drawing.Size(71, 17);
            this.mRedBlueRadioButton.TabIndex = 8;
            this.mRedBlueRadioButton.Text = "Red/Blue";
            this.mRedBlueRadioButton.UseVisualStyleBackColor = true;
            this.mRedBlueRadioButton.CheckedChanged += new System.EventHandler(this.mViewModeRadioButton_CheckedChanged);
            // 
            // mStereoscopicRadioButton
            // 
            this.mStereoscopicRadioButton.AutoSize = true;
            this.mStereoscopicRadioButton.Location = new System.Drawing.Point(6, 65);
            this.mStereoscopicRadioButton.Name = "mStereoscopicRadioButton";
            this.mStereoscopicRadioButton.Size = new System.Drawing.Size(87, 17);
            this.mStereoscopicRadioButton.TabIndex = 9;
            this.mStereoscopicRadioButton.Text = "Stereoscopic";
            this.mStereoscopicRadioButton.UseVisualStyleBackColor = true;
            this.mStereoscopicRadioButton.CheckedChanged += new System.EventHandler(this.mViewModeRadioButton_CheckedChanged);
            // 
            // mPrintRadioButton
            // 
            this.mPrintRadioButton.AutoSize = true;
            this.mPrintRadioButton.Location = new System.Drawing.Point(6, 87);
            this.mPrintRadioButton.Name = "mPrintRadioButton";
            this.mPrintRadioButton.Size = new System.Drawing.Size(46, 17);
            this.mPrintRadioButton.TabIndex = 11;
            this.mPrintRadioButton.TabStop = true;
            this.mPrintRadioButton.Text = "Print";
            this.mPrintRadioButton.UseVisualStyleBackColor = true;
            this.mPrintRadioButton.CheckedChanged += new System.EventHandler(this.mViewModeRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(768, 662);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Point diameter:";
            // 
            // mDebugCheckBox
            // 
            this.mDebugCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mDebugCheckBox.AutoSize = true;
            this.mDebugCheckBox.Location = new System.Drawing.Point(833, 35);
            this.mDebugCheckBox.Name = "mDebugCheckBox";
            this.mDebugCheckBox.Size = new System.Drawing.Size(58, 17);
            this.mDebugCheckBox.TabIndex = 15;
            this.mDebugCheckBox.Text = "Debug";
            this.mDebugCheckBox.UseVisualStyleBackColor = true;
            this.mDebugCheckBox.CheckedChanged += new System.EventHandler(this.mDebugCheckBox_CheckedChanged);
            // 
            // zf
            // 
            this.zf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zf.Location = new System.Drawing.Point(838, 435);
            this.zf.Name = "zf";
            this.zf.Size = new System.Drawing.Size(42, 20);
            this.zf.TabIndex = 21;
            this.zf.Text = "25";
            this.zf.TextChanged += new System.EventHandler(this.zf_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(835, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Arcs:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(833, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Zf:";
            // 
            // zfTrackBar
            // 
            this.zfTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zfTrackBar.Location = new System.Drawing.Point(838, 264);
            this.zfTrackBar.Maximum = 10000;
            this.zfTrackBar.Name = "zfTrackBar";
            this.zfTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.zfTrackBar.Size = new System.Drawing.Size(45, 165);
            this.zfTrackBar.TabIndex = 25;
            this.zfTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.zfTrackBar.Value = 2500;
            this.zfTrackBar.Scroll += new System.EventHandler(this.zfTrackBar_Scroll);
            // 
            // mSwitchCheckBox
            // 
            this.mSwitchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mSwitchCheckBox.AutoSize = true;
            this.mSwitchCheckBox.Location = new System.Drawing.Point(771, 614);
            this.mSwitchCheckBox.Name = "mSwitchCheckBox";
            this.mSwitchCheckBox.Size = new System.Drawing.Size(109, 17);
            this.mSwitchCheckBox.TabIndex = 26;
            this.mSwitchCheckBox.Text = "Switch Left/Right";
            this.mSwitchCheckBox.UseVisualStyleBackColor = true;
            this.mSwitchCheckBox.CheckedChanged += new System.EventHandler(this.mSwitchCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(836, 462);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Eyes:";
            // 
            // mEyesTrackBar
            // 
            this.mEyesTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mEyesTrackBar.Location = new System.Drawing.Point(838, 480);
            this.mEyesTrackBar.Maximum = 40;
            this.mEyesTrackBar.Name = "mEyesTrackBar";
            this.mEyesTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.mEyesTrackBar.Size = new System.Drawing.Size(45, 128);
            this.mEyesTrackBar.TabIndex = 28;
            this.mEyesTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.mEyesTrackBar.Value = 10;
            this.mEyesTrackBar.Scroll += new System.EventHandler(this.mEyesTrackBar_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mNormalRadioButton);
            this.groupBox1.Controls.Add(this.mRedBlueRadioButton);
            this.groupBox1.Controls.Add(this.mStereoscopicRadioButton);
            this.groupBox1.Controls.Add(this.mPrintRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(566, 597);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(97, 114);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.quickModeCheckBox);
            this.groupBox2.Controls.Add(this.mHiddenLineCheckBox);
            this.groupBox2.Controls.Add(this.mVectorsCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(669, 598);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 113);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View";
            // 
            // quickModeCheckBox
            // 
            this.quickModeCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quickModeCheckBox.AutoSize = true;
            this.quickModeCheckBox.Checked = true;
            this.quickModeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.quickModeCheckBox.Location = new System.Drawing.Point(6, 85);
            this.quickModeCheckBox.Name = "quickModeCheckBox";
            this.quickModeCheckBox.Size = new System.Drawing.Size(84, 17);
            this.quickModeCheckBox.TabIndex = 23;
            this.quickModeCheckBox.Text = "Quick Mode";
            this.quickModeCheckBox.UseVisualStyleBackColor = true;
            this.quickModeCheckBox.CheckedChanged += new System.EventHandler(this.quickModeCheckBox_CheckedChanged);
            // 
            // mHiddenLineCheckBox
            // 
            this.mHiddenLineCheckBox.AutoSize = true;
            this.mHiddenLineCheckBox.Checked = true;
            this.mHiddenLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mHiddenLineCheckBox.Location = new System.Drawing.Point(7, 16);
            this.mHiddenLineCheckBox.Name = "mHiddenLineCheckBox";
            this.mHiddenLineCheckBox.Size = new System.Drawing.Size(83, 17);
            this.mHiddenLineCheckBox.TabIndex = 3;
            this.mHiddenLineCheckBox.Text = "Hidden Line";
            this.mHiddenLineCheckBox.UseVisualStyleBackColor = true;
            this.mHiddenLineCheckBox.CheckedChanged += new System.EventHandler(this.mHiddenLineCheckBox_CheckedChanged);
            // 
            // mVectorsCheckBox
            // 
            this.mVectorsCheckBox.AutoSize = true;
            this.mVectorsCheckBox.Checked = true;
            this.mVectorsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mVectorsCheckBox.Location = new System.Drawing.Point(7, 39);
            this.mVectorsCheckBox.Name = "mVectorsCheckBox";
            this.mVectorsCheckBox.Size = new System.Drawing.Size(62, 17);
            this.mVectorsCheckBox.TabIndex = 2;
            this.mVectorsCheckBox.Text = "Vectors";
            this.mVectorsCheckBox.UseVisualStyleBackColor = true;
            this.mVectorsCheckBox.CheckedChanged += new System.EventHandler(this.mVectorsCheckBox_CheckedChanged);
            // 
            // mSwitchBackFrontCheckBox
            // 
            this.mSwitchBackFrontCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mSwitchBackFrontCheckBox.AutoSize = true;
            this.mSwitchBackFrontCheckBox.Location = new System.Drawing.Point(771, 637);
            this.mSwitchBackFrontCheckBox.Name = "mSwitchBackFrontCheckBox";
            this.mSwitchBackFrontCheckBox.Size = new System.Drawing.Size(109, 17);
            this.mSwitchBackFrontCheckBox.TabIndex = 32;
            this.mSwitchBackFrontCheckBox.Text = "Swith Back/Front";
            this.mSwitchBackFrontCheckBox.UseVisualStyleBackColor = true;
            this.mSwitchBackFrontCheckBox.CheckedChanged += new System.EventHandler(this.mSwitchBackFrontCheckBox_CheckedChanged);
            // 
            // mOpenFileDialog
            // 
            this.mOpenFileDialog.Filter = "(*.x3d)|*.x3d|All files (*.*)|*.*\"";
            this.mOpenFileDialog.InitialDirectory = "c:\\Program Files\\Blender Foundation\\Blender";
            this.mOpenFileDialog.Title = "Select an x3d file";
            this.mOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.mOpenFileDialog_FileOk);
            // 
            // mOpenButton
            // 
            this.mOpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mOpenButton.Location = new System.Drawing.Point(13, 665);
            this.mOpenButton.Name = "mOpenButton";
            this.mOpenButton.Size = new System.Drawing.Size(75, 23);
            this.mOpenButton.TabIndex = 33;
            this.mOpenButton.Text = "Open";
            this.mOpenButton.UseVisualStyleBackColor = true;
            this.mOpenButton.Click += new System.EventHandler(this.mOpenButton_Click);
            // 
            // mFilesComboBox
            // 
            this.mFilesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mFilesComboBox.FormattingEnabled = true;
            this.mFilesComboBox.Location = new System.Drawing.Point(94, 667);
            this.mFilesComboBox.Name = "mFilesComboBox";
            this.mFilesComboBox.Size = new System.Drawing.Size(133, 21);
            this.mFilesComboBox.TabIndex = 34;
            this.mFilesComboBox.SelectedIndexChanged += new System.EventHandler(this.mFilesComboBox_SelectedIndexChanged);
            // 
            // mView
            // 
            this.mView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mView.ArcAngleResolution = 10F;
            this.mView.ArcSweepAngle = 90F;
            this.mView.BackColor = System.Drawing.Color.White;
            this.mView.Location = new System.Drawing.Point(13, 63);
            this.mView.Name = "mView";
            this.mView.PaddingPercent = 0.1;
            this.mView.PointWidth = 5;
            this.mView.RotateCanvas = false;
            this.mView.ShowArcs = true;
            this.mView.Size = new System.Drawing.Size(814, 528);
            this.mView.StereoscopicDisparityAngle = 8;
            this.mView.StereoscopicMode = ScratchUtility.StereoscopicMode.NonStereoscopic;
            this.mView.SwitchBackFront = false;
            this.mView.SwitchLeftRight = false;
            this.mView.TabIndex = 29;
            this.mView.VectorMode = true;
            this.mView.ViewAngle = 0;
            this.mView.ViewMode = ViewSupport.ViewMode.Normal;
            this.mView.ViewPointsPerUnitLength = 0;
            this.mView.VisibilityMode = ViewSupport.VisibilityMode.Transparent;
            // 
            // mGenerateButton
            // 
            this.mGenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mGenerateButton.Location = new System.Drawing.Point(370, 665);
            this.mGenerateButton.Name = "mGenerateButton";
            this.mGenerateButton.Size = new System.Drawing.Size(75, 23);
            this.mGenerateButton.TabIndex = 35;
            this.mGenerateButton.Text = "Generate";
            this.mGenerateButton.UseVisualStyleBackColor = true;
            this.mGenerateButton.Click += new System.EventHandler(this.mGenerateButton_Click);
            // 
            // ScratchTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 723);
            this.Controls.Add(this.mGenerateButton);
            this.Controls.Add(this.mFilesComboBox);
            this.Controls.Add(this.mOpenButton);
            this.Controls.Add(this.mSwitchBackFrontCheckBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mView);
            this.Controls.Add(this.mEyesTrackBar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mSwitchCheckBox);
            this.Controls.Add(this.zfTrackBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zf);
            this.Controls.Add(this.mDebugCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mLineResolutionTrack);
            this.Controls.Add(this.mPointSizeTextBox);
            this.Controls.Add(this.mArcCheckBox);
            this.Controls.Add(this.mArcSweepAngleTrack);
            this.Controls.Add(this.mViewAngleTrack);
            this.Name = "ScratchTest";
            this.Text = "Scratch Test";
            ((System.ComponentModel.ISupportInitialize)(this.mViewAngleTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mArcSweepAngleTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLineResolutionTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zfTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEyesTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar mViewAngleTrack;
        private System.Windows.Forms.TrackBar mArcSweepAngleTrack;
        private System.Windows.Forms.CheckBox mArcCheckBox;
        private System.Windows.Forms.TextBox mPointSizeTextBox;
        private System.Windows.Forms.TrackBar mLineResolutionTrack;
        private System.Windows.Forms.RadioButton mNormalRadioButton;
        private System.Windows.Forms.RadioButton mRedBlueRadioButton;
        private System.Windows.Forms.RadioButton mStereoscopicRadioButton;
        private System.Windows.Forms.RadioButton mPrintRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox mDebugCheckBox;
        private System.Windows.Forms.TextBox zf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar zfTrackBar;
        private System.Windows.Forms.CheckBox mSwitchCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar mEyesTrackBar;
        private ScratchView.View mView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox mSwitchBackFrontCheckBox;
        private System.Windows.Forms.OpenFileDialog mOpenFileDialog;
        private System.Windows.Forms.Button mOpenButton;
        private System.Windows.Forms.ComboBox mFilesComboBox;
        private System.Windows.Forms.CheckBox mVectorsCheckBox;
        private System.Windows.Forms.CheckBox mHiddenLineCheckBox;
        private System.Windows.Forms.CheckBox quickModeCheckBox;
        private System.Windows.Forms.Button mGenerateButton;

    }
}

