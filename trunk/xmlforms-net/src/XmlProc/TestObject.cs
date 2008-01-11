using System;
using System.Collections.Generic;
using System.Text;

namespace XmlProc
{
    class TestObject
    {
        public string KeyValue;
        public string Name;
        public DateTime CreatedDate;

        public static TestObject GetRef(string key)
        {
            TestObject to = new TestObject();
            to.KeyValue = key;
            to.Name = "Ala ma kota";
            to.CreatedDate = DateTime.Now;
            return to;
        }
    }
}
