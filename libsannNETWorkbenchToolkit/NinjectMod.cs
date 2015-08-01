using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libsannNETWorkbenchToolkit
{
    using libsannNETWorkbenchToolkit.Lib;
    using libsannNETWorkbenchToolkit.Set;
    using libsannNETWorkbenchToolkit.Utils;
    using libsannNETWorkbenchToolkit.Configuration;

    public class NinjectBinding : NinjectModule
    {
        protected static IKernel kernel;

        /// <summary>
        /// Instantiate the application components
        /// </summary>
        public override void Load()
        {
            // Initialize objects
            kernel = new StandardKernel();

            kernel.Bind<AnnBuild>().To<AnnBuild>().InSingletonScope();
            kernel.Bind<BackPropagation>().To<BackPropagation>().InSingletonScope();
            kernel.Bind<ResilientPropagation>().To<ResilientPropagation>().InSingletonScope();
            kernel.Bind<ConfigurationFile>().To<ConfigurationFile>().InSingletonScope();
            kernel.Bind<SetModel>().To<SetModel>().InSingletonScope();
        }

        /// <summary>
        /// Ninject kernel
        /// </summary>
        public static IKernel GetKernel
        {
            get { return kernel; }
        }
    }
}
