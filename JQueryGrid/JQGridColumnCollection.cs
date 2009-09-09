using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Security.Permissions;

namespace Trirand.Web.UI.WebControls
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class JQGridColumnCollection : BaseItemCollection<JQGrid, JQGridColumn>
    {
        protected override object CreateKnownType(int index)
        {
            return new JQGridColumn();
        }

    }

}
