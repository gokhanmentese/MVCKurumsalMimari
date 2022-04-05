using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if ((files !=null && files.Count!=0))
            {
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (file != null)
                    {
                        if (!_extensions.Contains(extension.ToLower()))
                        {
                            return new ValidationResult(GetErrorMessage());
                        }
                    }
                }
            }
           

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Sadece doc, docx, xls, xlsx, ppt, pdf, jpg, png, rar türündeki  dosyalar yüklenebilir.";
        }
    }
}
