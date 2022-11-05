namespace DesafioBrainlaw.Application.Models.Product.Response
{
    public class ProductResponseModel
    {
        #region Public Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        #endregion Public Properties
    }
}