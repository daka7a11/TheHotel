﻿using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Contacts
{
    public class AllQuestionsViewModel : IMapFrom<Question>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Title { get; set; }
    }
}
