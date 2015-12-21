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

        public Dictionary<TContent, Pair<List<TValue>, int>> Table { get; private set; }

        public void Split (Func<TContent, IEnumerable<TContent>> splitter)
        {
            Tokens = splitter (Content);
        }

        public void Split (ISplitter<TContent> step)
        {
            Split (step.Split);
        }

        public void Process (Func<TContent, Pair<TContent, TValue>> processor)
        {
            Table = new Dictionary<TContent, Pair<List<TValue>, int>> ();
            foreach (var token in Tokens)
            {
                var tuple = processor (token);

                if (tuple == null || tuple.First == null || tuple.First.Equals (default (TContent)))
                    continue;

                if (Table.ContainsKey (tuple.First))
                {
                    var row = Table [tuple.First];
                    if (!tuple.Second.Equals (default (TValue)))
                        row.First.Add (tuple.Second);
                    row.Second += 1;
                }
                else
                {
                    var list = new List<TValue> ();
                    if (!tuple.Second.Equals (default (TValue)))
                        list.Add (tuple.Second);
                    Table.Add (tuple.First, new Pair<List<TValue>, int> (list, 1));
                }
            }
        }

        public void Process (IProcessor<TContent, TValue> step)
        {
            Process (step.Process);
        }

        public void Visualize (Action<Dictionary<TContent, Pair<List<TValue>, int>>> visualizer)
        {
            visualizer (Table);
        }

        public void Visualize (IVisualizer<TContent, TValue> visualizer)
        {
            Visualize (visualizer.Visualize);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            Func<TContent, Pair<TContent, TValue>> processor,
            Action<Dictionary<TContent, Pair<List<TValue>, int>>> visualizer)
        {
            Split (splitter);
            Process (processor);
            Visualize (visualizer);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            Func<TContent, Pair<TContent, TValue>> processor,
            IVisualizer<TContent, TValue> visualizer)
        {
            Analyze (splitter, processor, visualizer.Visualize);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            IProcessor<TContent, TValue> processor,
            Action<Dictionary<TContent, Pair<List<TValue>, int>>> visualizer)
        {
            Analyze (splitter, processor.Process, visualizer);
        }

        public void Analyze (
            Func<TContent, IEnumerable<TContent>> splitter,
            IProcessor<TContent, TValue> processor,
            IVisualizer<TContent, TValue> visualizer)
        {
            Analyze (splitter, processor.Process, visualizer.Visualize);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            Func<TContent, Pair<TContent, TValue>> processor,
            Action<Dictionary<TContent, Pair<List<TValue>, int>>> visualizer)
        {
            Analyze (splitter.Split, processor, visualizer);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            Func<TContent, Pair<TContent, TValue>> processor,
            IVisualizer<TContent, TValue> visualizer)
        {
            Analyze (splitter.Split, processor, visualizer.Visualize);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            IProcessor<TContent, TValue> processor,
            Action<Dictionary<TContent, Pair<List<TValue>, int>>> visualizer)
        {
            Analyze (splitter.Split, processor.Process, visualizer);
        }

        public void Analyze (
            ISplitter<TContent> splitter,
            IProcessor<TContent, TValue> processor,
            IVisualizer<TContent, TValue> visualizer)
        {
            Analyze (splitter.Split, processor.Process, visualizer.Visualize);
        }
    }

    public sealed class Pair<T1, T2>
    {
        public Pair (T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public T1 First { get; set; }

        public T2 Second { get; set; }
    }
}
