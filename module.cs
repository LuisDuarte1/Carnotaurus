using System;
using System.Threading;
using System.Collections;
using NLog;
using System.Collections.Generic;

namespace CarnotaurusV2{
    class Module{ //Every module must inherit this class for health_checking and other features

        private Queue recv;
        private Queue send;
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private List<Thread> t_list = new List<Thread>(); // Every persistent Thread must be added to this list to be shutdowned
        public Module(Queue _recv, Queue _send){
            recv = _recv;
            _send = send;
        }

        public bool GetStatus(){
            bool s = true;
            foreach(Thread t in t_list){
                if (t.ThreadState.Equals(ThreadState.Running)){
                    s = s & true;
                }
            }
            return s;
        }
        public void Shutdown(){
            foreach(Thread t in t_list){
                t.Abort();
            }
        }
    }
}