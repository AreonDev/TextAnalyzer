﻿//
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
using System.Windows.Forms;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot.Axes;
using TextAnalyzer;

namespace FeatureTextAnalyzer
{
    public class ChartVisualizerForm : Form
    {
        public ChartVisualizerForm (Dictionary<string, Pair<List<int>, int>> table)
        {
            Text = "Plot";
            Width = 600;
            Height = 400;
            var plot_view = new PlotView ();
            plot_view.Dock = DockStyle.Fill;
            Controls.Add (plot_view);

            var rnd = new Random ();

            var plot_model = new PlotModel ();
            var axis = new CategoryAxis { Position = AxisPosition.Bottom };
            plot_model.Axes.Add (axis);

            var series = new ColumnSeries ();
            foreach (var row in table)
            {
                axis.Labels.Add (row.Key);
                series.Items.Add (new ColumnItem (row.Value.First[0], axis.Labels.IndexOf (row.Key)) {
                    Color = OxyColor.FromHsv (rnd.NextDouble (), 1, 1)
                });
            }

            plot_model.Series.Add (series);
            plot_view.Model = plot_model;
        }
    }
}
