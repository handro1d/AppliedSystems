using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AppliedSystems.Common
{
    public static class EnumExtensions
    {
        public static string Description(this Enum enumValue)
        {
            var descriptions =
                (DescriptionAttribute[])enumValue.GetType().GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptions.Any() ? descriptions[0].Description : enumValue.ToString();
        }

        public static bool EnumEquals(this Enum enumValue, string value)
        {
            if (enumValue.Description().Equals(value, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            try
            {
                Type enumType = enumValue.GetType();
                return Enum.GetNames(enumType).Contains(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static TEnum Parse<TEnum>(string value)
        {
            Type type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return (TEnum) fieldInfo.GetValue(null);
                    }
                }
                else
                {
                    if (fieldInfo.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return (TEnum) fieldInfo.GetValue(null);
                    }
                }
            }

            return default(TEnum);
        }
    }
}