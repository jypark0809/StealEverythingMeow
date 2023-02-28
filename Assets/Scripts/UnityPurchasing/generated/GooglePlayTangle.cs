// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("q/JjHJ1lKZ2G2Za8w8E9OSywgvfIyrZmq3BkG3M1YKjJMeVdtyNZrLVPcoI6mp5cj50Q9beV0rjt9Sc66ubEetxLfHIvcGO9zEOSP1g5168TJKHq5UKY+lbymXcUKuBIkOI+cS19MNNG5YVDzbFJ3HfbaVo9bsOqqJM3VmtkUYwK2TyDm2HvtgnzSGASbgJh4+YZoft9sR+9gzk/xakTVcBDTUJywENIQMBDQ0Lxw51ShackYSuyseObRHMr+4lmElm1NWh6bTbwfC83GnOChpPHmU0Z3RbofuysEXLAQ2ByT0RLaMQKxLVPQ0NDR0JBQOFw2EIjvtWk/wmh+M77GdCEw5EmWNhWRhO0Z7JCaM/Sk3kLmEhyUA8ZwCLBbZjVfUBBQ0JD");
        private static int[] order = new int[] { 3,10,12,4,12,9,7,11,10,10,13,12,13,13,14 };
        private static int key = 66;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
