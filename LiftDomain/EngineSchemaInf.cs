using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class EngineSchemaInf : BaseLiftDomain 
	{
		public StringProperty engine_name = new StringProperty();
		public IntProperty version = new IntProperty();

		public EngineSchemaInf()
		{
			BaseTable = "engine_schema_info";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("engine_name", engine_name);
			attach("version", version);
		}
	}
}
