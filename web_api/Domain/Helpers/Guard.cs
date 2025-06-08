namespace Domain.Helpers;

public class Guard
{
    public static void AgainstNullOrEmpty( string value, string nameOfValue )
    {
        if ( string.IsNullOrEmpty( value ) )
        {
            throw new ArgumentException( $"'{nameOfValue}' can't be null or empty", nameOfValue );
        }
    }

    public static void AgainstEmptyGuid( Guid guid, string nameOfValue )
    {
        if ( guid == Guid.Empty )
        {
            throw new ArgumentNullException( $"{nameOfValue} can't be empty", nameOfValue );
        }
    }

    public static void AgainstInvalidDateRange( DateOnly arrivalDate, DateOnly departureDate, TimeOnly arrivalTime, TimeOnly departureTime )
    {
        if ( arrivalDate.ToDateTime( arrivalTime ) >= departureDate.ToDateTime( departureTime ) )
        {
            throw new ArgumentOutOfRangeException( "Departure date and time must be after arrival date and time" );
        }
    }

    public static void AgainstInvalidDailyPrice( decimal dailyPrice, string nameOfValue )
    {
        if ( dailyPrice <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"'{nameOfValue} must be grater than 0'" );
        }
    }

    public static void AgainstInvalidPersonCount( int minPersonCount, int maxPersonCount )
    {
        if ( minPersonCount <= 0 || maxPersonCount <= 0 || maxPersonCount < minPersonCount )
        {
            throw new ArgumentOutOfRangeException( "Invalid person count range." );
        }
    }

    public static void AgainstInvalidAvailableRooms( int availableRooms, string nameOfValue )
    {
        if ( availableRooms < 1 )
        {
            throw new ArgumentOutOfRangeException( $"{nameOfValue} must be grater than 1" );
        }
    }
}
