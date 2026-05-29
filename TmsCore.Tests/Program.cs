// ============================================
// ACTIVITY 2: Currency Converter
// ============================================

decimal amountETB = 3000m;
decimal usdRate = 0.0175m;
decimal eurRate = 0.0161m;

decimal usd = amountETB * usdRate;
decimal eur = amountETB * eurRate;

Console.WriteLine("=== Currency Converter (decimal) ===");
Console.WriteLine($"ETB: {amountETB:F2}");
Console.WriteLine($"USD: {usd:F2}");
Console.WriteLine($"EUR: {eur:F2}");

double amountDouble = 3000;
double usdDouble = amountDouble * 0.0175;
double eurDouble = amountDouble * 0.0161;

Console.WriteLine("\n=== Currency Converter (double) ===");
Console.WriteLine($"ETB (double): {amountDouble}");
Console.WriteLine($"USD (double): {usdDouble}");
Console.WriteLine($"EUR (double): {eurDouble}");

// ============================================
// ACTIVITY 3: Null Safety
// ============================================

string? name = null;
string? email = null;
string? region = null;
string? phone = null;
string? campus = null;

Console.WriteLine(name?.ToUpper() ?? "No name");
Console.WriteLine(email?.ToLower() ?? "No email");
Console.WriteLine(region ?? "No region");
Console.WriteLine(phone?.Trim() ?? "No phone");
campus ??= "5 Kilo Campus";
Console.WriteLine($"Campus: {campus}");
Console.WriteLine("\n5 potential NullReferenceException crashes prevented!");

// ============================================
// ACTIVITY 4: ToString() Override
// ============================================

var toStringStudent = new Student { Id = "STU-001", Name = "Abeba", Age = 20, GPA = 3.8m };
var toStringCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };

Console.WriteLine(toStringStudent);
Console.WriteLine(toStringCourse);

// ============================================
// ACTIVITY 5: Three LINQ Queries
// ============================================

List<Student> linqStudents = [
    new Student { Id = "S1", Name = "Abeba",    Age = 22, GPA = 3.8m, EnrolledAt = new DateTime(2024, 9, 1) },
    new Student { Id = "S2", Name = "Kidane",   Age = 21, GPA = 2.4m, EnrolledAt = new DateTime(2024, 9, 1) },
    new Student { Id = "S3", Name = "Dawit",    Age = 20, GPA = 3.1m, EnrolledAt = new DateTime(2023, 9, 1) },
    new Student { Id = "S4", Name = "Sara",     Age = 23, GPA = 3.9m, EnrolledAt = new DateTime(2023, 9, 1) },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m, EnrolledAt = new DateTime(2025, 9, 1) },
    new Student { Id = "S6", Name = "Yonas",    Age = 24, GPA = 3.5m, EnrolledAt = new DateTime(2025, 9, 1) },
    new Student { Id = "S7", Name = "Meron",    Age = 22, GPA = 1.8m, EnrolledAt = new DateTime(2024, 9, 1) },
    new Student { Id = "S8", Name = "Tesfaye",  Age = 21, GPA = 2.9m, EnrolledAt = new DateTime(2023, 9, 1) }
];

// Query 1: Names starting with "A"
var aNames = linqStudents
    .Where(s => s.Name.StartsWith("A"))
    .ToList();

Console.WriteLine("\n=== Names starting with A ===");
foreach (var s in aNames)
    Console.WriteLine($"  {s.Name} GPA: {s.GPA}");

// Query 2: Group by enrollment year
var byEnrollmentYear = linqStudents
    .GroupBy(s => s.EnrolledAt.Year);

Console.WriteLine("\n=== Students by Enrollment Year ===");
foreach (var group in byEnrollmentYear)
{
    Console.WriteLine($"{group.Key} ({group.Count()}):");
    foreach (var s in group)
        Console.WriteLine($"  {s.Name}, Enrolled At: {s.EnrolledAt.Year}");
}

// Query 3: Total grant for honors students
decimal totalHonorsGrant = linqStudents
    .Where(s => s.GPA >= 3.5m)
    .Sum(s => 5000m); // setting 5000m ETB as the grant amount for each honors student

Console.WriteLine($"\n=== Total Honors Grant ===");
Console.WriteLine($"Honors students: {linqStudents.Count(s => s.GPA >= 3.5m)}");
Console.WriteLine($"Total grant: {totalHonorsGrant:F2} ETB");