public class DependencyOne : IDependencyOne
{
    private IDependencyTwo dependencyTwo;
    public DependencyOne(IDependencyTwo dependencyTwo)
    {
        this.dependencyTwo = dependencyTwo;
    }
    public void DoSomething(string something)
    {
        //Use dependencyTwo and DoSomething
    }
}