<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportCreate.aspx.cs" ValidateRequest="false" Inherits="HealthAPP.ReportCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        textarea {
            max-width: 680px !important;
        }
    </style>
    <script type="text/javascript" src="../Scripts/Demo.js?v=16_1_5_0"></script>
    <script type="text/javascript">
        function onFileUploadComplete(s, e) {
            if (e.callbackData) {
                var fileData = e.callbackData.split('|');
                var fileName = fileData[0],
                    fileUrl = fileData[1],
                    fileSize = fileData[2];
                DXUploadedFilesContainer.AddFile(fileName, fileUrl, fileSize);
            }
        }

        function CloseGridLookup() {
            gridLookup.ConfirmCurrentSelection();
            gridLookup.HideDropDown();
            gridLookup.Focus();
        }</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="direction: rtl;">
        <span style="text-align: center; float: right; vertical-align: central; font-family: GESSTwoLight-Light; font-size: x-large; font-weight: 800;">إنشاء تقرير
        </span>
        <br />
        <%--      <a href="msgOutbox" class="btn btn-info">قائمة الرسائل المرسلة</a>
        <a href="msgInbox" class="btn btn-info">قائمة الرسائل الواردة</a>--%>

        <div style="text-align: center;">
            <asp:Label ID="lbl_error2" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="card-body">
            <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                <tr>
                    <td style="width: 15%;">نوع المعاملة :
                    </td>
                    <td style="width: 20%;">
                        <asp:RadioButtonList ID="rb_Type" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="إداري" Value="1"> </asp:ListItem>
                            <asp:ListItem Text="طبي" Value="2"> </asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 10%;">رقم المعاملة:
                    </td>
                    <td style="width: 20%; text-align: right;">
                        <asp:TextBox ID="txt_IssueNo" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_IssueNo"
                            ErrorMessage="*" ToolTip="ادخل النتائج!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10%;">تاريخ المعاملة:
                    </td>
                    <td style="width: 20%;">
                        <dx:ASPxDateEdit ID="txt_issueDate" runat="server" AllowMouseWheel="False" Font-Size="Small"
                            AllowUserInput="False" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Theme="DevEx" Width="120">
                        </dx:ASPxDateEdit>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>حالة المعاملة:
                    </td>
                    <td colspan="5">
                        <asp:RadioButtonList ID="rb_priority" runat="server" RepeatDirection="Horizontal" CellPadding="20" CellSpacing="10" CssClass="table">
                            <asp:ListItem Text="عادي" Value="1"></asp:ListItem>
                            <asp:ListItem Text="سري" Value="2"></asp:ListItem>
                            <asp:ListItem Text="عاجل" Value="3"></asp:ListItem>
                            <asp:ListItem Text="عاجل جدا" Value="4"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="rb_priority"
                            ErrorMessage="*" ToolTip="ادخل الحالة!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>الموضوع:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_subject" runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_subject"
                            ErrorMessage="*" ToolTip="ادخل الموضوع!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>مرفق الموضوع:
                    </td>
                    <td colspan="2" style="text-align: right;">
                        <dx:ASPxUploadControl ID="UploadControlPic" runat="server" ClientInstanceName="UploadControl"
                            NullText="Select multiple files..." UploadMode="Auto" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="True"
                            OnFileUploadComplete="UploadControlPic_FileUploadComplete">
                            <ClientSideEvents FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }"
                                FileUploadComplete="onFileUploadComplete" />
                            <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="True" EnableDragAndDrop="True" />
                            <ValidationSettings MaxFileSize="2097152" AllowedFileExtensions=".jpg,.doc,.docx,.pdf,.xls,.xlsx,">
                            </ValidationSettings>
                        </dx:ASPxUploadControl>
                        <dx:ASPxLabel ID="AllowedFileExtensionsLabel" runat="server" Text=".jpg, .doc,.docx,.pdf,.xls,.xlsx" Font-Size="8pt">
                        </dx:ASPxLabel>

                        <dx:ASPxLabel ID="MaxFileSizeLabel" runat="server" Text="اقصى مساحة للملف: 2 MB." Font-Size="8pt">
                        </dx:ASPxLabel>
                        <%--<asp:FileUpload ID="FileUpload_Subject" runat="server" />--%>
                    </td>

                    <td colspan="3">

                        <dx:UploadedFilesContainer ID="FileContainer" runat="server" Width="380" Height="40"
                            NameColumnWidth="240" SizeColumnWidth="70" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>الامر المستند عليه:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_basedon" runat="server" CssClass="form-control" Width="100%" MaxLength="100"></asp:TextBox>
                    </td>
                    <td>
                        <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_basedon"
                            ErrorMessage="*" ToolTip="ادخل الامر المستند عليه!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>الوقائع:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_Facts" runat="server" CssClass="form-control" Width="100%"
                            TextMode="MultiLine" Height="60"></asp:TextBox>
                    </td>
                    <td>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Facts"
                            ErrorMessage="*" ToolTip="ادخل الوقائع!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <%-- <tr>
                    <td>رفع ملف الوقائع:
                    </td>
                    <td colspan="5" style="text-align:right;">
                        <asp:FileUpload ID="FileUpload_facts" runat="server" />
                    </td>
                    <td></td>
                </tr>--%>
                <tr>
                    <td>الاجراءات:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_procedures" runat="server" CssClass="form-control" Width="100%"
                            TextMode="MultiLine" Height="60"></asp:TextBox>
                    </td>
                    <td>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_procedures"
                            ErrorMessage="*" ToolTip="ادخل الاجراءات!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>النتائج:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_results" runat="server" CssClass="form-control" Width="100%"
                            TextMode="MultiLine" Height="60"></asp:TextBox>
                    </td>
                    <td>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_results"
                            ErrorMessage="*" ToolTip="ادخل النتائج!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>التوصيات:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_recommend" runat="server" CssClass="form-control" Width="100%"
                            TextMode="MultiLine" Height="60"></asp:TextBox>
                    </td>
                    <td>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_recommend"
                            ErrorMessage="*" ToolTip="ادخل التوصيات!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>إضافة لجنة:
                    </td>
                    <td colspan="5">
                        <asp:ObjectDataSource ID="ObjectDataSource_Emp" runat="server" SelectMethod="GetByActive" TypeName="HealthAPP.BLL.Employee"></asp:ObjectDataSource>
                        <dx:ASPxGridLookup ID="GridLookup" runat="server" AutoGenerateColumns="False" Caption="اختار اعضاء اللجنة" ClientInstanceName="gridLookup"
                            DataSourceID="ObjectDataSource_Emp" KeyFieldName="Id" MultiTextSeparator=", " SelectionMode="Multiple" AllowUserInput="false" TextFormatString="{0}"
                            Width="70%" GridViewProperties-SettingsPager-PageSize="20">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" />
                                <dx:GridViewDataColumn FieldName="Name" />
                                <%--<dx:GridViewDataColumn FieldName="Id" Settings-AllowAutoFilter="False" />--%>
                            </Columns>
                            <GridViewProperties>
                                <Templates>
                                    <StatusBar>
                                        <table class="OptionsTable" style="float: right">
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="Close" runat="server" AutoPostBack="false" ClientSideEvents-Click="CloseGridLookup" Text="Close" />
                                                </td>
                                            </tr>
                                        </table>
                                    </StatusBar>
                                </Templates>
                                <Settings ShowFilterRow="True" ShowStatusBar="Visible" />
                            </GridViewProperties>
                        </dx:ASPxGridLookup>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>رأي الطرف الخارجي:
                    </td>
                    <td colspan="5">
                        <asp:TextBox ID="txt_Outsidenotes" runat="server" CssClass="form-control" Width="100%"
                            TextMode="MultiLine" Height="60"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                        <asp:Button ID="btn_Send" runat="server" Text="حفظ" OnClick="btn_Send_Click" CssClass="btn btn-primary" Width="100"
                            ValidationGroup="saveall" />
                        <br />
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="btn_Cancel" runat="server" Text="إلغاء" OnClick="btn_Cancel_Click" CssClass="btn btn-danger" Width="100" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="Server">
</asp:Content>
