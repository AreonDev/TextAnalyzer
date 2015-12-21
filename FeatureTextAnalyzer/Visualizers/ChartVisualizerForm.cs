//
//  ChartVisualizerForm.cs
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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using TextAnalyzer;

namespace FeatureTextAnalyzer
{
    public class ChartVisualizerForm<TContent> : Form
    {
        public ChartVisualizerForm (Dictionary<TContent, List<double>> table, VisualizationTarget target)
        {
            Text = "Plot";
            Width = 1000;
            Height = 400;
            var plot_view = new PlotView ();
            plot_view.Dock = DockStyle.Fill;
            Controls.Add (plot_view);

            var rnd = new Random ();

            var plot_model = new PlotModel ();
            var axis = new CategoryAxis { Position = AxisPosition.Bottom };
            plot_model.Axes.Add (axis);

            var series = new ColumnSeries ();
            series.LabelFormatString = "{0:F2}";
            series.LabelPlacement = LabelPlacement.Middle;
            var palette = OxyPalettes.Cool (table.Count);
            int idx = 0;
            foreach (var row in table)
            {
                axis.Labels.Add (row.Key.ToString ());

                switch (target)
                {
                case VisualizationTarget.CollectedElements:
                    foreach (var item in row.Value)
                    {
                        series.Items.Add (new ColumnItem (item, axis.Labels.IndexOf (row.Key.ToString ())) {
                            Color = palette.Colors[idx].ChangeIntensity (rnd.NextDouble () * 0.5 + 0.5)
                        });
                    }
                    break;
                case VisualizationTarget.FirstElement:
                    series.Items.Add (new ColumnItem (row.Value[0], axis.Labels.IndexOf (row.Key.ToString ())) {
                        Color = palette.Colors[idx]
                    });
                    break;
                case VisualizationTarget.TotalElementCount:
                    series.Items.Add (new ColumnItem (row.Value.Count, axis.Labels.IndexOf (row.Key.ToString ())) {
                        Color = palette.Colors[idx]
                    });
                    break;
                }

                idx++;
            }

            plot_model.Series.Add (series);
            plot_view.Model = plot_model;
        }
    }
}
