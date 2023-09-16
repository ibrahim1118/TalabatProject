namespace Adminpanal.Models
{
    public class UserVm
    {
        public  string Id { get; set; }
        public string UserName { get; set; }

        public string DispalyName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
