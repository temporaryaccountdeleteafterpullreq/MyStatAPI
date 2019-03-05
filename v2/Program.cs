using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;



namespace v2
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("https://mystat.itstep.org/");
            var cookieContainer = new CookieContainer();

            Console.Write("Login: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                       "PuTgfq2WT3n_7C8zDvvze0OV-rA5oF_SM5-K4meG_cOGPeXXUNZ8QNK3aPrG68m7");
                var homePageResult = client.GetAsync("/");
                homePageResult.Result.EnsureSuccessStatusCode();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("application_key", "6a56a5df2667e65aab73ce76d1dd737f7d1faef9c52e8b8c55ac75f565d8e8a6"),
                    new KeyValuePair<string, string>("id_city", "8"),
                    new KeyValuePair<string, string>("username", $"{username}"),
                    new KeyValuePair<string, string>("password", $"{password}"),
                });

                Console.ReadKey(true);
                var loginResult = client.PostAsync("https://msapi.itstep.org/api/v1/auth/login", content).Result;
                loginResult.EnsureSuccessStatusCode();

                Console.WriteLine();
                Console.WriteLine(loginResult.Content.ReadAsStringAsync().Result);

                Console.ReadKey();
                Console.WriteLine();
                var userInfoResult = client.GetAsync("https://msapi.itstep.org/api/v1/library/quiz/opened-interview");
                Console.WriteLine(userInfoResult.Result);

            }
        }
    }
}