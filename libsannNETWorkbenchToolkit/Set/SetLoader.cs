using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.IO;

namespace libsannNETWorkbenchToolkit.Set
{
    public class SetLoader
    {
        public static int LoadFromCsv(string path)
        {
            var lines = 0;
            if (File.Exists(path))
            {
                lines = File.ReadLines(path).Count();
                if (lines > 0)
                {
                    List<double[]> inMatrix = new List<double[]>();
                    List<double[]> outMatrix = new List<double[]>();
                    List<double[]> inValMatrix = new List<double[]>();
                    List<double[]> outValMatrix = new List<double[]>();
                    int size = 0;
                    int vSize = 0;

                    using (StreamReader r = new StreamReader(path))
                    {
                        while (r.Peek() >= 0)
                        {
                            string section = r.ReadLine();

                            if (!section.StartsWith("#"))
                            {
                                if (size == 0 || vSize == 0)
                                {
                                    if (section.StartsWith("i:"))
                                    {
                                        if (!int.TryParse(section.Split(':').Last(), out size))
                                            throw new Exception("");
                                    }
                                    if (section.StartsWith("v:"))
                                    {
                                        if (!int.TryParse(section.Split(':').Last(), out vSize))
                                            throw new Exception("");
                                    }
                                }

                                if (string.Compare(section, "[input]") == 0)
                                {
                                    BuildMatrix(r, size, out inMatrix);
                                }
                                else if (string.Compare(section, "[output]") == 0)
                                {
                                    BuildMatrix(r, size, out outMatrix);
                                }
                                else if (string.Compare(section, "[inValidation]") == 0)
                                {
                                    BuildMatrix(r, vSize, out inValMatrix);
                                }
                                else if (string.Compare(section, "[outValidation]") == 0)
                                {
                                    BuildMatrix(r, vSize, out outValMatrix);
                                }
                            }
                        }
                    }
                    Assemble(inMatrix, outMatrix, inValMatrix, outValMatrix, size, vSize);
                }
            }

            return lines;
        }

        protected static void Assemble(List<double[]> i, List<double[]> o, List<double[]> vi, List<double[]> vo, int size, int vSize)
        {
            if(i.Count != size || o.Count != size || vi.Count != vSize || vo.Count != vSize)
                throw new Exception("");

            SetModel set = NinjectBinding.GetKernel.Get<SetModel>();

            set.inMatrix = i.ToArray();
            set.outMatrix = o.ToArray();
            set.inValMatrix = vi.ToArray();
            set.outValMatrix = vo.ToArray();
        }

        protected static void BuildMatrix(StreamReader r, int size, out List<double[]> matrix)
        {
            matrix = new List<double[]>();

            for(int i = 0; i < size; i++)
            {
                string row = r.ReadLine();
                string[] inputs = row.Split(';');

                if(inputs.Count() > 0)
                {
                    matrix.Add(ToArray(inputs));
                } else
                {
                    throw new Exception("");
                }
            }
        }

        protected static double[] ToArray(string[] str)
        {
            List<double> array = new List<double>();
            foreach(string s in str)
            {
                double d;
                if(double.TryParse(s, out d))
                {
                    array.Add(d);
                }
            }
            return array.ToArray();
        }
    }
}
