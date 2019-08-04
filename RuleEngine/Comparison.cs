using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEngine
{
    class Comparison
    {
        internal List<object> DoComparison(List<List<object>> jsonData, List<List<object>> ruleData)
        {
            List<object> resultValue = new List<object>();
            try
            {
                for (int i = 0; i < jsonData.Count; i++)
                {
                    for (int j = 1; j < ruleData.Count; j++)
                    {
                        if ((Equals(jsonData[i][0], ruleData[j][0])) && (Equals(jsonData[i][1], ruleData[j][1])))
                        {
                            string comparisonTypeValue = Convert.ToString(ruleData[j][3]).ToLowerInvariant(); ;
                            switch (comparisonTypeValue)
                            {
                                case "greaterthan":
                                    string valueType = Convert.ToString(ruleData[j][1]);
                                    var greaterthanComparison = false;
                                    if (valueType == "Datetime")
                                    {
                                        int value = DateTime.Compare(Convert.ToDateTime(jsonData[i][2]), Convert.ToDateTime(ruleData[j][2]));
                                        if (value > 0)
                                        {
                                            greaterthanComparison = true;
                                        }
                                    }
                                    else
                                    {
                                        greaterthanComparison = Convert.ToInt32(jsonData[i][2]) > Convert.ToInt32(ruleData[j][2]);
                                    }
                                    if (greaterthanComparison)
                                    {
                                        resultValue.Add(jsonData[i][0]);
                                    }
                                    break;

                                case "equal":
                                    var equalComparison = Equals(jsonData[i][2], ruleData[j][2]);
                                    if (equalComparison)
                                    {
                                        resultValue.Add(jsonData[i][0]);
                                    }

                                    break;
                                case "lessthan":
                                    string Type = Convert.ToString(ruleData[j][1]);
                                    var lessthanComparison = false;
                                    if (Type == "Datetime")
                                    {
                                        int value = DateTime.Compare(Convert.ToDateTime(jsonData[i][2]), Convert.ToDateTime(ruleData[j][2]));
                                        if (value > 0)
                                        {
                                            lessthanComparison = true;
                                        }
                                    }
                                    else
                                    {
                                        lessthanComparison = Convert.ToInt32(jsonData[i][2]) > Convert.ToInt32(ruleData[j][2]);
                                    }
                                    if (lessthanComparison)
                                    {
                                        resultValue.Add(jsonData[i][0]);
                                    }
                                    break;
                                case "greaterthanequal":
                                    string typeValue = Convert.ToString(ruleData[j][1]);
                                    var greaterthanEqualComparison = false;
                                    if (typeValue == "Datetime")
                                    {
                                        int value = DateTime.Compare(Convert.ToDateTime(jsonData[i][2]), Convert.ToDateTime(ruleData[j][2]));
                                        if (value >= 0)
                                        {
                                            greaterthanEqualComparison = true;
                                        }
                                    }
                                    else
                                    {
                                        greaterthanEqualComparison = Convert.ToInt32(ruleData[i][2]) >= Convert.ToInt32(ruleData[j][2]);
                                    }
                                    if (greaterthanEqualComparison)
                                    {
                                        resultValue.Add(jsonData[i][0]);
                                    }
                                    break;
                                case "lessthanequal":
                                    string dataTypeValue = Convert.ToString(ruleData[j][1]);
                                    var lessthanEqualComparison = false;
                                    if (dataTypeValue == "Datetime")
                                    {
                                        int value = DateTime.Compare(Convert.ToDateTime(jsonData[i][2]), Convert.ToDateTime(ruleData[j][2]));
                                        if (value >= 0)
                                        {
                                            lessthanEqualComparison = true;
                                        }
                                    }
                                    else
                                    {
                                        lessthanEqualComparison = Convert.ToInt32(jsonData[i][2]) >= Convert.ToInt32(ruleData[j][2]);
                                    }
                                    if (lessthanEqualComparison)
                                    {
                                        resultValue.Add(jsonData[i][0]);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return resultValue;
        }
    }
}
