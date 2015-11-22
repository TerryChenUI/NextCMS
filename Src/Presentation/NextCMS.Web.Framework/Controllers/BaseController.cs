using NextCMS.Core.Infrastructure;
using NextCMS.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NextCMS.Web.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        #region
        ///// <summary>
        ///// Log exception
        ///// </summary>
        ///// <param name="exc">Exception</param>
        //protected void LogException(Exception exc)
        //{
        //    var workContext = EngineContext.Current.Resolve<IWorkContext>();
        //    var logger = EngineContext.Current.Resolve<ILogger>();

        //    var customer = workContext.CurrentCustomer;
        //    logger.Error(exc.Message, exc, customer);
        //}
        #endregion

        #region 消息通知
        /// <summary>
        /// Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }

        /// <summary>
        /// Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        ///// <summary>
        ///// Display error notification
        ///// </summary>
        ///// <param name="exception">Exception</param>
        ///// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        ///// <param name="logException">A value indicating whether exception should be logged</param>
        //protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true, bool logException = true)
        //{
        //    if (logException)
        //        LogException(exception);
        //    AddNotification(NotifyType.Error, exception.Message, persistForTheNextRequest);
        //}

        /// <summary>
        /// Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("nextcms.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }
        #endregion
    }
}
