using UnityEngine;
using Zenject;

public class TestPresenter
{
    //private TestModel testModel;

    //[Inject]
    //public void Construct(ITestData data,TestModel.Factory testModel)
    //{
    // Debug.Log($"      TestPresenter - {data.Message}");
    //   this.testModel = testModel.Create(data);
    //}
    //public class Factory : PlaceholderFactory<ITestData,TestPresenter>
    //{
    //}

    //[Inject]
    //public void con(ITestData data, TestModel testModel)
    //{
    //    Debug.Log($"      TestPresenter - {data.Message}");
    //}

    [Inject]
    public void con( TestModel testModel)
    {
        Debug.Log($"      TestPresenter - {testModel.datxa.Message}");
    }

}
