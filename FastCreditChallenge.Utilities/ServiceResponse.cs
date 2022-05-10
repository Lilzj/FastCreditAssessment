using Microsoft.AspNetCore.Mvc;

namespace FastCreditChallenge.Utilities
{
    public class ServiceResponse
    {
        public static ObjectResult BadRequest(object error)
        {
            return new BadRequestObjectResult(error);
        }

        public static ObjectResult Ok(object res)
        {
            return new OkObjectResult(res);
        }

        public static ObjectResult NotFound(object res)
        {
            return new NotFoundObjectResult(res);
        }

        public static ObjectResult Created(string routeName, object routeValues, object value)
        {
            return new CreatedAtRouteResult(routeName, routeValues, value);
        }

        public static StatusCodeResult NoContent()
        {
            return new NoContentResult();
        }
    }
}