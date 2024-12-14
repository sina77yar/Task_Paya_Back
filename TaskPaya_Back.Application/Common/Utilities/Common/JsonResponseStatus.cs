
using Microsoft.AspNetCore.Mvc;

namespace TaskPaya_Back.Application.Common.Utilities.Common
{
    public static class JsonResponseStatus
    {
        public static JsonResult Success() => new JsonResult(new { status = "success" });
        public static JsonResult Success(object returnData) => new JsonResult(new { status = "success", data = returnData });
        public static JsonResult NotFound() => new JsonResult(new { status = "NotFound" });
        public static JsonResult NotFound(object returnData) => new JsonResult(new { status = "NotFound", data = returnData });
        public static JsonResult Error() => new JsonResult(new { status = "Error" });
        public static JsonResult Error(object returnData) => new JsonResult(new { status = "Error", data = returnData });

    }
}
