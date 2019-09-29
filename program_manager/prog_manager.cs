using System;
using System.Collections;

namespace CarnotaurusV2{
    namespace Program_Manager{
        class Prg_Manager{

            private Module_Intercommunication m;
            private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            public Prg_Manager(){
                
                logger.Info("Initializing Program Manager ");
                m = new Module_Intercommunication();
            }
        }
    }   
}