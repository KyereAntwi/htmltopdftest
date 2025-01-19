using System.Text;
using HTMLToPDFConvertor.Application.Contracts;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HTMLToPDFConvertor.Application.Tests.Mocks;

public class MockHelper
{
    public Mock<IConvertor> MockConvertor { get; private set; } = new();

    public void SetupMockConvertorToReturn(byte[] returnValue)
    {
        MockConvertor
            .Setup(c => c.ConvertHtmlToPdfAsync(It.IsAny<IFormFile>()))
            .ReturnsAsync(returnValue);
    }
    
    public IFormFile CreateMockFile(string fileName, string content, string contentType = "text/html")
    {
        var mockFile = new Mock<IFormFile>();
        var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        mockFile.Setup(f => f.FileName).Returns(fileName);
        mockFile.Setup(f => f.OpenReadStream()).Returns(fileStream);
        mockFile.Setup(f => f.ContentType).Returns(contentType);
        mockFile.Setup(f => f.Length).Returns(fileStream.Length);

        return mockFile.Object;
    }
}