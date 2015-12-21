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
    public class AverageSentenceWordLengthProcessor : IProcessor<String, int>
    {
        public AverageSentenceWordLengthProcessor ()
        {
        }

        private int current_WordCount = 0;
        private int current_WordLength = 0;

        #region IProcessor implementation

        int IProcessor<string, int>.Process (string word)
        {
            if (word [word.Length - 1] == '.' || word [word.Length - 1] == '!' || word[word.Length -1] == '\n')
            {
                current_WordCount++;
                current_WordLength += word.Length - 1;

                int what_to_return = (int)Math.Floor ((float)(current_WordLength) / ((float)current_WordCount));

                current_WordCount = 0;
                current_WordLength = 0;

                return what_to_return;
            } else
            {
                current_WordCount++;
                current_WordLength += word.Length;

                return 0;//(int)Math.Floor ((float)(current_WordLength) / ((float)current_WordCount));
            }
        }

        #endregion
    }
}

