using Microsoft.Extensions.DependencyInjection;
using testeEFCore.Business.Intefaces;
using testeEFCore.Business.Notificacoes;
using testeEFCore.Business.Services;
using testeEFCore.Data.Context;
using testeEFCore.Data.Repository;

namespace testeEFCore.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}
