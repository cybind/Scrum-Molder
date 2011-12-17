namespace eGo.ScrumMolder.Dto
{
    public class Enums
    {
        public struct UserType
        {
            public const int Unknow = 1;
            public const int Developer = 2;
            public const int Manager = 3;
            public const int Hr = 4;
            public const int Designer = 5;
        }

        public struct State
        {
            public const int Active = 1;
            public const int Resolved = 2;
        }

        public struct Reason
        {
            public const int Fixed = 1;
            public const int AsDesigned = 2;
            public const int CannotReproduce = 3;
            public const int Deferred = 4;
            public const int Duplicate = 5;
            public const int Obsolete = 6;
        }

        public struct Priority
        {
            public const int Major = 1;
            public const int Minor = 2;
            public const int Critical = 3;
        }
    }
}