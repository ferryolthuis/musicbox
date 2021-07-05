using AutoMapper;
using System;

namespace MusicBox.API.Mapping
{
    internal class MillisecondsToTimeSpanConverter : ITypeConverter<int, TimeSpan>
    {
        public TimeSpan Convert(int source, TimeSpan destination, ResolutionContext context)
        {
            return TimeSpan.FromMilliseconds(source);
        }
    }
}
