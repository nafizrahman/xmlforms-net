using System;
using System.Collections.Generic;
using System.Text;
using AjaxPro;
using System.Collections;
using Spring.Expressions;
using Newtonsoft.Json;
using System.IO;


namespace XmlProc
{
    public class TestJSON
    {
        public string Name;
        public double Account;
        public DateTime Birth;
        public TestJSON Nephew;

        public static String SerializeTest1()
        {
            ArrayList al = new ArrayList();
            al.Add("Ala");
            al.Add("ma");
            al.Add(34.5);
            al.Add(DateTime.Now);
            return JavaScriptSerializer.Serialize(al);
        }

        public static string SerializeTest2(object obj)
        {
            return JavaScriptSerializer.Serialize(obj);
        }

        public static string SerializeTest3()
        {
            Hashtable ht = new Hashtable();
            ht["person"] = "Alicja";
            ht["action"] = "ma";
            ht["item"] = "koty";
            ht["count"] = 99;
            //return JavaScriptSerializer.Serialize(ht);
            JsonSerializer ser = new JsonSerializer();
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, ht);
            object obj = ser.Deserialize(new JsonReader(new StringReader(sw.ToString())));
            return sw.ToString();
        }

        public static string SerializeTest4()
        {
            return null;
        }

        public static string SerializeTest5()
        {
            TestJSON t = new TestJSON();
            t.Account = 33;
            t.Birth = DateTime.Now;
            t.Name = "Zenek";
            t.Nephew = new TestJSON();
            t.Nephew.Name = "Zbynio";
            t.Nephew.Account = 99;
            t.Nephew.Birth = DateTime.Now.AddDays(-5);
            //return JavaScriptSerializer.Serialize(t);
            JsonSerializer ser = new JsonSerializer();
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, t);
            return sw.ToString();
        }
    }
}
