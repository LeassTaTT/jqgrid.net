using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GridTest.examples.loading_data.datatable
{
    public partial class _default : System.Web.UI.Page
    {
        protected DataTable GetDataTableWithSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("invdate");
            dt.Columns.Add("name", typeof(int));
            dt.Columns.Add("amount", typeof(double));
            dt.Columns.Add("tax");
            dt.Columns.Add("total");
            dt.Columns.Add("note");

            return dt;
        }

        protected DataTable GetData()
        {
            Random r = new Random(System.DateTime.Now.Millisecond);
            DataTable dt = GetDataTableWithSchema();

            for (int i = 0; i < 100; i++)
            {
                dt.Rows.Add(new object[] { "a" + i.ToString(), "a", i, Convert.ToDouble(i), "a", "a", "a" });
            }

            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTableGrid.DataSource = GetData();
            DataTableGrid.DataBind();
        }     
        
    }
}
