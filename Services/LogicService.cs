using SimonP_amital.Context;
using SimonP_amital.Models;
using SimonP_amital.Requests;
using SimonP_amital.Responses;

namespace SimonP_amital.Services
{
    public class LogicService
    {
        private readonly DataBaseContext _dbContext;

        public LogicService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Users> GetUsers()
        {

            return _dbContext.Users;
        }


        public Users GetUser(int id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<Tasks> GetTasks()
        {

            return _dbContext.Tasks;
        }


        public Tasks GetTask(int id)
        {
            return _dbContext.Tasks.SingleOrDefault(t => t.Id == id);
        }


        public IEnumerable<Tasks> GetExceedTasksForTomorrow()
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            return _dbContext.Tasks.Where(t => t.TargetDate == tomorrow).ToList();
        }

        public TaskResponse AddTask(CreateTask value)
        {
            var response = new TaskResponse();

            if (_dbContext.Tasks.Any(t => t.UserId == value.UserId && t.Subject == value.Subject))
            {
                response.ErrorMessage = "Task with the same subject already exists for this user.";
                return response;
            }

            if (!IsUserTasksCountLessThanTen(value.UserId))
            {
                response.ErrorMessage = "User already has 10 tasks.";
                return response;
            }

            try
            {
                var task = new Tasks
                {
                    Subject = value.Subject,
                    IsCompleted = value.IsCompleted,
                    TargetDate = value.TargetDate,
                    UserId = value.UserId
                };
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"An error occurred while adding the task: {ex.Message}";
            }
            return response;
        }


        public bool IsUserTasksCountLessThanTen(int userId)
        {
            int taskCount = _dbContext.Tasks.Count(t => t.UserId == userId);

            return taskCount < 10;
        }

    }
}
