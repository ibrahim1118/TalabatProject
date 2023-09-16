namespace Talabt.APIs.Erorr
{
    public class VialdtionErorrResponse : ApiRespones
    {
        public IEnumerable<string> Erorrs { get; set; }

        public VialdtionErorrResponse(): base(400)
        {
            Erorrs = new  List<string>();   
        }
    }
}
