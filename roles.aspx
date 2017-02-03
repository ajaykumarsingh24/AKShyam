<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/SavvyGreatAdmin.Master" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="SavvyGreat.Admin.roles" %>

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

    <script type="text/javascript">
        function uploadStarted() {
            $get("imgDisplay").style.display = "none";
        }

        function uploadComplete(sender, args) {
            var imgDisplay = $get("imgDisplay");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function() {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            img.src = "<%= ResolveUrl(UploadFolderPath) %>" + args.get_fileName();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <div class="bar progress-bar-info">
                <h3>Roles Details</h3>
            </div>
        </div>
    </div>
    <div>
        <table align="center" style="position: relative; top: 20px;">
            <tr>
                <td>
                    <table align="center">
                        <tr>
                            <td>
                                Role Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Company Name :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCompany" DataMember="CompanyId" DataValueField="CompanyId" DataTextField="CompanyName" runat="server" AppendDataBoundItems="true" Width="255px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Description :
                            </td>
                            <td>
                                <asp:TextBox ID="txtdesc" runat="server" MaxLength="200" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Is Active :
                            </td>
                            <td>
                                <asp:CheckBox ID="ChkActive" runat="server" Checked="true"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Logo:
                            </td>
                            <td>
                                <cc1:AsyncFileUpload OnClientUploadComplete=" uploadComplete " runat="server" ID="AsyncFileUpload1"
                                                     Width="200px" UploaderStyle="Traditional" CompleteBackColor="White" UploadingBackColor="#CCFFFF"
                                                     ThrobberID="imgLoader" OnUploadedComplete="FileUploadComplete" OnClientUploadStarted=" uploadStarted "/>
                                <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/loader.gif" Width="80px"/>

                                <img id="imgDisplay" alt="" src="" style="display: none"/>

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
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Logo">
                                <ItemTemplate>
                                    <asp:Image ID="imgLogo" runat="server" Height="100px" Width="100px" AlternateText='<%#Bind("Logo") %>' ImageUrl='<%#  @"..\Uploads\" + Eval("Logo") %>'></asp:Image>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"/>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick=" return confirm('Are you sure? want to delete the department.'); "
                                                OnClick="btnDelete_Click"/>
                                    <asp:Label ID="lblCompanyId" runat="server" Text='<%#Eval("CompanyId") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblRoleID" runat="server" Text='<%#Eval("RoleID") %>' Visible="false"></asp:Label>
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