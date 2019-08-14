using System;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace O365_Location_Scaner
{
    class Program
    {

        static void Main(string[] args)
        {
            string temp;
            Console.WriteLine("Microsoft Office365 Account Location");
            Console.WriteLine("by Kevin");
            Console.WriteLine("v0.0.6-beta");
            Console.WriteLine("====================================");
            string[] list = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\account.txt");
            FileStream fs = new FileStream((Directory.GetCurrentDirectory() + "\\result.txt"), FileMode.OpenOrCreate);
            FileStream fs_error = new FileStream((Directory.GetCurrentDirectory() + "\\error.txt"), FileMode.OpenOrCreate);
            StreamWriter sw_error = new StreamWriter(fs_error);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Trim() != string.Empty)
                {
                Start:
                    if (isO365Account(list[i]))
                    {
                        try
                        {
                            string str = GetInfo(list[i]);
                            if (GetJsonNode(str, "IP") == "Null")
                            {
                                temp = list[i] + " MaybeNull?";
                                sw_error.WriteLine(temp);
                                goto FindNullIP;
                            }
                            temp = list[i] + " " + GetJsonNode(str, "IP") + " " + GetJsonNode(str, "Location").Trim().Replace(" ", ",");
                        }
                        catch
                        {
                            //Console.WriteLine("Waiting for thread...");
                            Thread.Sleep(1000);
                            goto Start;
                        }
                    }
                    else
                    {
                        temp = list[i] + " NotO365Account";
                        sw_error.WriteLine(temp);
                    }
                FindNullIP:
                    Console.WriteLine(temp);
                    sw.WriteLine(temp);
                    sw.Flush();
                    sw_error.Flush();
                }
            }
            sw.Close();
            Console.WriteLine("====================================");
            Console.WriteLine("Process finished!");
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }


        public static string GetJsonNode(string json, string node)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(json);
            return jo[node].ToString();
        }

        public static bool isO365Account(string o365account)
        {
            if (o365account.Contains("@") && o365account.Contains(".onmicrosoft.com"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// GetIpInfo
        /// </summary>
        /// <param name="o365Admin">Office365</param>
        /// <returns>数据 Json</returns>
        public static string GetInfo(string o365Admin)
        {
            string api = "https://moeclub.org/lookup?s=" + o365Admin;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage res = httpClient.GetAsync(api).Result;
            if (res.IsSuccessStatusCode)
            {
                Task<string> t = res.Content.ReadAsStringAsync();
                return t.Result;
            }
            else
            {
                return null;
            }
        }
    }
}
