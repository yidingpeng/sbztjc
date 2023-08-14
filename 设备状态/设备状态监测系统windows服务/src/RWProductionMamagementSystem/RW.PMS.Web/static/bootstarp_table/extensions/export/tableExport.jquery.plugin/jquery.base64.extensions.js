//������ʹ��bootstrap table�е����ݵ�������ʱ������һ����ֵ����⣬��ʹ�ò������ݽ��в��Ե�ʱ����������Ľ�����е����ݵ�����excel��txt���ļ��У�
//���������Ӧ�õ�web��Ŀ��ʱ�����������ʱʼ��û���κ���Ӧ������һ�������Ų�����շ��������ڱ�������к������ĵ��µģ�
//����ҳ�Ŀ�����ѡ���б�һ�� Uncaught INVALID_CHARACTER_ERR: DOM Exception 5 ���⡣�������������BootStrap table Ĭ�ϲ�֧�����ģ�
//ֻ��ʶ�� ASCII�ַ���Ϊ����bootstrap table ʶ������ģ�������Ҫ��չ��� jQuery.base64.js ������������ʶ���unicode �ַ���
//���������ķ���ʱ����Ĭ�ϵ�jquery.base64.js �ļ������滻�������ṩ����չ�棺

jQuery.base64 = (function ($) {

    // private property
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    // private method for UTF-8 encoding
    function utf8Encode(string) {
        string = string.replace(/\r\n/g,"\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    }

    function encode(input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = utf8Encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);
            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;
            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output +
                keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                keyStr.charAt(enc3) + keyStr.charAt(enc4);
        }
        return output;
    }

    return {
        encode: function (str) {
            return encode(str);
        }
    };

}(jQuery));