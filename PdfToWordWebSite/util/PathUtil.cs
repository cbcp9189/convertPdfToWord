using PdfToWordWebSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace PdfToWordWebSite.util
{
    public class PathUtil
    {
        public static List<PdfModel> mappingList = new List<PdfModel>();
        public static String qianzhui = "opt/";
        //public static Dictionary<string, string> paramMap = new Dictionary<string, string>(); 

        static PathUtil()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                String path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "PathFile.xml");
                xmlDoc.Load(path);
                XmlNode xn = xmlDoc.SelectSingleNode("pdflist");
                XmlNodeList xnl = xn.ChildNodes;
                foreach (XmlNode xn1 in xnl)
                {
                    PdfModel model = new PdfModel();
                    XmlElement xe = (XmlElement)xn1;
                    XmlNodeList xnl0 = xe.ChildNodes;
                    model.doctype = int.Parse(xnl0.Item(0).InnerText);
                    model.year = xnl0.Item(1).InnerText;
                    model.pdfPath = xnl0.Item(2).InnerText;
                    model.excelPath = xnl0.Item(3).InnerText;
                    model.excelHead = xnl0.Item(4).InnerText;
                    mappingList.Add(model);
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        //获取pdf绝对路径
        public static String getAbsolutePdfPath(String pdfPath, int type)
        {
            
            foreach (PdfModel mo in mappingList)
            {

                if (mo.doctype == type && pdfPath.Contains("2017/") && mo.year.Equals("2017") && pdfPath.Contains("GSGGFWB"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath.Replace(mo.excelHead, "").Replace("cninfo", "");
                        break;
                    }

                }
                else if (mo.doctype == type && pdfPath.Contains("2016/") && mo.year.Equals("2016") && pdfPath.Contains("GSGGFWB"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath.Replace(mo.excelHead, "").Replace("cninfo", "");
                        break;
                    }
                }
                else if (mo.doctype == type && !pdfPath.Contains("2016/") && !pdfPath.Contains("2017/") && mo.year.Equals("other") && pdfPath.Contains("GSGGFWB"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath.Replace(mo.excelHead, "").Replace("cninfo", "");
                        break;
                    }
                }
                else if (mo.doctype == type && pdfPath.Contains("cninfo/") && mo.year.Equals("cninfo"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && pdfPath.Contains("cninfoG") && mo.year.Equals("cninfoG"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("userupload"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("testuserupload"))
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("article"))  //微信
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("report") && pdfPath.Contains("report"))  //研报
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("luobo") && pdfPath.Contains("luobo"))  //萝卜
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath.Replace(mo.excelHead, "");
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("hkexnews"))  //港股
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }
                else if (mo.doctype == type && mo.year.Equals("bond"))  //bond
                {
                    if (pdfPath.Contains(qianzhui))
                    {
                        pdfPath = containOpt(pdfPath);
                        break;
                    }
                    else
                    {
                        pdfPath = mo.pdfPath + pdfPath;
                        break;
                    }
                }

            }
            return pdfPath;
        }

        public static String containOpt(String pdf)
        {
            pdf = "U:/" + pdf;
            return pdf;
        }

        //获取exlce绝对路径
        public static String getAbsolutExcelPath(String pdfPath, int type)
        {
            String excelPath = "";
            foreach (PdfModel mo in mappingList)
            {
                if (mo.doctype == type)
                {
                    excelPath = mo.excelPath + pdfPath;
                    excelPath = Path.ChangeExtension(excelPath, "xlsx");
                    break;
                }
            }
            Console.WriteLine(excelPath);
            return excelPath;
        }
    }
}