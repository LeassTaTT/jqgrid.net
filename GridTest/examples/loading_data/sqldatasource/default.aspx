<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.loading_data.sqldatasource._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>    
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script src="/js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <script src="/js/jquery.layout.js" type="text/javascript"></script>
    <script src="/js/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="/js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="/js/jquery.tablednd.js" type="text/javascript"></script>
    <script src="/js/jquery.contextmenu.js" type="text/javascript"></script>  
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    Binding to SqlDataSource
<%--    <trirand:JQueryGrid ID="JQGrid1" runat="server" DataSourceID="SqlDataSource1">        
    </trirand:JQueryGrid>--%>
   
    <br /><br />
    <%--<trirand:codetabs runat="server" id="codetabs1"></trirand:codetabs>    --%>
    
    </div>
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" 
        ConnectionString="<%$ ConnectionStrings:SQL2008_449777_fhsConnectionString %>" 
        SelectCommand="SELECT [ID], [Email], [Password], [Role], [RegistrationDate] FROM [User]"></asp:SqlDataSource>
        
        
    <trirand:JQueryGrid runat="server" ID="JQGrid1" DataSourceID="SqlDataSource1"></trirand:JQueryGrid>
    
    <asp:GridView runat="server" ID="GridView1" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Password" HeaderText="Password" 
                SortExpression="Password" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
            <asp:BoundField DataField="RegistrationDate" HeaderText="RegistrationDate" 
                SortExpression="RegistrationDate" />
        </Columns>
    </asp:GridView>
    
    </form>
</body>
</html>
