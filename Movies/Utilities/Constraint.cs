namespace Movies.Utilities
{
    public static class Constraint
    {
        public static class FilterName
        {
            public static string CATEGORY = "category";
            public static string RECENT_UPDATE = "recentupdate";
            public static string NATION = "nation";
            public static string ACTOR = "actor";
            public static string PRODUCER = "producer";
            public static string FEATURE = "feature";
            public static string NAME = "name";
        }

        public static class SortName
        {
            public static string PRODUCED_DATE = "produceddate";
            public static string CREATED_DATE = "createddate";
            public static string NAME = "name";
        }

        public static class StatusMovie
        {
            public static string UPCOMING = "Upcoming";
            public static string PENDING = "Pending";
            public static string RELEASE = "Release";
            public static string DELETED = "Deleted";
            public static string ALL_STATUS = "All";
            public static IEnumerable<string> ALL = new List<string> { UPCOMING, PENDING, RELEASE, DELETED };
        }

        public static class RolePerson
        {
            public static string ACTOR = "ACTOR";
            public static string PRODUCER = "PRODUCER";
        }

        public static class RoleUser
        {
            public const string ADMIN = "ADMIN";
            public const string USER = "USER";
        }

        public static class StatusUser
        {
            public static string ACTIVE = "ACTIVE";
            public static string PENDING = "PENDING";
            public static string BLOCK = "BLOCK";
        }

        public static class Resource
        {
            public static string CONFIRM_MAIL = "Resources/ConfirmMail.html";
            public static string ERROR_MAIL = "Resources/ErrorMail.html";
        }
    }
}
