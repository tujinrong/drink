using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DrinkService.Data.Common
{
    public class Locker : IDisposable
    {
        static Dictionary<string, AutoResetEvent> waitHandleDic = new Dictionary<string, AutoResetEvent>();

        List<WaitHandle> waitHandleList = new List<WaitHandle>();

        //static Dictionary<string, DateTime> lockTime = new Dictionary<string, DateTime>();
        public int WaitTimeOut = 30;
        public int GarbageTime = 300;

        public Locker()
        {
        }

        public Locker(string key)
        {
            _Locker(new string[] { key });

        }
        public Locker(string key1, string key2)
        {
            _Locker(new string[] { key1, key2 });
        }

        public Locker(string[] keys)
        {
            _Locker(keys);
        }
        private static int waitHandleCount = 0;
        private static object lockObject = new object();
        protected void _Locker(string[] keys)
        {
            lock (lockObject)
            {
                if (waitHandleCount == 0)
                {
                    waitHandleDic = new Dictionary<string, AutoResetEvent>();
                }

                waitHandleCount += keys.Length;
                // 防止互锁
                Array.Sort(keys);
                foreach (string key in keys)
                {
                    if (!waitHandleDic.ContainsKey(key))
                    {
                        waitHandleDic.Add(key, new AutoResetEvent(true));
                        //lockTime.Add(key, DateTime.Now);
                    }
                    //else
                    //{
                    //    //lockTime[key] = DateTime.Now;
                    //}
                    waitHandleList.Add(waitHandleDic[key]);

                }


                //List<string> gabage = new List<string>();
                //foreach(string key in lockTime.Keys)
                //{
                //    if (DateTime.Now.Subtract(lockTime[key]).Seconds>GarbageTime)
                //    {
                //        gabage.Add(key);
                //    }
                //}
                //foreach(string key in gabage)
                //{
                //    lockObjects.Remove(key);
                //    lockTime.Remove(key);
                //}
            }

            //WaitHandle.WaitAll(waitHandleList.ToArray());

            foreach (AutoResetEvent locker in waitHandleList)
            {
                //设置最多阻塞60秒
                locker.WaitOne(60000);
            }
        }

        public void Dispose()
        {
            lock (lockObject)
            {

                foreach (AutoResetEvent locker in waitHandleList)
                {
                    locker.Set();
                }
                waitHandleCount -= waitHandleList.Count;
            }

        }
    }
}
