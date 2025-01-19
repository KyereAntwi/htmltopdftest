using HTMLToPDFConvertor.Application.Contracts;
using MediatR;

namespace HTMLToPDFConvertor.Application.Features.Convertor.Commands.ConvertHtmlToPdf;

public class ConvertHtmlToPdfCommandHandler(
    IConvertor convertor) 
    : IRequestHandler<ConvertHtmlToPdfCommand, byte[]>
{
    public async Task<byte[]> Handle(ConvertHtmlToPdfCommand command, CancellationToken cancellationToken)
    {
        if(command.HtmlFile == null || command.HtmlFile.Length == 0)
        {
            throw new ArgumentException("HTML file is empty");
        }

        if (command.HtmlFile.ContentType != "text/html" 
            && command.HtmlFile.ContentType != "application/xhtml+xml")
        {
            throw new ArgumentException("HTML file is not a valid HTML file");
        }
        
        return await convertor.ConvertHtmlToPdfAsync(command.HtmlFile);
    }
}