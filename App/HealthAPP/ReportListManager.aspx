<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportListManager.aspx.cs" Inherits="HealthAPP.ReportListManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:RadioButtonList ID="rb_status" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" 
        CellSpacing="50" CellPadding="20">
        <asp:ListItem Selected="True" Text="مفتوح" Value="1" > </asp:ListItem>
        <asp:ListItem Text="ارسال للمدير" Value="2"> </asp:ListItem>
        <asp:ListItem Text="ارسال للرئيس التنفيذي" Value="3"> </asp:ListItem>
        <asp:ListItem Text="مغلق" Value="4"> </asp:ListItem>
    </asp:RadioButtonList>
    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" SettingsCommandButton-EditButton-Text="تعديل"
        SettingsCommandButton-DeleteButton-Text="حذف"  RightToLeft="True"
        KeyFieldName="Id" SettingsEditing-Mode="Inline" SettingsBehavior-ConfirmDelete="true"
        HorizontalAlign="Center" EnableRowsCache="False" Width="100%" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" EnableTheming="True"
        Theme="Office2010Blue" Font-Size="Small" Styles-Header-HorizontalAlign="Center" Styles-Cell-HorizontalAlign="Center">
        <SettingsPager Visible="true" PageSize="100">
        </SettingsPager>
        <SettingsSearchPanel Visible="True" />


        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
        <Settings GridLines="Both" ShowGroupedColumns="true" ShowGroupButtons="true" ShowHeaderFilterButton="true" ShowPreview="true" ShowFilterBar="Visible"
            ShowGroupPanel="True" ShowGroupFooter="VisibleAlways" GroupSummaryTextSeparator="" ShowFooter="true" />



        <Columns>

            <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" Caption="رقم" Width="50">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="MakeDate" VisibleIndex="1" Caption="تاريخ " PropertiesDateEdit-DisplayFormatString="{0:d/M/yyyy}">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="TypeName" VisibleIndex="2" Caption="نوع المعاملة">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PriorityName" VisibleIndex="3" Caption="حالة المعاملة ">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="IssueNo" VisibleIndex="3" Caption="رقم المعاملة ">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn FieldName="IssueDate" VisibleIndex="4" Caption="تاريخ المعاملة " PropertiesDateEdit-DisplayFormatString="{0:d/M/yyyy}">
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn FieldName="Subject" VisibleIndex="5" Caption="الموضوع">
            </dx:GridViewDataTextColumn>


            <dx:GridViewDataTextColumn FieldName="StatusName" VisibleIndex="6" Caption="الحالة" Width="10%">
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataColumn Caption="تعديل/عرض">
                <DataItemTemplate>
                    <asp:HyperLink ID="EditLink" Text="عرض" NavigateUrl='<%# Eval("ID","ReportEdit?rpt={0}") %>' Target="_blank"
                        runat="server">
                           <img alt="تعديل" src="../Content/img/edit-notes.png" />
                    </asp:HyperLink>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="طباعة">
                <DataItemTemplate>
                    <asp:HyperLink ID="prntLink" Text="طباعة" NavigateUrl='<%# Eval("Id","print?reportid={0}") %>' 
                      Target="_blank"
                        runat="server">
                           <img alt="طباعة" src="../Content/img/print-icon.png" width="30" />
                    </asp:HyperLink>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>

    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetByStatus"
        TypeName="HealthAPP.BLL.ReportData" DataObjectTypeName="HealthAPP.BLL.ReportData">
        <SelectParameters>
           <asp:ControlParameter ControlID="rb_status" Name="status" Type="Byte" />
        </SelectParameters>
    </asp:ObjectDataSource>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
