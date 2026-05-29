using System;
using System.Diagnostics;
Debug.WriteLine("=== TMS Console App ===");
// Exercise 6: Connection Dropping Under Load (LO 1.7: Async/Await)
// the wrong way 
var sw = Stopwatch.StartNew();
for (int i=0; i < 5; i++)
{
    Thread.Sleep(300);
}
System.Console.WriteLine($"Blocking sequential: {sw.ElapsedMilliseconds}ms");

//ASYNC but sequential - thread released but one at a time
sw.Restart();
for (int i=0; i < 5; i++)
{
    await Task.Delay(300);
}
Console.WriteLine($"Async sequential: {sw.ElapsedMilliseconds}ms");

// Right way
sw.Restart();
var tasks = Enumerable.Range(0,5).Select(_=> Task.Delay(300));
await Task.WhenAll(tasks);
Console.WriteLine($"Async parallel: {sw.ElapsedMilliseconds}ms");

// Exercise 6- step-2:  Build the TMS Student Fetcher
async Task<Student> FetchStudentAsync(string id)
{
    Console.WriteLine($"Fetching {id}...");
    await Task.Delay(300);
    return new Student
    {
      Id = id,
      Name = $"Student-{id}",
        Age = 20,
        GPA = id switch
        {
            "S1" => 3.8m,
            "S2" => 2.4m,
            "S3" => 3.5m,
            "S4" => 1.9m,
            "S5" => 3.2m,
            _ => 2.5m
        }  
    };
}

async Task<Course> FetchCourseAsync(string code)
{
    Console.WriteLine($"  Fetching course {code}...");
    await Task.Delay(200);
    return new Course
    {
        Code = code,
        Title = $"Course-{code}",
        Capacity = code switch
        {
            "CRS-101" => 2,
            "CRS-201" => 30,
            "CRS-301" => 15,
            _         => 25
        }
    };
}
// step -3 : load in parallel

sw.Restart();
string[] studentIds = {"S1", "S2", "S3", "S4", "S5"};
string[] courseCodes = {"CRS-101", "CRS-201", "CRS-301"};

var StudentTasks = studentIds.Select(id => FetchStudentAsync(id));
var CourseTasks = courseCodes.Select(code => FetchCourseAsync(code));

Student[] students = await Task.WhenAll(StudentTasks);
Course[] courses = await Task.WhenAll(CourseTasks);

Console.WriteLine($"\n Loaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");

foreach (var s in students)
{
    Console.WriteLine($": {s.Name}, GPA: {s.GPA}");
}


// Exercise 6  - part B - Enrollement Engine
var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 };
var enrollService = new EnrollmentService();
var enrollments = new List<EnrollementRecord>();
var failures = new List<string>();

sw.Restart();
foreach (var student in students)
{
    try
    {
        var record = enrollService.ProcessRegistration(student, enrollCourse);
        enrollCourse.EnrolledCount++;
        enrollments.Add(record);
        Console.WriteLine($"  Enrolled: {student.Name}");
    }
    catch (InvalidOperationException ex)
    {
        failures.Add($"{student.Name}: {ex.Message}");
        Console.WriteLine($"  Rejected: {student.Name} - {ex.Message}");
    }
}


// Exercise 7 - The Unhelpful Crash (LO 1.8: Exceptions and Custom Faults)
try
{
    var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Test", Capacity = 1 };
    overflowCourse.EnrolledCount = 1; // Simulate full course
    enrollService.ProcessRegistration(
        new Student { Id = "S99", Name = "Test", Age = 20, GPA = 3.0m },
        overflowCourse
    );
}
catch (CapacityReachedException ex)
{
    Console.WriteLine($"\nDomain exception caught:");
    Console.WriteLine($"  Course: {ex.CourseCode}");
    Console.WriteLine($"  Message: {ex.Message}");
}

// enrollement summary report
sw.Stop();

decimal classAverage = students.Length > 0 ? students.Average(s=>s.GPA) : 0m;
Console.WriteLine("\n========== ENROLLMENT SUMMARY ==========");
Console.WriteLine($"Total students loaded: {students.Length}");
Console.WriteLine($"Successful enrollments: {enrollments.Count}");
Console.WriteLine($"Failed enrollments: {failures.Count}");
Console.WriteLine($"Class average GPA: {classAverage:F2}");
Console.WriteLine($"Total elapsed time: {sw.ElapsedMilliseconds}ms");

if (failures.Count > 0)
{
    Console.WriteLine("\n--- Failure Details ---");
    foreach (var failure in failures)
    {
        Console.WriteLine($"  {failure}");
    }
}

Console.WriteLine("========================================");

// Create lamba that print sms message and attach it to service and call FinilizeEnrollment

var auditService = new EnrollmentService();
auditService.Listener = s =>
Console.WriteLine($"SMS SENT: Welcome to the Tms, {s.Name}!");

var auditStudent = new Student {Id = "S1", Name ="Abeba", Age =20, GPA = 3.8m};
auditService.FinilizeEnrollment(auditStudent);