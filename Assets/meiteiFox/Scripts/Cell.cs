using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class Cell
{
    public struct CellInfo
    {
        /// <summary>
        /// セルのワールド座標
        /// </summary>
        public Vector3 Pos { get; set; }
        /// <summary>
        /// 0 : 敵のルートではない 1~ : スタートから順にゴールまでの敵が進む順番
        /// </summary>
        public int RouteNum { get; set; }
    }
    /// <summary>
    /// 初期化。9*9のセルを生成し、敵のルートをランダムに生成する。
    /// </summary>
    /// <param name="leftup">マップの左上</param>
    /// <param name="rightdown">マップの右下</param>
    public void Initialize(out CellInfo[] cellInfo, out Vector3[] RouteVec, Vector3 topLeft, Vector3 bottomRight)
    {
        int gridSize = 9; // 9マス
        cellInfo = new CellInfo[gridSize * gridSize];
        float xStep = (bottomRight.x - topLeft.x) / (gridSize - 1);
        float yStep = (bottomRight.y - topLeft.y) / (gridSize - 1);
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                cellInfo[i * gridSize + j] = new CellInfo();
                cellInfo[i * gridSize + j].Pos = new Vector3(topLeft.x + j * xStep, topLeft.y + i * yStep);
                cellInfo[i * gridSize + j].RouteNum = 0;
            }
        }
        //Map1
        RouteVec = new Vector3[22]
        {
            cellInfo[1].Pos,
            cellInfo[10].Pos,
            cellInfo[19].Pos,
            cellInfo[28].Pos,
            cellInfo[37].Pos,
            cellInfo[38].Pos,
            cellInfo[39].Pos,
            cellInfo[40].Pos,
            cellInfo[31].Pos,
            cellInfo[22].Pos,
            cellInfo[23].Pos,
            cellInfo[24].Pos,
            cellInfo[25].Pos,
            cellInfo[34].Pos,
            cellInfo[43].Pos,
            cellInfo[52].Pos,
            cellInfo[61].Pos,
            cellInfo[70].Pos,
            cellInfo[69].Pos,
            cellInfo[68].Pos,
            cellInfo[67].Pos,
            cellInfo[76].Pos
        };
        cellInfo[1].RouteNum = 1;
        cellInfo[10].RouteNum = 2;
        cellInfo[19].RouteNum = 3;
        cellInfo[28].RouteNum = 4;
        cellInfo[37].RouteNum = 5;
        cellInfo[38].RouteNum = 6;
        cellInfo[39].RouteNum = 7;
        cellInfo[40].RouteNum = 8;
        cellInfo[31].RouteNum = 9;
        cellInfo[22].RouteNum = 10;
        cellInfo[23].RouteNum = 11;
        cellInfo[24].RouteNum = 12;
        cellInfo[25].RouteNum = 13;
        cellInfo[34].RouteNum = 14;
        cellInfo[43].RouteNum = 15;
        cellInfo[52].RouteNum = 16;
        cellInfo[61].RouteNum = 17;
        cellInfo[70].RouteNum = 18;
        cellInfo[69].RouteNum = 19;
        cellInfo[68].RouteNum = 20;
        cellInfo[67].RouteNum = 21;
        cellInfo[76].RouteNum = 22;
    }
}