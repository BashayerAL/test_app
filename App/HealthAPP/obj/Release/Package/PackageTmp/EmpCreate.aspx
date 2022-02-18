<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpCreate.aspx.cs" Inherits="HealthAPP.EmpCreate" %>

<%@ Register Assembly="DevExpress.Web.v18.1, Version=18.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lbl_Error" runat="server" Text="" ForeColor="Red"></asp:Label>
    <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" SettingsCommandButton-EditButton-Text="تعديل"
        SettingsCommandButton-DeleteButton-Text="حذف" OnRowInserted="grid_RowInserted" RightToLeft="True"
        KeyFieldName="Id" SettingsEditing-Mode="Inline" SettingsBehavior-ConfirmDelete="true" SettingsText-ConfirmDelete="هل تريد حذف هذه المادة بالفعل|؟"
        HorizontalAlign="Center" EnableRowsCache="False" Width="100%" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" EnableTheming="True"
        Theme="Office2010Blue" Font-Size="Small" Styles-Header-HorizontalAlign="Center" Styles-Cell-HorizontalAlign="Center">
        <SettingsPager Visible="true" PageSize="100">
        </SettingsPager>
        <SettingsSearchPanel Visible="True" />
        <SettingsEditing Mode="Inline">
        </SettingsEditing>

        <SettingsBehavior ConfirmDelete="True"></SettingsBehavior>
        <Settings GridLines="Both" ShowGroupedColumns="true" ShowGroupButtons="true" ShowHeaderFilterButton="true" ShowPreview="true" ShowFilterBar="Visible"
            ShowGroupPanel="True" ShowGroupFooter="VisibleAlways" GroupSummaryTextSeparator="" ShowFooter="true" />
        <SettingsCommandButton>

            <DeleteButton Image-Url="../Content/Images/Delete.png" Image-Width="25" Text=" " Image-ToolTip="حذف">
            </DeleteButton>
            <EditButton Text="تعديل" Image-Url="../Content/img/edit-notes.png" Image-ToolTip="تعديل"></EditButton>
            <NewButton Text="إضافة" Image-Url="../Content/img/plus-sign.png" Image-ToolTip="إضافة"></NewButton>
            <UpdateButton Text=" " Image-Url="../Content/img/right_icon.png" Image-Width="25" Image-ToolTip="حفظ"></UpdateButton>
            <CancelButton Text=" " Image-Url="../Content/img/icon-delete.gif" Image-Width="25" Image-ToolTip="الغاء"></CancelButton>
        </SettingsCommandButton>

        <SettingsText ConfirmDelete="هل تريد حذف  بالفعل|؟"></SettingsText>
        <Columns>

            <dx:GridViewDataTextColumn FieldName="Id" VisibleIndex="0" ReadOnly="true" Caption="رقم" Width="50">
                <PropertiesTextEdit Width="40px">
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" Caption="اسم الموظف كامل " Width="20%">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="SignName" VisibleIndex="2" Caption="التوقيع" Width="15%">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Notes" VisibleIndex="3" Caption="الصفة " Width="20%"> 
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UserName" VisibleIndex="3" Caption="اسم المستخدم " Width="15%">
                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Password" VisibleIndex="4" Caption="كلمة المرور " Width="15%" PropertiesTextEdit-Password="true">
                <%--                <PropertiesTextEdit>
                    <ValidationSettings>
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>--%>
                <%--  <EditItemTemplate>
                    <dx:ASPxTextBox runat="server" ID="txtpass"
                       Text='<%# Bind("Password")%>' Password="true"
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings RequiredField-IsRequired="true" />

                    </dx:ASPxTextBox>
                </EditItemTemplate>--%>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RoleName" VisibleIndex="5" Caption="الصلاحيات" Width="10%">
                <EditItemTemplate>
                    <dx:ASPxComboBox runat="server" ID="cb"
                        TextField="Name" ValueField="Id" Value='<%# Bind("RoleId")%>' ValueType="System.Byte"
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings RequiredField-IsRequired="true" />
                        <Items>
                            <dx:ListEditItem Text="موظف" Value="1" />
                            <dx:ListEditItem Text="مديــر" Value="2" />
                            <dx:ListEditItem Text="رئيس تنفيذي" Value="3" />
                        </Items>
                    </dx:ASPxComboBox>
                </EditItemTemplate>

            </dx:GridViewDataTextColumn>


            <dx:GridViewDataTextColumn FieldName="StatusName" VisibleIndex="7" Caption="الحالة" Width="10%">
                <EditItemTemplate>
                    <dx:ASPxComboBox runat="server" ID="cb"
                        TextField="Name" ValueField="Id" Value='<%# Bind("Status")%>' ValueType="System.Byte"
                        ValidationSettings-ValidationGroup='<%# Container.ValidationGroup %>'>
                        <ValidationSettings RequiredField-IsRequired="true" />
                        <Items>
                            <dx:ListEditItem Text="نشط" Value="1" />
                            <dx:ListEditItem Text="غير نشط" Value="2" />
                        </Items>
                    </dx:ASPxComboBox>
                </EditItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ShowDeleteButton="false" ShowEditButton="true" ShowNewButtonInHeader="True" VisibleIndex="8">
            </dx:GridViewCommandColumn>
            <%--<dx:GridViewDataTextColumn FieldName="BranchName" ReadOnly="True" VisibleIndex="2" Caption="الفرع ">
            </dx:GridViewDataTextColumn>--%>
        </Columns>

    </dx:ASPxGridView>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="CreateNew" SelectMethod="GetAll"
        TypeName="HealthAPP.BLL.Employee" UpdateMethod="Update" DataObjectTypeName="HealthAPP.BLL.Employee"></asp:ObjectDataSource>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
