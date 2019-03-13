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
using System.Threading.Tasks;

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
                    if (password?.Length != 0)
                        password = password?.Remove(password.Length - 1);
                }
                else
                    password += key.KeyChar;
            }

            Console.Write(Environment.NewLine);
            
            Api myStat = new Api(username, password);
            myStat.TryLogin(false);


            try
            {
                myStat.UploadHomeworkFile(@"C:\Users\l.listopadov\Desktop\new1.cs");
                Task.WaitAll();
            } catch(Exception e)
            {
                Logger.Log(e.Message, ConsoleColor.Gray);
            }
            Task.WaitAll();
            Console.ReadKey();
            //myStat.DownloadHomeworkFile(myStat.Homeworks[1]);

            //TEST ENV
            //myStat.LoadHomeworks();
            //for (int i = 0; i < 3; i++)
            //{
            //    foreach (var prop in typeof(HomeworkEntity).GetProperties())
            //    {
            //        Console.WriteLine(prop.Name + " => " + prop.GetValue(myStat.Homeworks[i]));
            //    }
            //}

            //TEST GETTING DAILY
            //if (!myStat.CollectDailyPoints())
            //{
            //    Logger.Log("Getting daily points isn't possible. Sleeping 3sec...", ConsoleColor.DarkCyan);
            //    Thread.Sleep(3000);
            //}

            //TEST DOWNLOADING HOMEWORK FILE

            //using (var client = new WebClient())
            //{
            //    client.DownloadFile(linkHw[0].file_path.ToString(), linkHw[0].filename.ToString());
            //}
        }
    }
}
