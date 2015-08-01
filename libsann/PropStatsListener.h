/*/////////////////////////////////////////////////////////////////
*	libsann - Artificial neural networks library
*	Designed to be reliable and cross-platform.
*	C++ 11
*
*	Author: Matteo Tosato - tosatz{at}tiscali{dot}it
*	2013, 2014
*	Version: 0.3.1
////////////////////////////////////////////////////////////////*/

/*
*	Statistics listener header file
*/

#ifndef PROPSTATSLISTENER_H
#define PROPSTATSLISTENER_H

#include "Global.h"
#include "Trainingset.h"
#include <mutex>

namespace libsann
{
  class PropStatsListener
  {
  protected:
    vector<double**> outputValues;
    vector<double> errors;
    mutex out_mtx;
    mutex err_mtx;

  public:
    PropStatsListener();
    ~PropStatsListener() {}

    // Handlers
    void OutputValuesHandler(double*** values, int size);
    void ErrorValueHandler(double value);

    // Methods:
    void Reset();

    vector<double**> GetOutputValues();
    vector<double> GetErrorValue();

    double*** PrepareValuesForStats(vector<double*> outputs, Trainingset* set);
  };
}

#endif