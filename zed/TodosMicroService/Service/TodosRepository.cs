using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;


namespace Todos.Api.Services
{
    public class TodosRepository : ITodosRepository
    {

        private readonly zedContext _zedContext;
        private readonly IConfiguration _configuration;
        private static readonly string DEFAULT_CONNECTION = "DefaultConnection";
        public TodosRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            string conString = ConfigurationExtensions.GetConnectionString(this._configuration, DEFAULT_CONNECTION);
            System.Console.WriteLine(conString);
            _zedContext = new zedContext(conString);
        }

        public List<Todos.Api.Models.Todo> GetAll()
        {
            List<Todos.Api.Models.Todo> ret = new List<Todos.Api.Models.Todo>();
            foreach (var task in _zedContext.Tasks)
            {
                if (task.Reminders.Count() > 0)
                {
                    var reminder = task.Reminders.ToArray()[0];

                    int ReminderMethod = reminder.ReminderTypeId;
                    DateTime ReminderFirstStartTime = task.StartTime.AddMinutes(-1 * reminder.FirstReminderLeadTimeInMinute);
                    int ReminderFrequency = reminder.FrequencyInMinute;
                    int ReminderTotalRetries = reminder.NumberOfRetries;
                    var todo = Todos.Api.Models.Todo.CreateTodo(task, task.TaskDependencyDependingTasks.Count, ReminderMethod, ReminderFirstStartTime, ReminderFrequency, ReminderTotalRetries);
                    ret.Add(todo);
                }
                else
                {
                    var todo = Todos.Api.Models.Todo.CreateTodo(task, task.TaskDependencyDependingTasks.Count);
                    ret.Add(todo);
                }

            }
            return ret;
        }

        public Todos.Api.Models.Todo Get(int Id)
        {
            foreach (var task in _zedContext.Tasks)
            {
                if (task.Id != Id) continue;
                if (task.Reminders.Count() > 0)
                {
                    var reminder = task.Reminders.ToArray()[0];

                    int ReminderMethod = reminder.ReminderTypeId;
                    DateTime ReminderFirstStartTime = task.StartTime.AddMinutes(-1 * reminder.FirstReminderLeadTimeInMinute);
                    int ReminderFrequency = reminder.FrequencyInMinute;
                    int ReminderTotalRetries = reminder.NumberOfRetries;
                    return Todos.Api.Models.Todo.CreateTodo(task, task.TaskDependencyDependingTasks.Count, ReminderMethod, ReminderFirstStartTime, ReminderFrequency, ReminderTotalRetries);
                }
                else
                {
                    return Todos.Api.Models.Todo.CreateTodo(task, task.TaskDependencyDependingTasks.Count);
                }
            }
            return null;
        }

        private Task GetTaskFromTodo(Todos.Api.Models.Todo todo)
        {
            Task task = new Task
            {
                Id = todo.id,
                Name = todo.name,
                Description = todo.description,
                StartTime = todo.startTime,
                TaskPriorityId = todo.priority,
                TaskTypeId = todo.type,
                DurationInMinutes = todo.durationInMinutes
            };
            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="todo">expect to see the ID being zero</param>
        /// <returns></returns>
        public ProcessingResult Add(Models.Todo todo)
        {
            var task = GetTaskFromTodo(todo);
            _zedContext.Tasks.Add(task);

            try
            {
                _zedContext.SaveChanges();
                return ProcessingResult.Success;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(string.Format("Exception encountered in {0} ex={1}", MethodInfo.GetCurrentMethod(), ex));
                return ProcessingResult.Failure;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="todo"></param>
        /// <returns>Success on successfully updated; Failure on error; Nooperation 
        /// when there is no op as the target is not found</returns>
        public ProcessingResult Update(Models.Todo todo)
        {
            var theOne = _zedContext.Tasks.FirstOrDefault(x => x.Id == todo.id);
            if (theOne == null)
            {
                Debug.WriteLine(string.Format("The todo with Id={0} is no longer " +
                    "existent in database", todo.id));
                return ProcessingResult.Nooperation;
            }
            try
            {
                var newOne = GetTaskFromTodo(todo);
                theOne.Transcribe(newOne);
                _zedContext.Update(theOne);
                var result = _zedContext.SaveChanges();
                if(result > 0)
                    return ProcessingResult.Success;
                return ProcessingResult.Failure;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception encountered in {0} ex={1}", 
                    MethodInfo.GetCurrentMethod(), ex));
                return ProcessingResult.Failure;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Success on successfully updated; Failure on error; Nooperation 
        /// when there is no op as the target is not found</returns>
        public ProcessingResult Delete(int Id)
        {
            var theOne = _zedContext.Tasks.FirstOrDefault(x => x.Id == Id);
            if (theOne != null)
            {
                try
                {
                    //find out all the records that each depends on the current one, and delete them.
                    var array = _zedContext.TaskDependencies.Where(x => x.TaskId == Id).ToArray();
                    foreach(var dep in array)
                    {
                        _zedContext.TaskDependencies.Remove(dep);
                    }
                    
                    // then delete the current task from Tasks
                    _zedContext.Tasks.Remove(theOne);
                    var result = _zedContext.SaveChanges();
                    if(result > 0)
                        return ProcessingResult.Success;
                    return ProcessingResult.Failure;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Exception encountered in {0} ex={1}", 
                        MethodInfo.GetCurrentMethod(), ex));
                    return ProcessingResult.Failure;
                }
            }
            return ProcessingResult.Nooperation;
        }

    }
}