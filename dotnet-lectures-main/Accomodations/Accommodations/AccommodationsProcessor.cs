using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    //fixed property name s_commandIndex --> _commandIndex
    private static int _commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        //fixed nullable string
        string? input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    //fixed nullable string
    private static void ProcessCommand( string? input )
    {
        //fixed nullable string
        if ( string.IsNullOrEmpty( input ) )
        {
            return;
        }

        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];

        switch ( commandName )
        {
            case "book":
                if ( parts.Length != 6 )
                {
                    //throw ArumentException instead Console.WriteLine
                    throw new ArgumentException( "Invalid number of arguments for booking." );
                }

                if ( !int.TryParse( parts[ 1 ], out int userId ) )
                {
                    //throw ArumentException for UserId
                    throw new ArgumentException( "Invalid value of UserId. Use number" );
                }

                if ( !DateTime.TryParse( parts[ 3 ], out DateTime startDate ) )
                {
                    //throw ArumentException for StartDate
                    throw new ArgumentException( "Invalid start date. Expected format: dd/mm/yyyy. Use existing date." );
                }

                if ( !DateTime.TryParse( parts[ 4 ], out DateTime endDate ) )
                {
                    //throw ArumentException for StartDate
                    throw new ArgumentException( "Invalid end date. Expected format: dd/mm/yyyy. Use existing date." );
                }

                if ( !Enum.TryParse( parts[ 5 ], true, out CurrencyDto currency ) )
                {
                    //throw ArumentException for currency
                    throw new ArgumentException( $"Invalid currency value. Expected format: Expected one of: {string.Join( ", ", Enum.GetNames( typeof( CurrencyDto ) ) )}." );
                }

                BookingDto bookingDto = new()
                {
                    UserId = userId,
                    Category = parts[ 2 ],
                    StartDate = startDate,
                    EndDate = endDate,
                    Currency = currency,
                };

                BookCommand bookCommand = new( _bookingService, bookingDto );
                bookCommand.Execute();
                _executedCommands.Add( ++_commandIndex, bookCommand );
                Console.WriteLine( "Booking command run is successful." );
                break;

            case "cancel":
                if ( parts.Length != 2 )
                {
                    //add ArgumentException
                    throw new ArgumentException( "Invalid number of arguments for canceling. Expected format: cancel <BookingId>" );
                }

                if ( !Guid.TryParse( parts[ 1 ], out Guid bookingId ) )
                {
                    //add ArgumentException
                    throw new ArgumentException( "Invalid booking id." );
                }

                CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
                cancelCommand.Execute();
                _executedCommands.Add( ++_commandIndex, cancelCommand );
                Console.WriteLine( "Cancellation command run is successful." );
                break;

            case "undo":
                //throw Exception if history is empty
                if ( _executedCommands.Count == 0 )
                {
                    throw new InvalidOperationException( "History of booking is empty" );
                }
                _executedCommands[ _commandIndex ].Undo();
                _executedCommands.Remove( _commandIndex );
                _commandIndex--;
                Console.WriteLine( "Last command undone." );

                break;
            case "find":
                if ( parts.Length != 2 )
                {
                    //add throw ArgumentException
                    throw new ArgumentException( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
                }

                if ( !Guid.TryParse( parts[ 1 ], out Guid id ) )
                {
                    //add throw ArgumentException for guid id
                    throw new ArgumentException( "Invalid booking id." );
                }

                FindBookingByIdCommand findCommand = new( _bookingService, id );
                findCommand.Execute();
                break;

            case "search":
                if ( parts.Length != 4 )
                {
                    //add throw ArgumentException
                    throw new ArgumentException( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'" );
                }

                if ( !DateTime.TryParse( parts[ 1 ], out startDate ) )
                {
                    //throw ArumentException for StartDate
                    throw new ArgumentException( "Invalid start date. Expected format: dd/mm/yyyy. Use existing date." );
                }

                if ( !DateTime.TryParse( parts[ 2 ], out endDate ) )
                {
                    //throw ArumentException for StartDate
                    throw new ArgumentException( "Invalid start date. Expected format: dd/mm/yyyy. Use existing date." );
                }

                string categoryName = parts[ 3 ];
                SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
                searchCommand.Execute();
                break;

            default:
                //add throw ArgumentException
                throw new ArgumentException( "Unknown command." );
        }
    }
}
