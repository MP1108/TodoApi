namespace TodoApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string Tytul { get; set; } = "";
    public bool Zrobione { get; set; }
    public DateTime Dodano { get; set; } = DateTime.Now;
}