using Commons.Types;

public static class Endpoints
{
    public const string DOMAIN = "test11111a435345.azurewebsites.net/";

    public static class Account
    {
        private const string ACCOUNT = "account/";

        public const string LOGIN = DOMAIN + ACCOUNT + "login/";
        public const string REGISTER = DOMAIN + ACCOUNT + "register/";
    }

    public static class Administration
    {
        private const string ADMINISTRATION = "administration/";

        public static string DeleteAccount(int accountId)
        {
            return DOMAIN + ADMINISTRATION + $"delete-account/{accountId}";
        }
    }

    public static class DrivingHistory
    {
        private const string DRIVING_HISTORY = "drivinghistory/";

        public const string ADD = DOMAIN + DRIVING_HISTORY + "add";
        public const string ALL = DOMAIN + DRIVING_HISTORY + "all";

        public static string Delete(int drivingHistoryId)
        {
            return DOMAIN + DRIVING_HISTORY + $"delete/{drivingHistoryId}";
        }
    }

    public static class DrivingLicense
    {
        private const string DRIVING_LICENSE = "drivinglicense/";

        public const string ADD = DOMAIN + DRIVING_LICENSE + "add";

        public const string UPDATE = DOMAIN + DRIVING_LICENSE + "update";

        public const string DELETE_OUTDATED = DOMAIN + DRIVING_LICENSE + "delete-outdated";

        public static string Get(int driverId)
        {
            return DOMAIN + DRIVING_LICENSE + $"get/{driverId}";
        }

        public static string Delete(int drivingLicenseId)
        {
            return DOMAIN + DRIVING_LICENSE + $"delete/{drivingLicenseId}";
        }
    }

    public static class Mcp
    {
        private const string MCP = "mcp/";

        public const string ADD = DOMAIN + MCP + "add";

        public const string GET_FULL = DOMAIN + MCP + "get/full";
        public const string GET_IN_RANGE = DOMAIN + MCP + "get/in-range";
        public const string GET_ALL = DOMAIN + MCP + "get/all";

        public const string UPDATE_LOAD = DOMAIN + MCP + "update-load";

        public static string Empty(int mcpId)
        {
            return DOMAIN + MCP + $"empty/{mcpId}";
        }

        public static string Delete(int mcpId)
        {
            return DOMAIN + MCP + $"delete/{mcpId}";
        }
    }

    public static class Message
    {
        private const string MESSAGE = "message/";

        public const string ADD = DOMAIN + MESSAGE + "add";

        public static string InboxLatest(int userId)
        {
            return DOMAIN + MESSAGE + $"inbox/latest/{userId}";
        }

        public static string InboxWith(int senderId, int receiverId)
        {
            return DOMAIN + MESSAGE + $"inbox/{senderId}/{receiverId}";
        }
    }

    public static class Route
    {
        private const string ROUTE = "route/";

        public const string ADD = DOMAIN + ROUTE + "add";
        public const string UPDATE = DOMAIN + ROUTE + "update";
    }

    public static class Settings
    {
        private const string SETTINGS = "settings/";

        public const string UPDATE_PASSWORD = DOMAIN + SETTINGS + "update/password";
        public const string UPDATE_SETTINGS = DOMAIN + SETTINGS + "update/settings";
    }

    public static class Task
    {
        private const string TASK = "task/";

        public const string ADD = DOMAIN + TASK + "add";

        public const string UPDATE = DOMAIN + TASK + "update";
        public const string GET_FREE_EMPLOYEES = DOMAIN + TASK + "free";

        public static string GetAll(int employeeId)
        {
            return DOMAIN + TASK + $"get/all/{employeeId}";
        }

        public static string DeleteAll(int employeeId)
        {
            return DOMAIN + TASK + $"delete/all/{employeeId}";
        }

        public static string Delete(int taskId)
        {
            return DOMAIN + TASK + $"delete/{taskId}";
        }
    }

    public static class UserProfile
    {
        private const string USER_PROFILE = "userprofile/";

        public const string ADD_SUPERVISOR = DOMAIN + USER_PROFILE + "add/supervisor";
        public const string ADD_CLEANER = DOMAIN + USER_PROFILE + "add/cleaner";
        public const string ADD_DRIVER = DOMAIN + USER_PROFILE + "add/driver";

        public const string UPDATE = DOMAIN + USER_PROFILE + "update";
        public const string GET_ALL = DOMAIN + USER_PROFILE + "all";

        public static string Delete(int userProfileId)
        {
            return DOMAIN + USER_PROFILE + $"delete/{userProfileId}";
        }

        public static string GetInfo(int userProfileId)
        {
            return DOMAIN + USER_PROFILE + $"info/{userProfileId}";
        }

        public static string GetAllWithRole(UserRole role)
        {
            return DOMAIN + USER_PROFILE + $"with-role/{role}";
        }
    }

    public static class Vehicle
    {
        private const string VEHICLE = "vehicle/";

        public const string ADD = DOMAIN + VEHICLE + "add";
        public const string GET_ALL = DOMAIN + VEHICLE + "get/all";
        public const string GET_ALL_LOCATION = DOMAIN + VEHICLE + "get/all/location";
        public const string UPDATE = DOMAIN + VEHICLE + "update";

        public static string Delete(int vehicleId)
        {
            return DOMAIN + VEHICLE + $"delete/{vehicleId}";
        }
    }
}