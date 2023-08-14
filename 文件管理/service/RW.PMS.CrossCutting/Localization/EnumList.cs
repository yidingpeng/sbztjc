using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Localization.Base;

namespace RW.PMS.CrossCutting.Localization
{
    public class EnumList : IEnumList
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public EnumList(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        public IList<EnumItem> GetList<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ApplicationException();
            }

            var stringLocalizer = _stringLocalizerFactory.Create(typeof(T));
            return Enum.GetValues(typeof(T)).Cast<T>().Select(item => new EnumItem
                {Text = stringLocalizer[item.ToString() ?? string.Empty].Value, Value = item.To<int>(), OriginalText = item.ToString()}).ToList();
        }
    }
}