using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ConnectOneMVC.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace ConnectOneMVC.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}