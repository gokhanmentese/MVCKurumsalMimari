using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class DownloadManager : IDownloadService
    {
        private readonly ITaskFileService _taskFileService;
        private readonly IFileService _fileService;

        public DownloadManager(ITaskFileService taskFileService, IFileService fileService)
        {
            _taskFileService = taskFileService;
            _fileService = fileService;
        }

        public IDataResult<DownloadFile> FileDownload(Guid taskfileid)
        {
            var taskFile = _taskFileService.GetById(taskfileid);
            if (taskFile == null)
                return new  ErrorDataResult<DownloadFile>(Messages.FileNotFound);

            var file = _fileService.GetById(taskFile.FileId);
            if (file == null)
                return new ErrorDataResult<DownloadFile>(Messages.FileNotFound);

            var path = file.FilePath; //Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot//taskfiles//tasks//test.txt");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                 stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();

            Entities.Concrete.DownloadFile downloadFile = new DownloadFile
            {
                MemoryStream = memory,
                ContentType = file.MimeType,
                DownloadName = Path.GetFileName(path)
            };

            return new SuccessDataResult<Entities.Concrete.DownloadFile>(downloadFile);
        }

  
    }
}
