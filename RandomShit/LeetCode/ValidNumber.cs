namespace RandomShit.LeetCode;

public class ValidNumber
{

    private bool _containsE;
    private bool _containsDot;
    private int _eIndex = -1;
    private int _dotIndex = -1;

    public bool IsNumber(string s)
    {
        _containsDot = false;
        _containsE = false;
        _eIndex = -1;
        _dotIndex = -1;
        
        var num = s.AsSpan();
        if (!ValidateCharacters(num)) return false;
        if (_containsE)
        {
            if (_dotIndex > _eIndex) return false;
            
            var beforeE = num.Slice(0, _eIndex);
            var afterE = num.Slice(_eIndex + 1);

            return IsNumberNoE(beforeE) && IsInteger(afterE, true);
        }

        return IsNumberNoE(num);
    }

    private bool IsNumberNoE(ReadOnlySpan<char> num)
    {
        return _containsDot ? IsDecimal(num) : IsInteger(num, true);
    }

    private bool IsInteger(ReadOnlySpan<char> num, bool mayHaveSign)
    {
        if (num.Length == 0) return false;

        int startIndex = 0;
        if (mayHaveSign && (num[0] == '+' || num[0] == '-'))
        {
            startIndex = 1;
        }
        else if (!char.IsDigit(num[0]))
        {
            return false;
        }

        if (startIndex >= num.Length) return false;

        for (int i = startIndex; i < num.Length; i++)
        {
            if (!char.IsDigit(num[i])) return false;
        }

        return true;
    }


    private bool IsDecimal(ReadOnlySpan<char> num)
    {
        if (num.Length < 2) return false;
        
        var leftPart = num.Slice(0, _dotIndex);
        var rightPart = num.Slice(_dotIndex + 1);

        if (leftPart.Length == 0 && rightPart.Length == 0)
        {
            return false;
        }

        bool leftValid = leftPart.Length == 0 || IsInteger(leftPart, true);
        bool rightValid = rightPart.Length == 0 || IsInteger(rightPart, false);

        return (leftValid && rightValid) || IsIntegerOrSign(leftPart, true) && IsInteger(rightPart, false);

    }

    private bool ValidateCharacters(ReadOnlySpan<char> s)
    {
        for (var index = 0; index < s.Length; index++)
        {
            char c = s[index];
            if (char.IsDigit(c)) continue;
            switch (c)
            {
                case 'e' or 'E' when _eIndex == -1:
                    _eIndex = index;
                    _containsE = true;
                    continue;
                case '.' when _dotIndex == -1:
                    _dotIndex = index;
                    _containsDot = true;
                    continue;
                case '+' or '-':
                    continue;
            }
            return false;
        }
        return true;
    }

    private bool IsIntegerOrSign(ReadOnlySpan<char> num, bool intMayHaveSign)
    {
        if (num.Length == 0) return false;
        if (num.Length > 1) return IsInteger(num, intMayHaveSign);
        return (num[0] == '+' || num[0] == '-') || char.IsDigit(num[0]);
    }
}