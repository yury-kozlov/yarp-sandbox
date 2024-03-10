using Microsoft.AspNetCore.Http.Extensions;
using Yarp.ReverseProxy.Transforms;
using Yarp.ReverseProxy.Transforms.Builder;

internal class MyTransformProvider : ITransformProvider
{
    public void ValidateRoute(TransformRouteValidationContext context)
    {
    }

    public void ValidateCluster(TransformClusterValidationContext context)
    {
    }

    public void Apply(TransformBuilderContext transformBuildContext)
    {
        transformBuildContext.AddRequestTransform(async ctx =>
        {
            await Task.Delay(1000); // simulate async
            ctx.ProxyRequest.Headers.Add("my-source-url", ctx.HttpContext.Request.GetDisplayUrl());
            ctx.ProxyRequest.RequestUri = new Uri($"http://localhost:8080/{DateTime.Now.Hour}/{DateTime.Now.Minute}/?sec={DateTime.Now.Second}");
        });
    }
}