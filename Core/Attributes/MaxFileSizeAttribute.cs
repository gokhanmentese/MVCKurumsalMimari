using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace Core.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if (files !=null && files.Count !=0)
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    //var allowedExtensions = new[] { ".jpg", ".png" };`enter code here`
                    if (file != null)
                    {
                        if (extension != ".rar")
                        {
                            if (file.Length > _maxFileSize)
                                return new ValidationResult(GetErrorMessage());
                        }
                        else
                        {
                            if (file.Length > 50 * 1024 * 1024)
                                return new ValidationResult(GetErrorMessage());
                        }

                    }
                } 
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $".rar dosya maksimum boyutu 50 mb,diğer türdeki dosya boyutları maksimum {_maxFileSize} mb büyüklükte olmalıdır.";
        }
    }
}
