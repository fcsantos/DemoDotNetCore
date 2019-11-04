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

            CreateMap<ProdutoViewModel, Produto>()
                .ForMember(x => x.DataCadastro, opt => opt.Ignore())
                .ForMember(x => x.DataAtualizacao, opt => opt.Ignore());

            CreateMap<FornecedorViewModel, Fornecedor>();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome))
                .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor.Documento))
                .ForMember(dest => dest.DataAtualizacaoFormat, opt => opt.MapFrom(src => src.DataAtualizacao.HasValue ? src.DataAtualizacao.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty));

            CreateMap<Fornecedor, FornecedorViewModel>()
                .ForMember(dest => dest.NomeTipoFornecedor, opt => opt.MapFrom(src => src.TipoFornecedor));
        }
    }
}
