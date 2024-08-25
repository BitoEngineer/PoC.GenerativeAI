﻿using System.ComponentModel;
using System.Text;

namespace PoC.GenerativeAI.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static string BuildDtoDescription(this Type type, string propertyPrefix = "\t")
        {
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var sb = new StringBuilder();
            sb.Append("\n");
            sb.Append(RemoveFirstTab(propertyPrefix));
            sb.Append("{\n");

            foreach (var property in properties)
            {
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute));
                if (attribute == null)
                {
                    continue;
                }

                string propertyName = property.Name;
                string description = property.PropertyType.Name;

                sb.Append(propertyPrefix);
                sb.Append($"\"{property.Name}\": ");
                Type enumType;
                if ((enumType = property.PropertyType).IsEnum
                    || (property.PropertyType.IsArray && (enumType = property.PropertyType.GetElementType()).IsEnum))
                {
                    var enumValues = Enum.GetValues(enumType);
                    string enumValuesString = string.Join(", ", enumValues.Cast<Enum>().Select(e => e.ToString() + "= " + Convert.ChangeType(e, e.GetTypeCode())));
                    description += $"Possible values: {enumValuesString}";
                }
                else if (!property.PropertyType.IsArray && property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    description += property.PropertyType.BuildDtoDescription(propertyPrefix + "\t");
                }
                else if (property.PropertyType.IsArray && property.PropertyType.GetElementType().IsClass && property.PropertyType.GetElementType() != typeof(string))
                {
                    description += $" [ \n {property.PropertyType.GetElementType().BuildDtoDescription(propertyPrefix + "\t")} \n]";
                }

                description += " //" + attribute.Description;
                sb.Append($"{description},\n");
            }

            // Remove trailing comma and newline
            if (sb.Length > 2)
            {
                sb.Length -= 2;
            }

            sb.Append("\n");
            sb.Append(RemoveFirstTab(propertyPrefix));
            sb.Append("}");

            return sb.ToString();
        }

        private static string RemoveFirstTab(string input)
        {
            int index = input.IndexOf("\t");

            if (index >= 0)
            {
                input = input.Remove(index, 1);
            }

            return input;
        }
    }
}