using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NLog;
using System.Collections.Specialized;
using System.IO;

namespace XmlProc
{
    public class FormHandlerManager
    {
        private string _formBaseDir = Atmo.AtmoConfig.GetString("XmlProc.FormProcessorFactory.BaseDir");
        public static FormHandlerManager Instance = new FormHandlerManager();
        private static Logger log = LogManager.GetCurrentClassLogger();

        public void Process(String formName, NameValueCollection parameters, XmlWriter output)
        {
            string formFile = Path.Combine(_formBaseDir, formName);
            XmlFormHandler fh = new XmlFormHandler();
            fh.FormPath = formFile;
            fh.Process(output, parameters);
        }
    }
}
