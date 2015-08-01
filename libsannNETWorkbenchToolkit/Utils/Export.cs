using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libsannNETWorkbenchToolkit.Utils
{
    internal class Export
    {
        public static void SetToFile(Stream stream)
        {
            using (StreamWriter w = new StreamWriter(stream, Encoding.UTF8))
            {
                // TODO: implemento esportazione
            }
        }
    }
}
