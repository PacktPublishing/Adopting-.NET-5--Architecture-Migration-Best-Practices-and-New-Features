using Autofac;
using BookApp.DAL;
using BookApp.DAL.DI;
using Serilog;
using System;
using System.Windows;

namespace AdminDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IContainer container;

        public App()
        {
                InitialiseDI();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalExceptionHandler);
        }

        private void InitialiseDI()
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            builder.RegisterModule(new BookAppDBModule());

            container = builder.Build();
        }

     
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Log.Error(e.Message); 
        }
    }
}
