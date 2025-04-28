using Fighters.Models.Fighters;

namespace Fighters;

public class GameProcessor
{
    private const string AddFighter = "add-fighter";
    private const string Play = "play";
    private const string Exit = "exit";

    private readonly List<IFighter> _fighters = new();

    public void RunGame()
    {
        PrintMenu();

        while ( true )
        {
            try
            {
                ProcessCommands();
            }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.Message );
            }
        }
    }

    private void ProcessCommands()
    {
        string? input = Console.ReadLine()?.ToLower();
        switch ( input )
        {
            case AddFighter:
                IFighter fighter = FightersCreator.CreateFighter();
                _fighters.Add( fighter );
                break;

            case Play:
                PlayGame();
                _fighters.Clear();
                break;

            case Exit:
                Environment.Exit( 0 );
                break;

            default:
                throw new InvalidOperationException( "Неизвестная команда" );
        }
    }

    private void PlayGame()
    {
        if ( _fighters.Count < 2 )
        {
            throw new IndexOutOfRangeException( "Количество бойцов должно быть не менее 2-х" );
        }

        List<IFighter> fightersForGame = new( _fighters );

        GameManager manager = new();
        IFighter winner = manager.PlayAndGetWinner( fightersForGame );

        Console.WriteLine( $"{winner.Name} всех уничтожил" );
    }

    private void PrintMenu()
    {
        Console.WriteLine(
            $"""           
            Введите команду
            {AddFighter} - Добавить нового бойца
            {Play} - Начать битву
            {Exit} - выйти из игры
            """
            );
    }
}
