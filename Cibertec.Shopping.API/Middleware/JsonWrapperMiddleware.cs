using System.Text.Json;

namespace Cibertec.Shopping.API.Middleware
{
    public class JsonWrapperMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody  = context.Response.Body;

            try
            {
                using (var newBody = new MemoryStream())
                {
                    context.Response.Body = newBody;

                    await _next(context);

                    var successContext = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

                    if (context.Response.ContentType?.StartsWith("application/json") == true && successContext)
                    {
                        newBody.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(newBody))
                        {
                            var responseBody = await reader.ReadToEndAsync();
                            var responseObject = new
                            {
                                success = successContext,
                                data = JsonSerializer.Deserialize<object>(responseBody)
                            };

                            newBody.Seek(0, SeekOrigin.Begin);
                            context.Response.Body = originalBody;
                            context.Response.ContentType = "application/json";

                            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                            var jsonResponse = JsonSerializer.Serialize(responseObject, jsonOptions);
                            await context.Response.WriteAsync(jsonResponse);


                        }
                    }
                    else
                    {
                        newBody.Seek(0, SeekOrigin.Begin);
                        await newBody.CopyToAsync(originalBody);
                    }

                }

            }
            finally {
                context.Response.Body = originalBody;
            }
        }
    }
}
