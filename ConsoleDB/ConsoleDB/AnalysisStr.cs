using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//默认的请求字符串格式如下:
//                          UserID#KindOFReq#Para1#Para2#......

namespace ConsoleDB
{
    class AnalysisStr
    {
        private char    splitChar='#';
        private string  analysisStr;
        private string[] resultSet;         //存放分割后的字符串
        
        public AnalysisStr(string _analysisStr)
        {
            analysisStr = _analysisStr;
            resultSet = analysisStr.Split(new char[]{splitChar});
        }

        public string GetUserID()
        {
            return resultSet[0];
        }

        public int GetKindOfReq()
        {
            return  int.Parse(resultSet[1]);
        }


        //根据索引返回参数，这里为了
        public string GetParaIndexOf(int index)
        {
            if (index + 2 > resultSet.Length-1)
                return null;
            else
                return resultSet[index + 2];
        }
        
        //针对传文件可能出的分隔符，需要特别处理
        public string GetParaFromIndex(int index)
        {
            string strTemp="";
            if (index + 2 > resultSet.Length-1)
                return null;
            else
            {
                for (int i = index + 2; i <= resultSet.Length-2; i++)
                {
                    strTemp = strTemp + resultSet[i] + splitChar;
                }
                strTemp = strTemp + resultSet[resultSet.Length-1];
                return strTemp;
            }
        }
    }
}
