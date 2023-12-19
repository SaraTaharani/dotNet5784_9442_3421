 

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 100;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependencyId = 250;
        private static int nextDependencyId = startTaskId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal static DateTime? dateBeginProject = null;
        internal static DateTime? dateEndProject = null;

    }
    internal static List<DO.Dependency?> Dependencies { get; } = new();
    internal static List<DO.Engineer?> Engineers { get; } = new();
    internal static List<DO.Task?> Tasks { get; } = new();
}

