namespace Adminpanal.Models
{
    public class UserRoleVM
    {
        public  string UserId { get; set; }
        public string UserName { get; set; }    

        public IList<RoleViewModle> Roles { get; set; }
    }
}
