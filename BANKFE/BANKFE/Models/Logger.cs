using BANKFE.Models;
using System;
using System.IO;

namespace BANKFE.Services
{
    public class Logger
    {
        
        private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Bank_log\" ;
        
        public DateTime dtm_crt { get; set; }
        public string username { get; set; }
        public string module { get; set; }
        public string message { get; set; }
        public string file_name { get; set; }

       
        public void createLog() {
            dtm_crt = DateTime.Now;
            file_name = dtm_crt.ToString("ddMMyyyy") + ".txt";
            
            FileStream fs = null;
            if (!File.Exists(path + file_name)) {
                fs = File.Create(path + file_name);
                fs.Close();
                using (StreamWriter sm = File.AppendText(path + file_name))
                {
                    sm.WriteLine(dtm_crt + "\tServer Up!");
                    if (message != null) {
                        sm.WriteLine(dtm_crt+"\t"+username+"\t"+module+"\t"+message);
                    }
                } 
            }
            else
            {
                using (StreamWriter sm = File.AppendText(path + file_name))
                {
                    sm.WriteLine(dtm_crt + "\t" + username + "\t" + module + "\t" + message);
                }
                
            }
        }
    }
}
