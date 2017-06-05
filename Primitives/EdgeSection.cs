using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchUtility;
using System.Drawing;

namespace Primitives
{
    /// <summary>Represents a section of an Edge with a constant QI value. Some Edges are made entirely of one EdgeSection, some are split into multiple EdgeSections if they go behind silhouette edges, etc.</summary>
    public class EdgeSection
    {
        private Rectangle mBoundingBox = Rectangle.Empty;

        /// <summary>Specifies whether or not this EdgeSection is visible and thus should be drawn.</summary>
        public bool Visible { get; set; }

        /// <summary>The Edge that this EdgeSection is a part of.</summary>
        public Edge Edge { get; private set; }

        /// <summary>Gets the 3D Coord value that begins this EdgeSection</summary>
        public Coord StartCoord { get; private set; }
        /// <summary>Gets the 3D Coord value that ends this EdgeSection</summary>
        public Coord EndCoord { get; private set; }

        /// <summary>Gets the Vertex that begins this EdgeSection, if it begins with one.</summary>
        public Vertex StartVertex { get; internal set; }
        /// <summary>Gets the Vertex that ends this EdgeSection, if it ends with one.</summary>
        public Vertex EndVertex { get; internal set; }

        /// <summary>Initializes an EdgeSection that spans the entire Edge.</summary>
        public EdgeSection(Edge e, Vertex startVertex, Vertex endVertex)
        {
            Edge = e;
            StartVertex = startVertex;
            StartCoord = startVertex.ViewCoord;
            EndVertex = endVertex;
            EndCoord = endVertex.ViewCoord;
        }
        /// <summary>Initializes an EdgeSection that starts at the Vertex at the beginning of an Edge and extends to somewhere in the middle of the Edge. Beginning refers to the direction that the Edge gets traversed.</summary>
        public EdgeSection(Edge e, Vertex startVertex, Coord endCoord)
        {
            Edge = e;
            StartVertex = startVertex;
            StartCoord = startVertex.ViewCoord;
            EndCoord = endCoord;
        }
        /// <summary>Initializes an EdgeSection that starts at the Vertex at the end of an Edge and extends to somewhere in the middle of the Edge. End refers to the direction that the Edge gets traversed.</summary>
        public EdgeSection(Edge e, Coord startCoord, Vertex endVertex)
        {
            Edge = e;
            StartCoord = startCoord;
            EndVertex = endVertex;
            EndCoord = EndVertex.ViewCoord;
        }
        /// <summary>Initializes an EdgeSection that starts and ends somewhere in the middle of an Edge.</summary>
        public EdgeSection(Edge e, Coord startCoord, Coord endCoord)
        {
            Edge = e;
            StartCoord = startCoord;
            EndCoord = endCoord;
        }
        
        /// <summary>Gets the Rectangle that bounds this EdgeSection when drawn on the screen.</summary>
        public Rectangle BoundingBox
        {
            get
            {
                if (mBoundingBox == Rectangle.Empty)
                    mBoundingBox = Global.GetRectangleWithGivenCorners(StartCoord.ToPointD(), EndCoord.ToPointD());
                return mBoundingBox;
            }
        }
    }
}
