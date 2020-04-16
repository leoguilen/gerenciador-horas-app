using ManagerHours.Implementation;
using ManagerHours.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerHours.Dependencies
{
    public class DateServiceDependencies
    {
        private DateServiceDependencies() { }

        public static IDateService Inject()
        {
            return InjectDependencies
                .Inject(typeof(IDateService), typeof(DateService))
                .GetRequiredService<IDateService>();
        }
    }
}
