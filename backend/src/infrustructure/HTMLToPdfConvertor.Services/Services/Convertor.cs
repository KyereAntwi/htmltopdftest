using HTMLToPDFConvertor.Application.Contracts;
using Microsoft.AspNetCore.Http;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace HTMLToPdfConvertor.Services.Services;

public class Convertor: IConvertor
{
    public async Task<byte[]> ConvertHtmlToPdfAsync(IFormFile htmlFile)
    {
        using var reader = new StreamReader(htmlFile.OpenReadStream());
        var htmlContent = await reader.ReadToEndAsync();

        // Download chromium browser
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true
        });
        await using var page = await browser.NewPageAsync();
        
        await page.SetContentAsync(htmlContent);
        
        var pdf = await page.PdfDataAsync(new PdfOptions
        {
            Format = PaperFormat.A4
        });

        return pdf.ToArray();
    }
}