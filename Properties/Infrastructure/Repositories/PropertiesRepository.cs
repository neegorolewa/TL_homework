using Domain.Entities;
using Domain.Repositories;
namespace Infrastructure.Repositories;
public class PropertiesRepository : IPropertiesRepository
{
    //бд
    private static List<Property> Properties = [];
    //
    public void Add( Property property )
    {
        property.Id = Guid.NewGuid();
        Properties.Add( property );
    }

    public void DeleteById( Guid id )
    {
        Property? existingProperty = GetById( id );
        if ( existingProperty is null )
        {
            throw new InvalidOperationException( $"Property with id - {id} does not exists" );
        }

        Properties.Remove( existingProperty );
    }

    public Property? GetById( Guid id )
    {
        return Properties.FirstOrDefault( p => p.Id == id );
    }

    public List<Property> List()
    {
        return Properties.ToList();
    }

    public void Update( Property property )
    {
        Property? existingProperty = GetById( property.Id );

        if ( existingProperty is null )
        {
            throw new InvalidOperationException( $"Property with id - {property.Id} does not exists" );
        }

        existingProperty.Name = property.Name;
    }
}
