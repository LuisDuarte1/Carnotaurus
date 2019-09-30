using System;
using System.Collections.Generic;
using System.Collections;

namespace CarnotaurusV2{
    namespace Program_Manager{
        class Module_Init{
            private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            private Module_Intercommunication m;
            private List<Module> m_list = new List<Module>();
            
            public Module_Init(Module_Intercommunication _m){
                logger.Info("Initialazing ModuleInit");
                m = _m;
            }

            public void AddModule(string class_name){
                var m1 = new Queue();
                var m2 = new Queue();
                var classtype = Type.GetType(class_name);
                dynamic c = Activator.CreateInstance(classtype);
                if (classtype.IsSubclassOf(typeof(Module)) is false){
                    logger.Warn(String.Format(
                        "Could not add {0} because it dosen't derive from Module", classtype));
                }
                c.recv = m1;
                c.send = m2;
                
            }
        }
    }
}