using System;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace LiftCommon
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
	public class Utilities
	{
		public Utilities()
		{
		}

	
		/// <summary>
		/// Extract only the hex digits from a string.
		/// </summary>
		public static string ExtractHexDigits(string input)
		{
			// remove any characters that are not digits (like #)
			Regex isHexDigit 
				= new Regex("[abcdefABCDEF\\d]+", RegexOptions.Compiled);
			string newnum = "";
			foreach (char c in input)
			{
				if (isHexDigit.IsMatch(c.ToString()))
					newnum += c.ToString();
			}
			return newnum;
		}

		public static int mulDiv( int n, int d, int m )
		{
			int result = 0;
			
			if (n == 0) return result;
			if (d == 0) return result;
			if (m == 0) return result;

			double temp  = n * m;
			temp = temp / (double) d;
			result = Convert.ToInt32( temp );

			return result;
		}

		
		public static uint fromHex( string h )
		{
			uint result = 0;
			uint x = 1;

			if (h == null) return 0;
			if (h.Length < 2) return 0;
			if (h.Length % 2 != 0) return 0;

			for (int offset = h.Length - 2; offset >= 0; offset -= 2)
			{
				byte b = fromHex( h, offset );
				result += ((uint) b * x);
				x *= 256;
			}

			return result;
		}






		public static byte fromHex( string h, int startAt)
		{
			byte result = 0;

			char msb = h[startAt];
			char lsb =  h[startAt + 1];

			result = numOf( msb );

			result = (byte) (result << 4);
			result += numOf( lsb );

			return result;
	
		}
		

		public static byte numOf( char ch )
		{
			int n = (int) '0';
			int _ch = (int) ch;

			switch( ch )
			{
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					n = _ch - '0';
					break;
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
					n = (_ch - 'A') + 10;
					break;
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
					n = (_ch - 'a') + 10;
					break;
			}

			return (byte) n;

		}
		

		public static string toHex(byte  b )
		{	
			string h = string.Empty;
			int msb =  b & 0xF0;
			msb = b >> 4;

			h += hexOf( (byte) msb );

			int lsb = b & 0x0F;
	
			h+= hexOf( (byte) lsb);

			Debug.Assert( h.Length == 2, "Invalid hex length");

			return h;
		}

		public static char hexOf( byte n ) 
		{
			int ch = '\0';

			switch( (int) n )
			{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					ch =   n + '0';
					break;
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
					ch = (n - 10) + 'A';
					break;
			}

			return (char) ch;
		}


        /// <summary>
        /// Converts a byte array into hexadecimal.
        /// </summary>
        public static string ByteToHex(byte[] byteArray)
        {
            string result = string.Empty;
            string hexValue = string.Empty;

            foreach (byte b in byteArray)
            {
                hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
                result += (hexValue.Length == 1 ? "0" : "") + hexValue;
            }

            return result;

        }
			
	}
}
