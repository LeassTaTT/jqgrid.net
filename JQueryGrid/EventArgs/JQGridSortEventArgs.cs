using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Permissions;
using System.ComponentModel;

namespace Trirand.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class JQGridSortEventArgs : CancelEventArgs
    {
        // Fields
        private string _sortDirection;
        private string _sortExpression;

        // Methods
        public JQGridSortEventArgs(string sortExpression, string sortDirection)
        {
            this._sortExpression = sortExpression;
            this._sortDirection = sortDirection;
        }

        // Properties
        public string SortDirection
        {
            get
            {
                return this._sortDirection;
            }
            set
            {
                this._sortDirection = value;
            }
        }

        public string SortExpression
        {
            get
            {
                return this._sortExpression;
            }
            set
            {
                this._sortExpression = value;
            }
        }
    }
}
