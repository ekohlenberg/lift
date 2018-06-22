using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
    public enum EncouragementType
    {
        Update = 0,
        Comment = 1,
        Praise = 2,
        Report = 3
    };

	public class Encouragement : BaseLiftDomain 
	{
        public const EncouragementType Update = EncouragementType.Update;
        public const EncouragementType Comment = EncouragementType.Comment;
        public const EncouragementType Praise = EncouragementType.Praise;
        public const EncouragementType Report = EncouragementType.Report;

		public UserTimeProperty approved_at = new UserTimeProperty();
		public UserTimeProperty created_at = new UserTimeProperty();
		public IntProperty encouragement_type = new IntProperty();
		public StringProperty from = new StringProperty();
		public StringProperty from_email = new StringProperty();
		public IntProperty id = new IntProperty();
		public IntProperty listed = new IntProperty();
		public StringProperty note = new StringProperty();
		public UserTimeProperty post_date = new UserTimeProperty();
		public IntProperty request_id = new IntProperty();
		public UserTimeProperty updated_at = new UserTimeProperty();
		public IntProperty user_id = new IntProperty();
        public IntProperty is_approved = new IntProperty();

		public Encouragement()
		{
			BaseTable = "encouragements";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("approved_at", approved_at);
			attach("created_at", created_at);
			attach("encouragement_type", encouragement_type);
			attach("from", from);
			attach("from_email", from_email);
			attach("id", id);
			attach("listed", listed);
			attach("note", note);
			attach("post_date", post_date);
			attach("request_id", request_id);
			attach("updated_at", updated_at);
			attach("user_id", user_id);
            attach("is_approved", is_approved );

		}

   





        public long save_encouragement()
        {
            long result = doCommand("save");

            Request pr = new Request();
            pr["id"] = this["request_id"];
            pr["last_action"] = this["updated_at"];
            pr["is_approved"]= 1;

            pr.doCommand("update");

            return result;
        }
	}
}
