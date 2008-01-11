using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XmlProc;
using NLog;
using System.Collections.Specialized;
using System.Xml;

namespace XmlProc.Web
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Form runat=server></{0}:Form>")]
    public class Form : WebControl
    {

        protected override void Render(HtmlTextWriter writer)
        {

            string formName = Context.Request.QueryString["form"];
            XmlWriter wr = new XmlTextWriter(Context.Response.Output);
            FormHandlerManager.Instance.Process(formName, Context.Request.Params, wr);
            wr.Flush();
        }
    }
}
