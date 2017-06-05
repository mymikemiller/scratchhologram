using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Primitives;
using System.Drawing;
using ScratchUtility;
using System.Collections;

namespace ViewSupport
{
    /// <summary>A static class used to paint Edges of IndexedFacesets according to the settings specified in DrawOptions.</summary>
    internal static class EdgePainter
    {
        private static Graphics surface;
        internal static List<EdgeSection> VisibleEdgeSections { get; set; }

        public static ShapeList ShapeList { get; set; }

        internal static void Draw(Graphics g)
        {
            surface = g;

            if (DrawOptions.ShowArcs)
            {
                foreach (IndexedFaceSet ifs in ShapeList)
                    foreach (Edge e in ifs.Edges)
                        DrawArcs(g, e);
            }

            if (DrawOptions.VisibilityMode == VisibilityMode.HiddenLine)
            {
                //recalculate the visibility for each Edge so we know which ones to draw.
                TraverseEdges();

                foreach (EdgeSection es in VisibleEdgeSections)
                    DrawEdgeSection(g, es);
            }
            else
            {
                foreach (IndexedFaceSet ifs in ShapeList)
                    foreach (Edge e in ifs.Edges)
                        DrawEdge(g, e);
            }
        }

        private static void DrawEdge(Graphics g, Edge e)
        {
            DrawEdgePart(g, e, e.StartVertex.ViewCoord, e.EndVertex.ViewCoord);
        }
        private static void DrawEdgeSection(Graphics g, EdgeSection es)
        {
            DrawEdgePart(g, es.Edge, es.StartCoord, es.EndCoord);
        }

        private static void DrawEdgePart(Graphics g, Edge e, Coord startCoord, Coord endCoord)
        {
            Point startPoint = startCoord.ToPointD().ToPoint();
            Point endPoint = endCoord.ToPointD().ToPoint();

            Pen pPen = Drawing.VectorPen;
            Color backupColor = pPen.Color;
            Color actualColor = (DrawOptions.ViewMode == ViewMode.RedBlue && ViewContext.StereoscopicMode != StereoscopicMode.NonStereoscopic) ? Color.Black : pPen.Color;

            pPen.Color = actualColor;

            if (DrawOptions.VectorMode)
                g.DrawLine(pPen, startPoint, endPoint);
            else
            {
                Brush pBrush = DrawOptions.ViewMode == ViewMode.RedBlue ? Drawing.RedBlueModePointBrush : Drawing.PointBrush;

                foreach (Coord c in e.GetPoints(DrawOptions.ViewPointsPerUnitLength, false))
                {
                    PointD p = c.ToPointD();

                    //Draw the point if visible
                    if (DrawOptions.VisibilityMode == VisibilityMode.Transparent || Global.IsBetween(p, startCoord.ToPointD(), endCoord.ToPointD()))
                        Drawing.DrawPoint(g, p.ToPoint(), pBrush, (int)DrawOptions.PointWidth);
                }
            }
            pPen.Color = backupColor;
        }

        private static void DrawArcs(Graphics g, Edge e)
        {
            List<Coord> points = e.GetPoints(DrawOptions.ViewPointsPerUnitLength, true);
            foreach (Coord c in points)
            {
                Rectangle arcRect = Transformer.GetArcSquare(c);
                float startAngle = c.Z - ViewContext.N_ViewCoordinates > 0 ? 0 : 180;
                g.DrawArc(Drawing.ArcPen, arcRect, startAngle, 180);

            }
        }



        internal static void TraverseEdges()
        {
            VisibleEdgeSections = new List<EdgeSection>();

            count = 0;

            foreach (IndexedFaceSet ifs in ShapeList)
            {
                foreach (Edge e in ifs.Edges)
                {
                    ProcessEdge(e);
                }
            }
        }
        static double count = 0;


        /// <summary>Splits the edge up into sections with constant visibility by determining if and where it intersects any of the ShapeList's Silhouette Edges.</summary>
        internal static void ProcessEdge(Edge e)
        {
            foreach (Intersection inter in e.FaceIntersections)
            {
                //surface.FillEllipse(Brushes.Black, new Rectangle((int)inter.IntersectionPoint_ViewCoordinates.X - 5, (int)inter.IntersectionPoint_ViewCoordinates.Y - 5, 10, 10));
                //surface.DrawString(ScratchUtility.Transformer.WindowToModel(inter.IntersectionPoint_ViewCoordinates).ToString(2), new Font("Arial", 8f, FontStyle.Regular), Brushes.Magenta, (float)inter.IntersectionPoint_ViewCoordinates.X - 5, (float)inter.IntersectionPoint_ViewCoordinates.Y - 5);
            }

            Global.Print("Processing Edge " + e.StartVertex.VertexIndex + " to " + e.EndVertex.VertexIndex);
            if (e.Type == EdgeType.FrontFacing || e.Type == EdgeType.Silhouette)
            {
                //initialize the intersections list with the face intersections.
                List<Intersection> intersections = new List<Intersection>(e.FaceIntersections);
                //List<Intersection> intersections = new List<Intersection>();


                //loop through every Silhouette Edge in the ShapeList
                foreach (IndexedFaceSet ifs in ShapeList)
                {
                    foreach (Edge edgeToCheck in ifs.Edges.Where(silhouetteEdge => !DrawOptions.QuickMode || silhouetteEdge.Type == EdgeType.Silhouette)) //check against all edges if not QuickMode
                    {
                        if (e != edgeToCheck) //don't compare against itself
                        {
                            //Check for intersection
                            Coord intersectionPoint;

                            if (e.IntersectsBehind(edgeToCheck, out intersectionPoint))
                            {
                                double distanceFromStart = (intersectionPoint - e.StartVertex.ViewCoord).Length / e.Length_ViewCoordinates;
                                intersections.Add(new Intersection(e, distanceFromStart));
                                if (Global.DebugMode)
                                    surface.FillEllipse(Brushes.Black, new Rectangle((int)intersectionPoint.X - 5, (int)intersectionPoint.Y - 5, 10, 10));
                            }
                        }
                    }
                }

                //we now know the location of every intersection with a Silhouette Edge that is in front of the Edge being processed.
                //the list needs to be ordered in the direction of travel so we can appropriately add the EdgeSections to the Edge's list.

                //Separate the Edge into the appropriate EdgeSections ordered by increasing distance from the e.StartVertex.
                IOrderedEnumerable<Intersection> orderedIntersections = intersections.OrderBy<Intersection, double>(c => (c.IntersectionPoint_ViewCoordinates - e.StartVertex.ViewCoord).Length);

                e.EdgeSections.Clear();
                Coord lastCoord = e.StartVertex.ViewCoord;
                EdgeSection es;
                foreach (Intersection si in orderedIntersections)
                {
                    es = new EdgeSection(e, lastCoord, si.IntersectionPoint_ViewCoordinates);
                    ComputeVisibility(es);
                    e.EdgeSections.Add(es);
                    if (es.Visible)
                        VisibleEdgeSections.Add(es);
                    lastCoord = si.IntersectionPoint_ViewCoordinates;
                }
                es = new EdgeSection(e, lastCoord, e.EndVertex.ViewCoord);
                ComputeVisibility(es);
                e.EdgeSections.Add(es);
                if (es.Visible)
                    VisibleEdgeSections.Add(es);
            }
        }

        /// <summary>Appropriately sets the Visible property of the specified EdgeSection.</summary>
        private static void ComputeVisibility(EdgeSection es)
        {
            Coord testPoint = (es.StartCoord + es.EndCoord) / 2; //test visibility of the midpoint of the EdgeSection
            if (count == 426 || count == 423)
                Console.WriteLine("found");
            if (Global.DebugMode)
                surface.DrawString(es.Edge.EdgeID.ToString() + " (" + count.ToString() + ")", new Font("Arial", 8f, FontStyle.Regular), Drawing.RedBlueModePointBrush, testPoint.ToPointF());
            count++;

            foreach (IndexedFaceSet ifs in ShapeList)
            {
                foreach (IndexedFace face in ifs.IndexedFaces.Where(iface => !iface.IsTransparent && (!DrawOptions.QuickMode || iface.IsFrontFacing))) //if QuickMode, only check against front-facing faces
                {
                    if (!es.Edge.ContainsFace(face)) //only check against faces that this EdgeSection's Edge is not a part of
                    {
                        if (face.BoundingBox.Contains(testPoint.ToPointD().ToPoint())) //if the bounding box doesn't contain the point, it must be outside of the polygon.
                        {
                            if (face.ContainsPoint2D(testPoint))
                            {
                                if (face.IsBetweenCameraAndPoint3D(testPoint))
                                {
                                    es.Visible = false;
                                    return;

                                }
                            }
                        }
                    }
                }
            }
            es.Visible = true;
        }



        

        


        private static bool SilhouetteEdgeExistsAtVertex(Vertex v)
        {
            foreach (Edge e in v.Edges)
                if (e.Type == EdgeType.Silhouette)
                    return true;
            return false;
        }

    }
}
