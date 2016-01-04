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
            #if LOWER_CASE_TEXT_FILE_LOADER
            var tfl = new LowerCaseTextFileLoader();
            #else
            var tfl = new TextFileLoader ();
            #endif

            #if SENTENCE_SPLITTER
            var ws = new SentenceSplitter ();
            #else
            var ws = new WordSplitter();
            #endif

            #if SENTENCE_LENGTH
            var wlp = new SentenceLengthProcessor ();
            #else
            var wlp = new WordLengthProcessor();
            #endif

            var aswlp = new AverageSentenceWordLengthProcessor ();

            var cv = new ChartVisualizer<string, int> ();
            var tv = new TextVisualizer<string, int> ();
            var cvd = new ChartVisualizer<string, double> ();
            var tvd = new TextVisualizer<string, double> ();

            var analyzer1 = new Analyzer<string, int> ("DemoText.txt", tfl);
            var analyzer3 = new Analyzer<string, double> ("DemoText.txt", tfl);
            var analyzer2 = new Analyzer<string, int> ("This is a test!");

            analyzer1.Analyze (
                ws,
                wlp,
                tv,
                VisualizationTarget.FirstElement
            );

            Console.WriteLine ();

            analyzer3.Analyze (
                ws,
                aswlp,
                tvd,
                VisualizationTarget.FirstElement
            );

            analyzer3.Analyze (
                ws,
                aswlp,
                cvd,
                VisualizationTarget.FirstElement
            );

            analyzer1.Analyze (
                ws,
                wlp,
                cv,
                VisualizationTarget.FirstElement
            );

            analyzer2.Analyze (
                ws,
                wlp,
                cv,
                VisualizationTarget.FirstElement
            );
        }
    }
}
