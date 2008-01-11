using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using Newtonsoft.Json;

namespace XmlProc
{
    
    /// <summary>
    /// Obs³uga wywo³añ JSON-RPC (trpc.js) z javascriptu
    /// </summary>
    public class JSONRPCHandler : MarshalByRefObject
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public string HandleRPCCall(string input)
        {
            log.Debug("Handling RPC Call: {0}", input);
            try
            {
                JavaScriptObject rq = (JavaScriptObject) JavaScriptConvert.DeserializeObject(input);
                IList<object> ops = (IList<object>)rq["operations"];
                foreach (IDictionary<string, object> op in ops)
                {

                }
                JavaScriptObject res = new JavaScriptObject();
                res["status"] = "ok";
                
                res["results"] = new JavaScriptArray();
                return JavaScriptConvert.SerializeObject(res);
            }
            catch (Exception ex)
            {
                log.Warn("RPC Call error: {0}", ex);
                Newtonsoft.Json.JavaScriptObject jso = new JavaScriptObject();
                jso["status"] = "error";
                jso["results"] = new JavaScriptArray(new object[] { ex.Message });
                return Newtonsoft.Json.JavaScriptConvert.SerializeObject(jso);
            }
        }
    }
}
