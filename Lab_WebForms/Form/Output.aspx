<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Output.aspx.cs" Inherits="Lab_WebForms.Form.Output" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>a value in a property is wiped out at postback</p>
    " <%= this.Message %> "
    <p>while a value in a web control is not</p>
    " <asp:Literal ID="lTest" runat="server" /> "
    <asp:LinkButton ID="test" OnClick="test_Click" Text="testttt" runat="server" />
    
</asp:Content>
