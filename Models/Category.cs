using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace projectef.Models;

[Table("Category")]
public class Category
{
    //[Key]
    public Guid CategoryId {get; set;}

    //[Required]
    //[MaxLength(150)]
    public string Name {get; set;}
    public string Description {get; set;}
    public virtual ICollection<Task> Tasks {get; set;}
}