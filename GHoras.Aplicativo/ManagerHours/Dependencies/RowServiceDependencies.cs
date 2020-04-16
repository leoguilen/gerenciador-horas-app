using ManagerHours.Implementation;
using ManagerHours.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerHours.Dependencies
{
    public class RowServiceDependencies
    {
        private RowServiceDependencies() { }

        public static IRowService Inject()
        {
            return InjectDependencies
                .Inject(typeof(IRowService), typeof(RowService))
                .GetRequiredService<IRowService>();
        }
    }
}
