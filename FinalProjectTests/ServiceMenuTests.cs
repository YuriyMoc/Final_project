using AutoServiceApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectTests
{
    public class ServiceMenuTests
    {
        [Theory]
        [MemberData(nameof(GetAllServices))]
        public void AllServices_ShouldHavePositivePrice(Service service)
        {
            Assert.True(service.Price > 0, $"Послуга '{service.Name}' має неприпустиму ціну: {service.Price}");
        }

        public static IEnumerable<object[]> GetAllServices()
        {
            foreach (Service s in ServiceMenu.EngineServices)
                yield return new object[] { s };

            foreach (Service s in ServiceMenu.MaintenanceServices)
                yield return new object[] { s };

            foreach (Service s in ServiceMenu.TransmishionServices)
                yield return new object[] { s };
        }
    }
}
