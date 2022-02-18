<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpChangePassword.aspx.cs" Inherits="HealthAPP.EmpChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <span style="text-align: right; align-content:start; float:right; vertical-align: central; font-family: GESSTwoLight-Light; font-size: x-large; font-weight: 800;">تغيير كلمة المرور
    </span>

    <%--      <a href="msgOutbox" class="btn btn-info">قائمة الرسائل المرسلة</a>
        <a href="msgInbox" class="btn btn-info">قائمة الرسائل الواردة</a>--%>
    <br />
    <div style="text-align: center;">
        <asp:Label ID="lbl_error2" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div class="card-body">
        <table class=" table-bordered"  id="dataTable1" style="width: 90%;" cellspacing="0" dir="rtl">
            <tr>
                <td style="width:15%;">اسم المستخدم
                </td>
                <td  style="width:25%;">
                    <asp:Label ID="lbl_Username" runat="server"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>كلمة المرور الحالية
                </td>
                <td>
                    <asp:TextBox ID="txt_PassCurrent" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_PassCurrent" ValidationGroup="pass"
                        ErrorMessage="يجب ادخال كلمة المرور الحالية" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>كلمة الجديدة
                </td>
                <td>
                    <asp:TextBox ID="txt_PassNew" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_PassNew2" ValidationGroup="pass"
                        ErrorMessage="يجب ادخال كلمة المرور الجديدة" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>إعادة ادخال كلمة المرور
                </td>
                <td>
                    <asp:TextBox ID="txt_PassNew2" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_PassNew2" ValidationGroup="pass"
                        ErrorMessage="يجب اعادة ادخال كلمة المرور الجديدة" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CompareValidator runat="server" ControlToCompare="txt_PassNew" ControlToValidate="txt_PassNew2" ForeColor="Red"
                        CssClass="text-danger" Display="Dynamic" ErrorMessage="اعادة ادخال كلمة المرور غير متطابق  !!"
                        ValidationGroup="pass" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Send" runat="server" Text="Submit" OnClick="btn_Send_Click" CssClass="btn btn-primary" Width="100"
                        ValidationGroup="pass" />
                </td>
                <td>
                    <asp:Timer ID="Timer1" runat="server" Interval="1500" OnTick="Timer1_Tick" Enabled="false"></asp:Timer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
