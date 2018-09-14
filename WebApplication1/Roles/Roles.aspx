<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="WebApplication2.Roles.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label ID="lblTitre" runat="server" Text="Rôles"></asp:Label></H1>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AuthConnectionString %>" SelectCommand="SELECT Id, Name FROM AspNetRoles ORDER BY Id" UpdateCommand="UPDATE AspNetRoles SET Name = @Name WHERE Id = @Id">
        <UpdateParameters>
            <asp:Parameter Name="Id" />
            <asp:Parameter Name="Name" />
        </UpdateParameters>
    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" EnableModelValidation="False" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound" OnRowUpdated="GridView1_RowUpdated" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:CommandField ShowEditButton="True" />
        <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id">
        <HeaderStyle CssClass="colId" BackColor="#99CCFF" />
        <ItemStyle CssClass="colId" />
        </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
        <HeaderStyle CssClass="colName" />
        <ItemStyle CssClass="colName" />
        </asp:BoundField>
        <asp:CommandField ShowSelectButton="True" />
    </Columns>
    <EmptyDataTemplate>
        .
    </EmptyDataTemplate>
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
    <SortedAscendingCellStyle BackColor="#FDF5AC" />
    <SortedAscendingHeaderStyle BackColor="#4D0000" />
    <SortedDescendingCellStyle BackColor="#FCF6C0" />
    <SortedDescendingHeaderStyle BackColor="#820000" />
</asp:GridView>
    <br />
    <asp:Label ID="lblErr" runat="server" ForeColor="Red" Text="_"></asp:Label>
    <br />
    <asp:Panel ID="pnlDelete" runat="server" Visible="False">
        <asp:Button ID="btnSupprimer" runat="server" OnClick="btnSupprimer_Click" Text="Supprimer" />
        &nbsp;<asp:Button ID="btnAnnulerSel" runat="server" OnClick="btnAnnulerSel_Click" Text="Annuler la sélection" />
    </asp:Panel>
    <br />
</asp:Content>
