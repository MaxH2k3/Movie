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
        
        public static class StatusMovie
        {
            public static string PENDING = "PENDING";
            public static string RELEASE = "RELEASE";
        }

        public static class RolePerson
        {
            public static string ACTOR = "AC";
            public static string PRODUCER = "PR";
        }
    }
}
