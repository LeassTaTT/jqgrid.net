<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.sorting.disable._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    Disabling sorting
    <br /><br />    
    
    <asp:SqlDataSource runat="server" ID="SqlDataSource1" 
        ConnectionString="<%$ ConnectionStrings:SQL2008_449777_fhsConnectionString %>" 
        SelectCommand="SELECT [ID], [Email], [Password], [Role], [RegistrationDate] FROM [User]"></asp:SqlDataSource>
        
        
    <trirand:JQGrid runat="server" ID="SqlDataSourceGrid" DataSourceID="SqlDataSource1">
        <Columns>
            <trirand:JQGridColumn Sortable="false" DataField="ID" />
            <trirand:JQGridColumn Sortable="false" DataField="Email"  />
            <trirand:JQGridColumn Sortable="false" DataField="Password"  />
            <trirand:JQGridColumn Sortable="false" DataField="Role"  />
            <trirand:JQGridColumn Sortable="false" DataField="RegistrationData"  />
        </Columns>
        
    </trirand:JQGrid>    

    <br /><br />
    <trirand:codetabs runat="server" id="SqlDataSourceTabs"></trirand:codetabs> 
    
    </div>
    </form>
</body>
</html>
