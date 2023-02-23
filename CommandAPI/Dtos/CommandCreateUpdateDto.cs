using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

public class CommandCreateUpdateDto
{
    [Required]
    [MaxLength]
    public string HowTo { get; set; }
    
    [Required]
    public string Platform { get; set; }
    
    [Required]
    public string CommandLine { get; set; }
}