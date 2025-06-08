namespace Fighters.Tests.ConsoleMethods;

public class ConsoleFixture : IDisposable
{
    public StringReader StringReader { get; private set; }
    public StringWriter StringWriter { get; }
    private readonly TextReader _originalIn;
    private readonly TextWriter _originalOut;
    private bool _disposed = false;

    public ConsoleFixture()
    {
        _originalIn = Console.In;
        _originalOut = Console.Out;
        StringWriter = new StringWriter();
        Console.SetOut( StringWriter );
    }

    ~ConsoleFixture()
    {
        Dispose( false );
    }

    public void SetInput( string input )
    {
        StringReader = new StringReader( input );
        Console.SetIn( StringReader );
    }

    public void Dispose()
    {
        Dispose( true );
        GC.SuppressFinalize( this );
    }

    public virtual void Dispose( bool disposing )
    {
        if ( _disposed ) return;

        if ( disposing )
        {
            Console.SetIn( _originalIn );
            Console.SetOut( _originalOut );
            StringReader?.Dispose();
            StringWriter.Dispose();
        }

        _disposed = true;
    }
}
