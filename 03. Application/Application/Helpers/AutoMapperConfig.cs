using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using Transversal.Dto;

namespace Application.Helpers
{
    public static class AutoMapperConfig
    {
        private static IMapper _mapper;
        private static readonly object Padlock = new object();

        /// <summary>
        /// Obtiene la instancia Singleton de IMapper.
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                // Manejo de inicialización tardía y Thread-safe
                if (_mapper == null)
                {
                    throw new InvalidOperationException("AutoMapper debe ser inicializado con AutoMapperConfig.Initialize() durante el arranque de la aplicación.");
                }
                return _mapper;
            }
        }
        /// <summary>
        /// Centraliza la configuración de TODOS los mapeos del proyecto.
        /// Debe llamarse UNA SOLA VEZ (ej. en Program.cs o Startup.cs).
        /// </summary>
        public static void Initialize()
        {
            lock (Padlock)
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        // ** 1. Configuración de Modelos Principales **
                        cfg.CreateMap<Property, PropertyDto>().ReverseMap();

                        // ** 2. Configuración de Modelos Anidados (Sub-Entidades) **
                        cfg.CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
                        cfg.CreateMap<PropertyTrace, PropertyTraceDto>().ReverseMap();

                    });

                    _mapper = config.CreateMapper();
                }
            }
        }
        public static IMapper GetMapper<T1, T2>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public static IMapper MapList<TSource, TDestination>()
        {
            List<TSource> source = new List<TSource>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>().ReverseMap();
            });
            IMapper mapper = config.CreateMapper();
            mapper.Map<List<TDestination>>(source);
            return mapper;
        }

        public static IMapper GetMapper<T1, T2, T3, T4>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<T3, T4>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public static IMapper GetMapper<T1, T2, T3, T4, T5, T6>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<T3, T4>().ReverseMap();
                cfg.CreateMap<T5, T6>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }


        public static IMapper GetMapper<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<T3, T4>().ReverseMap();
                cfg.CreateMap<T5, T6>().ReverseMap();
                cfg.CreateMap<T7, T8>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public static IMapper GetMapper<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ReverseMap();
                cfg.CreateMap<T3, T4>().ReverseMap();
                cfg.CreateMap<T5, T6>().ReverseMap();
                cfg.CreateMap<T7, T8>().ReverseMap();
                cfg.CreateMap<T9, T10>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}