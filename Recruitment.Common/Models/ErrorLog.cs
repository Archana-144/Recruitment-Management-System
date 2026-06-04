namespace Recruitment.Common.Models
{
    public class ErrorLog
    {
        public string ? ErrorMessage { get; set; }

        public string  ? StackTrace { get; set; }

        public string ControllerName { get; set; }

        public string MethodName { get; set; }


    }
}