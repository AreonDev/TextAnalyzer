//
//  ChartVisualizer.cs
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
using System.Collections.Generic;
using System.Windows.Forms;
using TextAnalyzer;

namespace FeatureTextAnalyzer
{
    public class ChartVisualizer<TContent, TValue> : IVisualizer<TContent, TValue>
    {
        #region IVisualizer implementation

        public void Visualize (Dictionary<TContent, List<TValue>> table, VisualizationTarget target)
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);

            if (table.Values.Count > 0)
            {
                var type = typeof (TValue);
                if (typeof (double).IsAssignableFrom (type) ||
                    typeof (float) == type ||
                    typeof (int) == type ||
                    typeof (short) == type ||
                    typeof (long) == type)
                {
                    var new_table = new Dictionary<TContent, List<double>> ();
                    foreach (var row in table)
                    {
                        var new_list = new List<double> ();
                        foreach (var item in row.Value)
                        {
                            new_list.Add (Convert.ToDouble (item));
                        }
                        new_table.Add (row.Key, new_list); 
                    }
                    var form = new ChartVisualizerForm<TContent> (new_table, target);
                    form.ShowDialog ();
                }
                else
                {
                    throw new NotSupportedException ("Incompatible type TValue");
                }
            }
            else
            {
                new ChartVisualizerForm<TContent> (new Dictionary<TContent, List<double>> (), target).ShowDialog ();
            }
        }

        #endregion
    }
}
