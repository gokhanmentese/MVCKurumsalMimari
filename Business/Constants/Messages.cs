using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string TaskAdded = "Görev başarıyla eklenildi";

        public static string TaskUpdated = "Görev başarıyla güncellendi";

        public static string TaskDeleted = "Görev başarıyla silindi";

        public static string TaskNotFound = "Görev bulunamadı";

        public static string FileNotFound = "Dosya bulunamadı";

        public static string AuthorizationDenied = "Yetkiniz yok";

        public static string UserNotFound = "Kullanıcı bulunamadı";

        public static string TaskTitleAlreadyExists = "Görev başlığı zaten var.";

        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";
        public static string UserPasswordReset = "Kullanıcı şifresi başarıyla resetlendi";
        public static string UserEmailError = "Sistemdeki kayıtlı email adresi ile uyuşmuyor";

        public static string FileAdded = "Dosya ekleme başarılı";

        public static string FileIdNotFound = "Dosya id değeri bulunamadı";

        public static string FileIsNotValid = @"Sadece doc, docx, xls, xlsx, ppt, pdf, jpg, png, rar türündeki  dosyalar yüklenebilir."+ Environment.NewLine+
                                               ".rar dosya maksimum boyutu 50 mb,diğer türdeki dosya boyutları maksimum 25 mb büyüklükte olmalıdır.";

        public static string TaskAddUserNotValid = "Geçerli veriler girilmedi";

    }
}
