using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class ConfigurationData {
    #region Fields

    List<TeleportationConfiguration> teleportationConfigurations = new List<TeleportationConfiguration>();

    #endregion

    #region Properties

    public List<TeleportationConfiguration> TeleportationConfigurations {
        get { return teleportationConfigurations; }
    }

    #endregion

    #region Constructor

    public ConfigurationData() {
        string teleportationDataFileName = "TeleportationData.csv";
        StreamReader teleportationDataFile = null;
        try {
            teleportationDataFile = File.OpenText(Path.Combine(Application.streamingAssetsPath, teleportationDataFileName));
            teleportationDataFile.ReadLine();

            string line;
            TeleportationConfiguration teleportationConfiguration;
            while ((line = teleportationDataFile.ReadLine()) != null) {
                teleportationConfiguration = ParseCsvLine(line);
                teleportationConfigurations.Add(teleportationConfiguration);
            }

        }
        catch (Exception e) {
            Console.WriteLine(e.Message);
        }
        finally {
            if (teleportationDataFile != null)
                teleportationDataFile.Close();
        }
    }

    #endregion

    #region Methods

    public TeleportationConfiguration ParseCsvLine(string line) {
        string[] values = line.Split(',');
        TeleportationConfiguration teleportationConfiguration = new TeleportationConfiguration(float.Parse(values[0]),
                                                                                                float.Parse(values[1]),
                                                                                                float.Parse(values[2]));
        return teleportationConfiguration;
    }

    #endregion
}
