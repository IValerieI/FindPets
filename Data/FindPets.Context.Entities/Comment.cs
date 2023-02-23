namespace FindPets.Context.Entities;

public class Comment : BaseEntity
{
    public int? AnimalId { get; set; }
    public virtual Animal Animal { get; set; }

    public string Name { get; set; }
    public string Text { get; set; }

    public DateTime Created { get; set; }

}

