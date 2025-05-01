using VC.Tenants.Application;
<<<<<<< HEAD
using VC.Tenants.Entities;
=======
>>>>>>> 6b80562d921f43934bfbd8a30494f15a581d28d7

namespace VC.Tenants.Infrastructure.Implementations;

internal class DyDateCodeGenerator : IEmailVerifyCodeGenerator
{
    public string GenerateCode()
    {
        int hashCode = DateTime.UtcNow.GetHashCode();
<<<<<<< HEAD
        string stringHashCode = hashCode > 0 ? hashCode.ToString() : (-hashCode).ToString();

        if(stringHashCode.Length > EmailVerification.CodeMaxLenght)
            stringHashCode = stringHashCode.Substring(0, EmailVerification.CodeMaxLenght - 1);

        return stringHashCode;
=======
        return hashCode > 0 ? hashCode.ToString() : (-hashCode).ToString();
>>>>>>> 6b80562d921f43934bfbd8a30494f15a581d28d7
    }
}