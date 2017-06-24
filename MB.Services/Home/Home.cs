using System;
using MB.Services.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;




namespace MB.Services.Home
{
    
    public class gare
    {

       
        static public string  getdata()
        {
            MBEntities mbdata = new MBEntities();



            List<area>  mblist = mbdata.areas.ToList();

            string result = JsonConvert.SerializeObject(mblist);


            return result;
        }



    }
  

   
}
