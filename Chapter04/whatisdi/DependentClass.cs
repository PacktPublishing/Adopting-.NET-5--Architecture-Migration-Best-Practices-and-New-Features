public class DependentClass : ISomeUsecase
{
    private IDependencyOne dependencyOne;
    public DependentClass(IDependencyOne dependencyOne)
    {
        this.dependencyOne = dependencyOne;
    }
    public void IDoSomeActivity() //From the interface ISomeUsecase
    {
        //I do some activity using dependencyOne
    }
}