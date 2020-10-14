<Query Kind="Program" />

public class Student
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int Grade { get; set; }
	public double GPA { get; set; }
	public int Year { get; set; }

	public override string ToString()
	{
		return $"{FirstName} {LastName}: {Grade}";
	}

}

void Main()
{
	var roster = new List<Student>();

	roster.Add(new Student { FirstName = "Bugs", LastName = "Bunny", Grade = 95, GPA = 3.4, Year = 11 });
	roster.Add(new Student { FirstName = "Daffy", LastName = "Duck", Grade = 76, GPA = 2.8, Year = 10 });
	roster.Add(new Student { FirstName = "Porky", LastName = "Pig", Grade = 85, GPA = 3.2, Year = 9 });
	roster.Add(new Student { FirstName = "Lola", LastName = "Bunny", Grade = 99, GPA = 3.95, Year = 11 });
	roster.Add(new Student { FirstName = "Elmer", LastName = "Fudd", Grade = 67, GPA = 2.3, Year = 12 });
	roster.Add(new Student { FirstName = "Tasmaniam", LastName = "Devil", Grade = 82, GPA = 3.1, Year = 12 });
	roster.Add(new Student { FirstName = "Honey", LastName = "Bunny", Grade = 93, GPA = 3.6, Year = 12 });
	roster.Add(new Student { FirstName = "Clyde", LastName = "Bunny", Grade = 72, GPA = 2.7, Year = 9 });
	roster.Add(new Student { FirstName = "Speedy", LastName = "Gonzales", Grade = 84, GPA = 3.4, Year = 9 });
	roster.Add(new Student { FirstName = "Yosemite", LastName = "Sam", Grade = 75, GPA = 2.9, Year = 10 });
	roster.Add(new Student { FirstName = "Petunia", LastName = "Pig", Grade = 93, GPA = 3.7, Year = 12 });
	roster.Add(new Student { FirstName = "Foghorn", LastName = "Leghorn", Grade = 95, GPA = 3.3, Year = 10 });
	
	// Example 1a query (query syntax) to retrieve all 12th graders (seniors)
	// var seniors = from student in roster where student.Year == 12 select student;

	// Example 1b query (method syntax) to retrieve all 12th graders (seniors)
//	var seniors = roster.Where(student => student.Year == 12);

	//Console.WriteLine("Seniors:");
	//DisplayOutput(seniors);
	
	// Example 2 grouping the results
//	Console.WriteLine("##### Group example ##### ");
//	var studentsWith3orHigherGPA = from student in roster where student.GPA >= 3.0 group student by student.Year;
//
//	foreach (var yearGroup in studentsWith3orHigherGPA)
//	{
//		int year = yearGroup.Key; // Key name for group
//		Console.WriteLine($"Year: {year}");
//
//		foreach (var currStudent in yearGroup)
//		{
//			Console.WriteLine(currStudent);
//		}
//	}

	// Example 3 returning an anonymous type
//	Console.WriteLine("##### Anonymous type example ##### ");
//	var seniorNames = from student in roster where student.Year == 12 select new { student.FirstName, student.LastName };
//	
//	string output = buildOutput(seniorNames);
//	Console.WriteLine("*****");
//	Console.WriteLine(output);
//	Console.WriteLine("*****");
//
//	DisplayAnonymousTypeOutput(seniorNames);


	// In-class exercises
	var values = new List<int>() { 5, 2, 2, -1, 10, 12, 7, 13, 3, -7, 8, 9};

	// Count the number of negative values in a list
	var numOfNeg = values.Count(x => x < 0);
	numOfNeg.Dump();
	
	var numOfNeg2 = values.Where(x => x < 0).Count();
	numOfNeg2.Dump();

	// Find the first negative value in a list
	var firstNeg = values.First(x => x < 0);
	firstNeg.Dump();

	// Sum only the negative values in a list
	var sumNegative = values.Where(x => x < 0).Sum();
	sumNegative.Dump();
	
	// Find the index of the first maximum element in the list
	var indexOfMax = values.IndexOf(values.Max());
	indexOfMax.Dump();

	// Compute the average GPA of all the students
	var averageGPA = roster.Average(student => student.GPA);

	// Compute the average GPA of just the seniors
	var averageSeniorGPA = roster.Where(student => student.Year ==12).Average(student => student.GPA);

	// Find the maximum grade of all the students


	// Find the maximum grade of all the juniors


	// Count the number of students whose current GPA is between 3.0 and 3.5, inclusive.
	var count = roster.Count(student => student.GPA >= 3.0 && student.GPA <= 3.5);

}

private string buildOutput(IEnumerable<Object> collection)
{
	string output = string.Empty;

	foreach (var currObject in collection)
	{
		output += currObject + Environment.NewLine;
	}

	return output;
}


public void DisplayOutput(IEnumerable<Student> collection)
{
	foreach (Student currStudent in collection)
	{
		Console.WriteLine(currStudent);
	}
}

public void DisplayAnonymousTypeOutput(IEnumerable<Object> collection)
{
	foreach (var currObject in collection)
	{
		Type type = currObject.GetType();

		PropertyInfo[] attributes = type.GetProperties();

		foreach (PropertyInfo property in attributes)
		{
			Console.Write(property.Name + ": " + property.GetValue(currObject) + " ");
		}

		Console.WriteLine();
	}
}