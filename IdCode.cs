public class IdCode
{
    private readonly string _idCode;

    public IdCode(string idCode)
    {
        _idCode = idCode;
    }

    private bool IsValidLength()
    {
        return _idCode.Length == 11;
    }

    private bool ContainsOnlyNumbers()
    {
        // return _idCode.All(Char.IsDigit);

        for (int i = 0; i < _idCode.Length; i++)
        {
            if (!Char.IsDigit(_idCode[i]))
            {
                return false;
            }
        }
        return true;
    }

    private int GetGenderNumber()
    {
        return Convert.ToInt32(_idCode.Substring(0, 1));
    }

    private bool IsValidGenderNumber()
    {
        int genderNumber = GetGenderNumber();
        return genderNumber > 0 && genderNumber < 7;
    }

    private int Get2DigitYear()
    {
        return Convert.ToInt32(_idCode.Substring(1, 2));
    }

    public int GetFullYear()
    {
        int genderNumber = GetGenderNumber();
        // 1, 2 => 18xx
        // 3, 4 => 19xx
        // 5, 6 => 20xx
        return 1800 + (genderNumber - 1) / 2 * 100 + Get2DigitYear();
    }

    private int GetMonth()
    {
        return Convert.ToInt32(_idCode.Substring(3, 2));
    }

    private bool IsValidMonth()
    {
        int month = GetMonth();
        return month > 0 && month < 13;
    }

    private static bool IsLeapYear(int year)
    {
        return year % 4 == 0 && year % 100 != 0 || year % 400 == 0;
    }
    private int GetDay()
    {
        return Convert.ToInt32(_idCode.Substring(5, 2));
    }

    private bool IsValidDay()
    {
        int day = GetDay();
        int month = GetMonth();
        int maxDays = 31;
        if (new List<int> { 4, 6, 9, 11 }.Contains(month))
        {
            maxDays = 30;
        }
        if (month == 2)
        {
            if (IsLeapYear(GetFullYear()))
            {
                maxDays = 29;
            }
            else
            {
                maxDays = 28;
            }
        }
        return 0 < day && day <= maxDays;
    }

    private int CalculateControlNumberWithWeights(int[] weights)
    {
        int total = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            total += Convert.ToInt32(_idCode.Substring(i, 1)) * weights[i];
        }
        return total;
    }

    private bool IsValidControlNumber()
    {
        int controlNumber = Convert.ToInt32(_idCode[^1..]);
        int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
        int total = CalculateControlNumberWithWeights(weights);
        if (total % 11 < 10)
        {
            return total % 11 == controlNumber;
        }
        // second round
        int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
        total = CalculateControlNumberWithWeights(weights2);
        if (total % 11 < 10)
        {
            return total % 11 == controlNumber;
        }
        // third round, control number has to be 0
        return controlNumber == 0;
    }

    public bool IsValid()
    {
        return IsValidLength() && ContainsOnlyNumbers()
            && IsValidGenderNumber() && IsValidMonth()
            && IsValidDay()
            && IsValidControlNumber();
    }

    public DateOnly GetBirthDate()
    {
        int day = GetDay();
        int month = GetMonth();
        int year = GetFullYear();
        return new DateOnly(year, month, day);
    }

    public static string GetBirthplaceFromIdCode(string IdCode)
    {
        List<char> IdCodeList = IdCode.ToCharArray().ToList();
        string tahed8910 = string.Join("", IdCodeList.Skip(7).Take(3));
        int t = int.Parse(tahed8910);

        string haigla = "";

        if (t > 1 && t < 10)
        {
            haigla = "Kuressaare Haigla";
        }
        else if (t > 10 && t < 19)
        {
            haigla = "Tartu Ülikooli Naistekliinik, Tartumaa, Tartu";
        }
        else if (t > 20 && t < 220)
        {
            haigla = "Ida-Tallinna Keskhaigla, Pelgulinna sünnitusmaja, Hiiumaa, Keila, Rapla haigla, Loksa haigla";
        }
        else if (t > 220 && t < 270)
        {
            haigla = "Ida-Viru Keskhaigla (Kohtla-Järve, endine Jõhvi)";
        }
        else if (t > 270 && t < 370)
        {
            haigla = "Maarjamõisa Kliinikum (Tartu), Jõgeva Haigla";
        }
        else if (t > 370 && t < 420)
        {
            haigla = "Narva Haigla";
        }
        else if (t > 420 && t < 470)
        {
            haigla = "Pärnu Haigla";
        }
        else if (t > 470 && t < 490)
        {
            haigla = "Pelgulinna Sünnitusmaja (Tallinn), Haapsalu haigla";
        }
        else if (t > 490 && t < 520)
        {
            haigla = "Järvamaa Haigla (Paide)";
        }
        else if (t > 520 && t < 570)
        {
            haigla = "Rakvere, Tapa haigla";
        }
        else if (t > 570 && t < 600)
        {
            haigla = "Valga Haigla";
        }
        else if (t > 600 && t < 650)
        {
            haigla = "Viljandi Haigla";
        }
        else if (t > 650 && t < 700)
        {
            haigla = "Lõuna-Eesti Haigla (Võru), Põlva Haigla";
        }
        else
        {
            haigla = "Valjaspool Eestit";
        }

        return haigla;
    }

    public static List<string> NaisedMehed(List<string> ikoodid)
    {
        List<string> naised = new List<string>();
        List<string> mehed = new List<string>();

        foreach (string kood in ikoodid)
        {
            char[] koodArray = kood.ToCharArray();
            if (int.Parse(koodArray[0].ToString()) % 2 == 0)
            {
                naised.Add(kood);
            }
            else
            {
                mehed.Add(kood);
            }
        }

        naised.AddRange(mehed);

        return naised;
    }




}