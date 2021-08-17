using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cursomvcapi.Models.WS;
using cursomvcapi.Models;

namespace cursomvcapi.Controllers
{
    public class AnimalController : BaseController
    {
        [HttpPost]
        public Reply get([FromBody]SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }                

            try
            {
                using (cursomvcapiEntities db = new cursomvcapiEntities())
                {
                    List<ListAnimalsViewModels> lst = (from d in db.animal
                                                      where d.idState == 1
                                                      select new ListAnimalsViewModels
                                                      {
                                                          Name = d.name,
                                                          Patas = d.patas
                                                      }).ToList();
                    oR.data = lst;
                    oR.result = 1;
                }
            }
            catch
            {
                oR.message = "Ocurrio un error en el servidor, intenta más tarde";
            }
            return oR;
        }


    }
}
