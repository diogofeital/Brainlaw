using FluentValidation;

namespace DesafioBrainlaw.Domain.Entities
{
    public class Product : Entity<Product>
    {
        #region Public Constructors

        public Product(string name, string description, decimal price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Update(string name, string description, decimal price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            ValidateName();
            ValidatePrice();
            ValidateQuantity();

            AddErrors(Validate(this));

            return ValidationResult.IsValid;
        }

        #endregion Public Methods

        #region Private Methods

        private void ValidateName()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido.");
        }

        private void ValidatePrice()
        {
            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("O valor precisa ser preenchido.");
        }

        private void ValidateQuantity()
        {
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .WithMessage("A quantidade precisa ser preenchida.");
        }

        #endregion Private Methods
    }
}