namespace FootTrack.Api.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class Users
        {
            private const string ControllerEndpoint = Base + "/users";

            public const string Register =  ControllerEndpoint + "/register";

            public const string Login = ControllerEndpoint + "/login";

            public const string GetById = ControllerEndpoint + "/{id:string}";
        }
    }
}
