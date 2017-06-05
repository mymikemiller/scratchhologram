#!BPY

"""
Name: 'Geodesic Dome Generator'
Blender: 232
Group: 'Add'
Tooltip: 'Generate various geodesic domes'
"""

__author__ = "Anthony D'Agostino (Scorpius)"
__url__ = ("blender", "elysiun",
"Author's homepage, http://www.redrival.com/scorpius")
__version__ = "1.0"

__bpydoc__ = """\
This script generates standard geodesic domes similar to Blender's
IcoSphere, but adds the following features: resolutions higher than 5;
two subdivision algorithms; spherize/keep-flat or full/half-dome
options; and 3 geodesic bases. It outputs meshes with no duplicate
vertices and is fully interactive.

Usage:<br>
	When the mouse pointer is over the scripts window (in this
case it is a slightly brownish color) the following keyboard shortcuts
are available:

NumPad Plus Key - Increase the resolution in
realtime<br>NumPad Minus Key - Decrease the resolution in realtime

The numbers 1 through 4 on the top row of the keyboard switch to one of
the four predifined base primitives.

The numbers 1 through 9 on the keypad switch to one of the three
predifined resolutions.

Missing:<br>
	A whole lot, see Andy Houston's script for more variations.

Notes:<br>
	This script is a clone of my geodome plugin for Wings. It can be
used to compare the Python and Erlang programming languages.

"""

# +---------------------------------------------------------+
# | Copyright (c) 2005 Anthony D'Agostino                   |
# | http://www.redrival.com/scorpius                        |
# | scorpius@netzero.com                                    |
# | March 12, 2005                                          |
# | Released under the Blender Artistic Licence (BAL)       |
# | Geodesic Dome Generator                                 |
# +---------------------------------------------------------+
# | Uses Erlang naming convention, port from Erlang/Wings3D |
# +---------------------------------------------------------+

import Blender, mod_meshtools, mod_vector

# =================
# === GUI Block ===
# =================
EvtBase,GeoBase = 1,Blender.Draw.Create(1)
EvtReso,GeoReso = 2,Blender.Draw.Create(3)
EvtAlgo,GeoAlgo = 3,Blender.Draw.Create(1)
EvtSphe,GeoSphe = 4,Blender.Draw.Create(1)
EvtDome,GeoDome = 5,Blender.Draw.Create(0)
EvtText,GeoText = 6,Blender.Draw.Create('')

def draw():
	global GeoBase, GeoReso, GeoAlgo, GeoSphe, GeoDome
	Blender.BGL.glClearColor(0.625, 0.5625, 0.5, 0.0) # Brown
	Blender.BGL.glClear(Blender.BGL.GL_COLOR_BUFFER_BIT)
	Blender.BGL.glColor3b(0,0,0)
	GeoBase = Blender.Draw.Menu("Base %t | Icosahedron %x1 | Octahedron %x2 | Tetrahedron %x3 | One Triangle %x4", EvtBase, 10, 10, 225, 20, GeoBase.val, "Select Geo-Base Object")
	GeoReso = Blender.Draw.Slider("Resolution: ", EvtReso, 10, 35, 225, 20, GeoReso.val, 1, 20, 0, "Select Resolution")
	GeoAlgo = Blender.Draw.Menu("Algorithm %t | Frequency (Edge-Cut Subdivision) %x1 | Depth (Slow Recursive Subdivision) %x2", EvtAlgo, 10, 60, 225, 20, GeoAlgo.val, "Select Algorithm")
	GeoSphe = Blender.Draw.Toggle("Spherize", EvtSphe, 10, 90, 225, 20, GeoSphe.val, "Spherize")
	GeoDome = Blender.Draw.Toggle("Half Dome", EvtDome, 10, 115, 225, 20, GeoDome.val, "HalfDome")

	Blender.BGL.glRasterPos2i(10, 150)
	GeoText = Blender.Draw.Text("http://www.redrival.com/scorpius")
	Blender.BGL.glRasterPos2i(10, 165)
	Blender.Draw.Text("Copyright (c) 2005 Anthony D'Agostino")
	Blender.BGL.glRasterPos2i(10, 180)
	Blender.Draw.Text("Geodesic Dome Generator")

def key_event(event, value):
	if (event==Blender.Draw.ESCKEY and not value):
		Blender.Draw.Exit()
	if (event==Blender.Draw.QKEY and not value):
		Blender.Draw.Exit()

	if (event==Blender.Draw.PADPLUSKEY and not value):
		GeoReso.val += 1
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PADMINUS and not value):
		GeoReso.val -= 1
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)

	if (event==Blender.Draw.ONEKEY and not value):
		GeoBase.val = 1
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.TWOKEY and not value):
		GeoBase.val = 2
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.THREEKEY and not value):
		GeoBase.val = 3
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.FOURKEY and not value):
		GeoBase.val = 4
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)

	if (event==Blender.Draw.PAD1 and not value):
		GeoReso.val = 1
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD2 and not value):
		GeoReso.val = 3
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD3 and not value):
		GeoReso.val = 5
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD4 and not value):
		GeoReso.val = 7
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD5 and not value):
		GeoReso.val = 9
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD6 and not value):
		GeoReso.val = 11
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD7 and not value):
		GeoReso.val = 13
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD8 and not value):
		GeoReso.val = 15
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	if (event==Blender.Draw.PAD9 and not value):
		GeoReso.val = 17
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)

def button_event(eventnumber):
	if eventnumber:
		make_geodome(GeoBase.val, GeoReso.val, GeoAlgo.val, GeoSphe.val, GeoDome.val)
	else:
		pass
	Blender.Draw.Draw()

Blender.Draw.Register(draw, key_event, button_event)

# ====================================
# === Indexed Polyhedra Data Block ===
# ====================================
def icosahedron(DomeFlag):
	if not DomeFlag:
		Verts = [
		(0.000000,0.894427,0.447214),(0.000000,0.000000,1.000000),
		(-0.000000,-0.894427,-0.447214),(0.850651,0.276393,0.447214),
		(0.525731,-0.723607,0.447214),(0.850651,-0.276393,-0.447214),
		(0.525731,0.723607,-0.447214),(-0.525731,0.723607,-0.447214),
		(-0.525731,-0.723607,0.447214),(-0.850651,0.276393,0.447214),
		(-0.000000,-0.000000,-1.000000),(-0.850651,-0.276393,-0.447214)]
		Faces = [
		[3,0,1],[1,0,9],[9,8,1],[8,4,1],[4,3,1],[6,7,0],[7,11,9],
		[11,2,8],[2,5,4],[5,6,3],[4,8,2],[3,4,5],[6,0,3],[9,0,7],
		[8,9,11],[5,2,10],[6,5,10],[7,6,10],[11,7,10],[2,11,10]]
		return (Verts, Faces)
	else:
		Verts = [
		(0.309017,-0.951057,0.000000),(-0.309017,0.951057,0.000000),
		(-0.850651,0.276393,0.447214),(-0.809017,0.587785,0.000000),
		(1.000000,0.000000,0.000000),(0.809017,-0.262866,0.525731),
		(-0.309017,-0.425325,0.850651),(0.000000,0.000000,1.000000),
		(0.000000,0.894427,0.447214),(-0.525731,-0.723607,0.447214),
		(-0.500000,0.688191,0.525732),(0.000000,0.525731,0.850651),
		(0.500000,0.688191,0.525732),(-0.500000,0.162460,0.850651),
		(0.525731,-0.723607,0.447214),(-1.000000,0.000000,0.000000),
		(0.309017,-0.425325,0.850651),(0.809017,0.587785,0.000000),
		(-0.809017,-0.262866,0.525731),(0.809017,-0.587785,0.000000),
		(0.850651,0.276393,0.447214),(-0.809017,-0.587785,0.000000),
		(0.000000,-0.850651,0.525731),(0.309017,0.951057,0.000000),
		(0.500000,0.162460,0.850651),(-0.309017,-0.951057,0.000000)]
		Faces = [
		[11,7,24],[12,24,20],[8,11,12],[11,24,12],[10,2,13],[11,13,7],
		[8,10,11],[10,13,11],[6,7,13],[18,13,2],[9,6,18],[6,13,18],
		[16,7,6],[22,6,9],[14,16,22],[16,6,22],[24,7,16],[5,16,14],
		[20,24,5],[24,16,5],[1,8,23],[15,2,3],[25,9,21],[0,19,14],
		[17,20,4],[22,0,14],[9,25,22],[25,0,22],[5,4,20],[14,19,5],
		[19,4,5],[12,20,17],[8,12,23],[12,17,23],[10,3,2],[8,1,10],
		[1,3,10],[18,21,9],[2,15,18],[15,21,18]]
		return (Verts, Faces)

def octahedron(DomeFlag):
	if not DomeFlag:
		Verts = [
		(0.0,0.0,1.0),(1.0,0.0,0.0),(0.0,0.0,-1.0),
		(0.0,1.0,0.0),(-1.0,0.0,0.0),(0.0,-1.0,0.0)]
		Faces = [
		[1,3,0],[3,4,0],[4,5,0],[5,1,0],[1,5,2],[3,1,2],[4,3,2],[5,4,2]]
		return (Verts, Faces)
	else:
		Verts = [
		(0.0,0.0,1.0),(1.0,0.0,0.0),(0.0,1.0,0.0),(-1.0,0.0,0.0),(0.0,-1.0,0.0)]
		Faces = [
		[1,2,0],[2,3,0],[3,4,0],[4,1,0]]
		return (Verts, Faces)

def tetrahedron(DomeFlag):
	if not DomeFlag:
		Verts = [
		(0.000000,0.000000,0.612372),(0.000000,0.577350,-0.204124),
		(-0.500000,-0.288675,-0.204124),(0.500000,-0.288675,-0.204124)]
		Faces = [
		[2,0,1],[3,0,2],[1,0,3],[2,1,3]]
		return (Verts, Faces)
	else:
		Verts = [
		(0.000000,0.000000,0.816496),(0.000000,0.577350,0.000000),
		(-0.500000,-0.288675,0.000000),(0.500000,-0.288675,0.000000)]
		Faces = [
		[2,0,1],[3,0,2],[1,0,3]]
		return (Verts, Faces)

def triangle(DomeFlag):
	Verts = [(0.816497,-0.471405,1.000000),(-0.816497,-0.471405,1.000000),
			 (0.000000,0.942809,1.000000)]
	Faces = [[1,2,0]]
	return (Verts, Faces)

# =====================================
# === Edge-Cut Subivision Functions ===
# =====================================
def subdivide_rawtriangles_by_frequency(RawTriangles, Frequency):
	Process_Triangle = lambda (V1,V2,V3): \
		subdivide_triangle_by_frequency(V1, V2, V3, Frequency)
	A = map(Process_Triangle, RawTriangles)
	RawTriangles2 = flatten_once(A)
	return RawTriangles2

def subdivide_triangle_by_frequency(V1, V2, V3, Frequency):
	if Frequency==1: return [[V1,V2,V3]]
	NumCuts = Frequency
	Verts = make_verts(V1, V2, V3, NumCuts)
	Faces = make_faces(NumCuts)
	RawTriangles = indexed_to_raw(Verts, Faces)
	return RawTriangles

# ==================
# === Make Faces ===
# ==================
def make_faces(NumCuts):
	NumVerts = sum(range(NumCuts+2))
	DnRange = range(0, NumVerts-1*NumCuts-1) or [0] # or is needed for compatibility
	UpRange = range(0, NumVerts-2*NumCuts-1) or [0] # with lists:seq(0,0)
	DnFaces = make_faces_dn(DnRange)
	UpFaces = make_faces_up(UpRange)
	A = flatten_once(DnFaces)
	B = flatten_once(UpFaces)
	return A + B

def make_faces_dn(Seq):
	return make_faces_dn_2(Seq, 1, [])

def make_faces_up(Seq):
	return make_faces_up_2(Seq, 1, [])

def make_faces_dn_2(Seq, N, List):
	if Seq==[]: return List
	(Head,Tail) = list_split(N, Seq)
	L = len(Head)
	Face = [[I, I+L+1, I+L] for I in Head]
	List.append(Face)
	return make_faces_dn_2(Tail, N+1, List)

def make_faces_up_2(Seq, N, List):
	if Seq==[]: return List
	(Head,Tail) = list_split(N, Seq)
	L = len(Head)
	Face = [[I+L, I+L+1, I+2*L+2] for I in Head]
	List.append(Face)
	return make_faces_up_2(Tail, N+1, List)

# ==================
# === Make Verts ===
# ==================
def make_verts(V1, V2, V3, NumCuts):
	U = edge_cut_exact(V1, V3, NumCuts)
	V = edge_cut_exact(V1, V2, NumCuts)
	A,B = U[0:2]; TU = U[2:]
	C,D = V[0:2]; TV = V[2:]
	Verts1 = [A,B,D]
	Verts2 = make_span_verts(TU, TV, 2, [])
	Verts2 = flatten_once(Verts2)
	Verts = Verts1 + Verts2
	NumVerts = sum(range(NumCuts+2))
	assert len(Verts) == NumVerts # verify sum
	return Verts

def make_span_verts(U, V, N, Seq):
	if U==[] and V==[]: return Seq
	H1 = U.pop(0); T1 = U
	H2 = V.pop(0); T2 = V
	Seq.append(edge_cut_exact(H1, H2, N))
	return make_span_verts(T1, T2, N+1, Seq)

def edge_cut(V1, V2, NumCuts):
	E = mod_vector.vsub(V2, V1)
	N = NumCuts-1
	return edge_cut_2(V1, V2, NumCuts, E, [], N)

def edge_cut_2(V1, V2, NumCuts, E, Seq, N):
	if N==0:
		Seq.reverse()
		Seq = flatten_once(Seq)
		return [V1] + Seq + [V2]
	A = mod_vector.vmul(E, float(N)/NumCuts)
	B = mod_vector.vadd(V1, A)
	Seq.append([B])
	return edge_cut_2(V1, V2, NumCuts, E, Seq, N-1)

def edge_cut_exact(V1, V2, NumCuts):
	A = edge_cut(V1, V2, NumCuts)
	B = edge_cut(V2, V1, NumCuts)
	B.reverse()
	return edge_cut_lists_average(A, B, [])

def edge_cut_lists_average(A, B, Seq):
	if A==[] and B==[]: return Seq
	HA = A.pop(0); TA = A
	HB = B.pop(0); TB = B
	(X1,Y1,Z1) = HA
	(X2,Y2,Z2) = HB
	AveragedVertex = ((X1+X2)/2, (Y1+Y2)/2, (Z1+Z2)/2)
	Seq.append(AveragedVertex)
	return edge_cut_lists_average(TA, TB, Seq)

# =======================================
# === Recursive Subdivision Functions ===
# =======================================
def subdivide_rawtriangles_by_depth(RawTriangles, Depth):
	Process_Triangle = lambda (V1,V2,V3): \
		subdivide_triangle_by_depth(V1, V2, V3, Depth)
	A = map(Process_Triangle, RawTriangles)
	RawTriangles2 = append_ntimes(A, Depth)
	return RawTriangles2

def subdivide_triangle_by_depth(V1, V2, V3, Depth):
	if Depth==1: return [V1,V2,V3] # [{a,b,c},{d,e,f},{g,h,i}];
	VA = mod_vector.vsplit(V1, V2)
	VB = mod_vector.vsplit(V2, V3)
	VC = mod_vector.vsplit(V3, V1)
	A  = subdivide_triangle_by_depth(V1, VA, VC, Depth-1)
	B  = subdivide_triangle_by_depth(V2, VB, VA, Depth-1)
	C  = subdivide_triangle_by_depth(V3, VC, VB, Depth-1)
	D  = subdivide_triangle_by_depth(VA, VB, VC, Depth-1)
	return [A,B,C,D]

def append_ntimes(DeepList, N):
	if N==1: return DeepList
	List = flatten_once(DeepList)
	return append_ntimes(List, N-1)

# ========================
# === Indexed <--> Raw ===
# ========================
def indexed_to_raw(Verts, Faces):
	make_raw_triangle = lambda Face: [Verts[Face[0]], Verts[Face[1]], Verts[Face[2]]]
	RawTriangles = map(make_raw_triangle, Faces)
	return RawTriangles

def raw_to_indexed(RawTriangles):			# replace values with indices
	VertsWithDups = flatten_once(RawTriangles)
	Verts = remove_dups(VertsWithDups)		# zero tolerance
	IndexedVerts = add_indices(Verts)
	Get_Index = lambda Vertex: IndexedVerts[Vertex]
	Process_Face = lambda Face: map(Get_Index, Face)
	Faces = map(Process_Face, RawTriangles) # re-index the Faces list
	return (Verts, Faces)

def raw_to_indexed(RawTriangles): # faster than the above Erlang "translation"
	Verts = []
	Coords = {}
	Index = 0
	Faces = RawTriangles[:]
	for i in range(len(RawTriangles)):
		for j in range(len(RawTriangles[i])):
			vertex = RawTriangles[i][j]
			if not Coords.has_key(vertex):
				Coords[vertex] = Index
				Index += 1
				Verts.append(vertex)
			Faces[i][j] = Coords[vertex]
	return (Verts, Faces)

def remove_dups(VertsWithDups): # return a list of unique verts
	Make_KeyValue_Pair = lambda Vertex: (Vertex, None)
	A = map(Make_KeyValue_Pair, VertsWithDups)
	B = dict(A)
	return B.keys()

def add_indices(Verts):
	NumVerts = len(Verts)
	Indices = range(NumVerts)
	A = zip(Verts, Indices)
	return dict(A)

def list_split(N, Seq): # equivalent to Erlang's "{H,T}=lists:split(N, Seq)"
	Head = Seq[0:N]
	Tail = Seq[N:]
	return (Head, Tail)

def flatten_once(DeepList):
	List = []
	for L in DeepList:
		List.extend(L)
	return List

def get_base(BaseFlag, DomeFlag):
	if BaseFlag==1: 				   # GUI menu returns 1 for first choice
		return icosahedron(DomeFlag)
	if BaseFlag==2:
		return octahedron(DomeFlag)
	if BaseFlag==3:
		return tetrahedron(DomeFlag)
	if BaseFlag==4:
		return triangle(DomeFlag)
	else:
		return None

# ============
# === Main ===
# ============
def make_geodome(BaseFlag, Resolution, AlgorithmFlag, SpherizeFlag, DomeFlag):
	Verts, Faces = get_base(BaseFlag, DomeFlag)
	#Faces = [[C,B,A] for [A,B,C] in Faces]  # flip normals
	RawTriangles = indexed_to_raw(Verts, Faces)
	if AlgorithmFlag==1:
		Frequency = Resolution
		RawTriangles = subdivide_rawtriangles_by_frequency(RawTriangles, Frequency)
	if AlgorithmFlag==2:
		Depth = Resolution
		RawTriangles = subdivide_rawtriangles_by_depth(RawTriangles, Depth)
	Verts, Faces = raw_to_indexed(RawTriangles)
	if SpherizeFlag: Verts = map(mod_vector.vunit, Verts)
	mod_meshtools.show_progress = 1
	mod_meshtools.overwrite_mesh_name = 1
	Names = ["Icosa", "Octa", "Tetra", "Tri"]
	ObjName = "GeoDome"
	#$ObjName = Names[BaseFlag-1] + "Dome"
	mod_meshtools.create_mesh(Verts, Faces, ObjName)
	Blender.Window.DrawProgressBar(1.0, "") # clear progressbar
	Blender.Window.RedrawAll()
