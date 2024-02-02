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
            public static string RECOMMEND = "recommend";
            public static string DELETED = "deleted";
        }

        public static class SortName
        {
            public static string PRODUCED_DATE = "produceddate";
            public static string CREATED_DATE = "createddate";
            public static string DELETED_DATE = "deleteddate";
            public static string NAME = "name";
            public static IEnumerable<string> ALL = new List<string> { PRODUCED_DATE, CREATED_DATE, DELETED_DATE, NAME };
        }

        public static class StatusMovie
        {
            public static string UPCOMING = "upcoming";
            public static string PENDING = "pending";
            public static string RELEASE = "released";
            public static string DELETED = "deleted";
            public static string ALL_STATUS = "all";
            public static string REVERT = "revert";
            public static IEnumerable<string> ALL = new List<string> { UPCOMING.ToLower(), PENDING.ToLower(), RELEASE.ToLower(), ALL_STATUS.ToLower() };
            public static IEnumerable<string> FILTER = new List<string> { UPCOMING.ToLower(), PENDING.ToLower(), RELEASE.ToLower(), REVERT.ToLower() };
            
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
