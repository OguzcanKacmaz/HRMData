using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HRMData.WEB.Entensions
{
    public static class ModelStateExtansion
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState, List<string> error)
        {
            error.ForEach(x =>
            {
                modelState.AddModelError(string.Empty, x);

            });
        }
    }
}
