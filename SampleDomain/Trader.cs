using Domain.Tests;

namespace Domain;

public class Trader : IEntity
{
    public Guid Id { get; }
    private string _firstName;
    public string FirstName => _firstName;
    private string _lastName;
    public string LastName => _lastName;
    private List<TraderItems> _traderItems;
    public List<TraderItems> TraderItems => _traderItems;

    private Trader()
    {
        
    }

    private Trader(Guid id, string firstName, string lastName, List<TraderItems> traderItems)
    {
        _firstName = firstName;
    }

    public static Trader Create(Guid id, string firstName, string lastName, List<TraderItems> traderItems)
    {
        return new Trader(id, firstName, lastName, traderItems);
    }
    

}
public class TraderItems
{
    
}
