namespace Keepr.Models;

public class Profile : ModelBase<string>
{
    public string Name { get; set; }
    public string Picture { get; set; }
    public string CoverImg { get; set; }
}