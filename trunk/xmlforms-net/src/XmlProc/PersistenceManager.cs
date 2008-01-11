using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace XmlProc
{
    public class PersistenceManager
    {
        private Hashtable _cache = new Hashtable();

        public IDictionary GetState(string tid)
        {
            return _cache.ContainsKey(tid) ? (IDictionary) _cache[tid] : null;
        }

        public void SaveState(IDictionary state, string tid)
        {
            _cache[tid] = state;
        }

        public void DeleteState(string tid)
        {
            if (_cache.ContainsKey(tid)) _cache.Remove(tid);
        }

        public string AllocateTid()
        {
            return Guid.NewGuid().ToString("N");
        }

        public string AllocateSubTid(string tid)
        {
            if (tid == null || tid.Length == 0)
                return AllocateTid();

            string[] parts = tid.Split('.');
            if (parts.Length == 1)
                return tid + ".1";
            else if (parts.Length == 2)
                return parts[0] + "." + Convert.ToString(Convert.ToInt32(parts[1]) + 1);
            else
                return AllocateTid();
        }
    }
}
