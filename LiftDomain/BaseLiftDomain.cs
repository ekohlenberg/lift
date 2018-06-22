using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LiftCommon;

namespace LiftDomain
{
    public class BaseLiftDomain : DatabaseObject 
    {
        protected IntProperty orgProperty = null;
        public bool OverrideAutoOrgAssignment = false;

        public override System.Data.DataSet doQuery(string action)
        {
            if ((orgProperty != null) && (!OverrideAutoOrgAssignment))
            {
                Organization org = Organization.Current;
                if (org != null)
                {
                    orgProperty.Value = org.id.Value;
                }
            }

            return base.doQuery(action);
        }

        public override long doCommand(string action)
        {
            if ((orgProperty != null) && (!OverrideAutoOrgAssignment))
            {
                Organization org = Organization.Current;
                if (org != null)
                {
                    orgProperty.Value = org.id.Value;
                }
            }

            return base.doCommand(action);
        }
    }

    
}
