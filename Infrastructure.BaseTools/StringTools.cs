using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseTools
{
    public class StringTools
    {
        public string StandardizeCharacters(string text)
        {
            return text
                .Replace('ك', 'ک')
                .Replace('ي', 'ی')
                .Replace('٠', '0')
                .Replace('١', '1')
                .Replace('٢', '2')
                .Replace('٣', '3')
                .Replace('٤', '4')
                .Replace('٥', '5')
                .Replace('٦', '6')
                .Replace('٧', '7')
                .Replace('٨', '8')
                .Replace('٩', '9')
                .Replace("ـّ", "")
                .Replace('ة', 'ه')
                .Replace('ٱ', 'ا')
                .Replace('إ', 'ا')
                .Replace('أ','ا')
                .Replace('ٵ','ا')
                .Replace("ـآ", "ا")
                .Replace("ﻼ","لا")
                .Replace("ﲓ","یم")
                .Replace("ﳰ", "یم")
                .Replace("ﳝ", "یم")
                .Replace("ﱘ","یم")
                .Replace("ﲅ", "لم")
                .Replace("ﳭ", "لم")
                .Replace("ﳌ", "لم")
                .Replace("ﱂ", "لم")
                .Replace("ﻻ","لا")
                .Replace("ـلا","لا")
                .Replace("ﻼ","لا")
                .Replace("ﷲ", "الله")
                .Replace('ڛ','س')
                .Replace("ـڛـ", "س")
                .Replace("ڛـ", "س")
                .Replace("ـڛ", "س")
                .Replace('ڤ','ف')
                .Replace('ڥ', 'ف')
                .Replace('ڨ', 'ق')
                .Replace('ݣ','ک')
                .Replace('ڭ', 'ک')
                ;

        }
    }
}
