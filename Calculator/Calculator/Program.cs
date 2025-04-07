
Calculator calculator = new Calculator();
calculator.Run();

public class Calculator
{
    public int firstOperand;
    public int secondOperand;
    public char operation;
    public int result;

    public void Run()
    {
        Console.Write( "Введите выражение: " );
        string expression = Console.ReadLine();
        Calculation( expression );
    }

    public void Calculation( string expression )
    {
        if ( !ParseExpression( expression ) )
            return;
        if ( SelectOperation( operation ) )
        {
            PrintResult();
        }
    }

    public bool Addition( int firstOperand, int secondOperand )
    {
        if ( secondOperand > 0 && firstOperand > int.MaxValue - secondOperand ||
            secondOperand < 0 && firstOperand < int.MinValue - secondOperand )
        {
            ErrorMessage( "Переполнение" );
            return false;
        }

        this.result = firstOperand + secondOperand;
        return true;
    }

    public bool Subtraction( int firstOperand, int secondOperand )
    {
        if ( secondOperand < 0 && firstOperand > int.MaxValue + secondOperand ||
            secondOperand > 0 && firstOperand < int.MinValue + secondOperand )
        {
            ErrorMessage( "Переполнение" );
            return false;
        }

        this.result = firstOperand - secondOperand;
        return true;
    }

    public bool Multiplication( int firstOperand, int secondOperand )
    {
        if ( firstOperand != 0 &&
            ( ( secondOperand > 0 && ( firstOperand > int.MaxValue / secondOperand || firstOperand < int.MinValue / secondOperand ) ) ||
            ( secondOperand < 0 && ( firstOperand < int.MaxValue / secondOperand || firstOperand > int.MinValue / secondOperand ) ) ) )
        {
            ErrorMessage( "Переполнение" );
            return false;
        }

        this.result = firstOperand * secondOperand;
        return true;
    }

    public bool Division( int firstOperand, int secondOperand )
    {
        if ( secondOperand == 0 )
        {
            ErrorMessage( "Деление на ноль невозможно" );
            return false;
        }

        if ( firstOperand == int.MinValue && secondOperand == -1 )
        {
            ErrorMessage( "Переполнение" );
            return false;
        }

        this.result = firstOperand / secondOperand;
        return true;
    }

    public bool SelectOperation( char op )
    {
        switch ( op )
        {
            case '+':
                return Addition( firstOperand, secondOperand );

            case '-':
                return Subtraction( firstOperand, secondOperand );

            case '*':
                return Multiplication( firstOperand, secondOperand );

            case '/':
                return Division( firstOperand, secondOperand );

            default:
                ErrorMessage( "Такой операции не существует" );
                return false;
        }
    }

    public bool ParseExpression( string expression )
    {
        expression = expression.Replace( " ", "" );
        int operationIndex = -1;
        char[] operations = { '+', '-', '/', '*' };

        for ( int i = expression.Length - 1; i >= 0; i-- )
        {
            if ( operations.Contains( expression[ i ] ) )
            {
                if ( i > 0 && operations.Contains( expression[ i - 1 ] ) && expression[ i ] == '-' )
                    continue;

                operationIndex = i;
                break;
            }
        }

        if ( operationIndex == -1 )
        {
            ErrorMessage( "Неверный формат выражения *{int}{+-*/}{int}*" );
            return false;
        }

        string firstOp = expression.Substring( 0, operationIndex );
        string secondOp = expression.Substring( operationIndex + 1 );
        this.operation = expression[ operationIndex ];

        if ( !int.TryParse( firstOp, out this.firstOperand ) )
        {
            ErrorMessage( "Неверный формат первого операнда *{int}*" );
            return false;
        }

        if ( !int.TryParse( secondOp, out this.secondOperand ) )
        {
            ErrorMessage( "Неверный формат второго операнда *{int}*" );
            return false;
        }

        return true;
    }
    public void PrintResult()
    {
        Console.WriteLine( $"Результат: {result}" );
    }

    public void ErrorMessage( string message )
    {
        Console.WriteLine( $"{message}" );
        return;
    }

    public string GetOperand()
    {
        return $"{firstOperand} {secondOperand}";
    }

}

