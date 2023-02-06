// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("zQsrLMjDDn4JV6VDv3ByiXAcey8VlpiXpxWWnZUVlpaXEHIw/4QrKzFpYYqFJ46WxiEDEFMuf8uHtvxW15nHw15un/2QNhI9XaBP0Utz5l+nFZa1p5qRnr0R3xFgmpaWlpKXlF8rLUcJ0ixgvlcM68s1PA6PDEL+6l5PIzOClWUFloT0eU+iImaIoXm/i8wDFo2IGokO4Aznb3cuuLSDNAUIL7NYpGxlMDkFPOHKKWcpT30IyGF/i4Rf/+OBcrbsLP005IBIye++CIZ3Dsk/T5Pvf8QRFBRZ7mrAPpjpP9pXBHJKTf+fbPqM3k4yyV+VNsHLOq9JSx8OBVWozIHr+VF0U8640hPlqiJ+s7S3rfK7R4ZJKIV5Pw+jYhAE03+a+JWUlpeW");
        private static int[] order = new int[] { 9,1,5,6,9,7,10,12,11,9,13,13,13,13,14 };
        private static int key = 151;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
