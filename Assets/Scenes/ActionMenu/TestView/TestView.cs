using MenuData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestView : MonoBehaviour
{
    private TestPresenter presenter;

    // Start is called before the first frame update

    [Inject]
    void Construct(ITestData data, TestPresenter presenter)
    {
        Debug.Log($"    TestView {data.Message}");
        this.presenter = presenter;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Factory : PlaceholderFactory<ITestData,TestView> { }


    //public class Factory : PlaceholderFactory<ITestData, TestPresenter, TestView>
    //{

    //}
}

public class TestViewSpawner
{
    private TestView.Factory factory;
    //private TestPresenter.Factory presenterFactory;

    //public TestViewSpawner(TestView.Factory factory, TestPresenter.Factory presenterFactory)
    //{
    //    this.factory = factory;
    //    this.presenterFactory = presenterFactory;
    //}

    public TestViewSpawner(TestView.Factory factory)
    {
        this.factory = factory;
       
    }

    public void InstanView(IActionMenuData data, object game, Transform transform)
    {
        var dat = new TestData(data.Name);
        this.factory.Create(dat);
    }
}
