using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;
using NLog;
using System.Reflection;
using Evaluator;

namespace XmlProc
{
    class JScriptExpressionEvaluator
    {
        //private Evaluator.Evaluator _evaler = new Evaluator.Evaluator();    
        
        public object Eval(string expr)
        {
            return null;// _evaler.Eval(expr);
        }

        public void SetVariable(string varName, object val)
        {
        }
    }
}
