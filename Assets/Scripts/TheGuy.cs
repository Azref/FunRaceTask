using Assets.Scripts.Enums;
using Assets.Scripts.Path;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TheGuy : MonoBehaviour
    {
        public RV_GameStatus Game;

        public Text LifeText;

        private float _rotSpeed = .5f;

        private int _targetMovePoint = 0;

        private Vector3 _rotVelocity;

        public bool _isDead;

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                GetComponent<Animator>().SetBool("IsRunning", _isRunning);
            }
        }

        private bool _fastRunner;
        public bool FastRunner
        {
            get { return _fastRunner; }
            set
            {
                _fastRunner = value;
                Speed = _fastRunner ? 5 : 3;
            }
        }

        private float _speed = 3f;
        [ShowInInspector]
        public float Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                GetComponent<Animator>().SetFloat("SpeedMultiplier", _speed/3);
            }
        }
        
        public RV_MovePointList MovePoints;

        private int _lifeCount;
        public int LifeCount
        {
            get { return _lifeCount; }
            set
            {
                _lifeCount = value;
                LifeText.text = "Life: " + _lifeCount.ToString();
            }
        }

        private void Start()
        {
            transform.position = Vector3.zero;

            IsRunning = false;

            _isDead = false;

            RagdollOn(false);

            LifeCount = 3;
        }

        private void Update()
        {
            if (_isDead)
                return;

            if (Game.status == GameStatus.IsStopped)
                return;

            CheckInput();

            if (!IsRunning)
                return;

            Move();

            CheckMovePoints();

            Rotate();
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.tag + " "  + other.gameObject.name);
            if (other.CompareTag("Obstacle") || other.CompareTag("Hole"))
            {
                _isDead = true;

                IsRunning = false;

                RagdollOn(true);

                if (other.gameObject.name == "Stick")
                {
                    transform.GetChild(0).GetComponent<Rigidbody>().AddExplosionForce(500, transform.position + transform.forward * 1, 10, 5);
                }

                _targetMovePoint--;
                StartCoroutine(DieCounter());
            }
            else if (other.CompareTag("Platform"))
            {
                transform.SetParent(other.GetComponentInParent<PathBase>().transform.GetChild(0), true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Platform"))
                transform.SetParent(null, true);
        }

        private IEnumerator DieCounter()
        {
            yield return new WaitForSeconds(3);

            ReSpawn();
        }

        private void CheckInput()
        {
            if (Input.GetMouseButton(0))
                IsRunning = true;
            else
                IsRunning = false;
        }

        private void Move()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void CheckMovePoints()
        {
            if (_targetMovePoint == MovePoints.list.Count)
            {
                if (Vector3.Distance(MovePoints.list[_targetMovePoint-1].Point, transform.position) < .5f)
                    Win();
            }
            else if (Vector3.Distance(MovePoints.list[_targetMovePoint].Point , transform.position) < _rotSpeed + .5f)
                _targetMovePoint++;
        }

        private void Win()
        {
            Game.status = GameStatus.IsStopped;

            IsRunning = false;
            LifeCount = 3;
            _targetMovePoint = 0;

            GameManager.Instance.GameFinish();
        }

        private void Rotate()
        {
            if (_targetMovePoint == MovePoints.list.Count)
                return;

            transform.LookAt(Vector3.SmoothDamp(transform.position + transform.forward, MovePoints.list[_targetMovePoint].Point, ref _rotVelocity, _rotSpeed), MovePoints.list[_targetMovePoint].PointNormal);
        }

        private void ReSpawn()
        {
            IsRunning = false;

            RagdollOn(false);

            LifeCount--;
            if (LifeCount < 0)
            {
                LifeCount = 3;
                _targetMovePoint = 0;
            }
            transform.SetParent(null);
            transform.position = MovePoints.list[_targetMovePoint].Point;
            transform.LookAt(MovePoints.list[_targetMovePoint + 1].Point);

            _isDead = false;
        }

        private void RagdollOn(bool val)
        {
            GetComponent<CapsuleCollider>().enabled = !val;
            GetComponent<Animator>().enabled = !val;
            GetComponent<Rigidbody>().isKinematic = val;

            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
                rb.isKinematic = !val;
            
        }
    }
}