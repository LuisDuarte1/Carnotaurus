using System;
using System.Threading;
using System.Collections;
using System.Collections.Concurrent;

namespace CarnotaurusV2{
    namespace Program_Manager{
        class Module_Intercommunication{
            private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            private ConcurrentDictionary<Queue,Queue> q_dict = new ConcurrentDictionary<Queue,Queue>();
            private Thread t;
            private int interval;
            public Module_Intercommunication(int _interval = 5){
                logger.Info("Creating communication handler thread.");
                t = new Thread(Module_Communicaton_HandlerRecv);
                interval = _interval;
                t.Start();
                logger.Info("Done creating communication handler thread.");
            }

            public void AddBus(Queue new_busrecv, Queue new_bussend){
                if(q_dict.TryAdd(new_busrecv, new_bussend) is false){
                    logger.Warn("Couldn't add queue because it already existed.");
                }
            }

            public void Shutdown(){
                t.Abort();
            }
            private void Module_Communicaton_HandlerRecv(){
                while(true){
                    foreach(Queue i in q_dict.Keys){
                        if (i.Count == 0){
                            continue;
                        }
                            var q = i.Dequeue();
                            logger.Debug(q);
                        
                    }
                    Thread.Sleep(interval);
                }
            }
        }
    }
}