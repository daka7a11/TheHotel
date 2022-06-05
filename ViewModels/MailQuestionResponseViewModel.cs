using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels
{
    public class MailQuestionResponseViewModel : IMapFrom<Question>
    {
        public string FirstName { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Response { get; set; }
    }
}
