namespace FindPets.Context.Entities;

public class Request : BaseEntity
{
    public int? AnimalId { get; set; }
    public virtual Animal Animal { get; set; }

    public string Name { get; set; }
    public string Phone { get; set; }

}

