//
//  Program.cs
//
//  Author:
//       dboeg <${AuthorEmail}>
//
//  Copyright (c) 2015 dboeg
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

namespace FeatureTextAnalyzer
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            var tfl = new TextFileLoader ();

            var ws = new WordSplitter ();

            var wlp = new WordLengthProcessor ();
            var aswlp = new AverageSentenceWordLengthProcessor ();

            var cv = new ChartVisualizer<string, double> ();
            var tv = new TextVisualizer<string, double> ();

            var analyzer = new Analyzer<string, double> ("DemoText.txt", tfl);
            analyzer.Analyze (
                ws,
                aswlp,
                cv,
                VisualizationTarget.FirstElement
            );
        }
    }
}
