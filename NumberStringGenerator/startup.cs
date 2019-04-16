using Autofac;
using NumberStringGenerator.Services;
using System;

namespace NumberStringGenerator
{
    class Startup
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleWrapper>().AsImplementedInterfaces();
            builder.RegisterType<ConsoleInputHandler>().AsImplementedInterfaces();
            builder.RegisterType<NumberComponentGenerator>().AsImplementedInterfaces();
            builder.RegisterType<EnglishNumericValuesToStringMapping>().AsSelf().AsImplementedInterfaces();
            builder.RegisterType<EnglishCurrencyStringGenerator>().AsImplementedInterfaces();
            builder.RegisterType<Program>().AsSelf();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var program = scope.Resolve<Program>();
                program.Start();
            }

        }
    }
}
