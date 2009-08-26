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
    public class JQueryGrid : GridView
    {
        JavaScriptSerializer sr;        

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

            JavaScriptSerializer sr = new JavaScriptSerializer();
            var result = sr.Serialize(response);

            HttpContext.Current.Response.Write(result);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private void ProcessCallBack()
        {            
            string nd = HttpContext.Current.Request.QueryString["nd"];
            if (!String.IsNullOrEmpty(nd))
            {                
                //DataSourceView dataView = this.GetData();
                //dataView.Select(
                GetData().Select(CreateDataSourceSelectArguments(), OnDataSourceViewSelectCallback);                
            }
        }
        
        protected override void RenderContents(HtmlTextWriter output)
        {            
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

            output.Write(GetStartupJavascript());
        }

        protected string GetColNames()
        {
            string[] colNames = new string[this.Columns.Count];

            for (var i = 0; i < this.Columns.Count; i++)
            {
                DataControlField field = this.Columns[i];
                BoundField boundFiled = (BoundField)field;
                string headerText = boundFiled.HeaderText;
                colNames[i] = String.IsNullOrEmpty(headerText) ? boundFiled.DataField : headerText;
            }

            return sr.Serialize(colNames);
        }

        protected string GetColModel()
        {
            ColModel[] model = new ColModel[this.Columns.Count];

            for (var i = 0; i < this.Columns.Count; i++)
            {
                DataControlField field = this.Columns[i];
                BoundField boundField = (BoundField) field;
                ColModel localModel = new ColModel() { index = boundField.DataField, name = boundField.DataField, width = "100" };

                model[i] = localModel;                
            }

            return sr.Serialize(model);
        }


        protected string GetStartupJavascript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>\n\r");            
            sb.Append("$(document).ready(function() {{");
            sb.AppendFormat("jQuery('#{0}').jqGrid({{\n\r", ClientID);
            sb.AppendFormat("url: '{0}',\n\r", "default.aspx");
            sb.AppendFormat("datatype: 'json',\n\r");
            sb.AppendFormat("colNames: {0},\n\r", GetColNames());
            sb.AppendFormat("colModel: {0},\n\r", GetColModel());
            sb.AppendFormat("rowNum: 10,\n\r", GetColModel());
            sb.AppendFormat("rowList: [10,20,30]\n\r,", GetColModel());
            sb.AppendFormat("pager: jQuery('#{0}'),", ClientID + "_pager");
            sb.AppendFormat("height: 300,");
            sb.AppendFormat("caption: '{0}'", "Server Control jqGrid");
            sb.AppendFormat("}})\n\r");
            sb.AppendFormat(".navGrid('#{0}', {{ edit: true, add: true, del: true}});", ClientID + "_pager");
            sb.Append("}});");
            sb.Append("</script>");

            return sb.ToString();
        }        
    }

    public class ColModel
    {
        public string name { get; set; }
        public string index { get; set; }
        public string width { get; set; }        
    }
}
