using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace XmlProc
{
    [ElementHandler("dummy", "http://www.rg.com/preprocess")]
    class DummyHandler : IElementHandler
    {
        private static Logger log = LogManager.GetLogger("PRE");

        public void ElementStart(IFormHandlerContext ctx)
        {
            log.Info("Preprocessing {0}:{1}", ctx.CurrentElement.Name, ctx.CurrentElement.NamespaceUri);
            ctx.Output.WriteStartElement("PRE", "http://www.rg.com");
            ctx.Output.WriteAttributeString("NAME", ctx.CurrentElement.Name);
            foreach (AttributeInfo att in ctx.CurrentElement.Attributes.Values)
            {
                ctx.Output.WriteAttributeString(att.Name, att.Value);
            }
            ctx.Output.WriteEndElement();
        }

        public void ElementEnd(IFormHandlerContext ctx)
        {
            
        }
    }
}
