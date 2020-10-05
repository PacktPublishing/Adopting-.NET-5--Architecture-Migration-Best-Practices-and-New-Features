using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DAL.DI
{
    

    public static class BookAppDBDIModule
    {
        
        public static IServiceCollection AddBookAppDB(this IServiceCollection services,string sqlConnection)
        {

            var dbOptions = new DbContextOptionsBuilder<BooksDBContext>();
            dbOptions.UseSqlServer(sqlConnection);
            services.AddSingleton(x => dbOptions.Options);
            services.AddScoped<IBookRepository, BookRepository>();

            return services;

        }
    }
}
