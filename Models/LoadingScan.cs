using System.ComponentModel.DataAnnotations;

namespace warehouse.Models;

public class LoadingScan
{
    [Key]
    public int Id { get; set; }
    public int PreparationId { get; set; }
    public PreparationScan PreparationScan { get; set; }
    public string Destination { get; set; }
    public int Drl { get; set; }
    public string Model { get; set; }
    public string Do { get; set; }
    public string ContainerNumber { get; set; }
    public string SerialNumber { get; set; }
    public int Actual { get; set; } // scan
    public int Plan { get; set; } // refer actual preparation
}