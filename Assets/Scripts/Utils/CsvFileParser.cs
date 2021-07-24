using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Utils {
    public class CsvFileParser {
        public static string csvFileName = null;

        public static List<(Vector3 position, float orientation)> GetCoordinates() {
            string csvFilePath = Path.Combine("Assets", "OutputFiles~", csvFileName);

			List<(Vector3 position, float orientation)> coordinates = new List<(Vector3 position, float orientation)>();

            using (var reader = new StreamReader(csvFilePath)) {
                bool isHeaderRow = true;
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    if (isHeaderRow) {
                        isHeaderRow = false;
                        continue;
                    }

                    var fields = line.Split(',');

                    Vector3 position = new Vector3(float.Parse(fields[2]), float.Parse(fields[3]), float.Parse(fields[4]));
                    float orientation = float.Parse(fields[5]);

                    coordinates.Add((position, orientation));
                }
            }

            return coordinates;
        }
    }
}
