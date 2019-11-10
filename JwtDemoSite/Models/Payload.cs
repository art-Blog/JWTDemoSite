namespace JwtDemoSite.Models
{
    /// <summary>
    /// ¦Û­qªºToken¹S¸ü
    /// </summary>
    internal class Payload
    {
        public User info { get; set; }
        public int exp { get; set; }
    }
}