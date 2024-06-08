using System.ComponentModel.DataAnnotations;

namespace warehouse.Models;

public class Shipping
{
    [Key]
    public int Id { get; set; }

    public string BookingConfirmation { get; set; }

    [DataType(DataType.Date)]
    public DateTime ActualShipment { get; set; }

    [DataType(DataType.Date)]
    public DateTime CargoReady { get; set; }

    public string ContNo { get; set; }
    public string SealNo { get; set; }
    public string Destination { get; set; }

    public ICollection<Invoice> Invoices { get; set; }
    public ICollection<SalesDocument> SalesDocuments { get; set; }
    public ICollection<Product> Products { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}