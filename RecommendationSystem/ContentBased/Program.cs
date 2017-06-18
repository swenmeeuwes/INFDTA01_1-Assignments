using ContentBased.controller;
using ContentBased.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased
{
    class Program
    {
        private readonly static string FILE_PATH = "assets/slide.data";
        private readonly static char DELIMITER = ',';

        static void Main(string[] args)
        {
            var data = DataProvider.GetData(FILE_PATH, DELIMITER);
            var deviationMatrix = DeviationMatrixFactory.Instance.Create(data);

            var ben = data["Ben"];
            Console.WriteLine("Prediction Ben for Whitney Houston: {0}", deviationMatrix.Predict(ben, "Whitney Houston"));

            //var user7 = data["7"];
            //Console.WriteLine("Prediction u7 for 101: {0}", deviationMatrix.Predict(user7, "101"));
        }
    }
}
