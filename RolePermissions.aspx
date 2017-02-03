<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SavvyGreatAdmin.Master" AutoEventWireup="true" CodeBehind="RolePermissions.aspx.cs" Inherits="SavvyGreat.Admin.RolePermissions" %>

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

    <%--    <script type="text/javascript">
        function uploadStarted() {
            $get("imgDisplay").style.display = "none";
        }
        function uploadComplete(sender, args) {
            var imgDisplay = $get("imgDisplay");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            img.src = "<%=ResolveUrl(UploadFolderPath) %>" + args.get_fileName();
        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table align="center" style="position: relative; top: 20px;">
            <tr>
                <td>
                    <table align="center">
                        <%-- <tr>
                            <td>Role Name :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRoleName" DataMember="roleId" DataTextField="RoleName" runat="server" AppendDataBoundItems="true" Width="255px">
                                </asp:DropDownList>

                            </td>
                        </tr>--%>

                        <tr>
                            <td>
                                Company :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" DataMember="CompanyId" DataValueField="CompanyId" DataTextField="CompanyName" runat="server" AppendDataBoundItems="false" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Permissions :
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkstPermissions" DataMember="PermissionId" DataValueField="PermissionId" DataTextField="PermissionName" runat="server" AppendDataBoundItems="false" Width="255px" CellPadding="1" CellSpacing="1">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />--%>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                                            Visible="false"/>
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br/>
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                  EmptyDataText="No Records Found" GridLines="both" CssClass="gv" EmptyDataRowStyle-ForeColor="Red">
                        <Columns>
                            <asp:TemplateField HeaderText="Role Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblRoleName" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Role Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick=" return confirm('Are you sure? want to delete the this roles's permission(s)'); "
                                                OnClick="btnDelete_Click"/>
                                    <asp:Label ID="lblCompanyId" runat="server" Text='<%# Eval("CompanyId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("RoleID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <input type="hidden" runat="server" id="hidRoleID"/>
    </div>
</asp:Content>