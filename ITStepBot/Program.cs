using System;
using Telegram.Bot;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using MyStatAPI;
using System.Threading;

namespace ITStepBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Graphics.ShowHello();

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = null;
            while (true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0 || password != null)
                        password = password.Remove(password.Length - 1);
                }
                else
                    password += key.KeyChar;
            }
            Console.Write(Environment.NewLine);

            using (StreamWriter sw = new StreamWriter("dyach.txt"))
            {
                sw.WriteLine(username);
                sw.WriteLine(password);
            }
            
            Api myStat = new Api(username, password);
            myStat.TryLogin();

            Console.WriteLine(myStat.GetUserInfo());
            Console.WriteLine(myStat.GetDailyPoints());
            Thread.Sleep(3000);
            Console.WriteLine(myStat.GetLatestNews());
            myStat.GetUserActivities(); //works
            myStat.GetGroupInfo(); //works
        }
    }
}
