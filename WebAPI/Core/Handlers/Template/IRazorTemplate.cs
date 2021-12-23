using System.Threading.Tasks;

namespace Core.Handlers.Template
{
    public interface IRazorTemplate
    {
        string TemplatesPath { get; set; }
        Task<string> RenderAsync<TModel>(string viewName, TModel model);
    }
}
