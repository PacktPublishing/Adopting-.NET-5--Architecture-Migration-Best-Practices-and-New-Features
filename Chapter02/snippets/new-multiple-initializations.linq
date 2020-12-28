<Query Kind="Program" />

void Main()
{
    Dictionary < string,List<string> > citiesInCountries = new()
    {
        { "USA", new() { "New York", "Los Angeles", "Chicago", "Houston" } },
        {"Australia", new() { "Sydney", "Melbourne", "Brisbane"} },
        { "Canada", new() { "Ottawa","Toronto","Edmonton" } }
	};

	Console.WriteLine(citiesInCountries["USA"][0]);		

}

