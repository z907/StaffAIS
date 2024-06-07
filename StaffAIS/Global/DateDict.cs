namespace StaffAIS.Global;

public class DateDict
{
    public static Dictionary<int, string> Months =new Dictionary<int, string>()
        {
            {1,"Январь"},
            {2,"Февраль"},
            {3,"Март"},
            {4,"Апрель"},
            {5,"Май"},
            {6,"Июнь"},
            {7,"Июль"},
            {8,"Август"},
            {9,"Сентябрь"},
            {10,"Октябрь"},
            {11,"Ноябрь"},
            {12,"Декабрь"}
        };
   public List<int> YearList = Enumerable.Range((DateTime.Now.Year - 10), 13).ToList();
}