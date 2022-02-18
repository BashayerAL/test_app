<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportEdit.aspx.cs" Inherits="HealthAPP.ReportEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="direction: rtl;">
        <asp:HiddenField ID="HiddenField_Id" runat="server" />
        <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
            <tr>
                <td style="width: 20%;">
                    <span style="text-align: center; vertical-align: central; font-family: GESSTwoLight-Light; font-size: large; font-weight: 600;">تعديل تقرير
                    </span></td>
                <td style="width: 70%;">
                    <asp:Label ID="lbl_error2" runat="server" ForeColor="Red"></asp:Label></td>
                <td style="width: 10%;">
                    <asp:HyperLink ID="prntLink" Text="طباعة" Target="_blank"
                        runat="server">
                           <img alt="طباعة" src="../Content/img/print-icon.png" width="30" />
                    </asp:HyperLink></td>
            </tr>
        </table>


        <div class="card-body">
            <asp:Panel ID="Pnl_Emp" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
                    <tr>
                        <td style="width: 15%;">رقم التقرير :
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="lbl_Id" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%;">تاريخه:
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="lbl_Makedate" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%;">انشاء بواسطة:
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="lbl_makername" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
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
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Upload1" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
                    <tr>
                        <td style="width: 15%;">مرفق الموضوع:
                        </td>
                        <td style="width: 20%; text-align: right;">
                            <%--<dx:ASPxUploadControl ID="UploadControlPic" runat="server" ClientInstanceName="UploadControl"
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
                            </dx:ASPxLabel>--%>
                            <asp:FileUpload ID="FileUpload_Subject" runat="server" />
                        </td>
                        <td style="width: 10%;">
                            <asp:Button ID="btn_Upload" runat="server" Text="حفظ الملف" OnClick="btn_Upload_Click" />
                            <%--  <dx:UploadedFilesContainer ID="FileContainer" runat="server" Width="380" Height="40"
                            NameColumnWidth="240" SizeColumnWidth="70" />--%>
                        </td>
                        <td colspan="3" style="width: 50%;">

                            <asp:Repeater ID="rep_Attach_1" runat="server">
                                <ItemTemplate>
                                    <div style="float: right; max-width: 200px; margin-left: 15px; border-radius: 20px 20px 20px 20px; box-shadow: 0 0 4px 0 #DCDCC6;">
                                        <asp:LinkButton ID="hlink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'
                                            OnClick="hlink_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
                                        <asp:LinkButton ID="lnk_DeletePic" runat="server" ForeColor="Red" CommandArgument='<%#Eval("Id") %>' OnClientClick="var r= confirm('هل ترغب فعلا في حذف هذا الملف؟'); if(r == false) {  return false;}  ;" OnClick="lnk_DeletePic_Click">حذف</asp:LinkButton>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>

                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Emp2" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
                    <tr>
                        <td style="width: 15%;">الامر المستند عليه:
                        </td>
                        <td colspan="5" style="width: 80%;">
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
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Upload2" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
                    <tr>
                        <td style="width: 15%;">مرفق الوقائع:
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <%--   <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" ClientInstanceName="UploadControl"
                            NullText="Select multiple files..." UploadMode="Auto" AutoStartUpload="true" ShowUploadButton="false" ShowProgressPanel="True"
                            OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">
                            <ClientSideEvents FilesUploadStart="function(s, e) { DXUploadedFilesContainer.Clear(); }"
                                FileUploadComplete="onFileUploadComplete2" />
                            <AdvancedModeSettings EnableMultiSelect="True" EnableFileList="True" EnableDragAndDrop="True" />
                            <ValidationSettings MaxFileSize="2097152" AllowedFileExtensions=".jpg,.doc,.docx,.pdf,.xls,.xlsx,">
                            </ValidationSettings>
                        </dx:ASPxUploadControl>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text=".jpg, .doc,.docx,.pdf,.xls,.xlsx" Font-Size="8pt">
                        </dx:ASPxLabel>

                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="اقصى مساحة للملف: 2 MB." Font-Size="8pt">
                        </dx:ASPxLabel>--%>
                            <asp:FileUpload ID="FileUpload_Facts" runat="server" AllowMultiple="false" />
                        </td>
                        <td style="width: 10%;">
                            <asp:Button ID="btn_fileFacts" runat="server" Text="حفظ الملف" OnClick="btn_fileFacts_Click" />

                        </td>
                        <td colspan="3" style="width: 50%;">

                            <asp:Repeater ID="rep_Attach_2" runat="server">
                                <ItemTemplate>
                                    <div style="float: right; max-width: 200px; margin-left: 15px; border-radius: 20px 20px 20px 20px; box-shadow: 0 0 4px 0 #DCDCC6;">
                                        <asp:LinkButton ID="hlink2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'
                                            OnClick="hlink_Click" CommandArgument='<%#Eval("Id") %>' />
                                        <asp:LinkButton ID="lnk_DeletePic" runat="server" ForeColor="Red" CommandArgument='<%#Eval("Id") %>' OnClientClick="var r= confirm('هل ترغب فعلا في حذف هذا الملف؟'); if(r == false) {  return false;}  ;" OnClick="lnk_DeletePic_Click">حذف</asp:LinkButton>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>

                        </td>
                        <td></td>
                    </tr> <tr>
                        <td style="width: 15%;">مرفق الاجراءات:
                        </td>
                        <td style="text-align: right; width: 20%;">
     
                            <asp:FileUpload ID="FileUpload_Proc" runat="server" AllowMultiple="false" />
                        </td>
                        <td style="width: 10%;">
                            <asp:Button ID="btn_fileProc" runat="server" Text="حفظ الملف" OnClick="btn_fileProc_Click" />

                        </td>
                        <td colspan="3" style="width: 50%;">

                            <asp:Repeater ID="rep_Attach_3" runat="server">
                                <ItemTemplate>
                                    <div style="float: right; max-width: 200px; margin-left: 15px; border-radius: 20px 20px 20px 20px; box-shadow: 0 0 4px 0 #DCDCC6;">
                                        <asp:LinkButton ID="hlink3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'
                                            OnClick="hlink_Click" CommandArgument='<%#Eval("Id") %>' />
                                        <asp:LinkButton ID="lnk_DeletePic" runat="server" ForeColor="Red" CommandArgument='<%#Eval("Id") %>' OnClientClick="var r= confirm('هل ترغب فعلا في حذف هذا الملف؟'); if(r == false) {  return false;}  ;" OnClick="lnk_DeletePic_Click">حذف</asp:LinkButton>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>

                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Emp3" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                    <tr>
                        <td style="width: 15%;">الاجراءات:
                        </td>
                        <td colspan="5" style="width: 80%;">
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
                        <td>إضافة لجنة:
                        </td>
                        <td colspan="3">
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
                        <td colspan="2">
                            <asp:GridView ID="GridView_emp" runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Name" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_Deleteemp" runat="server" ForeColor="Red"
                                                CommandArgument='<%#Eval("Id") %>'
                                                OnClientClick="var r= confirm('هل ترغب فعلا في الحذف؟'); if(r == false) {  return false;}  ;"
                                                OnClick="lnk_Deleteemp_Click">حذف</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_AttachList" runat="server" Visible="false">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">
                    <tr>
                        <td style="width: 15%;">المرفقات:
                        </td>
                        <td colspan="5" style="width: 80%;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource1">
                                <Columns>
                                    <asp:TemplateField HeaderText="الملف">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hlink2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'
                                                OnClick="hlink_Click" CommandArgument='<%#Eval("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TypeName" HeaderText="نوع الملف" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetByReport" TypeName="HealthAPP.BLL.ReportFiles">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="report" QueryStringField="rpt" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Committee" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                    <tr>
                        <td style="width: 15%;">النتائج:
                        </td>
                        <td style="width: 80%;">
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
                        <td>
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
                        <td>رأي الطرف الخارجي:
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txt_Outsidenotes" runat="server" CssClass="form-control" Width="100%"
                                TextMode="MultiLine" Height="60"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Manager" runat="server" Enabled="false">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                    <tr>

                        <td style="width: 15%;">مرئيات مدير الادارة:
                        </td>
                        <td style="width: 80%;">
                            <asp:TextBox ID="txt_ManagerNotes" runat="server" CssClass="form-control" Width="100%"
                                TextMode="MultiLine" Height="60"></asp:TextBox>
                        </td>
                        <td>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_results"
                            ErrorMessage="*" ToolTip="ادخل النتائج!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>

                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Pnl_Ceo" runat="server" Enabled="false">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                    <tr>

                        <td style="width: 15%;">توجيه الرئيس التنفيذي:
                        </td>
                        <td style="width: 80%;">
                            <asp:TextBox ID="txt_CeoNotes" runat="server" CssClass="form-control" Width="100%"
                                TextMode="MultiLine" Height="60"></asp:TextBox>
                        </td>
                        <td>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_results"
                            ErrorMessage="*" ToolTip="ادخل النتائج!" ValidationGroup="saveall"
                            ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="text-align: right;">
                <asp:Label ID="lbl_Edit" runat="server" Text=""></asp:Label>
            </div>
            <asp:Panel ID="Pnl_Save" runat="server">
                <table class="table table-bordered" style="width: 90%; color: black;" cellspacing="0" dir="rtl">

                    <tr>
                        <td align="center">
                            <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                            <asp:Button ID="btn_Save" runat="server" Text="حفظ" OnClick="btn_Save_Click" CssClass="btn btn-primary" Width="100"
                                ValidationGroup="saveall" />
                            <br />
                        </td>
                        <td>
                            <br />
                            <asp:Button ID="btn_Cancel" runat="server" Text="إلغاء التعديل" OnClick="btn_Cancel_Click" CssClass="btn btn-danger" Width="100" />
                        </td>
                        <td align="center">
                            <asp:Label ID="lbl_Send" runat="server" ForeColor="Green"></asp:Label>
                            <br />
                            <asp:Button ID="btn_Send" runat="server" Text="إرسال الى المدير" OnClick="btn_Send_Click"
                                CssClass="btn btn-primary" />


                            <asp:Button ID="btn_back" runat="server" Text="ارجاع الى الموظف" OnClick="btn_back_Click"
                                CssClass="btn btn-dark" Visible="false" />
                        </td>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" ForeColor="Green"></asp:Label>
                            <br />
                            <asp:Button ID="btn_prnt" runat="server" Text="طباعة" OnClick="btn_prnt_Click"
                                CssClass="btn btn-primary" />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
