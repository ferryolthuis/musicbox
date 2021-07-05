using AutoMapper;
using MusicBox.API.Mapping;
using MusicBox.API.Resources.Query;
using MusicBox.Model;
using System;

namespace PersonalFinance.API.Mapping
{
    public class QueryProfile : Profile
    {
        public QueryProfile()
        {
            CreateMap<int, TimeSpan>().ConvertUsing<MillisecondsToTimeSpanConverter>();

            CreateMap<Artist, ArtistResource>();
            CreateMap<Song, SongResource>();
        }
    }
}