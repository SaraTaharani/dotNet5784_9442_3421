using System;

namespace DO;
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Cost"></param>
/// <param name="Level"></param>
/// <param name="active"></param>
public record Engineer
(
    int Id,
    string Name,
    string Email,
    int Cost,
    EngineerExperience Level,
    bool active=true
);
