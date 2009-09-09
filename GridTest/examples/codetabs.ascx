<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="codetabs.ascx.cs" Inherits="GridTest.examples.codetabs" %>

<div id="tabs" runat="server">
    <ul>
        <li><a href="#<%= DescriptionContent.ClientID %>"><span>Description</span></a></li>
        <li><a href="#<%= ASPXContent.ClientID %>"><span>ASPX</span></a></li>
        <li><a href="#<%= CSharpContent.ClientID %>"><span>C#</span></a></li>
    </ul>
    <div id="DescriptionContent" runat="server">
        <pre><code><span runat="server" ID="DescriptionCode"></span></code></pre>
    </div>
    <div id="ASPXContent" runat="server">
        <pre><code><span runat="server" ID="ASPXCode"></span></code></pre>
    </div>
    <div id="CSharpContent" runat="server">
        <pre><code><span runat="server" ID="CSharpCode"></span></code></pre>
    </div>
</div>

<script type="text/javascript">

    $("#<%= tabs.ClientID %>").tabs();

</script>



