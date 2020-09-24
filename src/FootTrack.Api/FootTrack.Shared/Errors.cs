namespace FootTrack.Shared
{
    public static class Errors
    {
        public static class User
        {
            public static Error EmailIsTaken(string email = default) =>
                new Error("user.email.is.taken", $"User email '{email}' is taken.");

            public static Error IncorrectEmailOrPassword() =>
                new Error("incorrect.email.or.password", "Email or password is incorrect.");
        }

        public static class General
        {
            public static Error NotFound(string entityName = "Record", string id = default) =>
                new Error("record.not.found", $"'{entityName}' not found for Id '{id}'.");

            public static Error Empty(string entityName = "Record") =>
                new Error("record.empty", $"'{entityName}' should not be empty.");

            public static Error TooLong(int maxLength, string entityName = "Record") =>
                new Error("record.too.long", $"'{entityName}' too long. Max length is '{maxLength}'.");

            public static Error Invalid(string entityName = "Record") =>
                new Error("record.invalid", $"'{entityName}' is invalid.");
        }

        public static class Password
        {
            public static Error TooShort(int minLength, string entityName = "Record") =>
                new Error("password.too.short", $"'{entityName}' too short. Min length is '{minLength}'.");
        }

        public static class Device
        {
            public static Error DeviceUnreachable(string deviceName = "device") =>
                new Error("device.unreachable", $"Device called '{deviceName}' is unreachable.");
        }
    }
}