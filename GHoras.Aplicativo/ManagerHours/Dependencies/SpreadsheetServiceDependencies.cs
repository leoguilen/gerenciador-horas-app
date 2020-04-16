using ManagerHours.Implementation;
using ManagerHours.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ManagerHours.Dependencies
{
    public class SpreadsheetServiceDependencies
    {
        private SpreadsheetServiceDependencies() { }

        public static ISpreadsheetService Inject()
        {
            return InjectDependencies
                .Inject(typeof(ISpreadsheetService), typeof(SpreadsheetService))
                .GetRequiredService<ISpreadsheetService>();
        }
    }
}
