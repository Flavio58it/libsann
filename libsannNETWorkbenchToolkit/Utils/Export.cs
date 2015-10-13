using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsannNETWorkbenchToolkit.Set;

namespace libsannNETWorkbenchToolkit.Utils
{
    internal class Export
    {
        /// <summary>
        /// Print training set to file
        /// </summary>
        /// <param name="stream">Stream of file</param>
        /// <param name="set">Training set</param>
        public static void SetToFile(Stream stream, SetModel set)
        {
            using (StreamWriter w = new StreamWriter(stream, Encoding.UTF8))
            {
                w.WriteLine("# Set exported {0}", DateTime.Now.ToLongDateString());
                var inMatrixCount = set.inMatrix.Count();
                var outMatrixCount = set.outMatrix.Count();

                w.WriteLine("i:{0}", inMatrixCount);
                w.WriteLine("v:{0}", outMatrixCount);

                w.WriteLine("[input]");
                WriteMatrix(set.inMatrix, w);

                w.WriteLine("[output]");
                WriteMatrix(set.outMatrix, w);

                w.WriteLine("[inValidation]");
                WriteMatrix(set.inValMatrix, w);

                w.WriteLine("[outValidation]");
                WriteMatrix(set.outValMatrix, w);
            }
        }

        /// <summary>
        /// Print to file a simple data serie
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="serie []"></param>
        public static void SeriesToFile(StreamWriter stream, double[] serie, string name = "")
        {
            if (!serie.Any())
                return;

            stream.WriteLine("# Serie exported {0} {1}", DateTime.Now.ToLongDateString(), name);

            foreach (var d in serie)
            {
                stream.WriteLine("{0};", d);
            }
        }

        /// <summary>
        /// Print to file a output-target data serie
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="serie [][]"></param>
        public static void TargetOutputsToFile(StreamWriter stream, List<double[][]> serie, string name="")
        {
            if (!serie.Any())
                return;

            stream.WriteLine("# Serie exported {0} {1}", DateTime.Now.ToLongDateString(), name);

            foreach (var pair in serie)
            {
                stream.WriteLine("Outputs;");
                foreach (var output in pair[0])
                {
                    stream.Write("{0};",output);
                }
                stream.WriteLine();

                stream.WriteLine("Target;");
                foreach (var target in pair[1])
                {
                    stream.Write("{0};",target);
                }
                stream.WriteLine();
            }
        }

        /// <summary>
        /// Write matrix to a stream. One column for each pattern
        /// </summary>
        /// <param name="matrix">Matrix to write</param>
        /// <param name="writer">File stream</param>
        protected static void WriteMatrix(double[][] matrix, StreamWriter writer)
        {
            foreach (double[] row in matrix)
            {
                foreach (double value in row)
                {
                    writer.Write("{0};", value);
                }
                writer.WriteLine(); // New line
            }
        }
    }
}
