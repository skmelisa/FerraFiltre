using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ExcelDataReader;

public class FiltreData
{
    public string FerraNo { get; set; }
    public string FirmaAdi { get; set; }
    public string FiltreNo { get; set; }
    public string FiltreNoGoster { get; set; }
    public string Katalog { get; set; }
    public string OrjinalMuadil { get; set; }
}

public class ExcelService
{
    public static List<FiltreData> ReadExcel(string filePath)
    {
        List<FiltreData> list = new List<FiltreData>();
 System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
           

            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();
                DataTable dt = result.Tables[0]; 

                foreach (DataRow row in dt.Rows.Cast<DataRow>().Skip(1)) 
                {
                    list.Add(new FiltreData
                    {
                        FerraNo = row[0].ToString(),
                        FirmaAdi = row[1].ToString(),
                        FiltreNo = row[2].ToString(),
                        FiltreNoGoster = row[3].ToString(),
                        Katalog = row[4].ToString(),
                        OrjinalMuadil = row[5].ToString()
                    });
                }
            }
        }
        return list;
    }
}
