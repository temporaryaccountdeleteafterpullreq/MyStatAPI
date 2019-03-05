using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MyStatAPI
{
    public class Api
    {
        public string ApplicationKey { get; private set; } = "6a56a5df2667e65aab73ce76d1dd737f7d1faef9c52e8b8c55ac75f565d8e8a6";
        private string LoginUrl { get; set; } = @"https://msapi.itstep.org/api/v1/auth/login";
        private string UserInfoUrl { get; set; } = @"https://msapi.itstep.org/api/v1/settings/user-info";
        private string LatestNewsUrl { get; set; } = @"https://msapi.itstep.org/api/v1/news/operations/latest-news";
        private string UserActivitiesUrl { get; set; } = @"https://msapi.itstep.org/api/v1/dashboard/progress/activity";
        private string GroupInfoUrl { get; set; } = @"https://msapi.itstep.org/api/v1/dashboard/progress/leader-group";
        private string DailyPointsUrl { get; set; } = @"https://msapi.itstep.org/api/v1/feedback/students/comment-academy-day";
        public string Username { get; private set; }
        private string Password { get; set; }
        public string AccessToken { get; private set; }

        public Api(string username, string password)
        {
            Username = username;
            Password = password;
        }
        
        public bool TryLogin()
        {
            try
            {
                Logger.Log("Authorization process started.", ConsoleColor.Yellow);
                var baseAddress = new Uri("https://mystat.itstep.org/");
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var homePageResult = client.GetAsync("/");
                    homePageResult.Result.EnsureSuccessStatusCode();
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("application_key", "6a56a5df2667e65aab73ce76d1dd737f7d1faef9c52e8b8c55ac75f565d8e8a6"),
                    new KeyValuePair<string, string>("id_city", "8"),
                    new KeyValuePair<string, string>("username", $"{Username}"),
                    new KeyValuePair<string, string>("password", $"{Password}"),
                });
                    var loginResult = client.PostAsync("https://msapi.itstep.org/api/v1/auth/login", content).Result;
                    loginResult.EnsureSuccessStatusCode();
                    dynamic result = JsonConvert.DeserializeObject(loginResult.Content.ReadAsStringAsync().Result);
                    Logger.Log("Access token reading. DONE.", ConsoleColor.Yellow);
                    AccessToken = result.access_token;
                }
                return true;
            } catch(Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Red);
                return false;
            }
        }

        public string GetUserInfo()
        {
            Logger.Log("Getting user info.", ConsoleColor.Yellow);
            string pageSource;
            WebRequest getRequest = WebRequest.Create(UserInfoUrl);
            getRequest.Method = "GET";
            getRequest.Headers.Add("Accept", "application/json, text/plain, */*");
            getRequest.Headers.Add("Accept-Language", "ru_RU, ru");
            getRequest.Headers.Add("Authorization", $"Bearer {AccessToken}");
            getRequest.Headers.Add("Origin", "https://mystat.itstep.org");
            getRequest.Headers.Add("Referer", "https://mystat.itstep.org/ru/auth/login/index");

            WebResponse getResponse = getRequest.GetResponse();
            using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            {
                pageSource = sr.ReadToEnd();
            }
            Logger.Log("Getting user info DONE.", ConsoleColor.Green);
            return pageSource;
        }

        public string GetLatestNews()
        {
            try
            {
                Logger.Log("Getting latest news.", ConsoleColor.Yellow);
                string data;
                WebRequest getRequest = WebRequest.Create(LatestNewsUrl);
                getRequest.Method = "GET";
                getRequest.ContentType = "application/x-www-form-urlencoded";
                getRequest.Headers.Add("Accept", "application/json, text/plain, */*");
                getRequest.Headers.Add("Accept-Language", "ru_RU, ru");
                getRequest.Headers.Add("Authorization", $"Bearer {AccessToken}");
                getRequest.Headers.Add("Origin", "https://mystat.itstep.org");

                WebResponse getResponse = getRequest.GetResponse();
                using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
                {
                    data = sr.ReadToEnd();
                }
                Logger.Log("Getting latest news DONE.", ConsoleColor.Green);
                return data;
            } catch(Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Red);
                return null;
            }
        }

        public bool GetDailyPoints()
        {
            try
            {
                Random rnd = new Random();
                Logger.Log("Getting daily points started.", ConsoleColor.Yellow);
                WebRequest request = WebRequest.Create(DailyPointsUrl);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.Headers.Add("Accept", "application/json, text/plain, */*");
                request.Headers.Add("Accept-Language", "ru_RU, ru");
                request.Headers.Add("Authorization", $"Bearer {AccessToken}");
                request.Headers.Add("Content-Type", "application/json");
                request.Headers.Add("Origin", "https://mystat.itstep.org");
                request.Headers.Add("Referer", "https://mystat.itstep.org/ru/2U8ftB5OCmgrsK7c76pKE1TXHB_stVr4");
                //TODO: Понять какой ставить evaluation_id
                string payload = "evaluation_id=258600&evaluation_comment=''";
                byte[] bytes = Encoding.ASCII.GetBytes(payload);
                request.ContentLength = bytes.Length;
                using (Stream os = request.GetRequestStream())
                {
                    os.Write(bytes, 0, bytes.Length);
                }
                WebResponse response = request.GetResponse();
                Console.WriteLine(response);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Red);
                return false;
            }
        }

        public string GetUserActivities()
        {
            try
            {
                Logger.Log("Getting user activities.", ConsoleColor.Yellow);
                string data;
                WebRequest getRequest = WebRequest.Create(UserActivitiesUrl);
                getRequest.Method = "GET";
                getRequest.ContentType = "application/x-www-form-urlencoded";
                getRequest.Headers.Add("Accept", "application/json, text/plain, */*");
                getRequest.Headers.Add("Accept-Language", "ru_RU, ru");
                getRequest.Headers.Add("Authorization", $"Bearer {AccessToken}");
                getRequest.Headers.Add("Origin", "https://mystat.itstep.org");

                WebResponse getResponse = getRequest.GetResponse();
                using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
                {
                    data = sr.ReadToEnd();
                }
                Logger.Log("Getting user activities DONE.", ConsoleColor.Green);
                return data;
            } catch(Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Red);
                return null;
            }
        }

        public string GetGroupInfo()
        {
            try
            {
                Logger.Log("Getting group info.", ConsoleColor.Yellow);
                string data;
                WebRequest getRequest = WebRequest.Create(GroupInfoUrl);
                getRequest.Method = "GET";
                getRequest.ContentType = "application/x-www-form-urlencoded";
                getRequest.Headers.Add("Accept", "application/json, text/plain, */*");
                getRequest.Headers.Add("Accept-Language", "ru_RU, ru");
                getRequest.Headers.Add("Authorization", $"Bearer {AccessToken}");
                getRequest.Headers.Add("Origin", "https://mystat.itstep.org");

                WebResponse getResponse = getRequest.GetResponse();
                using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
                {
                    data = sr.ReadToEnd();
                }
                Logger.Log("Getting group info DONE.", ConsoleColor.Green);
                return data;
            } catch (Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Red);
                return null;
            }
        }
    }
}
