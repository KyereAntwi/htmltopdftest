using System.Text;
using FluentAssertions;
using HTMLToPDFConvertor.Application.Features.Convertor.Commands.ConvertHtmlToPdf;
using HTMLToPDFConvertor.Application.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HTMLToPDFConvertor.Application.Tests.Features.Convertor.Commands;

public class ConvertHtmlToPdfTests
{
    private readonly MockHelper _mockHelper = new MockHelper();

    [Fact]
    public async Task Handle_ShouldConvertHtmlFileToPdf_ReturnsPdfByteArray()
    {
        var expectedPdfBytes = Encoding.UTF8.GetBytes("MockPDFContent");
        _mockHelper.SetupMockConvertorToReturn(expectedPdfBytes);

        var mockConvertor = _mockHelper.MockConvertor;
        var handler = new ConvertHtmlToPdfCommandHandler(mockConvertor.Object);

        var mockHtmlFile = _mockHelper.CreateMockFile(
            fileName: "test.html",
            content: "<html><body><h1>Test</h1></body></html>"
        );

        var command = new ConvertHtmlToPdfCommand(mockHtmlFile);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedPdfBytes);
        mockConvertor.Verify(c => c.ConvertHtmlToPdfAsync(mockHtmlFile), Times.Once);
    }
    
    [Fact]
    public async Task Handle_WhenHtmlFileIsNull_ShouldThrowArgumentException()
    {
        
        var handler = new ConvertHtmlToPdfCommandHandler(_mockHelper.MockConvertor.Object);

        var command = new ConvertHtmlToPdfCommand(null!);
        
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("HTML file is empty");
        _mockHelper.MockConvertor.Verify(c => c.ConvertHtmlToPdfAsync(It.IsAny<IFormFile>()), Times.Never);
    }
    
    [Fact]
    public async Task Handle_WhenFileIsWrongType_ShouldThrowArgumentException()
    {
        var handler = new ConvertHtmlToPdfCommandHandler(_mockHelper.MockConvertor.Object);

        var invalidHtmlFile = _mockHelper.CreateMockFile(
            fileName: "test.pdf",
            content: "Some content",
            contentType: "application/pdf"
        );

        var command = new ConvertHtmlToPdfCommand(invalidHtmlFile);

        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("HTML file is not a valid HTML file");
        _mockHelper.MockConvertor.Verify(c => c.ConvertHtmlToPdfAsync(It.IsAny<IFormFile>()), Times.Never);
    }
}