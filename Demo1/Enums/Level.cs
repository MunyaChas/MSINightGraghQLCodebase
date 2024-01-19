using System.Reflection;

namespace Demo1.Enums
{
    public sealed class Level : Enumeration
    {
        public static Level Beginner = new(1, nameof(Beginner));
        public static Level Intermediate = new(2, nameof(Intermediate));
        public static Level Advanced = new(3, nameof(Advanced));
        public static Level Expert = new(4, nameof(Expert));
        public Level()
        {

        }
        private Level(int id, string name) : base(id, name) { }
        public static int Count
        {
            get
            {
                return typeof(Level)
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(f => f.FieldType == typeof(Level))
                    .Count();
            }
        }
    }

    public sealed class Cluster : Enumeration
    {
        public static Cluster Cloud = new(1, nameof(Cloud));
        public static Cluster Data = new(2, nameof(Data));
        public static Cluster DevOps = new(3, nameof(DevOps));
        public static Cluster Engineering = new(4, nameof(Engineering));
        public static Cluster DCX = new(5, nameof(DCX));
        public static Cluster CCADotNet = new(6, nameof(CCADotNet));
        public static Cluster Security = new(7, nameof(Security));
        public static Cluster Testing = new(8, nameof(Testing));
        public static Cluster Frontend = new(9, nameof(Frontend));
        public static Cluster CCAJava = new(10, nameof(CCAJava));
        public static Cluster AI = new(11, nameof(AI));
        public static Cluster ProjectManagement = new(12, nameof(ProjectManagement));
        public static Cluster Other = new(14, nameof(Other));
        public Cluster()
        {

        }
        private Cluster(int id, string name) : base(id, name) { }
    }
}
