namespace Application.Core
{
    public static class Extensions
    {
        public static bool ContainsAll(this string value, string[] wordList)
        {
            foreach (string word in wordList)
            {
                if (!value.Contains(word))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}