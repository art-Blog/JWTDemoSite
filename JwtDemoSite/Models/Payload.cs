namespace JwtDemoSite.Models
{
    /// <summary>
    /// �ۭq��Token�S��
    /// </summary>
    internal class Payload
    {
        public User info { get; set; }
        public int exp { get; set; }
    }
}