using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace GridTest.examples.sorting.custom
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CustomSortingGrid.DataSource = GetData();
                CustomSortingGrid.DataBind();
            }
        }

        protected void CustomSortingGrid_Sorting(object sender, Trirand.Web.UI.WebControls.JQGridSortEventArgs e)
        {
            // Cancel the default sorting, which essentially sorts the clicked column
            e.Cancel = true; 

            // Get data from the datasource
            DataTable dt = GetData(); 
            // Set sorting to always sort using the ID column
            // You can change that using the event arguments e.SortExpression (column name) and 
            // e.SortDirection (asc / desc)
            dt.DefaultView.Sort = "id";            

            // Rebind the grid using the sorted DataTable
            CustomSortingGrid.DataSource = dt;
            CustomSortingGrid.DataBind();
        }

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
                dt.Rows.Add(new object[] { "#id " + i.ToString(), System.DateTime.Now, i, Convert.ToDouble(i), i.ToString(), i.ToString(), i.ToString() });
            }

            return dt;
        }
    }
}
