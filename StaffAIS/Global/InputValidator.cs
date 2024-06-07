namespace StaffAIS.Global;

public class InputValidator
{
    public static bool ValidateNumberInput(string input)
    {
        if (input == "" ) return false;
        
        foreach (var c in input)
        {
            if (!char.IsDigit(c)) return false;
        }
       
        if (Convert.ToInt32(input)==0) return false;
        
        return true;
    }
}