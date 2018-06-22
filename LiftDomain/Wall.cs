using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class Wall : BaseLiftDomain 
	{
		public IntProperty id = new IntProperty();
		public StringProperty increment_option = new StringProperty();
		public IntProperty organization_id = new IntProperty();
		public StringProperty title = new StringProperty();
		public IntProperty user_id = new IntProperty();

		public Wall()
		{
			BaseTable = "walls";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("id", id);
			attach("increment_option", increment_option);
			attach("organization_id", organization_id);
			attach("title", title);
			attach("user_id", user_id);

            orgProperty = organization_id;
		}


        public long get_wall_count()
        {
            long result = 0;

            DataSet wallCount = doQuery("get_wall_count_internal");

            if (wallCount != null)
            {
                if (wallCount.Tables.Count > 0)
                {
                    if (wallCount.Tables[0].Rows.Count > 0)
                    {
                        result = Convert.ToInt64(wallCount.Tables[0].Rows[0][0]);
                    }
                }
            }

            return result;

        }

        public long remove()
        {
            Appt a = new Appt();
            a.wall_id.Value = getInt("id");
            a.doCommand("delete_from_wall");

            return doCommand("delete");
        }

	}
}
