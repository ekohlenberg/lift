using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LiftCommon;

namespace LiftDomain
{
    public class UserTimeProperty : LiftCommon.DateTimeProperty
    {
        public override DateTime Value
        {
            get
            {
                DateTime utcTime =  base.Value;
                DateTime result = LiftTime.toUserTime(utcTime);
                return result;
            }
            set
            {
                DateTime userTime = value;
                DateTime utcTime = LiftTime.fromUserTime(userTime);
                base.Value = utcTime;
            }
        }

    }
}
