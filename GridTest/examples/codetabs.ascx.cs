using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace GridTest.examples
{
    public partial class codetabs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentFilePath = Page.Request.CurrentExecutionFilePath;
            string currentFolderPath = currentFilePath.Substring(0, currentFilePath.LastIndexOf("/"));

            string infoPath = Server.MapPath(currentFolderPath + "/" + "info.txt");
            string aspxPath = Server.MapPath(currentFolderPath + "/" + "default.aspx");
            string csharpPath = Server.MapPath(currentFolderPath + "/" + "default.aspx.cs");
            string vbnetPath = Server.MapPath(currentFolderPath + "/" + "DefaultVBNET.aspx.vb");

            DescriptionCode.InnerHtml = ReplaceCodeForHtml(ReadFileContents(infoPath));
            ASPXCode.InnerHtml = EncodeCodeForHtml(ReadFileContents(aspxPath));
            CSharpCode.InnerHtml = EncodeCodeForHtml(ReadFileContents(csharpPath));

            if (File.Exists(vbnetPath))
            {
                //csharpContents.InnerHtml = EncodeCodeForHtml(ReadFileContents(csharpPath));
                //vbnetContents.InnerHtml = EncodeCodeForHtml(ReadFileContents(vbnetPath));
            }
            else
            {
                //csharpContents.InnerHtml = "There is no C# code-behind for this example. Everything in this example is achieved declaratively. Please, review the ASPX and Info tabs for more details.";
                //vbnetContents.InnerHtml = "There is no VB.NET code-behind for this example. Everything in this example is achieved declaratively. Please, review the ASPX and Info tabs for more details.";
            }

            //string codeArrayScript = @"
                                    //<script type='text/javascript'>                                    
                                    //var contentID = ['{0}','{1}','{2}','{3}'];
                                    //</script>
                                    //";
            //codeArrayScript = String.Format(codeArrayScript, infoContents.ClientID, aspxContents.ClientID, csharpContents.ClientID, vbnetContents.ClientID);
            //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), this.ClientID + "codeArray", codeArrayScript);
        }

        protected string ReadFileContents(string filePath)
        {
            StreamReader sr = File.OpenText(filePath);
            string fileText = sr.ReadToEnd();
            sr.Close();

            return fileText;
        }

        protected string EncodeCodeForHtml(string code)
        {
            string encodedCode = Server.HtmlEncode(code);
            encodedCode = encodedCode.Replace("\r\n", "<br/>");
            encodedCode = encodedCode.Replace(" ", "&nbsp;");

            return encodedCode;
        }

        protected string ReplaceCodeForHtml(string code)
        {
            code = code.Replace("\r\n", "<br/>");

            return code;
        }
    }
}