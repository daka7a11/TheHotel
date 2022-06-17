namespace TheHotel.Common
{
    public static class GlobalConstants
    {
        public const string AdministratorRole = "Администратор";

        public const string ReceptionistRole = "Рецепционист";

        public const string NameSymbolsRegex = "([A-Z][a-z]+)";

        public const string PinOnlyDigits = "[0-9]+";

        public const string SystemName = "TheHotel";

        public const int RoomMinSize = 1;

        public const int RoomMaxSize = 300;

        public const int RoomMinFloor = 1;

        public const int RoomMaxFloor = 50;

        public const double RoomMinPrice = 1;

        public const double RoomMaxPrice = 1000;

        public const int PINMinLength = 9;

        public const int PINMaxLength = 10;

        public const int ClientNameMinLength = 3;

        public const int ClientNameMaxLength = 30;

        public const int TitleMinLength = 3;

        public const int MessageTextMinLength = 10;

        public const string PersonalIdentityNumberErrorMsg = "Стойността трябва да е между 9 и 10 цифри!";

        public const string ClientNameLengthErrorMsg = "Value shoud be between 3 and 30 characters!";

        public const string ClientNameRegexErrorMsg = "Името трябва да съдържа само букви!";

        public const string InvalidHireDateErrorMsg = "You cannot hire room {0} right now. The room is hired from {1} to {2}";

        public const string InvalidTenancyDateErrorMsg = "{0} date is invalid!";

        public const string EmailExistErrorMsg = "{0} е използван от друг клиент!";

        public const string InvalidRoomErrorMsg = "Стая {0} не съществува!";

        public const string ReservationRequest = "Заявка за резервация.";

        public const string ReservationAccepted = "Резервацията Ви е приета.";

        public const string ReservationDeclined = "Резервацията Ви е отказана.";

        public const string RoomAlreadyHiredErrorMsg = "Стаята е резервирана!";

        public const string TenancyDateValidateErrorMsg = "Датата на напускане не може да е по-малка или равна на датата на настаняване!";

        public const string OfferDateValidateErrorMsg = "Датата на изтичане на офертата не може да е по-малка или равна на началната дата!";

        public const string RequiredTitleErrorMsg = "Полето заглавие е задължително!";

        public const string RequiredQuesitonErrorMsg = "Полето запитване е задължително!";

        public const string RequiredReviewErrorMsg = "Полето отзив е задължително!";

        public const string InvalidCaptchaErrorMsg = "Моля въведете валиден код!";

        public const string MessageLengthErrorMsg = "Полето трябва да съдържа поне 10 символа!";

        public const string TitleLengthErrorMsg = "Заглавието трябва да съдържа поне 3 символа!";

        public const string SuccessfullySubmittedQuestion = "Вие успешно изпратихте запитване към нас!";

        public const string SuccessfullyCreatedReview = "Благодарим Ви за вашия отзив!";

        public const string InvalidRating = "Моля въведете валиден рейтинг!";

        public const string RequiredPINErrorMsg = "Полето ЕГН е задължително!";

        public const string RequiredMiddleNamerrorMsg = "Полето Презиме е задължително!";

        public const string RequiredNameErrorMsg = "Полето име е задължително!";

        public const string RequiredLastNameErrorMsg = "Полето фамилия е задължително!";

        public const string RequiredPhoneErrorMsg = "Полето телефон е задължително!";

        public const string RequiredEmailErrorMsg = "Полето имейл е задължително!";

        public const string RequiredDescriptionErrorMsg = "Полето описание е задължително!";

        public const string RequiredAccommodationDateErrorMsg = "Датата на настаняване е задължителна!";

        public const string RequiredDepartureDateErrorMsg = "Датата на напускане е задължителна!";

        public const string InvalidRecaptchaErrorMsg = "Моля потвърдете, че не сте робот!";

        public const string RequiredPropertyErrorMsg = "Полето е задължително!";

        public const string RequiredImageErrorMsg = "Моля изберете поне 1 снимка!";

        public const string RequiredRoleErrorMsg = "Моля изберете роля!";
    }
}
