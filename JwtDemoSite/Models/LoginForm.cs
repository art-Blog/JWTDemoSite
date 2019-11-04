namespace JwtDemoSite.Models
{
    /// <summary>
    /// JWT登入表單
    /// </summary>
    public class LoginForm
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
    }
}