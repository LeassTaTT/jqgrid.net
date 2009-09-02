<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="codetabs.ascx.cs" Inherits="GridTest.examples.codetabs" %>

<style type="text/css">

#codeTabs 
{
	border-bottom : 1px solid #ccc;
	margin : 0;
	padding-bottom : 19px;
	padding-left : 10px;
}

#codeTabs ul, #codeTabs li	
{
	display : inline;
	list-style-type : none;
	margin : 0;
	padding : 0;
}

	
#codeTabs a:link, #codeTabs a:visited	
{
	background : #E8EBF0;
	border : 1px solid #ccc;
	color : #666;
	float : left;
	font-size : small;
	font-weight : normal;
	line-height : 14px;
	margin-right : 8px;
	padding : 2px 10px 2px 10px;
	text-decoration : none;
}

#codeTabs a:link.active, #codeTabs a:visited.active	
{
	background : #fff;
	border-bottom : 1px solid #fff;
	color : #000;
}

#codeTabs a:hover	
{
	color : #f00;
}

#codeContents 
{
	background : #fff;
	border : 1px solid #ccc;
	border-top : none;	
	margin : 0px;
	padding : 15px;	
	height: 300px; 
	width: 800px; 
	font-family: Courier New; 
	font-size: 8pt; 
	overflow: auto;
	white-space:inherit;
}


</style>

<script type="text/javascript">
                    
        function SwitchTabs(tabName)
        {
            SetTabsToDefault(tabName);
            var tabElement = document.getElementById(tabName + "Tab");
            
            tabElement.style.background = "#fff";
            tabElement.style.borderBottom = "1px solid #fff";
            tabElement.style.color = "#000";
            
            var index = 0;
            switch (tabName)
            {
                case "info"   : index = 0; break;
                case "aspx"   : index = 1; break;
                case "csharp" : index = 2; break;
                case "vbnet"  : index = 3; break;
                
            }
            document.getElementById("codeContents").innerHTML = document.getElementById(contentID[index]).innerHTML;
        }
        
        function SetTabsToDefault(currentTabName)
        {
            var links = document.getElementById("codeTabs").getElementsByTagName("A");
            for (var i=0; i<links.length; i++)
            {
                var link = links[i];
                if (! (link.id.indexOf(currentTabName) > -1))
                {
                    link.style.background = "#E8EBF0";
                    link.style.border = "1px solid #ccc";
                    link.style.color = "#666";    
                }
            }
        }
                    
</script>

<ul id="codeTabs">
    <li onclick="SwitchTabs('info')"><a href="#" id="infoTab">Demo Info</a> </li>
    <li onclick="SwitchTabs('aspx')"><a href="#" id="aspxTab">ASPX</a> </li>
    <li onclick="SwitchTabs('csharp')"><a href="#" id="csharpTab">C#</a></li>
    <li onclick="SwitchTabs('vbnet')"><a href="#" id="vbnetTab">VB.NET</a></li>
</ul>



<div id="codeContents" style="width: 970px; height: 200px;"></div>
<div id="infoContents" style="display: none" runat="server"></div>
<div id="aspxContents" style="display: none" runat="server"></div>
<div id="csharpContents" style="display: none" runat="server"></div>
<div id="vbnetContents" style="display: none" runat="server"></div>

<script type="text/javascript">                  
    SwitchTabs('info');                        
</script>


