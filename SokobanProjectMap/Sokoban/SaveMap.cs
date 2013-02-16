using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sokoban
{
    class MapSaver
    {
        private String mapName, file;
        private List<List<Tile>> tileMap;
        private String[] stringMap;
        private int forklifts;
        private int targets;
        private int boxes;

        public MapSaver(List<List<Tile>> tileMap, String mapName)
        {
            file = "map/" + mapName + ".map";
            if (File.Exists(file))
            {
                MessageBoxResult result = MessageBox.Show("Er bestaat al een map met deze naam. \r\n Wilt u deze overschrijven?", "File exists", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            this.mapName = mapName;
            this.tileMap = tileMap;
            stringMap = new String[tileMap.Count()];
            forklifts = 0;
            targets = 0;
            boxes = 0;

            convertToStringMap();
            if (validateMap())
            {
                save();
            }
        }

        private void convertToStringMap()
        {
            int lineNumber = 0;
            foreach(List<Tile> row in tileMap)
            {
                String line = "";
                foreach(Tile tile in row)
                {
                    switch (tile.ToString())
                    {
                        case "Sokoban.Wall":
                            line += "#";
                            break;
                        case "Sokoban.Target":
                            line += "x";
                            targets++;
                            break;
                        case "Sokoban.Box":
                            line += "o";
                            boxes++;
                            break;
                        case "Sokoban.Forklift":
                            line += "@";
                            forklifts++;
                            break;
                        default:
                            line += " ";
                            break;
                    }
                }
                stringMap[lineNumber] = line;
                lineNumber++;
            }
        }

        private Boolean validateMap()
        {
            if(forklifts > 1)
            {
                MessageBox.Show("Er mag maar 1 speler op de map voorkomen.", "Map incorrect", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (forklifts == 0)
            {
                MessageBox.Show("Er moet een speler op de map voorkomen.", "Map incorrect", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (targets == 0)
            {
                MessageBox.Show("Er moet minstens 1 doel op de map voorkomen.", "Map incorrect", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (boxes < targets)
            {
                MessageBox.Show("Er moeten minstens evenveel kisten als doelen op de map voorkomen.", "Map incorrect", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void save()
        {
            StreamWriter sw = new StreamWriter(file);
            foreach (String line in stringMap)
            {
                sw.WriteLine(line);
            }
            sw.Close();
            MessageBox.Show("Map opgeslagen onder de naam '"+mapName+"'.", "Success!", MessageBoxButton.OK);
        }
    }
}
