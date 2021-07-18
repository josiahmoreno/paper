using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargetSystem2
{


    public class TargetSystemPresenter : ITargetSystemPresenter
    {
        private ITargetSystemModel model;
        private ITargetSystemView view;
        // Start is called before the first frame update


        public TargetSystemPresenter(ITargetSystemModel model, ITargetSystemView view)
        {
            this.model = model;
            model.OnShowingTargetInformation += Model_OnShowingTargetInformation;
            this.view = view;
        }
        public void OnStart()
        {

          
            //view.ShowAllEnemies();
        }

        private void Model_OnShowingTargetInformation(object sender, TargetInformationArgs e)
        {
            if(e != null)
            {
                view.ShowTargetInformation(e);

            } else
            {
                view.StopShowing();
            }
        }
    }

}