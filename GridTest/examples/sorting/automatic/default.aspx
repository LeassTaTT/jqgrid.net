<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.sorting.automatic._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    Automatically sorting the grid:
    <br /><br />    
    
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" 
        ConnectionString="<%$ ConnectionStrings:SQL2008_449777_fhsConnectionString %>" 
        SelectCommand="SELECT [ID], [Email], [Password], [Role], [RegistrationDate] FROM [User]"></asp:SqlDataSource>
        
        
    <trirand:JQueryGrid runat="server" ID="SqlDataSourceGrid" DataSourceID="SqlDataSource1"></trirand:JQueryGrid>
    
    <br /><br />
    <trirand:codetabs runat="server" id="SqlDataSourceTabs"></trirand:codetabs> 
    
    </div>
    </form>
</body>
</html>
