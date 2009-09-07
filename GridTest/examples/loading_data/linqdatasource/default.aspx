<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.loading_data.linqdatasource._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    Binding to SqlDataSource
    <br /><br />
    
    </div>
    
        
    <trirand:JQueryGrid runat="server" ID="LinqDataSourceGrid" 
        DataSourceID="LinqDataSource1" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    ds
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID"  />
            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" 
                SortExpression="Email" />
            <asp:BoundField DataField="Password" HeaderText="Password" ReadOnly="True" 
                SortExpression="Password" />
            <asp:BoundField DataField="Role" HeaderText="Role" ReadOnly="True" 
                SortExpression="Role" />
            <asp:BoundField DataField="RegistrationDate" HeaderText="RegistrationDate" 
                ReadOnly="True" SortExpression="RegistrationDate" />
        </Columns>
    </trirand:JQueryGrid>
    
    <br /><br />
    <trirand:codetabs runat="server" id="codetabs1"></trirand:codetabs> 
 
    
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="GridTest.dbml.UserDataClassesDataContext" 
        Select="new (ID, Email, Password, Role, RegistrationDate)" TableName="Users">
    </asp:LinqDataSource>
 
    
    </form>
</body>
</html>
