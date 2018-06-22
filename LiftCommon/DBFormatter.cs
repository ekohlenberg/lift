using System;
using System.Text;

namespace LiftCommon
{
	/// <summary>
	/// Summary description for DBType.
	/// </summary>
	public class DBFormatter
	{
		public enum DatabaseType
		{
			dbase,
			sqlserver
		}

		public enum TimeUnit
		{
			minutes,
			seconds
		}

		public DBFormatter()
		{
			
		}

		public virtual DatabaseType DBType
		{
			get
			{
				return DBFormatter.DatabaseType.dbase;
			}
		}


		public virtual string ConCat
		{
			get
			{
				return "&";
			}
		}

		public virtual string dateToString( DateTime date )
		{
			string s = "'" + date.ToString() + "'";
			return s;
		}

			

		public virtual string upper
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual string lower
		{
			get
			{
				return string.Empty;
			}
		}

		public virtual int timeValue( DateTime time, TimeUnit timeUnit)
		{
			int result = 0;

			if (timeUnit == TimeUnit.seconds)
			{
				result = time.Hour * 3600 + time.Minute*60 + time.Second;
			}
			else
			{
				result = time.Hour * 60 + time.Minute;
			}

			return result;

		}

		public virtual string dateExpression( string dateCol, string timeCol, TimeUnit timeUnit )
		{
			return  string.Empty;
		}

		public virtual string dateExpression(string dateCol, string hrCol, string minCol, string secCol, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			return  string.Empty;
		}

		public virtual string dateExpression( DateTime datePart, int timePart, TimeUnit timeUnit )
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append( "'" );
			dateExp.Append( datePart.Month.ToString() );
			dateExp.Append( "/");
			dateExp.Append( datePart.Day.ToString() );
			dateExp.Append( "/" );
			dateExp.Append( datePart.Year.ToString() );
			dateExp.Append(" ");

			int hours;
			int minutes;
			int seconds;

			if ( timeUnit == TimeUnit.seconds)
			{
				hours = (int) timePart / 3600;
				minutes = (int) (timePart % 3600 ) / 60;
				seconds = (int) (timePart % 60 );
			}
			else
			{
				hours = (int) timePart / 1440;
				minutes = (int) timePart % 60;
				seconds = 0;
			}

			dateExp.Append( hours.ToString() );
			dateExp.Append(":");
			if (minutes < 0)
			{
				dateExp.Append("0");
			}

			dateExp.Append( minutes );
			dateExp.Append( ":" );
			
			if (seconds < 0)
			{
				dateExp.Append("0");
			}

			dateExp.Append( seconds );

			dateExp.Append("'");

			return dateExp.ToString();
		
		}

	}

	public class DBaseFormatter : DBFormatter
	{
		public DBaseFormatter()
		{
		}

		public override DatabaseType DBType
		{
			get
			{
				return DBFormatter.DatabaseType.dbase;
			}
		}

		public override string upper
		{
			get
			{
				return "UCASE";
			}
		}

		public override string lower
		{
			get
			{
				return "LCASE";
			}
		}


		public override string dateExpression(DateTime datePart, int timePart, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append( "cdate( " );
			dateExp.Append( base.dateExpression( datePart, timePart, timeUnit ));
			dateExp.Append( ")" );


			return dateExp.ToString();
		}

		public override string dateExpression(string dateCol, string hrCol, string minCol, string secCol, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append("cdate(");
			
			
			dateExp.Append("MONTH( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("'/' "); 
			dateExp.Append( ConCat );
			dateExp.Append("DAY( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("'/' ");
			dateExp.Append( ConCat );
			dateExp.Append("YEAR( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("' ' ");
			dateExp.Append( ConCat );

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Append(" ${HRCOL} ");
				dateExp.Append( ConCat );
				dateExp.Append("':' ");
				dateExp.Append( ConCat );
				dateExp.Append(" IIF( ${MINCOL} < 10, '0' & ${MINCOL}, ${MINCOL}) " );
				dateExp.Append( ConCat );
				dateExp.Append("':' ");
				dateExp.Append( ConCat );
				dateExp.Append(" IIF( ${SECCOL} < 10, '0' & ${MINCOL}, ${MINCOL})");
			}
			else
			{
				dateExp.Append(" ${HRCOL} ");
				dateExp.Append( ConCat );
				dateExp.Append("':'  ");
				dateExp.Append( ConCat );
				dateExp.Append(" IIF( ${MINCOL} < 10, '0' & ${MINCOL}, ${MINCOL}) " );
				dateExp.Append( ConCat );
				dateExp.Append("':00'");
			}

			
			dateExp.Append( ")" );

			dateExp.Replace( "${DATECOL}", dateCol );
			dateExp.Replace( "${HRCOL}", hrCol );
			dateExp.Replace( "${MINCOL}", minCol );

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Replace( "${SECCOL}", secCol );
			}
		

			return dateExp.ToString();
		}

		public override string dateExpression(string dateCol, string timeCol, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append("cdate(");
			
			
			dateExp.Append("MONTH( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("'/' "); 
			dateExp.Append( ConCat );
			dateExp.Append("DAY( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("'/' ");
			dateExp.Append( ConCat );
			dateExp.Append("YEAR( ${DATECOL} ) ");
			dateExp.Append( ConCat );
			dateExp.Append("' ' ");
			dateExp.Append( ConCat );

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Append(" INT(${TIMECOL}/3600 ) ");
				dateExp.Append( ConCat );
				dateExp.Append("':' ");
				dateExp.Append( ConCat );
				dateExp.Append(" IIF( INT  ((${TIMECOL} MOD 3600) / 60) < 10, '0' " );
				dateExp.Append( ConCat );
				dateExp.Append(" INT  ((${TIMECOL} MOD 3600) / 60), INT ((${TIMECOL} MOD 3600) / 60) ) ");
				dateExp.Append( ConCat );
				dateExp.Append("':' ");
				dateExp.Append( ConCat );
				dateExp.Append("IIF( ${TIMECOL} MOD 60 < 10, '0' " );
				dateExp.Append( ConCat );
				dateExp.Append("	${TIMECOL} MOD 60, ${TIMECOL} MOD 60 ) ");
			}
			else
			{
				dateExp.Append(" INT(${TIMECOL}/60 )  ");
				dateExp.Append( ConCat );
				dateExp.Append("':'  ");
				dateExp.Append( ConCat );
				dateExp.Append("IIF( ${TIMECOL} MOD 60 < 10, '0' ");
				dateExp.Append( ConCat );
				dateExp.Append("${TIMECOL} MOD 60 , ${TIMECOL} MOD 60");
				dateExp.Append( ConCat );
				dateExp.Append("':00')");
			}

			
			dateExp.Append( ")" );

			dateExp.Replace( "${DATECOL}", dateCol );
			dateExp.Replace( "${TIMECOL}", timeCol );
		

			return dateExp.ToString();
		}



		public override string dateToString(DateTime date)
		{
			string s =  "CDate( " +  base.dateToString (date) + ")";
			return s;
		}

	}

	public class SqlServerFormatter : DBFormatter
	{
		public SqlServerFormatter()
		{
		}


		public override string upper
		{
			get
			{
				return "UPPER";
			}
		}

		public override string lower
		{
			get
			{
				return "LOWER";
			}
		}


		public override string dateExpression(string dateCol, string hrCol, string minCol, string secCol, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append( "CONVERT");
			dateExp.Append( "( DATETIME, CONVERT( VARCHAR(10), MONTH( ${DATECOL} )) + ");
			dateExp.Append( "'/' + CONVERT( VARCHAR(10), DAY( ${DATECOL} ) )+ ");
			dateExp.Append( "'/' + CONVERT( VARCHAR(10), YEAR( ${DATECOL} ))");
			dateExp.Append( " +' ' + ");
			

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Append( " CONVERT( VARCHAR( 10) , ${HRCOL} ) ");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10), ${MINCOL})");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10),  ${SECCOL})");
			}
			else
			{
				dateExp.Append( " CONVERT( VARCHAR( 10) , ${HRCOL}) ");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10),  ${MINCOL})");
				dateExp.Append( " + ':00')");
			}

			dateExp.Append( " ) ");

			dateExp.Replace( "${DATECOL}", dateCol );
			dateExp.Replace( "${HRCOL}", hrCol );
			dateExp.Replace( "${MINCOL}", minCol );

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Replace( "${SECCOL}", secCol );
			}
		

			return dateExp.ToString();
		}

		public override string dateExpression(string dateCol, string timeCol, LiftCommon.DBFormatter.TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append( "CONVERT");
			dateExp.Append( "( DATETIME, CONVERT( VARCHAR(10), MONTH( ${DATECOL} )) + ");
			dateExp.Append( "'/' + CONVERT( VARCHAR(10), DAY( ${DATECOL} ) )+ ");
			dateExp.Append( "'/' + CONVERT( VARCHAR(10), YEAR( ${DATECOL} ))");
			dateExp.Append( " +' ' + ");
			

			if (timeUnit == TimeUnit.seconds)
			{
				dateExp.Append( " CONVERT( VARCHAR( 10) , CAST ( ${TIMECOL}/3600  AS INT ) ) ");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10), CAST( (CAST( ${TIMECOL}  AS INT)% 3600) / 60 AS INT))");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10), CAST( ${TIMECOL}  AS INT)% 60))");
			}
			else
			{
				dateExp.Append( " CONVERT( VARCHAR( 10) , CAST ( ${TIMECOL}/60  AS INT ) ) ");
				dateExp.Append( " 	+':'   +  CONVERT( VARCHAR(10), CAST ( ${TIMECOL}  AS int)% 60)");
				dateExp.Append( " + ':00')");
			}

			dateExp.Replace( "${DATECOL}", dateCol );
			dateExp.Replace( "${TIMECOL}", timeCol );
		

			return dateExp.ToString();
		}


		public override string dateExpression(DateTime datePart, int timePart, TimeUnit timeUnit)
		{
			StringBuilder dateExp = new StringBuilder();

			dateExp.Append( "convert( datetime, " );
			dateExp.Append( base.dateExpression (datePart, timePart, timeUnit));
			dateExp.Append( " ) ");

			return dateExp.ToString();

		}



		public override DatabaseType DBType
		{
			get
			{
				return DBFormatter.DatabaseType.sqlserver;
			}
		}

		public override string ConCat
		{
			get
			{
				return "+";
			}
		}
	} 

}
