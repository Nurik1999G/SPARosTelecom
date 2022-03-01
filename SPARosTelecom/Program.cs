using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using SPARosTelecom.Controllers;

namespace SPARosTelecom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Находим путь к базе данных
            DirectoryInfo directory = new DirectoryInfo("Files\\DataBase\\DataBase.mdf");
            string dir = directory.FullName;
            HomeController.connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"{dir}\";Integrated Security=True;Connect Timeout=30";
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
