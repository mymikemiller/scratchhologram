using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchUtility;

namespace ViewSupport
{
    public enum ViewMode { Normal, RedBlue, Stereoscopic, Print }
    public enum VisibilityMode { Transparent, HiddenLine }
    
    /// <summary>Specifies whether or not points in front of / behind the canvas should be shown, or if they should be colored differently.</summary>
    public enum CanvasCutoffMode { NoCutoff, ShowBehindOnly, ShowInFrontOnly, ToggleColor }

    public delegate void DrawOptionChangedHandler(ScratchUtility.RedrawRequiredEventArgs e);

    public class DrawOptions
    {
        //Options requiring no recalculation, just redraw
        private static ViewMode mViewMode = ViewMode.Normal;
        private static CanvasCutoffMode mCanvasCutoffMode = CanvasCutoffMode.NoCutoff;
        private static bool mSwitchLeftRight = false;
        private static bool mSwitchBackFront = false;
        private static double mPointWidth = 5; 
        private static bool mShowArcs = true;
        private static bool mVectorMode = true;
        private static VisibilityMode mVisibilityMode = VisibilityMode.Transparent;
        private static bool mRotateCanvas = false;
        private static bool mQuickMode = true;


        //Options requiring recalculation of viewpoints
        private static double mViewPointsPerUnitLength = 0;

        //Options requiring only recalculation of arcs
        private static float mArcSweepAngle = 90;
        private static float mArcAngleResolution = 10;

        //Options that don't require a redraw
        public static bool ToggleVisibilityBasedOnArcAngles { get; set; }

        public static event DrawOptionChangedHandler DrawOptionChanged;

        static DrawOptions()
        {
            ToggleVisibilityBasedOnArcAngles = false;
        }

        /// <summary>Fires the even that lets listening classes know that an option has changed.</summary>
        private static void FireOptionChangedEvent(RedrawTypeRequired type)
        {
            if (!Drawing.CurrentlyDrawing)
            {
                if (DrawOptionChanged != null)
                    DrawOptionChanged(new RedrawRequiredEventArgs(type));
            }
        }
        private static void FireOptionChangedEvent()
        {
            FireOptionChangedEvent(RedrawTypeRequired.Redraw);
        }

        #region Properties
        public static VisibilityMode VisibilityMode
        {
            get
            {
                return mVisibilityMode;
            }
            set
            {
                if (mVisibilityMode == value)
                    return;
                mVisibilityMode = value;
                FireOptionChangedEvent();
            }
        }

        public static bool VectorMode
        {
            get
            {
                return mVectorMode;
            }
            set
            {
                if (mVectorMode == value)
                    return;
                mVectorMode = value;
                FireOptionChangedEvent();
            }
        }
        public static bool ShowArcs
        {
            get
            {
                return mShowArcs;
            }
            set
            {
                if (mShowArcs == value)
                    return;
                mShowArcs = value;
                FireOptionChangedEvent();
            }
        }
        
        public static double PointWidth
        {
            get
            {
                return mPointWidth;
            }
            set
            {
                if (mPointWidth == value)
                    return;
                mPointWidth = value;
                FireOptionChangedEvent();
            }
        }
        public static float ArcAngleResolution
        {
            get
            {
                return mArcAngleResolution;
            }
            set
            {
                if (mArcAngleResolution == value)
                    return;
                mArcAngleResolution = value;
                FireOptionChangedEvent(RedrawTypeRequired.RecalculateAllArcs);
            }
        }
        public static float ArcSweepAngle
        {
            get
            {
                return mArcSweepAngle;
            }
            set
            {
                if (mArcSweepAngle == value)
                    return;
                mArcSweepAngle = value;
                FireOptionChangedEvent(RedrawTypeRequired.RecalculateAllArcs);
            }
        }

        public static CanvasCutoffMode CanvasCutoffMode
        {
            get
            {
                return mCanvasCutoffMode;
            }
            set
            {
                if (mCanvasCutoffMode == value)
                    return;
                mCanvasCutoffMode = value;
                FireOptionChangedEvent(RedrawTypeRequired.Redraw);
            }
        }
        public static bool SwitchLeftRight
        {
            get
            {
                return mSwitchLeftRight;
            }
            set
            {
                if (mSwitchLeftRight == value)
                    return;
                mSwitchLeftRight = value;
                FireOptionChangedEvent();
            }
        }
        public static bool SwitchBackFront
        {
            get
            {
                return mSwitchBackFront;
            }
            set
            {
                if (mSwitchBackFront == value)
                    return;
                mSwitchBackFront = value;
                FireOptionChangedEvent(RedrawTypeRequired.RecalculateViewPrimitives);
            }
        }
        public static ViewMode ViewMode
        {
            get
            {
                return mViewMode;
            }
            set
            {
                if (mViewMode == value)
                    return;
                mViewMode = value;
                FireOptionChangedEvent();
            }
        }


        public static float VectorWidth
        {
            get { return Drawing.VectorWidth; }
            set { Drawing.VectorWidth = value; }
        }

        public static double ViewPointsPerUnitLength
        {
            get { return mViewPointsPerUnitLength; }
            set
            {
                if (mViewPointsPerUnitLength == value)
                    return;
                mViewPointsPerUnitLength = value;
                FireOptionChangedEvent(RedrawTypeRequired.RecalculateViewPrimitives);
            }
        }


        public static bool RotateCanvas
        {
            get { return mRotateCanvas; }
            set
            {
                if (mRotateCanvas == value)
                    return;

                mRotateCanvas = value;
                FireOptionChangedEvent();
            }
        }
        public static bool QuickMode
        {
            get { return mQuickMode; }
            set
            {
                if (mQuickMode == value)
                    return;

                mQuickMode = value;
                FireOptionChangedEvent();
            }
        }



        #endregion


    }
}
