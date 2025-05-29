using System.Runtime.CompilerServices;

namespace Fighters.Tests.ConsoleMethods;

public class ConsoleFixture : IDisposable
{
    public StringReader StringReader { get; private set; }
    public StringWriter StringWriter { get; }
    private readonly TextReader _originalIn;
    private readonly TextWriter _originalOut;

    public ConsoleFixture()
    {
        _originalIn = Console.In;
        _originalOut = Console.Out;
        StringWriter = new StringWriter();
        Console.SetOut( StringWriter );
    }

    public void SetInput( string input )
    {
        StringReader = new StringReader( input );
        Console.SetIn( StringReader );
    }

    public void SetOut()
    {
        Console.SetOut( StringWriter );
    }

    public void Dispose()
    {
        Console.SetIn( _originalIn );
        Console.SetOut( _originalOut );
        StringReader?.Dispose();
        StringWriter?.Dispose();
    }
}
