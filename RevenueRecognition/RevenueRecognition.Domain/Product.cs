using System;
using RevenueRecognition.Core;

namespace RevenueRecognition.Domain
{
    public class Product : Entity<ProductId>
    {
        public ProductId productId { get; private set; }

        public ProductName Name { get; private set; }
        public String type { get; private set; }

        private RecognitionStrategy _recognitionStrategy;

        public ProductState State { get; private set; }

        public Product(ProductId productId, String type) =>
            Apply(new Events.ProductCreated
            {
                ProductId = productId,
                Type = type
            });

        public void SetName(ProductName name) =>
            Apply(new Events.ProductNameChanged
            {
                ProductId = productId,
                Name = name
            });

        protected void initRecognitionStrategy()
        {
            if ("WORDPROCESSOR".Equals(type))
            {
                _recognitionStrategy = new CompleteRecognitionStrategy();
            }
            else if ("SPREADSHEET".Equals(type))
            {
                _recognitionStrategy = new ThreeWayRecognitionStrategy(60, 90);
            }
            else if ("DATABASE".Equals(type))
            {
                _recognitionStrategy = new ThreeWayRecognitionStrategy(30, 60);
            }
            else
            {
                throw new ArgumentException(
                        "Unsupported product type [" + type + "]");
            }
        }

        public void CalculateRevenueRecognition(Contract contract)
        {
            if (!this.Equals(contract.product))
            {
                throw new ArgumentException("Contract is not for this product");
            }
            _recognitionStrategy.CalculateRevenueRecognitions(contract);
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ProductCreated e:
                    productId = new ProductId(e.ProductId);
                    Name = ProductName.FromString("default");
                    type = e.Type;
                    State = ProductState.Created;
                    initRecognitionStrategy();
                    break;
                case Events.ProductNameChanged e:
                    Name = ProductName.FromString(e.Name);
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            bool valid = productId != null;
            switch (State)
            {
                case ProductState.Created:
                    valid = valid && Name !=null && type != null;
                    break;
            };
    
            if (!valid)
                throw new InvalidEntityStateException(
                    this, $"Post-checks failed in state {State}");
        }

        public enum ProductState
        {
            Created,
            OutOfStock,
            Expired
        }
    }
}
