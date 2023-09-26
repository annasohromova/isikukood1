using System.Reflection.Emit;

public class Program
{
    public static void Main()
    {
    
            Console.WriteLine("Sisestage kasutaja isikood:");
            string customIdCode = Console.ReadLine();

        IdCode customId = new IdCode(customIdCode);

        int fullYear = customId.GetFullYear();
        DateOnly birthDate = customId.GetBirthDate();
        string birthplace = IdCode.GetBirthplaceFromIdCode(customIdCode);
        bool isValid = customId.IsValid();
        List<string> NaisedMehed = IdCode.NaisedMehed(new List<string> { customIdCode });

        Console.WriteLine($"Isikukood: {customIdCode}");
            Console.WriteLine($"Sünniaasta: {fullYear}");
            Console.WriteLine($"Sünnikuupäev: {birthDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"Sünnikoht: {birthplace}");
            Console.WriteLine($"Kehtivus: {isValid}");
            Console.WriteLine($"Sugu: {string.Join(", ", NaisedMehed)}");
            Console.WriteLine();
    
    }
}