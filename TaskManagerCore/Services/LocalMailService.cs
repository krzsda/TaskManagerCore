using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCore.Models.Interfaces;

namespace TaskManagerCore.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public LocalMailService(IConfiguration config)
        {
            _configuration = config
                             ?? throw new ArgumentNullException(nameof(config));
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_configuration["mailSettings:mailFromAddress"]} to {_configuration["mailSettings:mailToAddress"]}, with {this.GetType()}");
            Console.WriteLine($"Subject: {subject}.");
            Console.WriteLine($"Message: {message}.");
        }
    }
}
