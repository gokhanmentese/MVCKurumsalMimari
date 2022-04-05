using System;
using System.Collections.Generic;
using System.Linq;
using Business.Constants;
using Core;
using Core.Extensions;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models;
using Web.API.Models.Request;
using Web.API.Models.Response;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            return Ok(_taskService.GetAll());
        }

        [HttpPost]
        [Route("TaskList")]
        public TaskListResponse TaskList(TaskListRequest taskListRequest)
        {
            TaskListResponse response = new TaskListResponse();

            try
            {
                if (taskListRequest.Auth.UserName == "" && taskListRequest.Auth.Password == "")
                {
                    var tasks = _taskService.GetAll().Select(x => new
                    {
                        x.Description,
                        x.Subject,
                        x.AssignUserId,
                        x.StartDate,
                        x.EndDate

                    }).ToList()
                    .Select(x => new Web.API.Models.Attribute.Task()
                    {
                        Description = x.Description,
                        Subject = x.Subject,
                        StartDate = x.StartDate,
                        EndDate = x.StartDate

                    }).ToList();
                    //.OrderBy(x=>x.StartDate).ThenBy(y=>y.Title);


                    response = new TaskListResponse
                    {
                        Status = new Models.Attribute.Status()
                        {
                            Code = (int)Enums.MessageCode.Authorization,
                            Message = Enums.MessageCode.Authorization.ToString()
                        },
                        Tasks = tasks
                    };

                }
                else
                {
                    response = new TaskListResponse
                    {
                        Status = new Models.Attribute.Status()
                        {
                            Code = (int)Enums.MessageCode.Authorization,
                            Message = Enums.MessageCode.Authorization.ToString()
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                response = new TaskListResponse
                {
                    Status = new Models.Attribute.Status()
                    {
                        Code = (int)Enums.MessageCode.Error,
                        Message = Enums.MessageCode.Error.ToString() + ex.Message
                    }
                };
            }

            return response;
        }

        [HttpGet("{id}")]
        public Task GetById(Guid id)
        {
            return _taskService.GetById(id);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Task task)
        {
            _taskService.Add(task);
            return Ok(Messages.TaskAdded);

        }
        //public TaskResponse Add([FromBody] TaskRequest taskRequest)
        //{
        //    TaskResponse response = new TaskResponse();

        //    try
        //    {

        //        if (taskRequest.Auth.UserName == "" && taskRequest.Auth.Password == "")
        //        {
        //            var entity = _taskService.Add(new Entities.Concrete.Task
        //            {
        //                Title = taskRequest.Task.Title,
        //                Description = taskRequest.Task.Description,
        //                StartDate = taskRequest.Task.StartDate,
        //                EndDate = taskRequest.Task.EndDate
        //            });

        //            if (entity != null && entity.Id != Guid.Empty)
        //            {
        //                response = new TaskResponse
        //                {
        //                    Status = new Models.Attribute.Status()
        //                    {
        //                        Code = (int)Enums.MessageCode.Succeessful,
        //                        Message = Enums.MessageCode.Succeessful.ToString()
        //                    },
        //                    TaskId = entity.Id
        //                };
        //            }
        //            else
        //            {
        //                response = new TaskResponse
        //                {
        //                    Status = new Models.Attribute.Status()
        //                    {
        //                        Code = (int)Enums.MessageCode.UnSucceessful,
        //                        Message = Enums.MessageCode.UnSucceessful.ToString()
        //                    }
        //                };
        //            }
        //        }
        //        else
        //        {
        //            response = new TaskResponse
        //            {
        //                Status = new Models.Attribute.Status()
        //                {
        //                    Code = (int)Enums.MessageCode.Authorization,
        //                    Message = Enums.MessageCode.Authorization.ToString()
        //                }
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = new TaskResponse
        //        {
        //            Status = new Models.Attribute.Status()
        //            {
        //                Code = (int)Enums.MessageCode.Error,
        //                Message = Enums.MessageCode.Error.ToString() + ex.Message
        //            }
        //        };
        //    }

        //    return response;
        //}

        [HttpPost("update")]
        public IActionResult Update(Task task)
        {
            var tmp = _taskService.GetById(task.Id);

            if (tmp != null)
            {
                _taskService.Update(task);
                return Ok(Messages.TaskUpdated);
            }

            return BadRequest();
        }

        public void Delete(Entities.Concrete.Task task)
        {
            _taskService.Delete(task);
        }

        [HttpGet("closedtasks")]
        public List<Task> GetClosedTasks()
        {
            return _taskService.GetClosedTasks();
        }

        [HttpGet("waitingtasks")]
        public List<Task> GetWaitingTasks()
        {
            return _taskService.GetWaitingTasks();
        }

        [HttpPost("transaction")]
        public IActionResult TransactionTest(Entities.Concrete.Task task)
        {
            return Ok(_taskService.TransactionalOperation(task));
        }
    }
}
