using UnityEngine;

namespace TEDinc.LearningCards
{
    public class CardFlipInteractor : CardInteractorBasic
    {
        public Transform flippingTarget;
        public GameObject hidenState;
        public GameObject displayState;
        public float duration = 0.5f;

        private float timer;
        private float yRotation;
        private bool halfFliped;

        public override void Interact()
        {
            if (stateTransiton == CardInteractorStateTransiton.ended)
                stateTransiton = CardInteractorStateTransiton.inProgress;
            else
            {
                stateTransiton = CardInteractorStateTransiton.ended;
                flippingTarget.localRotation = Quaternion.Euler(0f, 0f, 0f);

                SwitchState();
                DisplayStateImmediatly();
            }

            timer = duration;
            halfFliped = false;
        }

        public override void Revert()
        {
            state = CardInteractorState.start;
            DisplayStateImmediatly();
        }



        private void Update()
        {
            if (stateTransiton == CardInteractorStateTransiton.ended)
                return;

            timer -= Time.deltaTime;

            CheckTransitionState();
            if (timer > duration / 2f)
                yRotation = Mathf.InverseLerp(duration, duration / 2f, timer) * -90f;
            else if (timer > 0f)
                yRotation = Mathf.InverseLerp(0f, duration / 2f, timer) * -90f;
            else
                yRotation = 0f;

            flippingTarget.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        private void SwitchState()
        {
            if (state == CardInteractorState.start)
                state = CardInteractorState.final;
            else
                state = CardInteractorState.start;
        }

        private void DisplayStateImmediatly()
        {
            hidenState.SetActive(state == CardInteractorState.start);
            displayState.SetActive(state != CardInteractorState.start);
        }

        private void CheckTransitionState()
        {
            if (timer < duration / 2f && !halfFliped)
            {
                halfFliped = true;
                SwitchState();
                DisplayStateImmediatly();
            }
            if (timer < 0f)
                stateTransiton = CardInteractorStateTransiton.ended;
        }
    }
}