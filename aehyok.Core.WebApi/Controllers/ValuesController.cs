﻿using aehyok.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public dynamic Get()
        {
            ResultModel result = new ResultModel();
            result.Code = "200";
            result.Msg = "OK";
            result.Data = new Dictionary<string, string>();
            return result;
        }
    }
}
