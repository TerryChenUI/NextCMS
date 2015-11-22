//using System;
//using System.Web.Mvc;
//using Newtonsoft.Json;

//namespace NextCMS.Web.Framework.Mvc
//{
//    public class DataTableJsonResult : JsonResult
//    {
//        private DataTableRetunParam _returnParam;

//        public DataTableJsonResult(DataTableRetunParam returnParam) 
//        {
//            this._returnParam = returnParam;
//        }

//        public override void ExecuteResult(ControllerContext context)
//        {
//            if (context == null)
//                throw new ArgumentNullException("context");

//            ////we do it as described here - http://stackoverflow.com/questions/15939944/jquery-post-json-fails-when-returning-null-from-asp-net-mvc

//            var response = context.HttpContext.Response;
//            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
//            if (ContentEncoding != null)
//                response.ContentEncoding = ContentEncoding;

//            this.Data = _returnParam;

//            //If you need special handling, you can call another form of SerializeObject below
//            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
//            response.Write(serializedObject);
//        }
//    }

//    public class DataTableRetunParam
//    {
//        public string sEcho { get; set; }
//        public int iDisplayStart { get; set; }

//        public int iTotalRecords { get; set; }

//        public int iTotalDisplayRecords { get; set; }

//        public object aaData { get; set; }
//    }
//}
