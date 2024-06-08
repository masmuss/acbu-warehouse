using System.ComponentModel.DataAnnotations;

namespace warehouse.Models.Dto;

public class CreateShippingView
{
    public string BookingConfirmation { get; set; }
    [DataType(DataType.Date)]
    public DateTime ActualShipment { get; set; }
    [DataType(DataType.Date)]
    public DateTime CargoReady { get; set; }
    public string ContNo { get; set; }
    public string SealNo { get; set; }
    public string Destination { get; set; }

    // Invoice and Sales Document properties
    public string InvoiceCodes { get; set; }
    public string SalesDocumentCodes { get; set; }

    // List of Products
    public List<ProductView> Products { get; set; }
    
    public CreateShippingView()
    {
        Products = new List<ProductView>();
    }
}