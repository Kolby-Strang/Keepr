namespace Keepr.Models;

public class VaultKeep : ModelBase<int>
{
    public string CreatorId { get; set; }
    public int VaultId { get; set; }
    public int KeepId { get; set; }
}