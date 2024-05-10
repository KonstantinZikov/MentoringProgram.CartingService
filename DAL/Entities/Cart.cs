using DAL.Common;
using DAL.ValueObjects;

namespace DAL.Entities
{
    public class Cart : BaseAuditableEntity
    {
        public IList<LineItem> Items { get; private set; } = new List<LineItem>();
    }
}
