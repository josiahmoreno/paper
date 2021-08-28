using Scenes.ActionMenu.DataView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActionDataInstaller : Installer<ActionDataInstaller>
{
    [Inject]
    IActionViewItem data;
    [Inject]
    Transform parent;
    //[Inject]
    //GameObject ActionViewPrefab;
    public override void InstallBindings()
    {
        Container.BindInstance(data).AsSingle();
        Container.BindInterfacesAndSelfTo<ActionDataModel>().AsTransient();
        Container.BindInterfacesAndSelfTo<ActionDataPresenter>().AsTransient();
        //Debug.Log($"parent name = {parent?.name}");
        Container.BindInterfacesAndSelfTo<ActionDataView>().FromComponentInHierarchy().AsSingle();

        //.FromComponentInNewPrefab(ActionViewPrefab)
        //.UnderTransform(parent)
        //.AsSingle();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
