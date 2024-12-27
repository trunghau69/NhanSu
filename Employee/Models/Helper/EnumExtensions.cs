using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Employee.Helpers // Hoặc namespace tương ứng trong dự án của bạn
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute != null ? attribute.Name : value.ToString();
        }
    }
}