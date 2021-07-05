using AutoMapper;
using System;

namespace MusicBox.API.Mapping
{
    internal class TimeSpanToMillisecondsConverter : ITypeConverter<TimeSpan, int>
    {
        public int Convert(TimeSpan source, int destination, ResolutionContext context)
        {
            return (int) Math.Floor(source.TotalMilliseconds);
        }
    }
}
