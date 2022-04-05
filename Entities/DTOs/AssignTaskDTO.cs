using Core.Entities;
using Core.Enums;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class AssignTaskDTO : IEntity
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid AssignUserId { get; set; }

        public Guid AssignedUserId { get; set; }

        public DateTime? AssignDate { get; set; }

        public int StatusCode { get; set; } = (int)Enumarations.AssignTaskStatus.Open;

        public string Status
        {
            get
            {
                return this.StatusCode.ToAssignStatus();
            }
        }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string AssignDateStr
        {
            get
            {
                if (this.AssignDate != null)
                    return this.AssignDate.Value.ToString("dd.MM.yyyy");
                else
                    return string.Empty;
            }
        }

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

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public string btn_Detail_Kullanici()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Detay" + " onclick=\"window.location.href='/User/Detail/" + this.AssignedUserId + "';\"><i class=\"entypo-newspaper\"></i></button>";
        }
        public string btn_Remove_Kullanici()
        {
            return "<button type=\"button\" class=\"btn btn-white tooltip-default\" data-toggle=\"tooltip\" data-placement=\"top\" data-original-title=" + "Sil" + " onclick=\"window.location.href='/User/Delete/" + this.AssignedUserId + "';\"><i class=\"entypo-trash\"></i></button>";
        }
    }
}
