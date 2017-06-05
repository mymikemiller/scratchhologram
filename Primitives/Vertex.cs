using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchUtility;

namespace Primitives
{
    /// <summary>Represents a vertex adjoining (generally) three or more IndexedFaces by way of being the end-point for the Edges connecting those IndexedFaces.</summary>
    public class Vertex
    {
        /// <summary>Specifies the index into the parent IndexFaceSet's AvailableVertexLocations and AvailableViewVertexLocations lists that this Vertex's location is stored.</summary>
        public int VertexIndex { get; private set; }

        /// <summary>The list of Edges that this Vertex is a part of.</summary>
        public List<Edge> Edges { get; private set; }

        /// <summary>The list of IndexedFaces that this Vertex is a part of.</summary>
        public List<IndexedFace> IndexedFaces { get; private set; }

        /// <summary>The IndexedFaceSet that this Vertex is a part of.</summary>
        public IndexedFaceSet ParentIndexedFaceSet { get; private set; }

        public Vertex(IndexedFaceSet parentIndexedFaceSet, int vertexIndex)
        {
            ParentIndexedFaceSet = parentIndexedFaceSet;
            VertexIndex = vertexIndex;
            Edges = new List<Edge>();
            IndexedFaces = new List<IndexedFace>();
        }

        public Coord ModelingCoord
        {
            get
            {
                return ParentIndexedFaceSet.AvailableVertexLocations[VertexIndex];
            }
        }
        public Coord ViewCoord
        {
            get
            {
                return ParentIndexedFaceSet.AvailableViewVertexLocations[VertexIndex];
            }
        }
        public Coord ViewCoord_ZeroAngle
        {
            get
            {
                return ParentIndexedFaceSet.AvailableViewVertexLocations_ZeroAngle[VertexIndex];
            }
        }

        public bool ContainsFace(IndexedFace ifc)
        {
            return IndexedFaces.Contains(ifc);
        }


    }
}
