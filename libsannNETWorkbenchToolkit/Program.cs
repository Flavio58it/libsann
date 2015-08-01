using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libsannNETWorkbenchToolkit
{
    using ExceptionHandling;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += 
                new System.Threading.ThreadExceptionEventHandler(ExceptionManager.DomainUnhandledExceptionHandler);

            // Solve dependencies with Ninject
            var binding = new NinjectBinding();
            binding.Load();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainConsole());
        }
    }
}