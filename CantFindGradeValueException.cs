using System;

public class CantFindGradeValueException : Exception
{
    public CantFindGradeValueException(CoinGrade grade):base("Ќе удалось найти стоимость дл€ монеты " + grade)
    {
        
    }
}