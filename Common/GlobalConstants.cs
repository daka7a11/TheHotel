namespace TheHotel.Common
{
    public static class GlobalConstants
    {
        public const string AdministratorRole = "Administrator";

        public const string ReceptionistRole = "Receptionist";

        public const string NameSymbolsRegex = "([A-Z][a-z]+)";

        public const string SystemName = "TheHotel";

        public const int RoomMinSize = 1;

        public const int RoomMaxSize = 300;

        public const int RoomMinFloor = 1;

        public const int RoomMaxFloor = 50;

        public const double RoomMinPrice = 1;

        public const double RoomMaxPrice = 1000;

        public const int PINMinLegth = 10;

        public const int PINMaxLegth = 12;

        public const string PersonalIdentityNumberErrorMsg = "Value shoud be between 10 and 12 characters!";

        public const int ClientNameMinLength = 3;

        public const int ClientNameMaxLength = 30;

        public const string ClientNameErrorMsg = "Value shoud be between 3 and 30 characters!";

        public const string InvalidHireDateErrorMsg = "You cannot hire room {0} right now. The room is hired from {1} to {2}";

        public const string InvalidTenancyDateErrorMsg = "{0} date is invalid!";

        public const string EmailExistErrorMsg = "{0} е използван от друг клиент!";

        public const string InvalidRoomErrorMsg = "Стая {0} не съществува!";

        public const string ReservationRequest = "Заявка за резервация.";

        public const string ReservationAccepted = "Резервацията ви е приета.";

        public const string ReservationDeclined = "Резервацията ви е отказана.";

        public const string RoomAlreadyHiredErrorMsg = "Стаята е резервирана!";

        public const string TenancyDateValidateErrorMsg = "Датата на напускане не може да е по-малка или равна на датата на настаняване!";

        public const string OfferDateValidateErrorMsg = "Датата на изтичане на офертата не може да е по-малка или равна на началната дата!";

        public const string RequiredPropertyErrorMsg = "Това поле е задължително!";

        public const string InvalidCaptchaErrorMsg = "Моля въведете валиден код!";
    }
}
