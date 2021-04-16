using UnityEngine;
using System.Collections;

namespace StelsTestTask.Core
{
    //компонент метка, для обнаружения сущности игрока
    [RequireComponent (typeof(CharacterController))]
    [RequireComponent(typeof(MovementInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private float appearanceSpeed = 0.1f;

        private CharacterController characterController;
        private MovementInput movementInput;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            movementInput = GetComponent<MovementInput>();

            //Инициализация
            characterController.enabled = false;
            movementInput.enabled = false;
            meshRenderer.enabled = false;
            meshRenderer.sharedMaterial.SetFloat("_strenght", -1f);
        }
             
        public void DeActivate()
        {         
            StartCoroutine(Dissolve());
        }

        public void Activate()
        {            
            StartCoroutine(Appearance());
        }

        IEnumerator Appearance()
        {
            meshRenderer.enabled = true;

            for (float i = -1f; i < 2f; i += appearanceSpeed)
            {
                meshRenderer.sharedMaterial.SetFloat("_strenght", i);
             
                yield return new WaitForEndOfFrame();
            }

            characterController.enabled = true;
            movementInput.enabled = true;
        }

        IEnumerator Dissolve()
        {
            characterController.enabled = false;
            movementInput.enabled = false;

            for (float i = 2f; i > -1f; i -= appearanceSpeed)
            {
                meshRenderer.sharedMaterial.SetFloat("_strenght", i);

                yield return new WaitForEndOfFrame();
            }

            meshRenderer.enabled = false;
        }
    }
}