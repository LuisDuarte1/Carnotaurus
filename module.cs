using System;
using System.Threading;
using System.Collections;
using NLog;
using System.Collections.Generic;

namespace CarnotaurusV2{
    class Module{ //Every module must inherit this class for health_checking and other features

        protected Queue recv;
        protected Queue send;
        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected List<Thread> t_list = new List<Thread>(); // Every persistent Thread must be added to this list to be shutdowned
        public Module(Queue _recv, Queue _send){
            recv = _recv;
            send = _send;
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