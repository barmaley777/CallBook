<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentCall.aspx.cs" Inherits="CallBook.CurrentCall" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="T_EVENT_TYPE.EVENT_NAME" HeaderText="EVENT">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="T_CALL.RECIEVER" HeaderText="RECIEVER">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RECORD_DATE" HeaderText="DATE" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
