namespace Mh.Business.Bo.Sys
{
    public class SysMailBo
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public string DisplayName { get; set; }
    }
}