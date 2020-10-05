using System.Linq;
using FootTrack.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FootTrack.Api.Utils
{
    public static class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState.First(x => x.Value.Errors.Count > 0);
            string errorSerialized = entry.Errors.First().ErrorMessage;

            Error error = Error.Deserialize(errorSerialized);
            Envelope envelope = Envelope.Error(error, fieldName);
            var result = new BadRequestObjectResult(envelope);

            return result;
        }
    }
}