using AutoMapper;
using Sodimac.Orders.Application.DTOs;
using Sodimac.Orders.Domain.Entities;

namespace Sodimac.Orders.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();

            CreateMap<CreatePedidoDto, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaPedido, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.MontoTotal, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.RutaEntrega, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos));

            CreateMap<CreatePedidoProductoDto, Producto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PedidoId, opt => opt.Ignore())
                .ForMember(dest => dest.Pedido, opt => opt.Ignore())
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => Guid.NewGuid().ToString().Substring(0, 8)))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Producto))
                .ForMember(dest => dest.Descripcion, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true));


            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nombre : null))
                .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos))
                .ForMember(dest => dest.RutaEntrega, opt => opt.MapFrom(src => src.RutaEntrega))
                .ReverseMap()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos));

            CreateMap<PedidoDto, Pedido>()
                .ForMember(dest => dest.FechaPedido, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.RutaEntrega, opt => opt.Ignore())
                .ForMember(dest => dest.Productos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nombre : null));

            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
                .ReverseMap()
                .ForMember(dest => dest.Pedido, opt => opt.Ignore());


            CreateMap<DeliveryRoute, CreatePedidoDeliveryRouteDto>()
                .ForMember(dest => dest.DeliveryStatusId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NombreRuta, opt => opt.MapFrom(src => src.NombreRuta))
                .ReverseMap()
                .ForMember(dest => dest.Estado, opt => opt.Ignore()) 
                .ForMember(dest => dest.Pedido, opt => opt.Ignore());
            
            CreateMap<CreatePedidoDeliveryRouteDto, DeliveryRoute>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PedidoId, opt => opt.Ignore())
                .ForMember(dest => dest.FechaAsignacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FechaEntregaReal, opt => opt.Ignore())
                .ForMember(dest => dest.Pedido, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore());


            CreateMap<DeliveryRoute, DeliveryRouteDto>()
                .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.Estado != null ? src.Estado.Nombre : "Sin Estado"))
                .ReverseMap()
                .ForMember(dest => dest.Estado, opt => opt.Ignore()) 
                .ForMember(dest => dest.Pedido, opt => opt.Ignore());


            CreateMap<DeliveryStatus, DeliveryStatusDto>().ReverseMap();
        }
    }
}
