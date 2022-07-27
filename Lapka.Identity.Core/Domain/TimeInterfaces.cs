namespace Lapka.Identity.Core.Domain;

public interface ICreatedAt
{
    public DateTime CreatedAt { get; set; }
}

public interface IModifiedAt
{
    public DateTime? ModifiedAt { get; set; }
}