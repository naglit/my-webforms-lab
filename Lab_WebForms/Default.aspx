<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab_WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p>a value in a property is wiped out at postback</p>
    " <%= this.Message %> "
    <p>while a value in a web control is not</p>
    " <asp:Literal ID="lTest" runat="server" /> "
    <asp:LinkButton ID="test" OnClick="test_Click" Text="testttt" runat="server" />
    
</asp:Content>
