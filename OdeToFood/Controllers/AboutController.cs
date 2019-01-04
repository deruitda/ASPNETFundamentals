using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    //[controller] is the name of the controller class (about)
    //[action] is the method name 
    [Route("[controller]/[action]")]
    public class AboutController
    {
        //blank route means if you just go to /about then it will go to this method
        [Route("")]
        
        public string Phone()
        {
            return "5555555";
        }

        [Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}
