using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RuleEngine
{
    class RuleEngineImplementation
    {
        private List<List<object>> ReadJsonFile(string path)
        {
            List<List<object>> jsonDataList = new List<List<object>>();
            try
            {
                string jsonFilePath = path;
                StreamReader jsonDataStreamReader = new StreamReader(jsonFilePath);
                string readJsonData = jsonDataStreamReader.ReadToEnd();
                List<DataUnit> jsonItems = JsonConvert.DeserializeObject<List<DataUnit>>(readJsonData);
                jsonDataList = getJsonData(jsonItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return jsonDataList;
        }
        private List<List<object>> ReadRule(string path)
        {
            List<List<object>> ruleBookList = new List<List<object>>();
            try
            {
                string rulePath = path;
                StreamReader ruleDataStreamReader = new StreamReader(rulePath);

                while (!ruleDataStreamReader.EndOfStream)
                {
                    var ruleLine = ruleDataStreamReader.ReadLine();
                    string[] ruleLineData = ruleLine.Split(',');
                    List<string> list = ruleLineData.ToList();
                    List<object> ruleDataList = GetRuleData(list);
                    ruleBookList.Add(ruleDataList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
            return ruleBookList;
        }



        private List<List<Object>> getJsonData(List<DataUnit> Data)
        {
           List<List<object>> jsonValuesList = new List<List<object>>();
            try
            {
                List<object> jsonValues = new List<object>();
                for (int i = 0; i < Data.Count; i++)
                {
                    object[] values = new object[3];
                    int j = 0;
                    values[j] = Data[i].signal;
                    values[++j] = Data[i].value_type;
                    string dataType = Data[i].value_type.ToLowerInvariant();
                    switch (dataType)
                    {
                        case "integer":
                            if (double.TryParse(Data[i].value, out double resultInteger)) { values[++j] = resultInteger; }
                            else { values[++j] = Data[i].value; }
                            break;

                        case "datetime":
                            if (DateTime.TryParse(Data[i].value, out DateTime resultDateTime)) { values[++j] = resultDateTime; }
                            else { values[++j] = Data[i].value; }
                            break;

                        default:
                            values[++j] = Data[i].value;
                            break;
                    }
                    jsonValues = values.ToList();
                    jsonValuesList.Add(jsonValues);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return jsonValuesList;
        }

        private  List<object> GetRuleData(List<string> list)
        {
            List<object> ruleValuesList = new List<object>();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    List<object> values = new List<object>();
                    int j = 0;
                    values.Add(list[i]);
                    values.Add(list[++i]);
                    string dataType = list[i].ToLowerInvariant();
                    switch (dataType)
                    {
                        case "integer":
                            if (double.TryParse(list[++i], out double resultInteger))
                            {
                                values.Add(resultInteger);
                            }
                            else
                            {
                                values.Add(list[++i]);
                            }
                            break;

                        case "datetime":
                            if (DateTime.TryParse(list[++i], out DateTime resultDateTime))
                            {
                                values.Add(resultDateTime);
                            }
                            else
                            {
                                values.Add(list[++i]);
                            }
                            break;

                        default:
                            values.Add(list[++i]);
                            break;
                    }
                    values.Add(list[++i]);
                    ruleValuesList = values;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ruleValuesList;
        }

        private void printVoilatedSignalData(List<object>resultValue)
        {
            if (resultValue.Count > 0)
            {
                Console.WriteLine("The signal that voilates the rule");
                foreach (var value in resultValue)
                {
                    Console.WriteLine(value);
                }
            }
            Console.ReadLine();
        }


        public static void Main(string[] args)
        {
          RuleEngineImplementation ruleEngine = new RuleEngineImplementation();
          var jsonData =  ruleEngine.ReadJsonFile("raw_data.json");
          var ruleData= ruleEngine.ReadRule("RuleBook.csv");
          Comparison comparison = new Comparison();
          var resultValue = comparison.DoComparison(jsonData, ruleData);
          ruleEngine.printVoilatedSignalData(resultValue);
        }
    }
 }


 

