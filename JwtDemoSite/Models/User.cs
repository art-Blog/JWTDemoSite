namespace JwtDemoSite.Models
{
    /// <summary>
    /// 使用者
    /// </summary>
    public class User
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        public int EmployeeNo { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// E-Mail
        /// </summary>
        public string EmailAccount { get; set; }
    }
}