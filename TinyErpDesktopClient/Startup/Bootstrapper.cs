using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using TinyErpDesktopClient.ViewModel;

namespace TinyErpDesktopClient.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            // register shared features

            // register events

            // register data providers

            // register view models
            builder.RegisterType<MainWindowModel>().AsSelf();

            // register views
            builder.RegisterType<MainWindow>().AsSelf();

            return builder.Build();
        }
    }
}
