namespace DesafioBrainlaw.Application.Models.Product.Request
{
    public class ProductRequestModel
    {
        #region Public Properties

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        #endregion Public Properties
    }
}