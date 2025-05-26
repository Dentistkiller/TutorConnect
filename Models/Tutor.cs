using System;
using System.Collections.Generic;

namespace TutorConnect.Models;

public partial class Tutor
{
    public int TutorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
