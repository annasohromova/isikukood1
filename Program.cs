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
            string NaisedMehed = customId.NaisedMehed();

        Console.WriteLine($"Isikukood: {customIdCode}");
            Console.WriteLine($"Sünniaasta: {fullYear}");
            Console.WriteLine($"Sünnikuupäev: {birthDate.ToString("dd.MM.yyyy")}");
            Console.WriteLine($"Sünnikoht: {birthplace}");
            Console.WriteLine($"Kehtivus: {isValid}");
            Console.WriteLine($"Sugu: {NaisedMehed}");
        Console.WriteLine();
    


    Console.WriteLine(new IdCode("37605030299").GetFullYear());  // 1976
        Console.WriteLine(new IdCode("50005200009").GetFullYear());  // 2000
        Console.WriteLine(new IdCode("27605030298").GetBirthDate());  // 03.05.1876
        Console.WriteLine(new IdCode("37605030299").GetBirthDate());  // 03.05.1976

        Console.WriteLine(IdCode.GetBirthplaceFromIdCode("50005200009"));

        Console.WriteLine(new IdCode("a").IsValid());  // False
        Console.WriteLine(new IdCode("123").IsValid());  // False
        Console.WriteLine(new IdCode("37605030299").IsValid());  // True
        // 30th February
        Console.WriteLine(new IdCode("37602300299").IsValid());  // False
        Console.WriteLine(new IdCode("52002290299").IsValid());  // False
        Console.WriteLine(new IdCode("50002290231").IsValid());  // True
        Console.WriteLine(new IdCode("30002290231").IsValid());  // False

        // control number 2nd round
        Console.WriteLine(new IdCode("51809170123").IsValid());  // True
        Console.WriteLine(new IdCode("39806302730").IsValid());  // True

        // control number 3rd round
        Console.WriteLine(new IdCode("60102031670").IsValid());  // True
        Console.WriteLine(new IdCode("39106060750").IsValid());  // True
    }
}