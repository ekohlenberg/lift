using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using LiftCommon;

namespace LiftDomain
{
	public class RequestType : BaseLiftDomain 
	{
		public IntProperty id = new IntProperty();
		public LangProperty title = new LangProperty();

		public RequestType()
		{
			BaseTable = "requesttypes";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("id", id);
			attach("title", title);
		}


        public virtual List<RequestType> doList(string action)
        {
            return doQuery<RequestType>(action);
        }

    }

    public class RequestTypeList : List<RequestType>
    {
    }
}
