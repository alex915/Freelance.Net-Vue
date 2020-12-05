using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProyectoOesia.Services.HereService;

namespace ProyectoOesia.Services
{
  public  interface IHereService
    {

        Task<GeoCodingArray> SearchGeocoding(string address, string city, string postalcode);
    }
}
