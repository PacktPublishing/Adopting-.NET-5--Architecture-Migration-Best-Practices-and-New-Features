<Query Kind="Program" />


void Main()
{
	void MakeHttpCall(string url, out long timeTook) =>
	            timeTook = 100;
	
    void MakeCallToPackt()
    {
        MakeHttpCall("www.packt.com",out _);
    }
	
	MakeCallToPackt();
	var items = new List<string>{"Item1","Item2","Item3"};
	items.ForEach(_ => Console.WriteLine("Another item processed"));
}

// You can define other methods, fields, classes and namespaces here
