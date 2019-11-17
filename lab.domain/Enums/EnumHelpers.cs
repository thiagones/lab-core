using System;

namespace lab.domain.Enums
{
    public static class EnumHelpers
    {
        public static string GetName<T>(this T e) where T : struct, Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.GetName(typeof(T), e); ;
        }

        public static T ToEnum<T>(this string val) where T : struct, Enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            if (!Enum.TryParse(val, true, out T result))
            {
                throw new ArgumentException("Val must be valid");
            }

            return result;
        }
    }
}