<%@ Page Trace="true" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="VisibilitySwitching.aspx.cs" Inherits="Lab_WebForms.Form.VisibilitySwitching" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <% if (this.DisplayProductPrice == false){ %>
        <p>ログインしてください</p>
    <% } %>
    <p style="display: <%= (this.DisplayProductPrice) ? "block" : "none" %>">チェックボックスON</p>

    <div id="Literal1" Visible="<%# this.DisplayProductPrice %>" runat="server">span</div>
    <asp:TextBox runat="server" Text="<%= DateTime.Now.ToString() %>" />

    <asp:Literal id="lCheckBoxStatus" visible="<%# this.DisplayProductPrice %>" Text="チェックボックスONでよ" runat="server"/>

    <asp:CheckBox ID="cbDisplayPrice" runat="server" OnCheckedChanged="cbDisplayPrice_CheckedChanged" AutoPostBack="true"/>
	<asp:Repeater ID="rProduct" ItemType="Model.Product" runat="server">

        <ItemTemplate>
            <div id="product" visible="<%# Item.IsAvailable %>" runat="server">
                <p><%#: Item.ProductName %></p>
                <asp:Literal id="lProductPrice" visible="<%# this.DisplayProductPrice %>" Text="<%#: Item.Price %>" runat="server"/>
                <p style="display: <%# (Item.Price > 2000) ? "block" : "none" %>">
                    送料無料
                </p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <tr ID="trCredential" Visible="<%# this.DisplayProductPrice %>" runat="server">
	<td>
		トップシークレット情報
        <%#: this.Name %>
	</td>
</tr>
</asp:Content>
