using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Sooda;
using Test.BusinessObjects;

namespace TestApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SoodaTransaction.DefaultObjectsAssembly = typeof(Contact).Assembly;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}