using Core.Entities;
using Core.Entities.Concrete;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [Table("UserProfiles")]
    public  class UserProfile : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string Position { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int YearOfBirth { get; set; } = 1900;

        public int? CategoryId { get; set; }

        public int? DirectorshipId { get; set; }

        public int? DepartmentId { get; set; }

        public int? UnitId { get; set; }

        public string btn_Detail_Kullanici()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Detay" + " onclick=\"window.location.href='/User/Detail/?q=" + SecurityExtensions.EncryptText("id=" + this.UserId.ToStringFromGuid()) + "';\"><i class=\"entypo-newspaper\"></i></button>";
        }
        public string btn_Delete_Kullanici()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Sil" + " onclick=\"window.location.href='/User/Delete/?q=" + SecurityExtensions.EncryptText("id=" + this.UserId.ToStringFromGuid()) + "';\"><i class=\"entypo-trash\"></i></button>";
        }
    }
}
