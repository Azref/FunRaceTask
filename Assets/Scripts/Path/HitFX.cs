using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Path
{
    public class HitFX : MonoBehaviour
    {
        private MaterialPropertyBlock materialBlock;

        private MeshRenderer meshRenderer;

        public Material CollisionMat;

        public Material _orgMat;

        private Vector2 _matPanSpeedValues = new Vector2(.1f,.5f);

        void Start()
        {
            materialBlock = new MaterialPropertyBlock();
            meshRenderer = GetComponent<MeshRenderer>();
            _orgMat = meshRenderer.material;
        }

        private void OnCollisionEnter(Collision collision)
        {
            ChangeMaterial();
        }

        public void ChangeMaterial()
        {
            materialBlock.SetVector("_offsetSpeed", new Vector4(_matPanSpeedValues.x, _matPanSpeedValues.y, 0, 0));
            meshRenderer.material = CollisionMat;
            meshRenderer.SetPropertyBlock(materialBlock);

            StartCoroutine( ResetCounter() );
        }

        private IEnumerator ResetCounter()
        {
            yield return new WaitForSeconds(3);

            ResetFX();
        }

        public void ResetFX()
        {
            meshRenderer.material = _orgMat;
        }
    }
}