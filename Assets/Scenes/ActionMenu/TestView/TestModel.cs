using UnityEngine;
using Zenject;

public class TestModel
{
    public ITestData datxa;

    [Inject]
    public void Construct(ITestData data)
    {
        this.datxa = data;
        Debug.Log($"              TestModel - {data.Message}");
    }
    public class Factory : PlaceholderFactory<ITestData,TestModel>
    {

    }
}