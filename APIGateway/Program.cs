using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System;


namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register HttpClients for microservices
            builder.Services.AddHttpClient("EventClient_D_BIT_23_0030", c => c.BaseAddress = new Uri("http://localhost:5001"));
            builder.Services.AddHttpClient("GuestClient_D_BIT_23_0030", c => c.BaseAddress = new Uri("http://localhost:5002"));
            builder.Services.AddHttpClient("TaskClient_D_BIT_23_0030", c => c.BaseAddress = new Uri("http://localhost:5003"));

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable Swagger at root
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway D_BIT_23_0030 v1");
                c.RoutePrefix = string.Empty; // Swagger opens at http://localhost:5000
            });

            // Proxy /gateway/events/... to Event Service
            app.Map("/{**rest}", async (HttpContext ctx) =>
            {
                var path = ctx.Request.Path.Value;
                if (path.StartsWith("/gateway/events"))
                    await ProxyRequest(ctx, "EventClient_D_BIT_23_0030", "/gateway/events");
                else if (path.StartsWith("/gateway/guests"))
                    await ProxyRequest(ctx, "GuestClient_D_BIT_23_0030", "/gateway/guests");
                else if (path.StartsWith("/gateway/tasks"))
                    await ProxyRequest(ctx, "TaskClient_D_BIT_23_0030", "/gateway/tasks");
                else
                    ctx.Response.StatusCode = 404;
            });

            // Summary endpoint
            app.MapGet("/gateway/summary/{eventId}", async (int eventId, IHttpClientFactory httpFactory) =>
            {
                var eventClient = httpFactory.CreateClient("EventClient_D_BIT_23_0030");
                var guestClient = httpFactory.CreateClient("GuestClient_D_BIT_23_0030");
                var taskClient = httpFactory.CreateClient("TaskClient_D_BIT_23_0030");

                var evtResp = await eventClient.GetAsync($"/events/{eventId}");
                if (!evtResp.IsSuccessStatusCode) return Results.NotFound(new { error = "Event not found" });
                var evt = await evtResp.Content.ReadFromJsonAsync<object>();

                var guestsResp = await guestClient.GetAsync($"/guests?eventId={eventId}");
                var guests = guestsResp.IsSuccessStatusCode ? await guestsResp.Content.ReadFromJsonAsync<object[]>() : Array.Empty<object>();

                var tasksResp = await taskClient.GetAsync($"/tasks?eventId={eventId}");
                var tasks = tasksResp.IsSuccessStatusCode ? await tasksResp.Content.ReadFromJsonAsync<object[]>() : Array.Empty<object>();

                return Results.Ok(new
                {
                    eventData = evt,
                    guestsData = guests,
                    tasksData = tasks
                });
            }).WithName("GetSummary_D_BIT_23_0030");

            app.Run("http://localhost:5000");

            // Helper method for proxying requests
            static async Task ProxyRequest(HttpContext ctx, string clientName, string gatewayPrefix)
            {
                var httpFactory = ctx.RequestServices.GetRequiredService<IHttpClientFactory>();
                var client = httpFactory.CreateClient(clientName);

                var path = ctx.Request.Path.Value.Replace(gatewayPrefix, "");
                var targetUri = new Uri(client.BaseAddress, path);

                var requestMessage = new HttpRequestMessage(new HttpMethod(ctx.Request.Method), targetUri)
                {
                    Content = new StreamContent(ctx.Request.Body)
                };

                foreach (var header in ctx.Request.Headers)
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());

                var response = await client.SendAsync(requestMessage);

                ctx.Response.StatusCode = (int)response.StatusCode;
                foreach (var h in response.Headers)
                    ctx.Response.Headers[h.Key] = h.Value.ToArray();

                await response.Content.CopyToAsync(ctx.Response.Body);
            };
        }
        
    }
}
