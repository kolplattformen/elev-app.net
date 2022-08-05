namespace SkolplattformenElevApi.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    //  public string Phone { get; set; }
    public string Email { get; set; }
    public List<string> Subjects { get; set; } = new List<string>();
}