#include "PropStatsListener.h"

namespace libsann
{
  PropStatsListener::PropStatsListener()
  {
  }

  void PropStatsListener::OutputValuesHandler(double*** values, int size)
  {
    out_mtx.lock();

    outputValues.clear();

    for (int i = 0; i < size; i++)
      outputValues.push_back(values[i]);

    out_mtx.unlock();
  }

  void PropStatsListener::ErrorValueHandler(double value)
  {
    err_mtx.lock();
    errors.push_back(value);
    err_mtx.unlock();
  }

  void PropStatsListener::Reset()
  {
    err_mtx.lock();
    out_mtx.lock();

    outputValues.clear();
    errors.clear();

    err_mtx.unlock();
    out_mtx.unlock();
  }

  vector<double**> PropStatsListener::GetOutputValues()
  {
    out_mtx.lock();

    vector<double**> retValue(outputValues.size());
    copy(outputValues.begin(), outputValues.end(), retValue.begin());
    outputValues.clear();
    
    out_mtx.unlock();
    
    return retValue;
  }

  vector<double> PropStatsListener::GetErrorValue()
  {
    err_mtx.lock();

    vector<double> retValue(errors.size());
    copy(errors.begin(), errors.end(), retValue.begin());
    errors.clear();

    err_mtx.unlock();

    return retValue;
  }

  double*** PropStatsListener::PrepareValuesForStats(vector<double*> outputs, Trainingset* set)
  {
    double*** data;
    data = (double***)malloc(sizeof(double**)*outputs.size());
    for (int i = 0; i < outputs.size(); i++)
    {
      vector<double> targetVector = set->GetPatternAt(i)->GetDataOutput();
      double* outputArray = outputs[i];

      int nSize = targetVector.size();

      double** a = (double**)malloc(sizeof(double*)* 2);
      a[0] = (double*)malloc(sizeof(double)*nSize);
      memcpy(a[0], outputArray, sizeof(double)*nSize);
      a[1] = (double*)malloc(sizeof(double)*nSize);
      memcpy(a[1], &(targetVector)[0], sizeof(double)*nSize);

      data[i] = a;
    }

    return data;
  }
}