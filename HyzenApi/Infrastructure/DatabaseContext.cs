using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using HyzenApi.Models;

namespace HyzenApi.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        private static AsyncLocal<DatabaseContext> _instance = new();
        private static Dictionary<string, DatabaseContext> _dicInstance = new();
        private static readonly object Lock = new();

        public static DatabaseContext Get()
        {
            if (_instance.Value == null)
            {
                lock (Lock)
                {
                    if (_instance.Value == null)
                        _instance.Value = new DatabaseContext();
                }
            }
            return _instance.Value;
        }

        public static DatabaseContext Get(string key)
        {
            lock (Lock)
                _dicInstance.TryAdd(key, new DatabaseContext());
            
            return _dicInstance[key];
        }

        public DbSet<BaseModel> BaseSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            lock (Lock)
            {
                if (optionsBuilder.IsConfigured)
                    return;

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets<Program>()
                    .Build();

                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 28)));
            }
        }
    }
}