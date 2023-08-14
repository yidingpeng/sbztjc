using System;
using AutoMapper;
using Microsoft.Extensions.Localization;

namespace RW.PMS.CrossCutting.Localization.AutoMapper
{
    public class StringTypeConverter : ITypeConverter<Enum, string>
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public StringTypeConverter(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public string Convert(Enum source, string destination, ResolutionContext context)
        {
            if (!Enum.IsDefined(source.GetType(), source)) return string.Empty;
            var stringLocalizer = _stringLocalizerFactory.Create(source.GetType());
            return stringLocalizer[source.ToString()];
        }
    }
}