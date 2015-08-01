#include "KohonenMap.h"

namespace libsann
{
	KohonenMap::KohonenMap(uint map_side_size)
	{
		if (map_side_size < Lower_limit)
			throw(new exception("Kohonen units number can not be less than 1024 (side limit: 32 => 32^2 = 1024 units)"));
		if (map_side_size > Upper_limit)
			throw(new exception("Kohonen units number must be less than 2143690000 (side limit: 46300 => 46300^2 = 2143690000 units)"));

		neighborhood_extension = map_side_size / 8;

		// Creates the Kohonen map
		for (uint r = 0; r < map_side_size; r++)
		{
			vector<Kneuron*> *v = new vector<Kneuron*>();
			for (uint c = 0; c < map_side_size; c++)
			{
				Kneuron *k = new Kneuron();
				k->SetActivationMode(IDENTITY);
				v->push_back(k);
			}
			map.push_back(v);
		}
	}

	KohonenMap::~KohonenMap()
	{
		for (uint i = 0; i < map.size(); i++)
		{
			for (uint j = 0; j < map[i]->size(); j++)
			{
				delete (&map)[i][j];
				//delete map.at[i][j];
			}
			delete map.at(i);
		}
	}

	void KohonenMap::SetNeighborhoodExtension(uint value)
	{
		_ASSERT(value <= neighborhood_extension);
		neighborhood_extension = value;
	}

	vector<Kneuron*>* KohonenMap::GetMapRow(uint index)
	{
		_ASSERT(index < map.size());
		return map[index];
	}

	vector<uint>* KohonenMap::FindWinnerNeuronCoordinates()
	{
		double max_activation = -1;
		uint row = 0;
		uint column = 0;

		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map[r]->size(); c++)
			{
				if (max_activation < map[r]->at(c)->Output())
				{
					max_activation = map[r]->at(c)->Output();
					row = r;
					column = c;
				}
			}
		}

		vector<uint> *v = new vector<uint>();
		v->push_back(row);
		v->push_back(column);

		return v;
	}

	Kneuron* KohonenMap::FindWinnerNeuron()
	{
		double max_activation = -1;
		uint row = 0;
		uint column = 0;

		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map[r]->size(); c++)
			{
				if (max_activation < map[r]->at(c)->Output())
				{
					max_activation = map[r]->at(c)->Output();
					row = r;
					column = c;
				}
			}
		}
		return map[row]->at(column);
	}

	vector<vector<double>>* KohonenMap::GetOutputMatrix()
	{
		vector<vector<double>>* outputs = new vector<vector<double>>();
		for (uint r = 0; r < map.size(); r++)
		{
			vector<double> v;
			for (uint c = 0; c < map[r]->size(); c++)
			{
				v.push_back(map[r]->at(c)->Output());
			}
			outputs->push_back(v);
		}
		return outputs;
	}

	Kneuron* KohonenMap::GetNeuron(uint r, uint c)
	{
		_ASSERT((r < map.size()) && (c < map[r]->size()));
		return map[r]->at(c);
	}

	void KohonenMap::Reset(SynInitMode mode)
	{
		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map[r]->size(); c++)
			{
				map[r]->at(c)->ResetAllSynapses(mode);
			}
		}
	}

	bool KohonenMap::UnitCoordinatesCoherence(int r, int c)
	{
		if ((r < 0) || (r >(int)map.size() - 1))
			return false;
		if ((c < 0) || (c >(int)map[r]->size() - 1))
			return false;

		return true;
	}

	bool KohonenMap::Ucc(int r, int c)
	{
		return UnitCoordinatesCoherence(r, c);
	}

	void KohonenMap::NormalizeMap()
	{
		double max = -999;
		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map[r]->size(); c++)
			{
				if (max < map[r]->at(c)->Output())
				{
					max = map[r]->at(c)->Output();
				}
			}
		}
		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map[r]->size(); c++)
			{
				map[r]->at(c)->SetOutput(map[r]->at(c)->Output() / max);
			}
		}
	}

	vector<double>* KohonenMap::KohonenToArray()
	{
		vector<double> *ret = new vector<double>();
		for (uint r = 0; r < map.size(); r++)
		{
			for (uint c = 0; c < map.at(r)->size(); c++)
			{
				ret->push_back(map[r]->at(c)->Output());
			}
		}
		return ret;
	}
}