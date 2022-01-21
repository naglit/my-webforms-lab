<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="Lab_WebForms.Form.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%= DateTime.Now.ToString() %>

            <% foreach (var i in Enumerable.Range(0, 10)) {%>
            <div style="font-size: <% Response.Write(i); %>">
                Hello World<br />
            </div>
            <% } %>
        </div>
    </form>
</body>
</html>
