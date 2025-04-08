public class Program
{
    public static void Main( string[] args )
    {
        Console.Write( "Введите название товара: " );
        string productName = GetStringValue();
        Console.Write( "Введите количество товара: " );
        int quantityProduct = GetQuantityValue();
        Console.Write( "Введите ваше имя: " );
        string userName = GetStringValue();
        Console.Write( "Введите адрес доставки: " );
        string deliveryAddress = GetStringValue();

        bool isCorrectOrder = IsCorrectOrder( productName, quantityProduct, userName, deliveryAddress );
        WriteOutMessage( isCorrectOrder, productName, quantityProduct, userName, deliveryAddress );
    }

    //переименовал функцию 
    static void WriteOutMessage( bool isCorrectOrder, string product, int quantity, string user, string address )
    {
        if ( isCorrectOrder )
        {
            DateTime delivery_date = DateTime.Now.AddDays( 3 );
            Console.WriteLine( $"{user}! Ваш заказ {product} в количестве {quantity} оформлен! Ожидайте доставку по адресу {address} к {delivery_date}" );
        }
        else
        {
            Console.WriteLine( "Вероятно это наша ошибка... Попробуйте собрать заказ заново!" );
        }
    }

    static bool IsCorrectOrder( string product, int quantity, string user, string address )
    {
        while ( true )
        {
            Console.Write( $"Здравствуйте, {user}, вы заказали {quantity} {product} на адрес {address}, все верно? (y/n): " );
            string input = Console.ReadLine();

            if ( string.IsNullOrEmpty( input ) )
            {
                Console.WriteLine( "Введите значение ['y' - да, 'n' - нет]. " );
                continue;
            }

            //добавил обработку на введную команду отличную от ['y', 'n']
            if ( input == "n" )
            {
                return false;
            }
            else if ( input == "y" )
            {
                return true;
            }
            else
            {
                Console.WriteLine( "Неверно введенная команда. Введите значение ['y' - да, 'n' - нет]." );
                continue;
            }
        }
    }
    static int GetQuantityValue()
    {
        while ( true )
        {
            //переместил инициализацию внутрь while
            string input = Console.ReadLine();

            if ( string.IsNullOrEmpty( input ) )
            {
                Console.Write( "Поле не может быть пустым. Пожалуйста введите значение: " );
                continue;
            }

            if ( !int.TryParse( input, out int value ) )
            {
                Console.Write( "Неккоректный формат числа. Пожалуйста, введите целое значение: " );
                continue;
            }

            if ( value <= 0 )
            {
                Console.Write( "Чтобы заказ был оформлен, количество товара должно быть больше 0. Введите корректное количество: " );
                continue;
            }

            return value;
        }
    }

    static string GetStringValue()
    {
        while ( true )
        {
            string value = Console.ReadLine();

            if ( string.IsNullOrEmpty( value ) )
            {
                Console.Write( "Поле не может быть пустым. Пожалуйста введите значение: " );
                continue;
            }

            return value;
        }
    }
}