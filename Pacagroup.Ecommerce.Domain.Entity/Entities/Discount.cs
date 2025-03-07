using Pacagroup.Ecommerce.Domain.Common;
using Pacagroup.Ecommerce.Domain.Enums;

namespace Pacagroup.Ecommerce.Domain.Entities;

public class Discount: BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Percent { get; set; }
    public DiscountEstatus Estatus {  get; set; }
}
