using System;
using System.Threading;
using System.Collections;
using System.Collections.Concurrent;

namespace CarnotaurusV2{
    namespace Program_Manager{
        class Module_Intercommunication{
            private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            private ConcurrentBag<Queue> q_list = new ConcurrentBag<Queue>();
            private Thread t;
            public Module_Intercommunication(){
                logger.Info("Creating communication handler thread.");
                t = new Thread(Module_Communicaton_Handler);
                t.Start();
                logger.Info("Done creating communication handler thread.");
            }

            public void AddBus(Queue new_bus){
                q_list.Add(Queue.Synchronized(new_bus));
            }

            public void Shutdown(){
                t.Abort();
            }
            private void Module_Communicaton_Handler(){
                while(true){
                    foreach(Queue i in q_list){
                        if (i.Count == 0){
                            continue;
                        }
                            var q = i.Dequeue();
                            logger.Debug(q);
                        
                    }
                }
            }
        }
    }
}