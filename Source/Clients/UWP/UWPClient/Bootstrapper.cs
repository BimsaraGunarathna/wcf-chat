﻿using System;
using Windows.UI.Xaml;
using Andead.Chat.Client.Uwp.Services;
using Andead.Chat.Client.Uwp.Wcf;
using Andead.Chat.Common.Logging;
using Andead.Chat.Common.Utilities;
using Autofac;

namespace Andead.Chat.Client.Uwp
{
    public class Bootstrapper : IServiceClientFactory
    {
        private readonly IContainer _container;
        private IDisposable _currentClient;

        public Bootstrapper()
        {
            var builder = new ContainerBuilder();

            Handle.Logger = new MetroLogger();
            builder.RegisterInstance(Handle.Logger).As<ILogger>();

            builder.RegisterInstance(this).As<IServiceClientFactory>();

            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<ChatViewModel>().SingleInstance();

            _container = builder.Build();
        }

        public static Bootstrapper Instance { get; } = Application.Current.Resources["Bootstrapper"] as Bootstrapper;

        public LoginViewModel LoginViewModel => _container.Resolve<LoginViewModel>();

        public IServiceClient GetServiceClient()
        {
            var client = new ServiceClient();

            if (_currentClient != null)
            {
                _currentClient.Dispose();
                _currentClient = client;
            }

            return client;
        }
    }
}