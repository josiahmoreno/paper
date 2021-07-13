using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Scenes.ActionMenu.DataView;
using UnityEngine;
using UnityEngine.TestTools;

public class ActionViewTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void ActionView_onselectechage()
    {
        //setup the mock object
        var mockedView = new Mock<IActionDataView>();
        var mockedModel = new Mock<IActionDataModel>();
        var item = new ActionViewItem(null, null, null);
        bool isSelected = true;
        mockedModel.Setup(model => model.IsSelected).Returns(true);
        var p = new ActionDataPresenter(mockedView.Object,mockedModel.Object);
        p.OnStart();
        // Raising an event on the mock
        mockedModel.Raise(m => m.OnSelectedChanged += null, mockedModel.Object,false);
        //check that the Result property was set to 6
        var sequence = new MockSequence();

        mockedView.InSequence(sequence).Setup(x => x.Select());
        mockedView.InSequence(sequence).Setup(x => x.Deselect());
        //mockedView.Verify(view =>view.Select(), Moq.Times.Exactly(1));
        //mockedView.Verify(view =>view.Deselect(), Moq.Times.Exactly(1));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ActionViewTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
