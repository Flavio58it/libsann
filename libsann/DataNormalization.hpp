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
*	Data normalization class, functions collection for data elaboration
*/

#ifndef DATANORM_HPP
#define DATANORM_HPP

#include "Global.h"

namespace libsann
{
  // Data normalization
  class DataNormalization
  {
  public:
    // Data normalization
    static void Normalize(vector<double> *data, DataNormMode norm_mode = DataNormMode::Z_AXIS)
    {
      double f = 0, q = 0, l = 0;
      double min = 999;
      double max = -999;
      uint minid = -1, maxid = -1;

      switch (norm_mode)
      {
      case libsann::Z_AXIS:
      {
                            f = 1.0 / sqrt((double)data->size());
                            for (uint i = 0; i < data->size(); i++)
                            {
                              data->at(i) *= f;
                            }
                            for (uint i = 0; i < data->size(); i++)
                            {
                              l += pow(data->at(i), 2);
                            }
                            data->push_back(f * sqrt(((double)data->size()) - pow(l, 2)));
                            break;
      }
      case libsann::ZERO_MAX:
      {
                              for (uint i = 0; i < data->size(); i++)
                              {
                                if (data->at(i) > max)
                                {
                                  max = data->at(i);
                                }
                              }
                              for (uint i = 0; i < data->size(); i++)
                                data->at(i) /= max;
                              break;
      }
      case libsann::MAPPING_P:
      {
                               for (uint i = 0; i < data->size(); i++)
                               {
                                 if (data->at(i) < min)
                                 {
                                   min = data->at(i);
                                   minid = i;
                                 }
                                 if (data->at(i) > max)
                                 {
                                   max = data->at(i);
                                   maxid = i;
                                 }
                               }

                               if ((maxid < 0) || (minid < 0))
                                 break;

                               for (uint i = 0; i < data->size(); i++)
                                 f += data->at(i);
                               q = data->at(maxid);

                               for (uint i = 0; i < data->size(); i++)
                               {
                                 data->at(i) -= min;
                                 data->at(i) /= max;
                               }

                               if ((f / (double)data->size()) == q)
                                 break;

                               data->at(maxid) = 1.0;
                               break;
      }
      case libsann::NO:
        break;
      }
    }

    static void Normalize(vector<vector<double>> *data, DataNormMode norm_mode = DataNormMode::Z_AXIS)
    {
      for (uint i = 0; i < data->size(); i++)
      {
        Normalize(&data->at(i), norm_mode);
      }
    }
  };
}

#endif