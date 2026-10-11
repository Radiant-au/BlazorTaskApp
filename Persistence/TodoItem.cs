using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.Persistence;

public sealed class TodoItem
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = "";

    public bool IsComplete { get; set; }
}
