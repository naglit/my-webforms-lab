<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab_WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<p>a</p>
	<asp:LinkButton runat="server" ID="aaaaa" OnClientClick="test_jquery('aaa');">aaaa</asp:LinkButton>
	<script>
		function test_jquery() {
			var empId = "aaaa"
			var dataaa = JSON.stringify({ employeeid: empId });
			debugger;
			$.ajax({
				type: "POST",
				url: "Default.aspx/TestJquery",
				data: '{"employeeid": "aaaa" }',
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				success: function (data) {
					console.log("successfully done");
				},
				error: function () { alert('error'); }
			});
		}

	</script>
	<asp:DropDownList id="ColorList"
		runat="server">

		<asp:ListItem Value="White"> White </asp:ListItem>
		<asp:ListItem Value="Silver"> Silver </asp:ListItem>
		<asp:ListItem Value="DarkGray"> Dark Gray </asp:ListItem>
		<asp:ListItem Value="Khaki"> Khaki </asp:ListItem>
		<asp:ListItem Value="DarkKhaki"> Dark Khaki </asp:ListItem>
	</asp:DropDownList>
</asp:Content>