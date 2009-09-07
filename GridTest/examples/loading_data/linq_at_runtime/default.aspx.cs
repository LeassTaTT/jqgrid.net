using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GridTest.dbml;

namespace GridTest.examples.loading_data.linq_at_runtime
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserDataClassesDataContext data = new UserDataClassesDataContext();
            LinqAtRuntimeGrid.DataSource = data.Users;
            LinqAtRuntimeGrid.DataBind();
        }
    }
}
