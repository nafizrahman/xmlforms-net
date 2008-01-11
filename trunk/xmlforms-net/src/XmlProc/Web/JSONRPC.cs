using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using XmlProc;

namespace XmlProc.Web
{
    [ToolboxData("<{0}:Form runat=server></{0}:Form>")]
    public class JSONRPC : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            StreamReader sr = new StreamReader(Page.Request.InputStream, Page.Request.ContentEncoding);
            string inputData = sr.ReadToEnd();
            Page.Response.ContentType = "text/json";
            XmlProc.JSONRPCHandler handler = new XmlProc.JSONRPCHandler();
            string outputData = handler.HandleRPCCall(inputData);
            Page.Response.Write(outputData);
        }
    }
}
