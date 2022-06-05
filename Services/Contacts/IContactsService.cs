using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.Contacts
{
    public interface IContactsService
    {
        Task AddQuestionAsync(Question question);

        IEnumerable<Question> GetAllQuestions();
        IEnumerable<T> GetAllQuestions<T>();

        Question GetById(int questionId);

        Task DeleteAsync(int questionId);

        IEnumerable<T> GetAnsweredQuestions<T>();
    }
}
