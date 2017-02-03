<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SavvyGreatAdmin.Master" AutoEventWireup="true" CodeBehind="UserRoles.aspx.cs" Inherits="SavvyGreat.Admin.UserRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <head>
        <style type="text/css">
            .gv {
                font-family: Arial;
                margin-top: 30px;
                font-size: 14px;
            }

            .gv th {
                background-color: #5D7B9D;
                font-weight: bold;
                color: #fff;
                padding: 2px 10px;
            }

            .gv td { padding: 2px 10px; }

            input[type="submit"] {
                margin: 2px 10px;
                padding: 2px 20px;
                background-color: #5D7B9D;
                border-radius: 10px;
                border: solid 1px #000;
                cursor: pointer;
                color: #fff;
            }

            input[type="submit"]:hover { background-color: orange; }
        </style>
    </head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table align="center" style="position: relative; top: 20px;">
        <tr>
        <td>
        <table align="center">
            <tr>
                <td>
                    Company Name :
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompany" DataMember="CompanyId" DataValueField="CompanyId" DataTextField="CompanyName" runat="server" AppendDataBoundItems="true" Width="257px">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    Roles :
                </td>
                <td>
                    <asp:CheckBoxList ID="chklstRoles" DataMember="RoleId" DataValueField="RoleId" DataTextField="RoleName" runat="server" AppendDataBoundItems="true" Width="255px">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                Visible="false"/>
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"/>
                </td>
            </tr>

        </table>


        <tr>
            <td align="center">
                <br/>
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                              EmptyDataText="No Records Found" GridLines="both" CssClass="gv" EmptyDataRowStyle-ForeColor="Red">
                    <Columns>
                        <asp:TemplateField HeaderText="User Email">
                            <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("your_Email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name">
                            <ItemTemplate>
                                <asp:Label ID="lblfname" runat="server" Text='<%#Eval("First_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Name">
                            <ItemTemplate>
                                <asp:Label ID="lbllname" runat="server" Text='<%#Eval("Last_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
                                <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure? want to delete the user's role.');"
                                                OnClick="btnDelete_Click" />--%>
                                <asp:Label ID="lblProfileId" runat="server" Text='<%#Eval("ProfileId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <input type="hidden" runat="server" id="hidRoleId"/>
        <input type="hidden" runat="server" id="hidProfileId"/>
    </div>
</asp:Content>