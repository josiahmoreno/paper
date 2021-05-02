using System;
using UnityEngine;

namespace Swap
{
    public class SwapAnimator
    {
        public ICharacterEntityProvider provider;
        public SwapAnimator(Battle.Battle battle, ICharacterEntityProvider provider)
        {
            this.provider = provider;
            battle.TurnSystem.OnSwapped += TurnSystemOnOnSwapped;
        }
        int swapBackwardHash = Animator.StringToHash("SwapBackward");
        int swapForwardHash = Animator.StringToHash("SwapForward");
        private bool swapped = false;
        private void TurnSystemOnOnSwapped(object sender, EventArgs e)
        {
            
            var firstHero = provider.CharacterEntities[0];
            
             var secondEntity = provider.CharacterEntities[1];
             Debug.Log($"SwapAnimator - first hero = {firstHero.battleHero}, second hero = {secondEntity.battleHero}");
             var animator =  firstHero.prefab.GetComponent<Animator>();
             var backAnimator = secondEntity.prefab.GetComponent<Animator>();
               //animator.SetTrigger(swapHash);
               if (!swapped)
               {
                   animator.SetTrigger(swapBackwardHash);
                   // .Play("entry");
                   //animator.SetTrigger(swapHash);
                   backAnimator.SetTrigger(swapForwardHash);
                   swapped = true;
               }
               else
               {
                   animator.SetTrigger(swapForwardHash);
                   // .Play("entry");
                   //animator.SetTrigger(swapHash);
                   backAnimator.SetTrigger(swapBackwardHash);
                   swapped = false;
               }
              
        }
    }
}