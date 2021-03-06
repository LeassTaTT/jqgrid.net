﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.loading_data.objectdatasource._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    Binding to ObjectDataSource
    <trirand:JQGrid ID="ObjectDataSourceGrid" runat="server" DataSourceID="ObjectDataSource1">        
    </trirand:JQGrid>
   
    <br /><br />
    <trirand:codetabs runat="server" id="ObjectDataSourceTabs"></trirand:codetabs>    
    
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName ="DataAccess" CacheDuration="300" CacheExpirationPolicy="Sliding">
        <SelectParameters>
            <asp:Parameter Name="p_sortExpression" Type="string" Direction="input" />
            <asp:Parameter Name="p_sortDirection" Type="string" Direction="input" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    </form>
</body>
</html>
