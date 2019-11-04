namespace JwtDemoSite.Models
{
    /// <summary>
    /// API結果
    /// </summary>
    public class APIResult
    {
        /// <summary>
        /// 執行結果
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 回應內容
        /// </summary>
        public object Data { get; set; }
    }
}