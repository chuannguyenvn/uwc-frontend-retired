using Models.Types;

namespace Https
{
    public static class Endpoints
    {
        public const string DOMAIN = "test11111a435345.azurewebsites.net/";

        public static class Account
        {
            private const string ACCOUNT = "account/";

            public const string LOGIN = ACCOUNT + "login/";
            public const string REGISTER = ACCOUNT + "register/";
        }

        public static class Administration
        {
            private const string ADMINISTRATION = "administration/";

            public static string DeleteAccount(int accountId)
            {
                return $"{ADMINISTRATION}delete-account/{accountId}";
            }
        }

        public static class DrivingHistory
        {
            private const string DRIVING_HISTORY = "drivinghistory/";

            public const string ADD = DRIVING_HISTORY + "add";
            public const string ALL = DRIVING_HISTORY + "all";

            public static string Delete(int drivingHistoryId)
            {
                return $"{DRIVING_HISTORY}delete/{drivingHistoryId}";
            }
        }

        public static class DrivingLicense
        {
            private const string DRIVING_LICENSE = "drivinglicense/";

            public const string ADD = DRIVING_LICENSE + "add";

            public static string Get(int driverId)
            {
                return $"{DRIVING_LICENSE}get/{driverId}";
            }

            public const string UPDATE = DRIVING_LICENSE + "update";

            public static string Delete(int drivingLicenseId)
            {
                return $"{DRIVING_LICENSE}delete/{drivingLicenseId}";
            }

            public const string DELETE_OUTDATED = DRIVING_LICENSE + "delete-outdated";
        }

        public static class Mcp
        {
            private const string MCP = "mcp/";

            public const string ADD = MCP + "add";

            public static string Empty(int mcpId)
            {
                return $"{MCP}empty/{mcpId}";
            }

            public const string GET_FULL = MCP + "get/full";
            public const string GET_IN_RANGE = MCP + "get/in-range";
            public const string GET_ALL = MCP + "get/all";

            public static string Delete(int mcpId)
            {
                return $"{MCP}delete/{mcpId}";
            }

            public const string UPDATE_LOAD = MCP + "update-load";
        }

        public static class Message
        {
            private const string MESSAGE = "message/";

            public const string ADD = MESSAGE + "add";

            public static string Inbox(int userId)
            {
                return $"{MESSAGE}inbox/{userId}";
            }

            public const string INBOX_LATEST = MESSAGE + "inbox/latest";
        }

        public static class Route
        {
            private const string ROUTE = "route/";

            public const string ADD = ROUTE + "add";
            public const string UPDATE = ROUTE + "update";
        }

        public static class Settings
        {
            private const string SETTINGS = "settings/";

            public const string UPDATE_PASSWORD = SETTINGS + "update/password";
            public const string UPDATE_SETTINGS = SETTINGS + "update/settings";
        }

        public static class Task
        {
            private const string TASK = "task/";

            public const string ADD = TASK + "add";

            public static string GetAll(int employeeId)
            {
                return $"{TASK}get/all/{employeeId}";
            }

            public static string DeleteAll(int employeeId)
            {
                return $"{TASK}delete/all/{employeeId}";
            }

            public static string Delete(int taskId)
            {
                return $"{TASK}delete/{taskId}";
            }

            public const string UPDATE = TASK + "update";
            public const string GET_FREE_EMPLOYEES = TASK + "free";
        }

        public static class UserProfile
        {
            private const string USER_PROFILE = "userprofile/";

            public const string ADD_SUPERVISOR = USER_PROFILE + "add/supervisor";
            public const string ADD_CLEANER = USER_PROFILE + "add/cleaner";
            public const string ADD_DRIVER = USER_PROFILE + "add/driver";

            public static string Delete(int userProfileId)
            {
                return $"{USER_PROFILE}delete/{userProfileId}";
            }

            public const string UPDATE = USER_PROFILE + "update";
            public const string GET_ALL = USER_PROFILE + "all";

            public static string GetInfo(int userProfileId)
            {
                return $"{USER_PROFILE}info/{userProfileId}";
            }

            public static string GetAllWithRole(UserRole role)
            {
                return $"{USER_PROFILE}with-role/{role}";
            }
        }

        public static class Vehicle
        {
            private const string VEHICLE = "vehicle/";

            public const string ADD = VEHICLE + "add";
            public const string GET_ALL = VEHICLE + "get/all";
            public const string UPDATE = VEHICLE + "update";

            public static string Delete(int vehicleId)
            {
                return $"{VEHICLE}delete/{vehicleId}";
            }
        }
    }
}