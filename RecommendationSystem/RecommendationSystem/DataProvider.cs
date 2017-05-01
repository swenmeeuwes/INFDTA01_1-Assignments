using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class DataProvider
    {
        readonly static char DELIMITER = ',';
        readonly static string DATASET_FILEPATH = "assets/userItem.data";

        public static Vector[] GetData()
        {
            List<Vector> vectorList = new List<Vector>();
            using (StreamReader streamReader = new StreamReader(DATASET_FILEPATH))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    float[] dimensions = line.Split(DELIMITER).Select(d => float.Parse(d)).ToArray();                    
                    vectorList.Add(new Vector(dimensions));
                }
            };
            return vectorList.ToArray();
        }
    }
}
