using AutoMapper;
using Newtonsoft.Json.Linq;
using Oms.Application.Contracts.Processings;
using Oms.Application.Jobs;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Processings;

namespace Oms.Application
{
    public class OmsApplicationAutoMapperProfile : Profile
    {
        public OmsApplicationAutoMapperProfile()
        {
            CreateMap<BusinessOrder, BusinessOrderDto>()
                .ForMember(dest => dest.MatchedTransportLineName, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.MatchedTransportLineName);
                })
                .ForMember(dest => dest.MatchType, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.MatchType);
                })
                .ForMember(dest => dest.TransportLineResources, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.TransportResources);
                })
                .ForMember(dest => dest.TransportDetails, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.TransportReceipts);
                });
        }
    }

    #region Processing mapping configuration

    public class ProcessingMapperProfile : Profile
    {
        public ProcessingMapperProfile()
        {
            CreateMap<ProcessingJob, ProcessingJobDto>();
            CreateMap<Processing, ProcessingDto>().ReverseMap();
        }
    }

    #endregion

    #region Transport order mapping configration
    public class TransportOrderMapperProfile : Profile
    {
        public TransportOrderMapperProfile()
        {
            CreateMap<TransportOrder, TransportOrderDto>()
                .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.SenderInfo))
                .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => src.ReceiverInfo))
                .ForMember(dest => dest.MatchedTransportLineName, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.MatchedTransportLineName);
                })
                .ForMember(dest => dest.MatchType, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.MatchType);
                })
                .ForMember(dest => dest.TransportLineResources, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.TransportResources);
                })
                .ForMember(dest => dest.TransportDetails, opt =>
                {
                    opt.PreCondition(src => src.MatchedTransportStrategy != null);
                    opt.MapFrom(src => src.MatchedTransportStrategy.TransportReceipts);
                });
        }
    }

    #endregion

    #region Outbound order mapping configuration

    public class OutboundOrderMapperProfile : Profile
    {
        public OutboundOrderMapperProfile()
        {
            CreateMap<OutboundOrder, OutboundOrderDto>();

            CreateMap<CheckoutProduct, CombinedProductDto>();
            CreateMap<OutboundOrder, CombineDetailDto>();
        }
    }

    #endregion

    #region Inbound order mapping configuration
    public class InboundOrderMapperProfile : Profile
    {
        public InboundOrderMapperProfile()
        {
            CreateMap<InboundOrder, InboundOrderDto>();
        }
    }
    #endregion
}
