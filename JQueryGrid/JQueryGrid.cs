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
        JavaScriptSerializer _sr;
        HtmlTextWriter _output;
        private static readonly object EventSorted;
        private static readonly object EventSorting;
        private static readonly object EventSearching;
        private static readonly object EventSearched;

        public delegate void JQGridSortEventHandler(object sender, JQGridSortEventArgs e);
        public delegate void JQGridSearchEventHandler(object sender, JQGridSearchEventArgs e);


        static JQueryGrid()
        {    
            EventSorted = new object();
            EventSorting = new object();
            EventSearched = new object();
            EventSearching = new object();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _sr = new JavaScriptSerializer();            
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
            //if (IsBoundUsingDataSourceID)
            //{
                //OnDataBinding(EventArgs.Empty);
            //}            
            // The PerformDataBinding method binds the data in the  
            // retrievedData collection to elements of the data-bound control.
            //PerformDataBinding(retrievedData);                                 

            int rows = Convert.ToInt32(HttpContext.Current.Request.QueryString["rows"]);
            int page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
            string sortExpression = HttpContext.Current.Request.QueryString["sidx"];
            string sortDirection = HttpContext.Current.Request.QueryString["sord"];
            string search = HttpContext.Current.Request.QueryString["_search"];

            DataView view = (DataView)retrievedData;

            PerformSort(view, sortExpression, sortDirection);
            PerformSearch(view, search);
           
            DataTable dt = view.ToTable();
            int count = dt.Rows.Count;
            
            // NEED TO FIX THIS
            int total = (count > 0) ? Convert.ToInt32(Math.Ceiling((double)(count / rows))) : 0;
            if (total == 0) 
                total++;

            int startIndex = (page * rows) - rows;
            int endIndex = count > rows ? startIndex + rows : count;
            int rowCount = count > rows ? rows : count;

            JsonResponse response = new JsonResponse();
            response.page = page;
            response.total = total;
            response.records = count;
            response.rows = new JsonRow[rowCount];


            int index = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                object[] cells = new object[dt.Rows[i].ItemArray.Length];
                for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
                {
                    cells[j] = dt.Rows[i].ItemArray[j];
                }

                JsonRow row = new JsonRow();
                row.id = cells[0].ToString();
                row.cell = cells;
                response.rows[index++] = row;
            }
            
            EmitResponse(_sr.Serialize(response));
        }

        private void PerformSort(DataView view, string sortExpression, string sortDirection)
        {
            if (!String.IsNullOrEmpty(sortExpression))
            {
                JQGridSortEventArgs args = new JQGridSortEventArgs(sortExpression, sortDirection);
                OnSorting(args);
                if (!args.Cancel)
                {
                    view.Sort = String.Format("{0} {1}", sortExpression, sortDirection);
                }
                OnSorted(new EventArgs());
            }
        }

        private void PerformSearch(DataView view, string search)
        {
            if (!String.IsNullOrEmpty(search) && Convert.ToBoolean(search))
            {
                // search here                
                JQGridSearchEventArgs args = new JQGridSearchEventArgs()
                {
                    SearchColumn = HttpContext.Current.Request.QueryString["searchField"],
                    SearchString = HttpContext.Current.Request.QueryString["searchString"],
                    SearchOperation = GetSearchOperationFromString(HttpContext.Current.Request.QueryString["searchOper"])
                };
                OnSearching(args);
                if (!args.Cancel)
                {
                    switch (args.SearchOperation)
                    {
                        case SearchOperation.IsEqualTo:
                            view.RowFilter = String.Format("[{0}] = {1}", args.SearchColumn, args.SearchString);
                            break;
                        default:
                            break;
                    }
                    // do the actual search logic
                }
                OnSearched(new EventArgs());
            }
        }

        private void EmitResponse(string responseText)
        {
            HttpContext.Current.Response.Write(responseText);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        private void ProcessCallBack()
        {            
            string jqGridID = HttpContext.Current.Request.QueryString["jqGridID"];
            if (!String.IsNullOrEmpty(jqGridID) && jqGridID == this.ClientID)
            {   
                GetData().Select(CreateDataSourceSelectArguments(), OnDataSourceViewSelectCallback);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            this._output = writer;
            GetData().Select(CreateDataSourceSelectArguments(), RenderGrid);            
        }

        private void RenderGrid(IEnumerable retrievedData)
        {
            DataView view = (DataView)retrievedData;
            DataTable dt = view.ToTable();

            _output.WriteBeginTag("table");
            _output.WriteAttribute("id", ClientID);
            _output.WriteAttribute("class", "scroll");
            _output.WriteAttribute("cellpadding", "0");
            _output.WriteAttribute("cellspacing", "0");
            _output.Write(HtmlTextWriter.TagRightChar);
            _output.WriteEndTag("table");

            _output.WriteBeginTag("div");
            _output.WriteAttribute("id", ClientID + "_pager");
            _output.WriteAttribute("class", "scroll");
            _output.WriteStyleAttribute("text-align", "center");
            _output.Write(HtmlTextWriter.TagRightChar);
            _output.WriteEndTag("div");

            _output.Write(GetStartupJavascript(dt));
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

        private SearchOperation GetSearchOperationFromString(string searchOperation)
        {
            switch (searchOperation)
            {
                case "eq": return SearchOperation.IsEqualTo;
                case "ne": return SearchOperation.IsNotEqualTo;
                case "lt": return SearchOperation.IsLessThan;
                case "le": return SearchOperation.IsLessOrEqualTo;
                case "gt": return SearchOperation.IsGreaterThan;
                case "ge": return SearchOperation.IsGreaterOrEqualTo;
                case "in": return SearchOperation.IsIn;
                case "ni": return SearchOperation.IsNotIn;
                case "bw": return SearchOperation.BeginsWith;
                case "bn": return SearchOperation.DoesNotEndWith;
                case "ew": return SearchOperation.EndsWith;
                case "en": return SearchOperation.DoesNotEndWith;
                case "cn": return SearchOperation.Contains;
                case "nc": return SearchOperation.DoesNotContain;
                default:
                    throw new Exception("Search operation not known: " + searchOperation);                    
            }
        }

        private string GetColNames(DataTable dt)
        {            
            string[] colNames = new string[dt.Columns.Count];

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                colNames[i] = dt.Columns[i].ColumnName;
            }

            return _sr.Serialize(colNames);
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

            return _sr.Serialize(model);
        }

        protected virtual void OnSorting(JQGridSortEventArgs e)
        {
            bool isBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
            JQGridSortEventHandler handler = (JQGridSortEventHandler)base.Events[EventSorting];
            if (handler != null)
            {
                handler(this, e);
            }
            else if (!isBoundUsingDataSourceID && !e.Cancel)
            {
                throw new HttpException("Unhandled event");
            }
        }

        protected virtual void OnSorted(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventSorted];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSearching(JQGridSearchEventArgs e)
        {
            JQGridSearchEventHandler handler = (JQGridSearchEventHandler)base.Events[EventSearching];
            if (handler != null)
            {
                handler(this, e);
            }            
        }

        protected virtual void OnSearched(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventSearched];
            if (handler != null)
            {
                handler(this, e);
            }
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
                ViewState["Caption"] = value;
            }
        }

        [Category("Action")]
        [Description("GridView_OnSorting")]
        public event JQGridSortEventHandler Sorting
        {
            add
            {
                base.Events.AddHandler(EventSorting, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventSorting, value);
            }
        }

        [Category("Action")]
        [Description("GridView_OnSorted")]
        public event EventHandler Sorted
        {
            add
            {
                base.Events.AddHandler(EventSorted, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventSorted, value);
            }
        }

        [Category("Action")]
        [Description("GridView_OnSearching")]
        public event JQGridSortEventHandler Searching
        {
            add
            {
                base.Events.AddHandler(EventSearching, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventSearching, value);
            }
        }

        [Category("Action")]
        [Description("GridView_OnSearched")]
        public event EventHandler Searched
        {
            add
            {
                base.Events.AddHandler(EventSearched, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventSearched, value);
            }
        }
 

 

 
    }   


    
}
