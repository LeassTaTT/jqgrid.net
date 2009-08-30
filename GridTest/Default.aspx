<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GridTest._Default" %>

<%@ Register assembly="JQueryGrid" namespace="Trirand.Web.UI.WebControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>jqGrid Demos</title>  
    <link rel="stylesheet" type="text/css" media="screen" href="themes/redmond/jquery-ui-1.7.1.custom.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="themes/ui.jqgrid.css" />
    <style>
        
    html, body {
    	margin: 0;			/* Remove body margin/padding */
    	padding: 0;
    	overflow: hidden;	/* Remove scroll bars on browser window */	
        font-size: 75%;
    }

    .ui-tabs-nav li {position: relative;}
    .ui-tabs-selected a span {padding-right: 10px;}
    .ui-tabs-close {display: none;position: absolute;top: 3px;right: 0px;z-index: 800;width: 16px;height: 14px;font-size: 10px; font-style: normal;cursor: pointer;}
    .ui-tabs-selected .ui-tabs-close {display: block;}
    .ui-layout-west .ui-jqgrid tr.jqgrow td { border-bottom: 0px none;}
    .ui-datepicker {z-index:1200;}
    </style>

    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.layout.js" type="text/javascript"></script>
    <script src="js/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/jquery.tablednd.js" type="text/javascript"></script>
    <script src="js/jquery.contextmenu.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table id="list2" class="scroll" cellpadding="0" cellspacing="0"></table>
    <div id="pager2" class="scroll" style="text-align:center;"></div>


  <%-- <script type="text/javascript">

        $(document).ready(function() {
            jQuery("#list2").jqGrid({
                url: 'default.aspx',
                datatype: "json",
                colNames: ['Inv No', 'Date', 'Client', 'Amount', 'Tax', 'Total', 'Notes'],
                colModel: [
   		            { name: 'id', index: 'id', width: 55 },
   		            { name: 'invdate', index: 'invdate', width: 90 },
   		            { name: 'name', index: 'name asc, invdate', width: 100 },
   		            { name: 'amount', index: 'amount', width: 80, align: "right" },
   		            { name: 'tax', index: 'tax', width: 80, align: "right" },
   		            { name: 'total', index: 'total', width: 80, align: "right" },
   		            { name: 'note', index: 'note', width: 150, sortable: false }
   	            ],
                rowNum: 10,
                rowList: [10, 20, 30],
                imgpath: '',
                pager: jQuery('#pager2'),
                sortname: 'id',
                viewrecords: true,
                sortorder: "desc",
                caption: "JSON Example"
            }).navGrid('#pager2', { edit: false, add: false, del: false });
        });
    
    </script>--%>
    
    
    
    
    </div>
    <cc1:JQueryGrid ID="JQGrid1" runat="server" onsorting="JQGrid1_Sorting"  >        
    </cc1:JQueryGrid>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ></asp:SqlDataSource>
    
    </form>
</body>
</html>
