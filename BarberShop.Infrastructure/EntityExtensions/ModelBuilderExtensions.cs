using System.Text;

namespace BarberShop.Infrastructure.EntityExtensions;
internal static class ModelBuilderExtensions
{
    internal static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        var builder = new StringBuilder(text.Length * 2);
        builder.Append(char.ToLowerInvariant(text[0]));

        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
            {
                builder.Append('_');
                builder.Append(char.ToLowerInvariant(text[i]));
            }
            else
            {
                builder.Append(text[i]);
            }
        }

        return builder.ToString();
    }
}
