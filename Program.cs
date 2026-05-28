Console.WriteLine("=== TMS Console App ===");
// Exercise 1: The First Safety Net (LO 1.1: Environment + Null Safety)
// step -1 - Legacy Bug - the compiler shows warning CS8600 and CS8602
 // string region = null;
 // System.Console.WriteLine(region.ToUpper());

  // step-2 - Fix: using 3 ways
  //1. Null-conditional operator
  string? region = null;
  string? upperRegion = region?.ToUpper();
  System.Console.WriteLine($"Region (conditional): {upperRegion}");

    //2. Null-coalescing operator

    string displayRegion = region ?? "Unassigned";
    System.Console.WriteLine($"Region (coalesced): {displayRegion}");

    //3. Null-coalescing assignment operator
    region ??= "Addis Ababa";
    System.Console.WriteLine($"Region (assigned): {region}");

// step-3 - Declare your First TMS variable.
string studName = "Abeba";
string studentId = "STU-001";
int enrollementCount = 3;
decimal grantAmount = 1999.99m;
DateTime enrolledAt = DateTime.UtcNow;
string? campusRegion = null;

System.Console.WriteLine($"Student: {studName} ({studentId})");
System.Console.WriteLine($"Course: {enrollementCount}");
Console.WriteLine($"Grant: {grantAmount:F2}");
Console.WriteLine($"Enrolled: {enrolledAt:yyyy-MM-dd}");
Console.WriteLine($"Campus: {campusRegion ?? "Not assigned"}");

// Exercise 2: The Ministry Audit Failure (LO 1.2: Primitives)

// step -1 - Legacy Bug - the compiler shows warning CS8600 and CS8602
// double grantPerStudent = 1999.99;
// double totalAllocation = grantPerStudent * 100_000;
// Console.WriteLine($"Total allocated (double): {totalAllocation}");

// step-2 - Fix: using decimal type
decimal grantPerStudent = 1999.99m;
decimal totalAllocation = grantPerStudent * 100_000m;
System.Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Total allocated (formatted): {totalAllocation:F2}");

// Exercise 3: Pipeline Data Corruption (LO 1.3 & 1.4: Encapsulation)


var enrollement = new EnrollementRecord("STU-001", "CS-401", DateTime.UtcNow);
System.Console.WriteLine(enrollement);

//enrollement.CourseCode = "Hacked"; // this shows init-only property.

 var corrected = enrollement with {CourseCode = "CS-402"};
 System.Console.WriteLine(corrected);

 // checking their equality value
 var duplicate = new EnrollementRecord("STU-001", "CS-401", enrollement.enrolledAt);
 System.Console.WriteLine($"same data? {enrollement == duplicate}");

 //Exercise 3 - part2: Course capacity with the field keyword

var course = new Course{ Code = "CS-401", Title = "Advanced C#", Capacity = 30};
System.Console.WriteLine($"Course: {course.Title}  Capacity: {course.Capacity}");

try
{
    course.Capacity = -5;

}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
try
{
    course.Title = " ";
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}

//Exercise 3 — Part 3: Student Model
var s = new Student {Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m};
// new Student { Id = "S2", Name = "", Age = 20, GPA= 3.0m };
// new Student { Id = "S3", Name = "Test", Age = 12, GPA = 3.0m };
// new Student { Id = "S4", Name = "Test", Age = 20, GPA = 5.0m }
System.Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");

//Exercise 3B: Interface Contract Wiring (LO 1.4: OOP Contracts)
void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("=== Grade Report ===");
    foreach (var item in assessments)
    {
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
    }
}
IGradable[] cohortAssessments = [
    new Quiz {Title ="C# Basics", CorrectAnswers = 18, TotalQuestions = 20},
    new LabAssignment {Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m}
];
PrintGradeReport(cohortAssessments);