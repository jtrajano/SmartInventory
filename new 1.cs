                    // Move the start to one position after the last occurrence
                    // dict is zero index so add 1
                    start = dict[c] + 1;
                }

                // 3. Update the character’s last index
                dict[c] = i;

                // 4. Update max length (window size = i - start + 1)
                // i here is 0 index so add 1
                maxLength = Math.Max(maxLength, i - start + 1);
            }

            return maxLength;
        }
        public static int LengthOfLongestSubstringWith2DistinctChar(string word)
        {
            var dict = new Dictionary<char, int>();
            var maxLength = 0;
            int start = 0;

            for (int i = 0; i < word.Length; i++)
            {
                char c = word[i];
                dict[c] = dict.ContainsKey(c) ? dict[c] + 1 : 1;

                while (dict.Count > 2)
                {
                    var s = word[start];
                    dict[s] = dict[s] + -1;
                    if (dict[s] == 0)
                        dict.Remove(s);

                    start++;
                }

                maxLength = Math.Max(maxLength, i - start + 1);

            }

            return maxLength;

        }

        public static char? GetNonReapeatedCharacter(string word)
        {

            if (string.IsNullOrEmpty(word)) return null;

            var dict = new Dictionary<char, int>();

            foreach (char c in word)
                dict[c] = dict.ContainsKey(c) ? dict[c] + 1 : 1;

            foreach (char c in word)
                if (dict[c] == 1) return c;

            return null;

            //          if (string.IsNullOrEmpty(word)) return null;

            // return word.GroupBy(c => c)
            //            .FirstOrDefault(g => g.Count() == 1)?
            //            .Key;
        }
        //[{1,2},{2,4}]
        public static int[][]? MergeOverlappingIntervals(int[][] intervals)
        {
            if (intervals.Length == 0) return null;
            // sort the array

            Array.Sort(intervals, (a, b) => a[0] - b[0]);
            // initialize the result array
            var result = new List<int[]>{ intervals[0]};

            // iterate
            foreach (var current in intervals)
            {
                var r = result[result.Count - 1];
                if (current[0] <= r[1])
                {
                    r[1] = Math.Max(current[1], r[1]);
                }
                else
                {
                    result.Add(current);
                }
            }

            return result.ToArray();
        }

        public static string[][] GroupAnagrams(string[] words)
        {
            var map = new Dictionary<string, List<string>>();

            foreach (var item in words)
            {
                var c = item.ToCharArray();
                Array.Sort(c);
                var s = new string(c);

                if (map.ContainsKey(s))
                    map[s].Add(item);
                else
                    map[s] = new List<string> { item };
                
            }
            return map.Select(m => m.Value.ToArray()).ToArray();
        }

    }
}
