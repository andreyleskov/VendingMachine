using System;

public class CantFindGradeValueException : Exception
{
    public CantFindGradeValueException(CoinGrade grade):base("�� ������� ����� ��������� ��� ������ " + grade)
    {
        
    }
}