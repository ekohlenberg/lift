using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class SchemaMigration : BaseLiftDomain 
	{
		public StringProperty version = new StringProperty();

		public SchemaMigration()
		{
			BaseTable = "schema_migrations";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("version", version);
		}
	}
}
