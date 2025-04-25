namespace MyDictionary;

public class Program
{
    const string GetTranslationCommand = "1";
    const string AddTranslationCommand = "2";
    const string ClearScreenCommand = "3";
    const string ExitCommand = "4";
    const string DictFilePath = "C:\\Users\\Eugene\\TL_practice\\Dictionary\\Dictionary\\dict.txt";

    public static void Main( string[] args )
    {
        MyDictionary dictionary = new MyDictionary( DictFilePath );
        RunProgramm( dictionary );
    }

    private static void PrintMenu()
    {
        Console.WriteLine(
                $"""
                Выберите действие:
               ({GetTranslationCommand}) Перевести слово
               ({AddTranslationCommand}) Добавить перевод
               ({ClearScreenCommand}) Убрать лишнее с экрана
               ({ExitCommand}) Выйти
               """
           );
    }

    private static void RunProgramm( MyDictionary dict )
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

    private static void GetTranslationFromDictionary( MyDictionary dict )
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

    private static void AddTranslationToDictionary( MyDictionary dict, string? word = null )
    {
        if ( word == null )
        {
            Console.Write( "Введите слово для перевода: " );
            word = GetStringValue();
        }

        Console.Write( $"Введите перевод для '{word}': " );
        string inputTranslate = GetStringValue();
        dict.AddTranslation( word, inputTranslate );
    }

    private static string GetStringValue()
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