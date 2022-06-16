using System;
using System.Threading.Tasks;
using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _statisticService.RegisterVisitAsync(context.Request.Path);
        await UpdateHeaders(context);

        await _next(context);
    }

    private async Task UpdateHeaders(HttpContext context)
    {
        var count = await _statisticService.GetVisitsCountAsync(context.Request.Path);
        context.Response.Headers.Add(CustomHttpHeaders.TotalPageVisits, count.ToString());
    }
}
