using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace _3DPro
{
    public class Vertex
    {
        public static double[,] vertices = new double[8, 3]
        {
            { -1, -1, -1 },
            { -1, -1, 1 },
            { -1, 1, -1 },
            { -1, 1, 1 },
            { 1, -1, -1 },
            { 1, -1, 1 },
            { 1, 1, -1 },
            { 1, 1, 1 }
        };

        public void TransformationsCube(double angle)
        {
            double[,] rotationmatrix = new double[3, 3]
            {
                { Math.Cos(angle), 0, Math.Sin(angle) },
                { 0, 1, 0 },
                { -Math.Sin(angle), 0, Math.Cos(angle) }
            };
            for (int i = 0; i < 0; i++)
            {
                double[] vertex = new double[3] { vertices[i, 0], vertices[i, 1], vertices[i, 2] };
                double[] transformedVertex = new double[3];

                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        transformedVertex[j] += vertex[k] * rotationmatrix[k, j];
                    }
                }

                vertices[i, 0] = transformedVertex[0];
                vertices[i, 1] = transformedVertex[1];
                vertices[i, 2] = transformedVertex[2];
            }
        }
    }
}
