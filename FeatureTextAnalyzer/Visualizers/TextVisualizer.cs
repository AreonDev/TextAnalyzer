//
//  TextVisualizer.cs
//
//  Author:
//       Fin Christensen <christensen.fin@gmail.com>
//
//  Copyright (c) 2015 Fin Christensen
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//

using System;
using TextAnalyzer;
using System.Collections.Generic;

namespace FeatureTextAnalyzer
{
    public class TextVisualizer<TContent, TValue> : IVisualizer<TContent, TValue>
    {
        #region IVisualizer implementation

        public void Visualize (Dictionary<TContent, List<TValue>> table, VisualizationTarget target)
        {
            foreach (var row in table)
            {
                switch (target)
                {
                case VisualizationTarget.CollectedElements:
                    string list = "[";
                    foreach (var item in row.Value)
                    {
                        list += item + ", ";
                    }
                    list = list.Substring (0, list.Length - 2) + "]";
                    Console.WriteLine ("{0}: {1}", row.Key, list);
                    break;
                case VisualizationTarget.FirstElement:
                    Console.WriteLine ("{0}: {1}", row.Key, row.Value[0]);
                    break;
                case VisualizationTarget.TotalElementCount:
                    Console.WriteLine ("{0}: {1}", row.Key, row.Value.Count);
                    break;
                }
            }
        }

        #endregion
    }
}
