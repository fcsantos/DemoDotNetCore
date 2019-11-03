using AutoMapper;
using testeEFCore.Business.Models;
using testeEFCore.ViewModels;

namespace testeEFCore.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {            
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<FornecedorViewModel, Fornecedor>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome))
                .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor.Documento));

            CreateMap<Fornecedor, FornecedorViewModel>();
        }
    }
}
