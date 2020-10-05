using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DAL.DI
{
    

    public class BookAppDBModule : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            builder
              .RegisterType<BooksDBContext>().AsSelf()
              .InstancePerLifetimeScope();

            builder.RegisterType<BookRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
