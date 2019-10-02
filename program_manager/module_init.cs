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

            public void AddModule(string class_path){
                Queue m1 = new Queue();
                Queue m2 = new Queue();
                var classtype = Type.GetType(class_path);
                if (classtype.IsSubclassOf(typeof(Module)) is false){ // check if is a subclass of module
                    logger.Warn(String.Format(
                        "Could not add {0} because it dosen't derive from Module", classtype));
                    return;
                } 
                dynamic c = Activator.CreateInstance(classtype, new object[] {m1, m2}); //Initialize the module
                m.AddBus(m1, m2);
                m_list.Add(c);

            }            
            public void AddModule(string class_path, object[] _args){ //Args respresent addition arguments
                Queue m1 = new Queue();
                Queue m2 = new Queue();
                object[] _args1 = new object[2] {m1,m2};
                object[] args = new object[_args.Length + _args1.Length];
                Array.Copy(_args1, args, _args1.Length);
                Array.Copy(_args, args, _args1.Length);
                var classtype = Type.GetType(class_path);
                if (classtype.IsSubclassOf(typeof(Module)) is false){ // check if is a subclass of module
                    logger.Warn(String.Format(
                        "Could not add {0} because it dosen't derive from Module", classtype));
                    return;
                } 
                dynamic c = Activator.CreateInstance(classtype, args); //Initialize the module
                m.AddBus(m1, m2);
                m_list.Add(c);

            }
        }
    }
}