using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class Util 
{
    static Random _random = new Random();
   public  static string[] RandomizeStrings(string[] arr)
    {
        List<KeyValuePair<int, string>> list =
            new List<KeyValuePair<int, string>>();
        // Add all strings from array.
        // ... Add new random int each time.
        foreach (string s in arr)
        {
            list.Add(new KeyValuePair<int, string>(_random.Next(), s));
        }
        // Sort the list by the random number.
        var sorted = from item in list
                     orderby item.Key
                     select item;
        // Allocate new string array.
        string[] result = new string[arr.Length];
        // Copy values to array.
        int index = 0;
        foreach (KeyValuePair<int, string> pair in sorted)
        {
            result[index] = pair.Value;
            index++;
        }
        // Return copied array.
        return result;
    }
   
   public static Regex sUserNameAllowedRegEx = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$", RegexOptions.Compiled);
   public static bool IsUserNameAllowed(string userName)
   {
       if (string.IsNullOrEmpty(userName)
           || !sUserNameAllowedRegEx.IsMatch(userName)
       )
       {
           return false;
       }
       return true;
   }
   public static bool IsValidEmail(string emailaddress)
   {
       if (emailaddress == "") return false;

       try
       {
           MailAddress m = new MailAddress(emailaddress);
           return true;
       }
       catch (FormatException)
       {
           return false;
       }
   }
}
