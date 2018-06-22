using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LiftCommon;

namespace LiftDomain
{
    public class PhraseProperty :StringProperty
    {
        public override string Value
        {
            get
            {
                string langLabel = base.Name;
                return Language.Current.phrase(langLabel);
            }
            
        }
    }
}
