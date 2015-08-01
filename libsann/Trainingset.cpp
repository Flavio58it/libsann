#include "Trainingset.h"

namespace libsann 
{
	Trainingset::Trainingset(vector<vector<double>> in, vector<vector<double>> out)
	{
		_ASSERT(in.size() == out.size());

		for (uint i = 0; i < in.size(); i++)
		{
			patternSet.push_back(new Pattern(in[i], out[i]));
		}
	}

	Trainingset::Trainingset(double** matrix_in, double** matrix_out, uint rows, uint n_in, uint n_out)
	{
		for (uint r = 0; r < rows; r++)
		{
			patternSet.push_back(new Pattern(matrix_in[r], matrix_out[r], n_in, n_out));
		}
	}

	Trainingset::~Trainingset()
	{
		for (uint i = 0; i < patternSet.size(); i++)
		{
			delete patternSet[i];
		}
	}

	void Trainingset::AddPattern(Pattern *p)
	{
		patternSet.push_back(p);
	}

	void Trainingset::RemovePattern(uint index)
	{
		if (index >= patternSet.size())
			return;
		patternSet.erase(patternSet.begin() + index);
	}

	uint Trainingset::size()
	{
		return patternSet.size();
	}
}