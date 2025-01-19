using HTMLToPDFConvertor.Application.Features.Convertor.Commands.ConvertHtmlToPdf;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HTMLToPDFConvertor.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class HtmlToPdfController(ISender sender) : ControllerBase
{
    [HttpPost("convert", Name = "Convert")]
    public async Task<IActionResult> Convert([FromForm] IFormFile htmlFile)
    {
        try
        {
            var pdfBytes = await sender
                .Send(new ConvertHtmlToPdfCommand(htmlFile));
            
            return File(pdfBytes, "application/pdf", "file.pdf");
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}