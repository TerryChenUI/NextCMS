using System;
using Autofac;
using NextCMS.Core.Domain.Configuration;
using NextCMS.Core.Infrastructure;
//using NextCMS.Services.Logging;

namespace NextCMS.Services.Configuration
{
    /// <summary>
    /// Task
    /// </summary>
    public partial class Task
    {
        /// <summary>
        /// Ctor for Task
        /// </summary>
        private Task()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        public Task(ScheduleTask task)
        {
            this.Type = task.Type;
            this.Enabled = task.Enabled;
            this.StopOnError = task.StopOnError;
            this.Name = task.Name;
        }

        private ITask CreateTask(ILifetimeScope scope)
        {
            ITask task = null;
            if (this.Enabled)
            {
                var type2 = System.Type.GetType(this.Type);
                if (type2 != null)
                {
                    object instance;
                    if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                    {
                        //not resolved
                        instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                    }
                    task = instance as ITask;
                }
            }
            return task;
        }
        
        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="throwException">A value indicating whether exception should be thrown if some error happens</param>
        /// <param name="dispose">A value indicating whether all instances hsould be disposed after task run</param>
        public void Execute(bool throwException = false, bool dispose = true)
        {
            this.IsRunning = true;

            //background tasks has an issue with Autofac
            //because scope is generated each time it's requested
            //that's why we get one single scope here
            //this way we can also dispose resources once a task is completed
            var scope = EngineContext.Current.ContainerManager.Scope();
            var scheduleTaskService = EngineContext.Current.ContainerManager.Resolve<IScheduleTaskService>("", scope);
            var scheduleTask = scheduleTaskService.GetTaskByType(this.Type);

            try
            {
                var task = this.CreateTask(scope);
                if (task != null)
                {
                    this.LastStartDate = DateTime.Now;
                    if (scheduleTask != null)
                    {
                        //update appropriate datetime properties
                        scheduleTask.LastStartDate = this.LastStartDate;
                        scheduleTaskService.UpdateTask(scheduleTask);
                    }

                    //execute task
                    task.Execute();
                    this.LastEndDate = this.LastSuccessDate = DateTime.Now;
                }
            }
            catch (Exception exc)
            {
                this.Enabled = !this.StopOnError;
                this.LastEndDate = DateTime.Now;

                //log error
                //var logger = EngineContext.Current.ContainerManager.Resolve<ILogger>("", scope);
                //logger.Error(string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
                //if (throwException)
                //    throw;
            }

            if (scheduleTask != null)
            {
                //update appropriate datetime properties
                scheduleTask.LastEndDate = this.LastEndDate;
                scheduleTask.LastSuccessDate = this.LastSuccessDate;
                scheduleTaskService.UpdateTask(scheduleTask);
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }

            this.IsRunning = false;
        }

        /// <summary>
        /// A value indicating whether a task is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Datetime of the last start
        /// </summary>
        public DateTime? LastStartDate { get; private set; }

        /// <summary>
        /// Datetime of the last end
        /// </summary>
        public DateTime? LastEndDate { get; private set; }

        /// <summary>
        /// Datetime of the last success
        /// </summary>
        public DateTime? LastSuccessDate { get; private set; }

        /// <summary>
        /// A value indicating type of the task
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// A value indicating whether to stop task on error
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// Get the task name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A value indicating whether the task is enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
