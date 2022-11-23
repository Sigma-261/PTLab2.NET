using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTLab2_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTLab2_Final.Test
{
    public static class DependencyInjection
    {
        public static ServiceCollection InitilizeServices()
        {
            var services = new ServiceCollection();
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("electronicsdb").Options;
            services.AddScoped(_ => new ApplicationContext(options));
            return services;
        }
    }
}
