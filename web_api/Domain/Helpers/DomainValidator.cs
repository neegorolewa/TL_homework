namespace Domain.Helpers;

public class DomainValidator
{
    public static void NullOrEmpty( string value, string nameOfValue )
    {
        if ( string.IsNullOrEmpty( value ) )
        {
            throw new ArgumentException( $"'{nameOfValue}' can't be null or empty", nameOfValue );
        }
    }

    public static void EmptyGuid( Guid guid, string nameOfValue )
    {
        if ( guid == Guid.Empty )
        {
            throw new ArgumentNullException( $"{nameOfValue} can't be empty", nameOfValue );
        }
    }

    public static void InvalidDateRange( DateOnly arrivalDate, DateOnly departureDate, TimeOnly arrivalTime, TimeOnly departureTime )
    {
        if ( arrivalDate.ToDateTime( arrivalTime ) >= departureDate.ToDateTime( departureTime ) )
        {
            throw new ArgumentOutOfRangeException( "Departure date and time must be after arrival date and time" );
        }
    }

    public static void InvalidDailyPrice( decimal dailyPrice, string nameOfValue )
    {
        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"'{nameOfValue} must be greater than 0'" );
        }
    }

    public static void InvalidPersonCount( int minPersonCount, int maxPersonCount )
    {
        if ( minPersonCount <= 0 || maxPersonCount <= 0 || maxPersonCount < minPersonCount )
        {
            throw new ArgumentOutOfRangeException( "Invalid person count range." );
        }
    }

    public static void InvalidAvailableRooms( int availableRooms, string nameOfValue )
    {
        if ( availableRooms < 1 )
        {
            throw new ArgumentOutOfRangeException( $"{nameOfValue} must be grater than 1" );
        }
    }
}
