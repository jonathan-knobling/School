namespace RandomShit.LeetCode;

public static class ValidateParantheses
{
    public static bool IsValid(string s)
    {
        var stack = new Stack<char>();
        foreach (char cur in s)
        {
            if (!stack.TryPeek(out char last) || IsOpenBracket(cur))
            {
                stack.Push(cur);
                continue;
            }
    
            if (!AreMatching(last, cur)) return false;
            
            stack.Pop();
        }
    
        return stack.Count.Equals(0);
    }
    
    public static bool IsOpenBracket(char c) => c switch
        {
            '(' => true,
            '{' => true,
            '[' => true,
            _ => false
        };
    
    public static bool AreMatching(char open, char closed) => open switch
        {
            '(' => closed == ')',
            '{' => closed == '}',
            '[' => closed == ']',
            _ => false
        };
}