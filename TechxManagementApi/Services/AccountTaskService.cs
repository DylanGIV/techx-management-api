using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Helpers;
using TechxManagementApi.Models.AccountTasks;

namespace TechxManagementApi.Services
{
    public interface IAccountTaskService
    {
        Task CreateAccountTaskAsync(CreateAccountTaskRequest model);
        List<AccountTaskResponse> GetAllAccountTasks();
    }

        public class AccountTaskService : IAccountTaskService
        {
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _accessor;
        private readonly AppSettings _appSettings;
        //    private readonly IEmailService _emailService;

        public AccountTaskService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor accessor)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _accessor = accessor;
       }

       public async Task CreateAccountTaskAsync(CreateAccountTaskRequest model)
       {

            var httpContext = _accessor.HttpContext;
            var accountTask = new AccountTask();

            var owner = (Account)httpContext.Items["Account"];

            var currentTime = DateTime.UtcNow;
            // var dueDate = DateTime.SpecifyKind(model.DueDate, DateTimeKind.Utc);
            var dueDate = model.DueDate;
            
            if (dueDate.Kind != DateTimeKind.Utc)
            {
                dueDate = dueDate.ToUniversalTime();
            }
            

            accountTask.Title = model.TaskTitle;
            accountTask.Description = model.TaskDescription;
            accountTask.CreatedBy = owner;
            accountTask.CreatedAt = currentTime;
            accountTask.DueDate = dueDate;
            accountTask.AssignedTo = await _context.Accounts.FindAsync(model.AccountId);
            accountTask.Project = await _context.Projects.FindAsync(model.ProjectId);

            // save task
            await _context.AccountTasks.AddAsync(accountTask);
            await _context.SaveChangesAsync();
       }
       
       public List<AccountTaskResponse> GetAllAccountTasks()
       {
            var accountTasksRaw = _context.AccountTasks.ToList();
            var accountTasks = new List<AccountTaskResponse>();

            accountTasks = _mapper.Map<List<AccountTask>, List<AccountTaskResponse>>(accountTasksRaw);
            
            return accountTasks;
       }

   }
}

