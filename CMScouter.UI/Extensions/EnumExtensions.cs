using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CMScouter.UI
{
    public static class EnumExtensions
    {
        public static string ToName(this Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute, string>(x => x.Description);
            return attribute == null ? value.ToString() : attribute;
        }

        private static Expected GetAttribute<T, Expected>(this Enum enumeration, Func<T, Expected> expression) where T : Attribute
        {
            var attributeInfo = enumeration
                .GetType()
                .GetMember(enumeration.ToString())
                .Where(member => member.MemberType == MemberTypes.Field)
                .FirstOrDefault();

            if (attributeInfo == null)
            {
                return default(Expected);
            }

            T attribute =
              attributeInfo
                .GetCustomAttributes(typeof(T), false)
                .Cast<T>()
                .SingleOrDefault();

            if (attribute == null)
            {
                return default(Expected);
            }

            return expression(attribute);
        }
    }
}
