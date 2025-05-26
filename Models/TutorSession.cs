using System;
using System.Collections.Generic;

namespace TutorConnect.Models;

public partial class TutorSession
{
    public int SessionId { get; set; }

    public DateTime SessionDate { get; set; }

    public int DurationMinutes { get; set; }

    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentEmail { get; set; } = null!;

    public string? StudentPhone { get; set; }

    public int TutorId { get; set; }

    public string TutorName { get; set; } = null!;

    public string TutorEmail { get; set; } = null!;

    public string TutorSubject { get; set; } = null!;
}
