using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class BaseEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public DateTime DateCreationAt { get; set; }
    public DateTime DateModifiedAt { get; set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}