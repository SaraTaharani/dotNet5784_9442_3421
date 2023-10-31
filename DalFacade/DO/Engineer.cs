using System;

namespace DO;

public record Engineer
(
    int Id,
    string Name,
    string Email,
    int Cost,
    EngineerExperience Level
);
