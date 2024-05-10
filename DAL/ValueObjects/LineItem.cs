using DAL.Common;

namespace DAL.ValueObjects
{
    public class LineItem : ValueObject
    {
        public required Guid Id { get; init; }

        public required int ProductId { get; init; }

        public required string Name { get; init; }

        public Image? Image { get; init; }

        public required Money Price { get; init; }

        public required int Quantity { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
