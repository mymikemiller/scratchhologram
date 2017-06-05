using System;
using System.Collections.Generic;
using System.Text;
using ScratchUtility;

namespace Primitives
{
    /// <summary>Represents the point at which an Edge intersects behind another Edge, or at which an Edge intersects an IndexedFace</summary>
    public class Intersection
    {
        public Edge Edge { get; set; }
        /// <summary>The percentage (0.0 to 1.0) along the Edge at which this Intersection occurs</summary>
        public double DistanceFromStart { get; set; }

        /// <summary>Gets the point at which the Intersection occurs.</summary>
        public Coord IntersectionPoint_ArcCoord
        {
            get
            {
                return Edge.StartVertex.ViewCoord + (Edge.EndVertex.ViewCoord - Edge.StartVertex.ViewCoord) * DistanceFromStart;
            }
        }

        public Intersection(Edge edge, double distanceFromStart)
        {
            Edge = edge;
            DistanceFromStart = distanceFromStart;
        }
        public Coord IntersectionPoint_ViewCoordinates
        {
            get
            {
                return Edge.StartVertex.ViewCoord + (Edge.EndVertex.ViewCoord - Edge.StartVertex.ViewCoord) * DistanceFromStart;
            }
        }
    }
}
