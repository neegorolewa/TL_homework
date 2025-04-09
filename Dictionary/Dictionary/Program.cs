namespace Dictionary;

public class Program
{
    const string GetTranslationCommand = "1";
    const string AddTranslationCommand = "2";
    const string ClearScreenCommand = "3";
    const string ExitCommand = "4";

    private static void Main( string[] args )
    {
        Dictionary dictionary = new Dictionary();
        RunProgramm( dictionary );
    }

    public static void PrintMenu()
    {
        Console.WriteLine( "Выберите действие: \n" +
           $"({GetTranslationCommand}) Перевести слово\n" +
           $"({AddTranslationCommand}) Добавить перевод\n" +
           $"({ClearScreenCommand}) Убрать лишнее с экрана\n" +
           $"({ExitCommand}) Выйти" );
    }

    public static void RunProgramm( Dictionary dict )
    {
        while ( true )
        {

            PrintMenu();
            Console.Write( "Введите команду: " );

            var command = Console.ReadLine();

            switch ( command )
            {
                case GetTranslationCommand:
                    GetTranslationFromDictionary( dict );
                    break;

                case AddTranslationCommand:
                    AddTranslationToDictionary( dict );
                    break;

                case ClearScreenCommand:
                    Console.Clear();
                    break;

                case ExitCommand:
                    dict.SaveDictionaryToFile();
                    return;

                default:
                    Console.WriteLine( "Неверная команда!" );
                    break;
            }
        }
    }

    public static void GetTranslationFromDictionary( Dictionary dict )
    {
        Console.Write( "Введите слово для перевода: " );
        string inputWord = GetStringValue();

        string translate = dict.GetTranslation( inputWord );

        if ( translate.Length > 0 )
        {
            Console.WriteLine( $"{inputWord} - {translate}" );
        }
        else
        {
            Console.Write( "Перевод остутствует. Хотите добавить перевод? (у/n): " );
            while ( true )
            {
                string answer = Console.ReadLine();
                if ( answer == "y" )
                {
                    AddTranslationToDictionary( dict, inputWord );
                    return;
                }
                else if ( answer == "n" )
                {
                    return;
                }
                else
                {
                    Console.Write( "Введите верную команду (y/n): " );
                }
            }
        }
    }

    public static void AddTranslationToDictionary( Dictionary dict, string? word = null )
    {
        if ( word == null )
        {
            Console.Write( "Введите слово для перевода: " );
            word = GetStringValue();
        }

        Console.Write( $"Введите перевод для '{word}': " );
        string inputTranslate = GetStringValue();
        dict.AddTranslation( word, inputTranslate );

        return;
    }

    public static string GetStringValue()
    {
        while ( true )
        {
            string value = Console.ReadLine();

            if ( string.IsNullOrEmpty( value ) )
            {
                Console.Write( "Пожалуйста, введите значение: " );
                continue;
            }

            return value;
        }
    }
}