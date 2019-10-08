using System;
using Microsoft.AspNetCore.Http;

namespace BoardGameDating.api.Helpers
{
    public static class Extensions
    {
        // adds application error details in the response header
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime dob)
        {
            int age = DateTime.Today.Year - dob.Year;
            // if date after adding the age > current date, (s)he is not that age yet, so 1 less
            if(dob.AddYears(age) > DateTime.Today) age --;
            return age;
        }
    }
}