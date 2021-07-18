using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TargetSystem2;
using UnityEngine;
using UnityEngine.TestTools;
using Moq;
public class TargetSystemTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TargetSystemTestsSimplePasses()
    {
        var model = new Mock<ITargetSystemModel>();
        var view = new Mock<ITargetSystemView>();
        var presenter = new TargetSystemPresenter(model.Object, view.Object);
        
        var args = new TargetInformationArgs(MenuData.TargetType.All);
        model.Raise(a => a.OnShowingTargetInformation += null, null, args);

        view.Verify(v => v.ShowTargetInformation(args), Times.AtLeastOnce); ;
        model.Raise(a => a.OnShowingTargetInformation += null, null, null);
        view.Verify(v => v.StopShowing(), Times.Once); ;
        // Use the Assert class to test conditions
    }

  
}
