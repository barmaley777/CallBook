<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CallBook.Default" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Caller: "></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Event: "></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Selected="True"></asp:ListItem>
                    <asp:ListItem>Pick-up</asp:ListItem>
                    <asp:ListItem>Dialling</asp:ListItem>
                    <asp:ListItem>Call Established</asp:ListItem>
                    <asp:ListItem>Call End</asp:ListItem>
                    <asp:ListItem>Hang-up</asp:ListItem>
                </asp:DropDownList>
            </p>
            <p>
                <asp:Label ID="Label3" runat="server" Text="Receiver: "></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            </p>
            <asp:Label ID="Label6" runat="server" Text="Clean Filters: " ToolTip="Clean selected filters."></asp:Label>
            <asp:Button ID="Button2" runat="server" Text="Clean" AccessKey="c" OnClick="Button2_Click" />
            <br />
            <br />
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" AllowCustomPaging="true" AllowSorting="true" AutoGenerateColumns="false" PageSize="10"
                OnPageIndexChanged="GridView1_PageIndexChanged"
                OnPageIndexChanging="GridView1_PageIndexChanging"
                OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                OnSorting="GridView1_Sorting"
                OnRowDataBound="GridView1_RowDataBound"
                OnRowCommand="GridView1_RowCommand" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" OnRowCreated="GridView1_RowCreated">
                <Columns>
                    <asp:TemplateField HeaderText="Caller" SortExpression="T_CALL.CALLER" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="Caller" runat="server" Target="_blank"
                                datanavigateurlfields="ID"
                                NavigateUrl='<%# "AllCalls2.aspx?Caller=" + Eval("T_CALL.CALLER") + "&recordID=" + Eval("T_CALL.RECORD_ID")%>'
                                ValidateRequestMode="Enabled" Enabled="true"
                                Text='<%# Eval("T_CALL.CALLER") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="T_EVENT_TYPE.EVENT_NAME" HeaderText="Event" SortExpression="T_EVENT_TYPE.EVENT_NAME" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="T_CALL.RECIEVER" HeaderText="Receiver" SortExpression="T_CALL.RECIEVER" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" ShowHeader="True" />
                    <asp:BoundField DataField="RECORD_DATE" HeaderText="Date" SortExpression="RECORD_DATE" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Call ID" Visible="false" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="CallID" runat="server"
                                Text='<%# Eval("Call_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <p>
                Elements per page:
        <br />
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem Value="5">5 elements</asp:ListItem>
                    <asp:ListItem Value="10" Selected="True">10 elements</asp:ListItem>
                    <asp:ListItem Value="25">25 elements</asp:ListItem>
                </asp:DropDownList>
            </p>
        </div>
        <div>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export" />
        </div>
    </form>
</body>
</html>
