﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;

namespace GridTest
{
    public partial class _Default : System.Web.UI.Page
    {
        public class JsonResponse
        {
            public int page { get; set; }
            public int total { get; set; }
            public int records { get; set; }
            public JsonRow[] rows { get; set; }
        }

        public class JsonRow
        {
            public string id;
            public object[] cell { get; set; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //string nd = Request.QueryString["nd"];
            //if (!String.IsNullOrEmpty(nd))
            //{
            //    DataTable dt = GetData();
            //    JsonResponse response = new JsonResponse();

            //    int rows = Convert.ToInt32(Request.QueryString["rows"]);
            //    int page = Convert.ToInt32(Request.QueryString["page"]);
            //    int count = dt.Rows.Count;                
            //    int total = (count > 0) ? Convert.ToInt32(Math.Ceiling( (double) (count / rows))) : 0;

            //    response.page = page;
            //    response.total = total;
            //    response.records = count;
            //    response.rows = new JsonRow[rows];

            //    for (int i = 0; i < rows; i++)
            //    {
            //        object [] newData = new object[] { dt.Rows[i][0],dt.Rows[i][0],dt.Rows[i][0],
            //                                        dt.Rows[i][0],dt.Rows[i][0],dt.Rows[i][0],dt.Rows[i][0] };
                    
            //        DataTable currentDt = GetDataTableWithSchema();
            //        currentDt.Rows.Add(newData);

            //        JsonRow row = new JsonRow();
            //        row.id = newData[0].ToString();
            //        row.cell = newData;
            //        response.rows[i] = row;                    
            //    }

            //    JavaScriptSerializer sr = new JavaScriptSerializer();
            //    var result = sr.Serialize(response);                
                
            //    Response.Write(result);
            //    Response.Flush();
            //    Response.End();
            //}
        }

        public object RowsToDictionary(DataTable table)
        {
            var columns = table.Columns.Cast<DataColumn>().ToArray();
            return table.Rows.Cast<DataRow>().Select(r => columns.ToDictionary(c => c.ColumnName, c => r[c]));
        }

        public Dictionary<string, object> ToDictionary(DataTable table)
        {
            return new Dictionary<string, object>
            {
                { table.TableName, RowsToDictionary(table) }
            };
        }       



        protected DataTable GetDataTableWithSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("invdate");
            dt.Columns.Add("name");
            dt.Columns.Add("amount");
            dt.Columns.Add("tax");
            dt.Columns.Add("total");
            dt.Columns.Add("note");

            return dt;
        }

        protected DataTable GetData()
        {
            Random r = new Random(System.DateTime.Now.Millisecond);
            DataTable dt = GetDataTableWithSchema();
            
            for (int i=0; i<100; i++)
            {
                object[] data = { r.Next(1000), System.DateTime.Now, "Rumen", r.Next(10000), r.Next(10000), r.Next(10000), "Note" };
                dt.Rows.Add(data);
            }

            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JQGrid1.DataSource = GetData();
            JQGrid1.DataBind();
        }
    }
}
