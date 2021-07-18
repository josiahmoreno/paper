using System;

namespace TargetSystem2
{
    public interface ITargetSystemModel
    {
        event EventHandler<TargetInformationArgs>  OnShowingTargetInformation;
    }
}