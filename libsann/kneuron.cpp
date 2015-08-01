#include "Kneuron.h"

namespace libsann
{
	Kneuron::Kneuron()
	{
		Neuron::Neuron();
		NeighborhoodElicitation = 0;
	}

	void Kneuron::AcquiringPotential()
	{
		double accumulator = 0;
		for (uint i = 0; i < inSynapses.size(); i++)
		{
			accumulator += pow(inSynapses[i]->GetPreNeuron()->Output() - inSynapses[i]->GetWeight(), 2);
		}
		potential = accumulator;

		output = activation_function->TransFunction(sqrt(potential));
	}

	void Kneuron::AcquiringNeighborhoodElicitation(int r, int c, uint ne_ext, KohonenMap* kmap_ptr)
	{
		// r & c are coordinates of the current units
		// ne_ext is the extension of the neighborhood
		// map_ptr is the pointer to Kohonen map

		// This method is a good solution for topological localization of current
		// neuron in a matrix and a fast method to access to the neighborhoods of the neuron

		NeighborhoodElicitation = 0;

		if (ne_ext == 0)
			return;

		_ASSERT(ne_ext <= 2147483647); // Prevent signed vulnerability

		int neext = (int)ne_ext;

		// Elicitation 
		for (int _r = r - neext; _r < r + neext; _r++)
		{
			for (int _c = c - neext; _c < c + neext; _c++)
			{
				if (!kmap_ptr->Ucc(_r, _c))
					continue;
				NeighborhoodElicitation += (kmap_ptr->GetMapRow(_r)->at(_c)->Output() * 0.25);
			}
		}

		// Inhibition
		for (int _c = c - neext - 1; _c < c + neext - 1; _c++)
		{
			if (!kmap_ptr->Ucc(r + neext + 1, _c))
				continue;
			NeighborhoodElicitation -= (kmap_ptr->GetMapRow(r + neext + 1)->at(_c)->Output() * 0.25);
		}
		for (int _c = c - neext - 1; _c < c + neext - 1; _c++)
		{
			if (!kmap_ptr->Ucc(r - neext - 1, _c))
				continue;
			NeighborhoodElicitation -= (kmap_ptr->GetMapRow(r - neext - 1)->at(_c)->Output() * 0.25);
		}
		for (int _r = r - neext; _r < r + neext; _r++)
		{
			if (!kmap_ptr->Ucc(_r, c - neext - 1))
				continue;
			NeighborhoodElicitation -= (kmap_ptr->GetMapRow(_r)->at(c - neext - 1)->Output() * 0.25);
			if (!kmap_ptr->Ucc(_r, c + neext + 1))
				continue;
			NeighborhoodElicitation -= (kmap_ptr->GetMapRow(_r)->at(c + neext + 1)->Output() * 0.25);
		}
	}

	void Kneuron::ApplyNeighborhoodElicitation()
	{
		output += 0.5 * NeighborhoodElicitation;
	}
}
