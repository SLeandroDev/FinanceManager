using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;

namespace FinanceManager.Filters
{
    public class AuthFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

            // Obtém o nome da ação e do controlador sendo executados
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var controllerName = context.ActionDescriptor.RouteValues["controller"];

            // Permitir acesso às páginas de Login e Index sem autenticação
            if ((controllerName == "Home" && (actionName == "Login" || actionName == "Index")))
            {
                return; // Permite o acesso a essas páginas sem verificar o token
            }


            var token = context.HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Nada necessário aqui por enquanto.
        }
    }
}
