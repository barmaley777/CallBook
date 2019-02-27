<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllCalls2.aspx.cs" Inherits="CallBook.AllCalls2" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
        <p>
            <asp:GridView ID="GridView1" runat="server" AllowSorting="false" AllowCustomPaging="true"
                AllowPaging="true" PageSize="10" EmptyDataText="No data available. "
                OnSorting="GridView1_Sorting"
                OnPageIndexChanging="GridView1_PageIndexChanging">
            </asp:GridView>
        </p>
    </form>
</body>
</html>
