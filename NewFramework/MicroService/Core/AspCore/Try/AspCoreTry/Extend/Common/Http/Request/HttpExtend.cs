

using Microsoft.AspNetCore.Http;

public static class HttpExtend{
   
    public static bool IsAjax(this HttpRequest request)
    {
        var result = false;
        var xreq = request.Headers.ContainsKey("x-requested-with");
        if (xreq)
        {
            result = request.Headers["x-requested-with"] == "XMLHttpRequest";
        }
        return result; 
    }


}