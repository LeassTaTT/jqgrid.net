using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trirand.Web.UI.WebControls
{
    internal class JsonResponse
    {
        public int page { get; set; }
        public int total { get; set; }
        public int records { get; set; }
        public JsonRow[] rows { get; set; }
    }
}
