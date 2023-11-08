using Microsoft.AspNetCore.Http;
using System.Text.Json;
namespace FIT_Project_Manager.Sessionlib;

public static class SessionHandler
{    
    public static string IdAccessKey = "UserID";
    public static string NameAccessKey = "UserName";
    public static void Set(this ISession session, string key, string value)
    {
        SessionExtensions.SetString(session, key, value);
    }

    public static string? Get(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : value;
    }

    public static void ClearSession(this ISession session)
    {
        session.Clear();
    }
    
    public static bool IsLogin(this ISession session)
    {
        if (string.IsNullOrEmpty(session.GetString(IdAccessKey)))
        {
            return false;
        }
        return true;
    }
}