<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="CMSModules_CMS_EmergeCommon_PrintPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>

    <style>
        #printarea {
            padding : 15px 15px 15px 15px;
        }
    </style>


    <script language="javascript" type="text/javascript">
        function PrintWindow() {

            document.getElementById('printarea').innerHTML = window.opener.document.getElementById('divPrint').innerHTML;
            window.print();
            // window.close();
        }
    </script>
</head>

<body onload=" PrintWindow();">


    <div id="printarea"></div>

</body>
</html>
