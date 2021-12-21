﻿using BANKFE.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace BANKFE.Interface
{
    public interface IHttpServices
    {
        public Task<string> GetData(string url);
        public Task<ResultModel> PostData(string url, object data);
    }
}
