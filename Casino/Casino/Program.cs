internal class Program
{
    private static int Main( string[] args )
    {
        double multiplicator = 1.5;

        Console.WriteLine( "Казино" );
        Console.Write( "Внесите стартовый баланс: " );

        double userBalance = Convert.ToDouble( Console.ReadLine() );

        while ( true )
        {
            Console.Clear();
            Console.WriteLine( $"Ваш баланс: {userBalance}" );

            Console.Write( "Ваша ставка: " );
            double bet = Convert.ToDouble( Console.ReadLine() );

            userBalance -= bet;

            Random random = new Random();
            int randomNumber = random.Next( 1, 21 );

            if ( randomNumber >= 18 )
            {
                double profit = ( double )( bet * ( 1 + multiplicator * ( randomNumber % 17 ) ) );
                userBalance += profit;

                Console.WriteLine( $"Вы выиграли. Ваш выигрыш: {profit}" );
                Console.Write( "Продолжить играть? (y/n): " );
                var play = Console.ReadLine();

                if ( NextPlay( play ) )
                {
                    continue;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                Console.Write( $"Вы проиграли {bet}...\nХотите продолжить?(y/n)" );
                var play = Console.ReadLine();

                if ( NextPlay( play ) )
                {
                    continue;
                }
                else
                {
                    return 0;
                }
            }
        }

        bool NextPlay( string result )
        {
            if ( result == "n" )
            {
                Console.WriteLine( $"Ваш баланс: {userBalance}, до скорых встреч!" );
                return false;
            }
            return true;
        }
    }
}