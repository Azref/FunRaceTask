using Assets.Scripts.Enums;
using Assets.Scripts.Path;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public RV_GameStatus Game;

        public Text LevelText;

        public PathBase CheckPointA, CheckPointB;

        public RV_MovePointList MovePoints;

        private int pathLength = 2;

        private int Level = 1;

        public List<PathBase> ThePath;

        public TheGuy Guy;

        private void Start()
        {
            Game.status = GameStatus.IsStopped;

            CheckPointA.transform.position = Vector3.zero;

            Guy.transform.position = Vector3.zero;

            BuildPlatform();
        }

        private void BuildPlatform()
        {
            ClearPlaform();

            CheckLevel();

            CheckPointA.transform.position = CheckPointB.transform.position;
            CheckPointA.transform.eulerAngles = CheckPointB.transform.eulerAngles;
            ThePath.Add(CheckPointA);

            MovePoints.list = new List<MovePointVo>();
            AddMovePoints(CheckPointA);

            PathBase path = null;
            for (int i = 0; i < pathLength; i++)
            {
                path = path.GetRandomPath(ThePath[ThePath.Count - 1].MountPoint);

                path.transform.localEulerAngles = Vector3.zero;
                path.transform.localPosition = Vector3.zero;
                path.transform.SetParent(this.transform);

                ThePath.Add(path);
                AddMovePoints(path);
            }

            CheckPointB.transform.SetParent(ThePath[ThePath.Count - 1].MountPoint);

            CheckPointB.transform.localEulerAngles = Vector3.zero;
            CheckPointB.transform.localPosition = Vector3.zero;
            CheckPointB.transform.SetParent(this.transform);

            AddMovePoints(CheckPointB);

            BuildFX();
            
        }

        private void CheckLevel()
        {
            switch (Level)
            {
                case 1:
                    pathLength = 2;
                    break;
                case 2:
                    pathLength = 5;
                    break;

                default:
                    pathLength = 12;
                    break;
            }

            LevelText.text = Level.ToString() + ". Level";
        }

        private void BuildFX()
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).position += Vector3.up * 20;
                transform.GetChild(i).DOMoveY(transform.GetChild(i).transform.position.y - 20, 1).SetDelay(i * .2f).SetEase(Ease.OutBack);
            }
            Invoke("StartGame", 1 + transform.childCount * .2f);
        }

        private void StartGame()
        {
            Game.status = GameStatus.IsPlayable;
        }

        private void AddMovePoints(PathBase path)
        {
            for (int i = 0; i < path.MovePointHolder.childCount; i++)
            {
                MovePoints.list.Add(new MovePointVo
                {
                    IsRespawn = true,
                    Point = path.MovePointHolder.GetChild(i).transform.position,
                    PointNormal = path.MovePointHolder.GetChild(i).transform.up
                });
            }
        }

        internal void GameFinish()
        {
            for (int i = 0; i < ThePath.Count; i++)
                ThePath[i].transform.DOMoveY(ThePath[i].transform.position.y - 20, 1).SetDelay(i * .2f).SetEase(Ease.InQuart);

            Level++;

            Invoke("BuildPlatform", 1 + ThePath.Count * .2f);
        }

        private void ClearPlaform()
        {
            for (int i = 1; i < ThePath.Count; i++)
                ThePath[i].Kill();

            ThePath.Clear();
        }
    }
}
