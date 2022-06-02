using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;

namespace TheHotel.Services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepository;

        public ContactsService(IDeletableEntityRepository<Question> questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public async Task AddQuestionAsync(Question question)
        {
            await questionsRepository.AddAsync(question);
            await questionsRepository.SaveChangesAsync();
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return questionsRepository.All().ToList();
        }

        public IEnumerable<T> GetAllQuestions<T>()
        {
            return questionsRepository.All()
                .To<T>()
                .ToList();

        }

        public Question GetById(int questionId)
        {
            return questionsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == questionId);
        }
    }
}
