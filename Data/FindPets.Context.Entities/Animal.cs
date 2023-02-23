namespace FindPets.Context.Entities;

public class Animal : BaseEntity
{
    public string Kind { get; set; }
    public string Breed { get; set; }

    public string Description { get; set; }
    public string Image { get; set; }

    public DateTime LostSince { get; set; } = DateTime.Now;

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
}

