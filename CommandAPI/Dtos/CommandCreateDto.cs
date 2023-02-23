using System.ComponentModel.DataAnnotations;

namespace CommandAPI.Dtos;

public class CommandCreateDto
{
    [Required]
    [MaxLength]
    public string HowTo { get; set; }
    
    [Required]
    public string Platform { get; set; }
    
    [Required]
    public string CommandLine { get; set; }
}