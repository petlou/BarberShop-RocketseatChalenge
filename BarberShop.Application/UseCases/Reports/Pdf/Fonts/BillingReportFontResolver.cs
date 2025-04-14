using System.Reflection;
using PdfSharp.Fonts;

namespace BarberShop.Application.UseCases.Reports.Pdf.Fonts;
public class BillingReportFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        if (stream is null)
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);

        var length = (int)stream!.Length;

        var fontData = new byte[length];

        stream.Read(buffer: fontData, offset: 0, count: length);

        return fontData;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var path = $"BarberShop.Application.UseCases.Reports.Pdf.Fonts.{faceName}.ttf";

        return assembly.GetManifestResourceStream(path);
    }
}
