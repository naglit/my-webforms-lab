<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DropDownList.aspx.cs" Inherits="Lab_WebForms.Form.DropDownList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
		<asp:DropDownList ID="ddlSelectedValueTest" SelectedValue="<%# this.SelectedValue %>" runat="server"></asp:DropDownList>
        </div>
    </form>
</body>
</html>
