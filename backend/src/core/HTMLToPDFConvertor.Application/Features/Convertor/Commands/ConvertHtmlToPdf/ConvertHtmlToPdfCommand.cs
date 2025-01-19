using MediatR;
using Microsoft.AspNetCore.Http;

namespace HTMLToPDFConvertor.Application.Features.Convertor.Commands.ConvertHtmlToPdf;

public record ConvertHtmlToPdfCommand(IFormFile HtmlFile) : IRequest<byte[]>;