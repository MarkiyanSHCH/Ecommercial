using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Core.Handlers.Template;

namespace WebAPI.API.RazorTemplateEngine
{
    public class RazorTemplate : IRazorTemplate
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public string TemplatesPath { get; set; }

        public RazorTemplate(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this._viewEngine = viewEngine;
            this._tempDataProvider = tempDataProvider;
            this._serviceProvider = serviceProvider;
            this.TemplatesPath = "/";
        }

        public async Task<string> RenderAsync<TModel>(string viewName, TModel model)
        {

            ActionContext actionContext = this.GetActionContext();

            IView view = this.FindView(viewName);

            using var output = new StringWriter();

            var viewDataDictionary = new ViewDataDictionary<TModel>(
                metadataProvider: new EmptyModelMetadataProvider(),
                modelState: new ModelStateDictionary())
            { Model = model };

            var tempDataDictionary = new TempDataDictionary(
               context: actionContext.HttpContext,
               provider: this._tempDataProvider);

            var viewContext = new ViewContext(
                actionContext: actionContext,
                view: view,
                viewData: viewDataDictionary,
                tempData: tempDataDictionary,
                writer: output,
                htmlHelperOptions: new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            string renderedTemplate = output.ToString();

            return renderedTemplate;
        }
        private ActionContext GetActionContext()
        => new ActionContext(
            httpContext: new DefaultHttpContext { RequestServices = this._serviceProvider },
            routeData: new RouteData(),
            actionDescriptor: new ActionDescriptor());

        private IView FindView(string viewName)
        {
            string razorView = $"{viewName}.cshtml";
            ViewEngineResult getViewResult = this._viewEngine.GetView(executingFilePath: this.TemplatesPath, viewPath: razorView, isMainPage: false);
            if (getViewResult.Success) return getViewResult.View;

            throw new InvalidOperationException($"Unable to find template '{viewName}'. The following locations were searched:");
        }
    }
}