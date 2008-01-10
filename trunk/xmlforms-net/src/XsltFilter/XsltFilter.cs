using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Text.RegularExpressions;

namespace XsltFilter
{
    class XsltFilter : System.IO.MemoryStream
    {
        private Stream _next;

        public XsltFilter(Stream next)
        {
            _next = next;
        }

        

        public delegate void HandleClose(XsltFilter flt, Stream originalFilter);
        public event HandleClose OnClose;

        public override void Close()
        {
            try
            {
                if (OnClose != null)
                {
                    OnClose(this, _next);
                }
                else
                {
                    _next.Write(this.GetBuffer(), 0, (int)this.Length);
                }
            }
            catch (Exception ex)
            {
                byte[] buf = Encoding.UTF8.GetBytes(ex.ToString());
                _next.Write(buf, 0, buf.Length);

            }
            finally
            {
                base.Close();
                _next.Close();
            }
        }
    }
}
