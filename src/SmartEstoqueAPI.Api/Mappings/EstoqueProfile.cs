using AutoMapper;
using SmartEstoqueAPI.Api.Requests;
using SmartEstoqueAPI.Api.Responses;
using SmartEstoqueAPI.Domain.Entities;

namespace SmartEstoqueAPI.Api.Mappings;

public class EstoqueProfile : Profile
{
    public EstoqueProfile()
    {
        CreateMap<Categoria, CategoriaResponse>();
        CreateMap<CategoriaRequest, Categoria>();
        CreateMap<Fornecedor, FornecedorResponse>();
        CreateMap<FornecedorRequest, Fornecedor>();
        CreateMap<ProdutoRequest, Produto>();
        CreateMap<Produto, ProdutoResponse>()
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome))
            .ForMember(dest => dest.Fornecedor, opt => opt.MapFrom(src => src.Fornecedor.RazaoSocial));
        CreateMap<ItemMovimentacaoEstoqueRequest, ItemMovimentacaoEstoque>();
        CreateMap<MovimentacaoEstoqueRequest, MovimentacaoEstoque>();
        CreateMap<ItemMovimentacaoEstoque, ItemMovimentacaoEstoqueResponse>()
            .ForMember(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto.Nome));
        CreateMap<MovimentacaoEstoque, MovimentacaoEstoqueResponse>();
    }
}