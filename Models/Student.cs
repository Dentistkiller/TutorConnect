using System;
using System.Collections.Generic;

namespace TutorConnect.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
