using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Primitives;
using ScratchUtility;
using System.IO;
using System.Xml;
using FileParser;

namespace ScratchTest
{
    public partial class ScratchTest : Form
    {
        public ScratchTest()
        {
            InitializeComponent();

            //halt drawing until we're all set up
            //mView.DrawingEnabled = false;
            mView.ArcSweepAngle = mArcSweepAngleTrack.Value;
            mView.ViewAngle = mViewAngleTrack.Value;
            mView.ShowArcs = mArcCheckBox.Checked;
            mView.RotateCanvas = mDebugCheckBox.Checked;
            SetPointsPerUnitLength();
            ViewSupport.DrawOptions.QuickMode = quickModeCheckBox.Checked;
            SetVisibilityMode();
            mView.SwitchLeftRight = mSwitchCheckBox.Checked;
            TrySetPointWidth();
            SetUpFileList(@"C:\Program Files\Blender Foundation\Blender");
            //begin drawing
            //mView.DrawingEnabled = true;
        }


        public void SetVisibilityMode()
        {
            mView.VisibilityMode = mHiddenLineCheckBox.Checked ? ViewSupport.VisibilityMode.HiddenLine : ViewSupport.VisibilityMode.Transparent;
        }



        private void mArcSweepAngleTrack_Scroll(object sender, EventArgs e)
        {
            if(!DesignMode)
                mView.ArcSweepAngle = mArcSweepAngleTrack.Value;
        }

        private void mViewAngleTrack_Scroll(object sender, EventArgs e)
        {
        }
        private void mViewAngleTrack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
                mViewAngleTrack.Value = 0;
        }

        private void mViewAngleTrack_ValueChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                mView.ViewAngle = mViewAngleTrack.Value;
        }

        private void mLineResolutionTrack_Scroll(object sender, EventArgs e)
        {
            if (!DesignMode)
                SetPointsPerUnitLength();
        }

        private void SetPointsPerUnitLength()
        {
            mView.ViewPointsPerUnitLength = mLineResolutionTrack.Value / 100.0;
        }

        private void mArcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                mView.ShowArcs = mArcCheckBox.Checked;
        }

        private void mDebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                Global.DebugMode = mDebugCheckBox.Checked;
        }
        private void mPointSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                TrySetPointWidth();
        }
        private void TrySetPointWidth()
        {
            int size = 0;
            if (int.TryParse(mPointSizeTextBox.Text, out size) || mPointSizeTextBox.Text.Length == 0)
            {
                mView.PointWidth = size;
            }
            else
            {
                mPointSizeTextBox.Text = mView.PointWidth.ToString();
            }
        }

        private void mViewModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (mPrintRadioButton.Checked)
                {
                    mView.ViewMode = ViewSupport.ViewMode.Print;
                }
                else
                {
                    if (mRedBlueRadioButton.Checked)
                        mView.ViewMode = ViewSupport.ViewMode.RedBlue;
                    else if (mStereoscopicRadioButton.Checked)
                        mView.ViewMode = ViewSupport.ViewMode.Stereoscopic;
                    else
                        mView.ViewMode = ViewSupport.ViewMode.Normal;
                }
            }
        }
        private void mVisibilityMode_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked)
                SetVisibilityMode();
        }


        private string GetFaceString(params int[] coordIndices)
        {
            string ret = "";
            foreach (int i in coordIndices)
            {
                ret += i.ToString() + " ";
            }
            return ret + "-1, ";
        }
        private string GetCoordString(params Coord[] coords)
        {
            string ret = "";
            foreach (Coord c in coords)
            {
                ret += CoordString(c) + " ";
            }
            return ret;
        }

        private string GetCubeCoordString(double xWidth, double yWidth, double zWidth, double xOffset, double yOffset, double zOffset)
        {
            string ret = "";
            ret += CoordString(new Coord(0 + xOffset, yWidth + yOffset, 0 + zOffset)); //topBack
            ret += CoordString(new Coord(xWidth + xOffset, yWidth + yOffset, 0 + zOffset)); //topRight
            ret += CoordString(new Coord(xWidth + xOffset, yWidth + yOffset, zWidth + zOffset)); //topFront
            ret += CoordString(new Coord(0 + xOffset, yWidth + yOffset, zWidth + zOffset)); //topLeft
            ret += CoordString(new Coord(0 + xOffset, 0 + yOffset, 0 + zOffset)); //bottomBack
            ret += CoordString(new Coord(xWidth + xOffset, 0 + yOffset, 0 + zOffset)); //bottomRight
            ret += CoordString(new Coord(xWidth + xOffset, 0 + yOffset, zWidth + zOffset)); //bottomFront
            ret += CoordString(new Coord(0 + xOffset, 0 + yOffset, zWidth + zOffset)); //bottomLeft
            return ret;
        }

        private string CoordString(Coord c)
        {
            return c.X + " " + c.Y + " " + c.Z + ", ";
        }


        private void LoadX3DFile(X3DFile file, double scale)
        {
            file.Parse(scale);
            mView.ClearShapes();
            foreach (IndexedFaceSet ifs in file.IndexedFaces)
            {
                mView.AddShape(ifs);
            }
            mView.PreProcessShapes();
            //mView.SetPo(file.CameraPosition);
            
        }


        private void zf_TextChanged(object sender, EventArgs e)
        {
            if(zf.Text == "")
            {
                mView.SetZf(0);
                zfTrackBar.Value = 0;
            }
            else
            {
                mView.SetZf(double.Parse(zf.Text));
                zfTrackBar.Value = (int)(double.Parse(zf.Text) * 100);
            }
        }

        private void zfTrackBar_Scroll(object sender, EventArgs e)
        {
            zf.Text = (zfTrackBar.Value / 100.0).ToString();
        }

        private void mSwitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mView.SwitchLeftRight = mSwitchCheckBox.Checked;
        }

        private void mEyesTrackBar_Scroll(object sender, EventArgs e)
        {
            mView.StereoscopicDisparityAngle = mEyesTrackBar.Value / 2.0;
        }

        private void mSwitchBackFrontCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            mView.SwitchBackFront = mSwitchBackFrontCheckBox.Checked;
        }

        private void mOpenButton_Click(object sender, EventArgs e)
        {
            mOpenFileDialog.ShowDialog();
        }



        private void mOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            HandleOpenFile(mOpenFileDialog.FileName);
        }

        private void HandleOpenFile(string fileName)
        {
            SetUpFileList(Path.GetDirectoryName(fileName), fileName);
        }

        private void SetUpFileList(string directory, string selectFileName)
        {
            mFilesComboBox.Items.Clear();
            foreach (string fileName in Directory.GetFiles(directory, "*.x3d"))
            {
                X3DFile f = new X3DFile(fileName);
                mFilesComboBox.Items.Add(f);
                if (fileName == selectFileName)
                    mFilesComboBox.SelectedIndex = mFilesComboBox.Items.Count - 1;
            }
        }
        private void SetUpFileList(string directory)
        {
            SetUpFileList(directory, null);
        }

        private void mFilesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadX3DFile((X3DFile)(mFilesComboBox.SelectedItem), 1);
        }

        private void mVectorsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!DesignMode)
                mView.VectorMode = mVectorsCheckBox.Checked;
        }

        private void quickModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ViewSupport.DrawOptions.QuickMode = quickModeCheckBox.Checked;
        }
        private void mHiddenLineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetVisibilityMode();
        }

        private void mGenerateButton_Click(object sender, EventArgs e)
        {
            //mView.GenerateArcs();
        }


    }

    
}
