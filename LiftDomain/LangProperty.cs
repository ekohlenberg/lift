using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LiftCommon;

namespace LiftDomain
{
    public class LangProperty :StringProperty
    {
        public override string Value
        {
            get
            {
                string langLabel = base.Value;
                return Language.Current.phrase(langLabel);
            }
            
        }
    }
}
