using System.ComponentModel.DataAnnotations;

namespace warehouse.Models;

public class Invoice
{
    [Key]
    public int Id { get; set; }

    public int ShippingId { get; set; }
    public Shipping Shipping { get; set; }

    public string InvoiceCode { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}