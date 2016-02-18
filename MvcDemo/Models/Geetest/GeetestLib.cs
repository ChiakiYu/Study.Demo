using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MvcDemo.Models.Geetest
{
    /// <summary>
    ///     GeetestLib 极验验证C# SDK基本库
    /// </summary>
    public class GeetestLib
    {
        /// <summary>
        ///     SDK版本号
        /// </summary>
        public const string Version = "3.1.1";

        /// <summary>
        ///     SDK开发语言
        /// </summary>
        public const string SdkLang = "csharp";

        /// <summary>
        ///     极验验证API URL
        /// </summary>
        protected const string ApiUrl = "http://api.geetest.com";

        /// <summary>
        ///     register url
        /// </summary>
        protected const string RegisterUrl = "/register.php";

        /// <summary>
        ///     validate url
        /// </summary>
        protected const string ValidateUrl = "/validate.php";

        /// <summary>
        ///     极验验证API服务状态Session Key
        /// </summary>
        public const string GtServerStatusSessionKey = "gt_server_status";

        /// <summary>
        ///     极验验证二次验证表单数据 Chllenge
        /// </summary>
        public const string FnGeetestChallenge = "geetest_challenge";

        /// <summary>
        ///     极验验证二次验证表单数据 Validate
        /// </summary>
        public const string FnGeetestValidate = "geetest_validate";

        /// <summary>
        ///     极验验证二次验证表单数据 Seccode
        /// </summary>
        public const string FnGeetestSeccode = "geetest_seccode";

        /// <summary>
        ///     验证成功结果字符串
        /// </summary>
        public const int SuccessResult = 1;

        /// <summary>
        ///     证结失败验果字符串
        /// </summary>
        public const int FailResult = 0;

        /// <summary>
        ///     判定为机器人结果字符串
        /// </summary>
        public const string ForbiddenResult = "forbidden";

        private readonly string captchaID = "";
        private readonly string privateKey = "";
        private string responseStr = "";

        /// <summary>
        ///     GeetestLib构造函数
        /// </summary>
        /// <param name="publicKey">极验验证公钥</param>
        /// <param name="privateKey">极验验证私钥</param>
        public GeetestLib(string publicKey, string privateKey)
        {
            this.privateKey = privateKey;
            captchaID = publicKey;
        }

        private int GetRandomNum()
        {
            var rand = new Random();
            var randRes = rand.Next(100);
            return randRes;
        }

        /// <summary>
        ///     验证初始化预处理
        /// </summary>
        /// <returns>初始化结果</returns>
        public byte PreProcess()
        {
            if (captchaID == null)
            {
                Console.WriteLine("publicKey is null!");
            }
            else
            {
                var challenge = RegisterChallenge();
                if (challenge.Length == 32)
                {
                    GetSuccessPreProcessRes(challenge);
                    return 1;
                }
                GetFailPreProcessRes();
                Console.WriteLine("Server regist challenge failed!");
            }

            return 0;
        }

        public string GetResponseStr()
        {
            return responseStr;
        }

        /// <summary>
        ///     预处理失败后的返回格式串
        /// </summary>
        private void GetFailPreProcessRes()
        {
            var rand1 = GetRandomNum();
            var rand2 = GetRandomNum();
            var md5Str1 = md5Encode(rand1 + "");
            var md5Str2 = md5Encode(rand2 + "");
            var challenge = md5Str1 + md5Str2.Substring(0, 2);
            responseStr = "{" + string.Format("\"success\":{0},\"gt\":\"{1}\",\"challenge\":\"{2}\"", 0, captchaID, challenge) + "}";
        }

        /// <summary>
        ///     预处理成功后的标准串
        /// </summary>
        private void GetSuccessPreProcessRes(string challenge)
        {
            challenge = md5Encode(challenge + privateKey);
            responseStr = "{" + string.Format("\"success\":{0},\"gt\":\"{1}\",\"challenge\":\"{2}\"", 1, captchaID, challenge) + "}";
        }

        /// <summary>
        ///     failback模式的验证方式
        /// </summary>
        /// <param name="challenge">failback模式下用于与validate一起解码答案， 判断验证是否正确</param>
        /// <param name="validate">failback模式下用于与challenge一起解码答案， 判断验证是否正确</param>
        /// <param name="seccode">failback模式下，其实是个没用的参数</param>
        /// <returns>验证结果</returns>
        public int FailbackValidateRequest(string challenge, string validate, string seccode)
        {
            if (!RequestIsLegal(challenge, validate, seccode)) return FailResult;
            var validateStr = validate.Split('_');
            var encodeAns = validateStr[0];
            var encodeFullBgImgIndex = validateStr[1];
            var encodeImgGrpIndex = validateStr[2];
            var decodeAns = DecodeResponse(challenge, encodeAns);
            var decodeFullBgImgIndex = DecodeResponse(challenge, encodeFullBgImgIndex);
            var decodeImgGrpIndex = DecodeResponse(challenge, encodeImgGrpIndex);
            var validateResult = ValidateFailImage(decodeAns, decodeFullBgImgIndex, decodeImgGrpIndex);
            return validateResult;
        }

        private int ValidateFailImage(int ans, int full_bg_index, int img_grp_index)
        {
            const int thread = 3;
            var fullBgName = md5Encode(full_bg_index + "").Substring(0, 10);
            var bgName = md5Encode(img_grp_index + "").Substring(10, 10);
            var answerDecode = "";
            for (var i = 0; i < 9; i++)
            {
                switch (i % 2)
                {
                    case 0:
                        answerDecode += fullBgName.ElementAt(i);
                        break;
                    case 1:
                        answerDecode += bgName.ElementAt(i);
                        break;
                }
            }
            var xDecode = answerDecode.Substring(4);
            var xInt = Convert.ToInt32(xDecode, 16);
            var result = xInt % 200;
            if (result < 40) result = 40;
            return Math.Abs(ans - result) < thread ? SuccessResult : FailResult;
        }

        private bool RequestIsLegal(string challenge, string validate, string seccode)
        {
            return !challenge.Equals(string.Empty) && !validate.Equals(string.Empty) && !seccode.Equals(string.Empty);
        }

        /// <summary>
        ///     向gt-server进行二次验证
        /// </summary>
        /// <param name="challenge">本次验证会话的唯一标识</param>
        /// <param name="validate">拖动完成后server端返回的验证结果标识字符串</param>
        /// <param name="seccode">验证结果的校验码，如果gt-server返回的不与这个值相等则表明验证失败</param>
        /// <returns>二次验证结果</returns>
        public int EnhencedValidateRequest(string challenge, string validate, string seccode)
        {
            if (!RequestIsLegal(challenge, validate, seccode)) return FailResult;
            if (validate.Length <= 0 || !CheckResultByPrivate(challenge, validate)) return FailResult;
            var query = "seccode=" + seccode + "&sdk=csharp_" + Version;
            var response = "";
            try
            {
                response = PostValidate(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return response.Equals(md5Encode(seccode)) ? SuccessResult : FailResult;
        }

        private string ReadContentFromGet(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 20000;
                var response = (HttpWebResponse)request.GetResponse();
                var myResponseStream = response.GetResponseStream();
                var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                var retstring = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retstring;
            }
            catch
            {
                return "";
            }
        }

        private string RegisterChallenge()
        {
            var url = string.Format("{0}{1}?gt={2}", ApiUrl, RegisterUrl, captchaID);
            var retstring = ReadContentFromGet(url);
            return retstring;
        }

        private bool CheckResultByPrivate(string origin, string validate)
        {
            var encodeStr = md5Encode(privateKey + "geetest" + origin);
            return validate.Equals(encodeStr);
        }

        private string PostValidate(string data)
        {
            var url = string.Format("{0}{1}", ApiUrl, ValidateUrl);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(data);
            // 发送数据
            var myRequestStream = request.GetRequestStream();
            var requestBytes = Encoding.ASCII.GetBytes(data);
            myRequestStream.Write(requestBytes, 0, requestBytes.Length);
            myRequestStream.Close();

            var response = (HttpWebResponse)request.GetResponse();
            // 读取返回信息
            var myResponseStream = response.GetResponseStream();
            var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            var retstring = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retstring;
        }

        private int DecodeRandBase(string challenge)
        {
            var baseStr = challenge.Substring(32, 2);
            var tempList = new List<int>();
            for (var i = 0; i < baseStr.Length; i++)
            {
                var tempAscii = (int)baseStr[i];
                tempList.Add((tempAscii > 57)
                    ? (tempAscii - 87)
                    : (tempAscii - 48));
            }
            var result = tempList.ElementAt(0) * 36 + tempList.ElementAt(1);
            return result;
        }

        private int DecodeResponse(string challenge, string str)
        {
            if (str.Length > 100) return 0;
            var shuzi = new[] { 1, 2, 5, 10, 50 };
            var chongfu = "";
            var key = new Hashtable();
            var count = 0;
            for (var i = 0; i < challenge.Length; i++)
            {
                var item = challenge.ElementAt(i) + "";
                if (chongfu.Contains(item)) continue;
                var value = shuzi[count % 5];
                chongfu += item;
                count++;
                key.Add(item, value);
            }
            var res = 0;
            for (var i = 0; i < str.Length; i++) res += (int)key[str[i] + ""];
            res = res - DecodeRandBase(challenge);
            return res;
        }

        private string md5Encode(string plainText)
        {
            var md5 = new MD5CryptoServiceProvider();
            var t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(plainText)));
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
    }
}