namespace ValkyrieHr.Contracts.ApiRouters
{
    public static class ApiRoute
    {
        // Account
        public static class AccountRoutes
        {
            public const string Create = "account/create";
            public const string Login = "account/login";
            public const string Update = "account/Update";
            public const string AllUser = "account/user";
            public const string ById = "account/user/{id}";
            public const string UserImageUpdate = "account/UserImageUpdate";
            public const string AddRole = "account/AddRole";

        }
        // Employees 
        public static class EmployeesRoutes
        {
            public const string Create = "Employee/create";
            public const string Update = "Employee/update";
            public const string Get = "Employee/{id}";
            public const string GetAll = "Employee";
            public const string Delete = "Employee/delete/{id}";
            public const string CreateForm = "Employee/CreateForm";
            public const string AllEmpReports = "Employee/AllEmpReports";
            public const string UpdateImage = "Employee/UpdateImage";
        }
        // Vacations
        public static class VacationsRoutes
        {
            public const string Create = "Vacation/create";
            public const string Update = "Vacation/update";
            public const string Get = "Vacation/{id}";
            public const string GetAll = "Vacation";
            public const string Delete = "Vacation/delete/{id}";
            public const string CreateForm = "Vacation/CreateForm";
            public const string AllEmpReports = "Vacation/AllEmpReports";
            public const string CreateEmpVacation = "Vacation/CreateEmpVacation";
        }

    }
}

