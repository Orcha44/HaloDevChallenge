using UnityEngine;
using System.Collections;

public class ShapeProperties : MonoBehaviour
{
    // Define fields and their default values
    #region Public Fields
    public Shape shapeType;

    public int columns = 1;
    public float margin = 1f;
    public float marginColumn = 1f;
    public float marginRow = 1f;
    public Plane plane = Plane.XY;
    public Axis axis = Axis.X;
    public float radius = 1f;
    public bool reverse = false;
    public bool angle = false;
    public float angleValue = 5f;
    public Align align = Align.Start;
    #endregion

    /*
     * Create the shape with the given properties. For box, check if we need to 
     * use the default margin or was the column/row margin changed separately.
     */

    public void createWithProperties()
    {
        switch (shapeType)
        {
            case Shape.Line:
                OrderAsShape.orderAsLine(transform, marginColumn, axis, reverse);
                break;
            case Shape.Box:                
                float tmpCol = margin != marginColumn ? marginColumn : margin;
                float tmpRow = margin != marginRow ? marginRow : margin;
                OrderAsShape.orderAsBox(transform, tmpCol, tmpRow, plane, reverse, columns, align);
                break;
            case Shape.Circle:
                OrderAsShape.orderAsCircle(transform, radius, plane, angle, angleValue);
                break;
        }

    }
}



// Global enum definition
#region Enums
public enum Shape { Line, Box, Circle }
public enum Plane { XY, XZ, YZ }
public enum Align { Start, Center, End }
public enum Axis { X, Y, Z }
#endregion