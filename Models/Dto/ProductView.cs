namespace warehouse.Models.Dto;

public class ProductView
{
    public int ProductId { get; set; }
    public string ContainerSize { get; set; }
    public string ContainerQ { get; set; }
    public string? Po { get; set; }
    public string Model { get; set; }
    public string Packing { get; set; }
    public int QtyPlan { get; set; }
    public double NwPack { get; set; }
    public double NwTotal { get; set; }
    public double GwPack { get; set; }
    public double GwTotal { get; set; }
    public double M3L { get; set; }
    public double M3Total { get; set; }
}