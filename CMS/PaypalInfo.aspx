<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaypalInfo.aspx.cs" Inherits="PaypalInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button runat="server" Text="PayPal REST implementation" id="btnRest" OnClick="btnRest_Click" />
        <asp:Button runat="server" Text="PayPal SOAP implementation" id="btnSOAP" OnClick="btnSOAP_Click"/>
        <asp:Button runat="server" Text="Authorize.Net implementation" id="btnAutho" OnClick="btnAutho_Click" />
        <asp:Button runat="server" Text="Orbital implementation" id="btnOrbital" OnClick="btnOrbital_Click" />
        <asp:Label runat="server" ID="lblMsg" ></asp:Label>
    </div>
    </form>
</body>
</html>
