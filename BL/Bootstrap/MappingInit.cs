using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.AdventureTypes;
using BL.DTOs.Buildings;
using BL.DTOs.BuildingTypes;
using BL.DTOs.UserAccount;
using BL.DTOs.Players;
using BL.DTOs.Products;
using BL.DTOs.ProductTypes;
using BL.DTOs.Resources;
using BL.DTOs.ResourceTypes;
using BL.DTOs.Units;
using BL.DTOs.UnitTypes;
using BL.DTOs.Village;
using DAL.Entities;

namespace BL.Bootstrap
{
    public static class MappingInit
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Adventure, AdventureDTO>()
                .ForMember(dest => dest.BreadPerUnit, opts => opts.MapFrom(src => src.AdventureType.BreadPerUnit))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.AdventureType.Name))
                .ForMember(dest => dest.EnemyCount, opts => opts.MapFrom(src => src.AdventureType.NumberOfEnemies))
                .ForMember(dest => dest.Enemy, opts => opts.MapFrom(src => src.AdventureType.Enemy.Name))
                .ForMember(dest => dest.ResourcesReward, opts => opts.MapFrom(src => src.AdventureType.ResourcesReward))
                .ForMember(dest => dest.ProductsReward, opts => opts.MapFrom(src => src.AdventureType.ProductsReward))

                .ReverseMap();

                config.CreateMap<AdventureType, AdventureTypeDTO>()
                .ReverseMap();

                config.CreateMap<Building, BuildingDTO>()
                .ForMember(dest => dest.BuildingType, opts => opts.MapFrom(src => src.BuildingType.Name))
                .ForMember(dest => dest.ResourceType, opts => opts.MapFrom(src => src.BuildingType.ResourceType.Name))
                .ForMember(dest => dest.Cost, opts => opts.MapFrom(src => src.BuildingType.Cost))
                .ForMember(dest => dest.ProductionPerWorker, opts => opts.MapFrom(src => src.BuildingType.ProdPerWorker))
                .ForMember(dest => dest.Production, opts => opts.MapFrom(src => src.BuildingType.ProdPerWorker * src.WorkersAssigned))
                .ReverseMap();

                config.CreateMap<BuildingType, BuildingTypeDTO>().ReverseMap();

                config.CreateMap<Player, PlayerDTO>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Account.Email))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Account.Username))
                .ReverseMap();

                config.CreateMap<UserAccount, UserAccountDTO>().ReverseMap();

                config.CreateMap<UserRegistrationDTO, UserAccount>();

                config.CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.ProductType, opts => opts.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.Cost, opts => opts.MapFrom(src => src.ProductType.Cost))
                .ForMember(dest => dest.BuildingType, opts => opts.MapFrom(src => src.ProductType.BuildingType.Name))
                .ReverseMap();

                config.CreateMap<ProductType, ProductTypeDTO>().ReverseMap();

                config.CreateMap<Resource, ResourceDTO>()
                .ForMember(dest => dest.ResourceType, opts => opts.MapFrom(src => src.ResourceType.Name))
                .ReverseMap();

                config.CreateMap<ResourceType, ResourceTypeDTO>().ReverseMap();

                config.CreateMap<Unit, UnitDTO>()
                .ForMember(dest => dest.UnitType, opts => opts.MapFrom(src => src.UnitType.Name))
                .ForMember(dest => dest.Cost, opts => opts.MapFrom(src => src.UnitType.Cost))
                .ForMember(dest => dest.Damage, opts => opts.MapFrom(src => src.UnitType.Damage))
                .ForMember(dest => dest.HP, opts => opts.MapFrom(src => src.UnitType.HP))
                .ReverseMap(); 

        
                config.CreateMap<UnitType, UnitTypeDTO>().ReverseMap();
                config.CreateMap<Village, VillageDTO>().ReverseMap();
            });
        }
    }
}
