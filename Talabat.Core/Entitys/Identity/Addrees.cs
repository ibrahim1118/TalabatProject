namespace Talabat.Core.Entitys.Identity
{
    public class Addrees
    {
        public int id { get; set; }
        public  string FName { get; set; }

        public string LName { get; set; }   

        public string Street { get; set; }

        public string City { get; set; }
        public string Cuntry { get; set; }

        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }


    }
}