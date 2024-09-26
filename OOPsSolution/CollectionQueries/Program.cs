// See https://aka.ms/new-console-template for more information
using OOPsReview;

Console.WriteLine("\n\tCollection Queries\n");

List<Employment> employments = CreateCollection();

//you could use var instead of Employment
//using var: the datatype is resolved at run time
//using datatype: the code is resolved at compile time
foreach(Employment item in employments)
{
    Console.WriteLine($"{item}");
}

Console.WriteLine($"\nThe number of items in the collection is " +
    $"{employments.Count}\n");

//locate an item in the collection
//sequencial search

//variable to hold the found item
Employment foundEmployment = null;

//iteration of the collection
//for(int i = 0; i < employments.Count; i++)
//{
//    if (employments[i].Title.Equals("PG II"))
//    {
//        foundEmployment = employments[i];
//        //quick exit of the loop
//        i = employments.Count;
//    }
//}

//here "item" holds an instance of the collection
//the collection is traversed from start to end
//this is a pre-test loop
foreach(var item in employments)
{
    if (item.Title.Equals("PG II"))
    {
        foundEmployment = item;
    }
}

//test to determine the results of the search
TestForFoundItem(foundEmployment,"PG II");

//is there an easier way to locate an item in a collection
foundEmployment = null;
//collection such as List<T> have methods already created for basic functionality
//one such method is .Find(predicate)
//a predicate is the condition your are using as if it was in a loop
//the predicate uses the lamda symbol in it's grammar
//  syntax:  placeholder => condition(s)
//example: locate an item within the employments

//.Equals(condition) is exact match
//found
foundEmployment = employments.Find(e => e.Title.Equals("PG II"));
TestForFoundItem(foundEmployment, "PG II");

//not found
foundEmployment = employments.Find(e => e.Title.Equals("PG"));
TestForFoundItem(foundEmployment,"PG");


//.Contains(condition) looks for the condition somewhere within your data
foundEmployment = employments.Find(e => e.Title.Contains("PG"));
TestForFoundItem(foundEmployment, "PG");

//the .Any and .All return a boolean result instance of the actual instance
//problem: do not care about the actual data, just need to known
//          if it exists
SupervisoryLevel argument = SupervisoryLevel.TeamLeader;
if (employments.Any(e => e.Level == argument))
{
    Console.WriteLine($"\nEmployee was once a {argument}");
}
else
{
    Console.WriteLine($"\nEmployee was never a {argument}");
}

//there is another set of commands that can be used that look very
//  similar to sql query commands and clauses

//this software is call Linq
//use a search for data method: .Where()
//the Where method assumes by default that a collection is returned
//By default the return datatype is one of two generic collection types:
//      a) IQueryable or b) IEnumerable
//within C# and typically using a List<T>, you will need to convert
//  the returned data collection into a list using : .ToList()
List<Employment> foundCollection = null;
foundCollection = employments.Where(e => e.Title.Contains("PG"))
                              .ToList();
foreach(var item in foundCollection)
{
    Console.WriteLine($"\n Av employment of PG was found " +
           $"{item}");
}


static List<Employment> CreateCollection()
{
    List<Employment> newCollection = new List<Employment>();
    newCollection.Add(new Employment("PG I", SupervisoryLevel.Entry,
                        DateTime.Parse("May 1, 2010"), 0.5));
    newCollection.Add(new Employment("PG II", SupervisoryLevel.TeamMember,
                    DateTime.Parse("Nov 1, 2010"), 3.2));
    newCollection.Add(new Employment("PG III", SupervisoryLevel.TeamLeader,
                    DateTime.Parse("Jan 6, 2014"), 8.6));
    newCollection.Add(new Employment("SP I", SupervisoryLevel.Supervisor,
                    DateTime.Parse("Jul 22, 2022"), 2.25));
    return newCollection;
}

static void TestForFoundItem(Employment foundEmployment, string searcharg)
{
    if (foundEmployment == null)
    {
        Console.WriteLine($"\nPerson had no {searcharg} employment.");
    }
    else
    {
        Console.WriteLine($"\n A {searcharg} employment found; {foundEmployment}" +
            $" (via a loop)");
    }
}
