using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class Vector : IEnumerable
    {
        public float[] Dimensions { get; set; }

        public double Length
        {
            get
            {
                double squaredDimensions = 0;
                foreach (var dimension in Dimensions)
                {
                    squaredDimensions += Math.Pow(dimension, 2);
                }
                return Math.Sqrt(squaredDimensions);
            }
        }

        public Vector() { }

        public Vector(params float[] dimensions)
        {
            this.Dimensions = dimensions;
        }

        public static double Distance(Vector v1, Vector v2)
        {
            if (v1.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            double squaredDimensions = 0;
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                squaredDimensions += Math.Pow(v1.Dimensions[i] - v2.Dimensions[i], 2);
            }
            return Math.Sqrt(squaredDimensions);
        }

        public double Distance(Vector v2)
        {
            if (this.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            double squaredDimensions = 0;
            for (int i = 0; i < Dimensions.Length; i++)
            {
                squaredDimensions += Math.Pow(this.Dimensions[i] - v2.Dimensions[i], 2);
            }
            return Math.Sqrt(squaredDimensions);
        }

        public IEnumerator GetEnumerator()
        {
            return Dimensions.GetEnumerator();
        }

        public static Vector operator *(Vector v1, Vector v2)
        {
            if (v1.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            float[] dimensions = new float[v1.Dimensions.Length];
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] * v2.Dimensions[i];
            }
            return new Vector(dimensions);
        }

        public static Vector operator *(Vector v1, int n)
        {
            float[] dimensions = new float[v1.Dimensions.Length];
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] * n;
            }
            return new Vector(dimensions);
        }

        public static Vector operator /(Vector v1, Vector v2)
        {
            if (v1.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            float[] dimensions = new float[v1.Dimensions.Length];
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] / v2.Dimensions[i];
            }
            return new Vector(dimensions);
        }

        public static Vector operator /(Vector v1, int n)
        {
            float[] dimensions = new float[v1.Dimensions.Length];
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] / n;
            }
            return new Vector(dimensions);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            float[] dimensions = new float[v1.Dimensions.Length];
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] + v2.Dimensions[i];
            }
            return new Vector(dimensions);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Dimensions.Length != v2.Dimensions.Length)
                throw new Exception();

            float[] dimensions = new float[v1.Dimensions.Length];            
            for (int i = 0; i < v1.Dimensions.Length; i++)
            {
                dimensions[i] += v1.Dimensions[i] - v2.Dimensions[i];
            }
            return new Vector(dimensions);
        }

        public Vector Clone()
        {
            return new Vector(Dimensions);
        }

        public override string ToString()
        {
            string concatenation = "";
            foreach (var dimension in Dimensions)
            {
                concatenation += dimension + ";";
            }
            concatenation = concatenation.Substring(0, concatenation.Length - 1);
            return string.Format("Vector[Dimensions: {0}]", concatenation);
        }
    }
}
