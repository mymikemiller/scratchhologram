using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Primitives;
using System.Diagnostics;
using ScratchUtility;

namespace ViewSupport
{
    public delegate void SceneChangedHandler();


    public static class Drawing
    {
        /// <summary>Specifies whether or not this class should respond to events that would cause it to be redrawn. True (should not respond to events) if a drawing operation (onto the off-screen buffer) is already occuring.</summary>
        public static bool CurrentlyDrawing { get; set; }
        private static bool mDoDraw = false;

        public static RedrawTypeRequired NextRedraw { get; set; }

        internal static ShapeList Shapes { get; private set; }

        internal static Brush PointBrush { get; set; }
        internal static Brush PointBrush_BehindCanvas { get; set; }
        internal static Brush PointBrush_InFrontOfCanvas { get; set; }
        internal static Brush RedBlueModePointBrush { get; set; }
        internal static Pen VectorPen { get; set; }
        internal static Pen ArcPen { get; set; }

        public static Point NullPoint { get { return new Point(int.MaxValue, int.MaxValue); } }

        private static Bitmap mOffScreenBitmap;
        private static Graphics mOffScreenGraphics;
        
        public static event SceneChangedHandler SceneChanged;

        static Drawing()
        {
            Shapes = new ShapeList();

            //todo: properly dispose of these at end of program
            PointBrush = Brushes.Blue;
            PointBrush_BehindCanvas = Brushes.DarkViolet;
            PointBrush_InFrontOfCanvas = Brushes.Blue;
            RedBlueModePointBrush = Brushes.Black;
            VectorPen = new Pen(Color.Blue, VectorWidth);
            ArcPen = new Pen(Color.Gray);

            mOffScreenBitmap = new Bitmap(10, 10);
            mOffScreenGraphics = Graphics.FromImage(mOffScreenBitmap);

            ViewContext.ViewChanged += new ViewChangedHandler(ViewContext_ViewChanged);
            DrawOptions.DrawOptionChanged += new DrawOptionChangedHandler(DrawOptions_DrawOptionChanged);
            CurrentlyDrawing = true;
        }

        private static void ViewContext_ViewChanged(RedrawRequiredEventArgs e)
        {
            RespondToRedrawRequired(e.RedrawTypeRequired);
        }
        private static void DrawOptions_DrawOptionChanged(RedrawRequiredEventArgs e)
        {
            RespondToRedrawRequired(e.RedrawTypeRequired);
        }

        private static void RespondToRedrawRequired(RedrawTypeRequired type)
        {
            NextRedraw = type;
            if (!CurrentlyDrawing)
                FireSceneChangedEvent();
        }
        ///<summary>Marks the offscreen buffer as dirty and fires the ScreenChanged event causing it to be redrawn by the host.</summary>
        public static void MarkAsDirty(RedrawTypeRequired type)
        {
            NextRedraw = type;
            FireSceneChangedEvent();
        }

        #region Properties

        //public static bool Enabled
        //{
        //    get { return mEnabled; }
        //    set
        //    {
        //        if (mEnabled == value)
        //            return;
        //        mEnabled = value;
        //        //mark as dirty if the user just toggled Enabled on.
        //        if (mEnabled)
        //        {
        //            ViewContext.RecalculateMatrix();
        //            MarkAsDirty(RedrawTypeRequired.RecalculateViewPrimitives);
        //        }
        //    }
        //}

        /// <summary>Specifies whether or not the screen should be drawn to.</summary>
        public static bool DoDraw
        {
            get
            {
                return mDoDraw;
            }
            set
            {
                if (mDoDraw == value)
                    return;
                mDoDraw = value;
                FireSceneChangedEvent();
            }
        }

        public static Size CanvasSize
        {
            get { return ViewContext.CanvasSize; }
            set
            {
                if (ViewContext.CanvasSize == value)
                    return;
                ViewContext.CanvasSize = value;
                mOffScreenBitmap = new Bitmap(CanvasSize.Width, CanvasSize.Height);
                mOffScreenGraphics = Graphics.FromImage(mOffScreenBitmap);
            }
        }
        public static float VectorWidth
        {
            get
            {
                if (VectorPen != null)
                    return VectorPen.Width;
                else
                    return 0;
            }
            set
            {
                if (VectorPen != null)
                    VectorPen.Width = value;
            }
        }
#endregion


        /// <summary>If Enabled is true, causes the internal ViewPrimitives list to be recalculated and redrawn. Returns true if the invalid and redraw events occurred successfully.</summary>
        //public static bool Invalidate()
        //{
        //    if (Busy)
        //        return false;

        //    //suppress events
        //    Busy = true;
        //    //Recalculate ViewPrimitives list (by creating a new one based on the Primitives list)
        //    ViewPrimitives = new ViewPrimitiveList(Primitives);
        //    ReDraw();

        //    //specify that we are now ready to respond to events
        //    Busy = false;
        //    return true;
        //}

        /// <summary>Redraws the current ViewPrimitives list onto the off screen buffer. Calling Blit() will blit it to the supplied graphics object. Returns true if the redraw occurred successfully.</summary>
        private static bool ReDraw()
        {
            //if (Enabled && NextRedraw != RedrawTypeRequired.None)
            if (NextRedraw != RedrawTypeRequired.None)
            {
                CurrentlyDrawing = true;
                if (NextRedraw == RedrawTypeRequired.RecalculateViewPrimitives)
                {
                    Shapes.Refresh(DrawOptions.SwitchBackFront, false);
                }
                else if (NextRedraw == RedrawTypeRequired.RecalculateAllArcs)
                {
                    Shapes.Refresh(DrawOptions.SwitchBackFront, true);
                }
                else if (NextRedraw == RedrawTypeRequired.RecalculateArcPositionsOnly)
                {
                    Shapes.Refresh(DrawOptions.SwitchBackFront, true);
                }

                switch (DrawOptions.ViewMode)
                {
                    case ViewMode.Normal:
                        mOffScreenGraphics.Clear(Color.White);
                        DrawNormal(mOffScreenGraphics);
                        break;
                    case ViewMode.RedBlue:
                        DrawRedBlue(mOffScreenGraphics);
                        break;
                    case ViewMode.Stereoscopic:
                        mOffScreenGraphics.Clear(Color.White);
                        DrawStereoscopic(mOffScreenGraphics);
                        break;
                    case ViewMode.Print:
                        mOffScreenGraphics.Clear(Color.White);
                        DrawPrint(mOffScreenGraphics);
                        break;
                }

                NextRedraw = RedrawTypeRequired.None;
                CurrentlyDrawing = false;





                //foreach (ViewPolygon p in ViewPolygons)
                //{
                //    mOffScreenGraphics.DrawRectangle(Pens.Black, Rectangle.Round(p.BoundingBox));
                //}


                FireSceneChangedEvent();
            }

            return true;
        }

        private static void FireSceneChangedEvent()
        {
            if (SceneChanged != null && DoDraw)
                SceneChanged();
        }
        private static void DrawNormal(Graphics g)
        {
            //if (DrawOptions.ShowArcs)
            //    ViewPrimitives.DrawArcs(g);

            if (Shapes.Count > 0)
            {
                EdgePainter.ShapeList = Shapes;
                EdgePainter.Draw(g);
            }

            

            //if (DrawOptions.ShowPoints)
            //{
            //    if (DrawOptions.CanvasCutoffMode != CanvasCutoffMode.ToggleColor)
            //    {
            //        ViewPrimitives.Draw(g);
            //    }
            //    else
            //    {
            //        //if we're supposed to be drawing different colors for in front vs. behind, we need to draw the behind points first, change the color, then draw the in front points
            //        Brush backupBrush = PointBrush;

            //        PointBrush = PointBrush_BehindCanvas;
            //        DrawOptions.CanvasCutoffMode = CanvasCutoffMode.ShowBehindOnly;
            //        ViewPrimitives.Draw(g);

            //        PointBrush = PointBrush_InFrontOfCanvas; ;
            //        DrawOptions.CanvasCutoffMode = CanvasCutoffMode.ShowInFrontOnly;
            //        ViewPrimitives.Draw(g);

            //        PointBrush = backupBrush;
            //        DrawOptions.CanvasCutoffMode = CanvasCutoffMode.ToggleColor;

            //    }
            //}
        }

        private static void DrawRedBlue(Graphics g)
        {
            bool buShowArcs = DrawOptions.ShowArcs;
            DrawOptions.ShowArcs = false;


            float transparency = .6f;
            float[][] mtrx = new float[5][] {
            new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f, transparency, 0.0f},
            new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}};


            ColorMatrix colorMatrix = new ColorMatrix(mtrx);
            using (ImageAttributes ia = new ImageAttributes())
            {
                ia.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                Color leftColor = DrawOptions.SwitchLeftRight ? Color.Red : Color.Cyan;
                Color rightColor = DrawOptions.SwitchLeftRight ? Color.Cyan : Color.Red;

                //The rightBitmap will be blitted onto g after the left image is drawn onto g
                Bitmap rightBitmap = new Bitmap(ViewContext.CanvasSize.Width, ViewContext.CanvasSize.Height);
                Graphics rightGraphics = Graphics.FromImage(rightBitmap);

                rightGraphics.Clear(rightColor);
                g.Clear(leftColor);

                ViewContext.StereoscopicMode = StereoscopicMode.Left; 
                Shapes.Refresh(DrawOptions.SwitchBackFront, true);
                    
                DrawNormal(g);

                ViewContext.StereoscopicMode = StereoscopicMode.Right;
                Shapes.Refresh(DrawOptions.SwitchBackFront, true);
                    
                DrawNormal(rightGraphics);
                ViewContext.StereoscopicMode = StereoscopicMode.NonStereoscopic;

                Shapes.Refresh(DrawOptions.SwitchBackFront, true);
                    


                g.DrawImage(rightBitmap, new Rectangle(0, 0, rightBitmap.Width, rightBitmap.Height), 0, 0, rightBitmap.Width, rightBitmap.Height, GraphicsUnit.Pixel, ia);
            }

            DrawOptions.ShowArcs = buShowArcs;
        }
        private static void DrawStereoscopic(Graphics g)
        {
            //because we're shrinking the drawing, we need to increase the point size.
            DrawOptions.PointWidth *= 1.5;
            //shrink and move the drawing to the left by manipulating g's TransformMatrix.

            //offset the first direction (default left)
            if (DrawOptions.SwitchLeftRight)
                TranslateGraphicsRight(g);
            else
                TranslateGraphicsLeft(g);

            ViewContext.StereoscopicMode = StereoscopicMode.Left;
            Shapes.Refresh(DrawOptions.SwitchBackFront, true);
            DrawNormal(g);

            //now offset the other direction (default right)
            if (DrawOptions.SwitchLeftRight)
                TranslateGraphicsLeft(g);
            else
                TranslateGraphicsRight(g);

            ViewContext.StereoscopicMode = StereoscopicMode.Right;
            Shapes.Refresh(DrawOptions.SwitchBackFront, true);
            DrawNormal(g);

            //reset the ViewAngle and graphics transform
            ViewContext.StereoscopicMode = StereoscopicMode.NonStereoscopic;
            Shapes.Refresh(DrawOptions.SwitchBackFront, true);
            g.ResetTransform();
            g.ResetClip();
            //set the point size back to where it was.
            DrawOptions.PointWidth /= 1.5;
        }

        private static void TranslateGraphicsLeft(Graphics g)
        {
            Size canvasSize = ViewContext.CanvasSize;
            g.ResetTransform();
            g.ResetClip();
            g.SetClip(new Rectangle(0,0,canvasSize.Width / 2, canvasSize.Height));
            ShrinkGraphics(g);
            g.TranslateTransform(-canvasSize.Width / 4, 0, MatrixOrder.Append);
        }
        private static void TranslateGraphicsRight(Graphics g)
        {
            Size canvasSize = ViewContext.CanvasSize;
            g.ResetTransform();
            g.ResetClip();
            g.SetClip(new Rectangle(canvasSize.Width / 2, 0, canvasSize.Width / 2, canvasSize.Height));
            ShrinkGraphics(g);
            g.TranslateTransform(canvasSize.Width / 4, 0, MatrixOrder.Append);
        }
        private static void ShrinkGraphics(Graphics g)
        {
            Size canvasSize = ViewContext.CanvasSize;
            //in reverse order, do the following: translate center to 0,0, shrink, translate center back.
            g.TranslateTransform(canvasSize.Width / 2, canvasSize.Height / 2);
            g.ScaleTransform(.5f, .5f);
            g.TranslateTransform(-canvasSize.Width / 2, -canvasSize.Height / 2);
        }


        private static void DrawPrint(Graphics g)
        {
            DrawNormal(g);

            foreach (IndexedFaceSet ifs in EdgePainter.ShapeList)
            {
                foreach (Edge e in ifs.Edges)
                {
                    Rectangle r = Transformer.GetArcSquare(e.StartVertex.ViewCoord_ZeroAngle);
                    Point center = new Point(r.X + (int)(r.Width / 2), r.Y + (int)(r.Height / 2));
                    g.FillEllipse(Brushes.Black, new Rectangle(center.X - 1, center.Y - 1, 3, 3));
                }
            }



            //if (ShowArcs)
            //{
            //    using (Pen pPen = new Pen(Color.LightGray))
            //    {

            //        foreach(ViewPoint vp in viewPrimitives)
            //            if (vp.BaseVoxel.IsEndPoint)
            //                vp.DrawArc(g, pPen);

            //    }
            //}

            //using (Pen pPen = new Pen(pointColor))
            //{
            //    using (Brush pBrush = pPen.Brush)
            //    {

            //        foreach(ViewPoint vp in viewPrimitives)
            //        {

            //            Point center = Point.Round(vp.ArcCenter);
            //            DrawPoint(g, Point.Round(center), pBrush, PointWidth);

            //            if (ShowPoints)
            //            {
            //                if (vp.BaseVoxel.IsEndPoint)
            //                {
            //                    FontStyle fontStyle = FontStyle.Regular;
            //                    g.DrawString(Convert.ToInt32(vp.ArcRadius).ToString(), new Font("Arial", 8f, fontStyle), pBrush, Point.Round(new PointF(center.X + (int)(PointWidth / 2) + 2, center.Y)));
            //                }
            //            }
            //        }
            //    }
            //}
        }




        //private static void DrawPoints(Graphics g, Color pointColor, ViewPrimitiveList viewPrimitives, bool forceSpecifiedColor)
        //{
        //            foreach (ViewPrimitive vp in viewPrimitives)
        //            {
        //                if (vp.BasePrimitive.DrawAsVector || DrawAllAsVectors)
        //                    DrawAsVector(g, vp, pointColor, forceSpecifiedColor);
        //                else
        //                    vp.DrawPoints(g, pBrush);
        //            }
        //        }
        //    }
        //}


        

        ///// <summary>Rotates a ViewPoint 180 degrees about another ViewPoint.</summary>
        //private static void RotatePoint(ViewPoint stationaryPoint, ViewPoint pointToRotate)
        //{
        //    pointToRotate.Location = new ViewCoord(stationaryPoint.Location - (pointToRotate.Location - stationaryPoint.Location), pointToRotate.IsBehindUser);
        //}

        ///// <summary>If one or both of the given points are off the canvas, this method moves the problematic point(s) to the edge of the canvas. The points remain on the line that connects them.</summary>
        ///// <returns>True if there were no problems moving the points to the edges of the canvas.</returns>
        //private static bool EnsureOnScreen(ViewPoint startPoint, ViewPoint endPoint)
        //{
        //    CutOffLineAtXValue(startPoint, endPoint, 0);
        //    CutOffLineAtXValue(endPoint, startPoint, 0);
        //    CutOffLineAtYValue(startPoint, endPoint, 0);
        //    CutOffLineAtYValue(endPoint, startPoint, 0);
        //    CutOffLineAtXValue(startPoint, endPoint, ViewContext.CanvasSize.Width);
        //    CutOffLineAtXValue(endPoint, startPoint, ViewContext.CanvasSize.Width);
        //    CutOffLineAtYValue(startPoint, endPoint, ViewContext.CanvasSize.Height);
        //    CutOffLineAtYValue(endPoint, startPoint, ViewContext.CanvasSize.Height);

        //    return true;
        //}

        //private static void CutOffLineAtXValue(ViewPoint pointToCutOff, ViewPoint anotherPointOnLine, double xValue)
        //{
        //    //...if xValue is between the two points
        //    if (xValue > Math.Min(pointToCutOff.Location.X, pointToCutOff.Location.X) && xValue < Math.Max(pointToCutOff.Location.X, pointToCutOff.Location.X))
        //    {
        //        double lengthOfX = Math.Abs(anotherPointOnLine.Location.X) + Math.Abs(pointToCutOff.Location.X);
        //        double validPercent = (Math.Abs(anotherPointOnLine.Location.X) + xValue) / lengthOfX;

        //        double shiftX = -anotherPointOnLine.Location.X;
        //        double shiftY = -anotherPointOnLine.Location.Y;
        //        double shiftZ = -anotherPointOnLine.Location.Z;

        //        pointToCutOff.Location.X = ((pointToCutOff.Location.X + shiftX) * validPercent - shiftX);
        //        pointToCutOff.Location.Y = ((pointToCutOff.Location.Y + shiftY) * validPercent - shiftY);
        //        pointToCutOff.Location.Z = ((pointToCutOff.Location.Z + shiftZ) * validPercent - shiftZ);
        //    }
        //}
        //private static void CutOffLineAtYValue(ViewPoint pointToCutOff, ViewPoint anotherPointOnLine, double yValue)
        //{
        //    //...if yValue is between the two points
        //    if (yValue > Math.Min(pointToCutOff.Location.Y, pointToCutOff.Location.Y) && yValue < Math.Max(pointToCutOff.Location.Y, pointToCutOff.Location.Y))
        //    {
        //        double lengthOfY = Math.Abs(anotherPointOnLine.Location.Y) + Math.Abs(pointToCutOff.Location.Y);
        //        double validPercent = (Math.Abs(anotherPointOnLine.Location.Y) + yValue) / lengthOfY;

        //        double shiftX = -anotherPointOnLine.Location.X;
        //        double shiftY = -anotherPointOnLine.Location.Y;
        //        double shiftZ = -anotherPointOnLine.Location.Z;

        //        pointToCutOff.Location.X = ((pointToCutOff.Location.X + shiftX) * validPercent - shiftX);
        //        pointToCutOff.Location.Y = ((pointToCutOff.Location.Y + shiftY) * validPercent - shiftY);
        //        pointToCutOff.Location.Z = ((pointToCutOff.Location.Z + shiftZ) * validPercent - shiftZ);
        //    }
        //}









        ///// <summary>Sets VoxelToViewPointMatrix such that the specified Primitives list will fit exactly within the specified Rectangle.</summary>
        //public static void AutoFitWithin(Rectangle fitWithin, PrimitiveList primitives)
        //{
        //    //if (primitives.Count > 0)
        //    //{
        //    //    Rectangle boundingRect = Rectangle.Round(new ViewPointList(primitives).BoundingRect);
        //    //    double currentAspectRatio = fitWithin.Width / (double)fitWithin.Height;
        //    //    double desiredAspectRatio = boundingRect.Width / (double)boundingRect.Height;

        //    //    if (currentAspectRatio > desiredAspectRatio)
        //    //    {
        //    //        //Too wide. We need to adjust the left and right.
        //    //        int desiredWidth = (int)(fitWithin.Height * desiredAspectRatio);
        //    //        int dx = (int)((desiredWidth - fitWithin.Width) / 2);
        //    //        fitWithin.X -= dx;
        //    //        fitWithin.Width = desiredWidth;
        //    //    }
        //    //    else
        //    //    {
        //    //        //Too tall. We need to adjust the top and bottom.
        //    //        int desiredHeight = (int)(fitWithin.Width / desiredAspectRatio);
        //    //        int dy = (int)((desiredHeight - fitWithin.Height) / 2);
        //    //        fitWithin.Y -= dy;
        //    //        fitWithin.Height = desiredHeight;
        //    //    }

        //    //    ViewContext.FitToScreenMatrix2D = new System.Drawing.Drawing2D.Matrix();
        //    //    ViewContext.FitToScreenMatrix2D.Translate(fitWithin.X, fitWithin.Y);
        //    //    ViewContext.FitToScreenMatrix2D.Scale(fitWithin.Width / (float)boundingRect.Width, fitWithin.Height / (float)boundingRect.Height);
        //    //    ViewContext.FitToScreenMatrix2D.Translate(-boundingRect.X, -boundingRect.Y);
        //    //}
        //}

        





        #region Helper Functions

        //public static PointF TransformPoint(PointF toTransform, System.Drawing.Drawing2D.Matrix transformMatrix)
        //{
        //    PointF[] ps = new PointF[] { toTransform };
        //    transformMatrix.TransformPoints(ps);
        //    return ps[0];
        //}

        //public static RectangleF TransformRectangle(RectangleF toTransform, System.Drawing.Drawing2D.Matrix transformMatrix)
        //{
        //    PointF[] ps = new PointF[2] { toTransform.Location, new PointF(toTransform.Right, toTransform.Bottom) };
        //    transformMatrix.TransformPoints(ps);
        //    return RectangleF.FromLTRB(ps[0].X, ps[0].Y, ps[1].X, ps[1].Y);
        //}

        public static void DrawPoint(Graphics g, Point p, Brush b, int pointSize)
        {
            g.FillEllipse(b, p.X - (int)(DrawOptions.PointWidth / 2), p.Y - (int)(DrawOptions.PointWidth / 2), pointSize, pointSize);
        }
        #endregion



        /// <summary>
        /// Blits the off-screen buffer onto the supplied Graphics object. This function should be called from the consuming usercontrol's OnPaint function.
        /// </summary>
        /// <param name="g"></param>
        public static void Blit(Graphics g)
        {
            if (DoDraw)
            {
                if (NextRedraw != RedrawTypeRequired.None)
                    ReDraw();

                g.ResetTransform();
                if (DrawOptions.RotateCanvas)
                {
                    /*
                     * Rotate 180 degrees about the center of the View. To do this, 
                     * we rotate 180 about the top-left (which puts the rotated image 
                     * above and to the left of the view), then translate back onto the 
                     * screen. Matrix operations are done in reverse.
                     */
                    g.TranslateTransform(CanvasSize.Height, CanvasSize.Width);
                    g.RotateTransform(180);
                }
                g.DrawImageUnscaled(mOffScreenBitmap, 0, 0);
            }
        }

        public static void AddShape(IndexedFaceSet ifs)
        {
            Shapes.Add(ifs);
            MarkAsDirty(RedrawTypeRequired.RecalculateViewPrimitives);
        }

        public static void ClearShapes()
        {
            Shapes.Clear();
        }

        public static void PreProcessShapes()
        {
            Shapes.PreProcess();
        }
    }
}
