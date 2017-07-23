using System.Web.Http;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Test;

namespace DynamicQuery.Net.Web.Controllers.api
{
    public class QueryController:ApiController
    {
        public IHttpActionResult MultiValueInput(DynamicQueryNetInput input)
        {
            return Ok(Mock.QueryableItems.Filter(input));
        }
    }
}