using System;

#nullable disable
namespace MiscellaneousJSON.Helpers;

public static class EnumHelpers
{
    public static bool TryParse<T>(string str, out T enumValue) where T : Enum
    {
        try
        {    
            enumValue = (T)Enum.Parse(typeof(T), str);
        }
        catch(Exception)
        {
            enumValue = default(T);
            return false;
        } 
        return true;
    }
}
