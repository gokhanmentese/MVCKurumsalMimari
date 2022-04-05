using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.DTOs;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IEmailService _emailService;


        public PostController(IEmailService emailService, IEMaiIServerService eMailIServerService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            PostCreateViewModel postCreateViewModel = new PostCreateViewModel();

            postCreateViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Yeni Posta", Controller = "Post", Action = "Create" }
            };

            return View(postCreateViewModel);
        }

        [Authorize]
        [HttpPost("send")]
        [ValidateAntiForgeryToken]
        public IActionResult Send(PostCreateViewModel postCreateViewModel)
        {
            var currentUserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            Email email = new Email();

            email.To = postCreateViewModel.To;
            email.Cc = postCreateViewModel.Cc;
            email.Bcc = postCreateViewModel.Bcc;
            email.Subject = postCreateViewModel.Subject;
            email.IsHtml = true;
            email.Body = postCreateViewModel.Body;
            email.CreatedBy = new Guid(currentUserId);
            email.SenderId = new Guid(currentUserId);

            var isSended = _emailService.SendEmail(email);

            return RedirectToAction("Outgoing", "Post");

        }

        public int PageSize = 5;
        public IActionResult Incoming(int page = 1)
        {
            PostIncomingViewModel postIncomingViewModel = new PostIncomingViewModel();

            postIncomingViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Gelen Posta", Controller = "Post", Action = "Incoming" }
            };

            var currentUserEmail = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            var emails = _emailService.GetByEmail(currentUserEmail);

            postIncomingViewModel.IncomingEmails = emails.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            postIncomingViewModel.SelectedEmail = postIncomingViewModel.IncomingEmails.FirstOrDefault();
            postIncomingViewModel.PagingInfo = new PagingInfo
            {
                ItemsPerPage = PageSize,
                TotalItems = emails.Count,
                CurrentPage = page
            };

            return View(postIncomingViewModel);
        }


        public IActionResult EmailGetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var tmp = _emailService.GetById(new Guid(id));
            if (tmp != null)
            {
                var emailDTO = new EmailDTOs
                {
                    Id = tmp.Id,
                    Subject = tmp.Subject,
                    Body = tmp.Body
                };
                return Ok(emailDTO);
            }

            return BadRequest();
        }


        public IActionResult Outgoing(int page = 1)
        {
            PostOutgoingViewModel postOutgoingViewModel = new PostOutgoingViewModel();

            postOutgoingViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Giden Posta", Controller = "Post", Action = "Outgoing" }
            };

            var currentUserId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var emails = _emailService.GetByUserId(new Guid(currentUserId));

            postOutgoingViewModel.OutgoingEmails = emails.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            postOutgoingViewModel.SelectedEmail = postOutgoingViewModel.OutgoingEmails.FirstOrDefault();
            postOutgoingViewModel.PagingInfo = new PagingInfo
            {
                ItemsPerPage = PageSize,
                TotalItems = emails.Count,
                CurrentPage = page
            };

            return View(postOutgoingViewModel);
        }
    }
}
