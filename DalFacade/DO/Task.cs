namespace DO;

/// <summary>
/// An engineer's mission statement entity
/// </summary>
/// <param name="Id">A unique identification number for the task</param>
/// <param name="Description">Description of task</param>
/// <param name="Alias">nickname of task</param>
/// <param name="Milestone">Milestone of task</param>
/// <param name="CreatedAt">Creation date of task</param>
/// <param name="Start"> Start date of task</param>
/// <param name="ForecastDate">Estimated date of completion of task</param>
/// <param name="Deadline">Final date for completion of task</param>
/// <param name="Complete">Actual end date of task</param>
/// <param name="Deliverables">Product of task</param>
/// <param name="Remarks">Notes for task</param>
/// <param name="EngineerId">The ID of the engineer assigned to the task</param>
/// <param name="">Difficulty level of task</param>
public record Task
(
   int Id,
    string? Description = null,
    string? Alias = null,
    bool? Milestone = null,
    DateTime? CreatedAt = null,
    DateTime? Start = null,
    DateTime? ForecastDate = null,
    DateTime? Deadline = null,
    DateTime? Complete = null,
    string? Deliverables = null,
    string? Remarks = null,
    int? EngineerId = null,
    EngineerExperience? ComlexityLevel = null,
    bool Active = true
)


{
       public Task() : this(0) { }

}

