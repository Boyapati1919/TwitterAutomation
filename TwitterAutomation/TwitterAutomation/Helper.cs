using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TwitterAutomation
{
    public static class Helper
    {
        public static string PerfectString(string Comment)
        {
            string val = "𝐊..𝐊..𝐊.. 𝐊𝐢𝐥𝐥-𝐊𝐢𝐥𝐥" ;
            //string ld = ConvertToConsolas(val);
            string outputString = string.Empty;
            for (int i = 0; i < Comment.Length; i++)
            {
                int ascii =Comment.ToCharArray()[i];
                if (ascii >= 32 && ascii <= 127)
                {
                    outputString = outputString + Comment.ToCharArray()[i];
                }
            }
            return outputString;
        }
        static string ConvertToConsolas(string input)
        {
            // Mapping table for characters that differ in width
            string[] widthConversionTable = {
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
            "##########                                      !!##$$        ",
            "##########                                      !!##$$        ",
            "##          ##    ##    ##  ##########  ##          ##    ##  ",
            "##          ##    ##    ##  ##########  ##          ##    ##  ",
            "##  ##      ##    ##    ##    ##      ######  ######    ##    ",
            "##  ##      ##    ##    ##    ##      ######  ######    ##    ",
            "##    ##  ##    ##    ##        ##  ##    ##  ##    ##      ##",
            "##    ##  ##    ##    ##        ##  ##    ##  ##    ##      ##",
            "##      ##      ######          ##    ##      ######    ##    ",
            "##      ##      ######          ##    ##      ######    ##    ",
            "##    ##  ##    ##    ##        ##  ##    ##  ##    ##      ##",
            "##    ##  ##    ##    ##        ##  ##    ##  ##    ##      ##",
            "##  ##      ##    ##    ##    ##      ######  ######    ##    ",
            "##  ##      ##    ##    ##    ##      ######  ######    ##    ",
            "##          ##    ##    ##  ##########  ##          ##    ##  ",
            "##          ##    ##    ##  ##########  ##          ##    ##  ",
            "##########                                      !!##$$        ",
            "##########                                      !!##$$        ",
        };

            // Convert characters
            string result = "";
            foreach (char c in input)
            {
                int index = (int)c;
                if (index >= 32 && index <= 126)
                {
                    result += widthConversionTable[0][index - 32];
                }
                else
                {
                    // If the character is not in the conversion table, just keep it as it is
                    result += c;
                }
            }

            return result;
        }
    }

}
