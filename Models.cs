//Domain models will be added here
public record EnrollementRecord(string StudentId, string CourseCode, DateTime enrolledAt);

// course class
public class Course
{
    public required string Code { get; init; }
    public required string Title { get; set =>field= !string.IsNullOrWhiteSpace(value) ? value :
    throw new ArgumentException("Title cannot be empty or whitespace", nameof(value)); }

    public int Capacity
    {
        get;
        set => field = value > 0 ? value :throw new ArgumentOutOfRangeException(nameof(value), "System Constraint: Capacity must be greater than zero.");
    }
        public int EnrolledCount{get;set;}

// we used for activity 4 - ToString() override
public override string ToString()
    => $"Course: {Title} ({Code}), Capacity: {Capacity}, Enrolled: {EnrolledCount}";

    }

// student model
public class Student
{
    public required string Id {get; init;}

    public required string Name {get; 
    set => field = !string.IsNullOrWhiteSpace(value) ? value :
     throw new ArgumentException("Name cannot cannot be empty or whitespace", nameof(value)); }
     public int Age
    {
        get;
        set => field = value is >= 16 and <= 100 ? value :
        throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
    }
    public decimal GPA
    {
        get;
        set=> field = value is >= 0.0m and <= 4.0m ? value:
        throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0 and 4.0.");
    }

    public DateTime EnrolledAt {get; set;} = DateTime.UtcNow;
// we used for activity 4 - ToString() override
  public override string ToString()
    => $"Student: {Name} ({Id}), Age: {Age}, GPA: {GPA}, Enrolled At: {EnrolledAt}";
}

// define IGradable interface
public interface IGradable
{
    string Title { get; }
    decimal CalculateGrade();
}

// quiz class implementing IGradable
public class Quiz : IGradable
{
    public required string Title { get; init; }
    public int CorrectAnswers { get; init; }
    public int TotalQuestions { get; init; }

    public decimal CalculateGrade()
    {
        if (TotalQuestions == 0) return 0.0m; // Avoid division by zero
        return (decimal)CorrectAnswers / TotalQuestions * 100m;
    }

}
// LabAssignment class implementing IGradable
public class LabAssignment : IGradable
{
    public required string Title {get; init;}
    public required decimal FunctionalityScore {get; init;}
    public required decimal CodeQualityScore {get; init;}

    public decimal CalculateGrade()
    {
        // 70 % functionality and 30% code quality
        return (FunctionalityScore * 0.7m + CodeQualityScore * 0.3m);
    }
}

//custom Exception 
public class TmsDatabaseException : Exception
{
    public string Operation { get; }
    public TmsDatabaseException(string operation, string message) : base(message)
    {
        Operation = operation;
    }

    public TmsDatabaseException(string operation, string message, Exception innerException)
        : base(message, innerException)
    {
        Operation = operation;
    }
}

public class CapacityReachedException : InvalidOperationException
{
    public string CourseCode { get; }

    public CapacityReachedException(string courseCode)
        : base($"Course {courseCode} has reached maximum capacity.")
    {
        CourseCode = courseCode;
    }

    public CapacityReachedException(string courseCode, Exception innerException)
        : base($"Course {courseCode} has reached maximum capacity.", innerException)
    {
        CourseCode = courseCode;
    }
}

