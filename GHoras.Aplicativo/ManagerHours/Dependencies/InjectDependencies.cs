using Microsoft.Extensions.DependencyInjection;
using System;

namespace ManagerHours.Dependencies
{
    public class InjectDependencies
    {
        private InjectDependencies() { }

        /// <summary>
        /// Inject dependencies between interfaces and your implementations
        /// To use this dependencies in services
        /// </summary>
        /// <param name="interfacee">A type that receive a interface that should be implemented</param>
        /// <param name="implementation">A type that implements the type receive in T</param>

        public static ServiceProvider Inject(Type interfacee, Type implementation)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped(interfacee, implementation)
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
