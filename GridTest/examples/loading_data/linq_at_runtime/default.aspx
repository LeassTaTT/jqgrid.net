<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GridTest.examples.loading_data.linq_at_runtime._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
        <div>
            Binding to Linq at runtime
            <br />
            <br />
        </div>
        <trirand:JQueryGrid runat="server" ID="LinqAtRuntimeGrid">
        </trirand:JQueryGrid>
        <br />
        <br />
        <trirand:codetabs runat="server" ID="LinqAtRuntimeTabs"></trirand:codetabs>
        
    </form>
</body>
</html>
