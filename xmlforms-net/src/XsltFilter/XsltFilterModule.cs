using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XsltFilter
{
    public class XsltFilterModule : IHttpModule
    {
        #region IHttpModule Members
        private HttpApplication _theApp;
        public void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Init(HttpApplication context)
        {
            _theApp = context;
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if (app.Request.Url.PathAndQuery.IndexOf("default.aspx") < 0)
                return;
            XsltFilter flt = new XsltFilter(app.Response.Filter);
            flt.OnClose += new XsltFilter.HandleClose(flt_OnClose);
            app.Response.Filter = flt;
        }

        private string FindXslPI(Stream stm)
        {
            stm.Seek(0, SeekOrigin.Begin);
            XmlReader xr = new XmlTextReader(stm);
            bool exit = false;
            Regex re = new Regex("href=\\\"(.+)\\\"");
            while (xr.Read())
            {
                switch (xr.NodeType)
                {
                    case XmlNodeType.ProcessingInstruction:
                        if (xr.Name == "xml-stylesheet")
                        {
                            Match m = re.Match(xr.Value);
                            if (!m.Success) throw new Exception("Unrecognized xml-stylesheet PI: " + xr.Value);
                            return m.Groups[1].Captures[0].Value;
                        }
                        break;
                    case XmlNodeType.Comment:
                        break;
                    case XmlNodeType.XmlDeclaration:
                        break;
                    default:
                        exit = true;
                        break;

                }
                if (exit) break;
            }
            return null;
        }

        void flt_OnClose(XsltFilter flt, System.IO.Stream originalFilter)
        {
            string xsl = FindXslPI(flt);
            if (xsl == null) throw new Exception("Unknown XSL stylesheet");
            flt.Seek(0, SeekOrigin.Begin);
            XmlReader xr = new XmlTextReader(flt);

            XPathDocument doc = new XPathDocument(xr);
            string path = _theApp.Server.MapPath("szablon.xsl");
            XslTransform t = new XslTransform();
            using (XmlTextReader xtr = new XmlTextReader(path))
            {
                t.Load(xtr);
            }
            t.Transform(doc, new XsltArgumentList(), originalFilter);
        }

        #endregion
    }
}
