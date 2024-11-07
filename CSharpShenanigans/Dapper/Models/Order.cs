namespace CSharpShenanigans.Dapper.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Amount { get; set; }
}
