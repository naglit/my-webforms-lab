<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ClickFromCodeBehind.aspx.cs" Inherits="Lab_WebForms.Form.ClickFromCodeBehind" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Literal ID="lUser" runat="server" />
    <asp:LinkButton ID="lbRunClientScript" OnClick="lbRunClientScript_Click" Text="Run a Client Script" runat="server"/>
    <asp:LinkButton ID="lbClearOutput" OnClick="lbClearOutput_Click" runat="server" />
    <script>
        function processCompletionNotification() {
            var userName = "John";
            var message = `The process has been successfuly done. Are you sure you'll finish serving ${userName}?`;
            var result = window.confirm(message);
            if (result) {
                var a = $("#<%= lbClearOutput.ClientID %>");
                $("#<%= lbClearOutput.ClientID %>")[0].click();
            }
        }
    </script>
    
</asp:Content>
