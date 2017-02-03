<%@ Page Language="C#" MasterPageFile="~/Admin/SavvyGreatAdmin.Master" AutoEventWireup="true"
CodeBehind="News.aspx.cs" Inherits="SavvyGreat.Admin.News" Title="News" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdNews" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload"/>
    </Triggers>
    <ContentTemplate>
        <div class="panel panel-default">
            <div class="panel-heading">
                <strong>
                    News Update
                </strong>
            </div>
            <div class="panel-body">

                <div class="row">
                    <div class="col-md-2">
                        Company Name
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlCompany" DataTextField="CompanyName" DataValueField="CompanyID">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Create Date
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtNewsDate" MaxLength="20" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtNewsDate"
                                              Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-2">
                        Title
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtTitle" MaxLength="200" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:Label ID="lblSrNo" runat="server" Visible="false"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        News Category
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row margintop10">
                    <div class="col-md-2">
                        Sub Category
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Expire Date
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtexpDate" MaxLength="20" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtexpDate"
                                              Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </div>
                </div>
                <div class="row margintop10">
                    <div class="col-md-2">
                        Country
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control input-sm" AppendDataBoundItems="true">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        Is Breaking New
                    </div>
                    <div class="col-md-3">
                        <asp:CheckBox runat="server" ID="chlisbreakingnew"/>
                    </div>
                </div>

                <div class="row margintop10">
                    <div class="col-md-2">
                        News Description
                    </div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtNewsDescription" runat="server" CssClass="form-control input-sm"
                                     Height="50px" TextMode="MultiLine">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="row margintop10">
                    <div class="col-md-2">
                        Image
                    </div>
                    <div class="col-md-2">
                        <asp:FileUpload ID="FileUpload1" runat="server"/>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnUpload" CssClass="btn btn-default btn-sm" runat="server" Text="Upload" OnClick="btnUpload_Click"/>
                    </div>
                    <div class="col-md-4">
                        <asp:ImageMap ID="ImageMap1" runat="server" Height="80px" Width="80px">
                        </asp:ImageMap>
                    </div>
                </div>
                <hr/>
                <div>
                    <br/>
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label>
                </div>
                <div class="row margintop10">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-sm" runat="server" Text="Submit"
                                    OnClick="btnSubmit_Click"/>
                    </div>
                </div>
                <hr/>
                <div class="row margintop10">
                    <div class="col-md-12">
                        <asp:GridView ID="GrdNewsUpdate" runat="server" AutoGenerateColumns="False"
                                      CellPadding="3" CssClass="table table-bordered table-responsive table-hover">
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountryId" runat="server" Text='<%# Bind("CountryId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unique Sr No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUniqueSrNo" runat="server" Text='<%# Bind("UniqueSrNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImageTitle" runat="server" Text='<%# Bind("ImageTitle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="News Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsCatName" runat="server" Text='<%# Bind("NewsCatName") %>'></asp:Label>
                                        <asp:Label ID="lblNewsCatCode" runat="server" Visible="false" Text='<%# Bind("NewsCatCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="News Sub Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsSubCatName" runat="server" Text='<%# Bind("NewsSubCatName") %>'></asp:Label>
                                        <asp:Label ID="lblNewsSubCatCode" runat="server" Visible="false" Text='<%# Bind("NewsSubCatCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="News Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNewsDesc" runat="server" Text='<%# Bind("NewsDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:ImageButton AlternateText="." ID="ImageButton1" ImageUrl='<%# "NewsUpdate.ashx?val=" + Eval("UniqueSrNo") %>'
                                                         runat="server" ImageAlign="AbsMiddle" Height="85px" Width="85px"/>
                                        <asp:Label ID="lblcompanyId" Visible="false" runat="server" Text='<%# Bind("companyId") %>'></asp:Label>
                                        <asp:Label ID="lblUniqueSrNo1" Visible="false" runat="server" Text='<%# Bind("UniqueSrNo") %>'></asp:Label>
                                        <asp:Label ID="lblbrakingnew" Visible="false" runat="server" Text='<%# Bind("isBreakingNew") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" OnClientClick=" return confirm('Are you sure want to edit this news') "
                                                        OnClick="lnkEdit_Click">
                                            Edit
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick=" return confirm('Are you sure want to delete this news') "
                                                        OnClick="lnkDelete_Click">
                                            Delete
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="rowStyle"/>
                            <HeaderStyle CssClass="headerStyle"/>
                            <FooterStyle CssClass="footerStyle"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<div class="row">
    <div class="col-md-12">
            <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
            <script src="../../Content/Shared/Plugins/bootstrap-3.1.3/dist/js/jasny-bootstrap.min.js"
                    type="text/javascript"></script>
            <script src="../../Content/Shared/Theme/Panel/js/plugins/datatables/jquery.dataTables.js"
                    type="text/javascript"></script>
            <script src="../../Content/Shared/Theme/Panel/js/plugins/datatables/dataTables.bootstrap.js"
                    type="text/javascript"></script>
            <script src="../../Content/Shared/Scripts/JQuery/GridviewFix.js" type="text/javascript"></script>
            <script type="text/javascript">
                $('#ContentPlaceHolder1_GrdNewsUpdate').GridviewFix().dataTable();
                //});
            </script>

        <%--<script src="../../plugins/jQuery/jQuery-2.1.4.min.js"></script>
            <!-- Bootstrap 3.3.5 -->
            <script src="../../bootstrap/js/bootstrap.min.js"></script>
            <!-- FastClick -->
            <script src="../../plugins/fastclick/fastclick.min.js"></script>
            <!-- Savvygreat App -->
            <script src="../../dist/js/app.min.js"></script>
            <!-- Savvygreat for demo purposes -->
            <script src="../../dist/js/demo.js"></script>--%>
    </div>
</div>
</asp:Content>