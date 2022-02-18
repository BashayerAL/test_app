<%@ Page Title="تسجيل دخول" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HealthAPP.Account.Login" Async="true" %>

<%--<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>--%>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Login </title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="../Content/img/icons/favicon.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/animate/animate.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/select2/select2.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/vendor/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/css/util.css">
    <link rel="stylesheet" type="text/css" href="../Content/css/main.css">
    <!--===============================================================================================-->
</head>
<body id="page-top">
    <form runat="server">

        <div class="limiter">
            <div class="container-login100" style="background-image: url('Content/img/41.jpg');">
                <div class="wrap-login100 p-t-30 p-b-50">

                    <span class="login100-form-title p-b-41">
                    </span>
                    <span class="login100-form-title p-b-41">تسجيل الدخول
                    </span>
                    <div class="login100-form validate-form p-b-33 p-t-5">

                        <div class="wrap-input100 validate-input" data-validate="Enter username">
                            <%--<input class="input100" type="text" name="username" placeholder="User name">--%>
                            <%--<dx:ASPxTextBox ID="username" runat="server" CssClass="input100" NullText="User Name..." aria-describedby="emailHelp">
                                <ValidationSettings ValidationGroup="ttt" ErrorFrameStyle-Paddings-PaddingRight="20">
                                    <RequiredField IsRequired="true" ErrorText="الزامي" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>--%>

                            <asp:TextBox ID="username" CssClass="input100" runat="server" placeholder="ادخل اسم المستخدم"></asp:TextBox>
                            <span class="focus-input100" data-placeholder="&#xe82a;"></span>
                        </div>

                        <div class="wrap-input100 validate-input" data-validate="Enter password">
                            <%--<input class="input100" type="password" name="pass" placeholder="Password">--%>
                            <%--<dx:ASPxTextBox ID="pass" type="password" runat="server" ClientInstanceName="pass" Password="true" CssClass="input100" NullText="Password...">
                                <ValidationSettings ValidationGroup="ttt">
                                    <RequiredField IsRequired="true" ErrorText="الزامي" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>--%>
                            <asp:TextBox ID="pass" TextMode="Password" CssClass="input100" runat="server" placeholder="كلمة المرور"></asp:TextBox>
                            <span class="focus-input100" data-placeholder="&#xe80f;"></span>
                        </div>

                        <div class="container-login100-form-btn">
                            <%--  <button id="btn_Login" class="login100-form-btn" runat="server" onclick="btn_Login_Click" validationgroup="sign" causesvalidation="true">
                                Login
                            </button>--%>
                            <asp:LinkButton ID="btn_Login" class="login100-form-btn"  runat="server" OnClick="btn_Login_Click" Text="Login" ValidationGroup="sign" ></asp:LinkButton>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="  UserName " ControlToValidate="username" ForeColor="Red" ValidationGroup="sign"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="  Password " ControlToValidate="pass" ForeColor="Red" ValidationGroup="sign"></asp:RequiredFieldValidator>

                        <%--<dx:ASPxButton ID="btn_Login" Visible="false" runat="server" AutoPostBack="true" CausesValidation="true" Text="دخــول"
                                OnClick="LogIn" Theme="Moderno" ValidationGroup="ttt">
                            </dx:ASPxButton>--%>
                        <%--   <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="true" CausesValidation="true" Text="دخــول"
                                        OnClick="LogIn" Theme="Moderno" ValidationGroup="ttt">
                                    </dx:ASPxButton>


                                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel2" runat="server" ClientInstanceName="lpanel"
                                        Modal="true" Text="جاري تسجيل الدخول.." Border-BorderStyle="None" Image-Url="~/Content/img/loading2.gif">
                                    </dx:ASPxLoadingPanel>
                        </div>
                        <table>
                            <tr>
                                <td style="width: 20%;"></td>
                                <td>
                                    <asp:CheckBox runat="server" ID="RememberMe" Visible="false" Checked="false" />
                                </td>
                                <td>
                                  

                                    <dx:ASPxButton ID="btn_Login" runat="server" AutoPostBack="true" CausesValidation="true" Text="دخــول"
                                        OnClick="LogIn" Theme="Moderno" ValidationGroup="ttt">

                                    </dx:ASPxButton>

                                    <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="lpanel"
                                        Modal="true" Text="جاري تسجيل الدخول.." Border-BorderStyle="None" Image-Url="~/Content/img/loading2.gif">
                                    </dx:ASPxLoadingPanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"></td>
                                <td></td>
                                <td>
                                   
                                </td>
                            </tr>
                        </table>

                        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                            <ClientSideEvents ValidationCompleted="function(s, e) {if (e.isValid) {lpanel.Show(); e.processOnServer = true;}}" />
                        </dx:ASPxGlobalEvents>--%>
                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger" style="color: red;">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </div>

        <div id="dropDownSelect1"></div>
    </form>
    <!--===============================================================================================-->
    <script src="../Content/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/vendor/bootstrap/js/popper.js"></script>
    <script src="../Content/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/vendor/daterangepicker/moment.min.js"></script>
    <script src="../Content/vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="../Content/js/main.js"></script>


</body>
</html>
