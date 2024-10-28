using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication71.Services
{
    /// <summary>
    /// Klasa przekształca byte[] na IFormFile
    /// </summary>
    public static class FileHelper
    {
        public static IFormFile TransformToIFormFile(byte[] bytes, string fileName, string contentType)
        {
            return new FormFileFromBytes(bytes, fileName, contentType);
        }

        private class FormFileFromBytes : IFormFile
        {
            private readonly byte[] _fileContents;

            public FormFileFromBytes(byte[] fileContents, string fileName, string contentType)
            {
                _fileContents = fileContents;
                FileName = fileName;
                ContentType = contentType;
                Length = fileContents.Length;
            }

            public string ContentType { get; }
            public string FileName { get; }
            public string Name => "file"; // Nazwa pliku w formularzu
            public long Length { get; }

            public string ContentDisposition => throw new System.NotImplementedException();

            public IHeaderDictionary Headers => throw new System.NotImplementedException();

            public async Task CopyToAsync(Stream target)
            {
                await target.WriteAsync(_fileContents, 0, _fileContents.Length);
            }

            public Stream OpenReadStream()
            {
                return new MemoryStream(_fileContents);
            }

            public void CopyTo(Stream target)
            {
                target.Write(_fileContents, 0, _fileContents.Length);
            }

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
