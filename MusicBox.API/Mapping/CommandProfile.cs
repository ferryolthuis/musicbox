using AutoMapper;
using MusicBox.API.Resources.Command;
using MusicBox.Model;
using System;

namespace MusicBox.API.Mapping
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<TimeSpan, int>().ConvertUsing<TimeSpanToMillisecondsConverter>();
            
            CreateMap<SaveArtistResource, Artist>();
            CreateMap<SaveSongResource, Song>()
                .ForMember(s => s.Artist, options => options.Ignore());
        }
    }
}