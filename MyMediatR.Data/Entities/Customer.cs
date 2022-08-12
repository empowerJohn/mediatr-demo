using System.ComponentModel.DataAnnotations.Schema;

namespace MyMediatR.Data.Entities;

public class Customer
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
