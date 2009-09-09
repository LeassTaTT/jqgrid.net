using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web;
using System.Security.Permissions;

namespace Trirand.Web.UI.WebControls
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class JQGridColumn : BaseItem
    {
        // Methods
        public JQGridColumn()
        {
        }

        [DefaultValue(150)]
        public int Width
        {
            get
            {
                return ViewState["Width"] == null ? 150 : (int)ViewState["Width"];
            }
            set
            {
                ViewState["Width"] = value;
            }
        }

        [DefaultValue(true)]
        public bool Sortable
        {
            get
            {
                return ViewState["Sortable"] == null ? true : (bool)ViewState["Sortable"];
            }
            set
            {
                ViewState["Sortable"] = value;
            }
        }

        [DefaultValue("")]
        public string DataField
        {
            get
            {
                return ViewState["DataField"] == null ? "" : (string)ViewState["DataField"];
            }
            set
            {
                ViewState["DataField"] = value;
            }
        }

    }

}

