using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Primitives;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ViewSupport;
using ScratchUtility;

namespace ScratchView
{

    public partial class View : UserControl
    {
        private double mPaddingPercent = .1;

        private Point mLastMousePosition = Drawing.NullPoint;


        public View()
        {
            InitializeComponent();

            Drawing.SceneChanged += new SceneChangedHandler(Drawing_SceneChanged);

            Global.DesignMode = DesignMode;

            if(!DesignMode)
                ViewContext.CanvasSize = this.Size;
        }


        //public double LongestArcRadiusPrintMode
        //{
        //    get { return mLongestArcRadiusPrintMode; }
        //    set
        //    {
        //        if (mLongestArcRadiusPrintMode != value)
        //        {
        //            mLongestArcRadiusPrintMode = value;
        //            if(mViewMode == ViewModes.Print)
        //                Invalidate();
        //        }
        //    }
        //}
        public double PaddingPercent
        {
            get { return mPaddingPercent; }
            set
            {
                if (mPaddingPercent != value)
                {
                    mPaddingPercent = value;
                    Invalidate();
                }
            }
        }
        /// <summary>Gets a Rectangle the side of the ClientRectangle shrunk by the PaddingPercent.</summary>
        public Rectangle FitWithin
        {
            get
            {
                Rectangle fitWithin = ClientRectangle;
                fitWithin.Inflate(-(int)(fitWithin.Width * PaddingPercent), -(int)(fitWithin.Height * PaddingPercent));
                return fitWithin;
            }
        }



        //public bool DrawingEnabled { get { return Drawing.Enabled; } set { Drawing.Enabled = value; } }

        #region DrawOption Properties


        public ViewMode ViewMode { get { return DrawOptions.ViewMode; } set { DrawOptions.ViewMode = value; } }
        public bool SwitchLeftRight { get { return DrawOptions.SwitchLeftRight; } set { DrawOptions.SwitchLeftRight = value; } }
        public float ArcSweepAngle { get { return DrawOptions.ArcSweepAngle; } set { DrawOptions.ArcSweepAngle = value; } }
        public float ArcAngleResolution { get { return DrawOptions.ArcAngleResolution; } set { DrawOptions.ArcAngleResolution = value; } }
        public double PointWidth { get { return DrawOptions.PointWidth; } set { DrawOptions.PointWidth = value; } }
        public bool ShowArcs { get { return DrawOptions.ShowArcs; } set { DrawOptions.ShowArcs = value; } }
        public bool VectorMode { get { return DrawOptions.VectorMode; } set { DrawOptions.VectorMode = value; } }
        public VisibilityMode VisibilityMode { get { return DrawOptions.VisibilityMode; } set { DrawOptions.VisibilityMode = value; } }
        public bool RotateCanvas { get { return DrawOptions.RotateCanvas; } set { DrawOptions.RotateCanvas = value; } }
        public double ViewPointsPerUnitLength { get { return DrawOptions.ViewPointsPerUnitLength; } set { DrawOptions.ViewPointsPerUnitLength = value; } }
        public bool SwitchBackFront { get { return DrawOptions.SwitchBackFront; } set { DrawOptions.SwitchBackFront = value; } }

        #endregion

        #region ViewContext Properties

        public StereoscopicMode StereoscopicMode { get { return ViewContext.StereoscopicMode; } set { ViewContext.StereoscopicMode = value; } }
        public double ViewAngle { get { return ViewContext.ViewAngle; } set { ViewContext.ViewAngle = value; } }
        public double StereoscopicDisparityAngle { get { return ViewContext.StereoscopicDisparityAngle; } set { ViewContext.StereoscopicDisparityAngle = value; } }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!DesignMode)
            {
                e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
                Drawing.Blit(e.Graphics);
            }
        }

        private void Drawing_SceneChanged()
        {
            if (!DesignMode)
            {
                Invalidate();
            }
        }

        private void View_Resize(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (Width > 0 && Height > 0)
                {
                    Drawing.CanvasSize = this.Size;
                }
            }
        }

        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                //prepare for the user to drag by storing the current mouse location.
                mLastMousePosition = e.Location;
            }
        }

        private void View_MouseUp(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                //the user is done dragging.
                mLastMousePosition = Drawing.NullPoint;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!DesignMode)
            {
                base.OnMouseWheel(e);
                if (ModifierKeys == Keys.Control || ModifierKeys == (Keys.Control | Keys.Shift))
                {
                    double flyAmount = (e.Delta < 0) ? -1 : 1;
                    if (ViewContext.SlowNavigation)
                        flyAmount /= 10;
                    ViewContext.Fly(flyAmount);
                }
                else
                {
                    if (MouseButtons == MouseButtons.Left)
                    {
                        double zoomAmount = (e.Delta < 0) ? -1 : 1;
                        if (ViewContext.SlowNavigation)
                            zoomAmount /= 10;
                        ViewContext.Zoom(zoomAmount);
                    }
                    else if (MouseButtons == MouseButtons.None)
                    {
                        double scaleAmount = (e.Delta < 0) ? .9 : -1.1;
                        if (ViewContext.SlowNavigation)
                            scaleAmount += .09;
                        ViewContext.Scale(Math.Abs(scaleAmount));
                    }
                }
            }
        }
        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.Button != MouseButtons.None)
                {
                    /*
                     * left mouse = orbit
                     * middle mouse (or both left and right at same time) = pan
                     * scroll = zoom
                     * right mouse = look around
                     */
                    //Point deltaMousePosition = e.Location;
                    if (mLastMousePosition != Drawing.NullPoint)
                    {
                        PointD deltaMousePosition = DrawOptions.RotateCanvas ? GetDelta(mLastMousePosition, e.Location) : GetDelta(e.Location, mLastMousePosition);

                        if (e.Button == MouseButtons.Left)
                            Orbit(deltaMousePosition);
                        else if (e.Button == (MouseButtons.Left | MouseButtons.Right) || e.Button == MouseButtons.Middle)
                            Pan(mLastMousePosition, e.Location);
                        else if (e.Button == MouseButtons.Right)
                            LookAround(deltaMousePosition);
                    }


                    mLastMousePosition = e.Location;
                }
            }
        }

        private PointD GetDelta(Point start, Point end)
        {
            if (ViewContext.SlowNavigation)
                return new PointD((end.X - start.X) / 10.0, (end.Y - start.Y) / 10.0);
            return new PointD(end.X - start.X, end.Y - start.Y);
        }

        private void Pan(Point lastMouseClick, Point currentMouseClick)
        {
            double multiplier = 1;
            Coord lastMouse = new Coord(lastMouseClick.X * multiplier, lastMouseClick.Y * multiplier, 0);
            Coord currentMouse = new Coord(currentMouseClick.X * multiplier, currentMouseClick.Y * multiplier, 0);
            ViewContext.Pan(lastMouse, currentMouse);
        }
        private void Orbit(PointD deltaMousePosition)
        {
            Coord newPoLocation_ViewCoordinates = new Coord((deltaMousePosition.X * ViewContext.N.Length / 2) + (Width / 2), (deltaMousePosition.Y * ViewContext.N.Length / 2) + (Height / 2), 0);
            ViewContext.Orbit(newPoLocation_ViewCoordinates);
        }
        private void LookAround(PointD deltaMousePosition)
        {
            //deltaMousePosition will be a small increment.
            Coord newPrLocation_ViewCoordinates = new Coord((deltaMousePosition.X * ViewContext.N.Length / 2) + (Width / 2), (deltaMousePosition.Y * ViewContext.N.Length / 2) + (Height / 2), 0);
            ViewContext.LookAround(newPrLocation_ViewCoordinates);
        }





        public void SetPo(double x, double y, double z)
        {
            ViewContext.Po = new Coord(x, y, z);
        }
        public void SetPo(Coord c)
        {
            ViewContext.Po = c;
        }
        public void SetZf(double zf)
        {
            ViewContext.Zf = zf;
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!DesignMode)
            {
                if (e.KeyChar == 'r' || e.KeyChar == 'R')
                    ViewContext.ResetCamera();
                else if (e.KeyChar == 'a' || e.KeyChar == 'A')
                    DrawOptions.CanvasCutoffMode = CanvasCutoffMode.NoCutoff;
                else if (e.KeyChar == 'f' || e.KeyChar == 'F')
                    ToggleCanvasCutoffMode(CanvasCutoffMode.ShowInFrontOnly);
                else if (e.KeyChar == 'b' || e.KeyChar == 'B')
                    ToggleCanvasCutoffMode(CanvasCutoffMode.ShowBehindOnly);
                else if (e.KeyChar == 'c' || e.KeyChar == 'C')
                    ToggleCanvasCutoffMode(CanvasCutoffMode.ToggleColor);
            }
        }
        private void ToggleCanvasCutoffMode(CanvasCutoffMode modeToToggle)
        {
            if (DrawOptions.CanvasCutoffMode == modeToToggle)
                DrawOptions.CanvasCutoffMode = CanvasCutoffMode.NoCutoff;
            else
                DrawOptions.CanvasCutoffMode = modeToToggle;
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (!DesignMode)
            {
                //if (e.KeyCode == Keys.Menu) //alt
                //    ViewContext.NavigationMode = NavigationMode.UserView;
                //else if (e.KeyCode == Keys.ControlKey)
                //    ViewContext.NavigationMode = NavigationMode.UserView;
                //else 
                    if (e.KeyCode == Keys.ShiftKey)
                    ViewContext.SlowNavigation = false;
            }
        }

        private void View_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!DesignMode)
            {
                //if (e.KeyCode == Keys.Menu) //alt
                //    ViewContext.NavigationMode = NavigationMode.CanvasViewShowBehind;
                //else if (e.KeyCode == Keys.ControlKey)
                //    ViewContext.NavigationMode = NavigationMode.CanvasViewShowInFront;
                //else 
                    if (e.KeyCode == Keys.ShiftKey)
                    ViewContext.SlowNavigation = true;
            }
        }

        public void AddShape(IndexedFaceSet ifs)
        {
            Drawing.AddShape(ifs);
        }

        public void ClearShapes()
        {
            Drawing.ClearShapes();
        }


        public void PreProcessShapes()
        {
            Drawing.PreProcessShapes();
            ViewContext.Pr = new Coord(0, 0, 0);
            Drawing.DoDraw = true;
        }
    }
}
