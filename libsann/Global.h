/*/////////////////////////////////////////////////////////////////
*	libsann - Artificial neural networks library
*	Designed to be reliable and cross-platform.
*	C++ 11
*
*	Author: Matteo Tosato - tosatz{at}tiscali{dot}it
*	2013, 2014
*	Version: 0.3.1
////////////////////////////////////////////////////////////////*/

#ifndef GLOBAL_H
#define GLOBAL_H

#include <cmath>
#include <cstdint>
#include <vector>
#include <ctime>

using namespace std;

#ifndef uint
typedef uint32_t uint;
#endif

//#define USE_BOOST_LIB

// Boost support
#ifdef USE_BOOST
#include "boost\thread\thread.hpp"
#endif

// Library initialization, Calls this MACRO at application begin.
// (for correct netoworks synapses initialization process)
#define INIT srand((uint)time(NULL));

#define MIN(a,b) (((a) > (b)) ? b : a);
#define MAX(a,b) (((a) > (b)) ? a : b);

namespace libsann {
	// Normalization mode supported
	enum DataNormMode { Z_AXIS, MAPPING_P, ZERO_MAX, NO };
	// Synapses initialization mode
	enum SynInitMode { POSITIVE_NEGATIVE_RANGE, POSITIVE_RANGE, NGUYEN_WINDROW, FAN_IN, ZERO, NONE };
}

#endif