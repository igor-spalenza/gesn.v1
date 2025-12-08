using gesn.webApp.Models.Enums.Global;
using System.ComponentModel;
using System.Reflection;

namespace gesn.webApp.Infrastructure.Helpers
{
    public class EnumHelper
    {/// <summary>
     /// Converte enum para string
     /// </summary>
        public static string ToStringValue<T>(T enumValue) where T : Enum
        {
            return enumValue.ToString();
        }

        /// <summary>
        /// Converte string para enum
        /// </summary>
        public static T FromString<T>(string value) where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                return default;

            if (Enum.TryParse(value, true, out T result))
                return result;

            return default;
        }

        /// <summary>
        /// Converte enum para int (para ObjectState)
        /// </summary>
        public static int ToIntValue<T>(T enumValue) where T : Enum
        {
            return Convert.ToInt32(enumValue);
        }

        /// <summary>
        /// Converte int para enum (para ObjectState)
        /// </summary>
        public static T FromInt<T>(int value) where T : struct, Enum
        {
            if (Enum.IsDefined(typeof(T), value))
                return (T)Enum.ToObject(typeof(T), value);

            return default;
        }

        /// <summary>
        /// Obtém a descrição do enum (se houver atributo Description)
        /// </summary>
        public static string GetDescription<T>(T enumValue) where T : Enum
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? enumValue.ToString();
        }

        /// <summary>
        /// Obtém todos os valores de um enum como lista
        /// </summary>
        public static List<T> GetAllValues<T>() where T : struct, Enum
        {
            return Enum.GetValues<T>().ToList();
        }

        /// <summary>
        /// Obtém todos os valores de um enum como dicionário (valor, descrição)
        /// </summary>
        public static Dictionary<T, string> GetValueDescriptionMap<T>() where T : struct, Enum
        {
            var result = new Dictionary<T, string>();
            foreach (T value in Enum.GetValues<T>())
            {
                result[value] = GetDescription(value);
            }
            return result;
        }

        /// <summary>
        /// Verifica se um valor string é válido para um enum
        /// </summary>
        public static bool IsValidEnumValue<T>(string value) where T : struct, Enum
        {
            return Enum.TryParse<T>(value, true, out _);
        }

        /// <summary>
        /// Converte ObjectState para bool (Active = true, Inactive = false)
        /// </summary>
        public static bool ObjectStateToBool(EObjectState state)
        {
            return state == EObjectState.ACTIVE;
        }

        /// <summary>
        /// Converte bool para ObjectState (true = Active, false = Inactive)
        /// </summary>
        public static EObjectState BoolToObjectState(bool isActive)
        {
            return isActive ? EObjectState.ACTIVE : EObjectState.INACTIVE;
        }
    }
}
