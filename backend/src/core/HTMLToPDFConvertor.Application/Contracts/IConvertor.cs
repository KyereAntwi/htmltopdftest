using Microsoft.AspNetCore.Http;

namespace HTMLToPDFConvertor.Application.Contracts;

public interface IConvertor
{
    Task<byte[]> ConvertHtmlToPdfAsync(IFormFile htmlFile);
}