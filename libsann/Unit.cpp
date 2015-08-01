#include "Unit.h"

namespace libsann
{
	Unit::~Unit()
	{
		for (uint i = 0; i < inSynapses.size(); i++)
			delete inSynapses[i];
	}

	void Unit::AddSynapse(uint position, Synapse* syn)
	{
		vector<Synapse*>::iterator i;
		i = inSynapses.begin();

		inSynapses.insert(i + position, syn);
	}

	void Unit::AddSynapseOut(uint position, Synapse* syn)
	{
		vector<Synapse*>::iterator i;
		i = outSynapses.begin();

		outSynapses.insert(i + position, syn);
	}

	void Unit::AddSynapse(Synapse* syn)
	{
		vector<Synapse*>::iterator i;
		i = inSynapses.end();

		inSynapses.insert(i, syn);
	}

	void Unit::AddSynapseOut(Synapse* syn)
	{
		vector<Synapse*>::iterator i;
		i = outSynapses.end();

		outSynapses.insert(i, syn);
	}

	void Unit::AddSynapses(vector<Synapse*> SynapseCollection)
	{
		int i = 0;
		for (uint j = 0; j < SynapseCollection.size(); j++)
		{
			inSynapses.push_back(SynapseCollection[i++]);
		}
	}

	void Unit::AddSynapsesOut(vector<Synapse*> SynapseCollection)
	{
		int i = 0;
		for (uint j = 0; j < SynapseCollection.size(); j++)
		{
			outSynapses.push_back(SynapseCollection[i++]);
		}
	}

	void Unit::RemoveAllSynapses()
	{
		inSynapses.clear();
		outSynapses.clear();
	}

	void Unit::ZeroResetAllSynapses()
	{
		for (uint i = 0; i < inSynapses.size(); i++)
		{
			inSynapses[i]->Reset();
			inSynapses[i]->SetUpdateValue(0.1);
		}
	}

	void Unit::ResetAllSynapses(SynInitMode mode, double alfa, double beta)
	{
		switch (mode)
		{
		case SynInitMode::POSITIVE_NEGATIVE_RANGE:
		{
																ZeroResetAllSynapses();

																for (uint i = 0; i < inSynapses.size(); i++)
																{
																	// set synapse from 1 to -1
																	inSynapses[i]->SetWeight(((rand() % 20000 / 10000.0) - 1.0) + (rand() % 10000 / 100000000.0f));
																}
																break;
		}
		case SynInitMode::POSITIVE_RANGE:
		{
													   ZeroResetAllSynapses();

													   for (uint i = 0; i < inSynapses.size(); i++)
													   {
														   // set synapse from 1 to 0
														   inSynapses[i]->SetWeight((double)(rand() / RAND_MAX));
													   }
													   break;
		}
		case SynInitMode::FAN_IN:
		{
											   ZeroResetAllSynapses();

											   // Using "Fan-In Weight Randomization"
											   for (uint i = 0; i < inSynapses.size(); i++)
											   {
												   double r = ((rand() % 20000 / 10000.0) - 1.0) + (rand() % 10000 / 100000000.0f);
												   inSynapses[i]->SetWeight((-1.0 / alfa) + r * ((+1 - -1) / alfa));
											   }
											   break;
		}
		case SynInitMode::NGUYEN_WINDROW:
		{
													   // Apply "Nguyen-Widrow Randomization"
													   for (uint i = 0; i < inSynapses.size(); i++)
													   {
														   inSynapses[i]->SetWeight(beta * inSynapses[i]->GetWeight() / alfa);
													   }
													   break;
		}
		case SynInitMode::ZERO:
		{
											 for (uint i = 0; i < inSynapses.size(); i++)
											 {
												 inSynapses[i]->SetWeight(0.0);
											 }
											 break;
		}
		case SynInitMode::NONE:
			break;
		}
	}

	Synapse* Unit::GetSynapseAt(uint index)
	{
		if (index >= inSynapses.size())
			throw(new exception("Synapses vector: The index required is out of range."));
		return inSynapses[index];
	}

	Synapse* Unit::GetSynapseOutAt(uint index)
	{
		if (index >= outSynapses.size())
			throw(new exception("Synapses vector: The index required is out of range."));
		return outSynapses[index];
	}
}