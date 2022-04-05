using Core.Entities;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Entities.Concrete
{

    public class Task : BaseEntity
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public int? PriorityCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string StartDateStr
        {
            get
            {
                if (this.StartDate != null)
                    return this.StartDate.Value.ToString("dd.MM.yyyy");
                else
                    return string.Empty;
            }
        }

        public string EndDateStr
        {
            get
            {
                if (this.EndDate != null)
                    return this.EndDate.Value.ToString("dd.MM.yyyy");
                else
                    return string.Empty;
            }
        }

        public int? CategoryId { get; set; }

        //public virtual Category Category { get; set; }

        public int? ActualDurationMinutes { get; set; }

        public Guid? AssignUserId { get; set; }

        public int StateCode { get; set; }

        public int StatusCode { get; set; }

        public string Status
        {
            get
            {
                return this.StatusCode.ToTaskStatus();
            }
        }

        public virtual List<TaskFile> TaskFiles { get; set; }


        public string btn_Detail_Task()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Detay" + " onclick=\"window.location.href='/Task/Detail/?q=" + SecurityExtensions.EncryptText("id="+this.Id.ToStringFromGuid()) + "';\"><i class=\"entypo-newspaper\"></i></button>";
        }
        public string btn_Delete_Task()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Sil" + " onclick=\"window.location.href='/Task/Delete/?q=" + SecurityExtensions.EncryptText("id=" + this.Id.ToStringFromGuid())+ "';\"><i class=\"entypo-trash\"></i></button>";
        }

        public string btn_Assign_Task()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Devret" + " onclick=\"window.location.href='/Task/Assignment/?q=" + SecurityExtensions.EncryptText("id=" + this.Id.ToStringFromGuid()) + "';\"><i class=\"entypo-newspaper\"></i></button>";
        }

    }
}
