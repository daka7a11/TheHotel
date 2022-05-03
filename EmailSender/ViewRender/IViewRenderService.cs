using System.Threading.Tasks;

namespace TheHotel.EmailSender.ViewRender
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
