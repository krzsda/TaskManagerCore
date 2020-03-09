using Microsoft.Extensions.Configuration;
using System;
using TaskManagerCore.Models.Interfaces;

namespace TaskManagerCore.Services
{
    public class CloudMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public CloudMailService(IConfiguration config)
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
