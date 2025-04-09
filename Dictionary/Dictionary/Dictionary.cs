namespace Dictionary;

public class Dictionary
{
    private const string DictFilePath = "C:\\Users\\Eugene\\TL_practice\\Dictionary\\Dictionary\\dict.txt";
    private const char TranslateSeparator = ':';
    private const string TranslationNotFound = "";

    private readonly Dictionary<string, string> _dictionary;
    public Dictionary()
    {
        _dictionary = new Dictionary<string, string>();

        LoadDictionaryFromFile();
    }

    private void LoadDictionaryFromFile()
    {
        if ( !File.Exists( DictFilePath ) )
            return;

        StreamReader file = new StreamReader( DictFilePath );

        while ( !file.EndOfStream )
        {
            string line = file.ReadLine();
            if ( string.IsNullOrEmpty( line ) )
                continue;

            string[] parts = line.Split( TranslateSeparator );
            if ( parts.Length != 2 )
                continue;

            string word = parts[ 0 ].Trim();
            string translate = parts[ 1 ];

            _dictionary.Add( word, translate );
        }

        file.Close();
    }

    public string GetTranslation( string word )
    {
        if ( _dictionary.TryGetValue( word, out string translation ) )
        {
            return translation;
        }

        return TranslationNotFound;
    }

    public void AddTranslation( string word, string translate )
    {
        if ( _dictionary.ContainsKey( word ) )
        {
            Console.WriteLine( "Перевод с таким словом уже существует" );
            return;
        }

        _dictionary.Add( word, translate );
        return;
    }

    public void SaveDictionaryToFile()
    {
        if ( _dictionary.Count == 0 ) return;

        StreamWriter file = new StreamWriter( DictFilePath );

        foreach ( var pair in _dictionary )
        {
            file.WriteLine( $"{pair.Key}{TranslateSeparator}{pair.Value}" );
        }

        file.Close();
    }
}
