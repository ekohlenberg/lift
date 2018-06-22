using System;
using System.Data;
using System.Collections;
using LiftCommon;

namespace LiftDomain
{
	public class HeaderImage : BaseLiftDomain 
	{
		public IntProperty  activated = new IntProperty ();
		public StringProperty content_type = new StringProperty();
		public StringProperty filename = new StringProperty();
		public IntProperty height = new IntProperty();
		public IntProperty id = new IntProperty();
		public IntProperty parent_id = new IntProperty();
		public IntProperty size = new IntProperty();
		public StringProperty thumbnail = new StringProperty();
		public IntProperty width = new IntProperty();

		public HeaderImage()
		{
			BaseTable = "header_images";
			AutoIdentity=true;
			PrimaryKey = "id";

			attach("activated", activated);
			attach("content_type", content_type);
			attach("filename", filename);
			attach("height", height);
			attach("id", id);
			attach("parent_id", parent_id);
			attach("size", size);
			attach("thumbnail", thumbnail);
			attach("width", width);
		}
	}
}
