using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using LiftCommon;

namespace LiftDomain
{
	public class GroupRelationshipType : BaseLiftDomain 
	{
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty id = new IntProperty();
		public LangProperty name = new LangProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();

		public GroupRelationshipType()
		{
			BaseTable = "group_relationship_types";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("created_at", created_at);
			attach("id", id);
			attach("name", name);
			attach("updated_at", updated_at);
		}

        public virtual List<GroupRelationshipType> doList(string action)
        {
            return doQuery<GroupRelationshipType>(action);
        }
	}
}
