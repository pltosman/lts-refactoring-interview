using System;
using Microsoft.Extensions.DependencyInjection;

namespace RefactoringTest.ProductService.Infrastructure
{
    public interface IServiceRegistration
    {
        void RegisterServices(IServiceCollection services);
    }
}
