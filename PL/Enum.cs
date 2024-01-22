using System.Collections.Generic;
using System;
using System.Collections;
namespace PL;
internal class EngineerExperiencesCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
    (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}internal class StatusTaskCollection : IEnumerable
{
    static readonly IEnumerable<BO.Status> t_enums =
    (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => t_enums.GetEnumerator();
}