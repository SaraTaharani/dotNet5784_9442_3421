﻿namespace BlApi;
public interface IBI
{
    public ITask Task{ get; }
    public IMilestone Milestone { get; }
    public IEngineer Engineer { get; }
}
