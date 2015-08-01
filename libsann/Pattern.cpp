#include "Pattern.h"

namespace libsann
{
	Pattern::Pattern(vector<double> in, vector<double> out)
	{
		for (uint i = 0; i < in.size(); i++)
		{
			dataInput.push_back(in[i]);
		}
		for (uint i = 0; i < out.size(); i++)
		{
			dataOutput.push_back(out[i]);
		}
	}

	Pattern::Pattern(double* in, double* out, uint in_count, uint out_count)
	{
		for (uint i = 0; i < in_count; i++)
		{
			dataInput.push_back(in[i]);
		}
		for (uint i = 0; i < out_count; i++)
		{
			dataOutput.push_back(out[i]);
		}
	}

	void Pattern::SetData(vector<double> input, vector<double> output)
	{
		if ((input.size() <= 0) || (output.size() <= 0))
			throw(new exception("Vector is empty."));

		dataInput.clear();
		dataOutput.clear();

		for (uint i = 0; i < input.size(); i++)
		{
			dataInput.push_back(input[i]);
		}
		for (uint i = 0; i < output.size(); i++)
		{
			dataOutput.push_back(output[i]);
		}
	}
}