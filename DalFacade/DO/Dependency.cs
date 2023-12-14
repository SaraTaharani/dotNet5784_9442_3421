
namespace DO;
/// <summary>
/// An entity that describes a dependency between an engineer and a task
/// </summary>
/// <param name="Id">Unique identification number to hang</param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependsOnTask">Previous assignment ID number</param>
public record Dependency
(
    int Id,
    int? DependentTask,
    int? DependsOnTask
)
{
    public Dependency() : this(0,null,null) { }
}
