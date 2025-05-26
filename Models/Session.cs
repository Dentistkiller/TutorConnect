using System;
using System.Collections.Generic;

namespace TutorConnect.Models;

public partial class Session
{
    public int SessionId { get; set; }

    public int StudentId { get; set; }

    public int TutorId { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public virtual Student Student { get; set; } = null!;

    public virtual Tutor Tutor { get; set; } = null!;
}
