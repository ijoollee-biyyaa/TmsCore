public class EnrollmentService
{
    public EnrollementRecord ProcessRegistration(Student? student , Course? course)
    {
        // adding guard cluase -fail fast principle
        if(student is null)
        {
            throw new ArgumentNullException(nameof(student), "Student cannot be null.");
        }
        if(course is null)
        {
            throw new ArgumentNullException(nameof(course), "Course cannot be null.");
        }
        if(course.Capacity <= 0 || course.EnrolledCount >= course.Capacity)
        {
            throw new InvalidOperationException($"Course {course.Title} is at full capacity.");
        }

        // switch expression - classify students based on GPA
        string standing = student.GPA switch
        {
            >= 3.5m => "Honors",
            >= 2.5m => "Good Standing",
            _ => "Academic Warning"
        };
        Console.WriteLine($"{student.Name} is in {standing}.");

        // returning enrollment record
        return new EnrollementRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}