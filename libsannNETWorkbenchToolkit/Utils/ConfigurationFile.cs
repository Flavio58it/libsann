using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using log4net;

namespace libsannNETWorkbenchToolkit.Utils
{
    using libsannNETWorkbenchToolkit.Lib;
    using libsannNETWorkbenchToolkit.Configuration;

    public class ConfigurationFile : ObjectLoggable
    {
        protected static readonly string annCfg = "ann.cfg";
        protected static readonly string backPropCfg = "backprop.cfg";
        protected static readonly string rPropCfg = "rprop.cfg";

        protected string filePath;
        protected AnnBuild annComp;
        protected BackPropagation backPropComp;
        protected ResilientPropagation rPropComp;

        public ConfigurationFile()
        {
            // Retrieve instances
            annComp = NinjectBinding.GetKernel.Get<AnnBuild>();
            backPropComp = NinjectBinding.GetKernel.Get<BackPropagation>();
            rPropComp = NinjectBinding.GetKernel.Get<ResilientPropagation>();
        }

        public void LoadFromFile()
        {
            if(File.Exists(annCfg))
            {
                logger.InfoFormat("Read neural network configuration from file: {0}", annCfg);
                var _annComp = (AnnBuild)ObjectXMLSerializer.Load(annComp, annCfg);
                annComp.SetData(_annComp);
            }
            if(File.Exists(backPropCfg))
            {
                logger.InfoFormat("Read back-propagation training configuration from file: {0}", backPropCfg);
                var _backProp = (BackPropagation)ObjectXMLSerializer.Load(backPropComp, backPropCfg);
                backPropComp.SetData(_backProp);
            }
            if(File.Exists(rPropCfg))
            {
                logger.InfoFormat("Read resilient-propagation training configuration from file: {0}", rPropCfg);
                var _rprop = (ResilientPropagation)ObjectXMLSerializer.Load(rPropComp, rPropCfg);
                rPropComp.SetData(_rprop);
            }
        }

        public void SaveAnnOptToFile()
        {
            logger.InfoFormat("Save neural network configuration to file: {0}", annCfg);
            ObjectXMLSerializer.Save(annComp, annCfg);
        }

        public void SaveBackPropToFile()
        {
            logger.InfoFormat("Save back-propagation training configuration to file: {0}", backPropCfg);
            ObjectXMLSerializer.Save(backPropComp, backPropCfg);
        }

        public void SaveRPropToFile()
        {
            logger.InfoFormat("Save resilient-propagation training configuration to file: {0}", rPropCfg);
            ObjectXMLSerializer.Save(rPropComp, rPropCfg);
        }

        public void SaveAllConfigToFile()
        {
            logger.InfoFormat("Save all configurations to files: {0}, {1}, {2}", rPropCfg,backPropCfg,annCfg);
            ObjectXMLSerializer.Save(annComp, annCfg);
            ObjectXMLSerializer.Save(backPropComp, backPropCfg);
            ObjectXMLSerializer.Save(rPropComp, rPropCfg);
        }
    }
}
