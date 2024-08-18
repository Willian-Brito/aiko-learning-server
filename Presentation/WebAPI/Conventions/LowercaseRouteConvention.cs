using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AikoLearning.Presentation.WebAPI.Conventions;

public class LowercaseRouteConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var controllerName = controller.ControllerName.ToLower();
            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel != null)
                {
                    selector.AttributeRouteModel.Template = 
                        selector.AttributeRouteModel.Template.Replace("[controller]", controllerName);
                }
            }
        }
    }
}
