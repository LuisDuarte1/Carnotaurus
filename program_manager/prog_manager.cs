using System;
using System.Threading;

namespace CarnotaurusV2{
    namespace Program_Manager{
        class Prg_Manager{

            private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            public Prg_Manager(){
                logger.Info("Initializing Program Manager");
                
            }
        }
    }
}