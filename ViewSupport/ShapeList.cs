using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScratchUtility;
using Primitives;

namespace ViewSupport
{
    public class ShapeList : IEnumerable<IndexedFaceSet>
    {
        private List<IndexedFaceSet> mShapes;

        public ShapeList()
        {
            mShapes = new List<IndexedFaceSet>();
        }

        public void Add(IndexedFaceSet ifs)
        {
            mShapes.Add(ifs);
        }
        public void Clear()
        {
            mShapes.Clear();
        }
        public int Count { get { return mShapes.Count; } }

        /// <summary>Calls Refresh() on each of the IndexedFaceSets in this ShapeList (to update the AvailableViewVertexLocations for each to match the latest transform matrix). Also sets the NearestVertex.</summary>
        public void Refresh(bool switchBackFront, bool arcLocationsOnly)
        {
            foreach (IndexedFaceSet ifs in mShapes)
            {
                if(arcLocationsOnly)
                    ifs.RefreshArcLocationsOnly(switchBackFront);
                else
                    ifs.Refresh(switchBackFront);
            }
            //BoundingBoxes must be refreshed after refreshing all the IndexedFaceSets. Otherwise, the BoundingBox for Auxiliary Edges won't be accurate.
            foreach (IndexedFaceSet ifs in mShapes)
            {
                foreach (Edge e in ifs.Edges)
                {
                    e.RefreshBoundingBox();
                }
            }
        }


        /// <summary>Processes the List of IndexedFaceSets to fix visual problems. For example, adds a line at the position where two IndexedFaceSets intersect.</summary>
        public void PreProcess()
        {

            //todo: first do the plane-plane intersections, then only do the following for planes that intersect


            //compare every face to every edge to split edges at intersections with faces.
            foreach (IndexedFaceSet ifs in mShapes)
            {
                foreach (Edge e in ifs.Edges)
                {
                    foreach (IndexedFaceSet ifsInner in mShapes)
                    {
                        foreach (IndexedFace ifc in ifsInner.IndexedFaces)
                        {
                            if (!e.ContainsFace(ifc) && !e.StartVertex.ContainsFace(ifc) && !e.EndVertex.ContainsFace(ifc)) //only compare to faces that the edge isn't a part of
                            {
                                Coord c;
                                if (ifc.IntersectsWith_ModelingCoordinates(e, out c))
                                {
                                    if(ifc.ContainsPoint2D_ModelingCoordinates(c))
                                    {
                                        double distanceFromStart = (c - e.StartVertex.ModelingCoord).Length / e.Length_ModelingCoordinates;

                                        e.FaceIntersections.Add(new Intersection(e, distanceFromStart));
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }


        #region IEnumerable<IndexedFaceSet> Members

        public IEnumerator<IndexedFaceSet> GetEnumerator()
        {
            return mShapes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return mShapes.GetEnumerator();
        }

        #endregion


    }
}
