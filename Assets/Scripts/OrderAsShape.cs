using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderAsShape : MonoBehaviour
{

    // Order all childs in line form
    public static void orderAsLine(Transform parent, float colMargin,Axis axis, bool reverse)
    {
        float marginCounter = 0;
        for (int i = 0; i < parent.childCount; i++)
        {
            switch (axis)
            {
                case Axis.X:
                    parent.GetChild(i).position = new Vector3(marginCounter, 0, 0);
                    break;
                case Axis.Y:
                    parent.GetChild(i).position = new Vector3(0, marginCounter, 0);
                    break;
                case Axis.Z:
                    parent.GetChild(i).position = new Vector3(0, 0, marginCounter);
                    break;
            }            
            if (!reverse)
                marginCounter += colMargin;
            else
                marginCounter -= colMargin;
        }
    }

    // Order all childs in row and column form
    public static void orderAsBox(Transform parent, float colMargin, float rowMargin, Plane plane, bool reverse, int columns, Align align)
    {
        float marginCounter = 0;
        float rowCounter = 0;
        float originMargin;

        // Our alignment will determine our starting point
        switch (align)
        {
            case Align.Start:
                marginCounter = 0;
                break;
            case Align.Center:
                marginCounter = (columns - 1) * colMargin / 2;
                break;
            case Align.End:
                marginCounter = (columns - 1) * colMargin;
                break;
        }

        originMargin = marginCounter;
        int colCounter = 0;

        for (int i = 0; i < parent.childCount; i++)
        {
            switch (plane)
            {
                case Plane.XY:
                    parent.GetChild(i).position = new Vector3(marginCounter, rowCounter, 0);
                    break;
                case Plane.XZ:
                    parent.GetChild(i).position = new Vector3(marginCounter, 0, rowCounter);
                    break;
                case Plane.YZ:
                    parent.GetChild(i).position = new Vector3(0, rowCounter, marginCounter);
                    break;
            }

            colCounter++;

            // The align will dictate wether we go rtl, ltr or middle - out
            switch (align)
            {
                case Align.Start:
                    marginCounter += colMargin;
                    break;
                case Align.Center:
                    if (colCounter % 2 == 0)
                        marginCounter += colMargin * colCounter;
                    else
                        marginCounter -= colMargin * colCounter;
                    break;
                case Align.End:
                    marginCounter -= colMargin;
                    break;
            }
                
            // After finishing the column we update the row variable
            if (colCounter == columns)
            {
                marginCounter = originMargin;
                colCounter = 0;
                if (!reverse)
                    rowCounter += rowMargin;
                else
                    rowCounter -= rowMargin;
            }


        }
    }

    // Order all childs in circle form
    public static void orderAsCircle(Transform parent, float radius, Plane plane, bool angle, float angleValue = 0)
    {
        float radian;

        for (int i = 0; i < parent.childCount; i++)
        {
            // If angle isn't selected, we use the relative part of the circle circumference.
            if (!angle)
                radian = i * (2 * Mathf.PI) / parent.childCount;
            // If it is selected, we take only the angle value as is.
            else
                radian = i * angleValue * 0.01745329252f;

            switch (plane)
            {
                case Plane.XY:
                    parent.GetChild(i).position = new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0);
                    break;
                case Plane.XZ:
                    parent.GetChild(i).position = new Vector3(radius * Mathf.Cos(radian), 0, radius * Mathf.Sin(radian));
                    break;
                case Plane.YZ:
                    parent.GetChild(i).position = new Vector3(0, radius * Mathf.Cos(radian), radius * Mathf.Sin(radian));
                    break;
            }
        }
    }

}
