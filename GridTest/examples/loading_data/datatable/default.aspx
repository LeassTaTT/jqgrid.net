<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.loading_data.datatable._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title> 
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
       Binding to DataTable
       <trirand:JQGrid ID="DataTableGrid" runat="server" >        
       </trirand:JQGrid>
       
       <br /><br />
       <trirand:codetabs runat="server" id="DataTableCodeTabs"></trirand:codetabs>    
    </div>
    </form>
</body>
</html>
