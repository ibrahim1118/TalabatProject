namespace Talabt.APIs.Erorr
{
    public class ExcepationRespons: ApiRespones
    {
        public string? Detiles { get; set; } 
        public ExcepationRespons(int statuseCode , string? Message  =null, string? Detiles = null):base(statuseCode , Message) 
        {

            this.Detiles = Detiles; 
        }
    }
}
