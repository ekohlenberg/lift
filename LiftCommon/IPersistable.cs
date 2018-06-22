using System;
using System.Data;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for IPersistable.
	/// </summary>
	public interface IPersistable
	{
		DataSet				getobject();
		DataSet				select();
		DataSet				getall();
		long					insert();
		long					update();
		long					delete();
		long					save();
		long					put();
		long					conditional_put();
	}
}
