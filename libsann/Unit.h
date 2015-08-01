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
*	 Unit header file
*/

#ifndef UNIT_H
#define UNIT_H

#include "Synapse.h"
#include "Global.h"

namespace libsann
{
  // Interface class for neural network units
  class Unit
  {
  public:
    Unit() { }
    ~Unit();
    // Operations on synapses 
    void AddSynapse(Synapse* syn);
    void AddSynapse(uint position, Synapse* syn);
    void AddSynapses(vector<Synapse*> SynapseCollection);
    virtual void RemoveAllSynapses();
    virtual void ResetAllSynapses(SynInitMode mode, double alfa = 0, double betac = 0);
    inline vector<Synapse*>* GetInSyn()			{ return &inSynapses; }
    Synapse* GetSynapseAt(uint index);
    void AddSynapseOut(Synapse* syn);
    void AddSynapseOut(uint position, Synapse* syn);
    void AddSynapsesOut(vector<Synapse*> SynapseCollection);
    inline vector<Synapse*>* GetOutSyn()		{ return &outSynapses; }
    Synapse* GetSynapseOutAt(uint index);
    // Access to the error of the neuron
    inline void SetDelta(double value)			{ delta = value; }
    inline double GetDelta()					{ return delta; }
    // Get output value after transfer
    virtual inline double Output() = 0;
    // Get derivative
    virtual inline double Derivative() = 0;
    // Update potential of the neuron
    virtual void AcquiringPotential() = 0;

  protected:
    // Synapses collection
    vector<Synapse*> inSynapses;		// IN synapses
    vector<Synapse*> outSynapses;		// OUT synapses
    // Delta error
    double delta;
    // Reset all synapses
    void ZeroResetAllSynapses();
  };
}

#endif