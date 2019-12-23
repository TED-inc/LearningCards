using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TEDinc.Utils
{
    public class UiGrid : MonoBehaviour
    {
        public Vector2 cellSize = new Vector2(100f, 200f);
        public int maxPerMainLine = 0;
        public UiGridPivot pivot = UiGridPivot.topLeft;
        public UiGridDirection direction = UiGridDirection.rightDown;
        public bool resetZ = false;
        public bool ignoreInactive = true;
        public bool ignoreSelected = true;
        public List<Transform> SelectedObjs = new List<Transform>();

        private Int2 cellCount;
        private Vector2 margins; //move grid to current point
        private Vector2 marginsRepos; //reposition of grid to botLeft point
        private Int2 scale;
        private List<Transform> objs;
        private int objsCount;



        [ContextMenu("Reposition")]
        public void Reposition()
        {
            scale = new Int2(1, 1);
            objs = new List<Transform>();
            objsCount = 0;

            FillObjsList();
            CalcualteMargins();
            CalculateRepositionOfMargins();
            ScaleRedirectIfItNecessary();
            SetPositionForElements();
        }



        private void FillObjsList()
        {
            for (int i = 0; i < transform.childCount; i++)
                if (!ignoreInactive || transform.GetChild(i).gameObject.activeInHierarchy)
                    if (SelectedObjs.Contains(transform.GetChild(i)) ^ ignoreSelected)
                    {
                        objsCount++;
                        objs.Add(transform.GetChild(i));
                    }
        }

        private void ScaleRedirectIfItNecessary()
        {
            //revers scale if direction to left or down
            if ((int)direction / 10 == 1 || (int)direction % 10 == 1)
                scale.y = -1;
            if ((int)direction / 10 == 2 || (int)direction % 10 == 2)
                scale.x = -1;
        }

        private void CalculateRepositionOfMargins()
        {
            if ((int)direction % 10 == 3 || (int)direction / 10 == 3) //if one of direction is right
                marginsRepos.x = 0;
            if ((int)direction % 10 == 0 || (int)direction / 10 == 0) //if one of direction is up
                marginsRepos.y = 0;
        }

        private void CalcualteMargins()
        {
            if ((int)direction / 10 >= 2) //if primal direction is left or right
            {
                if (maxPerMainLine != 0)
                    cellCount = new Int2(maxPerMainLine, (objsCount - 1) / maxPerMainLine + 1);
                else
                    cellCount = new Int2(objsCount, 1);
            }
            else if (maxPerMainLine != 0) //if primal direction is up or bottom
                cellCount = new Int2((objsCount - 1) / maxPerMainLine + 1, maxPerMainLine);
            else
                cellCount = new Int2(1, objsCount);

            margins = cellSize * (cellCount - 1);
            marginsRepos = cellSize * (cellCount - 1);
            margins.x *= (float)((int)pivot / 10) / 2;
            margins.y *= 1 - (float)((int)pivot % 10) / 2;
        }

        private void SetPositionForElements()
        {
            Transform obj;
            if ((int)direction / 10 >= 2) //if primal direction is left or right
                for (int y = 0, i = 0; y < cellCount.y; y++)
                    for (int _x = 0; _x < cellCount.x && i < objsCount; _x++)
                    {
                        obj = objs[i++];
                        obj.localPosition = new Vector3(
                               cellSize.x * _x * scale.x + marginsRepos.x - margins.x,
                               cellSize.y * y * scale.y + marginsRepos.y - margins.y,
                               resetZ ? 0 : obj.localPosition.z);
                    }
            else //if primal direction is up or bottom
                for (int x = 0, i = 0; x < cellCount.x; x++)
                    for (int y = 0; y < cellCount.y && i < objsCount; y++)
                    {
                        obj = objs[i++];
                        obj.localPosition = new Vector3(
                               cellSize.x * x * scale.x + marginsRepos.x - margins.x,
                               cellSize.y * y * scale.y + marginsRepos.y - margins.y,
                               resetZ ? 0 : obj.localPosition.z);
                    }
        }
    }

    public enum UiGridPivot
    {
        topLeft = 00, topCenter = 10, topRight = 20,
        midLeft = 01, midCenter = 11, midRight = 21,
        botLeft = 02, botCenter = 12, botRight = 22,
    }

    public enum UiGridDirection
    {
        upLeft = 02, leftUp = 20,
        upRight = 03, rightUp = 30,
        downLeft = 12, leftDown = 21,
        downRight = 13, rightDown = 31,
    }
}
