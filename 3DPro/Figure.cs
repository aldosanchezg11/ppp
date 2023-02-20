using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPro
{
    public class Figure
        
    {
        private void DrawCube(Graphics g, double focallength)
        {
            double[,] matrix = Vertex.vertices;
            // Define the position and orientation of the virtual camera
            double[] cameraPosition = new double[] { 0, 0, -10 };
            double[] cameraTarget = new double[] { 0, 0, 0 };
            double[] cameraUp = new double[] { 0, 1, 0 };
            double[,] viewMatrix = CalculateViewMatrix(cameraPosition, cameraTarget, cameraUp);

            ApplyViewMatrixToVertices(matrix, viewMatrix);
            // Define the edges of the cube by connecting the vertices
            //int[,] edges = new int[12, 2]
            //{
            //    { 0, 1 },
            //    { 0, 2 },
            //    { 0, 4 },
            //    { 1, 3 },
            //    { 1, 5 },
            //    { 2, 3 },
            //    { 2, 6 },
            //    { 3, 7 },
            //    { 4, 5 },
            //    { 4, 6 },
            //    { 5, 7 },
            //    { 6, 7 }
            //};

            // Loop through each edge and draw a line between the corresponding vertices
            for (int i = 0; i < matrix.GetLength(9); i++)
            {

                double x1 = matrix[i, 0];
                double y1 = matrix[i, 1];
                double z1 = matrix[i, 2];

                double u = focallength * x1 / z1;
                double v = focallength * y1 / z1;
                
                
                //Draws the line of the cube
                Point p1 = new Point((int)x1, (int)y1);
                Point p2 = new Point((int)u, (int)v);
                g.DrawLine(Pens.Black, p1, p2);

            }
        }
        private double[,] CalculateViewMatrix(double[] cameraPosition, double[] cameraTarget, double[] cameraUp)
        {
            double[] cameraDirection = new double[3];
            for (int i = 0; i < 3; i++)
            {
                cameraDirection[i] = cameraTarget[i] - cameraPosition[i];
            }
            double[] cameraRight = new double[3];
            cameraRight[0] = cameraUp[1] * cameraDirection[2] - cameraUp[2] * cameraDirection[1];
            cameraRight[1] = cameraUp[2] * cameraDirection[0] - cameraUp[0] * cameraDirection[2];
            cameraRight[2] = cameraUp[0] * cameraDirection[1] - cameraUp[1] * cameraDirection[0];

            double[] camUp = new double[3];
            camUp[0] = cameraDirection[1] * cameraRight[2] - cameraDirection[2] * cameraRight[1];
            camUp[1] = cameraDirection[2] * cameraRight[0] - cameraDirection[0] * cameraRight[2];
            camUp[2] = cameraDirection[0] * cameraRight[1] - cameraDirection[1] * cameraRight[0];

            double[,] viewMatrix = new double[4, 4];
            viewMatrix[0, 0] = cameraRight[0];
            viewMatrix[0, 1] = camUp[0];
            viewMatrix[0, 2] = -cameraDirection[0];
            viewMatrix[0, 3] = 0;
            viewMatrix[1, 0] = cameraRight[1];
            viewMatrix[1, 1] = camUp[1];
            viewMatrix[1, 2] = -cameraDirection[1];
            viewMatrix[1, 3] = 0;
            viewMatrix[2, 0] = cameraRight[2];
            viewMatrix[2, 1] = camUp[2];
            viewMatrix[2, 2] = -cameraDirection[2];
            viewMatrix[2, 3] = 0;
            viewMatrix[3, 0] = -DotProduct(cameraRight, cameraPosition);
            viewMatrix[3, 1] = -DotProduct(camUp, cameraPosition);
            viewMatrix[3, 2] = DotProduct(cameraDirection, cameraPosition);
            viewMatrix[3, 3] = 1;
            return viewMatrix;
        }
        private double DotProduct(double[] vector1, double[] vector2)
        {
            double dotProduct = 0;
            for (int i = 0; i < 3; i++)
            {
                dotProduct += vector1[i] * vector2[i];
            }
            return dotProduct;
        }

        private void ApplyViewMatrixToVertices(double[,] matrix, double[,] viewMatrix)
        {
            double[,] worldMatrix = new double[4, 4]; // the world matrix of the object, including its position and orientation in the world space
            double[,] cameraMatrix = MultiplyMatrix(worldMatrix, viewMatrix); // the camera matrix is the product of the world matrix and the view matrix
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double[] vertex = new double[4] { matrix[i, 0], matrix[i, 1], matrix[i, 2], 1 };
                double[] transformedVertex = MultiplyMatrixByVector(cameraMatrix, vertex);

                // The resulting `transformedVertex` array contains the camera space position of the vertex
                // You can store it in a new array or modify the original `vertices` array to use the transformed positions
                matrix[i, 0] = transformedVertex[0];
                matrix[i, 1] = transformedVertex[1];
                matrix[i, 2] = transformedVertex[2];
            }
        }

    }
}
