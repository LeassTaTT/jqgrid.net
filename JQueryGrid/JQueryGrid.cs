using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;
using System.Reflection;

namespace Trirand.Web.UI.WebControls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:jqgrid runat=server></{0}:jqgrid>")]
    public class JQueryGrid : CompositeDataBoundControl
    {
        JavaScriptSerializer sr;
        HtmlTextWriter output;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            sr = new JavaScriptSerializer();            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ProcessCallBack();
        }

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            base.CreateChildControls();
            return 0;
        }

        private void OnDataSourceViewSelectCallback(IEnumerable retrievedData)
        {
            // Call OnDataBinding only if it has not already been 
            // called in the PerformSelect method.
            if (IsBoundUsingDataSourceID)
            {
                OnDataBinding(EventArgs.Empty);
            }

            DataView view = (DataView)retrievedData;
            DataTable dt = view.ToTable();
            // The PerformDataBinding method binds the data in the  
            // retrievedData collection to elements of the data-bound control.
            //PerformDataBinding(retrievedData);                      
            JsonResponse response = new JsonResponse();

            int rows = Convert.ToInt32(HttpContext.Current.Request.QueryString["rows"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            int count = dt.Rows.Count;
            int total = (count > 0) ? Convert.ToInt32(Math.Ceiling((double)(count / rows))) : 0;

            response.page = page;
            response.total = total;
            response.records = count;
            response.rows = new JsonRow[rows];

            int index = 0;
            for (int i = (page - 1) * rows; i < (page - 1) * rows + rows; i++)
            {
                object[] newData = new object[] { dt.Rows[i][0],dt.Rows[i][1],dt.Rows[i][2],
                                                    dt.Rows[i][3],dt.Rows[i][4],dt.Rows[i][5],dt.Rows[i][6] };                

                JsonRow row = new JsonRow();
                row.id = newData[0].ToString();
                row.cell = newData;
                response.rows[index++] = row;
            }
            
            EmitResponse(sr.Serialize(response));
        }

        private void EmitResponse(string responseText)
        {
            HttpContext.Current.Response.Write(responseText);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private void ProcessCallBack()
        {            
            string nd = HttpContext.Current.Request.QueryString["nd"];
            if (!String.IsNullOrEmpty(nd))
            {   
                GetData().Select(CreateDataSourceSelectArguments(), OnDataSourceViewSelectCallback);
            }
        }
        
        

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            this.output = writer;
            GetData().Select(CreateDataSourceSelectArguments(), RenderGrid);            
        }

        private void RenderGrid(IEnumerable retrievedData)
        {
            DataView view = (DataView)retrievedData;
            DataTable dt = view.ToTable();

            output.WriteBeginTag("table");
            output.WriteAttribute("id", ClientID);
            output.WriteAttribute("class", "scroll");
            output.WriteAttribute("cellpadding", "0");
            output.WriteAttribute("cellspacing", "0");
            output.Write(HtmlTextWriter.TagRightChar);
            output.WriteEndTag("table");

            output.WriteBeginTag("div");
            output.WriteAttribute("id", ClientID + "_pager");
            output.WriteAttribute("class", "scroll");
            output.WriteStyleAttribute("text-align", "center");
            output.Write(HtmlTextWriter.TagRightChar);
            output.WriteEndTag("div");

            output.Write(GetStartupJavascript(dt));
        }

        protected string GetStartupJavascript(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>\n\r");            
            sb.Append("$(document).ready(function() {");
            sb.AppendFormat("jQuery('#{0}').jqGrid({{\n\r", ClientID);
            sb.AppendFormat("url: '{0}?jqGridID={1}',\n\r", "default.aspx", this.ClientID);
            sb.AppendFormat("datatype: 'json',\n\r");
            sb.AppendFormat("colNames: {0},\n\r", GetColNames(dt));
            sb.AppendFormat("colModel: {0},\n\r", GetColModel(dt));
            sb.AppendFormat("viewrecords: true,\n\r", GetColModel(dt));
            sb.AppendFormat("rowNum: 10,\n\r");
            sb.AppendFormat("rowList: [10,20,30],\n\r");
            sb.AppendFormat("pager: jQuery('#{0}'),\n\r", ClientID + "_pager");            
            sb.AppendFormat("multiselect: {0},", MultiSelect.ToString().ToLower());
            sb.AppendFormat("caption: '{0}'", "Server Control jqGrid");
            sb.AppendFormat("}})\n\r");
            sb.AppendFormat(".navGrid('#{0}', {{ edit: true, add: true, del: true}});", ClientID + "_pager");
            sb.Append("});");
            sb.Append("</script>");

            return sb.ToString();
        }

        private string GetColNames(DataTable dt)
        {            
            string[] colNames = new string[dt.Columns.Count];

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                colNames[i] = dt.Columns[i].ColumnName;
            }

            return sr.Serialize(colNames);
        }

        private string GetColModel(DataTable dt)
        {
            ColModel[] model = new ColModel[dt.Columns.Count];

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                model[i] = new ColModel()
                {
                    index = dt.Columns[i].ColumnName,
                    name = dt.Columns[i].ColumnName,
                    width = "100"
                };
            }

            return sr.Serialize(model);
        }

        [DefaultValue(false)]
        public bool MultiSelect
        {
            get
            {
                object o = ViewState["MultiSelect"];
                return o != null ? (bool)o : false;
            }
            set
            {
                ViewState["MultiSelect"] = value;
            }
        }

        [DefaultValue("")]
        public string Caption
        {
            get
            {
                object o = ViewState["Caption"];
                return o != null ? (string)o : string.Empty;
            }
            set
            {
                ViewState["MultiSelect"] = value;
            }
        }
    }   


    
}
