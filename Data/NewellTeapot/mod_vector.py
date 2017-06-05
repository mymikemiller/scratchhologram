# +---------------------------------------------------------+
# | Copyright (c) 2001 Anthony D'Agostino                   |
# | http://www.redrival.com/scorpius                        |
# | scorpius@netzero.com                                    |
# | March 27, 2001                                          |
# +---------------------------------------------------------+
# | Vector Functions                                        |
# +---------------------------------------------------------+

"A simple vector module"

import math

def vadd(u, v):
	"Returns a vector [u+v]"
	return (u[0]+v[0], u[1]+v[1], u[2]+v[2])

def vsplit(u, v):
	"returns a vector [u+v]/2"
	return ((u[0]+v[0])/2, (u[1]+v[1])/2, (u[2]+v[2])/2)

def vsub(u, v):
	"Returns a vector from point v to point u [u-v]"
	return (u[0]-v[0], u[1]-v[1], u[2]-v[2])

def vmul(u, s):
	"Returns a vector [u*scalar]"
	return (u[0]*s, u[1]*s, u[2]*s)

def vdiv(u, s):
	"Returns a vector [u/scalar]"
	return (u[0]/s, u[1]/s, u[2]/s)

def vcross(u, v):
	"Returns a vector [u*v] Cross Product"
	return (u[1]*v[2]-u[2]*v[1], u[2]*v[0]-u[0]*v[2], u[0]*v[1]-u[1]*v[0])

def vunit(u):
	"Returns a vector of unit length (normalized)"
	return vmul(u, 1/vlen(u))

def vlen(u):
	"Returns a real number (Vector Length)"
	return math.sqrt(u[0]**2 + u[1]**2 + u[2]**2)

def vdot(u, v):
	"Returns a real number (Dot Product)"
	return u[0]*v[0] + u[1]*v[1] + u[2]*v[2]

def vdot2(u, v):
	"Returns the cosine of theta in the range (0, 1)"
	costheta = vdot(u, v)
	if costheta > 0:
		return costheta
	else:
		return 0

# Test functions
if __name__ == "__main__":
	t = (math.sqrt(5)-1)/2
	a = (0, 1, t)
	b = (0, 1, -t)
	print "t             :", t
	print "a             :", a
	print "b             :", b
	print
	print "vadd(a, b)    :", vadd(a, b)
	print "vsub(a, b)    :", vsub(a, b)
	print "vcross(a, b)  :", vcross(a, b)
	print "vmul(b, 3)    :", vmul(b, 3)
	print
	print "vdot(a, b)    :", vdot(a, b)
	print "vdot2(a, b)   :", vdot2(a, b)
	print "vlen(a)       :", vlen(a)
	print "vlen(b)       :", vlen(b)
	print "vunit(a)      :", vunit(a)
	print "vunit(b)      :", vunit(b)
	print "vdot(ua, ub)  :", vdot(vunit(a), vunit(b))
	print "vcross(ua, ub):", vcross(vunit(a), vunit(b)), vunit(vcross(a, b) )
	print "Angle         :", math.acos(vdot(vunit(a), vunit(b)))*180/math.pi
