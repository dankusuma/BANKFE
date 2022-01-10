using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BANKFE.Views.Registration
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IHostEnvironment _environment;

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public Index(ILogger<Index> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                return;
            }

            Console.WriteLine(UploadedFile.FileName);
            Console.WriteLine(UploadedFile.Length);
            Console.WriteLine(UploadedFile.Name);
            Console.WriteLine(UploadedFile.ContentType);
            Console.WriteLine(UploadedFile.ContentDisposition);
            Console.WriteLine(UploadedFile.Headers);

            _logger.LogInformation($"Uploading {UploadedFile.FileName}.");
            string targetFileName = $"{_environment.ContentRootPath}/{UploadedFile.FileName}";
            string folderLocation = @"D:\Quiz\.Net Bootcamp\Self Exercise\TestingUpload\TestingUpload\";

            string fileName = UploadedFile.FileName;
            targetFileName = folderLocation + fileName;
            using (var stream = new FileStream(targetFileName, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(stream);
            }
        }
    }
}
