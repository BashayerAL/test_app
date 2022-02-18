using System;
using System.Web;
using System.Web.UI;

using HealthAPP.BLL;
using System.Web.UI.WebControls;

namespace HealthAPP.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// RegisterHyperLink.NavigateUrl = "Register";
            //// Enable this once you have account confirmation enabled for password reset functionality
            //HyperLink forget = loginview.FindControl("ForgotPasswordHyperLink") as HyperLink;
            //forget.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            if (!IsPostBack)
            {
                //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
                //if (!String.IsNullOrEmpty(returnUrl) && (HttpContext.Current.User.Identity.IsAuthenticated))
                //{
                //    IdentityHelper.RedirectToReturnUrl(returnUrl, Response);
                //}

                //    var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                //    AuthenticationManager.SignOut();
                Session["emp"] = null;
                //    Session["projectset"] = null;
                //    Session["branchset"] = null;
            }
            //else
            //    btn_Login_Click(null, null);
            //if (HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    btn_Login.Visible = false; username.Visible = false; pass.Visible = false;
            //}
            //else
            //{
            //    btn_Login.Visible = username.Visible = pass.Visible = true;
            //}
        }

        protected void LogIn(object sender, EventArgs e)
        {

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect("~/default");
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            // System.Threading.Thread.Sleep(1000);
            //Session.Clear();
            //Session.Abandon();
            Session["emp"] = null;
            if (IsValid)
            {
                Employee emp = Employee.GetByUserName(username.Text.Trim());
                if (emp == null)
                {
                    FailureText.Text = "اسم مستخدم خطأ!!";
                    ErrorMessage.Visible = true;
                    return;
                }
                else if (emp.Status != 1)
                {
                    FailureText.Text = "مستخدم غير نشط.. غير مصرح بالدخول!!";
                    ErrorMessage.Visible = true;
                    return;
                }
                if (emp.Password == pass.Text.Trim() && emp.Status == 1)
                {
                    Session["emp"] = emp.Id;
                    Response.Redirect("default");
                }
            }
            //    // Validate the user password
            //    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
            //    //TextBox Email = loginview.FindControl("Email") as TextBox;
            //    //TextBox Password = loginview.FindControl("Password") as TextBox;
            //    //Literal FailureText = loginview.FindControl("FailureText") as Literal;
            //    //PlaceHolder ErrorMessage = loginview.FindControl("ErrorMessage") as PlaceHolder;
            //    // This doen't count login failures towards account lockout
            //    // To enable password failures to trigger lockout, change to shouldLockout: true
            //    var result = signinManager.PasswordSignIn(username.Text, pass.Text, false, shouldLockout: true);
            //    string url = "~/default";
            //    //if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            //    //    url = "~/ProjectSet?url=" + Request.QueryString["ReturnUrl"];

            //    switch (result)
            //    {
            //        case SignInStatus.Success:
            //            {
            //                Session["emp"] = null;
            //                Session["projectset"] = null;
            //                Session["branchset"] = null;

            //                //  var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //                //   var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>()));
            //                //ApplicationUser user = manager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //                //if (user != null)
            //                //{
            //                //    Employee _Emp = Employee.GetEmployeeByUserId(Guid.Parse(user.Id));
            //                //    if (_Emp != null)
            //                //    {
            //                //        System.Web.HttpContext.Current.Session["emp"] = _Emp;
            //                //        Employee.SetLogin(DateTime.Now, _Emp.Id);
            //                //    }
            //                //}
            //                //if (UberAll.IsInRole("Dam"))
            //                //    url = "~/Dams/Default";     

            //                Session["LastLogin"] = DateTime.Now;

            //                Employee emp = UberAll.GetCurrentEmp();
            //                //if (emp.Status > 2)
            //                //{
            //                //    var AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            //                //    AuthenticationManager.SignOut();
            //                //    FailureText.Text = "موظف غير فعال";
            //                //    ErrorMessage.Visible = true;
            //                //    break;
            //                //}
            //                IdentityHelper.RedirectToReturnUrl(url, Response);
            //                break;
            //            }
            //        case SignInStatus.LockedOut:
            //            Response.Redirect("/Lockout");
            //            break;
            //        case SignInStatus.RequiresVerification:
            //            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
            //                                            Request.QueryString["ReturnUrl"],
            //                                            false),
            //                              true);
            //            break;
            //        case SignInStatus.Failure:
            //        default:
            //            FailureText.Text = "خطأ في اسم المستخدم او كلمة المرور";
            //            ErrorMessage.Visible = true;
            //            break;
            //    }
            //}
        }
    }
}