using System;
using System.Collections;
using System.Collections.Generic;
using MenuData;
using Moq;
using NUnit.Framework;
using Scenes.ActionMenu.DataView;
using UnityEngine;
using UnityEngine.TestTools;

public class ActionDataModelTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void test_isSelected()
    {
        var mockedItem = new Mock<IActionMenuData>().Object;
        var actionItem = new Mock<IActionViewItem>();
        actionItem.Setup(a => a.data).Returns(mockedItem);
        var menu = new Mock<MenuData.IActionMenu>();
        menu.Setup(a => a.ActiveAction).Returns(mockedItem);
        var model = new ActionDataModel(actionItem.Object,menu.Object);
        Assert.IsTrue(model.IsSelected);
        menu.Setup(a => a.ActiveAction).Returns((IActionMenuData)null);
        Assert.False(model.IsSelected);
    }
    
    [Test]
    public void test_OnSelectionChange()
    {
      
        var actionItem = new Mock<IActionViewItem>();
        var menu = new Mock<MenuData.IActionMenu>();
        bool wasSelectedChange = false;

        void Sub(object sender, IActionMenuData b)
        {
            Debug.Log("yo");
            wasSelectedChange = true;
        }
       
        var model = new ActionDataModel(actionItem.Object,menu.Object);
        model.OnSelectedChanged += (a,b) => wasSelectedChange = true ;
        menu.Raise(a => a.OnActiveActionChanged += Sub,null,null);
        Assert.IsTrue(wasSelectedChange);
       
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ActionModelTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
