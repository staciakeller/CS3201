<Query Kind="Program">
  <Namespace>System.Diagnostics.CodeAnalysis</Namespace>
</Query>

public class Student : IComparable<Student>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int Grade { get; set; }
	public double GPA { get; set; }
	public int Year { get; set; }

	public int CompareTo(Student other)
	{
		//if (this.Grade > other.Grade)
		//{
		//	return 1;
		//} 
		//else if (this.Grade < other.Grade)
		//{
		//	return -1;
		//} 
		//else 
		//{
		//	return 0;
		//}
		return this.Grade - other.Grade;
	}

	public override string ToString()
	{
		return $"{FirstName} {LastName}: {Grade}";
	}

}

public class GPADescendingComparer : IComparer<Student>
{
	public int Compare(Student student1, Student student2)
	{
		return student2.GPA.CompareTo(student1.GPA);
	}
}

public class LastThenFirstNameComparer : IComparer<Student>
{
	public int Compare(Student student1, Student student2)
	{
		String lastName1 = student1.LastName;
		String lastName2 = student2.LastName;
		if (lastName1.Equals(lastName2,
		StringComparison.CurrentCultureIgnoreCase))
		{
			return student1.FirstName.CompareTo(student2.FirstName);
		}
		else
		{ // Just compare on last name
			return lastName1.CompareTo(lastName2);
		}
	}
}

public void DemoSortingPrimitiveCollections()
{
	var values = new List<int>() { 5, 2, 2, -1, 10, 12, 7, 13, 3, -7, 8, 9 };
	values.Sort();
	values.Dump();
	
	var names = new List<string>() {"Sue", "Bob", "Mary", "John", "Eleanor", "Max"};
	names.Sort();
	names.Dump();

	var data = new int[] {10, 13, 2, -5, 2, 15, 8, 7, 6, 3, 1, 3};
	Array.Sort(data);
	data.Dump();

}

void Main()
{
//	DemoSortingPrimitiveCollections();
	
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
	
	roster.Sort(new GPADescendingComparer());
	roster.Dump();
	
	//DemoSortingUserCollection(roster);
}

void DemoSortingUserCollection(List<Student> roster)
{
	var sortByGrade = roster.Select(student => student).OrderBy(student => student.Grade);
	var sortByGradeV2 = roster.OrderBy(student => student.Grade);

	Console.WriteLine("##### Result set after ordering #####");
	sortByGrade.Dump();

	Console.WriteLine("##### Result set after ordering Version 2 #####");
	sortByGradeV2.Dump();

	Console.WriteLine("##### Roster unchanged ordering via LINQ #####");
	roster.Dump();

	var sortedLastThenFirst =
	   roster.OrderBy(student => student.LastName).ThenBy(student
		=> student.FirstName);

	Console.WriteLine("##### Result set of last and first ordering #####");
	sortedLastThenFirst.Dump();

	Console.WriteLine("##### Roster not changed after ordering with LINQ #####");
	roster.Dump();
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