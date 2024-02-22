using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebStore.IoC.Interfaces;

public interface IDependencyInjection
{
        IServiceCollection AddInfrastructure(IServiceCollection services, IConfiguration configuration);
}