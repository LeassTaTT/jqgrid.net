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
    public class JQGridSearchEventArgs : CancelEventArgs
    {
        // Fields
        private string _searchColumn;
        private string _searchString;
        private SearchOperation _searchOperation;

        // Methods
        public JQGridSearchEventArgs()
        { 
        }

        public JQGridSearchEventArgs(string searchColumn, string searchString, SearchOperation searchOperation) : this()
        {
            this._searchColumn = searchColumn;
            this._searchString = searchString;
            this._searchOperation = searchOperation;
        }

        // Properties
        public string SearchColumn
        {
            get
            {
                return this._searchColumn;
            }
            set
            {
                this._searchColumn = value;
            }
        }

        public string SearchString
        {
            get
            {
                return this._searchString;
            }
            set
            {
                this._searchString = value;
            }
        }

        public SearchOperation SearchOperation
        {
            get
            {
                return this._searchOperation;
            }
            set
            {
                this._searchOperation = value;
            }
        }

    }
}
