﻿using ItemItem.controller;
using ItemItem.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemItem
{
    class Program
    {
        private readonly static string FILE_PATH = "assets/u.data";
        private readonly static char DELIMITER = '\t';

        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            var data = DataProvider.GetData(FILE_PATH, DELIMITER);

            stopwatch.Start();
            var deviationMatrix = DeviationMatrixFactory.Instance.Create(data);
            stopwatch.Stop();

            //Console.WriteLine("Elapsed seconds: {0}.", stopwatch.ElapsedMilliseconds / 1000);

            var user186 = data["186"];
            //Console.WriteLine("Prediction 186 for 1599: {0}", deviationMatrix.Predict(user186, "1599"));
            var top5 = deviationMatrix.PredictTop(user186, 5);
            Array.ForEach(top5, p => Console.WriteLine(p));
        }
    }
}
