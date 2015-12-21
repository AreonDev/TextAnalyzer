//
//  Analyzer.cs
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
using System.IO;
using System.Collections.Generic;

namespace TextAnalyzer
{
    public sealed class Analyzer<TContent, TValue>
    {
        public Analyzer (TContent text)
        {
            Content = text;
        }

        public Analyzer (FileInfo file, Func<FileInfo, TContent> loader)
        {
            Content = loader (file);
        }

        public Analyzer (string filename, Func<FileInfo, TContent> loader) : this (new FileInfo (filename), loader)
        {
        }

        public Analyzer (string filename, ILoader<TContent> loader)
        {
            Content = loader.Load (new FileInfo (filename));
        }

        public TContent Content { get; private set; }

        public IEnumerable<TContent> Tokens { get; private set; }

        public Dictionary<TContent, List<TValue>> Table { get; private set; }

        public void Split (Func<TContent, IEnumerable<TContent>> splitter)
        {
            Tokens = splitter (Content);
        }

        public void Split (ISplitter<TContent> step)
        {
            Split (step.Split);
        }

        public void Process (Func<TContent, Tuple<TContent, TValue>> processor)
        {
            Table = new Dictionary<TContent, List<TValue>> ();
            foreach (var token in Tokens)
            {
                var tuple = processor (token);

                if (tuple == null || tuple.Item1 == null || tuple.Item1.Equals (default (TContent)))
                    continue;

                if (Table.ContainsKey (tuple.Item1))
                {
                    var row = Table [tuple.Item1];
                    if (!tuple.Item2.Equals (default (TValue)))
                        row.Add (tuple.Item2);
                }
                else
                {
                    var list = new List<TValue> ();
                    if (!tuple.Item2.Equals (default (TValue)))
                        list.Add (tuple.Item2);
                    Table.Add (tuple.Item1, new List<TValue> (list));
                }
            }
        }

        public void Process (IProcessor<TContent, TValue> step)
        {
            Process (step.Process);
        }

        public void Visualize (Action<Dictionary<TContent, List<TValue>>, VisualizationTarget> visualizer,
            VisualizationTarget target)
        {
            visualizer (Table, target);
        }

        public void Visualize (IVisualizer<TContent, TValue> visualizer, VisualizationTarget target)
        {
            Visualize (visualizer.Visualize, target);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            Func<TContent, Tuple<TContent, TValue>> processor,
            Action<Dictionary<TContent, List<TValue>>, VisualizationTarget> visualizer,
            VisualizationTarget target)
        {
            Split (splitter);
            Process (processor);
            Visualize (visualizer, target);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            Func<TContent, Tuple<TContent, TValue>> processor,
            IVisualizer<TContent, TValue> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter, processor, visualizer.Visualize, target);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            IProcessor<TContent, TValue> processor,
            Action<Dictionary<TContent, List<TValue>>, VisualizationTarget> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter, processor.Process, visualizer, target);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            IProcessor<TContent, TValue> processor,
            IVisualizer<TContent, TValue> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter, processor.Process, visualizer.Visualize, target);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            Func<TContent, Tuple<TContent, TValue>> processor,
            Action<Dictionary<TContent, List<TValue>>, VisualizationTarget> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter.Split, processor, visualizer, target);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            Func<TContent, Tuple<TContent, TValue>> processor,
            IVisualizer<TContent, TValue> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter.Split, processor, visualizer.Visualize, target);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            IProcessor<TContent, TValue> processor,
            Action<Dictionary<TContent, List<TValue>>, VisualizationTarget> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter.Split, processor.Process, visualizer, target);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            IProcessor<TContent, TValue> processor,
            IVisualizer<TContent, TValue> visualizer,
            VisualizationTarget target)
        {
            Analyze (splitter.Split, processor.Process, visualizer.Visualize, target);
        }
    }

    public enum VisualizationTarget
    {
        CollectedElements,
        FirstElement,
        TotalElementCount
    }
}
