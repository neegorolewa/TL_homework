namespace MyDictionary;

public class MyDictionary
{
    private const char TranslateSeparator = ':';
    private static readonly string TranslationNotFound = string.Empty;
    private readonly Dictionary<string, string> _dictionary;
    private readonly string DictFilePath;

    public MyDictionary( string filePath )
    {
        DictFilePath = filePath;
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
            string translate = parts[ 1 ].Trim();

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

        foreach ( (string key, string value) in _dictionary )
        {
            file.WriteLine( $"{key}{TranslateSeparator}{value}" );
        }
        file.Close();
    }
}
