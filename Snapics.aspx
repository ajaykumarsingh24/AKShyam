<%@ Page Language="C#" MasterPageFile="~/Admin/SavvyGreatAdmin.Master" AutoEventWireup="true"
    CodeBehind="Snapics.aspx.cs" Inherits="SavvyGreat.Admin.Snapics" Title="Snapics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdSnapics" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload1" />
            <asp:PostBackTrigger ControlID="btnUpload2" />
            <asp:PostBackTrigger ControlID="btnUpload3" />
            <asp:PostBackTrigger ControlID="btnUpload4" />
        </Triggers>
        <ContentTemplate>
            <div align="center">
                <br />
                <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label>
            </div>
            <div>
                <div>
                    <b>Company Name</b>
                </div>
                <asp:DropDownList ID="ddlCompany" DataMember="CompanyId" DataValueField="CompanyId" DataTextField="CompanyName" runat="server" AppendDataBoundItems="true" Width="255px">
                </asp:DropDownList>
                <div>
                    <div>
                        <b>Upload Images</b>
                    </div>
                </div>
                <div>
                    <div>
                        <asp:LinkButton ID="LinkButton1" Font-Underline="false" Font-Bold="true" runat="server" PostBackUrl="~/UserLogin.aspx">Go To Login Page</asp:LinkButton>
                    </div>
                </div>
                <div>
                    <div>
                        <hr />
                    </div>
                </div>
                <div>
                    <div>
                        <asp:ImageMap ID="ImageMap1" runat="server" Height="150px" Width="150px" ImageUrl="~/Images/BlankImage.jpg">
                        </asp:ImageMap>
                        <br />
                        <asp:TextBox ID="txtImage1" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="btnUpload1_Click" />
                    </div>
                    <div>
                        align="center">
                        <asp:ImageMap ID="ImageMap2" runat="server" Height="150px" Width="150px" ImageUrl="~/Images/BlankImage.jpg">
                        </asp:ImageMap>
                        <br />
                        <asp:TextBox ID="txtImage2" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="FileUpload2" runat="server" />
                        <asp:Button ID="btnUpload2" runat="server" Text="Upload" OnClick="btnUpload2_Click" />
                    </div>
                </div>
                <div>
                    <div>
                        <hr />
                    </div>
                </div>
                <div>
                    <div>
                        <asp:ImageMap ID="ImageMap3" runat="server" Height="150px" Width="150px" ImageUrl="~/Images/BlankImage.jpg">
                        </asp:ImageMap>
                        <br />
                        <asp:TextBox ID="txtImage3" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="FileUpload3" runat="server" />
                        <asp:Button ID="btnUpload3" runat="server" Text="Upload" OnClick="btnUpload3_Click" />
                    </div>
                    <div>
                        <asp:ImageMap ID="ImageMap4" runat="server" Height="150px" Width="150px" ImageUrl="~/Images/BlankImage.jpg">
                        </asp:ImageMap>
                        <br />
                        <asp:TextBox ID="txtImage4" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="FileUpload4" runat="server" />
                        <asp:Button ID="btnUpload4" runat="server" Text="Upload" OnClick="btnUpload4_Click" />
                    </div>
                </div>
                <div>
                    <div>
                        <hr />
                    </div>
                </div>
                <div>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" Width="300px" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
