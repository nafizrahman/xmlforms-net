using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using NLog;
using System.Collections.Specialized;

namespace XmlProc
{
    class Program
    {
        static void Main(string[] args)
        {
            NLog.Config.SimpleConfigurator.ConfigureForConsoleLogging(LogLevel.Info);
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: XmlProc <input xml file> <output xml file>");
                return;
            }
            using (StreamReader sr = new StreamReader(args[0]))
            {
                XmlTextReader xtr = new XmlTextReader(sr);
                xtr.Namespaces = true;
                XmlTextWriter xtw = new XmlTextWriter(args[1], Encoding.UTF8);
                xtw.Namespaces = true;
                
                try
                {
                    FormProcessorFactory fact = new FormProcessorFactory();
                    NameValueCollection parm = new NameValueCollection();
                    foreach (string arg in args)
                    {
                        if (arg.StartsWith("/"))
                        {
                            string[] nv = arg.Substring(1).Split('=');
                            if (nv.Length == 2)
                                parm[nv[0]] = nv[1];
                        }
                    }
                    //fact.Process(xtr, xtw, parm);
                    //XmlFormProcessor proc = new XmlFormProcessor(xtr, xtw);
                    //proc.Process();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: input line: {0}: {1}", xtr.LineNumber, ex);
                }
                xtw.Flush();
                xtw.Close();
            }
        }
    }
}
