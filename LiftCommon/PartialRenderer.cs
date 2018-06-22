using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Compilation;

namespace LiftCommon
{
    public class PartialRenderer
    {
        public delegate void RenderHelper(DataRow r, Hashtable h);

        protected HttpContext mCtx = null;
        protected DataSet mDs = null;
        protected string mFilename = string.Empty;
        protected RenderHelper mRh = null;
        protected Hashtable h = new Hashtable();
        protected int rowsPerPage = 60;
      

        public PartialRenderer(HttpContext ctx, DataSet ds, string filename, RenderHelper rh)
        {
            mCtx = ctx;
            mDs = ds;
            mFilename = filename;
            mRh = rh;
        }

        public PartialRenderer()
        {
        }

        public static implicit operator string(PartialRenderer st)
        {
            return st.render(st.mCtx, st.mDs, st.mFilename, st.mRh);
        }

        
        protected string render(HttpContext ctx, DataRow r, Hashtable h, string filename, RenderHelper rh)
        {
            if (h == null) return string.Empty;
            if (filename == null) return string.Empty;
            if (h.Count <= 0) return string.Empty;
            if (filename.Length <= 0) return string.Empty;
            string fullpath = ctx.Server.MapPath(filename);

            StringBuilder result = new StringBuilder();

            try
            {
                StreamReader sr = File.OpenText(fullpath);
                result.Append(sr.ReadToEnd());
                sr.Close();

                if (rh != null)
                {
                    rh(r, h);
                }

                foreach (DataColumn dc in r.Table.Columns)
                {
                    replace(result, dc.ColumnName, r[dc.ColumnName]);
                }

                IDictionaryEnumerator de = h.GetEnumerator();

                while (de.MoveNext())
                {
                    replace(result, de.Key.ToString(), de.Value);
                }
            }
            catch (Exception x)
            {
                Logger.log(filename, x, "Error processing template.");
            }

            return result.ToString();
        }
        

        protected  string render(DataSet ds, string filename)
        {
            return render(ds, filename, null);
        }

        protected  string render(HttpContext ctx, DataSet ds, string filename)
        {
            return render(ctx, ds, filename, null);
        }

        protected  string render(HttpContext ctx, DataSet ds, string filename, RenderHelper rh)
        {
            string fullpath = ctx.Server.MapPath(filename);
            return render(ds, fullpath, rh);
        }

        protected virtual bool canRender()
        {
            return true;
        }

        protected virtual string beginPage()
        {
            return string.Empty;
        }

        protected virtual string endPage()
        {
            return string.Empty;
        }

        protected virtual string beginDocument()
        {
            return string.Empty;
        }

        protected virtual string endDocument()
        {
            return string.Empty;
        }

        protected  string render(DataSet ds, string filename, RenderHelper rh)
        {
            if (ds == null) return string.Empty;
            if (filename == null) return string.Empty;
            if (filename.Length <= 0) return string.Empty;
            if (ds.Tables.Count <= 0) return string.Empty;
            if (ds.Tables[0].Rows.Count <= 0) return string.Empty;
            StringBuilder result = new StringBuilder();

            if (!canRender()) return result.ToString();

            try
            {
                result.Append( beginDocument() );

                StringBuilder template = new StringBuilder();
                StreamReader sr = File.OpenText(filename);
                template.Append(sr.ReadToEnd());
                sr.Close();
               // foreach (DataRow r in ds.Tables[0].Rows)
                int iRow = 0;
                for( iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                {
                    if (iRow % rowsPerPage == 0) result.Append( beginPage() );

                    DataRow r = ds.Tables[0].Rows[iRow];
                    result.Append(render(r, template, rh));

                    if (iRow % rowsPerPage == (rowsPerPage - 1)) result.Append(endPage());
                }

                if ((iRow > 0) && ((iRow % rowsPerPage) < (rowsPerPage - 1))) result.Append(endPage());

                result.Append(endDocument());
            }
            catch( Exception x)
            {
                Logger.log(filename, x, "Error processing template");
            }
            
            return result.ToString();

        }


        protected  string render(DataRow r, StringBuilder template)
        {
            return render(r, template, null);
        }

        protected  string render(DataRow r, StringBuilder template, RenderHelper rh)
        {
            if (r == null) return string.Empty;
            if (r.Table.Columns.Count <= 0) return string.Empty;
            if (template == null) return string.Empty;
            if (template.Length <= 0) return string.Empty;
            h.Clear();
            StringBuilder result = new StringBuilder(template.ToString());

            if (rh != null)
            {
                rh(r, h);  // now the r and h thing was just a coincidence...
            }

            foreach (DataColumn dc in r.Table.Columns)
            {
                replace(result, dc.ColumnName, r[dc.ColumnName]);
            }

            IDictionaryEnumerator de = h.GetEnumerator();
            while (de.MoveNext())
            {
                replace(result, de.Key.ToString(), de.Value);
            }

            return result.ToString();
        }

        protected void replace(StringBuilder s, string token, object o)
        {
            string macro = "<%=";
            macro += token;
            macro += "%>";

            string replText = "NULL";
            
            if (o != null)
            {
                if (!o.GetType().Equals(typeof(System.DBNull)))
                {
                    replText = o.ToString();
                }
            }

            s.Replace(macro, replText);
        }
        protected  string render( DataRow r, string filename )
        {
            if (r == null) return string.Empty;
            if (filename == null) return string.Empty;
            if (r.Table.Columns.Count <= 0) return string.Empty;
            if (filename.Length <= 0) return string.Empty;
            
            string result= string.Empty;
            StringBuilder template = new StringBuilder();
         
            try
            {
                StreamReader sr = File.OpenText(filename);
                template.Append(sr.ReadToEnd());
                sr.Close();
                result = render(r, template);
            }
            catch( Exception x)
            {
                Logger.log(filename, x, "Error processing template.");
            }

            return result;
        }

        protected  string render(Hashtable h, string filename)
        {
            if (h == null) return string.Empty;
            if (filename == null) return string.Empty;
            if (h.Count <= 0) return string.Empty;
            if (filename.Length <= 0) return string.Empty;

            StringBuilder result = new StringBuilder();

            try
            {
                StreamReader sr = File.OpenText(filename);
                result.Append(sr.ReadToEnd());
                sr.Close();

                IDictionaryEnumerator de = h.GetEnumerator();

                while (de.MoveNext())
                {
                    replace(result, de.Key.ToString(), de.Value);
                }
            }
            catch (Exception x)
            {
                Logger.log(filename, x, "Error processing template.");
            }

            return result.ToString();
        }



        protected string AppPath
        {
            get
            {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                if (appPath.Length > 1) appPath += "/";
                return appPath;
            }
        }
    }
}
