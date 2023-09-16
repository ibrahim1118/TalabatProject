namespace Talabat.Core.Entitys.Order_Aggraget
{
    public class ProductItmeOrder
    {
        public ProductItmeOrder()
        {

        }

        public ProductItmeOrder(int prodcutId, string prodcutName, string imageUrl)
        {
            ProdcutId = prodcutId;
            ProdcutName = prodcutName;
            ImageUrl = imageUrl;
        }

        public int ProdcutId { get; set; }

        public string ProdcutName { get; set; }
        public string ImageUrl { get; set; }

    }
}