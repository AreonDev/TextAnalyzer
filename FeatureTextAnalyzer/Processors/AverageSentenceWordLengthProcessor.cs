//
//  AverageSentenceWordLengthProcessor.cs
//
//  Author:
//       dboeg <>
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
    public partial class AverageSentenceWordLengthProcessor : IProcessor<string, double>
    {
        int current_WordCount;
        int current_WordLength;
        string sentence = "";

        #region IProcessor implementation

        public virtual Tuple<string, double> Process (string word)
        {
            if (word [word.Length - 1] == '.' ||
                word [word.Length - 1] == '!' ||
                word [word.Length - 1] == '?')
            {
                current_WordCount++;
                current_WordLength += word.Length - 1;
                sentence += word;

                Tuple<string, double> what_to_return = new Tuple<string, double> (
                    sentence.Substring (0, sentence.Length),
                    current_WordLength / (double) current_WordCount
                );

                current_WordCount = 0;
                current_WordLength = 0;
                sentence = "";

                return what_to_return;
            }
            else
            {
                current_WordCount++;
                current_WordLength += word.Length;
                sentence += word + " ";

                return null;
            }
        }

        #endregion
    }
}

