<Query Kind="Program" />

  public class Student : IComparable<Student>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int Grade { get; set; }
	public double GPA { get; set; }
	public int Year { get; set; }

	public int CompareTo(Student other)
	{
		return this.Grade.CompareTo(other.Grade);
	}

	public override string ToString()
	{
		return $"{FirstName} {LastName}: {Grade}";
	}

}

public class LastNameComparer : IComparer<Student>
{
	public int Compare(Student student1, Student student2)
	{
		return student1.LastName.CompareTo(student2.LastName);
	}
}

public class Roster : ICollection<Student>
{
	private ICollection<Student> students;
	//type as ICollection to type with minimum requirements
	
	public int Count => students.Count;

	public bool IsReadOnly => students.IsReadOnly;

	//	 public Student this[int i] {
	//	 	get => ((IList<Student>)students)[i];
	//		set => ((IList<Student>)students)[i] = value;
	//		//cast to IList so that you can use the indexer when typed as Collection
	//	 }


	public Roster()
	{
		this.students = new List<Student>();
	}
	
	public void Add(Student student)
	{
		if (student == null)
		{
			throw new NullReferenceException(nameof(student));
		}
		
		this.students.Add(student);
	}

	public IEnumerator<Student> GetEnumerator()
	{
		return students.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)students).GetEnumerator();
	}

	public void Clear()
	{
		students.Clear();
	}

	public bool Contains(Student item)
	{
		return students.Contains(item);
	}

	public void CopyTo(Student[] array, int arrayIndex)
	{
		students.CopyTo(array, arrayIndex);
	}

	public bool Remove(Student item)
	{
		return students.Remove(item);
	}
}

public void DisplayRoster(Roster roster)
{
	Console.WriteLine("Count: " + roster.Count);
	foreach (Student currStudent in roster)
	{
		Console.WriteLine(currStudent);
	}
	//for (int i = 0; i < roster.Count; i++)
	//{
	//	Console.WriteLine(roster[i]);
	//}

}

void Main()
{
//	DemoSortingPrimitiveCollections();

	var roster = new Roster();
//	List<Student> roster = new List<Student>();
	
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

//	roster.Sort();
//	roster.Sort(new LastNameComparer());
//	roster.Sort((student1, student2) =>
//		student1.LastName.Equals(student2.LastName) ? student1.FirstName.CompareTo(student2.FirstName) : student1.LastName.CompareTo(student2.LastName));
		
	DisplayRoster(roster);

//	var sortByGrade = roster.OrderBy(student => student.Grade);
//	DisplayRoster(sortByGrade);

//	DemoSortingUserCollectionWithLinq(roster);
}


void DemoSortingUserCollectionWithLinq(List<Student> roster)
{
	var sortByGrade = roster.OrderBy(student => student.Grade);
	
	Console.WriteLine("##### Result set after ordering via Linq #####");
	sortByGrade.Dump();

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


public void DemoSortingPrimitiveCollections()
{
	var values = new List<int>() { 5, 2, 2, -1, 10, 12, 7, 13, 3, -7, 8, 9 };
	values.Sort();
	values.Dump();

	var names = new List<string>() { "Sue", "Bob", "Mary", "John", "Eleanor", "Max" };
	names.Sort();
	names.Dump();

	var data = new int[] { 10, 13, 2, -5, 2, 15, 8, 7, 6, 3, 1, 3 };
	Array.Sort(data);
	data.Dump();
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