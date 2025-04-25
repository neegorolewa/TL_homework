using System;
using Fighters.Models.Fighters;

namespace Fighters;

public class GameProcessor
{
    private const string AddFighter = "add-fighter";
    private const string Play = "play";
    private const string Exit = "exit";

    private static List<IFighter> _fighters = new List<IFighter>();

    public static void RunGame()
    {
        PrintMenu();

        string? input;
        while ( ( input = Console.ReadLine().ToLower() ) != Exit )
        {
            try
            {
                ProcessCommands( input );
            }
            catch ( SystemException ex )
            {
                Console.WriteLine( ex.Message );
            }
        }
    }

    private static void ProcessCommands( string input )
    {
        if ( string.IsNullOrEmpty( input ) || input != AddFighter && input != Play )
        {
            throw new ArgumentException( "Неизвестная команда" );
        }

        if ( input == AddFighter )
        {
            IFighter fighter = FightersCreator.CreateFighter();
            _fighters.Add( fighter );
        }

        if ( input == Play )
        {
            if ( _fighters.Count < 2 )
            {
                throw new IndexOutOfRangeException( "Количество бойцов должно быть не менее 2-х" );
            }

            GameManager manager = new GameManager();
            IFighter winner = manager.PlayAndGetWinner( _fighters );

            Console.WriteLine( $"{winner.Name} всех уничтожил" );

        }
    }

    private static void PrintMenu()
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
