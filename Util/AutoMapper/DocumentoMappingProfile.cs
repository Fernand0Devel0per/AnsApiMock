using AutoMapper;
using MockAbiANS.DTOs.Peticao;
using MockAbiANS.Entities;
using MockAbiANS.Util.Extension;

namespace MockAbiANS.Util.AutoMapper
{
    public class DocumentoMappingProfile : Profile
    {
        public DocumentoMappingProfile()
        {
            CreateMap<Documento, DocumentoResponse>()
                .ForMember(dest => dest.DataDocumento, opt => opt.MapFrom(src => src.DataDocumento.ToLocalTimeFromUtc()))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro.ToLocalTimeFromUtc()))
                .ForMember(dest => dest.DataAtualizacao, opt => opt.MapFrom(src => src.DataAtualizacao.ToLocalTimeFromUtc()))
                .ReverseMap();

            CreateMap<TipoDocumento, TipoDocumentoResponse>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.DataCadastro.ToLocalTimeFromUtc()))
                .ReverseMap();

            CreateMap<Arquivo, ArquivoResponse>()
                .ForMember(dest => dest.Link, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
