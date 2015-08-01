#include "Som.h"

namespace libsann
{
	Som::Som(uint input_size, uint map_side_size)
	{
		_ASSERT(input_size > 0);

		kmap = new KohonenMap(map_side_size);
		InLayer = new InputLayer(input_size);
	}

	Som::~Som()
	{
		delete kmap;
		delete InLayer;
	}

	void Som::BuildNet()
	{
		// For each ouput neuron
		for (uint r = 0; r < kmap->GetNumberOfRows(); r++)
		{
			for (uint c = 0; c < kmap->GetMapRow(r)->size(); c++)
			{
				// Connects to inputs
				for (uint i = 0; i < InLayer->GetNeurons()->size(); i++)
				{
					Synapse *s = new Synapse();
					s->ConnectPre(InLayer->GetNeuronAt(i));
					kmap->GetMapRow(r)->at(c)->AddSynapse(s);
				}
			}
		}
	}

	void Som::Reset(SynInitMode mode)
	{
		kmap->Reset(mode);
	}

	void Som::SetInput(vector<double> data)
	{
		InLayer->SetInput(data);
	}

	void Som::Exec()
	{
		for (uint r = 0; r < kmap->GetNumberOfRows(); r++)
		{
			for (uint c = 0; c < kmap->GetMapRow(r)->size(); c++)
			{
				kmap->GetMapRow(r)->at(c)->AcquiringPotential();
			}
		}

		for (uint r = 0; r < kmap->GetNumberOfRows(); r++)
		{
			for (uint c = 0; c < kmap->GetMapRow(r)->size(); c++)
			{
				kmap->GetMapRow(r)->at(c)->AcquiringNeighborhoodElicitation(r, c, kmap->GetNeighborhoodExtension(), kmap);
			}
		}

		for (uint r = 0; r < kmap->GetNumberOfRows(); r++)
		{
			for (uint c = 0; c < kmap->GetMapRow(r)->size(); c++)
			{
				kmap->GetMapRow(r)->at(c)->ApplyNeighborhoodElicitation();
			}
		}

		for (uint r = 0; r < kmap->GetNumberOfRows(); r++)
		{
			for (uint c = 0; c < kmap->GetMapRow(r)->size(); c++)
			{
				kmap->NormalizeMap();
			}
		}
	}

	Kneuron* Som::WinnerNeuron()
	{
		return kmap->FindWinnerNeuron();
	}

	vector<uint>* Som::WinnerNeuronCoordinates()
	{
		return kmap->FindWinnerNeuronCoordinates();
	}

	uint Som::GetNeighbohroodExtension()
	{
		return kmap->GetNeighborhoodExtension();
	}

	void Som::SetNeighbohroodExtension(uint value)
	{
		kmap->SetNeighborhoodExtension(value);
	}

	Kneuron* Som::GetOutputNeuronAt(uint r, uint c)
	{
		return kmap->GetNeuron(r, c);
	}

	vector<vector<double>>* Som::GetOutputMatrix()
	{
		return kmap->GetOutputMatrix();
	}

	bool Som::Ucc(int r, int c)
	{
		return kmap->Ucc(r, c);
	}

	vector<double> *Som::KohonenMapToArray()
	{
		return kmap->KohonenToArray();
	}
}