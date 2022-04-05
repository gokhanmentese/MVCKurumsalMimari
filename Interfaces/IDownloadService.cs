using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDownloadService
    {
        IDataResult<DownloadFile> FileDownload(Guid taskfileid);
    }
}
